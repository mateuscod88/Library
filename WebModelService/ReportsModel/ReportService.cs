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
        public IList<BookWithFilterViewModel> GetBooks()
        {
            var books = (from book in _context.Books
                         orderby book.Borrow.Count descending
                         select new BookWithFilterViewModel
                         {
                             BookId = book.BookId,
                             Author = book.Author,
                             Title = book.Title,
                             BorrowCount = book.Borrow.Count,
                             AddDate = book.AddDate
                             
                             }).ToList();
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
        public IList<BookWithFilterViewModel> GetBooksByFilterCriteria(FilterDataModel filterDataModel)
        {

            var tempBook = (from book in _context.Books select book);

            if (!string.IsNullOrEmpty(filterDataModel.Title))
            {
                tempBook = (from book in tempBook where filterDataModel.Title == book.Title select book);
            }
            if((filterDataModel.GenreId).HasValue)
            {
                tempBook = (from book in tempBook where filterDataModel.GenreId == book.DictBookGenre.BookGenreId select book);
            }
            if(filterDataModel.BorrowFrom.HasValue)
            {
                tempBook = (from book in tempBook where filterDataModel.BorrowFrom < book.AddDate select book);
            }
            if(filterDataModel.BorrowTo.HasValue)
            {
                tempBook = (from book in tempBook where filterDataModel.BorrowTo > book.AddDate select book);
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
                                }).ToList();
            return filteredBook;
        }
    }
}
