﻿using MeControla.Core.Data.Entities;
using System;
using System.Collections.Generic;

namespace Stefanini.ViaReport.Data.Entities
{
    public class Project : IEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; }
        public long Key { get; set; }
        public string Name { get; set; }
        public long ProjectCategoryId { get; set; }
        public bool Selected { get; set; }
        public ProjectCategory ProjectCategory { get; set; }
        public IList<Issue> Issues { get; set; }
    }
}