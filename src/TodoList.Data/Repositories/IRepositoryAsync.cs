using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Shared;

namespace TodoList.Data.Repositories
{
    public interface IRepositoryAsync<T>
    {
        Task AddAsync(T entity);
        Task<T?> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id);
        Task<int> SaveAsync();
    }
}
