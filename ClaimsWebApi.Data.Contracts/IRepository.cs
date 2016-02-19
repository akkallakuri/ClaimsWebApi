using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsWebApi.Data
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> ListAllAsync(string query);
        Task<string> AddAsync(T item);
        Task<string> UpdateAsync(T item);
        Task<string> DeleteAsync(string key);
    }
}
