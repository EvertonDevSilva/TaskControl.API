using TaskControl.Models.Enums;

namespace TaskControl.DTOs
{
    public class CreateTaskItemDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CompletionDate { get; set; }
    }
}
