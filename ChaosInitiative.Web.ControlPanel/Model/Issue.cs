using System.ComponentModel.DataAnnotations.Schema;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    [Table("Issues")]
    public class Issue
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public uint IssueId { get; set; }

        public string GetFullPath()
        {
            return $"{Game.GitHubRepositoryUri}/issues/{IssueId}";
        }
    }
}