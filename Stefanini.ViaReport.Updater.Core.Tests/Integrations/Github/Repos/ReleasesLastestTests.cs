﻿using FluentAssertions;
using Stefanini.ViaReport.Updater.Core.Integrations.Github.Repos;
using Stefanini.ViaReport.Updater.Core.Tests.Mocks.Dtos;
using System.Threading.Tasks;
using Xunit;

namespace Stefanini.ViaReport.Updater.Core.Tests.Integrations.Github.Repos
{
    public class ReleasesLastestTests : BaseGithubApiTests
    {
        private readonly IReleasesLastest releasesLastest;

        public ReleasesLastestTests()
            : base()
        {
            ConfigureReleasesLastest();

            releasesLastest = new ReleasesLastest(GetConfiguration());
        }

        [Fact(DisplayName = "[ReleasesLastest.Execute] Deve recuperar a última release publicada no GitHub.")]
        public async Task DeveRetornarListaProjetosCadastradosJira()
        {
            var expected = ReleaseDtoMock.Create();
            var actual = await releasesLastest.Execute(GetCancellationToken());

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}