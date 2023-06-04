﻿using Application.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICrawlerHubService
    {
        Task RemovedAsync(Guid id, CancellationToken cancellationToken);
    }
}
