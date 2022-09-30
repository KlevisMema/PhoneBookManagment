using PhoneBookManagment.BLL.ResponseTypeService;
using PhoneBookManagment.DAL.Models;
using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.BLL.RepositoryService.Interface
{
    public interface IUserService
    {
        Response<List<UsersInfoViewModel>> GetAllUsers();
        Response<AddUserViewModel> AddUser(AddUserViewModel user);
        Response<UsersInfoViewModel> GetUser(int id);
        Response<UpdateUserViewModel> EditUser(UpdateUserViewModel user);
        Response<UsersInfoViewModel> DeleteUser(int id);
    }
}