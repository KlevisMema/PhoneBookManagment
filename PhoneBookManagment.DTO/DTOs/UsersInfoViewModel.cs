namespace PhoneBookManagment.DTO.DTOs
{
    public class UsersInfoViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeled { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
