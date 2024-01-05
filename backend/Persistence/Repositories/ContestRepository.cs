using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories
{
    public class ContestRepository : GenericRepository<Contest>,IContestRepository
    {
        private readonly ContestCentralDbContext _context;

        public ContestRepository(ContestCentralDbContext context):base(context){
            _context = context;
        }
    }
}