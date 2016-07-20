using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.BorrowModel.ViewModel;

namespace WebModelServices.BorrowModel
{
     interface IBorrowService
    {
        IList<UsersAddBorrowViewModel> GetAllUsers();
        BorrowsViewModel GetBorrowListViewModel();
        void SaveAllBorrowsToUser(BorrowsToSaveModel borrowsToSaveModel);
        IList<UserWithBorrowsViewModel> GetUsersWithBorrows();
        void ReturnBook(int borrowId);
        ReturnBookFromUserViewModel GetBorrowedBooksFromUser(int userId);
    }
}
