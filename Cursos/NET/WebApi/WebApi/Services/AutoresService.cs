using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Context;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IAutoresService
    {
        Task<List<Autor>> Get();
        Task<AutorDTO> Get(int id);
        Task<Autor> Post(Autor autor);
        Task Put(Autor autor);
        Task<Autor> Delete(int id);
    }
    public class AutoresService : IAutoresService
    {
        private readonly ApplicationDbContext _db;
        public readonly IMapper _mapper;
        public AutoresService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<Autor>> Get()
        {
            return await _db.Autores.ToListAsync();
        }

        public async Task<AutorDTO> Get(int id)
        {
            Autor autor = await _db.Autores.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<AutorDTO>(autor);
        }

        public async Task<Autor> Post(Autor autor)
        {
            _db.Add(autor);
            await _db.SaveChangesAsync();
            return autor;
        }

        public async Task Put(Autor autor)
        {
            _db.Entry(autor).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Autor> Delete(int id)
        {
            Autor autor = await _db.Autores.FirstOrDefaultAsync(x => x.Id == id);
            
            if(autor != null)
            {
                _db.Autores.Remove(autor);
                _db.SaveChanges();    
            }

            return autor;
        }
    }
}
