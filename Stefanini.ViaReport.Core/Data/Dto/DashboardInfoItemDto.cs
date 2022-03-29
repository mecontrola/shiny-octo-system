﻿using System;
using System.Collections.Generic;

namespace Stefanini.ViaReport.Core.Data.Dto
{
    public class DashboardInfoItemDto
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public IList<IssueInfoDto> Issues { get; set; }

    }
}