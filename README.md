# Example Project - ASP.NET Core Multi-Layered Architecture

Bu proje, modern yazılım prensipleri (SOLID, Clean Code) ve en iyi uygulamalar kullanılarak geliştirilmiş, yüksek ölçeklenebilir ve bakımı kolay bir **ASP.NET Core 10.0 Web API** şablonudur.

## 🚀 Öne Çıkan Özellikler

- **Çok Katmanlı Mimari (N-Tier):** Sorumlulukların net ayrımı için Entities, Dal, Business, Api ve Common katmanları.
- **Otomatik Bağımlılık Enjeksiyonu (DI):** [Autofac](https://autofac.org/) kullanarak Assembly Scanning ile otomatik servis kaydı.
- **AOP (Aspect Oriented Programming):** [Castle DynamicProxy](http://www.castleproject.org/projects/dynamicproxy/) ile metot düzeyinde merkezi **Auth**, **Log**, **Cache** ve **Validation** yönetimi.
- **Dinamik Yetkilendirme:** Token bağımlılığı olmayan, her istekte veritabanı kontrollü ve kısa süreli önbellek (MemoryCache) destekli yetki kontrolü.
- **Otomatik Yetki Kayıt Sistemi (`ClaimSeedService`):** Kod içerisindeki `[Auth]` niteliklerini tarayarak veritabanına otomatik yetki ve açıklama (description) tanımlama.
- **JWT Kimlik Doğrulama:** Güvenli ve standartlara uygun JSON Web Token altyapısı.
- **Docker Desteği:** Tek komutla (API + SQL Server) tüm ortamın ayağa kaldırılması.

## 🏗️ Mimari Yapı

| Katman | Sorumluluk |
| :--- | :--- |
| **Example.Api** | Uygulamanın giriş noktası, Controller'lar ve Middleware yapılandırmaları. |
| **Example.Business** | İş mantığı (Business Logic), validasyonlar ve servis implementasyonları. |
| **Example.Dal** | Veri erişim katmanı, EF Core Repository implementasyonları ve DbContext. |
| **Example.Entities** | Veritabanı varlıkları (Entities) ve veri transfer nesneleri (DTOs). |
| **Example.Common** | Çapraz kesen ilgiler (Cross-cutting concerns), yardımcı sınıflar (Helpers) ve sonuç modelleri. |
| **Example.Core** | Merkezi konfigürasyonlar ve Autofac modülleri. |

## 🛠️ Teknoloji Yığını

- **Backend:** .NET 10.0
- **ORM:** Entity Framework Core 10.0
- **Veritabanı:** Microsoft SQL Server
- **DI/AOP:** Autofac & Castle DynamicProxy
- **Mapping:** AutoMapper
- **Dokümantasyon:** Swagger (Swashbuckle)

## 🚦 Hızlı Başlangıç

### Docker ile Çalıştırma
Proje kök dizininde aşağıdaki komutu çalıştırarak tüm sistemi (API + Veritabanı) başlatabilirsiniz:

```bash
docker-compose up --build
```

Uygulama ayağa kalktığında:
1. Veritabanı otomatik olarak oluşturulur.
2. `admin@example.com` / `19Mayis1919!` bilgilerine sahip bir yönetici hesabı oluşturulur.
3. Tüm `[Auth]` nitelikleri taranarak yetki tablosu doldurulur.

**Swagger:** `http://localhost:5000`

## 🔐 Yetki Kullanımı

Bir metoda yetki kontrolü eklemek için sadece `[Auth]` niteliğini eklemeniz yeterlidir:

```csharp
[Auth("Product.Add", "Yeni ürün ekleme yetkisi")]
public async Task<IResult> AddProduct(ProductModel product)
{
    // ...
}
```

*Not: Yetkiler veritabanından her istekte kontrol edilir, ancak performans için 1 dakikalık kısa süreli önbelleğe alınır.*

## 📄 Lisans
Bu proje [MIT](LICENSE) lisansı altında lisanslanmıştır.
