using Application.Common.Interfaces;
using Application.Common.Models.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, AuthRegisterDto>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthRegisterCommandHandler(IAuthenticationService authenticationService, IJwtService jwtService, IEmailService emailService)
        {
            _authenticationService = authenticationService;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<AuthRegisterDto> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
        {
            var createUserDto = new CreateUserDto(request.FirstName, request.LastName, request.Email, request.Password);

            var userId = await _authenticationService.CreateUserAsync(createUserDto, cancellationToken);

            var emailToken = await _authenticationService.GenerateEmailActivationTokenAsync(userId, cancellationToken);

            var fullName = $"{request.FirstName} {request.LastName}";

            var jwtDto = _jwtService.Generate(userId, request.Email, request.FirstName, request.LastName);


            return new AuthRegisterDto(request.Email, fullName, jwtDto.AccessToken);
        }
    }
}
