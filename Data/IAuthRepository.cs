using System.Threading.Tasks;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}