using Microsoft.AspNetCore.Mvc;
using PhoneBookManagment.BLL.RepositoryService.Interface;
using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.WEB.Controllers
{
    /// <summary>
    /// User API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Service injection
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersInfoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UsersInfoViewModel))]
        public ActionResult<List<UsersInfoViewModel>> GetUsers()
        {
            var getAllUsersResult = _userService.GetAllUsers();

            if (getAllUsersResult.Success)
                return Ok(getAllUsersResult.Value);

            return BadRequest(getAllUsersResult.Message);
        }

        /// <summary>
        /// Get a single user by id
        /// </summary>
        /// <param name="id"> Id of the user you want info about </param>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpGet("GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersInfoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UsersInfoViewModel))]
        public ActionResult<UsersInfoViewModel> GetSingleUser(int id)
        {
            var getUserResult = _userService.GetUser(id);

            if (getUserResult.Success)
                return Ok(getUserResult.Value);

            return BadRequest(getUserResult.Message);
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user"> User object </param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddUserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AddUserViewModel))]
        public ActionResult<List<AddUserViewModel>> CreateUser([FromForm] AddUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var addUserResult = _userService.AddUser(user);

                if (addUserResult.Success)
                    return Ok(addUserResult.Value);

                return BadRequest(addUserResult.Message);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="type"> User u want to update </param>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUserViewModel))]
        public ActionResult<UpdateUserViewModel> UpdateType([FromForm] UpdateUserViewModel type)
        {
            if (ModelState.IsValid)
            {
                var updatedUserResult = _userService.EditUser(type);

                if (updatedUserResult.Success)
                    return Ok(updatedUserResult.Value);

                return BadRequest(updatedUserResult.Message);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"> Id of the user you want to delete</param>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TpyesInfoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TpyesInfoViewModel))]
        public ActionResult<TpyesInfoViewModel> DeleteType(int id)
        {
            var deleteUserRsult = _userService.DeleteUser(id);

            if (deleteUserRsult.Success)
                return Ok(deleteUserRsult.Value);

            return BadRequest(deleteUserRsult.Message);
        }
    }
}
