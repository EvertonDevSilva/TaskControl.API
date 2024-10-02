using TaskControl.Models.Enums;

namespace TaskControl.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public EStatus Status { get; set; }
    public DateTime CompletionDate { get; set; }
}
