using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter an email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string PasswordHash { get; set; }
        public string CreatedAt { get; set; }
        public string DeletedAt { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }
    }
}
