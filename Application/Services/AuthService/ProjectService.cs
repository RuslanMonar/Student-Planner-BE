using Application.Services.AuthService.Dto;
using Application.Services.AuthService.Interfaces;
using Domain;
using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Application.Services.AuthService
{
    public class ProjectService : IProjectService
    {
        private readonly IInfrastructureContext _context;
        public ProjectService(IInfrastructureContext context)
        {
            _context = context;
        }

        public async Task AddGroup(string title, string color, Guid userId)
        {
            _context.Groups.Add(new Group
            {
                Title = title,
                UserId = userId,
                Color = color
            });

            _context.Projects.Add(new Project
            {
                Title = string.Empty,
                UserId = userId,
                Color = string.Empty,
                GroupdId = null,
                isInitial = true
            });

            await _context.SaveChangesAsync();
        }

        public async Task AddProject(string title, string color, Guid userId, int? groupId = null)
        {
            _context.Projects.Add(new Project
            {
                Title = title,
                UserId = userId,
                Color = color,
                GroupdId = groupId,
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupViewModel>> GetProjetcsByGroups(Guid userId)
        {
            //var a = await _context.Projects.Include(x => x.Group).Where(x => x.UserId == userId).ToListAsync();

            //var projects = await _context.Projects.Where(x => x.UserId == userId).ToListAsync();
            //var groups = await _context.Groups.Where(x => x.UserId != userId).ToListAsync();


            var data = (from project in _context.Projects
                        where project.UserId == userId
                        join groups in _context.Groups on project.UserId equals groups.UserId into GP
                        from m in GP
                        select new GroupViewModel
                        {
                            Title = m.Title,
                            Color = m.Color,
                            Id = m.Id,
                            Projects = m.Projects.Select(x => new ProjectViewModel
                            {
                                Id = x.Id,
                                Title = x.Title,
                                Color = x.Color,
                                GroupdId = x.GroupdId,
                            }).ToList(),
                        }).ToArray();

            var result = data.DistinctBy(x => x.Title).ToList();

            //var result = a.GroupBy(x => x.GroupdId).ToArray();

            return result;
        }

        public async Task<List<Group>> GetGroups(Guid userId)
        {
            var res = await _context.Groups.Where(x => x.UserId == userId).ToListAsync();

            return res;
        }
        public async Task<List<ProjectViewModel>> GetProjetcsWithoutGroup(Guid userId)
        {
            var res = await _context.Projects.Where(x => x.UserId == userId && x.GroupdId == null).Select(x => new ProjectViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Color = x.Color
            }).ToListAsync();

            return res;
        }
        public async Task<List<ProjectViewModel>> GetAllProjects(Guid userId)
        {
            var res = await _context.Projects.Where(x => x.UserId == userId && x.isInitial == false).Select(x =>
            new ProjectViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Color = x.Color
            }).ToListAsync();

            return res;
        }

        public async Task AddTask(TaskDto dto, Guid userId)
        {
            _context.Tasks.Add(new Tasks
            {
                UserId = userId,
                Title = dto.Title,
                TomatoCount = dto.TomatoCount,
                TomatoLength = dto.TomatoLength,
                TotalTime = dto.TotalTime,
                Flag = dto.Flag,
                Completed = false,
                Date = dto.Date,
                Description = dto.Description,
                TimeComplited = 0,
                TimeLeft = dto.TotalTime,
                ProjectId = dto.ProjectId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<TasksViewModel>> GetAllTasks(Guid userId)
        {
            var res = await _context.Tasks.Where(x => x.UserId == userId).Select(x =>
            new TasksViewModel
            {
                Id = x.Id,
                Title = x.Title,
                TomatoCount = x.TomatoCount,
                TomatoLength = x.TomatoLength,
                TotalTime = x.TotalTime,
                Flag = x.Flag,
                Completed = x.Completed,
                Date = x.Date,
                Description = string.IsNullOrEmpty(x.Description) ? String.Empty : x.Description,
                TimeComplited = x.TimeComplited,
                TimeLeft = x.TimeLeft,
                ProjectId = x.ProjectId,
                Project = new Project
                {
                    Id = x.Project.Id,
                    Title = x.Project.Title,
                    Color = x.Project.Color,
                }
            }).ToListAsync();

            return res;
        }

        public async Task EditTask(AddTaskDto dto)
        {
            var task = _context.Tasks.Where(x => x.Id == dto.Id).FirstOrDefault();

            task.Title = dto.Title;
            task.TomatoCount = dto.TomatoCount;
            task.TomatoLength = dto.TomatoLength;
            task.TotalTime = dto.TotalTime;
            task.Flag = dto.Flag;
            task.Date = dto.Date;
            task.Description = string.IsNullOrEmpty(dto.Description) ? String.Empty : dto.Description;
            task.ProjectId = dto.ProjectId;



            await _context.SaveChangesAsync();
        }

        public async Task<List<TasksViewModel>> GetTasksById(Guid userId, int id)
        {
            var res = await _context.Tasks.Where(x => x.UserId == userId && x.ProjectId == id).Select(x =>
            new TasksViewModel
            {
                Id = x.Id,
                Title = x.Title,
                TomatoCount = x.TomatoCount,
                TomatoLength = x.TomatoLength,
                TotalTime = x.TotalTime,
                Flag = x.Flag,
                Completed = x.Completed,
                Date = x.Date,
                Description = string.IsNullOrEmpty(x.Description) ? String.Empty : x.Description,
                TimeComplited = x.TimeComplited,
                TimeLeft = x.TimeLeft,
                ProjectId = x.ProjectId,
                Project = new Project
                {
                    Id = x.Project.Id,
                    Title = x.Project.Title,
                    Color = x.Project.Color,
                }
            }).ToListAsync();

            return res;
        }

        public async Task StartTask(TrackTimeDto dto)
        {
            _context.TasksTrack.Add(new TaskTrack
            {
                TaskId = dto.TaskId,
                StartDate = dto.Date,
                TimeSpentMinutes = 0
            });

            await _context.SaveChangesAsync();
        }

        public async Task EndTask(TrackTimeDto dto)
        {
            var trackTask = _context.TasksTrack.FirstOrDefault(x => x.TaskId == dto.TaskId && x.EndDate == null);

            trackTask.EndDate = dto.Date;
            trackTask.TimeSpentMinutes = dto.TimeSpentMinutes.HasValue ? (double)dto.TimeSpentMinutes : 0;

            var task = _context.Tasks.FirstOrDefault(x => x.Id == dto.TaskId);
            task.TimeComplited = (int)(task.TimeComplited + dto.TimeSpentMinutes);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TasksStatisticViewModel>> GetTasksStatic(Guid userId)
        {
            var res = await _context.Tasks.Where(x => x.UserId == userId).Select(x => new TasksStatisticViewModel
            {
                ProjectTitle = x.Project.Title,
                ProjectColor = x.Project.Color,
                TaskTitle = x.Title,
                TasksTrack = x.TasksTrack.Select(m => new TaskTrackViewModel
                {
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    TimeSpentMinutes = m.TimeSpentMinutes,
                    TaskId = m.TaskId
                }).ToList(),
            }).ToListAsync();

            return res;
        }
    }
}
