using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemModel>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        // GET: api/ToDoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemModel>> GetToDoItemModel(int id)
        {
            var toDoItemModel = await _context.ToDoItems.FindAsync(id);

            if (toDoItemModel == null)
            {
                return NotFound();
            }

            return toDoItemModel;
        }

        // PUT: api/ToDoItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItemModel(int id, ToDoItemModel toDoItemModel)
        {
            if (id != toDoItemModel.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(toDoItemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItemModel>> PostToDoItemModel(ToDoItemModel toDoItemModel)
        {
            _context.ToDoItems.Add(toDoItemModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoItemModel", new { id = toDoItemModel.ItemId }, toDoItemModel);
        }

        // DELETE: api/ToDoItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItemModel(int id)
        {
            var toDoItemModel = await _context.ToDoItems.FindAsync(id);
            if (toDoItemModel == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemModelExists(int id)
        {
            return _context.ToDoItems.Any(e => e.ItemId == id);
        }
    }
}
