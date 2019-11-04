using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;

namespace API.Repositories {
    public class FabricanteRepository : IFabricanteRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<Fabricante>> Get() {
            return await _context.Fabricante.ToListAsync();
        }

        public async Task<Fabricante> Get(int id) {
            return await _context.Fabricante.FindAsync(id);
        }

        public async Task<Fabricante> Post(Fabricante fabricante) {
            await _context.Fabricante.AddAsync(fabricante);
            await _context.SaveChangesAsync();

            return fabricante;
        }

        public async Task<Fabricante> Put(Fabricante fabricante) {
            _context.Entry(fabricante).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return fabricante;
        }

        public async Task<Fabricante> Delete(Fabricante fabricanteRetornado) {
            _context.Fabricante.Remove(fabricanteRetornado);
            await _context.SaveChangesAsync();

            return fabricanteRetornado;
        }
    }
}
