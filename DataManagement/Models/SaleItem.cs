using System;
using System.Collections.Generic;

namespace DataManagement.Models
{
    public partial class SaleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? SolidPrice { get; set; }
        public double? AuctionPrice { get; set; }
        public string AuctionWinner { get; set; }
        public DateTime? TimeAdded { get; set; }
        public int? UserId { get; set; }

        public virtual BasicUser User { get; set; }
    }
}
