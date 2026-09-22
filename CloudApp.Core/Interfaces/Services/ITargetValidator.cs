using CloudApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Services
{
    public interface ITargetValidator
    {
        Task<bool> TargetExistsAsync(int targetId, SubscriptionTargetType targetType, CancellationToken ct = default);
    }
}
