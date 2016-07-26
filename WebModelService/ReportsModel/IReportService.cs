using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.ReportsModel.ViewModel;

namespace WebModelServices.ReportsModel
{
    public interface IReportService
    {
        IList<UserWithFilterViewModel> GetUserByFilterCriteria();
        IList<BookWithFilterViewModel> GetBooks();
        IList<BookTitleViewModel> GetTitle(string title);
        IList<DictGenreViewModel> GetDictGenre();
        IQueryable<BookWithFilterViewModel> GetBooksByFilterCriteria(string title, int? genreId, string borrowFrom, string borrowTo);
        IQueryable<BookWithFilterViewModel> SortBooks();
    }
}
