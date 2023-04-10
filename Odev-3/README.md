# Ödev için yapılan işlemler sırasıyla aşağıdaki gibidir:
________________________________

User-Address işlemleri
________________________________

-User.cs:  ICollection Address için kod satırı eklendi

-AddressConfiguration: Address-User ilişkisi için gerekli kod satırları eklendi. (bire-çok/one-to-many)


________________________________

Note ve NoteCategory işlemleri
________________________________

-NoteCategory: Note.cs ve Category.cs göz önüne alınarak dolduruldu

-Note.cs ve Category.cs: İkisine de ICollection NoteCategories kod satırı eklendi

-NoteConfiguration: Oluşturuldu

-NoteCategoryConfiguration: Oluşturularak Note ve Category arasındaki ilişki sağlandı. (çoka-çok/many-to-many)

________________________________

CQRS işlemleri
________________________________

-Command ve Queries klasörleri: Application/Features/Addresses dosya yoluna oluşturuldu. Add, Update, Delete, HardDelete Command’leri ve GetById, GetAll Query’leri burada yer almaktadır.

-IApplicationDbContext ve ApplicationDbContext: DbSetler için kod satırı eklendi

-Controllers: WebAPI içerisinde bulunan bu klasöre AddressesController oluşturarak gerekli işlemleri gerçekleştirildi
