# ReplyVeys 📧

Outlook için akıllı e-posta cevap asistanı. Seçili e-postayı analiz edip hızlı ve tutarlı Türkçe cevap taslağı üretir.

## ✨ Özellikler

- **Tek tıkla taslak üretimi**: Ribbon'daki butona tıklayarak seçili e-postaya cevap taslağı oluşturur
- **Özelleştirilebilir yanıtlar**: Farklı ton ve stil seçenekleri (resmi/samimi)
- **Hızlı entegrasyon**: Taslağı direkt Outlook yanıt penceresine ekler
- **LLM desteği**: Mock implementasyon ile gerçek AI model entegrasyonuna hazır

## 🚀 Nasıl Çalışır

1. Outlook'ta bir e-posta seçin
2. Ribbon'daki **"Cevap Oluştur"** butonuna tıklayın
3. Sağ tarafta açılan panelde taslağı görüntüleyin ve düzenleyin
4. **"Uygula"** ile yanıt penceresini açın veya **"Gönder"** ile direkt gönderin

## 🛠️ Teknoloji

- **C#** (.NET Framework 4.8)
- **VSTO** Outlook Add-in
- **Microsoft.Office.Interop.Outlook** (Office 15/16 uyumlu)
- **WinForms** + Ribbon XML + Custom Task Pane
- **ClickOnce** dağıtım

## 📋 Gereksinimler

- Microsoft Outlook 2016 veya üzeri
- .NET Framework 4.8
- Visual Studio (geliştirme için)

## 🚀 Kurulum

1. Projeyi klonlayın:
```bash
git clone https://github.com/yourusername/replyveys.git
```

2. Visual Studio ile açın ve derleyin

3. **Setup** klasöründeki `setup.exe` ile kurulum yapın

## 🔧 Geliştirme

### Proje Yapısı
- `ThisAddIn.cs` - Ana eklenti mantığı
- `ReplyRibbon.cs` - Ribbon arayüzü
- `ReplyPane.cs` - Yan panel kontrolü
- `Llm.cs` - LLM entegrasyon arayüzü

### LLM Entegrasyonu
Şu an `MockLlmClient` kullanılıyor. Gerçek AI model entegrasyonu için:

```csharp
public class YourLlmClient : ILlmClient
{
    public async Task<string> GenerateReplyAsync(string subject, string body, string from)
    {
        // AI API çağrısı burada
    }
}
```

`ThisAddIn_Startup` metodunda `_llm = new YourLlmClient();` olarak değiştirin.



## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.


**Not**: Bu proje eğitim amaçlı geliştirilmiştir. Üretim ortamında kullanmadan önce güvenlik ve gizlilik politikalarını gözden geçirin.
