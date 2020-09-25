using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaosInitiative.Web.ControlPanel.Pages.Panel
{
    [Authorize(Policy = "OrgMemberChaos")]
    public class Index : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}