using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModelServices.ReportsModel;
using WebModelServices.ReportsModel.NewFolder1;
using WebModelServices.ReportsModel.ViewModel;

namespace LibraryMVC.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        public PartialViewResult UserWithFilter()
        {
            
            return PartialView("UsersWithFilter");
        }
        [HttpPost]
        public JsonResult FilterUserss([DataSourceRequest]DataSourceRequest request)
        {
            var users = _reportService.GetUserByFilterCriteria();
            return Json(users.ToDataSourceResult(request));
        }
       
        [HttpGet]
        public PartialViewResult BookWithFilter()
        {
            
            return PartialView("BookWithFilter");
        }
       
        [HttpGet]
        public JsonResult GetBooks()
        {
            var books = _reportService.GetBooks();
            return Json(books, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDictGenre()
        {
           
            return Json(_reportService.GetDictGenre(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTitle(BookTitleViewModel bookTitleModel)
       {
            
            return Json(_reportService.GetTitle(bookTitleModel.Title), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SortBooks([DataSourceRequest]DataSourceRequest request, string title , int? genreId,string borrowFrom , string borrowTo)
        {
            IQueryable<BookWithFilterViewModel> sortedBooks;
            

            if (genreId.HasValue && genreId>-1 || !string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(borrowFrom)|| !string.IsNullOrEmpty(borrowTo))
            {
               
                sortedBooks = _reportService.GetBooksByFilterCriteria(title,genreId,borrowFrom,borrowTo);

            }
            else
            {
                sortedBooks = _reportService.SortBooks();
            }
            
            var s = sortedBooks.ToDataSourceResult(request);
            return Json(s,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FilterBooks()
        {


            return Json(_reportService.GetBooks(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ResetFilterBooks()
        {
            return Json(_reportService.GetBooks(), JsonRequestBehavior.AllowGet);

        }
    }
}