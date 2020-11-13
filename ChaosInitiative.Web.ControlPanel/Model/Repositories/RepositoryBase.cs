using System;

namespace ChaosInitiative.Web.ControlPanel.Model.Repositories
{
    public abstract class RepositoryBase : IDisposable
    {
        protected readonly ApplicationContext Context;
        
        public RepositoryBase(ApplicationContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
        
    }
}