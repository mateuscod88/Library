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
    }
}