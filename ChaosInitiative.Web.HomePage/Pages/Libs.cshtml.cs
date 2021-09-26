using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaosInitiative.Web.HomePage.Pages
{
    public class LibsModel : PageModel
    {
        public List<Lib> Libs { get; set; }

        public LibsModel()
        {
            // This may be overkill, considering that currently there is only one library under lgpl.
            Libs = new List<Lib>
            {
                new Lib
                {
                    LibName = "Qt",
                    LibDesc = "<a href=\"qt.io\">Qt</a> is a cross-platform application and UI framework. The Chaos Initiative utilizes Qt in the new Hammer Editor. We ship vanilla Qt.",
                    LibDownload = "#"

                },
            };
        }

        public void OnGet()
        {
        }
    }
    public class Lib
    {
        public string LibName { get; set; } // Name of the library
        public string LibDesc { get; set; } // Describes the library
        public string LibDownload { get; set; } // Download link to source code
    }
}
