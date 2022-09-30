using Microsoft.AspNetCore.Mvc;
using PhoneBookManagment.BLL.RepositoryService.Interface;
using PhoneBookManagment.DTO.DTOs;

namespace PhoneBookManagment.WEB.Controllers
{
    /// <summary>
    /// Type Conntroller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        /// <summary>
        /// Service Ijection
        /// </summary>
        /// <param name="typeService"></param>
        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        /// <summary>
        /// Get all types from json file
        /// </summary>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TpyesInfoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TpyesInfoViewModel))]
        public ActionResult<List<TpyesInfoViewModel>> GetUsers()
        {
            var types = _typeService.GetAllTypes();

            if (types.Success)
                return Ok(types.Value);

            return BadRequest(types.Message);
        }

        /// <summary>
        /// Get a single type by id
        /// </summary>
        /// <param name="id"> Id of the type </param>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpGet("GetTypeId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TpyesInfoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TpyesInfoViewModel))]
        public ActionResult<TpyesInfoViewModel> GetSingleType(int id)
        {
            var types = _typeService.GetA_Type(id);

            if (types.Success)
                return Ok(types.Value);

            return BadRequest(types.Message);
        }

        /// <summary>
        /// Post a type
        /// </summary>
        /// <param name="type"> Type object </param>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddTypeViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AddTypeViewModel))]
        public ActionResult<List<AddTypeViewModel>> PostType([FromForm] AddTypeViewModel type)
        {
            if (ModelState.IsValid)
            {
                var types = _typeService.AddType(type);

                if (types.Success)
                    return Ok(types.Value);

                return BadRequest(types.Message);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Update a type
        /// </summary>
        /// <param name="type"> Type object </param>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TpyesInfoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TpyesInfoViewModel))]
        public ActionResult<TpyesInfoViewModel> UpdateType([FromForm] UpdateTypeViewModel type)
        {
            if (ModelState.IsValid)
            {
                var updatedType = _typeService.UpdateType(type);

                if (updatedType.Success)
                    return Ok(updatedType.Value);

                return BadRequest(updatedType.Message);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete a type
        /// </summary>
        /// <param name="id"> Id of the type you want to delete </param>
        /// <returns code="400"> Bad Request </returns>
        /// <returns code="200"> Ok </returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TpyesInfoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TpyesInfoViewModel))]
        public ActionResult<TpyesInfoViewModel> DeleteType(int id)
        {
            var deleteType = _typeService.DeleteType(id);

            if (deleteType.Success)
                return Ok(deleteType.Value);

            return BadRequest(deleteType.Message);
        }
    }
}
