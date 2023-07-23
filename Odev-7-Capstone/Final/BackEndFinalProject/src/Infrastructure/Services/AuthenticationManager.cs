using Application.Common.Interfaces;
using Application.Common.Models.Auth;
using Application.Features.Auth.Commands.Login;
using Domain.Identity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        // Verilen kullanıcı bilgileriyle yeni bir kullanıcı oluşturur.
        public async Task<string> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var user = createUserDto.MapToUser();
            var identityResult = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!identityResult.Succeeded)
            {
                var failures = identityResult.Errors
                    .Select(x => new ValidationFailure(x.Code, x.Description));

                throw new ValidationException(failures);
            }

            return user.Id;
        }

        // Belirtilen kullanıcıya ait e-posta doğrulama token'ını oluşturur.
        public async Task<string> GenerateEmailActivationTokenAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        // Belirtilen e-posta adresine sahip bir kullanıcının sistemde olup olmadığını kontrol eder.
        public Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken)
        {
            return _userManager.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }

        // Kullanıcıyı belirtilen e-posta ve parola ile oturum açar.
        public async Task<JwtDto> LoginAsync(AuthLoginRequest authLoginRequest, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(authLoginRequest.Email);

            var loginResult = await _signInManager.PasswordSignInAsync(user, authLoginRequest.Password, false, false);

            if (!loginResult.Succeeded)
            {
                throw new ValidationException(CreateValidationFailure);
            }

            return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName);
        }


        public async Task<JwtDto> SocialLoginAsync(string email, string firstName, string lastName, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
                return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName);

            var userId = Guid.NewGuid().ToString();

            user = new User()
            {
                Id = userId,
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FirstName = firstName,
                LastName = lastName,
                CreatedOn = DateTimeOffset.Now,
                CreatedByUserId = userId,
            };

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded)
            {
                var failures = identityResult.Errors
                    .Select(x => new ValidationFailure(x.Code, x.Description));

                throw new ValidationException(failures);
            }

            return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName);
        }



        // Oturum açma hatası için oluşturulmuş hata listesini döndürür.
        private List<ValidationFailure> CreateValidationFailure => new List<ValidationFailure>()
        {
            new ValidationFailure("Email & Password", "Your email or password is not correct")
        };
    }
}
