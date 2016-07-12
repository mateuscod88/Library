using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.BookModel.ViewModel
{
    public class BookBorrowHistoryViewModel
    {

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsReturned { get; set; }
    }
}
