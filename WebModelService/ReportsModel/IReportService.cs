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
        IList<BookWithFilterViewModel> GetBooksByFilterCriteria();
        IList<BookTitleModel> GetTitle(string title);
        IList<DictGenreModel> GetDictGenre();
    }
}
