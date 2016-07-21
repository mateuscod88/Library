using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.ReportsModel.ViewModel;

namespace WebModelServices.ReportsModel.NewFolder1
{
    public class ReportService : IReportService
    {
        private BookLibraryEF _context;
        public ReportService()
        {
            _context = new BookLibraryEF();
        }
        public IList<UserWithFilterViewModel> GetUserByFilterCriteria()
        {
            using (_context)
            {
                var users = (from user in _context.User
                             orderby user.Borrow.Count descending
                             select new UserWithFilterViewModel
                             {
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 UserId = user.UserId,
                                 BorrowedBook = user.Borrow.Count
                                
                             }).ToList();
                return users;

            }
        }
        public IList<BookWithFilterViewModel> GetBooksByFilterCriteria()
        {
            var books = (from book in _context.Books
                         orderby book.Borrow.Count descending
                         select new BookWithFilterViewModel
                         {
                             Author = book.Author,
                             Title = book.Title,
                             BorrowCount = book.Borrow.Count,
                             AddDate = book.AddDate,
                             ReleaseDate = book.ReleaseDate
                         }).ToList();
            return books;
        }
        public IList<DictGenreModel> GetDictGenre()
        {
            var dictGenres = (from dictGenre in _context.DictBookGenre
                              select new DictGenreModel
                              {
                                  Name = dictGenre.Name,
                                  BookGenreId = dictGenre.BookGenreId
                              }).ToList();
            return dictGenres;
        }
        public IList<BookTitleModel> GetTitle(string title)
        {

            var bookTitle = (from book in _context.Books
                             where (book.Title).StartsWith(title)
                             select new BookTitleModel
                             {
                                 BookId = book.BookId,
                                 Title = book.Title
                             }).ToList();
            return bookTitle;
        }
    }
}
