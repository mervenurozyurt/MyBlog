namespace MyBlog.DTO
{
    public class MailDTO : IEntity
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
