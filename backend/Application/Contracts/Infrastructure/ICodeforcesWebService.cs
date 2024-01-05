using System;

namespace Application.Contracts.Infrastructure
{
    public interface ICodeforcesWebService
    {
        public  Task GetStanding(int contestId);
    }
}