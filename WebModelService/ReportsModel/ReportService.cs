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
        public ReportService(BookLibraryEF context)
        {
            _context = context;
        }
        public IList<UserWithFilterViewModel> GetUserByFilterCriteria()
        {
            using (_context)
            {
                var users = (from user in _context.User
                             select new UserWithFilterViewModel
                             {
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 UserId = user.UserId,
                                 BorrowedBook = user.Borrow.Count
                                
                             }).OrderByDescending(x=>x.BorrowedBook).ToList();
                return users;

            }
        }
        public IList<BookWithFilterViewModel> GetBooks()
        {
            var books = (from book in _context.Books
                         select new BookWithFilterViewModel
                         {
                             BookId = book.BookId,
                             Author = book.Author,
                             Title = book.Title,
                             BorrowCount = book.Borrow.Count,
                             AddDate = book.AddDate
                             
                             }).OrderByDescending(x=>x.BorrowCount).ToList();
            return books;
        }
        

        public IQueryable<BookWithFilterViewModel> SortBooks()
        {
            var books = (from book in _context.Books
                         select new BookWithFilterViewModel
                         {
                             BookId = book.BookId,
                             Author = book.Author,
                             Title = book.Title,
                             BorrowCount = book.Borrow.Count,
                             AddDate = book.AddDate

                         }).OrderByDescending(x=>x.BorrowCount);
            return books;
        }
        public IList<DictGenreViewModel> GetDictGenre()
        {
            var dictGenres = (from dictGenre in _context.DictBookGenre
                              select new DictGenreViewModel
                              {
                                  Name = dictGenre.Name,
                                  BookGenreId = dictGenre.BookGenreId
                              }).ToList();
            return dictGenres;
        }
        public IList<BookTitleViewModel> GetTitle(string title)
        {

            var bookTitle = (from book in _context.Books
                             where (book.Title).Contains(title)
                             select new BookTitleViewModel
                             {
                                 BookId = book.BookId,
                                 Title = book.Title
                             }).ToList();
            return bookTitle;
        }
        public IQueryable<BookWithFilterViewModel> GetBooksByFilterCriteria(string title, int? genreId, string borrowFrom, string borrowTo)
        {

            var tempBook = (from book in _context.Books select book);

            if (!string.IsNullOrEmpty(title))
            {
                tempBook = (from book in tempBook where book.Title.Contains(title) select book);
            }
            if((genreId).HasValue)
            {
                tempBook = (from book in tempBook where genreId == book.DictBookGenre.BookGenreId select book);
            }
            if(!string.IsNullOrEmpty(borrowFrom))
            {
                var BorrowFrom = DateTime.Parse(borrowFrom);
                tempBook = (from book in tempBook where BorrowFrom < book.AddDate select book);
            }
            if(!string.IsNullOrEmpty(borrowTo))
            {
                var BorrowTo = DateTime.Parse(borrowTo);
                tempBook = (from book in tempBook where BorrowTo > book.AddDate select book);
            }

            var filteredBook = (from book in tempBook
                                orderby book.Borrow.Count descending
                                select new BookWithFilterViewModel
                                {
                                    BookId = book.BookId,
                                    Author = book.Author,
                                    Title = book.Title,
                                    BorrowCount = book.Borrow.Count,
                                    AddDate = book.AddDate
                                });
            return filteredBook;
        }
    }
}
