using System;
using System.Threading.Tasks;
using System.Web;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace ReplyVeys
{
    public partial class ThisAddIn
    {
        private ILlmClient _llm;
        private Outlook.MailItem _lastReplyWindow;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _llm = new MockLlmClient();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        // Ribbon'ı bağla
        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new ReplyRibbon();
        }

        // Seçili maili al
        private Outlook.MailItem GetSelectedMail()
        {
            var explorer = this.Application?.ActiveExplorer();
            if (explorer?.Selection != null && explorer.Selection.Count > 0)
                return explorer.Selection[1] as Outlook.MailItem;
            return null;
        }

        // GenerateDraftAsync kaldırıldı; akış doğrudan SuggestIntoCurrentComposeAsync üzerinden

        // Açık yanıt penceresine öneriyi göm
        public async Task SuggestIntoCurrentComposeAsync()
        {
            // Öncelik: Okuma bölmesinde inline reply kullan
            Outlook.Explorer explorer = this.Application?.ActiveExplorer();
            if (explorer == null) return;

            Outlook.MailItem compose = explorer.ActiveInlineResponse as Outlook.MailItem;

            if (compose == null)
            {
                // Inline yanıt başlat (popup açmadan)
                try
                {
                    // Reply All tetikle; kullanıcı ayarları inline ise Reading Pane içinde açılır
                    (explorer.CommandBars as Office.CommandBars)?.ExecuteMso("MailReplyAll");
                }
                catch { }

                compose = explorer.ActiveInlineResponse as Outlook.MailItem;
                if (compose == null)
                {
                    // Fallback: inline açılamazsa sessizce çık
                    return;
                }
            }

            // Kaynak maili belirle (PR prefix'li yanıt gövdesinden önceki bölüm)
            string originalSubject = compose.Subject?.Replace("RE:", "").Replace("Re:", "").Trim() ?? "";

            // Orijinal e-posta içeriğini bulmak için mevcut HTMLBody'deki alıntı kısmını da gönderebiliriz
            string plainForModel = compose.Body ?? compose.HTMLBody ?? "";
            string from = this.Application?.Session?.CurrentUser?.Name ?? "";

            string draft = await _llm.GenerateReplyAsync(originalSubject, plainForModel, from);
            string htmlDraft = HttpUtility.HtmlEncode(draft).Replace("\n", "<br/>");
            string insertHtml = $"<div style=\"font-size:12.0pt; mso-bidi-font-size:12.0pt; line-height:normal;\">{htmlDraft}</div><br>";

            compose.HTMLBody = $"{insertHtml}{compose.HTMLBody}";
            await ForceTopParagraphFontSizeAsync(compose, 12f);
            await ForceSelectionFontSizeAsync(compose, 12f);
            _lastReplyWindow = compose;
        }

        // Panel akışı kaldırıldı

        private async Task ForceTopParagraphFontSizeAsync(Outlook.MailItem mail, float points)
        {
            try
            {
                await Task.Delay(200);
                var inspector = mail?.GetInspector;
                Word.Document doc = inspector?.WordEditor as Word.Document;
                if (doc == null)
                {
                    var explorer = this.Application?.ActiveExplorer();
                    doc = explorer?.ActiveInlineResponseWordEditor as Word.Document;
                }
                if (doc == null) return;
                Word.Range main = doc.StoryRanges[Word.WdStoryType.wdMainTextStory];
                if (main == null || main.Paragraphs == null || main.Paragraphs.Count == 0) return;
                int limit = main.Paragraphs.Count < 5 ? main.Paragraphs.Count : 5;
                for (int i = 1; i <= limit; i++)
                {
                    Word.Range r = main.Paragraphs[i].Range;
                    if (r != null) r.Font.Size = points;
                }
            }
            catch { }
        }

        private async Task ForceSelectionFontSizeAsync(Outlook.MailItem mail, float points)
        {
            try
            {
                await Task.Delay(50);
                var inspector = mail?.GetInspector;
                Word.Document doc = inspector?.WordEditor as Word.Document;
                if (doc == null)
                {
                    var explorer = this.Application?.ActiveExplorer();
                    doc = explorer?.ActiveInlineResponseWordEditor as Word.Document;
                }
                if (doc == null) return;

                Word.Application wordApp = doc.Application;
                Word.Selection sel = wordApp?.Selection;
                if (sel == null) return;

                sel.HomeKey(Word.WdUnits.wdStory);
                sel.MoveEnd(Word.WdUnits.wdParagraph, 1);
                sel.Font.Size = points;
            }
            catch { }
        }

        

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}