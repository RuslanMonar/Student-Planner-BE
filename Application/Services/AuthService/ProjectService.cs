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
                Color = color,
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
                        join groups in _context.Groups on project.UserId equals groups.UserId into GP
                        from m in GP.DefaultIfEmpty()
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

    }
}
