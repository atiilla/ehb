
namespace aspnet
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Navigation property
        public ICollection<Project> Projects { get; set; }
    }

}