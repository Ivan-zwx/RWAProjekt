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

        [Required(ErrorMessage = "Unesite email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite korisnicko ime")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Unesite lozinku")]
        public string PasswordHash { get; set; }
        public string CreatedAt { get; set; }
        public string DeletedAt { get; set; }

        [Required(ErrorMessage = "Unesite broj telefona")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Unesite email adresu")]
        public string Address { get; set; }
    }
}
