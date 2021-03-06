﻿using Kendo.Mvc.Extensions;
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
        
        private IBorrowService _borrowService;
        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUsersWithBorrows([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_borrowService.GetUsersWithBorrows().ToDataSourceResult(request));
        }
        

        public ActionResult GetBorrowedBook([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_borrowService.GetBorrowListViewModel().BorrowedBooks.ToDataSourceResult(request));
        }
        public ActionResult AddNewBorrow()
        {
            return PartialView("AddNewBorrow");
        }
      
        public JsonResult GetUsers()
        {
            IList<UsersAddBorrowViewModel> userAddBorrowViewModel = _borrowService.GetAllUsers();
            return Json(userAddBorrowViewModel , JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetrieveBorrows()
        {
            var books = _borrowService.GetAllBooks();
            return Json( books, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddBorrowsToUser(BorrowsToSaveModel borrowsToSaveModel)
        {
            _borrowService.SaveAllBorrowsToUser(borrowsToSaveModel);
            return Json(new { isDone = true });
        }
        [HttpPost]
        public JsonResult ReturnBook(int id)
        {
            _borrowService.ReturnBook(id);
            return Json(new { isDone = true });
        }

        [HttpGet]
        public ActionResult ReturnBooksByUserViewModel(int id)
        {
            var returnBookByUserViewModel = _borrowService.GetBorrowedBooksFromUser(id);
            return View("ReturnBooksByUser",returnBookByUserViewModel);
        }
        [HttpPost]
        public ActionResult ReturnBooksByUser(ReturnBookFromUserViewModel returnBookFromUserViewModel)
        {
            _borrowService.ReturnBookFromUser(returnBookFromUserViewModel);
            return RedirectToAction("Index", "Borrow");
        }
    }
}
