using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories {
    public class ImagemRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<Imagem>> Get() {
            return await _context.Imagem.ToListAsync();
        }

        public async Task<Imagem> Get(int id) {
            return await _context.Imagem.FindAsync(id);
        }

        public async Task<Imagem> Post(Imagem imagem) {
            await _context.Imagem.AddAsync(imagem);
            await _context.SaveChangesAsync();

            return imagem;
        }

        public async Task<Imagem> Put(Imagem imagem) {
            _context.Entry(imagem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return imagem;
        }

        public async Task<Imagem> Delete(Imagem imagemRetornada) {
            _context.Imagem.Remove(imagemRetornada);
            await _context.SaveChangesAsync();

            return imagemRetornada;
        }
    }
}
