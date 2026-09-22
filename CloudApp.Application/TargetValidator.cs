using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Application
{
    public class TargetValidator : ITargetValidator
    {
        private readonly IConcertRepository _concertRepo;
        public TargetValidator(IConcertRepository concertRepo)
        {
            _concertRepo = concertRepo;
        }
        public Task<bool> TargetExistsAsync(int targetId, SubscriptionTargetType targetType, CancellationToken ct = default)
                        => targetType switch
                        {
                            SubscriptionTargetType.Concert => _concertRepo.ExistsAsync(targetId),
                            _ => Task.FromResult(false)
                        };
    }
}
