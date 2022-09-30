using System.ComponentModel.DataAnnotations;

namespace PhoneBookManagment.DTO.DTOs
{
    public class AddTypeViewModel
    {
        [Required(ErrorMessage = "Type name is required !!")]
        [StringLength(maximumLength:7,MinimumLength =2)]
        public string TypeName { get; set; }
    }
}
