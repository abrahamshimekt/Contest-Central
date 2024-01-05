using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContestCentralDbContext _context;
        public UnitOfWork(ContestCentralDbContext context)
        {
            _context = context;
        }

        private IStandingRepository _StandingRepository;
        private IContestRepository _ContestRepository;
        private IGroupRepository _GroupRepository;
        private IProblemRepository _ProblemRepository;
        public IStandingRepository StandingRepository
        {
            get
            {
                if (_StandingRepository == null)
                    _StandingRepository = new StandingRepository(_context);
                return _StandingRepository;
            }
        }
        public IGroupRepository GroupRepository
        {
            get
            {
                if (_GroupRepository == null)
                    _GroupRepository = new GroupRepository(_context);
                return _GroupRepository;
            }
        }

        public IContestRepository ContestRepository
        {
            get
            {
                return _ContestRepository ??= new ContestRepository(_context);
            }
        }

        public IProblemRepository ProblemRepository{
            get{
                return _ProblemRepository??= new ProblemRepository(_context);
            }
        }

        public async Task<int> CommitAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }

        }



        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);

        }
    }


}