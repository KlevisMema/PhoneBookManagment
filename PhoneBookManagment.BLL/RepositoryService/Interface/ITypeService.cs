using PhoneBookManagment.BLL.ResponseTypeService;
using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.BLL.RepositoryService.Interface
{
    public interface ITypeService
    {
        Response<AddTypeViewModel> AddType(AddTypeViewModel type);
        Response<List<TpyesInfoViewModel>> GetAllTypes();
        Response<TpyesInfoViewModel> GetA_Type(int id);
        Response<TpyesInfoViewModel> UpdateType(UpdateTypeViewModel type);
        Response<TpyesInfoViewModel> DeleteType(int id);
    }
}