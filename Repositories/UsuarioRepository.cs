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

        /// <summary>
        /// Método privado que valida se um usuário existe no nosso banco de dados
        /// </summary>
        /// <param name="login">Objeto do tipo usuário</param>
        /// <returns>Objeto do tipo usuário</returns>
        public Usuario AutenticarLogin(UsuarioLogin login) {
            Usuario usuario = _context.Usuario
                .Include(tipo => tipo.FkIdTipoUsuarioNavigation)
                .FirstOrDefault(user => user.EmailUsuario == login.EmailUsuario && user.SenhaUsuario == login.SenhaUsuario);

            return usuario;
        }

        public async Task<List<Usuario>> Listar() {
            return await _context.Usuario.Include(tu => tu.FkIdTipoUsuarioNavigation).ToListAsync();
        }

        public async Task<Usuario> BuscarPorId(int id) {
            return await _context.Usuario.Include(tu => tu.FkIdTipoUsuarioNavigation).FirstOrDefaultAsync(x => x.IdUsuario == id);
        }

        public async Task<Usuario> Cadastrar(Usuario usuario) {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> VerificarEmail(Usuario login) {
            Usuario verif = await _context.Usuario.FirstOrDefaultAsync(user => user.EmailUsuario == login.EmailUsuario);

            return verif;
        }

        public async Task<Usuario> Atualizar(Usuario usuario) {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> Deletar(Usuario usuarioRetornado) {
            _context.Usuario.Remove(usuarioRetornado);
            await _context.SaveChangesAsync();

            return usuarioRetornado;
        }
    }
}
