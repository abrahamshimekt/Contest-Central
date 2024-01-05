using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
public readonly ContestCentralDbContext _dbContext;

    public GenericRepository(ContestCentralDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T> Get(string id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<PaginatedResponse<T>> GetAll(int pageNumber, int pageSize)
    {
        var query = _dbContext.Set<T>().AsNoTracking();
        var temp = await query.CountAsync();
        var response = new PaginatedResponse<T>(){
            Data = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),
            Count = temp
        };
        return response;
    }

    public virtual async Task<int> GetCountAsync()
    {
        return await _dbContext.Set<T>().CountAsync();
    }

    public virtual async Task<T> Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<int> Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<int> Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return await _dbContext.SaveChangesAsync();
    }
    }
}