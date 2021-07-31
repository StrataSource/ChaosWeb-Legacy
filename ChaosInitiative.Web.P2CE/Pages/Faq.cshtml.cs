using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaosInitiative.Web.P2CE.Pages
{
    public class FaqModel : PageModel
    {

        public List<FaqElement> faqs;

        public FaqModel()
        {
            faqs = new List<FaqElement>()
            {
                //new FaqElement("How did you get the source code?", "We don't feel comfortable putting details on Valve's internal processes openly on the internet. If you've got a specific question regarding this, ask us directly."),
                new("Will this support BEE-Mod?", "No, But not because we don't like BEE, but because we're rebuilding the entire in-game puzzle maker from scratch, which makes BEE obsolete. However, we work closely with the BEE-Mod developers to satisfy the needs of everyone."),
                new("How much does it cost?", "Nothing. P2:CE is non-profit and fully free to play without any micro-transactions or the like. All you need is Portal 2!"),
                new("Do I need to own Portal 2 to play P2:CE?", "Yes. You need to own Portal 2 on Steam to download P2:CE. If Portal 2 is installed, P2:CE will automatically detect and mount it. We're planning to add the ability for P2:CE to download Portal 2 for you if you don't have already."),
                new("How do I get access to the closed beta?", "You can't. The closed beta is only accessible to a selected group of testers. Please be patient."),
                new("When does P2:CE release?", "We're currently aiming for late 2021. Watch out for announcements on Discord or Twitter to stay up to date."),
                new("Which operating systems does it run on?", "P2:CE currently runs on Windows, but a Linux build is on its way. In the future, we'd also like to support MacOS. All 64bit of course."),
                new("Is there a workshop?", "Yes. That's definitely planned for release."),
                new("Is multiplayer supported?", "Yes."),
                new("Is VR planned?", "No. VR is out of the scope for the time being. We don't rule it out entirely and might look into it in the future, but there's nothing to get excited about at this time."),
            };
        }
        
        public void OnGet()
        {
            
        }
    }

    public class FaqElement
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public FaqElement(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }

        public string ElementId => Question.ToLower().Replace(" ", "-").Replace("?", "");
    }
}