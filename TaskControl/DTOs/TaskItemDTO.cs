using TaskControl.Models.Enums;

namespace TaskControl.DTOs
{
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EStatus Status { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
