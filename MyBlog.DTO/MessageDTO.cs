namespace MyBlog.DTO
{
    public class MessageDTO : IEntity
    {
        public int MessageId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string MessageContent { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
