using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.UserModel.Contracts.ViewModel
{
    public class UserBookViewModel
    {
        public int BookId { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public string ISBN { get; set; }
        public bool IsReturned { get; set; }

        
        
        
    }
}
