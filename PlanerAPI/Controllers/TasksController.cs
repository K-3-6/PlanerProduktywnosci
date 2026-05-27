using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanerAPI.Data;

namespace PlanerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanerAPI.Models.TaskItem item)
        {
            if (item == null) return BadRequest();

            _context.Tasks.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            if (taskItem == null) return NotFound();

            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}