using Newtonsoft.Json;
using PhoneBookManagment.BLL.RepositoryService.GenericImplementation;
using PhoneBookManagment.BLL.RepositoryService.Interface;
using PhoneBookManagment.BLL.ResponseTypeService;
using PhoneBookManagment.DAL.Models;
using PhoneBookManagment.DTO.DTOs;
using PhoneBookManagment.DTO.Mappings;

namespace PhoneBookManagment.BLL.RepositoryService.Implementation
{
    public class UserService : IUserService
    {
        // Get the directoryo  of the  project 
        private readonly string DatabaseContext = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName + "\\PhoneBookManagment\\PhoneBookManagment.DAL\\ApplicationContext";

        // Private fields that will hold the directory of each file 
        private readonly string _User;
        private readonly string _Type;
        private readonly string _UserType;

        public UserService()
        {
            _User = Path.Combine(DatabaseContext, "Users.json");
            _Type = Path.Combine(DatabaseContext, "Types.json");
            _UserType = Path.Combine(DatabaseContext, "UserTypes.json");
        }


        // Add first users in user.json and usertype.json 
        private void AddFirstUser(AddUserViewModel user)
        {
            var newUser = new User
            {
                Id = 1,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsDeleted = false,
                CreatedOn = DateTime.Now,
            };

            var saveUser = "[ " + JsonConvert.SerializeObject(newUser, Formatting.Indented) + " ]";
            File.WriteAllText(_User, saveUser);

            // check if  UserTypes file is empty if yes populate with data

            var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType)?.ToList();
            if (userTypes is null)
            {
                var currentUser = Deserialize_Read<User>.DesirializeRead(_User)?.FirstOrDefault(x => x.Id == 1);
                var currentType = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type)?.ToList()
                                                                   .FirstOrDefault(x => x.Id == user.TypeId);


                var newUserType = new UserType
                {
                    Id = 1,
                    Number = user.Number,
                    TypeId = user.TypeId,
                    UserId = 1,
                    CreatedOn = DateTime.Now,
                    User = currentUser,
                    Type = currentType
                };

                var saveUserType = " [ " + JsonConvert.SerializeObject(newUserType, Formatting.Indented) + " ] ";
                File.WriteAllText(_UserType, saveUserType);
            }
        }

        // User is present add  to usertype.json
        private void AddExistingUser(AddUserViewModel user)
        {
            var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType)?.ToList();
            var userTypeId = userTypes.Count() + 1;

            var currentUser = Deserialize_Read<User>.DesirializeRead(_User).FirstOrDefault(x => x.Id == user.UserId);

            var currentType = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type)?.ToList()
                                                        .FirstOrDefault(x => x.Id == user.TypeId);

            userTypes.Add(new UserType
            {
                Id = userTypeId,
                Number = user.Number,
                TypeId = user.TypeId,
                UserId = user.UserId,
                CreatedOn = DateTime.Now,
                User = currentUser,
                Type = currentType
            });

            Serialize_Write<UserType>.SerializeWriteOnFile(userTypes, _UserType);
        }

        // Get all Users
        public Response<List<UsersInfoViewModel>> GetAllUsers()
        {
            try
            {
                var users = Deserialize_Read<User>.DesirializeRead(_User)?.Where(x => !x.IsDeleted).ToList();

                if (users is null)
                    return Response<List<UsersInfoViewModel>>.ErrorMsg("Empty file..");

                return Response<List<UsersInfoViewModel>>.Ok(users.Select(x => x.GetUsersInfo()).ToList());
            }
            catch (Exception ex)
            {
                return Response<List<UsersInfoViewModel>>.ExceptionThrow(ex.Message);
            }
        }

        // Get a user by id
        public Response<UsersInfoViewModel> GetUser(int id)
        {
            try
            {
                var getUser = Deserialize_Read<User>.DesirializeRead(_User);

                if (getUser is null)
                    return Response<UsersInfoViewModel>.ErrorMsg("Empty file..");

                var user = getUser.FirstOrDefault(x => x.Id == id);

                if (user is null)
                    return Response<UsersInfoViewModel>.ErrorMsg("Type doesnt exits");

                return Response<UsersInfoViewModel>.Ok(user.GetUsersInfo());
            }
            catch (Exception ex)
            {
                return Response<UsersInfoViewModel>.ExceptionThrow(ex.Message);
            }
        }

        // Add a user
        public Response<AddUserViewModel> AddUser(AddUserViewModel user)
        {
            try
            {
                // Post user 
                var users = Deserialize_Read<User>.DesirializeRead(_User)?
                                                  .Where(isdeleted => isdeleted.IsDeleted == false)
                                                  .ToList();

                var currentType = Deserialize_Read<DAL.Models.Type>.DesirializeRead(_Type)?
                                                                   .ToList()
                                                                   .Where(x => x.IsDeleted == false)
                                                                   .FirstOrDefault(x => x.Id == user.TypeId);

                if (currentType is null)
                    return Response<AddUserViewModel>.ErrorMsg("Type doesnt  exists");

                if (users is null)
                {
                    AddFirstUser(user);
                    return Response<AddUserViewModel>.Ok(user);
                }

                var userExists = Deserialize_Read<User>.DesirializeRead(_User)?
                                                       .ToList()
                                                       .Any(x => x.Id == user.UserId);

                if (userExists is true)
                {
                    AddExistingUser(user);
                    return Response<AddUserViewModel>.Ok(user);
                }

                var userId = users.Count() + 1;

                users.Add(new User
                {
                    Id = userId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                });

                Serialize_Write<User>.SerializeWriteOnFile(users, _User);

                // Post UserType

                var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType).ToList();
                var userTypeId = userTypes.Count() + 1;

                var currentUser = Deserialize_Read<User>.DesirializeRead(_User).FirstOrDefault(x => x.Id == userId);

                userTypes.Add(new UserType
                {
                    Id = userTypeId,
                    Number = user.Number,
                    TypeId = user.TypeId,
                    UserId = userId,
                    CreatedOn = DateTime.Now,
                    User = currentUser,
                    Type = currentType
                });

                Serialize_Write<UserType>.SerializeWriteOnFile(userTypes, _UserType);

                return Response<AddUserViewModel>.Ok(user);
            }
            catch (Exception ex)
            {
                return Response<AddUserViewModel>.ExceptionThrow(ex.Message);
            }
        }

        // Edit a user
        public Response<UpdateUserViewModel> EditUser(UpdateUserViewModel user)
        {
            try
            {
                // edit user 
                var users = Deserialize_Read<User>.DesirializeRead(_User)?
                                                  .Where(isdeleted => isdeleted.IsDeleted == false)
                                                  .ToList();

                if (user is null)
                    return Response<UpdateUserViewModel>.ErrorMsg("File is empty...");

                var getUserToBeUpdated = users.Where(x => x.Id == user.Id).FirstOrDefault();

                if (getUserToBeUpdated is null)
                    return Response<UpdateUserViewModel>.ErrorMsg("User doesn't exists");

                users.Where(x => x.Id == user.Id).ToList().ForEach(x =>
                {
                    x.CreatedOn = DateTime.Now;
                    x.IsDeleted = user.IsDeleted;
                    x.FirstName = user.FirstName;
                    x.LastName = user.LastName;
                });

                Serialize_Write<User>.SerializeWriteOnFile(users, _User);

                // update  all users in  UserType.json

                var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType)?.ToList();

                var currentUser = Deserialize_Read<User>.DesirializeRead(_User)
                                                        .FirstOrDefault(x => x.Id == user.Id);

                userTypes.Where(x => x.UserId == user.Id).ToList().ForEach(x =>
                {
                    x.User = currentUser;
                });

                Serialize_Write<UserType>.SerializeWriteOnFile(userTypes, _UserType);

                return Response<UpdateUserViewModel>.Ok(user);
            }
            catch (Exception ex)
            {
                return Response<UpdateUserViewModel>.ExceptionThrow(ex.Message);
            }
        }

        // Delete a user
        public Response<UsersInfoViewModel> DeleteUser(int id)
        {
            try
            {
                // remove user from users.json
                var readUserFileJson = Deserialize_Read<User>.DesirializeRead(_User).Where(x => !x.IsDeleted).ToList();

                if (readUserFileJson is null)
                    return Response<UsersInfoViewModel>.ErrorMsg("Empty file..");

                var getUser = readUserFileJson.FirstOrDefault(x => x.Id == id);
                if (getUser is null)
                    return Response<UsersInfoViewModel>.ErrorMsg("User doesnt exits");

                readUserFileJson.Where(x => x.Id == id).ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                });

                Serialize_Write<User>.SerializeWriteOnFile(readUserFileJson, _User);

                // remove users from UserYTypes.json

                var userTypes = Deserialize_Read<UserType>.DesirializeRead(_UserType).ToList();

                var currentUser = Deserialize_Read<User>.DesirializeRead(_User)
                                                        .FirstOrDefault(x => x.Id == id);

                userTypes.Where(x => x.UserId == id).ToList().ForEach(x =>
                {
                    x.User.IsDeleted = true;
                });

                Serialize_Write<UserType>.SerializeWriteOnFile(userTypes, _UserType);

                return Response<UsersInfoViewModel>.Ok(getUser.GetUsersInfo());

            }
            catch (Exception ex)
            {
                return Response<UsersInfoViewModel>.ExceptionThrow(ex.Message);
            }
        }
    }
}