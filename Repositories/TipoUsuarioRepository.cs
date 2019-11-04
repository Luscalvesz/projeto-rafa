using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;

namespace API.Repositories {
    public class TipoUsuarioRepository : ITipoUsuarioRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<TipoUsuario>> Get() {
            return await _context.TipoUsuario.ToListAsync();
        }

        public async Task<TipoUsuario> Get(int id) {
            return await _context.TipoUsuario.FindAsync(id);
        }

        public async Task<TipoUsuario> Post(TipoUsuario tipo_usuario) {
            await _context.TipoUsuario.AddAsync(tipo_usuario);
            await _context.SaveChangesAsync();

            return tipo_usuario;
        }

        public async Task<TipoUsuario> Put(TipoUsuario tipo_usuario) {
            _context.Entry(tipo_usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return tipo_usuario;
        }

        public async Task<TipoUsuario> Delete(TipoUsuario tipo_usuarioRetornado) {
            _context.TipoUsuario.Remove(tipo_usuarioRetornado);
            await _context.SaveChangesAsync();

            return tipo_usuarioRetornado;
        }
    }
}