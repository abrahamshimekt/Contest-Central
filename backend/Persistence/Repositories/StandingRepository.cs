using System;
using Application.Contracts.Persistence;
using Application.Features.Standings.Dtos;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StandingRepository : GenericRepository<Standing>, IStandingRepository
    {
        private readonly ContestCentralDbContext _context;
        public StandingRepository(ContestCentralDbContext context) : base(context)
        {
            _context = context;

        }


        public async Task<IReadOnlyList<Standing>> GetStandingFilter(Guid contestId, string university, string groupId)
        {
            var standingsQuery = _context.Standings.Where(s => s.ContestId == contestId);

            if (university != "any")
            {
                standingsQuery = standingsQuery.Where(s => _context.Users.Any(u => u.UserName == s.UserHandle && u.UniversityName == university));
            }

            if (groupId != "any")
            {
                standingsQuery = standingsQuery.Where(s => _context.Users.Any(u => u.UserName == s.UserHandle && u.GroupId == groupId));
            }

            var standings = await standingsQuery.ToListAsync();
            return standings;
        }


        public async Task<UserStatisticsDto> GetUserStatistics(string userHandle)
        {
            var userStatistics = new UserStatisticsDto();
            var standings = await _context.Standings.Where(s => s.UserHandle == userHandle).ToListAsync();
            userStatistics.NumberOfProblemsSolved = standings.Sum(s => s.SolvedProblems);

            foreach (var standing in standings)
            {

                userStatistics.NumberOfContestsParticipated++;
                var problemsInContest = await _context.Problems
                    .Where(p => p.ContestId == standing.ContestId)
                    .CountAsync();

                userStatistics.TotalContestProblems += problemsInContest;

            }

            return userStatistics;


        }
    }
}