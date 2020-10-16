using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octokit;

namespace ChaosInitiative.Web.ControlPanel.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {

        private ILogger _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("Logout")]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
        
        [HttpGet("Login")]
        public async Task<IActionResult> SignInAsync()
        {
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!User.Identity.IsAuthenticated)
            {
                string state = GitHubUtil.GenerateState(32);
                
                OauthLoginRequest request = new OauthLoginRequest(GitHubUtil.ApplicationClientId)
                {
                    State = state
                };
                GitHubClient client = GitHubUtil.CreateClient();
                
                HttpContext.Session.Set("csrf:state", Encoding.ASCII.GetBytes(state));

                return Redirect(client.Oauth.GetGitHubLoginUrl(request).ToString());
            }

            return RedirectToPage("/Index");

        }

        [HttpGet("LoggedIn")]
        public async Task<IActionResult> AuthorizeAsync(string code, string state)
        {
            if (String.IsNullOrWhiteSpace(code)) return RedirectToPage("/Auth/Error");
            
            _logger.LogInformation("====================================================");
            _logger.LogInformation($"New authentication request from {HttpContext.Connection.RemoteIpAddress}. Code: {code}");
            _logger.LogInformation("====================================================");

            string expectedState = Encoding.ASCII.GetString(HttpContext.Session.Get("csrf:state"));
            if (state != expectedState)
            {
                _logger.LogInformation(" - Authentication state doesn't match! Aborting");
                _logger.LogInformation($" - Expected: {expectedState}");
                _logger.LogInformation($" - Got: {state}");
                return Unauthorized();
            }
            
            _logger.LogInformation(" - Authentication state matches.");
            _logger.LogInformation(" - Requesting github api token...");

            // Get token
            
            OauthTokenRequest request = new OauthTokenRequest(GitHubUtil.ApplicationClientId, GitHubUtil.ApplicationClientSecret, code);
            GitHubClient client = GitHubUtil.CreateClient();
            string token = (await client.Oauth.CreateAccessToken(request)).AccessToken;
            
            _logger.LogInformation($" - Token: {token}");

            if (String.IsNullOrWhiteSpace(token)) return RedirectToPage("/Auth/Error");
            client.Credentials = new Credentials(token);
            
            _logger.LogInformation(" - Requesting user info...");
            
            // Get user details
            
            var user = await client.User.Current();
            
            _logger.LogInformation("--------------------");
            _logger.LogInformation($" - Username: {user.Login}");
            _logger.LogInformation($" - E-Mail: {user.Email ?? "(none)"}");
            _logger.LogInformation("--------------------");
            _logger.LogInformation(" - Checking organization membership...");
            
            if (!await client.Organization.Member.CheckMember(GitHubUtil.GITHUB_ORG_NAME, user.Login))
            {
                _logger.LogInformation(" - Not an organization member! Not logging in.");
                return RedirectToPage("/Auth/NoMember");
            }
            
            _logger.LogInformation(" - User is organization member. Creating claims.");
            
            // Now we're authenticated and checked if we're an org member :)

            string issuer = "Chaos Initiative";
            
            Claim memberClaim = new Claim(ClaimTypes.Role, "Member", ClaimValueTypes.String, issuer);
            Claim nameClaim = new Claim(ClaimTypes.Name, user.Login, ClaimValueTypes.String, issuer);
            
            ClaimsIdentity identity = new ClaimsIdentity(
                new []{ memberClaim, nameClaim }, 
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            _logger.LogInformation($" - Signing user in using {CookieAuthenticationDefaults.AuthenticationScheme}");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }
        
    }
}