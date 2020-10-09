using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace ChaosInitiative.Web.ControlPanel.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {

        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                
                var request = new OauthLoginRequest(GitHubUtil.ApplicationClientId)
                {
                    State = state
                };
                var client = GitHubUtil.CreateClient();
                
                HttpContext.Session.Set("csrf:state", System.Text.Encoding.ASCII.GetBytes(state));

                return Redirect(client.Oauth.GetGitHubLoginUrl(request).ToString());
            }

            return RedirectToPage("/Index");

        }

        [HttpGet("LoggedIn")]
        public async Task<IActionResult> AuthorizeAsync(string code, string state)
        {
            if (String.IsNullOrWhiteSpace(code)) return RedirectToPage("/Auth/Error");
            
            Console.WriteLine("====================================================");
            Console.WriteLine($"New authentication request from {HttpContext.Connection.RemoteIpAddress}. Code: {code}");
            Console.WriteLine("====================================================");

            string expectedState = System.Text.Encoding.ASCII.GetString(HttpContext.Session.Get("csrf:state"));
            if (state != expectedState)
            {
                Console.WriteLine(" - Authentication state doesn't match! Aborting");
                Console.WriteLine($" - Expected: {expectedState}");
                Console.WriteLine($" - Got: {state}");
                return Unauthorized();
            }
            
            Console.WriteLine(" - Authentication state matches.");
            Console.WriteLine(" - Requesting github api token...");

            // Get token
            
            var request = new OauthTokenRequest(GitHubUtil.ApplicationClientId, GitHubUtil.ApplicationClientSecret, code);
            var client = GitHubUtil.CreateClient();
            string token = (await client.Oauth.CreateAccessToken(request)).AccessToken;
            
            Console.WriteLine($" - Token: {token}");

            if (String.IsNullOrWhiteSpace(token)) return RedirectToPage("/Auth/Error");
            client.Credentials = new Credentials(token);
            
            Console.WriteLine(" - Requesting user info...");
            
            // Get user details
            
            var user = await client.User.Current();
            
            Console.WriteLine("--------------------");
            Console.WriteLine($" - Username: {user.Login}");
            Console.WriteLine($" - E-Mail: {user.Email ?? "(none)"}");
            Console.WriteLine("--------------------");
            Console.WriteLine(" - Checking organization membership...");
            
            if (!await client.Organization.Member.CheckMember(GitHubUtil.GITHUB_ORG_NAME, user.Login))
            {
                Console.WriteLine(" - Not an organization member! Not logging in.");
                return RedirectToPage("/Auth/NoMember");
            }
            
            Console.WriteLine(" - User is organization member. Creating claims.");
            
            // Now we're authenticated and checked if we're an org member :)

            string issuer = "Chaos Initiative";
            
            Claim memberClaim = new Claim(ClaimTypes.Role, "Member", ClaimValueTypes.String, issuer);
            Claim nameClaim = new Claim(ClaimTypes.Name, user.Login, ClaimValueTypes.String, issuer);
            
            ClaimsIdentity identity = new ClaimsIdentity(
                new []{ memberClaim, nameClaim }, 
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            Console.WriteLine($" - Signing user in using {CookieAuthenticationDefaults.AuthenticationScheme}");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }
        
    }
}