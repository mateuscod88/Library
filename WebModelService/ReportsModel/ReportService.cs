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
                             select new UserWithFilterViewModel
                             {
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 UserId = user.UserId
                             }).ToList();
                return users;

            }
        }
    }
}
