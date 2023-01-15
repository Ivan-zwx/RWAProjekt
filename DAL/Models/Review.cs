using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public string CreatedAt { get; set; }
        public string Details { get; set; }
        public int Stars { get; set; }
    }
}
