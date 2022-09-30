using Newtonsoft.Json;
using PhoneBookManagment.BLL.RepositoryService.GenericImplementation;
using PhoneBookManagment.BLL.RepositoryService.Interface;
using PhoneBookManagment.BLL.ResponseTypeService;
using PhoneBookManagment.DAL.Models;
using PhoneBookManagment.DTO.DTOs;
using PhoneBookManagment.DTO.Mappings;
using System.Collections.Generic;
using System.Reflection;
using Type = PhoneBookManagment.DAL.Models.Type;

namespace PhoneBookManagment.BLL.RepositoryService.Implementation
{
    public class PhoneBookRepositoryService : IPhoneBookRepositoryService
    {
        private readonly string DatabaseContext = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName + "\\PhoneBookManagment\\PhoneBookManagment.DAL\\ApplicationContext";

        private readonly string _User;
        private readonly string _Type;
        private readonly string _UserType;

        public PhoneBookRepositoryService()
        {
            _User = Path.Combine(DatabaseContext, "Users.json");
            _Type = Path.Combine(DatabaseContext, "Types.json");
            _UserType = Path.Combine(DatabaseContext, "UserTypes.json");
        }

        // Get all users
        public Response<List<GetAllUsersViewModel>> GetAll()
        {
            try
            {
                var users = Deserialize_Read<User>.DesirializeRead(_User)?.Where(x => !x.IsDeleted).ToList();

                if (users is null)
                    return Response<List<GetAllUsersViewModel>>.ErrorMsg("Empty file..");

                var types = Deserialize_Read<Type>.DesirializeRead(_Type)?.Where(x => !x.IsDeleted).ToList();

                var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType)?.Where(x => !x.IsDeleted).ToList();

                userTypes?.ForEach(x => x.Type = types.FirstOrDefault(y => y.Id == x.TypeId));

                users?.ForEach(x => x.UserTypes = userTypes.Where(y => y.UserId == x.Id));

                return Response<List<GetAllUsersViewModel>>.Ok(users.Select(x => x.UserTranslationObj()).ToList());
            }
            catch (Exception ex)
            {
                return Response<List<GetAllUsersViewModel>>.ExceptionThrow(ex.Message);
            }
        }

        //  Get a user by id
        public Response<GetAllUsersViewModel> GetUser(int id)
        {
            try
            {
                var userJson = Deserialize_Read<User>.DesirializeRead(_User)?.FirstOrDefault(x => !x.IsDeleted && x.Id == id);

                var userTypeJson = Deserialize_Read<UserType>.DesirializeRead(_UserType)?.Where(x => !x.IsDeleted && x.UserId == id).ToList();

                var typeJson = Deserialize_Read<Type>.DesirializeRead(_Type)?.Where(x => !x.IsDeleted).ToList();

                userTypeJson.ForEach(x => x.Type = typeJson.FirstOrDefault(y => y.Id == x.TypeId));

                userJson.UserTypes = userTypeJson;


                return Response<GetAllUsersViewModel>.Ok(userJson.UserTranslationObj());
            }
            catch (Exception ex)
            {
                return Response<GetAllUsersViewModel>.ExceptionThrow(ex.Message);
            }
        }

        //  Order by first name or last name 
        public Response<IEnumerable<GetAllUsersViewModel>> GetAllOrderedBy(bool orderByFirstName)
        {
            try
            {
                var user = GetAll().Value;

                IEnumerable<GetAllUsersViewModel> result = null;

                if (!orderByFirstName)
                {
                    result = user.OrderBy(x => x.LastName);
                }
                else
                {
                    result = user.OrderBy(x => x.FirstName);
                }

                return Response<IEnumerable<GetAllUsersViewModel>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<GetAllUsersViewModel>>.ExceptionThrow(ex.Message);
            }
        }
    }
}
