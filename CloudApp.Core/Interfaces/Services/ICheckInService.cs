using CloudApp.Core.Dtos.CheckIn;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Services
{
    public interface ICheckInService
    {
        Task<CheckInResult> CheckInAsync(string openId, CancellationToken ct = default);
        Task<CheckInStatus> GetStatusAsync(string openId, CancellationToken ct = default);
    }
}
