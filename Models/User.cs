namespace Kramskoi_AutoService.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
    }
}
