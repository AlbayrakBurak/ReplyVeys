using System;
using System.Threading.Tasks;
using System.Web;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace ReplyVeys
{
    public partial class ThisAddIn
    {
        private Microsoft.Office.Tools.CustomTaskPane _taskPane;
        private ReplyPane _paneControl;
        private ILlmClient _llm;
        private Outlook.MailItem _lastReplyWindow;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _llm = new MockLlmClient();

            _paneControl = new ReplyPane();
            _paneControl.ApplyClicked += Pane_ApplyClicked;
            _paneControl.SendClicked += Pane_SendClicked;

            _taskPane = this.CustomTaskPanes.Add(_paneControl, "Cevap Taslağı");
            _taskPane.Visible = false;
            _taskPane.Width = 420;
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

        // Ribbon butonundan çağrılacak
        public async Task GenerateDraftAsync()
        {
            var mail = GetSelectedMail();
            if (mail == null) return;

            string subject = mail.Subject ?? "";
            string body = mail.Body ?? mail.HTMLBody ?? "";
            string from = $"{mail.SenderName} <{mail.SenderEmailAddress}>";

            _paneControl.SetEnabled(false);
            _taskPane.Visible = true;

            string draft = await _llm.GenerateReplyAsync(subject, body, from);
            _paneControl.DraftText = draft;
            _paneControl.SetEnabled(true);
        }

        // Task Pane'deki "Uygula" butonu
        private void Pane_ApplyClicked(object sender, EventArgs e)
        {
            var selected = GetSelectedMail();
            if (selected == null) return;

            var reply = selected.ReplyAll();
            string htmlDraft = HttpUtility.HtmlEncode(_paneControl.DraftText).Replace("\n", "<br/>");
            reply.HTMLBody = $"<p>{htmlDraft}</p><br/><br/>{reply.HTMLBody}";
            reply.Display();
            _lastReplyWindow = reply;
        }

        // Task Pane'deki "Gönder" butonu
        private void Pane_SendClicked(object sender, EventArgs e)
        {
            if (_lastReplyWindow == null)
                Pane_ApplyClicked(sender, e);

            _lastReplyWindow?.Send();
            _lastReplyWindow = null;
            _paneControl.SetEnabled(false);
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