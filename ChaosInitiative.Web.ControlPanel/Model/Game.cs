using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    [Table("Games")]
    public class Game
    {

        public static readonly IReadOnlyCollection<string> LegalRepositoryOwners = new[]
        {
            "ChaosInitiative"
        };
        
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Name is too short")]
        public string Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Repository Owner is too short")]
        public string RepositoryOwner { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "Repository Name is too short")]
        public string RepositoryName { get; set; }
        
        [Required]
        [MinLength(6, ErrorMessage = "HEX value must be 6 characters long")]
        [MaxLength(6, ErrorMessage = "HEX value must be 6 characters long")]
        public string HexColor { get; set; }

        public string GitHubRepositoryUri => 
            $"https://github.com/{RepositoryOwner}/{RepositoryName}";
        
    }
}