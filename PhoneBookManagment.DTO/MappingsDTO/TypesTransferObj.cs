using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.DTO.MappingsDTO
{
    public static class TypesTransferObj
    {
        public static TpyesInfoViewModel GetTypesInfo(this PhoneBookManagment.DAL.Models.Type type)
        {
            return new TpyesInfoViewModel
            {
                Id = type.Id,
                Name = type.Name,
                DateCreatd = type.CreatedOn,
                IsDeleted = type.IsDeleted,
            };
        }
    }
}
