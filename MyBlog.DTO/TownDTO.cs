namespace MyBlog.DTO
{
    public class TownDTO : IEntity
    {
        public int TownID { get; set; }
        public string TownName { get; set; }
        public int CityID { get; set; }
    }
}
