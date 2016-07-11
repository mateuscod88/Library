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
        public IList<BookViewModel> RetriveAll()
        {
            using (var context = new BookLibraryEF())
            {
                var books = (from book in context.Books
                            select new BookViewModel
                            {
                                BookId = book.BookId,
                                Author = book.Author,
                                ISBN = book.ISBN,
                                ModifiedDate = book.ModifiedDate,
                                ReleaseDate = book.ReleaseDate,
                                AddDate = book.AddDate,
                                Count = book.Count,
                                Title = book.Title
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
                book.BookGenreId = 2;

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
        //BookDetailsViewModel GetBookDetails(int bookId)
        //{
        //    using (var context)
        //    {

        //    }
        //}
    }
}
