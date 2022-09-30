using PhoneBookManagment.DAL.Models;

namespace PhoneBookManagment.DTO.DTOs
{
    public class GetAllUsersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Number { get; set; }
        public List<string> TypeName { get; set; }
    }
}
