using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;

namespace ReplyVeys
{
    [ComVisible(true)]
    public class ReplyRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI _ribbon;

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("ReplyVeys.ReplyRibbon.xml");
        }

        public void OnLoad(Office.IRibbonUI ribbonUI)
        {
            _ribbon = ribbonUI;
        }

        public async void OnGenerateClick(Office.IRibbonControl control)
        {
            // Taslağı panelde göstermek yerine doğrudan mevcut compose içine ekle
            await Globals.ThisAddIn.SuggestIntoCurrentComposeAsync();
        }

        // (Kaldırıldı) Ayrı bir buton gerekmiyor; OnGenerateClick aynı işi yapıyor

        private static string GetResourceText(string resourceName)
        {
            var asm = Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream(resourceName))
            using (var reader = stream != null ? new StreamReader(stream) : null)
                return reader?.ReadToEnd();
        }
    }
}