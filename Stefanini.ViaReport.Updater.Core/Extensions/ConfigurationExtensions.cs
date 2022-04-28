﻿using Microsoft.Extensions.Configuration;
using Stefanini.ViaReport.Updater.Core.Data.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Stefanini.ViaReport.Updater.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static IUpdaterConfiguration GetUpdaterConfiguration(this IConfiguration configuration)
            => new UpdaterConfiguration
            {
                ApplicationName = "AHM Report.exe",
                FilenameToDownload = "AHM.Report-{0}_{1}.zip",
                RenameFileDownloaded = "update.zip",
                GitUrl = "https://api.github.com"
            };
    }
}