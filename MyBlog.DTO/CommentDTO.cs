namespace MyBlog.DTO
{
    public class CommentDTO : IEntity
    {
        public int CommentId { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string CommentContent { get; set; }
        public bool IsConfirmed { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public string BlogName { get; set; }
        public string UserName { get; set; }
    }
}
