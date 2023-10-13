
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace SuperheroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await context.SupeHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await context.SupeHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            context.SupeHeroes.Add(hero);
            await context.SaveChangesAsync();

            return Ok(await context.SupeHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {

            var dbHero = await context.SupeHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await context.SaveChangesAsync();

            return Ok(await context.SupeHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await context.SupeHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            context.SupeHeroes.Remove(dbHero);
            await context.SaveChangesAsync();
            return Ok(await context.SupeHeroes.ToListAsync());
        }
    }
}
