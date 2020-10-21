using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string RepositoryOwner { get; set; }
        public string RepositoryName { get; set; }

        public string GetGitHubRepositoryUri()
        {
            return $"https://github.com/{RepositoryOwner}/{RepositoryName}";
        }
    }
}