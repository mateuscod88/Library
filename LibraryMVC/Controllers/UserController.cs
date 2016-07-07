using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryMVC.Models;
using WebModelServices.UserModel.contracts.DTO;
using WebModelServices.UserModel.Contracts.ViewModel;
using WebModelServices.UserModel.contracts.Interface;

namespace LibraryMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        public ActionResult Index()
        {
            UserListViewModel userListViewModel = new UserListViewModel(); 
            IList<UserViewModel> usersViewModel = _userService.RetrieveAll();
            userListViewModel.User = usersViewModel;
            return View("Index",userListViewModel);
        }
        
        [HttpPost]
        public ActionResult Add(UserViewModel userViewModel)
        {
            
                if (!ModelState.IsValid && userViewModel != null)
                {
                    return View(userViewModel);    
                }
                else
                {
                    _userService.AddUserViewModel(userViewModel);
                    return RedirectToAction("Index", "User");
                }
            
        }
        [HttpGet]
        public ActionResult Add()
        {
            
            return View("Add");
        }
        [HttpGet]
        public ActionResult Edit(int UserId)
        {
            
            var user = _userService.GetUserById(UserId);
            return View("Edit",user);
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            var name = user.FirstName;
            _userService.SaveUserViewModel(user);

            return RedirectToAction("Index");
        }
        public ActionResult Details(int UserId)
        {
            DetailsViewModel userDetails = _userService.GetDetailsByUserId(UserId);
            return View("Details",userDetails);
        }

        [HttpGet]
        public ActionResult Delete(int UserId)
        {
            _userService.DeleteUserById(UserId);
            return RedirectToAction("Index","User");
        }
    }
}