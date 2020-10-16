namespace ChaosInitiative.Web.ControlPanel.Model
{
    public class Issue
    {
        public Game Game { get; set; }
        public uint IssueId { get; set; }

        public string GetFullPath()
        {
            return $"{Game.Repository}/issues/{IssueId}";
        }
    }
}