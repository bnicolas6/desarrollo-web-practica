using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Context;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface ILibrosService
    {
        Task<List<Libro>> Get();
        Task<Libro> Get(int id);
        Task<Libro> Post(Libro libro);
        Task Put(Libro libro);
        Task<Libro> Delete(int id);

    }
    public class LibrosService : ILibrosService
    {
        private readonly ApplicationDbContext _db;

        public LibrosService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Libro>> Get()
        {
            return await _db.Libros.ToListAsync();
        }

        public async Task<Libro> Get(int id)
        {
            return await _db.Libros.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Libro> Post(Libro libro)
        {
            _db.Add(libro);
            await _db.SaveChangesAsync();
            return libro;
        }

        public async Task Put(Libro libro)
        {
            _db.Entry(libro).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Libro> Delete(int id)
        {
            Libro libro = await _db.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (libro != null)
            {
                _db.Libros.Remove(libro);
                await _db.SaveChangesAsync();
            }

            return libro;
        }
    }
}
