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
                new("Will this support BEE-Mod?", "Unfortunately no. In the future, we will be building an open source puzzlemaker replacement that aims to solve the issues surrounding Portal 2's puzzlemaker. We'll be working closely with the BEE-Mod developers to bring the best BEE-Mod features to it."),
                new("How much will it cost?", "Nothing! P2:CE is a completely free, non-profit project- you just need to own Portal 2!"),
                new("Do I need to own Portal 2 to play P2:CE?", "Yes, Portal 2 must be owned and installed in order to play P2:CE. Portal 2's content will automatically be mounted by P2:CE when the game starts."),
                new("How do I get access to the closed beta?", "We have a beta access application form in our Discord server. We tend to prioritize active members of our community and those with technical experience, but anyone can apply!"),
                new("When does P2:CE release?", "We haven't announce a release date for P2:CE yet. Keep an eye on our Discord or Twitter to be the first person to know when we do."),
                new("Which operating systems does it run on?", "Currently P2:CE supports 64-bit Windows and Linux. In the future we may support OSX, depending on interest in the platform. See the Steam page for more detailed system requirements."),
                new("Is there a workshop?", "Yes."),
                new("Is 2+ multiplayer supported?", "Yes."),
                new("Is VR planned?", "No, it's out of the scope of the project for now. We're not ruling it out entirely, but it's not likely to happen."),
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

        public string ElementId => Question.ToLower().Replace(" ", "-").Replace("?", "").Replace(":", "").Replace("+", "");
    }
}
