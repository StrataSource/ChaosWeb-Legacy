using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Octokit;

namespace ChaosInitiative.Web.ControlPanel.Pages
{
    // No authorization required
    public class IndexModel : PageModel
    {
        
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;
        
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger/*, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager*/)
        {
            _logger = logger;/*
            _roleManager = roleManager;
            _userManager = userManager;*/
        }

        public void OnGet()
        {
#if false
            var token = HttpContext.GetTokenAsync("access_token").Result;
            GitHubClient client = new GitHubClient(new ProductHeaderValue("ChaosInitiativeControlPanel"));
            client.Credentials = new Credentials(token);
            var members = client.Organization.Member.GetAll(Constants.GITHUB_ORG_NAME).Result;

            foreach (var member in members)
            {
                if (User.Identity.Name == member.Name)
                {
                    _userManager.AddToRoleAsync(_userManager.GetUserAsync(User).Result, "Member").Wait();
                    return;
                }
            }
#endif
        }
    }
}