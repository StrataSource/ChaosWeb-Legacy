using System;
using System.Linq;
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

        private readonly int ITEMS_PER_PAGE = 15;
        private readonly string FEATURES_GRID_NAME = "featuresGrid";
        private readonly FeatureRepository _featureRepository;

        public GridModelService(FeatureRepository repository)
        {
            _featureRepository = repository;
        }
        
        private ItemsDTO<Feature> GetFeaturesGridRows(Action<IGridColumnCollection<Feature>> columns,
                                                     QueryDictionary<StringValues> query)
        {
            var server = new GridServer<Feature>(_featureRepository.GetAllInclusive(), 
                                                 new QueryCollection(query), 
                                                 false, 
                                                 FEATURES_GRID_NAME, 
                                                 columns, 
                                                 ITEMS_PER_PAGE)
                         .Sortable()
                         .Filterable()
                         .WithMultipleFilters()
                         .Selectable(false)
                         .WithGridItemsCount();

            return server.ItemsToDisplay;
        }

        public IGridClient<Feature> GetFeaturesGridClient()
        {
            Action<IGridColumnCollection<Feature>> columns = collection =>
            {
                collection.Add(f => f.Id);
                collection.Add(f => f.Name);
                collection.Add(f => f.Type);
                collection.Add(f => f.RelatedIssues)
                          .Sanitized(false).Encoded(false)
                          .RenderValueAs(RenderFeatureRelatedIssues);
                collection.Add(f => f.Completed);
            };

            QueryDictionary<StringValues> query = new QueryDictionary<StringValues>
            {
                {"grid-page", "2"}
            };
        
            IGridClient<Feature> client = new GridClient<Feature>(queryDictionary => GetFeaturesGridRows(columns, queryDictionary),
                                                                  query,
                                                                  false,
                                                                  FEATURES_GRID_NAME,
                                                                  columns)
                                          .Sortable()
                                          .Filterable()
                                          .WithMultipleFilters()
                                          .Selectable(false)
                                          .WithGridItemsCount();

            return client;
        }

        private string RenderFeatureRelatedIssues(Feature feature)
        {
            return String.Join(' ', 
                               feature.RelatedIssues.Select(
                                   i => $"<a class='badge' href='{i.GetFullPath()}' style='{i.Game.HexColor}'>{i.IssueId}</a>"));
        }
    }
}