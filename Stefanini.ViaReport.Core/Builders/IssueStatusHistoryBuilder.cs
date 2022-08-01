﻿using MeControla.Core.Builders;
using Stefanini.ViaReport.Data.Entities;
using System;

namespace Stefanini.ViaReport.Core.Builders
{
    public class IssueStatusHistoryBuilder : BaseBuilder<IssueStatusHistoryBuilder, IssueStatusHistory>, IBuilder<IssueStatusHistory>
    {
        protected override void Initialize()
            => obj = new()
            {
                Uuid = Guid.NewGuid()
            };

        public IssueStatusHistoryBuilder SetDateTime(DateTime value)
            => Set(obj => obj.DateTime = value);

        public IssueStatusHistoryBuilder SetStatusId(long value)
            => Set(obj => obj.StatusId = value);

        public IssueStatusHistoryBuilder SetIssueId(long value)
            => Set(obj => obj.IssueId = value);
    }
}