using PhoneBookManagment.BLL.ResponseTypeService;
using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.BLL.RepositoryService.Interface
{
    public interface IPhoneBookRepositoryService
    {
        Response<List<GetAllUsersViewModel>> GetAll();
        Response<GetAllUsersViewModel> GetUser(int id);
        Response<IEnumerable<GetAllUsersViewModel>> GetAllOrderedBy(bool orderByFirstName);
    }
}