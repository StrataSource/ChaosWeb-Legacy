using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace ChaosInitiative.Web.ControlPanel.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
        
        [Route("Login")]
        [Authorize(AuthenticationSchemes = GitHubAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Login()
        {
            var authenticationService = HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
            var result = await authenticationService.AuthenticateAsync(HttpContext, GitHubAuthenticationDefaults.AuthenticationScheme);

            foreach (var token in result.Properties.GetTokens())
            {
                Console.WriteLine($"{token.Name}: {token.Value}");
            }
            
            string accessToken = result.Properties.GetTokenValue("access_token");
            string userName = User.Identity.Name;
            
            // Find out if github account is part of the organisation
            // TODO: This currently only works on members who show their membership publicly, because github's api 
            // for authenticated access on that endpoint is broken or whatever
            GitHubClient client = new GitHubClient(new ProductHeaderValue("ChaosInitiativeControlPanel"));

            bool isOrgMember = await client.Organization.Member.CheckMember(Constants.GITHUB_ORG_NAME, userName);
            
            Console.WriteLine($"{userName} is member of {Constants.GITHUB_ORG_NAME}: {isOrgMember}");
            
            if (isOrgMember)
            {
                ClaimsIdentity chaosIdentity = new ClaimsIdentity();
                chaosIdentity.AddClaim(new Claim(ClaimTypes.Role, "Member"));
                
                ClaimsPrincipal principal = new ClaimsPrincipal();
                principal.AddIdentity(chaosIdentity);
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok();
                //return RedirectToPage("/Panel/Dashboard");
            }
            else
            {
                return RedirectToPage("/Auth/NoMember");
            }
            
        }

        [Route("LoggedIn")]
        public IActionResult LoggedIn()
        {
            return Ok();
        }
        
    }
}