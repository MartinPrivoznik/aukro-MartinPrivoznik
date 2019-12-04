using SQLite;

namespace Data.Database.Models
{
    public class BasicUser
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
