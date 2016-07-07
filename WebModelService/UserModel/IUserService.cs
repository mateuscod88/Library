using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModelServices.UserModel.contracts.DTO;
using WebModelServices.UserModel.Contracts.ViewModel;

namespace WebModelServices.UserModel.contracts.Interface
{
    public interface IUserService
    {
        IList<UserViewModel> RetrieveAll();
        void AddUserViewModel(UserViewModel user);
        UserViewModel GetUserById(int userId);
        void SaveUserViewModel(UserViewModel user);

        void DeleteUserById(int userId);

        DetailsViewModel GetDetailsByUserId(int userId);
    }
}
