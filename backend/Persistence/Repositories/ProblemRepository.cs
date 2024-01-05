using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProblemRepository : GenericRepository<Problem>,IProblemRepository
    {
        private readonly ContestCentralDbContext _context;
        public ProblemRepository(ContestCentralDbContext context):base(context){
            _context=context;
        }

        public  async Task<Problem> Get(string Id)
        {
          var problem = await _context.Problems.FirstOrDefaultAsync(p=>p.Id==Id);
          return problem;
        }
    }
}