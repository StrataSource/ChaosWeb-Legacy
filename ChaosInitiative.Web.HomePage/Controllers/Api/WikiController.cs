using ChaosInitiative.Web.HomePage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChaosInitiative.Web.HomePage.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class WikiController : ControllerBase
    {

        private readonly WikiService _wikiService;

        public WikiController(WikiService wikiService)
        {
            _wikiService = wikiService;
        }
        
        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult OnRefresh()
        {
            _wikiService.RefreshWikiGitRepository();
            return NoContent();
        }

        [HttpPost]
        [Route("build")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult OnBuild()
        {
            _wikiService.BuildWiki();
            return NoContent();
        }
    }

    [ApiController]
    [Route("wiki")]
    public class WikiIndexToReadmeRedirectController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        public IActionResult OnIndex()
        {
            return RedirectPermanent("/wiki/readme.html");
        }
    }
}