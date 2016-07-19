using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using System.Data.Entity.Validation;
using WebModelServices.UserModel.contracts.Interface;
using WebModelServices.UserModel.Contracts.ViewModel;
using System.Data.Entity;

namespace WebModelServices.UserModel.contracts.DTO
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public IList<UserViewModel> RetrieveAll()
        {
            //List<UserViewModel> usersDTO = new List<UserViewModel>();
            using (var context = new BookLibraryEF())
            {
                var users = from user in context.User
                                //from borrowShort in context.Borrow.Where(x => x.UserId == user.UserId).DefaultIfEmpty()
                            join borrow in context.Borrows on user.UserId equals borrow.UserId
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
                int id = newUser.UserId;
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
                var userDetails = context.User
                    .Where(m => m.UserId == userId)
                    .Select(x => new
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Phone = x.Phone,
                        BirthDate = x.BirthDate,
                    }).FirstOrDefault();

                var userBorrows = context.Borrows
                    .Where(m => m.UserId == userId)
                    .Select(m => new
                    {
                        Book_Id = m.BookId,
                        Borrow_Id = m.BorrowId,
                        FromDate = m.FromDate,
                        ToDate = m.ToDate,
                        Author = m.Book.Author,
                        Title = m.Book.Title,
                        IsReturned = m.IsReturned,
                        UserId = m.UserId,
                    }).ToList();

                DetailsViewModel detailsViewModel = new DetailsViewModel();
                IList<BorrowHistoryViewModel> borrows = new List<BorrowHistoryViewModel>();
                IList<UserBookViewModel> books = new List<UserBookViewModel>();

                foreach (var borrow in userBorrows)
                {
                    BorrowHistoryViewModel borrowHistoryViewModel = new BorrowHistoryViewModel();
                    UserBookViewModel userBookViewModel = new UserBookViewModel();

                    borrowHistoryViewModel.BookAuthor = borrow.Author;
                    borrowHistoryViewModel.BookTitle = borrow.Title;
                    borrowHistoryViewModel.FromDate = borrow.FromDate;
                    borrowHistoryViewModel.IsReturned = borrow.IsReturned;
                    borrowHistoryViewModel.ToDate = borrow.ToDate;
                    borrowHistoryViewModel.UserId = borrow.UserId;

                    userBookViewModel.Author = borrow.Author;
                    userBookViewModel.Title = borrow.Title;
                    userBookViewModel.BookId = borrow.Book_Id;
                    userBookViewModel.IsReturned = borrow.IsReturned;

                    borrows.Add(borrowHistoryViewModel);
                    books.Add(userBookViewModel);
                }
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.FirstName = userDetails.FirstName;
                userViewModel.LastName= userDetails.LastName;
                userViewModel.Email = userDetails.Email;
                userViewModel.Phone = userDetails.Phone;
                userViewModel.BirthDate = userDetails.BirthDate;
                
                detailsViewModel.User = userViewModel;
                detailsViewModel.Borrows = borrows;
                detailsViewModel.Book = books;

                return detailsViewModel;
            }
        }
    }
}
