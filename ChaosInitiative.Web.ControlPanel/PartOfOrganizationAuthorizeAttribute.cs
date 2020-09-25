using Microsoft.AspNetCore.Authorization;

namespace ChaosInitiative.Web.ControlPanel
{
    public class PartOfOrganizationAuthorizeAttribute : AuthorizeAttribute
    {

        public static readonly string POLICY_NAME = "OrganizationMember";
        
        public PartOfOrganizationAuthorizeAttribute()
        {
            Policy = "";
        }
    }
}