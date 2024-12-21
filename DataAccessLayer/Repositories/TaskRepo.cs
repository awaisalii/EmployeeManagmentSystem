
using AutoMapper;
using BuisnessLayer.DTO.Request;
using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TaskRepo : ITask
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public TaskRepo(AppDbContext context , UserManager<ApplicationUser> userManager , IMapper mapper )
        {
            this._context = context;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse> CreateAsync(TaskRequest model, string creatorId)
        {
            var user=await _context.Users.FindAsync(creatorId);
            if (model == null)
            {
                return new ServiceResponse(false, "Invalid input");
            }
            if (user == null)
            {
                return new ServiceResponse(false, "Creator not found");
            }
            var newTask = new TaskModel
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                DueDate = model.DueDate,
                Priority = model.Priority,
                Progress = model.Progress,
                Status = model.Status,
                AssignedToId = model.AssignedToId,
                ApplicationUserId=model.AssignedToId,
                CreatedById = user.Id          
            };
            try
            {
                await _context.AddAsync(newTask);
                await _context.SaveChangesAsync();
                var taskGroup = new GroupChat()
                {
                    Name = newTask.Title,
                    TaskModelId = newTask.Id,
                    AssociatedTask = newTask,
                    IsPrivate = false,
                    ChatUsers = new List<ApplicationUser>
                {
                    user
                }
                };
                _context.GroupChats.Add(taskGroup);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Task created successfully");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, "Error creating task: " + ex.Message);
            }
        }




        public async Task<ServiceResponse> DeleteTaskAsync(string taskId, string userId)
        {
            if (!int.TryParse(taskId, out var parsedTaskId))
            {
                return new ServiceResponse(false, "Invalid Task ID");
            }

            var task = await _context.TasksModel.FindAsync(parsedTaskId);
            if (task == null)
            {
                return new ServiceResponse(false, "Task not found");
            }
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Notes.RemoveRange(_context.Notes.Where(n => n.TaskId == task.Id));
                _context.Activities.RemoveRange(_context.Activities.Where(a => a.TaskId == task.Id));
                var groupChat = _context.GroupChats.FirstOrDefault(x => x.TaskModelId == task.Id);
                if (groupChat != null)
                {
                    _context.GroupChats.Remove(groupChat);
                }
                _context.TasksModel.Remove(task);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResponse(true, "Task deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ServiceResponse(false, $"An error occurred: {ex.Message}");
            }
        }


        public async Task<List<TaskDto>> GetAllAsync()
        {
            var tasks = await _context.TasksModel
           .Include(t => t.AssignedTo)
           .Include(t => t.CreatedBy)
           .ToListAsync();
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);
            return taskDtos;
        }

        public async Task<TaskDto> GetById(string taskId,string userId)
        {
            var t =await _context.TasksModel
            .Include(x=>x.AssignedTo)
            .Include(y=>y.CreatedBy)
            .Include(x=>x.Notes)
            .Include(x=>x.Activities)
            .Include(x=>x.GroupChat)
            .FirstOrDefaultAsync(t=>t.Id==int.Parse(taskId));
            var result = _mapper.Map<TaskDto>(t);
            return result;
        }

        public async Task<IEnumerable<TaskModel>> GetUserTasks(string userId)
        {
            var tasks = await _context.TasksModel
                                  .Include(y => y.CreatedBy)
                                  .Where(t => t.AssignedToId == userId)
                                  .ToListAsync();
            return tasks;
        }


        public async Task<ServiceResponse> UpdateTask(TaskRequest model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse(false, "Unauthorized");
            }
            var assignedUser = await _userManager.FindByIdAsync(model.AssignedToId);
            if (assignedUser == null)
            {
                return new ServiceResponse(false, "Assigned user not found");
            }
            var existingTask = await _context.TasksModel.FindAsync(model.Id);
            if (existingTask == null)
            {
                return new ServiceResponse(false, "Task not found");
            }
            existingTask.Description = model.Description;
            existingTask.Title = model.Title;
            existingTask.AssignedToId = model.AssignedToId;
            existingTask.Status = model.Status;
            existingTask.DueDate = model.DueDate;
            existingTask.Priority = model.Priority;
            existingTask.StartDate = model.StartDate;
            existingTask.ApplicationUserId = model.AssignedToId;
            try
            {
                _context.TasksModel.Update(existingTask);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Task updated successfully");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, $"Error updating task: {ex.Message}");
            }
        }

    }
}
