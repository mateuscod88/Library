using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using System.Data.Entity.Validation;
using WebModelServices.UserModel.contracts.Interface;
using WebModelServices.UserModel.Contracts.ViewModel;

namespace WebModelServices.UserModel.contracts.DTO
{
    public class UserService : IUserService
    {
        public IList<UserViewModel> RetrieveAll()
        {
            //List<UserViewModel> usersDTO = new List<UserViewModel>();
            using (var context = new BookLibraryEF())
            {
                var users = from user in context.User
                            join borrow in context.Borrow on user.UserId equals borrow.UserId
                            into borrowCount
                            from borrow in borrowCount.DefaultIfEmpty().GroupBy(m => user.UserId)

                            select new UserViewModel
                            {
                                UserId = user.UserId,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                BirthDate = user.BirthDate,
                                Email = user.Email,
                                AddDate = user.AddDate,
                                Modified = user.ModifiedDate,
                                isActive = user.IsActive,
                                Phone = user.Phone,
                                BooksBorrowed = borrowCount.Count(m => m.IsReturned == false)
                            };
                 return  users.ToList();
            }

           

           // return usersDTO;
        }
        public void AddUserViewModel(UserViewModel user)
        {

            User newUser = new User();
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.BirthDate = Convert.ToDateTime(user.BirthDate);
            newUser.AddDate = System.DateTime.Now;
            newUser.IsActive = true;
            newUser.Phone = user.Phone;
            newUser.ModifiedDate = System.DateTime.Now;
            using (var context = new BookLibraryEF())
            {
                context.User.Add(newUser);
                context.SaveChanges();
            }

        }
        public UserViewModel GetUserById(int userId)
        {
            UserViewModel userViewModel = new UserViewModel();
            using (var context = new BookLibraryEF())
            {
                var selectedUser = context.User.SingleOrDefault(m => m.UserId == userId);
                userViewModel.UserId = selectedUser.UserId;
                userViewModel.FirstName = selectedUser.FirstName;
                userViewModel.LastName = selectedUser.LastName;
                userViewModel.Email = selectedUser.Email;
                userViewModel.Phone = selectedUser.Phone;
                //user.Modified = selectedUser.ModifiedDate.ToString("u");
                userViewModel.BirthDate = selectedUser.BirthDate;
            }
            return userViewModel;
        }
        public void SaveUserViewModel(UserViewModel userViewModel)
        {
           if (userViewModel != null)
           { 
            using (var context = new BookLibraryEF())
            {
                var selectedUser = context.User.SingleOrDefault(m => m.UserId == userViewModel.UserId);
                selectedUser.FirstName = userViewModel.FirstName;
                selectedUser.LastName = userViewModel.LastName;
                selectedUser.Email = userViewModel.Email;
                selectedUser.Phone = userViewModel.Phone;
                selectedUser.ModifiedDate = System.DateTime.Now;
                selectedUser.BirthDate = Convert.ToDateTime(userViewModel.BirthDate);
                context.SaveChanges();
            }
           }
        }
        public void DeleteUserById(int userId)
        {
            using (var context = new BookLibraryEF())
            {
                var selectedUser = context.User.SingleOrDefault(m => m.UserId == userId);
                selectedUser.IsActive = false;
                context.SaveChanges();
            }
        }
        public DetailsViewModel  GetDetailsByUserId(int userId)
        {
            using (var context = new BookLibraryEF())
            {
                var userDetails = context.User.SingleOrDefault(m => (m.UserId == userId));
                var borrowHistory = userDetails.Borrows.ToList();
                IList<Book> books = new List<Book>();
                foreach(var borrow in borrowHistory)
                {
                    var book = context.Book.SingleOrDefault(m => (m.BookId == borrow.BookId));
                    books.Add(book);
                }
                DetailsViewModel details = new DetailsViewModel();
                IList<BorrowHistoryViewModel> borrows = new List<BorrowHistoryViewModel>();

                UserViewModel userViewModel = new UserViewModel();

                userViewModel.FirstName = userDetails.FirstName;
                userViewModel.LastName= userDetails.LastName;
                userViewModel.Email = userDetails.Email;
                userViewModel.FirstName = userDetails.FirstName;
                userViewModel.Phone = userDetails.Phone;
                userViewModel.BirthDate = userDetails.BirthDate;
                details.User = userViewModel;

                foreach(var borrow in borrowHistory)
                {
                    BorrowHistoryViewModel borrowHistoryViewModel = new BorrowHistoryViewModel();
                    borrowHistoryViewModel.FromDate = borrow.FromDate;
                    borrowHistoryViewModel.ToDate = borrow.ToDate;
                    borrowHistoryViewModel.BookId = borrow.BookId;
                    borrowHistoryViewModel.BookAuthor = borrow.Book.Author;
                    borrowHistoryViewModel.BookTitle = borrow.Book.Title;
                    borrowHistoryViewModel.IsReturned = borrow.IsReturned;
                    details.Borrows.Add(borrowHistoryViewModel);
                }
                foreach (var book in books)
                {
                    UserBookViewModel userBookViewModel = new UserBookViewModel();
                    userBookViewModel.Author = book.Author;
                    userBookViewModel.Title = book.Title;
                    userBookViewModel.BookId = book.BookId;
                    userBookViewModel.IsReturned = details.Borrows.FirstOrDefault(m => (m.BookId == book.BookId)).IsReturned;
                    details.Book.Add(userBookViewModel);
                }
                return details;

            }
        }
    }
}
