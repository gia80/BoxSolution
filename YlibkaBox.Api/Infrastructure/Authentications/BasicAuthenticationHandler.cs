using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using YlibkaBox.Api.Domain.Contracts;
using YlibkaBox.Api.Infrastructure.Providers;

namespace YlibkaBox.Api.Infrastructure.Authentications;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserRepository _userRepository;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserRepository userRepository)
        : base(options, logger, encoder, clock)
    {
        _userRepository = userRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
                return await Task.FromResult(AuthenticateResult.NoResult());

            if (!Request.Headers.ContainsKey("Authorization"))
                return await Task.FromResult(AuthenticateResult.Fail("Отсутствует заголовок Authorization"));

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (authHeader.Parameter == null) return await Task.FromResult(AuthenticateResult.NoResult());

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];

            var user = await _userRepository.GetUser(username);

            if (user == null)
                return await Task.FromResult(AuthenticateResult.Fail("Пользователь не найден"));


            if (user.Password != CredentialsProvider.HashPassword(password, user.Salt))
                return await Task.FromResult(AuthenticateResult.Fail("Неверный пароль"));


            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch
        {
            return AuthenticateResult.Fail("Произошла Ошибка. Не удалось выполнить авторизацию.");
        }
    }
}