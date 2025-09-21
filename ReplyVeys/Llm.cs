using System;
using System.Threading.Tasks;

namespace ReplyVeys
{
    public interface ILlmClient
    {
        Task<string> GenerateReplyAsync(string subject, string body, string from);
    }

    public class MockLlmClient : ILlmClient
    {
        public Task<string> GenerateReplyAsync(string subject, string body, string from)
        {
            var urgent = body?.IndexOf("acil", StringComparison.OrdinalIgnoreCase) >= 0;
            var greeting = "Merhaba,";
            var main = urgent
                ? "Talebinizi aldım. En kısa sürede dönüş yapacağım. Bugün 15:00 için kısa bir görüşme ayarlayabilirim."
                : "Mesajınız için teşekkürler. Detayları inceledim ve gerekli aksiyonları başlatıyorum. İlerlemeyi paylaşacağım.";
            var closing = "İyi çalışmalar,\n[İsminiz]";
            return Task.FromResult($"{greeting}\n\n{main}\n\n{closing}");
        }
    }
}