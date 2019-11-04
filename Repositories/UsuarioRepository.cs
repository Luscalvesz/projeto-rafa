using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;

namespace API.Repositories {
    public class UsuarioRepository : IUsuarioRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<Usuario>> Get() {
            return await _context.Usuario.Include(tu => tu.FkIdTipoUsuarioNavigation).ToListAsync();
        }

        public async Task<Usuario> Get(int id) {
            return await _context.Usuario.Include(tu => tu.FkIdTipoUsuarioNavigation).FirstOrDefaultAsync(x => x.IdUsuario == id);
        }

        public async Task<Usuario> Post(Usuario usuario) {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> Put(Usuario usuario) {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> Delete(Usuario usuarioRetornado) {
            _context.Usuario.Remove(usuarioRetornado);
            await _context.SaveChangesAsync();

            return usuarioRetornado;
        }
    }
}
