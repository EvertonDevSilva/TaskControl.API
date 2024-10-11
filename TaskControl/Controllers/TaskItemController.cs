using Microsoft.AspNetCore.Mvc;
using TaskControl.Data;
using TaskControl.DTOs;
using TaskControl.Models;
using TaskControl.Models.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController(ITaskItemRepository taskItemRepository) : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        [HttpGet]
        public async Task<IActionResult> GetAllTaskItem()
        {
            var response = await _taskItemRepository.GetAllTaskItem();
            return Ok(TaskItemToTaskItemDTO(response));
        }

        [HttpGet("ByStatus/{status}")]
        public async Task<IActionResult> GetAllTaskItemByStatus(EStatus status)
        {
            var taskItemList = await _taskItemRepository.GetAllTaskItemByStatus(status);
            return Ok(TaskItemToTaskItemDTO(taskItemList));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItemById(int id)
        {
            var response = await _taskItemRepository.GetTaskItemById(id);
            if(response is not null)
                return Ok(TaskItemToTaskItemDTO(response));

            throw new Exception("Object not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskItem([FromBody] CreateTaskItemDTO request)
        {
            var taskItem = CreateTaskItemDTOToTaskItem(request);
            taskItem.Status = EStatus.Pending;
            var response = await _taskItemRepository.AddTaskItem(taskItem);
            return Ok(TaskItemToTaskItemDTO(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskItemDTO request)
        {
            var taskItem = await _taskItemRepository.UpdateTaskItem(UpdateTaskItemDTOToTaskItem(id, request));
            return Ok(TaskItemToTaskItemDTO(taskItem));
        }

        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await _taskItemRepository.DeleteTaskItem(id);
        }

        #region Mappers
        private static List<TaskItemDTO> TaskItemToTaskItemDTO(List<TaskItem> taskItemlist)
        {
            var response = new List<TaskItemDTO>();
            foreach (var taskItem in taskItemlist)
            {
                response.Add(
                    new TaskItemDTO
                    {
                        Id = taskItem.Id,
                        Title = taskItem.Title,
                        Description = taskItem.Description,
                        Status = taskItem.Status,
                        CompletionDate = taskItem.CompletionDate
                    });
            }

            return response;
        }

        private static TaskItemDTO TaskItemToTaskItemDTO(TaskItem taskItem)
        {
            return new TaskItemDTO
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status,
                CompletionDate = taskItem.CompletionDate
            };
        }

        private static TaskItem CreateTaskItemDTOToTaskItem(CreateTaskItemDTO taskItem)
        {
            return new TaskItem
            {
                Title = taskItem.Title,
                Description = taskItem.Description,
                CompletionDate = taskItem.CompletionDate
            };
        }

        private static TaskItem UpdateTaskItemDTOToTaskItem(int id, UpdateTaskItemDTO taskItem)
        {
            return new TaskItem
            {
                Id = id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status,
                CompletionDate = taskItem.CompletionDate
            };
        }
        #endregion
    }
}
