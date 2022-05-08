using Application.Services.AuthService.Interfaces;
using Domain;
using Domain.Models;
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

        [HttpGet("GetProjetcsByGroups")]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> GetProjects()
        {
            var res = await _project.GetProjetcsByGroups(GetUserId());
            return Ok(res);
        }

        [HttpGet("GetProjetcsWithoutGroup")]
        public async Task<ActionResult<List<ProjectViewModel>>> GetProjetcsWithoutGroup()
        {
            var res = await _project.GetProjetcsWithoutGroup(GetUserId());
            return Ok(res);
        }

        [HttpGet("GetGroups")]
        public async Task<ActionResult<List<Group>>> GetGroups()
        {
            var res = await _project.GetGroups(GetUserId());
            return Ok(res);
        }
    }
}
