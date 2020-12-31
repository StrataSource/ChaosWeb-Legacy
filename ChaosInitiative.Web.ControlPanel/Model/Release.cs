using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ChaosInitiative.Web.ControlPanel.Model
{
    [Table("Releases")]
    public class Release
    {
        public int VersionId { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public List<ReleaseFeatures> ReleaseFeatures { get; set; }

        [NotMapped] 
        public List<Feature> Features => ReleaseFeatures.Select(rf => rf.Feature).ToList();
    }
}