﻿using FluentAssertions;
using Stefanini.Core.Extensions;
using Stefanini.Core.Tests.Mocks;
using Xunit;

namespace Stefanini.Core.Tests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact(DisplayName = "[DateTimeExtensions.GetWeekOfYear] Deve retornar o número da semana de um DateTime.")]
        public void DeveRetornarSemanaAnoDeDateTime()
            => DataMock.DATETIME_QUARTER_2_2000.GetWeekOfYear().Should().Be(DataMock.WEEK_YEAR);
    }
}