using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Responses;

namespace Application.Contracts.Persistence
{
    public interface IGenericRepository<T>   where T: class
    {
        
    Task<T> Get(string id);
    Task<PaginatedResponse<T>> GetAll(int pageIndex, int pageSize);
    Task<int> GetCountAsync();
    Task<T> Add(T entity);
    Task<int> Update(T entity);
    Task<int> Delete(T entity);
        
    }
}