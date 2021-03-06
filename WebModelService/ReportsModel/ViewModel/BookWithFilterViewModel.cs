﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModelServices.ReportsModel.ViewModel
{
    public class BookWithFilterViewModel
    {
        public int BookId { get; set; }

        public int BookGenreId { get; set; }
        public string BookGenre { get; set; }
        public string Author { get; set; }

        public string Title { get; set; }

        public DateTime AddDate { get; set; }
        public string AddDateDisplay
        {
            get
            {
                return this.AddDate.ToShortDateString();
            }
        }

        public int BorrowCount { get; set; }
    }
}
