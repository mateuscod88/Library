using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.BorrowModel.ViewModel;
using WebModelServices.UserModel.contracts.DTO;

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
                                  join borrow in _context.Borrows on user.UserId equals borrow.UserId
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
                                      ToDate = borrow.ToDate,
                                      BorrowId = borrow.BorrowId

                                  };
            foreach (var borrow in selectedBorrows)
            {

                BorrowedBookViewModel borrowedBookViewModel = new BorrowedBookViewModel();
                borrowedBookViewModel.BookId = borrow.BookId;
                borrowedBookViewModel.Author = borrow.Author;
                borrowedBookViewModel.Title = borrow.Title;
                borrowedBookViewModel.Count = borrow.Count;
                borrowedBookViewModel.ISBN = borrow.ISBN;
                borrowedBookViewModel.UserName = borrow.FirstName + " " + borrow.LastName;
                borrowedBookViewModel.FromDate = borrow.FromDate;
                borrowedBookViewModel.ToDate = borrow.ToDate;
                borrowedBookViewModel.BorrowId = borrow.BorrowId;
                borrowsViewModel.BorrowedBooks.Add(borrowedBookViewModel);
            }
            return borrowsViewModel;
        }
        public IList<UserWithBorrowsViewModel> GetUsersWithBorrows()
        {
            List<UserWithBorrowsViewModel> usersViewModel = new List<UserWithBorrowsViewModel>();
            var usersWithBorrows = (from user in _context.User
                                    join borrow in _context.Borrows on user.UserId equals borrow.UserId
                                    where borrow.IsReturned == false
                                    group user by user.UserId into usery
                                    select new UserWithBorrowsViewModel
                                    {
                                        FirstName = usery.FirstOrDefault().FirstName,
                                        LastName = usery.FirstOrDefault().LastName,
                                        UserId = usery.FirstOrDefault().UserId
                                    }).ToList();
            
            return usersWithBorrows;
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
                                   .Where(m=> m.Count > m.Borrow.Count(c => c.IsReturned == false))
                                   .Select(m => new BooksAddBorrowViewModel
                                   {
                                       BookId = m.BookId,
                                       Title = m.Title
                                   }).ToList();
            return allBooks;
        }
    
        public void SaveAllBorrowsToUser(BorrowsToSaveModel borrowsToSaveModel)
        {
            using (_context)
            {
                foreach (var borrow in borrowsToSaveModel.Borrows)
                {
                    var todayTime = System.DateTime.Now;

                    _context.Borrows.Add(new Borrow
                    {
                        BookId = borrow.BookId,
                        UserId = borrowsToSaveModel.User.UserId,
                        FromDate = todayTime,
                        ToDate = todayTime.AddMonths(1),
                        IsReturned = false

                    });
                   
                }
                _context.SaveChanges();
            }
        }
        public void ReturnBook(int borrowId)
        {
            using (_context)
            {
                var borrow = _context.Borrows.Single(x => x.BorrowId == borrowId);
                borrow.IsReturned = true;
                _context.SaveChanges();
            }
        }
        public void ReturnBookFromUser(ReturnBookFromUserViewModel returnedBooksFromUser)
        {
            using (_context)
            {
                var userBorrows = (from borrow in _context.Borrows
                                   where borrow.UserId == returnedBooksFromUser.UserWithBorrowsViewModel.UserId
                                   where borrow.IsReturned == false
                                   select borrow).ToList();
                
                foreach (var books in returnedBooksFromUser.BorrowedBooksViewModel)
                {
                    if(books.iisActive == true)
                    {
                        userBorrows.FirstOrDefault(m => m.BorrowId == books.BorrowId).IsReturned = true;
                    }
                }
                _context.SaveChanges();
            }
        }
        public ReturnBookFromUserViewModel GetBorrowedBooksFromUser(int userId)
        {
            ReturnBookFromUserViewModel returnBookFromUserViewModel = new ReturnBookFromUserViewModel();

            var selectedBorrows = from user in _context.User
                                  join borrow in _context.Borrows on user.UserId equals borrow.UserId
                                  where borrow.IsReturned == false && borrow.UserId == userId
                                  join book in _context.Books on borrow.BookId equals book.BookId
                                  select new 
                                  {
                                      UserId = user.UserId,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      BorroweId = borrow.BorrowId,
                                      Author = book.Author,
                                      Title = book.Title,
                                      BookId = book.BookId

                                  };
            foreach (var borrow in selectedBorrows)
            {
                BorrowedBookViewModel borrowedBookViewModel = new BorrowedBookViewModel();
                borrowedBookViewModel.BorrowId = borrow.BorroweId;
                borrowedBookViewModel.BookId = borrow.BookId;
                borrowedBookViewModel.Author = borrow.Author;
                borrowedBookViewModel.Title = borrow.Title;
                returnBookFromUserViewModel.BorrowedBooksViewModel.Add(borrowedBookViewModel);
            }
            var currentUser = selectedBorrows.FirstOrDefault();
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.FirstName = currentUser.FirstName;
            userViewModel.LastName = currentUser.LastName;
            userViewModel.UserId = currentUser.UserId;
            returnBookFromUserViewModel.UserWithBorrowsViewModel = userViewModel;

            return returnBookFromUserViewModel;
        }

    }  
}
