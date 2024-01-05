using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Contracts.Persistence
{
    public interface IProblemRepository : IGenericRepository<Problem>
    {
        Task<Problem> Get(string Id);
    }
}