using SQLite;
using System;

namespace Data.Database.Models
{
    public class SaleItem
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long SolidPrice { get; set; } 
        public long AuctionPrice { get; set; }
        public string AuctionWinner { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
