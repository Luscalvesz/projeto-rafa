using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;

namespace API.Repositories {
    public class InteresseRepository : IInteresseRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<Interesse>> Get() {
            return await _context.Interesse.Include(a => a.FkIdAnuncioNavigation).Include(u => u.FkIdUsuarioNavigation).ToListAsync();
        }

        public async Task<Interesse> Get(int id) {
            return await _context.Interesse.Include(a => a.FkIdAnuncioNavigation).Include(u => u.FkIdUsuarioNavigation).FirstOrDefaultAsync(x => x.IdInteresse == id);
        }

        public async Task<Interesse> Post(Interesse interesse) {
            await _context.Interesse.AddAsync(interesse);
            await _context.SaveChangesAsync();

            return interesse;
        }

        public async Task<Interesse> Put(Interesse interesse) {
            _context.Entry(interesse).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return interesse;
        }

        public async Task<Interesse> Delete(Interesse interesseRetornado) {
            _context.Interesse.Remove(interesseRetornado);
            await _context.SaveChangesAsync();

            return interesseRetornado;
        }
    }
}
