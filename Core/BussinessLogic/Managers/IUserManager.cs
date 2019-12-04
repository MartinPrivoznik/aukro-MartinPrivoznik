using Data.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.BussinessLogic.Managers
{
    public interface IUserManager
    {
        BasicUser LoggedUser { get; set; }
        List<UserDummy> Users { get; set; }
        Task<BasicUser> AuthenticateLogin(string login, string password);
        Task RegisterToDatabase(string username, string password, string firstName, string lastName, string phoneNumber);
        Task<UserDummy> GetUserById(int id);
        Task FetchAllUsers();
    }

    public class UserDummy
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
