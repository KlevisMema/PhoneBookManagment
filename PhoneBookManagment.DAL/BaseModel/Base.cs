namespace PhoneBookManagment.DAL.BaseModel
{
    public abstract class Base
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
