using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase {
        UsuarioRepository _usuarioRepository = new UsuarioRepository();
        /// <summary>
        /// Listagem de todos os usuários
        /// </summary>
        /// <returns>Retorna ao usuário todos os usuários</returns>
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Usuario>>> Get() {
            try {
                List<Usuario> lstUsuario = await _usuarioRepository.Get();

                if (lstUsuario == null) {
                    return NotFound();
                }

                foreach (var item in lstUsuario) {
                    item.FkIdTipoUsuarioNavigation.Usuario = null;
                }

                return lstUsuario;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Lista de um usuário específico
        /// </summary>
        /// <param name="id">Recebe o id do usuário informado</param>
        /// <returns>Retorna ao usuário as informações do usuário informado</returns>
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Usuario>> Get(int id) {
            try {
                Usuario usuario = await _usuarioRepository.Get(id);

                if (usuario == null) {
                    return NotFound();
                }

                return usuario;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Incusão de um novo usuário
        /// </summary>
        /// <param name="usuario">Parâmetro recebe um novo usuário</param>
        /// <returns>Retorna ao usuário os campos para criar um novo usuário</returns>
        [HttpPost("insert")]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario) {
            try {
                return await _usuarioRepository.Post(usuario);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Alteração de um usuário específico
        /// </summary>
        /// <param name="id">Recebe o id específico do usuário</param>
        /// <param name="usuario">Recebe as informações que serão alteradas</param>
        /// <returns>Retorna ao usuário os campos para alteração de um usuário</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Usuario>> Put(int id, Usuario usuario) {
            if (id != usuario.IdUsuario) {
                return BadRequest();
            }

            try {
                return await _usuarioRepository.Put(usuario);
            }
            catch (DbUpdateException ex) {
                var usuarioValido = await _usuarioRepository.Get(id);

                if (usuarioValido == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Deleta um usuário 
        /// </summary>
        /// <param name="id">Recebe o id do usuário que será deletado</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id) {
            try {
                Usuario usuarioRetornado = await _usuarioRepository.Get(id);

                if (usuarioRetornado == null) {
                    return NotFound();
                }

                await _usuarioRepository.Delete(usuarioRetornado);

                return usuarioRetornado;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
