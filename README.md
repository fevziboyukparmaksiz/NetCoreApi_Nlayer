# NetCoreApi_Nlayer

## Proje Açıklaması
Bu proje, **N-Katmanlı Mimari (NLayer Architecture)** kullanılarak .NET Core ile geliştirilmiş bir API şablonudur. Proje, modüler bir yapıda tasarlanmış olup çeşitli yazılım geliştirme bileşenlerini içerir.

## Özellikler
- **N-Katmanlı Mimari**: Uygulama, katmanlar arasında sorumluluk ayrımıyla organize edilmiştir (Sunum, İş, Veri Erişim Katmanları).
- **Repository Design Pattern**: Veri erişim katmanında, veri tabanı işlemlerini yönetmek için Repository Pattern kullanılmıştır.
- **Unit of Work**: Tüm veri erişim işlemleri, Unit of Work tasarım deseni kullanılarak tek bir işlem biriminde yönetilir.
- **Result Pattern**: İşlemlerin sonucunu belirlemek için Result Pattern kullanılmıştır. Her işlem sonucu başarılı ya da hatalı olarak döndürülür.
- **Validasyon**: Veri girişlerinin doğruluğunu sağlamak için FluentValidation kullanılarak çeşitli validasyon kuralları uygulanmıştır. 
- **SaveChanges Interceptor**: Veri tabanı işlemlerini izlemek ve özelleştirmek için kullanılmıştır.
- **Filtreler**: Global filtreler ile veri doğrulama ve iş mantığı süreci yönetilmektedir.
- **AutoMapper**: Nesneler arası dönüşümler için kullanılmıştır.
- **Exception Handlers**: Merkezi hata yönetimi ile istisnalar düzgün bir şekilde ele alınır.
- **Pagination**: API sonuçlarında verileri sayfalayarak döndürmek için Pagination uygulanmıştır.
