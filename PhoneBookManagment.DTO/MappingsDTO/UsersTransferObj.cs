using PhoneBookManagment.DAL.Models;
using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.DTO.Mappings
{
    public static class UsersTransferObj
    {

        public static UsersInfoViewModel GetUsersInfo(this User user)
        {
            return new UsersInfoViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateCreated = user.CreatedOn,
                IsDeled = user.IsDeleted
            };
        }

        public static GetAllUsersViewModel UserTranslationObj(this User user)
        {
            return new GetAllUsersViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Number = user.UserTypes.Select(x=>x.Number).ToList(),
                TypeName = user.UserTypes.Select(x=>x.Type.Name).ToList(),
            };
        }

    }
}
