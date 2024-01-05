using System;
using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        private readonly ContestCentralDbContext _context;
        public GroupRepository(ContestCentralDbContext context) : base(context)
        {
            _context = context;
            
        }
    }
}