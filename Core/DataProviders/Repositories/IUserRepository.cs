using Data.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataProviders.Repositories
{
    public interface IUserRepository
    {
        Task<BasicUser> AuthenticateUser(string name, string password);
        Task RegisterUserToDatabase(BasicUser user);
        Task<BasicUser> GetUserById(int id);
        Task<List<BasicUser>> GetAllUsers();
    }
}
