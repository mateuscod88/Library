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
        private static List<int> _selectedBooks;
        public static List<int> SelectedBooks
        {
            get
            {
                if (_selectedBooks == null)
                    _selectedBooks = new List<int>();
                return _selectedBooks;
            }

            set
            {
                _selectedBooks = value;
            }
        }
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
        public IList<BooksAddBorrowViewModel> GetAllBooks()
        {
            var allBooks = _context.Books
                                   .Select(m => new BooksAddBorrowViewModel
                                   {
                                       BookId = m.BookId,
                                       Title = m.Title
                                   }).ToList();
            return allBooks;
        }
        public IList<BooksAddBorrowViewModel> GetBooksAndRemoveRedudant(int bookId)
        {
            
            var allBooks = _context.Books
                                   .Select(m => new BooksAddBorrowViewModel
                                   {
                                       BookId = m.BookId,
                                       Title = m.Title
                                   }).ToList();
            if(SelectedBooks.Any(m => m == bookId) || bookId == 1)
            {
                SelectedBooks.Clear();
            }
            if(allBooks.Any(m=> m.BookId == bookId))
            {
                SelectedBooks.Add(bookId);
            }
            foreach(var book in SelectedBooks)
            {
                var bookToDelete = allBooks.SingleOrDefault(m=> m.BookId == book);
                allBooks.Remove(bookToDelete);
            }

            return allBooks;
        }
    }
}
