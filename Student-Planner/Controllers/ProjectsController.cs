using Application.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Student_Planner.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Student_Planner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        public readonly IProjectService _project;

        public ProjectsController(IProjectService project)
        {
            _project = project;
        }

        [Authorize]
        protected Guid GetUserId()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""); 
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            var userId = token.Claims.First(claim => claim.Type == "nameid").Value; 
            
            return new Guid(userId);
        }

        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroup(GroupDto dto)
        {
            await _project.AddGroup(dto.Title, dto.Color, GetUserId());
            return Ok();
        }

        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject(ProjectDto dto)
        {
            await _project.AddProject(dto.Title, dto.Color, GetUserId(), dto.GroupId);
            return Ok();
        }
    }
}
