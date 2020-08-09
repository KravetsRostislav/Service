using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Domain.Models;

namespace Shop.API.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> ListAsync();
        Task AddAsync(Role user);
        void Update(Role user);
        Task<Role> FindByIdAsync(int id);
        void Remove(Role user);
    }
}