﻿using AutoMapper;
using MeControla.Core.Mappers;
using Stefanini.Core.Extensions;
using Stefanini.ViaReport.Core.Helpers;
using Stefanini.ViaReport.Data.Dtos.Jira;
using Stefanini.ViaReport.Data.Entities;

namespace Stefanini.ViaReport.Core.Mappers.DtoToEntity
{
    public class JiraIssueDtoToEntityMapper : BaseMapper<IssueDto, Issue>, IJiraIssueDtoToEntityMapper
    {
        private readonly IIssueFieldsValidationHelper issueFieldsValidationHelper;
        private readonly IMountJiraUrlHelper mountJiraUrlHelper;

        public JiraIssueDtoToEntityMapper(IIssueFieldsValidationHelper issueFieldsValidationHelper,
                                          IMountJiraUrlHelper mountJiraUrlHelper)
        {
            this.issueFieldsValidationHelper = issueFieldsValidationHelper;
            this.mountJiraUrlHelper = mountJiraUrlHelper;
        }

        protected override IMappingExpression<IssueDto, Issue> CreateMap(IMapperConfigurationExpression cfg)
            => cfg.CreateMap<IssueDto, Issue>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.Key, opt => opt.MapFrom(source => source.Key))
                  .ForMember(dest => dest.Summary, opt => opt.MapFrom(source => source.Fields.Summary))
                  .ForMember(dest => dest.Incident, opt => opt.MapFrom(source => issueFieldsValidationHelper.HasLabelIndicent(source.Fields.Labels)))
                  .ForMember(dest => dest.Created, opt => opt.MapFrom(source => source.Fields.Created))
                  .ForMember(dest => dest.Updated, opt => opt.Ignore())
                  .ForMember(dest => dest.Resolved, opt => opt.MapFrom(source => source.Fields.Resolutiondate))
                  .ForMember(dest => dest.Link, opt => opt.MapFrom(source => mountJiraUrlHelper.GetIssueUrl(source.Self, source.Key)))
                  .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                  .ForMember(dest => dest.Project, opt => opt.Ignore())
                  .ForMember(dest => dest.StatusId, opt => opt.Ignore())
                  .ForMember(dest => dest.Status, opt => opt.Ignore())
                  .ForMember(dest => dest.Statuses, opt => opt.Ignore())
                  .ForMember(dest => dest.CustomField14503, opt => opt.MapFrom(source => source.Fields.Customfield_14503.ToDateTime()));
    }
}