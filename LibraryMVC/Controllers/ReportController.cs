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
        public JsonResult FilterLastName()
        {
            var LastName = "Zdzisiek";
            return Json(LastName, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult BookWithFilter()
        {

            return PartialView("BookWithFilter",_reportService.GetBooks());
        }
        [HttpPost]
        public JsonResult GetBooks([DataSourceRequest]DataSourceRequest request)
        {
            var books = _reportService.GetBooks();
            return Json(books.ToDataSourceResult(request));
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
        public JsonResult GetTitle(BookTitleModel bookTitleModel)
       {
            
            return Json(_reportService.GetTitle(bookTitleModel.Title), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FilterBooks(FilterDataModel filterDataModel)
        {
            
            return Json(_reportService.GetBooksByFilterCriteria(filterDataModel), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ResetFilterBooks()
        {
            return Json(_reportService.GetBooks(), JsonRequestBehavior.AllowGet);

        }
    }
}