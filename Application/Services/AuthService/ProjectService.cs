using Application.Services.AuthService.Interfaces;
using Domain;
using Infrastructure;

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
    }
}
