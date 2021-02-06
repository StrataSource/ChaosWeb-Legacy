using System;
using ChaosInitiative.Web.ControlPanel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChaosInitiative.Web.ControlPanel.Extensions
{
    public static class GitHubServiceExtensions
    {
        public static IServiceCollection AddGitHub(this IServiceCollection services, Action<GitHubServiceOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            services.Configure(configure);
            services.AddGitHub();

            return services;
        }
        
        public static IServiceCollection AddGitHub(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<IGitHubService, GitHubService>();
            return services;
        }
    }
}