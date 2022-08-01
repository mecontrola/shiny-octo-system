﻿using Stefanini.ViaReport.Core.Tests.Mocks;
using Stefanini.ViaReport.Core.Tests.Mocks.Builders.Jira;
using Stefanini.ViaReport.Data.Dtos.Jira.Inputs;
using System;

namespace Stefanini.ViaReport.Updater.Core.Tests.Mocks.Data.Dtos.Jira.Inputs
{
    public class SearchInputDtoMock
    {
        public static SearchInputDto CreateWithJqlCustom(string jql)
            => new()
            {
                Fields = DataMock.JIRA_SEARCH_FIELDS_DEFAULT,
                Jql = jql,
                MaxResults = DataMock.JIRA_SEARCH_MAX_RESULTS_DEFAULT,
                StartAt = DataMock.JIRA_SEARCH_START_AT_DEFAULT
            };

        public static SearchInputDto CreateWithCriteriaProjectOrderByKey()
            => CreateWithJqlCustom(JqlBuilderMock.CreateCriteriaProjectOrderByKey().ToBuild());

        public static SearchInputDto CreateWithCriteriaProjectOrderByKeyAndStartAt256()
            => new()
            {
                Fields = DataMock.JIRA_SEARCH_FIELDS_DEFAULT,
                Jql = JqlBuilderMock.CreateCriteriaProjectOrderByKey().ToBuild(),
                MaxResults = DataMock.JIRA_SEARCH_MAX_RESULTS_DEFAULT,
                StartAt = DataMock.JIRA_SEARCH_START_AT_256
            };

        public static SearchInputDto CreateWithCriteriaProjectOrderByKeyAndMaxResults512()
            => new()
            {
                Fields = DataMock.JIRA_SEARCH_FIELDS_DEFAULT,
                Jql = JqlBuilderMock.CreateCriteriaProjectOrderByKey().ToBuild(),
                MaxResults = DataMock.JIRA_SEARCH_MAX_RESULTS_512,
                StartAt = DataMock.JIRA_SEARCH_START_AT_DEFAULT
            };

        public static SearchInputDto CreateWithCriteriaProjectOrderByKeyAndFieldsStatusAndSummary()
            => new()
            {
                Fields = DataMock.JIRA_SEARCH_FIELDS_STATUS_AND_SUMMARY,
                Jql = JqlBuilderMock.CreateCriteriaProjectOrderByKey().ToBuild(),
                MaxResults = DataMock.JIRA_SEARCH_MAX_RESULTS_DEFAULT,
                StartAt = DataMock.JIRA_SEARCH_START_AT_DEFAULT
            };

        public static SearchInputDto CreatePart1To5From10()
            => new()
            {
                Fields = Array.Empty<string>(),
                Jql = JqlBuilderMock.CreateCriteriaProjectOrderByKey().ToBuild(),
                MaxResults = DataMock.JIRA_SEARCH_MAX_RESULTS_DEFAULT,
                StartAt = DataMock.JIRA_SEARCH_START_AT_DEFAULT
            };

        public static SearchInputDto CreatePart6To10From10()
            => new()
            {
                Fields = Array.Empty<string>(),
                Jql = JqlBuilderMock.CreateCriteriaProjectOrderByKey().ToBuild(),
                MaxResults = DataMock.JIRA_SEARCH_MAX_RESULTS_DEFAULT,
                StartAt = DataMock.JIRA_SEARCH_START_AT_256
            };
    }
}