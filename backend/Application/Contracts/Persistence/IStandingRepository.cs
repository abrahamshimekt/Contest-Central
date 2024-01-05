using System;
using Application.Features.Standings.Dtos;
using Domain;

namespace Application.Contracts.Persistence
{
    public interface IStandingRepository : IGenericRepository<Standing>
    {
        Task<IReadOnlyList<Standing>> GetStandingFilter(Guid contestId, string university, string group);
        Task<UserStatisticsDto> GetUserStatistics(string userHandle);
        
    }
}