using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;

namespace API.Repositories {
    public class ConservacaoRepository : IConservacaoRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<Conservacao>> Get() {
            return await _context.Conservacao.ToListAsync();
        }

        public async Task<Conservacao> Get(int id) {
            return await _context.Conservacao.FindAsync(id);
        }

        public async Task<Conservacao> Post(Conservacao conservacao) {
            await _context.Conservacao.AddAsync(conservacao);
            await _context.SaveChangesAsync();

            return conservacao;
        }

        public async Task<Conservacao> Put(Conservacao conservacao) {
            _context.Entry(conservacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return conservacao;
        }

        public async Task<Conservacao> Delete(Conservacao conservacaoRetornada) {
            _context.Conservacao.Remove(conservacaoRetornada);
            await _context.SaveChangesAsync();

            return conservacaoRetornada;
        }
    }
}
