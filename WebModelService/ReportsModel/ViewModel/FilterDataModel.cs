using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.ReportsModel.ViewModel
{
    public class FilterDataModel
    {
        public int? GenreId { get; set; }
        public string Title { get; set; }
        public DateTime? BorrowFrom { get; set; }
        public DateTime? BorrowTo { get; set; }
        //public string BorrowFrom { get; set; }
        //public string BorrowTo { get; set; }
    }
}
