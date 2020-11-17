using System.Collections.Generic;
using System.Linq;
using ChaosInitiative.Web.Shared;

namespace ChaosInitiative.Web.ControlPanel.Model.Repositories
{
    public class FeatureRepository : RepositoryBase
    {
        public FeatureRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Feature> GetAll()
        {
            return Context.Features.ToList();
        }

        public void Insert(Feature feature)
        {
            IEnumerable<Issue> sharedIssues = Context.Features.SelectMany(f => f.GetSharedRelatedIssues(feature));
            if (!sharedIssues.IsEmpty())
            {
                throw new FeatureSameRelatedIssuesException(sharedIssues);
            }
        }
        
    }
}