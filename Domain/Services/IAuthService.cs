using System.Threading.Tasks;
using Shop.API.Domain.Models;

namespace Shop.API.Domain.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(string login, string password);
    }
}