﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stefanini.Core.Extensions;
using Stefanini.ViaReport.Core.Data.Configurations;
using Stefanini.ViaReport.Core.IoC;
using Stefanini.ViaReport.Helpers;
using Stefanini.ViaReport.Updater.Core.Data.Configurations;
using Stefanini.ViaReport.Updater.Core.Extensions;
using Stefanini.ViaReport.Updater.Core.Helpers;
using Stefanini.ViaReport.Updater.Core.Integrations.Github.Repos;

namespace Stefanini.ViaReport
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationConfiguration = GetApplicationConfiguration();
            var jiraConfiguration = GetJiraConfiguration();

            services.AddSingleton(applicationConfiguration);
            services.AddSingleton(jiraConfiguration);
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AuthenticationWindow>();

            services.AddSingleton<IUpdateToastHelper, UpdateToastHelper>();

            InjectUpdater(services);

            services.AddBusiness();
            services.AddHelpers();
            services.AddServices();
            services.AddMappers();
            services.AddIntegrations();
        }

        private void InjectUpdater(IServiceCollection services)
        {
            services.AddSingleton<IRemoteVersionHelper, RemoteVersionHelper>();
            services.AddSingleton<IGitHubLastReleaseHelper, GitHubLastReleaseHelper>();
            services.AddSingleton<IReleasesLastest, ReleasesLastest>();
            services.AddSingleton(Configuration.GetUpdaterConfiguration());
        }

        private IApplicationConfiguration GetApplicationConfiguration()
            => Configuration.Load<ApplicationConfiguration>();

        private IJiraConfiguration GetJiraConfiguration()
            => Configuration.Load<JiraConfiguration>();
    }
}