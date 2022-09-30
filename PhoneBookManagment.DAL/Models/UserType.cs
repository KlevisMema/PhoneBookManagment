using PhoneBookManagment.DAL.BaseModel;

namespace PhoneBookManagment.DAL.Models
{
    public class UserType : Base
    {
        public string Number { get; set; } = String.Empty;

        // // Relationship M:M property
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public User User { get; set; }
        public Type Type { get; set; }
    }
}
