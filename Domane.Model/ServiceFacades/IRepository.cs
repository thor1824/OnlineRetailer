using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailApi.Domain.Model.ServiceFacades
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAync(T entity);
        Task EditAsync(T entity);
        Task RemoveAsync(int id);
    }
}
