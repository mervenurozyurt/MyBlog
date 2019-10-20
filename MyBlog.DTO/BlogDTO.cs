namespace MyBlog.DTO
{
    public class BlogDTO : IEntity
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        public string BlogContent { get; set; }
        public bool IsConfirmed { get; set; }
        public byte RecordStatusId { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public string CategoryName { get; set; }
        public string Author { get; set; }
        public string RecordStatusName { get; set; }
    }
}
