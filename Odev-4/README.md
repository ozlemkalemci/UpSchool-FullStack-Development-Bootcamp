# UpSchool unit test ödevi adım adım anlatımı:

1. İlk olarak, `FakeItEasy` adlı bir kütüphaneyi kullanmak için `using` ifadelerini ekliyoruz.
2. Ardından, `UpSchool.Domain.Data`, `UpSchool.Domain.Entities` ve `UpSchool.Domain.Services` isim alanlarını ekliyoruz.
3. `UserServiceTests` adında bir test sınıfı tanımlıyoruz.
4. `GetUser_ShouldGetUserWithCorrectId` adlı bir test metodu tanımlıyoruz.
   - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
   - `Guid` türünden bir `userId` oluşturuyoruz.
   - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
   - Beklenen kullanıcıyı temsil eden bir `expectedUser` örneği oluşturuyoruz ve `Id` özelliğini `userId` olarak ayarlıyoruz.
   - `userRepositoryMock` üzerinde `GetByIdAsync` metodunu çağırdığımızda `expectedUser` örneğini döndürmesini sağlıyoruz.
   - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
   - `userService` üzerinde `GetByIdAsync` metodunu çağırarak kullanıcıyı alıyoruz.
   - Beklenen kullanıcıyla alınan kullanıcıyı karşılaştırarak doğrulama yapıyoruz.
5. `AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull` adlı bir test metodu tanımlıyoruz.
   - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
   - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
   - `Guid` türünden bir `userId` oluşturuyoruz.
   - Bir kullanıcı örneği olan `user` oluşturuyoruz ve `Email` özelliğini boş bir dize olarak ayarlıyoruz.
   - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
   - `userRepositoryMock` üzerinde `AddAsync` metodunu çağırdığımızda `ArgumentException` fırlatmasını sağlıyoruz.
   - `userService` üzerinde `AddAsync` metodunu çağırırken, `user` örneğinin bilgilerini geçiriyoruz ve `ArgumentException` fırlatılmasını bekliyoruz.
6. `AddAsync_ShouldntReturn_NullUserId` adlı bir test metodu tanımlıyoruz.
   - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
   - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
   - Bir kullanıcı örneği olan `user` oluşturuyoruz ve `Id` özelliğine yeni bir `Guid` değeri atıyoruz.
   - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
   - `userService` üzerinde `AddAsync` metodunu çağırarak kullanıcıyı ekliyoruz ve dönen kullanıcı kimliğini `addedUserId` değişkeninde saklıyoruz.
   - `addedUserId` değerini `Guid.Empty` ile karşılaştırarak doğrulama yapıyoruz.
7. `DeleteAsync_ShouldReturnTrue_WhenUserExists` adlı bir test metodu tanımlıyoruz.
   - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
   - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
   - Bir kullanıcı örneği olan `user` oluşturuyoruz ve `Id` özelliğine yeni bir `Guid` değeri atıyoruz.
   - `userRepositoryMock` üzerinde `DeleteAsync` metodunu çağırdığımızda 1 döndürmesini sağlıyoruz.
   - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
   - `userService` üzerinde `DeleteAsync` metodunu çağırarak kullanıcıyı silmeyi deneriz ve sonucun `true` olduğunu doğrularız.
8. `DeleteAsync_ShouldThrowException_WhenUserDoesntExists` adlı bir test metodu tanımlıyoruz.
   - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
   - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
   - Bir kullanıcı örneği olan `user` oluşturuyoruz ve `Id` özelliğine yeni bir `Guid` değeri atıyoruz.
   - `userRepositoryMock` üzerinde `DeleteAsync` metodunu çağırdığımızda `ArgumentException` fırlatmasını sağlıyoruz.
   - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
   - `userService` üzerinde `DeleteAsync` metodunu çağırırken, `user` örneğinin kimliğini geçiriyoruz ve `ArgumentException` fırlatılmasını bekliyoruz.
9. `UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty` adlı bir test metodu tanımlıyoruz.
   - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
   - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
   - Bir kullanıcı örneği olan `user` oluşturuyoruz ve `Id` özelliğini `Guid.Empty` olarak ayarlıyoruz.
   - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
   - `userRepositoryMock` üzerinde `UpdateAsync` metodunu çağırdığımızda `ArgumentException` fırlatmasını sağlıyoruz.
   - `userService` üzerinde `UpdateAsync` metodunu çağırırken, `user` örneğini geçiriyoruz ve `ArgumentException` fırlatılmasını bekliyoruz.
10. `UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull` adlı bir test metodu tanımlıyoruz.
    - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
    - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
    - Boş bir `Email` değerine sahip `userEmpty` adlı bir kullanıcı örneği oluşturuyoruz.
    - `null` bir `Email` değerine sahip `userNull` adlı bir kullanıcı örneği oluşturuyoruz.
    - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
    - `userRepositoryMock` üzerinde `UpdateAsync` metodunu çağırdığımızda `ArgumentException` fırlatmasını sağlıyoruz.
    - `userService` üzerinde `UpdateAsync` metodunu çağırırken, `userEmpty` ve `userNull` örneklerini geçirerek `ArgumentException` fırlatılmasını bekliyoruz.
11. `GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords` adlı bir test metodu tanımlıyoruz.
    - `IUserRepository` türünde bir `userRepositoryMock` örneği oluşturuyoruz.
    - Bir `CancellationTokenSource` nesnesi oluşturuyoruz.
    - Bir kullanıcı listesi olan `userList` oluşturuyoruz.
    - `IUserService` türünden bir `userService` örneği oluşturuyoruz ve `userRepositoryMock` ile başlatıyoruz.
    - `userRepositoryMock` üzerinde `GetAllAsync` metodunu çağırdığımızda `userList` örneğini döndürmesini sağlıyoruz.
    - `userService` üzerinde `GetAllAsync` metodunu çağırarak tüm kullanıcıları alıyoruz.
    - Elde edilen sonucun `null` olmadığını ve en az iki kayıt içerdiğini doğruluyoruz.

Bu şekilde, `UserServiceTests` sınıfında adım adım anlatılan test metotları ile kullanıcı hizmetlerinin doğru bir şekilde çalışıp çalışmadığını test ediyoruz.
