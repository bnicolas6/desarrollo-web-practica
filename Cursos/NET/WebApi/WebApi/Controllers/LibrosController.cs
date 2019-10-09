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
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public LibrosController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> Get()
        {
            return await _db.Libros.ToListAsync();
        }

        [HttpGet("id", Name = "ObtenerLibro")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            Libro libro = await _db.Libros.FirstOrDefaultAsync(x => x.Id == id);
            if (libro == null)
                return NotFound();

            return libro;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Libro libro)
        {
            _db.Add(libro);
            await _db.SaveChangesAsync();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put([FromBody] Libro libro, int id)
        {
            if (libro.Id != id)
                return BadRequest();

            _db.Entry(libro).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Libro>> Delete(int id)
        {
            Libro libro = await _db.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null)
                return NotFound();

            _db.Libros.Remove(libro);
            await _db.SaveChangesAsync();
            return libro;
        }
    }
}
