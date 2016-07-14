using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebModelServices.BookModel;
using WebModelServices.BorrowModel;
using WebModelServices.BorrowModel.ViewModel;

namespace LibraryMVC.Controllers
{
    public class BorrowController : Controller
    {
        // GET: Borrow
        private BorrowService _borrowService;
        public BorrowController()
        {
            _borrowService = new BorrowService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUsersWithBorrows([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_borrowService.GetBorrowListViewModel().UserWithBorrows.ToDataSourceResult(request));
        }
        

        public ActionResult GetBorrowedBook([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_borrowService.GetBorrowListViewModel().BorrowedBooks.ToDataSourceResult(request));
        }
        public ActionResult AddNewBorrow()
        {
            return PartialView("AddNewBorrow");
        }
       // [HttpPost]
        //public ActionResult AddNewBorrow(NewBorrowViewModel newBorrowViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        NewBorrowViewModel borrowViewModel = new NewBorrowViewModel();
        //        _borrowService.AddNewBorrow(newBorrowViewModel);
        //        return Json(new { isDone = true });
        //    }
        //    else
        //    {
        //        return PartialView("AddBook", newBorrowViewModel);
        //    }

        //}
        public JsonResult GetUsers()
        {
            IList<UsersAddBorrowViewModel> userAddBorrowViewModel = _borrowService.GetAllUsers();
            return Json(userAddBorrowViewModel , JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetrieveBorrows(int id)
        {

            int Id = id;
            return Json(_borrowService.GetAllBooks(), JsonRequestBehavior.AllowGet);
        }

       

    }
}
