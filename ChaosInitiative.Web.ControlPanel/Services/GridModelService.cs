using System;
using ChaosInitiative.Web.ControlPanel.Model;
using ChaosInitiative.Web.ControlPanel.Services.Repositories;
using GridBlazor;
using GridMvc.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ChaosInitiative.Web.ControlPanel.Services
{
    public class GridModelService
    {

        public readonly int ITEMS_PER_PAGE = 15;
        public readonly string FEATURE_GRID_NAME = "featuresGrid";
            
        private readonly FeatureRepository _featureRepository;
        
        public GridModelService(FeatureRepository context)
        {
            _featureRepository = context;
        }

        
        public ItemsDTO<Feature> GetFeaturesGridRows(Action<IGridColumnCollection<Feature>> columns,
                                                     QueryDictionary<StringValues> query)
        {
            var server = new GridServer<Feature>(_featureRepository.GetAll(), 
                                                 new QueryCollection(query), 
                                                 false, 
                                                 FEATURE_GRID_NAME, 
                                                 columns, 
                                                 ITEMS_PER_PAGE)
                         .Sortable()
                         .Filterable()
                         .WithMultipleFilters();

            return server.ItemsToDisplay;
        }

        public IGridClient<Feature> CreateFeatureGridClient()
        {
            Action<IGridColumnCollection<Feature>> columns = collection =>
            {
                collection.Add(f => f.Id);
                collection.Add(f => f.Name);
                collection.Add(f => f.Type);
                collection.Add(f => f.RelatedIssues);
                collection.Add(f => f.Completed);
            };

            var query = new QueryDictionary<StringValues>
            {
                {"grid-page", "2"}
            };
            IGridClient<Feature> client = new GridClient<Feature>(
                                              queryDictionary => GetFeaturesGridRows(columns, queryDictionary),
                                              query,
                                              false,
                                              FEATURE_GRID_NAME,
                                              columns)
                                          .Sortable()
                                          .Filterable()
                                          .WithMultipleFilters();

            return client;
        }
    }
}