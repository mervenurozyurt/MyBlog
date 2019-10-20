namespace MyBlog.DTO
{
    public class UserTypeDTO : IEntity
    {
        public byte UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }
}
