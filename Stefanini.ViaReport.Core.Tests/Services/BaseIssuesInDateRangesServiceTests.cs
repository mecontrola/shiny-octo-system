﻿using NSubstitute;
using Stefanini.ViaReport.Core.Data.Dto.Jira.Inputs;
using Stefanini.ViaReport.Core.Integrations.Jira.V2.Projects;
using Stefanini.ViaReport.Core.Services;
using Stefanini.ViaReport.Core.Tests.TestUtils;
using System;
using System.Threading;

namespace Stefanini.ViaReport.Core.Tests.Services
{
    public abstract class BaseIssuesInDateRangesServiceTests<T> : BaseAsyncMethods
        where T : BaseIssuesInDateRangesService
    {
        private const string PROJECT = "project";

        private static readonly DateTime DATE_INI = new(2000, 1, 1);
        private static readonly DateTime DATE_END = new(2000, 12, 31);

        private readonly IBaseIssuesInDateRangesService service;
        private readonly ISearchPost api;

        public BaseIssuesInDateRangesServiceTests()
        {
            api = Substitute.For<ISearchPost>();

            service = (T)Activator.CreateInstance(typeof(T), new object[] { api });
        }

        protected abstract string GetJqlExpected();

        protected void RunTest()
        {
            service.GetData(string.Empty, string.Empty, PROJECT, DATE_INI, DATE_END, GetCancellationToken());

            api.Received().Execute(Arg.Any<string>(),
                                   Arg.Any<string>(),
                                   Arg.Is<SearchInputDto>(x => CheckEqualJql(x)),
                                   Arg.Any<CancellationToken>());
        }

        private bool CheckEqualJql(SearchInputDto input)
            => input.Jql.Equals(GetJqlExpected());
    }
}