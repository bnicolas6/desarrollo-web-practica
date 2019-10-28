using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Context;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Esto me permite obviar validar el modelo
    public class AutoresController : ControllerBase
    {
        private readonly IAutoresService _autoresService;

        public AutoresController(IAutoresService autoresService)
        {
            _autoresService = autoresService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            return await _autoresService.Get();
        }

        [HttpGet("id", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            AutorDTO autorDTO = await _autoresService.Get(id);
            if (autorDTO == null)
                return NotFound();

            return autorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            await _autoresService.Post(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put([FromBody] Autor autor, int id)
        {
            if (id != autor.Id)
                return BadRequest();

            await _autoresService.Put(autor);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            Autor autor = await _autoresService.Delete(id);

            if (autor == null)
                return NotFound();

            return autor;
        }
    }
}
