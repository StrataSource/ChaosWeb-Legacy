namespace ChaosInitiative.Web.ControlPanel.Model
{
    public class ReleaseFeatures
    {
        public int ReleaseId { get; set; }
        public Release Release { get; set; }
        
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}