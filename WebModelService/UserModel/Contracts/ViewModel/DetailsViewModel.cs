using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.UserModel.contracts.DTO;

namespace WebModelServices.UserModel.Contracts.ViewModel
{
    public class DetailsViewModel
    {
        public UserViewModel User{ get; set; }
        public IList<UserBookViewModel> Book{ get; set; }
        public IList<BorrowHistoryViewModel> Borrows{ get; set; }
        public DetailsViewModel()
        {
            User = new UserViewModel();
            Book = new List<UserBookViewModel>();
            Borrows = new List<BorrowHistoryViewModel>();
        }
    }
}
