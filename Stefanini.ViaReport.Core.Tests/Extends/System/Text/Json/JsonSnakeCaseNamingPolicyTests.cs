﻿using FluentAssertions;
using Stefanini.ViaReport.Core.Tests.Data.Entities;
using Stefanini.ViaReport.Core.Tests.Mocks;
using Stefanini.ViaReport.Core.Tests.Mocks.Entities;
using System.Text.Json;
using Xunit;

namespace Stefanini.ViaReport.Core.Tests.Extends.System.Text.Json
{
    public class JsonSnakeCaseNamingPolicyTests
    {
        private readonly JsonSerializerOptions options;

        public JsonSnakeCaseNamingPolicyTests()
        {
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
            };
        }

        [Fact(DisplayName = "[JsonSnakeCaseNamingPolicy.ConvertName] Deve retornar o nome dos atributos no formato snake case.")]
        public void DeveRetornarAtributoQuandoExistir()
        {
            var actual = JsonSerializer.Deserialize<ClassTest>(DataMock.JSON_CLASS_TEST, options);
            var expected = ClassTestMock.CreateNoDateTime();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ClassTest>();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}