using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.BorrowModel.ViewModel
{
    public class BorrowsToSaveModel
    {
        public UsersAddBorrowViewModel User {get;set;}
        public IList<BooksAddBorrowViewModel> Borrows { get; set; } 
    }
}
