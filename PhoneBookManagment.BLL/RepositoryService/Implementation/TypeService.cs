using Newtonsoft.Json;
using PhoneBookManagment.BLL.RepositoryService.GenericImplementation;
using PhoneBookManagment.BLL.RepositoryService.Interface;
using PhoneBookManagment.BLL.ResponseTypeService;
using PhoneBookManagment.DAL.Models;
using PhoneBookManagment.DTO.DTOs;
using PhoneBookManagment.DTO.MappingsDTO;

namespace PhoneBookManagment.BLL.RepositoryService.Implementation
{
    public class TypeService : ITypeService
    {
        // Get the directoryo  of the  project 
        private readonly string DatabaseContext = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName + "\\PhoneBookManagment\\PhoneBookManagment.DAL\\ApplicationContext";

        // Private fields that will hold the directory of each file 
        private readonly string _Type;
        private readonly string _UserType;

        public TypeService()
        {
            _Type = Path.Combine(DatabaseContext, "Types.json");
            _UserType = Path.Combine(DatabaseContext, "UserTypes.json");
        }

        // Create first type if file is empty
        private void CreateFirstType(AddTypeViewModel type)
        {
            var newType = new PhoneBookManagment.DAL.Models.Type
            {
                Id = 1,
                Name = type.TypeName,
                IsDeleted = false,
                CreatedOn = DateTime.Now,
            };

            var saveUser = "[ " + JsonConvert.SerializeObject(newType, Formatting.Indented) + " ]";
            File.WriteAllText(_Type, saveUser);
        }

        // Get all Types
        public Response<List<TpyesInfoViewModel>> GetAllTypes()
        {
            try
            {
                var types = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type);

                if (types is null)
                    return Response<List<TpyesInfoViewModel>>.ErrorMsg("Empty file..");

                return Response<List<TpyesInfoViewModel>>.Ok(types.Where(x => !x.IsDeleted)
                                                                  .Select(x => x.GetTypesInfo())
                                                                  .ToList());
            }
            catch (Exception ex)
            {
                return Response<List<TpyesInfoViewModel>>.ExceptionThrow(ex.Message);
            }
        }

        // Get a type by id
        public Response<TpyesInfoViewModel> GetA_Type(int id)
        {
            try
            {
                var types = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type);

                if (types is null)
                    return Response<TpyesInfoViewModel>.ErrorMsg("Empty file..");

                var type = types.FirstOrDefault(x => x.Id == id);

                if (type is null)
                    return Response<TpyesInfoViewModel>.ErrorMsg("Type doesnt exits");

                return Response<TpyesInfoViewModel>.Ok(type.GetTypesInfo());
            }
            catch (Exception ex)
            {
                return Response<TpyesInfoViewModel>.ExceptionThrow(ex.Message);
            }
        }

        // Add a Type
        public Response<AddTypeViewModel> AddType(AddTypeViewModel type)
        {
            try
            {
                var typesReadFromFile = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type);

                if (typesReadFromFile is null)
                {
                    CreateFirstType(type);
                    return Response<AddTypeViewModel>.Ok(type);
                }

                var typeId = typesReadFromFile.Count() + 1;

                typesReadFromFile.Add(new DAL.Models.Type
                {
                    Id = typeId,
                    Name = type.TypeName,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                });

                Serialize_Write<DAL.Models.Type>.SerializeWriteOnFile(typesReadFromFile, _Type);

                return Response<AddTypeViewModel>.Ok(type);
            }
            catch (Exception ex)
            {
                return Response<AddTypeViewModel>.ExceptionThrow(ex.Message);
            }
        }

        // Update a type
        public Response<TpyesInfoViewModel> UpdateType(UpdateTypeViewModel type)
        {
            try
            {
                var readTypeFileJson = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type).ToList();

                if (readTypeFileJson is null)
                    return Response<TpyesInfoViewModel>.ErrorMsg("Empty file..");

                var getType = readTypeFileJson.FirstOrDefault(x => x.Id == type.Id);
                if (getType is null)
                    return Response<TpyesInfoViewModel>.ErrorMsg("Type doesnt exits");

                readTypeFileJson.Where(x => x.Id == type.Id).ToList().ForEach(x =>
                {
                    x.CreatedOn = DateTime.Now;
                    x.IsDeleted = type.IsDeleted;
                    x.Name = type.Name;
                });

                Serialize_Write<DAL.Models.Type>.SerializeWriteOnFile(readTypeFileJson, _Type);

                // update all types in usertypes.json

                var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType).ToList();

                var currentType = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type)
                                                                   .FirstOrDefault(x => x.Id == type.Id);

                userTypes.Where(x => x.TypeId == type.Id).ToList().ForEach(x =>
                {
                    x.Type = currentType;
                });

                Serialize_Write<UserType>.SerializeWriteOnFile(userTypes, _UserType);

                return Response<TpyesInfoViewModel>.Ok(getType.GetTypesInfo());

            }
            catch (Exception ex)
            {
                return Response<TpyesInfoViewModel>.ExceptionThrow(ex.Message);
            }
        }

        // Delete a type
        public Response<TpyesInfoViewModel> DeleteType(int id)
        {
            try
            {
                // remove a type
                var readTypeFileJson = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type)
                                                                        .Where(x => !x.IsDeleted).ToList();

                if (readTypeFileJson is null)
                    return Response<TpyesInfoViewModel>.ErrorMsg("Empty file..");

                var getType = readTypeFileJson.FirstOrDefault(x => x.Id == id);
                if (getType is null)
                    return Response<TpyesInfoViewModel>.ErrorMsg("Type doesnt exits");

                readTypeFileJson.Where(x => x.Id == id).ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                });

                Serialize_Write<DAL.Models.Type>.SerializeWriteOnFile(readTypeFileJson, _Type);

                // remove users from UserYTypes.json

                var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType).ToList();

                var currentType = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type)
                                                                   .FirstOrDefault(x => x.Id == id);

                userTypes.Where(x => x.TypeId == id).ToList().ForEach(x =>
                {
                    x.Type.IsDeleted = true;
                });

                Serialize_Write<UserType>.SerializeWriteOnFile(userTypes, _UserType);

                return Response<TpyesInfoViewModel>.Ok(getType.GetTypesInfo());

            }
            catch (Exception ex)
            {
                return Response<TpyesInfoViewModel>.ExceptionThrow(ex.Message);
            }
        }
    }
}