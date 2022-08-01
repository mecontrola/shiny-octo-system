﻿using FluentAssertions;
using Stefanini.Core.TestingTools.FluentAssertions.Extensions;
using Stefanini.ViaReport.DataStorage.IoC;
using Stefanini.ViaReport.DataStorage.Repositories;
using Xunit;

namespace Stefanini.ViaReport.Core.Tests.IoC
{
    public class DatabaseInjectorTests : BaseInjectorTests
    {
        private const int TOTAL_RECORDS = 10;

        [Fact(DisplayName = "[DatabaseInjector.AddBusiness] Deve gerar exceção quando o serviceCollection for nulo.")]
        public void DeveGerarExcecaoQuandoServiceCollectionNulo()
            => RunServiceCollectionNull(serviceCollection => serviceCollection.AddRepositories());

        [Fact(DisplayName = "[DatabaseInjector.AddMappers] Verifica se a injeções estão corretas.")]
        public void DeveVerificarInjecao()
        {
            serviceCollection.AddRepositories();

            serviceCollection.Should().HaveCount(TOTAL_RECORDS);
            serviceCollection.ShouldAsTransient<IIssueEpicRepository, IssueEpicRepository>();
            serviceCollection.ShouldAsTransient<IIssueRepository, IssueRepository>();
            serviceCollection.ShouldAsTransient<IIssueImpedimentRepository, IssueImpedimentRepository>();
            serviceCollection.ShouldAsTransient<IIssueTypeRepository, IssueTypeRepository>();
            serviceCollection.ShouldAsTransient<IIssueStatusHistoryRepository, IssueStatusHistoryRepository>();
            serviceCollection.ShouldAsTransient<IProjectRepository, ProjectRepository>();
            serviceCollection.ShouldAsTransient<IProjectCategoryRepository, ProjectCategoryRepository>();
            serviceCollection.ShouldAsTransient<IQuarterRepository, QuarterRepository>();
            serviceCollection.ShouldAsTransient<IStatusRepository, StatusRepository>();
            serviceCollection.ShouldAsTransient<IStatusCategoryRepository, StatusCategoryRepository>();
        }
    }
}