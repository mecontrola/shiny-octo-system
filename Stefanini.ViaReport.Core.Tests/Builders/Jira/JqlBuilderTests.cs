﻿using FluentAssertions;
using Stefanini.Core.Extensions;
using Stefanini.ViaReport.Core.Builders.Jira;
using Stefanini.ViaReport.Core.Tests.Mocks;
using Stefanini.ViaReport.Data.Enums;
using Xunit;

namespace Stefanini.ViaReport.Core.Tests.Builders.Jira
{
    public class JqlBuilderTests
    {
        private const string VALUE_TESTE_1 = "Teste1";
        private const string VALUE_TESTE_2 = "Teste2";

        [Fact(DisplayName = "[JqlBuilder.AddResolvedIsNull] Deve gerar criteria IS NULL do campo resolved.")]
        public void DeveGerarCriteriaCampoResolvedNull()
        {
            var expected = $"resolved IS NULL";
            var actual = JqlBuilder.GetInstance().AddResolvedIsNull().ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddFixVersionIsNull] Deve gerar criteria IS NULL do campo fixVersion.")]
        public void DeveGerarCriteriaCampoFixVersionNull()
        {
            var expected = $"fixVersion IS NULL";
            var actual = JqlBuilder.GetInstance().AddFixVersionIsNull().ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddLabelsCriteria] Deve gerar criteria IN do campo labels.")]
        public void DeveGerarCriteriaCampoLabels()
        {
            var expected = $"labels IN ('{VALUE_TESTE_1}','{VALUE_TESTE_2}')";
            var actual = JqlBuilder.GetInstance().AddLabelsCriteria(new string[] { VALUE_TESTE_1, VALUE_TESTE_2 }).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddProjectCriteria] Deve gerar criteria = do campo project.")]
        public void DeveGerarCriteriaCampoProject()
        {
            var expected = $"project = '{VALUE_TESTE_1}'";
            var actual = JqlBuilder.GetInstance().AddProjectCriteria(VALUE_TESTE_1).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddInIssueTypesCriteria] Deve gerar criteria IN do campo IN issueTypes.")]
        public void DeveGerarCriteriaCampoInIssueTypes()
        {
            var expected = $"issuetype IN ({(int)IssueTypes.TechnicalDebt})";
            var actual = JqlBuilder.GetInstance().AddInIssueTypesCriteria(IssueTypes.TechnicalDebt).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddNotInIssueTypesCriteria] Deve gerar criteria NOT IN do campo issueTypes.")]
        public void DeveGerarCriteriaCampoNotInIssueTypes()
        {
            var expected = $"issuetype NOT IN ({(int)IssueTypes.TechnicalDebt})";
            var actual = JqlBuilder.GetInstance().AddNotInIssueTypesCriteria(IssueTypes.TechnicalDebt).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddStatusCriteria] Deve gerar criteria IN do campo status.")]
        public void DeveGerarCriteriaCampoStatus()
        {
            var expected = $"status IN ({StatusTypes.Cancelled},{StatusTypes.Done})";
            var actual = JqlBuilder.GetInstance().AddStatusCriteria(StatusTypes.Cancelled, StatusTypes.Done).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddCreatedIsLessThan] Deve gerar criteria <= do campo created.")]
        public void DeveGerarCriteriaCampoIsLessThanCreated()
        {
            var expected = $"created < '{DataMock.DATETIME_FIRST_DAY_YEAR:yyyy-MM-dd}'";
            var actual = JqlBuilder.GetInstance().AddCreatedIsLessThan(DataMock.DATETIME_FIRST_DAY_YEAR).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddUpdatedIsGreaterEqualThan] Deve gerar criteria >= do campo updated.")]
        public void DeveGerarCriteriaCampoIsGreaterEqualThanUpdated()
        {
            var expected = $"updated >= '{DataMock.DATETIME_FIRST_DAY_YEAR:yyyy-MM-dd}'";
            var actual = JqlBuilder.GetInstance().AddUpdatedIsGreaterEqualThan(DataMock.DATETIME_FIRST_DAY_YEAR).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddBetweenCreatedDateCriteria] Deve gerar criteria do campo created.")]
        public void DeveGerarCriteriaCampoBetweenCreated()
        {
            var expected = $"created >= '{DataMock.DATETIME_FIRST_DAY_YEAR:yyyy-MM-dd}' AND created <= '{DataMock.DATETIME_LAST_DAY_YEAR:yyyy-MM-dd}'";
            var actual = JqlBuilder.GetInstance().AddBetweenCreatedDateCriteria(DataMock.DATETIME_FIRST_DAY_YEAR, DataMock.DATETIME_LAST_DAY_YEAR).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddBetweenResolvedDateCriteria] Deve gerar criteria do campo resolved.")]
        public void DeveGerarCriteriaCampoBetweenResolved()
        {
            var expected = $"resolved >= '{DataMock.DATETIME_FIRST_DAY_YEAR:yyyy-MM-dd}' AND resolved <= '{DataMock.DATETIME_LAST_DAY_YEAR:yyyy-MM-dd}'";
            var actual = JqlBuilder.GetInstance().AddBetweenResolvedDateCriteria(DataMock.DATETIME_FIRST_DAY_YEAR, DataMock.DATETIME_LAST_DAY_YEAR).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddNotInDeletedStatusesCriteria] Deve gerar criteria NOT IN do campo status com valores Removed e Cancelled.")]
        public void DeveGerarCriteriaCampoNotInDeletedStatuses()
        {
            var expected = $"status NOT IN ({StatusTypes.Removed},{StatusTypes.Cancelled})";
            var actual = JqlBuilder.GetInstance().AddNotInDeletedStatusesCriteria().ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddNotInDeletedStatusesCriteria] Deve gerar criteria NOT IN do campo status com valores Removed, Cancelled e Done.")]
        public void DeveGerarCriteriaCampoNotInDeletedStatusesComParametroDone()
        {
            var expected = $"status NOT IN ({StatusTypes.Removed},{StatusTypes.Cancelled},{StatusTypes.Done})";
            var actual = JqlBuilder.GetInstance().AddNotInDeletedStatusesCriteria(StatusTypes.Done).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddInDeletedStatusesCriteria] Deve gerar criteria IN do campo status com valores Removed e Cancelled.")]
        public void DeveGerarCriteriaCampoInDeletedStatuses()
        {
            var expected = $"status IN ({StatusTypes.Removed},{StatusTypes.Cancelled})";
            var actual = JqlBuilder.GetInstance().AddInDeletedStatusesCriteria().ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddInDeletedStatusesCriteria] Deve gerar criteria IN do campo status com valores Removed, Cancelled e Done.")]
        public void DeveGerarCriteriaCampoInDeletedStatusesComParametroDone()
        {
            var expected = $"status IN ({StatusTypes.Removed},{StatusTypes.Cancelled},{StatusTypes.Done})";
            var actual = JqlBuilder.GetInstance().AddInDeletedStatusesCriteria(StatusTypes.Done).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddNotInStatusCategoriesCriteria] Deve gerar criteria do campo resolved.")]
        public void DeveGerarCriteriaCampoNotInStatusCategories()
        {
            var expected = $"statusCategory NOT IN ('{StatusCategories.ToDo.GetDescription()}','{StatusCategories.InProgress.GetDescription()}')";
            var actual = JqlBuilder.GetInstance().AddNotInStatusCategoriesCriteria(StatusCategories.ToDo, StatusCategories.InProgress).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddBasicIssueTypesCriteria] Deve gerar criteria do campo resolved.")]
        public void DeveGerarCriteriaCampoResolved()
        {
            var expected = $"issueType IN ({(int)IssueTypes.Bug},{(int)IssueTypes.Task},{(int)IssueTypes.Epic},{(int)IssueTypes.Story},{(int)IssueTypes.TechnicalDebt})";
            var actual = JqlBuilder.GetInstance().AddBasicIssueTypesCriteria().ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddOr] Deve gerar criteria OR dos campos resolved e fixVersion.")]
        public void DeveGerarCriteriaOrCamposResolvedNullFixVersionNull()
        {
            var expected = $"((resolved IS NULL ) OR (fixVersion IS NULL ))";
            var actual = JqlBuilder.GetInstance().AddOr(x => x.AddResolvedIsNull().AddFixVersionIsNull()).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddAnd] Deve gerar criteria AND dos campos resolved e fixVersion.")]
        public void DeveGerarCriteriaAndCamposResolvedNullFixVersionNull()
        {
            var expected = $"resolved IS NULL AND fixVersion IS NULL";
            var actual = JqlBuilder.GetInstance().AddAnd(x => x.AddResolvedIsNull().AddFixVersionIsNull()).ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddOrderByKey] Deve gerar criteria ORDER BY do campo key.")]
        public void DeveGerarOrderByCampoKey()
        {
            var expected = "order by key";
            var actual = JqlBuilder.GetInstance().AddKeyOrderBy().ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "[JqlBuilder.AddOrderByKey] Deve gerar criteria ORDER BY sempre no final da jql.")]
        public void DeveGerarOrderByCampoKeyNoFim()
        {
            var expected = "fixVersion IS NULL order by key";
            var actual = JqlBuilder.GetInstance().AddKeyOrderBy().AddFixVersionIsNull().AddKeyOrderBy().ToBuild();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}