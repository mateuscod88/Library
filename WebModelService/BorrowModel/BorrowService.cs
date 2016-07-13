using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.BorrowModel.ViewModel;

namespace WebModelServices.BorrowModel
{
    public class BorrowService : IBorrowService
    {
        private BookLibraryEF _context;
        public BorrowService()
        {
            _context = new BookLibraryEF();
        }
        public BorrowsViewModel GetBorrowListViewModel()
        {
            BorrowsViewModel borrowsViewModel = new BorrowsViewModel();
            var selectedBorrows = from user in _context.User
                                  join borrow in _context.Borrow on user.UserId equals borrow.UserId
                                  where borrow.IsReturned == false
                                  join book in _context.Books on borrow.BookId equals book.BookId
                                  select new
                                  {
                                      Author = book.Author,
                                      Title = book.Title,
                                     ISBN = book.ISBN,
                                     Count = book.Count,
                                     BookId = book.BookId,
                                     FirstName = user.FirstName,
                                     LastName = user.LastName,
                                     UserId = user.UserId,
                                     FromDate = borrow.FromDate,
                                     ToDate = borrow.ToDate

                                     };
            foreach(var borrow in selectedBorrows)
            {

                BorrowedBookViewModel borrowedBookViewModel = new BorrowedBookViewModel();
                UserWithBorrowsViewModel userWithBorrowsViewModel = new UserWithBorrowsViewModel();
                borrowedBookViewModel.BookId = borrow.BookId;
                borrowedBookViewModel.Author = borrow.Author;
                borrowedBookViewModel.Title = borrow.Title;
                borrowedBookViewModel.Count = borrow.Count;
                borrowedBookViewModel.ISBN = borrow.ISBN;
                borrowedBookViewModel.UserName = borrow.FirstName +" "+ borrow.LastName;
                borrowedBookViewModel.FromDate = borrow.FromDate;
                borrowedBookViewModel.ToDate = borrow.ToDate;
                userWithBorrowsViewModel.BookName = borrow.Title;
                userWithBorrowsViewModel.FirstName = borrow.FirstName;
                userWithBorrowsViewModel.LastName = borrow.LastName;
                userWithBorrowsViewModel.UserId = borrow.UserId;
                borrowsViewModel.BorrowedBooks.Add(borrowedBookViewModel);
                borrowsViewModel.UserWithBorrows.Add(userWithBorrowsViewModel);
            }
            return borrowsViewModel;
        }
        public IList<UsersAddBorrowViewModel> GetAllUsers()
        {
            var allUsers = _context.User
                                   .Select(m => new UsersAddBorrowViewModel
                                   {
                                       Name = m.FirstName + " " + m.LastName,
                                       UserId = m.UserId
                                   }).ToList();
            return allUsers;
        }
    }
}
