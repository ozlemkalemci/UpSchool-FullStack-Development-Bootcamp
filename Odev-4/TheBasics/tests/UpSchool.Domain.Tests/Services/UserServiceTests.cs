using FakeItEasy;
using System.Linq.Expressions;
using UpSchool.Domain.Data;
using UpSchool.Domain.Entities;
using UpSchool.Domain.Services;

namespace UpSchool.Domain.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUser_ShouldGetUserWithCorrectId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var cancellationSource = new CancellationTokenSource();

            var expectedUser = new User()
            {
                Id = userId
            };

            A.CallTo(() => userRepositoryMock.GetByIdAsync(userId, cancellationSource.Token))
                .Returns(Task.FromResult(expectedUser));

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.GetByIdAsync(userId, cancellationSource.Token);

            Assert.Equal(expectedUser, user);
        }


        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull()
        {
            // Arrange
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();
            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var user = new User()
            {
                Id = userId,
                FirstName = "firstName",
                LastName = "lastName",
                Age = 30,
                Email = string.Empty,
            };

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            A.CallTo(() => userRepositoryMock.AddAsync(A<User>.Ignored, cancellationSource.Token))
                .Throws(new ArgumentException("Email cannot be null or empty."));

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => userService.AddAsync(user.FirstName, user.LastName, user.Age, user.Email, cancellationSource.Token));
        }

        [Fact]
        public async Task AddAsync_ShouldntReturn_NullUserId()  
        {
            // Ödevin AddAsync_ShouldReturn_CorrectUserId() metod başlığı adımı değiştirilerek bu şekilde yapılmıştır.
            // AddAsync metodu içinde new guid generate edip döndürüldüğü için istenen metod başlığına göre test yapılamamaktadır.
            // Id boş olmamalı diyerek bu testi yapıyorum ve eğer id boş değilse testi geçmesini istiyorum.
            // Guid nullable olmadığı için sadece empty için kontrol gerçekleştiriyorum:


            // Arrange
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();
            
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "firstName",
                LastName = "lastName",
                Age = 30,
                Email = "ozlemkalemci@upschool.com",
            };

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            var addedUserId = await userService.AddAsync(user.FirstName, user.LastName, user.Age, user.Email, cancellationSource.Token);


            // Assert
            Assert.NotEqual(Guid.Empty, addedUserId);

        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "firstName",
                LastName = "lastName",
                Age = 30,
                Email = "ozlemkalemci@upschool.com",
            };

            // Simulate successful deletion
            A.CallTo(() => userRepositoryMock.DeleteAsync(A<Expression<Func<User, bool>>>.Ignored, cancellationSource.Token))
                .Returns(Task.FromResult(1));

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            var isDeleted = await userService.DeleteAsync(user.Id, cancellationSource.Token);

            // Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenUserDoesntExists()
        {
            // Arrange
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "firstName",
                LastName = "lastName",
                Age = 30,
                Email = "ozlemkalemci@upschool.com",
            };

            // Simulate exception when deleting the user
            A.CallTo(() => userRepositoryMock.DeleteAsync(A<Expression<Func<User, bool>>>.Ignored, cancellationSource.Token))
                .Throws(new ArgumentException("id cannot be empty."));

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            Func<Task> isUserDoesntExists = async () => await userService.DeleteAsync(user.Id, cancellationSource.Token);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(isUserDoesntExists);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty()
        {
            // Arrange
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            var user = new User()
            {
                Id = Guid.Empty,
                FirstName = "firstName",
                LastName = "lastName",
                Age = 30,
                Email = "ozlemkalemci@upschool.com",
            };

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            A.CallTo(() => userRepositoryMock.UpdateAsync(A<User>.Ignored, cancellationSource.Token))
                .Throws(new ArgumentException("User Id cannot be null or empty."));

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(user, cancellationSource.Token));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull()
        {
            // Arrange
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            var userEmpty = new User()
            {
                Id = Guid.NewGuid(),
                Email = string.Empty,
            };

            var userNull = new User()
            {
                Id = Guid.NewGuid(),
                Email = null,
            };

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            A.CallTo(() => userRepositoryMock.UpdateAsync(A<User>.Ignored, cancellationSource.Token))
                .Throws(new ArgumentException("Email cannot be null or empty."));

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(userEmpty, cancellationSource.Token));
            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(userNull, cancellationSource.Token));
            
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            List<User> userList = new List<User>
            {
                new User() {Id = Guid.NewGuid(),FirstName = "Up", LastName="School"},
                new User() {Id = Guid.NewGuid(),FirstName = "Ozlem", LastName="Kalemci"},
                new User() {Id = Guid.NewGuid(), FirstName = "FirstName", LastName = "LastName"}
            };

            IUserService userService = new UserManager(userRepositoryMock);

            // Act
            A.CallTo(() => userRepositoryMock.GetAllAsync(cancellationSource.Token))
                .Returns(Task.FromResult(userList));

            var result = await userService.GetAllAsync(cancellationSource.Token);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
        }


    }
}
