namespace MyBlog.DTO
{
    public class UserDTO : IEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TownId { get; set; }
        public string MobilePhone { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public byte UserTypeID { get; set; }
        public string Token { get; set; }
        public System.DateTime? TokenValidUntil { get; set; }
        public byte RecordStatusId { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string TownName { get; set; }
        public string IsEmailVerifiedDisplay { get { return IsEmailVerified ? "Onaylandı" : "Onaylanmadı"; } }
        public string CreatedDateDisplay { get { return CreatedDate.ToShortDateString(); } }
        public string UserTypeName { get; set; }
        public string RecordStatusName { get; set; }
    }
}
