using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TipoUsuarioController : ControllerBase {
        TipoUsuarioRepository _tipousuarioRepository = new TipoUsuarioRepository();

        [HttpGet("tolist")]
        public async Task<ActionResult<List<TipoUsuario>>> Get() {
            try {
                List<TipoUsuario> lstTipoUsuario = await _tipousuarioRepository.Get();

                if (lstTipoUsuario == null) {
                    return NotFound();
                }

                return lstTipoUsuario;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpGet("search/{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id) {
            try {
                TipoUsuario tipoUsuario = await _tipousuarioRepository.Get(id);

                if (tipoUsuario == null) {
                    return NotFound();
                }

                return tipoUsuario;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipo_usuario) {
            try {
                return await _tipousuarioRepository.Post(tipo_usuario);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<TipoUsuario>> Put(int id, TipoUsuario tipo_usuario) {
            if (id != tipo_usuario.IdTipoUsuario) {
                return BadRequest();
            }

            try {
                return await _tipousuarioRepository.Put(tipo_usuario);
            }
            catch (DbUpdateException ex) {
                var tipo_usuarioValido = await _tipousuarioRepository.Get(id);

                if (tipo_usuarioValido == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id) {
            try {
                TipoUsuario tipo_usuarioRetornado = await _tipousuarioRepository.Get(id);

                if (tipo_usuarioRetornado == null) {
                    return NotFound();
                }

                await _tipousuarioRepository.Delete(tipo_usuarioRetornado);

                return tipo_usuarioRetornado;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}