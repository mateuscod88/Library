using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModelServices.UserModel.contracts.DTO;
namespace LibraryMVC.Models
{
    public class UserListViewModel
    {
        public IList<UserViewModel> User { get; set; }

    }
}