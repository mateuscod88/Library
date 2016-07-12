﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.BookModel.ViewModel
{
    public class BookDetailsViewModel
    {
        public BookViewModel BookDetails { get; set; }
        public IList<BookBorrowHistoryViewModel> BorrowHistory { get; set; }
    }
}
