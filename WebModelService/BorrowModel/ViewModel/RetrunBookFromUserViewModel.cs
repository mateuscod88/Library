using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.UserModel.contracts.DTO;

namespace WebModelServices.BorrowModel.ViewModel
{
    public class ReturnBookFromUserViewModel
    {
        public UserViewModel UserWithBorrowsViewModel { get; set; }
        public IList<BorrowedBookViewModel> BorrowedBooksViewModel { get; set; }
        public ReturnBookFromUserViewModel()
        {
            UserWithBorrowsViewModel= new UserViewModel();
            BorrowedBooksViewModel = new List<BorrowedBookViewModel>();
        }
    }
}
