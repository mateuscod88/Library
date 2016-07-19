using EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.BookModel.ViewModel;

namespace WebModelServices.BookModel
{
    public class BookService : IBookService
    {
        private BookLibraryEF _context;
        public BookService()
        {
            _context = new BookLibraryEF();
        }
        public IList<BookViewModel> RetriveAll()
        {
            using (_context)
            {
                var books = _context.Books.Include("DictBookGenre").Select(book => new BookViewModel
                {
                    BookId = book.BookId,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    ModifiedDate = book.ModifiedDate,
                    ReleaseDate = book.ReleaseDate,
                    AddDate = book.AddDate,
                    Count = book.Count,
                    Title = book.Title,
                    BookGenreId = book.BookGenreId,
                    BookGenre = book.DictBookGenre.Name
                }
                            ).ToList(); 
                return books;
            }
        }
        public void AddNewBook(AddBookViewModel bookViewModel)
        {
            Book book = new Book();

                book.Author = bookViewModel.Author;
                book.Title = bookViewModel.Title;
                book.AddDate = System.DateTime.Now;
                book.Count = bookViewModel.Count;
                book.ModifiedDate = System.DateTime.Now;
                book.ISBN = bookViewModel.ISBN;
                book.ReleaseDate = bookViewModel.ReleaseDate;
                book.BookGenreId = bookViewModel.BookGenreId;

            using (var context = new BookLibraryEF())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public BookViewModel GetBookById(int bookId)
        {
            using (var context = new BookLibraryEF())
            {
                var selectedBook = context.Books.SingleOrDefault(m => (m.BookId == bookId));
                BookViewModel bookViewModel = new BookViewModel
                {
                    Author = selectedBook.Author,
                    Title = selectedBook.Title,
                    BookId = selectedBook.BookId,
                    AddDate = selectedBook.AddDate,
                    Count = selectedBook.Count,
                    ReleaseDate = selectedBook.ReleaseDate,
                    ISBN = selectedBook.ISBN,
                    ModifiedDate = selectedBook.ModifiedDate
                };
                return bookViewModel;
            }
        }
        public void SaveBookViewModel(BookViewModel bookViewModel)
        {
            using (var context = new BookLibraryEF())
            {
                var selectedBook = context.Books.SingleOrDefault(m => (m.BookId == bookViewModel.BookId));
                selectedBook.Author = bookViewModel.Author;
                selectedBook.Count = bookViewModel.Count;
                selectedBook.Title = bookViewModel.Title;
                selectedBook.ReleaseDate = bookViewModel.ReleaseDate;
                selectedBook.ISBN = bookViewModel.ISBN;
                selectedBook.ModifiedDate = System.DateTime.Now;
                context.SaveChanges();
            }
        }
        public EditBookViewModel GetEditBookById(int bookId)
        {
            using (_context)
            {
                var selectedBook = _context.Books
                .Select(m => new EditBookViewModel
                {
                    BookId = m.BookId,
                    Author = m.Author,
                    Count = m.Count,
                    ISBN = m.ISBN,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    BookGenreIdd = m.BookGenreId,
                })
                .Where(m => m.BookId == bookId)
                .FirstOrDefault();
                return selectedBook;
            }
                
        }
        public void SaveEditedBook(EditBookViewModel editBookViewModel)
        {
            using (_context)
            {
                var selectedBook = _context.Books.SingleOrDefault(m => (m.BookId == editBookViewModel.BookId));
                selectedBook.Author = editBookViewModel.Author;
                selectedBook.Count = editBookViewModel.Count;
                selectedBook.Title = editBookViewModel.Title; 
                selectedBook.ReleaseDate = editBookViewModel.ReleaseDate;
                selectedBook.ISBN = editBookViewModel.ISBN;
                selectedBook.ModifiedDate = System.DateTime.Now;
                selectedBook.BookGenreId = editBookViewModel.BookGenreIdd;
                _context.SaveChanges();

            }
        }
        public BookDetailsViewModel GetBookDetailsById(int bookId)
        {
            using (_context)
            {
                var selectedBook = _context.Books.Include("DictBookGenre").Where(m =>m.BookId == bookId)
                    .Select(m => new BookViewModel
                    {
                        Author = m.Author,
                        Title = m.Title,
                        Count = m.Count,
                        BookGenre = m.DictBookGenre.Name,
                        BookGenreId = m.BookGenreId,
                        ISBN = m.ISBN,
                        AddDate = m.AddDate,
                        ReleaseDate =   m.ReleaseDate,
                        BookId = m.BookId
                    }).SingleOrDefault();
                var borrowsHistory = (_context.Borrows.Where(m => m.BookId == bookId)
                    .Select(borrow => new BookBorrowHistoryViewModel
                    {
                        FromDate = borrow.FromDate,
                        ToDate = borrow.ToDate,
                        IsReturned = borrow.IsReturned
                    })).ToList();

                selectedBook.IsActive = _context.Borrows.Any(m => m.IsReturned && m.BookId == bookId);
                
                BookDetailsViewModel bookDetailsViewModel = new BookDetailsViewModel();
                bookDetailsViewModel.BookDetails = selectedBook;
                bookDetailsViewModel.BorrowHistory = borrowsHistory;

                return bookDetailsViewModel;
            }
        }
        public IList<DictBookGenreModel> GetBookGenre()
        {
            var allBookGenre = (from dict in _context.DictBookGenre
                                select new DictBookGenreModel
                                {
                                    BookGenreId = dict.BookGenreId,
                                    Name = dict.Name
                                }).ToList();
            return allBookGenre;
        }
    }
}
