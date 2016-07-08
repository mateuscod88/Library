using EF;
using System;
using System.Collections.Generic;
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
                var books = (from book in context.Book
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
            using (var context = new BookLibraryEF())
            {
                Book book = new Book
                {
                    Author = bookViewModel.Author,
                    Title = bookViewModel.Title,
                    AddDate = System.DateTime.Now,
                    Count = bookViewModel.Count,
                    ModifiedDate = System.DateTime.Now,
                    ISBN = bookViewModel.ISBN,
                    ReleaseDate = bookViewModel.ReleaseDate,
                };
                context.Book.Add(book); 
            }
        }

        public BookViewModel GetBookById(int bookId)
        {
            using (var context = new BookLibraryEF())
            {
                var selectedBook = context.Book.SingleOrDefault(m => (m.BookId == bookId));
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
                var selectedBook = context.Book.SingleOrDefault(m => (m.BookId == bookViewModel.BookId));
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
