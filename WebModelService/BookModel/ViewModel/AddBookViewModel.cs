using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.BookModel.ViewModel
{
    public class AddBookViewModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string ISBN { get; set; }

        [Required]
        public int Count { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
