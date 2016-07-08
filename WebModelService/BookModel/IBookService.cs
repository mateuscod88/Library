using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.BookModel.ViewModel;

namespace WebModelServices.BookModel
{
    public interface IBookService
    {
        IList<BookViewModel> RetriveAll();
        void AddNewBook(AddBookViewModel bookViewModel);
        BookViewModel GetBookById(int bookId);
        void SaveBookViewModel(BookViewModel bookViewModel);

    }
}
