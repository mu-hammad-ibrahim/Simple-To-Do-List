namespace To_Do_List.Dto
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Low";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
    }
}
