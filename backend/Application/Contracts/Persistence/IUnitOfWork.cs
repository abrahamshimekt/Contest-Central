using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IStandingRepository StandingRepository{get;}
        IGroupRepository GroupRepository {get;}
        IContestRepository ContestRepository{get;}
        IProblemRepository ProblemRepository{get;}
        Task<int> CommitAsync();
        

    }
}