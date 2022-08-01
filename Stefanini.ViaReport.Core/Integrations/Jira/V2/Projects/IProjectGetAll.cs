﻿using Stefanini.ViaReport.Data.Dtos.Jira;
using System.Threading;
using System.Threading.Tasks;

namespace Stefanini.ViaReport.Core.Integrations.Jira.V2.Projects
{
    public interface IProjectGetAll
    {
        Task<ProjectDto[]> Execute(string username, string password, CancellationToken cancellationToken);
    }
}