using Application.Services.AuthService.Dto;
using Domain;
using Domain.Models;

namespace Application.Services.AuthService.Interfaces
{
    public interface IProjectService
    {
        Task AddGroup(string title, string color, Guid userId);
        Task AddProject(string title, string color, Guid userId, int? groupId);

        Task<IEnumerable<GroupViewModel>> GetProjetcsByGroups(Guid userId);
        Task<List<Group>> GetGroups(Guid userId);
        Task<List<ProjectViewModel>> GetProjetcsWithoutGroup(Guid userId);
        Task<List<ProjectViewModel>> GetAllProjects(Guid userId);
        Task AddTask(TaskDto dto, Guid userId);
        Task<List<TasksViewModel>> GetAllTasks(Guid userId);
        Task EditTask(AddTaskDto dto);
        Task<List<TasksViewModel>> GetTasksById(Guid userId, int id);
        Task StartTask(TrackTimeDto dto);
        Task EndTask(TrackTimeDto dto);
        Task<List<TasksStatisticViewModel>> GetTasksStatic(Guid userId);
    }
}
