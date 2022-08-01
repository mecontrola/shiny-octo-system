﻿using NSubstitute;
using NSubstitute.Equivalency;
using Stefanini.Core.TestingTools;
using Stefanini.ViaReport.Core.Integrations.Jira.V2.Projects;
using Stefanini.ViaReport.Core.Services;
using Stefanini.ViaReport.Core.Tests.Mocks;
using Stefanini.ViaReport.Updater.Core.Tests.Mocks.Data.Dtos.Jira.Inputs;
using System.Threading;
using Xunit;

namespace Stefanini.ViaReport.Core.Tests.Services
{
    public class CFDExportReportIntegrationServiceTests : BaseAsyncMethods
    {
        private readonly ICFDExportReportIntegrationService service;
        private readonly ISearchPost api;

        public CFDExportReportIntegrationServiceTests()
        {
            api = Substitute.For<ISearchPost>();

            service = new CFDExportReportIntegrationService(api);
        }

        private static string GetJqlExpected()
            => "project = 'Search' AND status NOT IN (Removed,Cancelled) AND issuetype IN (3,4,7)";

        [Fact(DisplayName = "[IssuesEpicByLabelService.GetData] Deve montar o JQL de acordo com o parametros criados.")]
        public async void DeveMontarJQLCorretamente()
        {
            await service.GetData(DataMock.VALUE_USERNAME,
                                  DataMock.VALUE_PASSWORD,
                                  DataMock.TEXT_SEARCH_PROJECT,
                                  GetCancellationToken());

            var expected = SearchInputDtoMock.CreateWithJqlCustom(GetJqlExpected());

            await api.Received().Execute(Arg.Is<string>(x => x.Equals(DataMock.VALUE_USERNAME)),
                                         Arg.Is<string>(x => x.Equals(DataMock.VALUE_PASSWORD)),
                                         ArgEx.IsEquivalentTo(expected),
                                         Arg.Any<CancellationToken>());
        }
    }
}