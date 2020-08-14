namespace Shop.API.Domain.Models
{
    public class AdminRole
    {
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}