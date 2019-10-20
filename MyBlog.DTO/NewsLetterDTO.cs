namespace MyBlog.DTO
{
    public class NewsLetterDTO : IEntity
    {
        public int NewsLetterId { get; set; }
        public string EmailAddress { get; set; }
    }
}
