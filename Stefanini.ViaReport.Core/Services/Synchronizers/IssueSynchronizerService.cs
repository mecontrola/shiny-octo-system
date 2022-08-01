﻿using Stefanini.ViaReport.Core.Builders.Jira;
using Stefanini.ViaReport.Core.Integrations.Jira.V2.Issues;
using Stefanini.ViaReport.Core.Integrations.Jira.V2.Projects;
using Stefanini.ViaReport.Core.Services.Synchronizers.ExtraIssueData;
using Stefanini.ViaReport.Data.Dtos.Jira;
using Stefanini.ViaReport.Data.Dtos.Jira.Inputs;
using Stefanini.ViaReport.Data.Dtos.Synchronizers;
using Stefanini.ViaReport.Data.Entities;
using Stefanini.ViaReport.Data.Parameters;
using Stefanini.ViaReport.DataStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stefanini.ViaReport.Core.Services.Synchronizers
{
    public class IssueSynchronizerService : IIssueSynchronizerService
    {
        private const long SEARCH_ISSUE_MAX_RESULTS = 256;

        private readonly IIssueRepository issueRepository;
        private readonly IIssueTypeRepository issueTypeRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IStatusRepository statusRepository;

        private readonly IIssueGet issueGet;
        private readonly ISearchPost searchPost;

        private readonly IIssueDataSynchronizerService issueDataSynchronizerService;
        private readonly IIssueImpedimentSynchronizerService issueImpedimentSynchronizerService;
        private readonly IIssueStatusHistorySynchronizerService issueStatusHistorySynchronizerService;
        private readonly IIssueEpicDataSynchronizerService issueEpicDataSynchronizerService;

        public IssueSynchronizerService(IIssueRepository issueRepository,
                                        IIssueTypeRepository issueTypeRepository,
                                        IProjectRepository projectRepository,
                                        IStatusRepository statusRepository,
                                        IIssueGet issueGet,
                                        ISearchPost searchPost,
                                        IIssueDataSynchronizerService issueDataSynchronizerService,
                                        IIssueImpedimentSynchronizerService issueImpedimentSynchronizerService,
                                        IIssueStatusHistorySynchronizerService issueStatusHistorySynchronizerService,
                                        IIssueEpicDataSynchronizerService issueEpicDataSynchronizerService)
        {
            this.issueRepository = issueRepository;
            this.issueTypeRepository = issueTypeRepository;
            this.projectRepository = projectRepository;
            this.statusRepository = statusRepository;
            this.issueGet = issueGet;
            this.searchPost = searchPost;
            this.issueDataSynchronizerService = issueDataSynchronizerService;
            this.issueImpedimentSynchronizerService = issueImpedimentSynchronizerService;
            this.issueStatusHistorySynchronizerService = issueStatusHistorySynchronizerService;
            this.issueEpicDataSynchronizerService = issueEpicDataSynchronizerService;
        }

        public async Task SynchronizeData(ConfigurationSynchronizerDto configurationSynchronizerDto, CancellationToken cancellationToken)
        {
            var issueConfigurationSynchronizerDto = (IssueConfigurationSynchronizerDto)configurationSynchronizerDto;
            var projects = await RetrieveProjectsData(issueConfigurationSynchronizerDto, cancellationToken);

            if (!projects.Any())
                return;

            foreach (var project in projects)
            {
                var lastUpdated = await RetrieveDateTimeLastUpdate(issueConfigurationSynchronizerDto.SyncAllData, project.Id, cancellationToken);

                await SynchronizeAllIssuesInProject(configurationSynchronizerDto, project, lastUpdated, cancellationToken);
            }
        }

        private async Task<DateTime?> RetrieveDateTimeLastUpdate(bool syncAllData, long projectId, CancellationToken cancellationToken)
            => syncAllData
             ? null
             : await RetrieveDateTimeLastUpdatesIssue(projectId, cancellationToken);

        private async Task<IList<Project>> RetrieveProjectsData(IssueConfigurationSynchronizerDto issueConfigurationSynchronizerDto, CancellationToken cancellationToken)
            => await projectRepository.FindAllInIdListAsync(issueConfigurationSynchronizerDto.Projects, cancellationToken);

        private async Task<DateTime?> RetrieveDateTimeLastUpdatesIssue(long projectId, CancellationToken cancellationToken)
            => await issueRepository.GetLastUpdatedAsync(projectId, cancellationToken);

        private async Task SynchronizeAllIssuesInProject(ConfigurationSynchronizerDto configurationSynchronizerDto, Project project, DateTime? lastUpdated, CancellationToken cancellationToken)
        {
            var issues = await RetrieveAllIssueInProject(configurationSynchronizerDto, project.Name, lastUpdated, cancellationToken);
            var statuses = await RetrieveAllKeyByIdStatus(cancellationToken);
            var issueTypes = await RetrieveAllKeyByIdIssueTypes(cancellationToken);

            foreach (var issue in issues)
            {
                var issueJira = await RetrieveIssueData(configurationSynchronizerDto, issue.Key, cancellationToken);

                var parameters = new IssueSynchronizerParam()
                {
                    IssueDto = issueJira,
                    ProjectId = project.Id,
                    Statuses = statuses,
                    IssueTypes = issueTypes
                };

                await SaveIssue(parameters, cancellationToken);
            }
        }

        private async Task<IDictionary<string, long>> RetrieveAllKeyByIdStatus(CancellationToken cancellationToken)
        {
            var list = await statusRepository.FindAllAsync(cancellationToken);
            return list.ToDictionary(x => $"{x.Key}", x => x.Id);
        }

        private async Task<IDictionary<string, long>> RetrieveAllKeyByIdIssueTypes(CancellationToken cancellationToken)
        {
            var list = await issueTypeRepository.FindAllAsync(cancellationToken);
            return list.ToDictionary(x => $"{x.Key}", x => x.Id);
        }

        private async Task<IEnumerable<IssueDto>> RetrieveAllIssueInProject(ConfigurationSynchronizerDto configurationSynchronizerDto, string projectName, DateTime? lastUpdated, CancellationToken cancellationToken)
        {
            var startAt = 0L;
            var issues = new List<IssueDto>();

            bool goToNextPage;
            do
            {
                var searchResult = await searchPost.Execute(configurationSynchronizerDto.Username, configurationSynchronizerDto.Password, MountSearchData(projectName, lastUpdated, startAt), cancellationToken);

                issues.AddRange(searchResult.Issues);

                startAt += SEARCH_ISSUE_MAX_RESULTS;
                goToNextPage = (searchResult.StartAt + searchResult.MaxResults) < searchResult.Total;
            } while (goToNextPage);

            return issues;
        }

        private static SearchInputDto MountSearchData(string projectName, DateTime? lastUpdated, long startAt)
            => SearchInputDtoBuilder.GetInstance()
                                    .AddStartAt(startAt)
                                    .AddJql(MountJql(projectName, lastUpdated))
                                    .ToBuild();

        private static JqlBuilder MountJql(string projectName, DateTime? lastUpdated)
        {
            var jql = JqlBuilder.GetInstance()
                                .AddProjectCriteria(projectName)
                                .AddKeyOrderBy();

            if (lastUpdated.HasValue)
                jql.AddUpdatedIsGreaterEqualThan(lastUpdated.Value);

            return jql;
        }

        private async Task<IssueDto> RetrieveIssueData(ConfigurationSynchronizerDto configurationSynchronizerDto, string issueKey, CancellationToken cancellationToken)
            => await issueGet.Execute(configurationSynchronizerDto.Username, configurationSynchronizerDto.Password, issueKey, cancellationToken);

        private async Task SaveIssue(IssueSynchronizerParam parameters, CancellationToken cancellationToken)
        {
            await issueDataSynchronizerService.Save(parameters, cancellationToken);
            await issueImpedimentSynchronizerService.Save(parameters, cancellationToken);
            await issueStatusHistorySynchronizerService.Save(parameters, cancellationToken);
            await issueEpicDataSynchronizerService.Save(parameters, cancellationToken);
        }
    }
}