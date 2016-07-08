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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, AddBookViewModel addbookViewModel)
        {
            if (addbookViewModel != null && ModelState.IsValid)
            {
                _bookService.AddNewBook(addbookViewModel);
            }

            return Json(new[] { addbookViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, BookViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                productService.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, ProductViewModel product)
        {
            if (product != null)
            {
                productService.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

    }
}