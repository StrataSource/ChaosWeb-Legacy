using System.Collections.Generic;
using System.Linq;
using ChaosInitiative.Web.ControlPanel.Model;
using ChaosInitiative.Web.Shared;

namespace ChaosInitiative.Web.ControlPanel.Services.Repositories
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

        public Feature GetById(int id)
        {
            return Context.Features.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Feature> GetByType(FeatureType type)
        {
            return Context.Features.Where(f => f.Type == type).ToList();
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