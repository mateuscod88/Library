using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModelServices.BookModel;
using WebModelServices.BookModel.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private IBookService _bookService;
        // GET: Book
        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }
        public ActionResult Index()
        {
            
            return View("Index");
        }
        public ActionResult AllBooks([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_bookService.RetriveAll().ToDataSourceResult(request));
        }

        

    }
}