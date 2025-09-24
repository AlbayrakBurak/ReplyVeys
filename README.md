# ReplyVeys 📧

Outlook için akıllı e-posta cevap asistanı. Seçili e-postayı analiz edip hızlı ve tutarlı Türkçe cevap taslağı üretir.

## ✨ Özellikler

- **Tek tıkla öneri**: Seçili e-posta için inline yanıtı otomatik başlatır ve öneriyi ekler
- **12pt görünüm**: Öneri metni 12pt olarak en üste eklenir
- **Hızlı entegrasyon**: Inline yanıt açılamazsa otomatik pencere (fallback) ile ekleme yapar
- **LLM desteği**: Mock implementasyon ile gerçek AI model entegrasyonuna hazır

## 🚀 Nasıl Çalışır

1. Outlook'ta bir e-posta seçin
2. Ribbon'da **Akıllı Yanıt > Öneri Hazırla**'ya tıklayın
3. Eklenti inline yanıtı otomatik başlatır ve öneriyi en üste 12pt olarak ekler
4. Inline yanıt kapalıysa, eklenti otomatik bir yanıt penceresi açıp öneriyi ekler

## 🛠️ Teknoloji

- **C#** (.NET Framework 4.8)
- **VSTO** Outlook Add-in
- **Microsoft.Office.Interop.Outlook** (Office 15/16 uyumlu)
- **Ribbon XML** (tek sekme/tek buton akışı)
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

2. Visual Studio ile açın ve derleyin (F5 ile Outlook eklentiyle başlar)

3. Yayın/kurulum için: **Setup** klasöründeki `setup.exe` ile kurulum yapın
   - İlk çalıştırmada Outlook > File > Options > Add-ins > COM Add-ins > Go… içinde “ReplyVeys” etkin olmalı

## 🔧 Geliştirme

### Proje Yapısı
- `ThisAddIn.cs` - Ana eklenti mantığı (inline başlatma, öneri ekleme, 12pt ayarı)
- `ReplyRibbon.cs` - Ribbon arayüzü (buton handler)
- `ReplyRibbon.xml` - Ribbon XML tanımı
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

### Sorun Giderme
- Inline yanıt açılmıyor: Outlook Ayarları > Mail > Reply/Forward > “Reading Pane’de yanıtla/ilet”i açın. Açık değilse eklenti otomatik yanıt penceresi açar.
- Yazı boyutu farklı görünüyor: Eklenti 12pt’i hem HTML hem Word editörü üzerinden zorlar. Görüntü aynı kalmalı.
- “Yeni Outlook” modunda VSTO eklentileri desteklenmez. Klasik Outlook’ta çalıştırın.

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

**Not**: Bu proje eğitim amaçlı geliştirilmiştir. Üretim ortamında kullanmadan önce güvenlik ve gizlilik politikalarını gözden geçirin.
