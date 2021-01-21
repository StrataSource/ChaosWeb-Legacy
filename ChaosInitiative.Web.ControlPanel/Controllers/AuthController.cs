using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChaosInitiative.Web.ControlPanel.Services;
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

        private const string SessionStateKeyName = "csrf:state";

        private ILogger _logger;
        private IGitHubService _gitHubService;

        public AuthController(ILogger<AuthController> logger, IGitHubService gitHubService)
        {
            _logger = logger;
            _gitHubService = gitHubService;
        }
        
        [HttpGet("Logout")]
        public async Task<IActionResult> SignOutAsync()
        {
            _logger.LogInformation("User {IpAddress} is signing out", HttpContext.Connection.RemoteIpAddress);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
        
        [HttpGet("Login")]
        public async Task<IActionResult> SignInAsync()
        {
            
            _logger.LogInformation("New login request from {IpAddress}", HttpContext.Connection.RemoteIpAddress);
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (User.Identity is null || !User.Identity.IsAuthenticated)
            {
                
                _logger.LogInformation("User is not authenticated");
                
                string state = _gitHubService.GenerateState(32);
                _logger.LogInformation("Generating OAuth state: {State}", state);
                
                OauthLoginRequest request = new OauthLoginRequest(_gitHubService.ClientId)
                {
                    State = state
                };
                GitHubClient client = _gitHubService.GetClient();
                
                HttpContext.Session.Set(SessionStateKeyName, Encoding.ASCII.GetBytes(state));
                
                _logger.LogInformation("Redirecting user to authenticate with github");
                return Redirect(client.Oauth.GetGitHubLoginUrl(request).ToString());
            }
            
            _logger.LogInformation("User is already authenticated. Redirecting to home page...");

            return RedirectToPage("/Index");

        }

        [HttpGet("LoggedIn")]
        public async Task<IActionResult> AuthorizeAsync(string code, string state)
        {
            if (String.IsNullOrWhiteSpace(code)) return RedirectToPage("/Auth/Error");
            
            _logger.LogInformation("====================================================");
            _logger.LogInformation("New authentication request from {IpAddress}. Code: {Code}. State: {State}", HttpContext.Connection.RemoteIpAddress, code, state);
            _logger.LogInformation("====================================================");

            string expectedState = Encoding.ASCII.GetString(HttpContext.Session.Get(SessionStateKeyName));
            if (state != expectedState)
            {
                _logger.LogInformation("Authentication state doesn't match! Expected {ExpectedState} but got {ActualState}. Aborting!", expectedState, state);
                return Unauthorized();
            }
            
            _logger.LogInformation("Authentication state matches");
            _logger.LogInformation("Requesting github api token...");

            // Get token
            
            OauthTokenRequest request = new OauthTokenRequest(_gitHubService.ClientId, _gitHubService.ClientSecret, code);
            GitHubClient client = _gitHubService.GetClient();
            string token = (await client.Oauth.CreateAccessToken(request)).AccessToken;
            
            _logger.LogInformation("Token: {Token}", token);

            if (String.IsNullOrWhiteSpace(token)) return RedirectToPage("/Auth/Error");
            client.Credentials = new Credentials(token);
            
            _logger.LogInformation("Requesting user info...");
            
            // Get user details
            
            var user = await client.User.Current();
            
            _logger.LogInformation("--------------------");
            _logger.LogInformation("Username: {Username}", user.Login);
            _logger.LogInformation("E-Mail: {Email}", user.Email ?? "(none)");
            _logger.LogInformation("--------------------");
            _logger.LogInformation("Checking organization membership...");
            
            // Authorize
            
            if (!await client.Organization.Member.CheckMember(_gitHubService.GitHubOrganisationName, user.Login))
            {
                _logger.LogInformation("Not an organization member! Not logging in");
                return RedirectToPage("/Auth/NoMember");
            }
            
            _logger.LogInformation("User is organization member. Creating claims....");
            
            // Now we're authenticated and checked if we're an org member :)

            string issuer = "Chaos Initiative";
            
            Claim memberClaim = new Claim(ClaimTypes.Role, "Member", ClaimValueTypes.String, issuer);
            Claim nameClaim = new Claim(ClaimTypes.Name, user.Login, ClaimValueTypes.String, issuer);
            
            ClaimsIdentity identity = new ClaimsIdentity(
                new []{ memberClaim, nameClaim }, 
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            _logger.LogDebug("Signing user in using {AuthenticationScheme}", CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            
            _logger.LogInformation("User {Username} successfully signed in", user.Login);

            return RedirectToPage("/Index");
        }
        
    }
}