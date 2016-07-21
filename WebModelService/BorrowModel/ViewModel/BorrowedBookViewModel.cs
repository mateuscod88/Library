using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.BorrowModel.ViewModel
{
    public class BorrowedBookViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int BookId { get; set; }

        public int BorrowId { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }


        public int Count { get; set; }

        public string UserName {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        public bool iisActive { get; set; }

        }
    

}
