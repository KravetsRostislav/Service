using System.Threading.Tasks;
using Shop.API.Domain.Models;
using System.Collections.Generic;
using Shop.API.Domain.Services.Communication;

namespace Shop.API.Domain.Services
{
    public interface IUserRolesService
    {
        Task<IEnumerable<User>> ListUsersByRoleAsync(int roleId);
        Task<UserResponse> SetRole(int userId, int roleId);
        Task<UserResponse> DeleteRole(int userId, int roleId);
    }
}