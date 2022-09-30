using System.ComponentModel.DataAnnotations;

namespace PhoneBookManagment.DTO.DTOs
{
    public class UpdateTypeViewModel
    {
        [Required(ErrorMessage = "Type Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Type name is required")]
        [StringLength(maximumLength: 7, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
    }
}
