using Microsoft.AspNetCore.Mvc;
using PhoneBookManagment.BLL.RepositoryService.Interface;
using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.WEB.Controllers
{
    /// <summary>
    /// PhoneBook Api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookRepositoryService _phoneBookRepositoryService;

        /// <summary>
        /// Depenency injection
        /// </summary>
        /// <param name="phoneBookRepositoryService"></param>
        public PhoneBookController(IPhoneBookRepositoryService phoneBookRepositoryService)
        {
            _phoneBookRepositoryService = phoneBookRepositoryService;
        }

        /// <summary>
        ///  Get details about all clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllUsersViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUsersViewModel))]
        public ActionResult<List<GetAllUsersViewModel>> Get()
        {
            var result = _phoneBookRepositoryService.GetAll();

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.Message);
        }

        /// <summary>
        ///  Get details about client
        /// </summary>
        /// <param name="id"> Id of the user </param>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllUsersViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUsersViewModel))]
        public ActionResult<GetAllUsersViewModel> GetUserInfo(int id)
        {
            var result = _phoneBookRepositoryService.GetUser(id);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Get all Phone Books ordered
        /// </summary>
        /// <param name="orderByFirstName"> Firstname is true by  default </param>
        /// <returns >List of phone books</returns>
        [HttpGet("OrderBy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllUsersViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUsersViewModel))]
        public ActionResult<List<GetAllUsersViewModel>> Get(bool orderByFirstName = true)
        {
            var result = _phoneBookRepositoryService.GetAllOrderedBy(orderByFirstName);

            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result.Message);
        }
    }
}