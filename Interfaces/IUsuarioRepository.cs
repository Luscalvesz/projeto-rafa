using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface IUsuarioRepository {
        Task<List<Usuario>> Get();
        Task<Usuario> Get(int id);
        Task<Usuario> Post(Usuario usuario);
        Task<Usuario> Put(Usuario usuario);
        Task<Usuario> Delete(Usuario usuarioRetornado);
    }
}
