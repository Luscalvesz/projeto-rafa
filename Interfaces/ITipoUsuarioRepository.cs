using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface ITipoUsuarioRepository {
        Task<List<TipoUsuario>> Get();
        Task<TipoUsuario> Get(int id);
        Task<TipoUsuario> Post(TipoUsuario tipo_usuario);
        Task<TipoUsuario> Put(TipoUsuario tipo_usuario);
        Task<TipoUsuario> Delete(TipoUsuario tipo_usuarioRetornado);
    }
}
