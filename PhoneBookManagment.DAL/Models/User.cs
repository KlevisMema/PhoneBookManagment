using PhoneBookManagment.DAL.BaseModel;

namespace PhoneBookManagment.DAL.Models
{
    public class User : Base
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        // Relationship M:M property
        public IEnumerable<UserType> UserTypes { get; set; }
    }
}
