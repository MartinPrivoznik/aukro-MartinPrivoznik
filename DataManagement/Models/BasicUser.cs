using System;
using System.Collections.Generic;

namespace DataManagement.Models
{
    public partial class BasicUser
    {
        public BasicUser()
        {
            SaleItem = new HashSet<SaleItem>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<SaleItem> SaleItem { get; set; }
    }
}
