using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface IUsuarioRepository {
        Usuario AutenticarLogin(UsuarioLogin login);
        Task<List<Usuario>> Listar();
        Task<Usuario> BuscarPorId(int id);
        Task<Usuario> Cadastrar(Usuario usuario);
        Task<Usuario> VerificarEmail(Usuario login);
        Task<Usuario> Atualizar(Usuario usuario);
        Task<Usuario> Deletar(Usuario usuarioRetornado);
    }
}
