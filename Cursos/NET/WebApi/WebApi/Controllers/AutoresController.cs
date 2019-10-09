using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Context;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AutoresController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            return await _db.Autores.ToListAsync();
        }

        [HttpGet("id", Name = "ObtenerAutor")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            Autor autor = await _db.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
                return NotFound();

            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            _db.Add(autor);
            await _db.SaveChangesAsync();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put([FromBody] Autor autor, int id)
        {
            if (id != autor.Id)
                return BadRequest();

            _db.Entry(autor).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            Autor autor = await _db.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
                return NotFound();

            _db.Autores.Remove(autor);
            _db.SaveChanges();
            return autor;
        }
    }
}
