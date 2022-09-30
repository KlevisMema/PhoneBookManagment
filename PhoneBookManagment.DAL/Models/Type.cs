using PhoneBookManagment.DAL.BaseModel;

namespace PhoneBookManagment.DAL.Models
{
    public class Type : Base
    {
        public string Name { get; set; } = String.Empty;

        // Relationship M:M property
        public IEnumerable<UserType> UserTypes { get; set; }
    }
}
