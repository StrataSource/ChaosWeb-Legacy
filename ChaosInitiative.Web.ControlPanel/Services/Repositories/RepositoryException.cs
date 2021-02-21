using System;

namespace ChaosInitiative.Web.ControlPanel.Services.Repositories
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {
        }
    }

    public class RepositoryInsertException : RepositoryException
    {
        public RepositoryInsertException(string message) : base(message)
        {
        }
    }
}