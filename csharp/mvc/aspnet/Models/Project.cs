
namespace aspnet
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        // Foreign key and navigation property
        public User User { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }

}