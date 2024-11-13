

namespace aspnet
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }

        // Foreign key and navigation property
        public Project Project { get; set; }
    }

}