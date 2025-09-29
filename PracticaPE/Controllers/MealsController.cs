using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaPE.Data;
using PracticaPE.Models;

namespace PracticaPE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public MealsController(AppDbContext ctx) => _ctx = ctx;

        // GET: api/meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
            => await _ctx.Meals.AsNoTracking().ToListAsync();

        // GET: api/meals/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            var meal = await _ctx.Meals.FindAsync(id);
            return meal is null ? NotFound() : meal;
        }

        // POST: api/meals
        [HttpPost]
        public async Task<ActionResult<Meal>> CreateMeal(Meal meal)
        {
            _ctx.Meals.Add(meal);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMeal), new { id = meal.Id }, meal);
        }

        // PUT: api/meals/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMeal(int id, Meal meal)
        {
            if (id != meal.Id) return BadRequest("Id de ruta y body no coinciden.");

            // Seguimiento y actualización
            _ctx.Entry(meal).State = EntityState.Modified;

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _ctx.Meals.AnyAsync(m => m.Id == id);
                if (!exists) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE: api/meals/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _ctx.Meals.FindAsync(id);
            if (meal is null) return NotFound();

            _ctx.Meals.Remove(meal);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
