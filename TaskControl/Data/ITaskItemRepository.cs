using TaskControl.Models;
using TaskControl.Models.Enums;

namespace TaskControl.Data;

public interface ITaskItemRepository
{
    Task<List<TaskItem>> GetAllTaskItem();
    Task<TaskItem> GetTaskItemById(int id);
    Task<List<TaskItem>> GetAllTaskItemByStatus(EStatus status);
    Task<TaskItem> AddTaskItem(TaskItem request);
    Task<TaskItem> UpdateTaskItem(TaskItem request);
    Task DeleteTaskItem(int id);
}