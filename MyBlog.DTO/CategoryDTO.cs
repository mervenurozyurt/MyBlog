namespace MyBlog.DTO
{
    public class CategoryDTO : IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte RecordStatusId { get; set; }

        public int NumberOfBlogs { get; set; }
        public string RecordStatusName { get; set; }
    }
}
