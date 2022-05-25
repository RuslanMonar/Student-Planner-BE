using Application.Services.AuthService.Dto;
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
            var token = handler?.ReadJwtToken(accessToken);
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

        [HttpGet("GetAllProjects")]
        public async Task<ActionResult<List<ProjectViewModel>>> GetAllProjects()
        {
            var res = await _project.GetAllProjects(GetUserId());
            return Ok(res);
        }


        [HttpGet("GetGroups")]
        public async Task<ActionResult<List<Group>>> GetGroups()
        {
            var res = await _project.GetGroups(GetUserId());
            return Ok(res);
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult<List<Group>>> AddTask(TaskDto dto)
        {
            await _project.AddTask(dto, GetUserId());
            return Ok();
        }


        [HttpPost("GetAllTasks")]
        public async Task<ActionResult<List<TasksViewModel>>> GetAllTasks(GetAllTasksDto dto)
        {
           var result =  await _project.GetAllTasks(dto, GetUserId());
            return Ok(result);
        }

        [HttpGet("GetTasksById/{id}")]
        public async Task<ActionResult<List<TasksViewModel>>> GetTasksById(int id)
        {
            var result = await _project.GetTasksById(GetUserId(), id);
            return Ok(result);
        }

        [HttpPost("EditTask")]
        public async Task<ActionResult> EditTask(AddTaskDto dto)
        {
            await _project.EditTask(dto);
            return Ok();
        }

        [HttpPost("StartTask")]
        public async Task<ActionResult> StartTask(TrackTimeDto dto)
        {
            await _project.StartTask(dto);
            return Ok();
        }

        [HttpPost("EndTask")]
        public async Task<ActionResult> EndTask(TrackTimeDto dto)
        {
            await _project.EndTask(dto);
            return Ok();
        }

        [HttpGet("GetTasksStatic")]
        public async Task<ActionResult<List<TasksStatisticViewModel>>> GetTasksStatic()
        {
            var result = await _project.GetTasksStatic(GetUserId());
            return Ok(result);
        }

        [HttpPost("TaskCompleted")]
        public async Task<ActionResult> TaskCompleted(TaskCompleteDto dto)
        {
            await _project.TaskCompleted(dto, GetUserId());
            return Ok();
        }
        [HttpPost("DeleteTask")]
        public async Task<ActionResult> DeleteTask(DeleteTaskDto dto)
        {
            await _project.DeleteTask(dto);
            return Ok();
        }
    }
}
