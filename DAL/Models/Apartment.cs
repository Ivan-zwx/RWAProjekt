using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string DeletedAt { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }
        public int BeachDistance { get; set; }
        public string Owner { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
    }
}
