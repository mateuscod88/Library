using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.BorrowModel.ViewModel
{
    public class BorrowsViewModel
    {
        public BorrowsViewModel()
        {
            BorrowedBooks = new List<BorrowedBookViewModel>();
        }
        public IList<BorrowedBookViewModel> BorrowedBooks { get; set; }

    }
}
