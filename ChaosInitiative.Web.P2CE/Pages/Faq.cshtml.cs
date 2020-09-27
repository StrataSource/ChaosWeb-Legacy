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
                new FaqElement("How did you get the source code?", "We don't feel comfortable putting details on Valve's internal processes openly on the internet. If you've got a specific question regarding this, ask us directly."),
                new FaqElement("Will this support BEE-Mod?", "No. But not because we don't like BEE, but because we're rebuilding the entire in-game puzzle maker from scratch, which makes BEE obsolete. However, we work closely with the developers to satisfy the needs of everyone."),
                new FaqElement("Is there a workshop?", "Yes. That's definitely planned for release.")
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
    }
}