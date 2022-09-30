using System.ComponentModel.DataAnnotations;

namespace PhoneBookManagment.DTO.DTOs
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "User id is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string LastName { get; set; }
        public int TypeId { get; set; }
        public string Number { get; set; }
    }
}
