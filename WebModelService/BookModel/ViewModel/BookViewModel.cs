using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.BookModel.ViewModel
{
    public class BookViewModel
    {


        public int BookId { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string ISBN { get; set; }


        public int Count { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }
}
