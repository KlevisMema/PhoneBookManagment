using System.ComponentModel.DataAnnotations;

namespace PhoneBookManagment.DTO.DTOs
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "User id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
    }
}
