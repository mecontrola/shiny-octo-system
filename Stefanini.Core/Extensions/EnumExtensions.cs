﻿using System;
using System.ComponentModel;

namespace Stefanini.Core.Extensions
{
    public static class EnumExtensions
    {
#if !DEBUG
        [System.Diagnostics.DebuggerStepThrough]
#endif
        public static string GetDescription(this Enum value)
            => value.GetType()
                    .GetField(value.ToString())
                    .GetCustomAttribute<DescriptionAttribute>()?
                    .Description ?? null;
    }
}