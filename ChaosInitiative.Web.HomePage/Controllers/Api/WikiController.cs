using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChaosInitiative.Web.HomePage.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class WikiController : ControllerBase
    {
        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult OnRefresh()
        {
            WikiUtil.RefreshWikiGitRepository();
            return NoContent();
        }

        [HttpPost]
        [Route("build")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult OnBuild()
        {
            WikiUtil.BuildWiki();
            return NoContent();
        }
    }
}