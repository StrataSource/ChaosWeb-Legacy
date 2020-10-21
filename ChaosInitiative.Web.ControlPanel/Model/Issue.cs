using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    [Table("Issues")]
    public class Issue
    {
        [Key]
        public Game Game { get; set; }
        [Key]
        public uint IssueId { get; set; }

        public string GetFullPath()
        {
            return $"{Game.GetGitHubRepositoryUri()}/issues/{IssueId}";
        }
    }
}