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
    }
}
