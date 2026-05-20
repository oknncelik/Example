# Proje Talimatları - Example

Bu belge, **Example** projesi için temel rehber ve standartları içerir. Projenin tutarlılığını ve mimari bütünlüğünü korumak için bu standartlara uyunuz.

## Proje Özeti
Bu proje, Bağımlılık Enjeksiyonu (DI) için **Autofac** ve Enine Kesen İlgiler (AOP) için **Castle DynamicProxy** kullanan çok katmanlı (N-Tier) bir ASP.NET Core Web API projesidir.

## Mimari ve Katmanlar
Proje şu katmanlardan oluşur:

- **Example.Api:** Giriş noktası. Modern birleşik `Program.cs` yapısı kullanılır.
- **Example.Business:** İş mantığı (Business Logic) katmanı.
- **Example.Dal:** Veri Erişim Katmanı (Data Access Layer).
- **Example.Entities:** Domain varlıkları (Entities) ve Veri Transfer Nesneleri (DTOs).
- **Example.Common:** Paylaşılan çapraz kesen ilgiler (Cross-cutting concerns), yardımcı sınıflar ve sonuç modelleri.
- **Example.Core:** Bağımlılık Enjeksiyonu yapılandırması (Autofac).

## Teknoloji Yığını
- **Framework:** .NET 10.0
- **DI Konteynırı:** Autofac & Autofac.Extensions.DependencyInjection
- **AOP:** Castle DynamicProxy
- **ORM:** Entity Framework Core 10.0
- **Veritabanı:** Microsoft SQL Server
- **Kimlik Doğrulama:** JWT (JSON Web Tokens)
- **Dokümantasyon:** Swagger/OpenAPI (Swashbuckle)
- **Konteynırlaştırma:** Docker & Docker Compose

## Gelişmiş Özellikler

### 1. Otomatik Yetki Kayıt Sistemi (`ClaimSeedService`)
Proje, metotlar üzerindeki `[Auth("...")]` niteliklerini (attribute) tarayan ve veritabanındaki `OperationClaims` tablosuna otomatik olarak kaydeden bir mekanizmaya sahiptir. Yeni bir yetki eklemek için sadece ilgili metoda niteliği eklemeniz yeterlidir.

### 2. Dinamik Yetkilendirme (`AuthAttribute`)
AOP kullanılarak metot düzeyinde yetki kontrolü yapılır. Yetkisiz erişimlerde sistem otomatik olarak `403 Forbidden` yanıtı döner ve asenkron metotları güvenli bir şekilde yönetir.

### 3. Performans Optimizasyonları
- **AsNoTracking:** Okuma işlemlerinde EF Core takip mekanizması kapatılarak bellek kullanımı optimize edilmiştir.
- **Caching:** `MemoryCacheManager` üzerinden desen tabanlı (pattern-based) önbellek yönetimi sağlanmıştır.
- **DbContext Yönetimi:** Veritabanı bağlantıları bağlantı havuzu verimliliği için optimize edilmiştir.

## Kurulum ve Çalıştırma

### Docker ile Başlatma
Proje Docker ile tam uyumludur. Tüm servisleri (API + SQL Server) tek komutla başlatabilirsiniz:

```bash
docker-compose up --build
```

- **API:** http://localhost:5000 (Otomatik Swagger açılır)
- **SQL Server Port:** 1433
- **JWT Anahtarı:** Güvenlik standartları gereği 256-bit+ anahtar kullanılmaktadır.

### Veritabanı Yapılandırması
Uygulama ilk kez ayağa kalkarken:
1. Veritabanının varlığını kontrol eder, yoksa oluşturur.
2. `Auth` niteliklerini tarayarak yetkileri tablolarına işler.
3. Docker ortam değişkenlerinden (`ConnectionStrings__DefaultConnection`) bağlantı bilgilerini otomatik alır.

## Kodlama Standartları
- **İsimlendirme:** Sınıf ve metot isimlendirmelerinde PascalCase kullanılır.
- **Async/Await:** Tüm katmanlarda asenkron programlama zorunludur.
- **Sonuç Deseni (Result Pattern):** Business metodları her zaman `IResult` veya `IDataResult<T>` dönmelidir.
- **Mapping:** Nesne dönüşümleri için **AutoMapper** kullanılmalıdır.
