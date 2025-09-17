using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace LogHopper.Authentication
{
    public class TokenAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TokenAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

            var header = Request.Headers["Authorization"].ToString();
            if (!header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Invalid Scheme"));

            var token = header.Substring("Bearer ".Length).Trim();

            if (token != "super-secret-token")
                return Task.FromResult(AuthenticateResult.Fail("Invalid Token"));

            var claims = new[] { new Claim(ClaimTypes.Name, "ApiUser") };
            var identity = new ClaimsIdentity(claims, "Token");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "TokenScheme");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
