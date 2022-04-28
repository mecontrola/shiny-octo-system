﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stefanini.ViaReport.Updater.Core.Services
{
    public interface IManagerUpdateService
    {
        Task CheckAndDownloadUpdate(Action<string> fncUpdateStatus, Action<bool, long> fncUpdateProgress, CancellationToken cancellationToken);
    }
}