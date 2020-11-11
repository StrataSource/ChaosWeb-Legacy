using System;
using ChaosInitiative.Web.ControlPanel.Model;
using ChaosInitiative.Web.ControlPanel.Model.Repositories;
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
        
        public ItemsDTO<Feature> GetFeaturesGridRows(Action<IGridColumnCollection<Feature>> columns,
            QueryDictionary<StringValues> query, ApplicationContext context)
        {
            var repository = new FeatureRepository(context);
            var server = new GridServer<Feature>(repository.GetAll(), 
                new QueryCollection(query), 
                false, 
                "featuresGrid", 
                columns, 
                ITEMS_PER_PAGE)
                .Sortable()
                .Filterable()
                .WithMultipleFilters();

            return server.ItemsToDisplay;
        }
    }
}