namespace Application.Services.AuthService.Interfaces
{
    public interface IProjectService
    {
        Task AddGroup(string title, string color, Guid userId);
        Task AddProject(string title, string color, Guid userId, int? groupId);
    }
}
