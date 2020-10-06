using System.Collections.Generic;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    public class Release
    {
        public Game Game { get; set; }
        public List<Feature> Features { get; set; }
        public int VersionId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
    }
}