using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.UserModel.contracts.DTO
{
    public class UserViewModel
    {
        [Required]
        public int UserId {get;set;}

        [Required]
        public string FirstName{get; set;}
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate {get; set;}
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public DateTime AddDate { get; set; }
       
        public DateTime? Modified { get; set; }
        [Required]
        public int BooksBorrowed { get; set; }
        [Required]
        public bool isActive { get; set; }
     
    }
}
