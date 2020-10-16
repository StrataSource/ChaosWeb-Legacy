using System.Collections.Generic;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public string Name { get; set; }
        public FeatureType Type { get; set; }
        public List<Issue> RelatedIssues { get; set; }
        public bool Completed { get; set; }
    }

    public enum FeatureType
    {
        ADDITION = 0,
        FIX,
        CHANGE,
        REFACTOR,
        OTHER
    }
}