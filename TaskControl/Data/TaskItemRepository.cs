using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskControl.Context;
using TaskControl.Models;
using TaskControl.Models.Enums;

namespace TaskControl.Data;

public class TaskItemRepository(AppDbContext dbContext) : ITaskItemRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<TaskItem>> GetAllTaskItem()
    {
        return await _dbContext.TaskItems.ToListAsync();
    }

    public async Task<List<TaskItem>> GetAllTaskItemByStatus(EStatus status)
    {
        return await _dbContext.TaskItems.Where(t => t.Status == status).ToListAsync();
    }

    public async Task<TaskItem> GetTaskItemById(int id)
    {
        return await _dbContext.TaskItems.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> AddTaskItem(TaskItem request)
    {
        await _dbContext.TaskItems.AddAsync(request);
        await _dbContext.SaveChangesAsync();
        return request;
    }

    public async Task<TaskItem> UpdateTaskItem(TaskItem request)
    {
        var taskItemn = await GetTaskItemById(request.Id);
        if (taskItemn != null)
        {
            taskItemn.Id = request.Id;
            taskItemn = request;

            _dbContext.TaskItems.Update(taskItemn);
            await _dbContext.SaveChangesAsync();
            return taskItemn;
        }

        throw new Exception("Object not found");
    }

    public async Task DeleteTaskItem(int id)
    {
        _dbContext.TaskItems.Remove(new TaskItem { Id = id});
        await _dbContext.SaveChangesAsync();
    }
}
