using Core.BussinessLogic.Managers;
using Core.DataProviders.Repositories;
using Data.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessLogic.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            Users = new List<UserDummy>();
        }

        public BasicUser LoggedUser { get; set; }
        public List<UserDummy> Users { get; set; }

        public Task<BasicUser> AuthenticateLogin(string name, string password)
        {
            return _userRepository.AuthenticateUser(name.ToUpper(), password);
        }

        public async Task FetchAllUsers()
        {
            var usrs = await _userRepository.GetAllUsers();
            foreach (var usr in usrs)
            {
                Users.Add(new UserDummy { ID = usr.id, FirstName = usr.FirstName, PhoneNumber = usr.PhoneNumber, Surname = usr.Surname });
            }
        }

        public Task<UserDummy> GetUserById(int id)
        {
            return Task.Run(() =>
           {
               return Users.Where(usr => usr.ID == id).FirstOrDefault();
           });
        }

        public Task RegisterToDatabase(string username, string password, string firstName, string lastName, string phoneNumber)
        {
            return _userRepository.RegisterUserToDatabase(new BasicUser { FirstName = firstName, Password = password, PhoneNumber = phoneNumber, Surname = lastName, UserName = username.ToUpper() });
        }
    }
}
