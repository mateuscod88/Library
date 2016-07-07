using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.UserModel.Contracts.ViewModel
{
    public class BorrowHistoryViewModel
    {
        public int BorrowId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }
        public string BookTitle{ get; set; }
        public string BookAuthor { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsReturned { get; set; }
        
    }
}
