using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.BLL.Repositories.Interfaces;
using To_Do_List.DAL.Models;
using To_Do_List.Dto;

namespace To_Do_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IGenericRepository<Tasks> _repository;
        private readonly IMapper _mapper;

        public TaskController(IGenericRepository<Tasks> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _repository.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return NotFound("Task not found.");

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddAssessment([FromBody] TaskDto taskdto)
        {

            var mappedTask = _mapper.Map<TaskDto, Tasks>(taskdto);

            if (mappedTask == null)
                return BadRequest("Task is null.");

            await _repository.AddAsync(mappedTask);
            return CreatedAtAction(nameof(GetById), new { id = mappedTask.Id }, mappedTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssessment(int id, [FromBody] TaskDto taskdto)
        {
            if (taskdto == null)
                return BadRequest("Assessment data is null.");

            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound("Assessment not found.");

            _mapper.Map(taskdto, existingTask);

            await _repository.UpdateAsync(existingTask);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssessment(int id)
        {
            var assessment = await _repository.GetByIdAsync(id);
            if (assessment == null)
                return NotFound("Assessment not found.");

            await _repository.DeleteAsync(assessment);
            return NoContent();
        }

    }
}
