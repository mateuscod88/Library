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

        public ActionResult AddBook()
        {
            
            return PartialView("AddBook");
        }
        [HttpPost]
        public ActionResult AddBook(AddBookViewModel addBookViewModel)
        {
            if (ModelState.IsValid )
            {
                AddBookViewModel newAddBookVM = new AddBookViewModel();
                
                _bookService.AddNewBook(addBookViewModel);
                return Json(new { isDone = true });
            }
            else
            {
                return PartialView("AddBook", addBookViewModel);
            }
        }
        public ActionResult EditBook(int id)
        {
            EditBookViewModel selectedEditView = _bookService.GetEditBookById(id);
            return PartialView("EditBook",selectedEditView);
        }
        //public ActionResult EditBook()
        //{
        //    return View("404");
        //}
        [HttpPost]
        public ActionResult EditBook(EditBookViewModel editBookViewModel)
        {

            if (ModelState.IsValid)
            {
                _bookService.SaveEditedBook(editBookViewModel);
                return Json(new { isDone = true });
            }
            else
            {
                return PartialView("EditBook", editBookViewModel);
            }

        }
        [HttpGet]
        public ActionResult BookDetails(int id)
        {
            
            return PartialView(_bookService.GetBookDetailsById(id));
        }
        public JsonResult GetBookGenre()
        {
            IList<DictBookGenreModel> bookGenreModel = _bookService.GetBookGenre();
            return Json(bookGenreModel, JsonRequestBehavior.AllowGet);
        }

    }
        

    }
