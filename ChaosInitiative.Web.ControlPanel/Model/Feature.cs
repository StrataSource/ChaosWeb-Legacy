using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    [Table("Features")]
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public FeatureType Type { get; set; }
        public List<Issue> RelatedIssues { get; set; } = new List<Issue>();
        public bool Completed { get; set; }
    }
}