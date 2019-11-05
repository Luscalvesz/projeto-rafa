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
    public class InteresseController : ControllerBase {
        InteresseRepository _interesseRepository = new InteresseRepository();
        /// <summary>
        /// Listagem de todos os interesses
        /// </summary>
        /// <returns>Retorna ao usuário todos os interesses</returns>
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Interesse>>> Get() {
            try {
                List<Interesse> lstInteresse = await _interesseRepository.Get();

                if (lstInteresse == null) {
                    return NotFound();
                }

                foreach (var item in lstInteresse) {
                    item.FkIdUsuarioNavigation.Interesse = null;
                    item.FkIdAnuncioNavigation.Interesse = null;
                }

                return lstInteresse;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Lista de um interesse específico
        /// </summary>
        /// <param name="id">Recebe o id do interesse informado</param>
        /// <returns>Retorna ao usuário as informações de interesse informado</returns>
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Interesse>> Get(int id) {
            try {
                Interesse interesse = await _interesseRepository.Get(id);

                if (interesse == null) {
                    return NotFound();
                }

                return interesse;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Incusão de um novo interesse
        /// </summary>
        /// <param name="interesse">Parâmetro recebe um novo interesse</param>
        /// <returns>Retorna ao usuário os campos para criar um novo interesse</returns>
        [HttpPost("insert")]
        public async Task<ActionResult<Interesse>> Post(Interesse interesse) {
            try {
                return await _interesseRepository.Post(interesse);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Alteração de um interesse específico
        /// </summary>
        /// <param name="id">Recebe o id específico do interesse</param>
        /// <param name="interesse">Recebe as informações que serão alteradas</param>
        /// <returns>Retorna ao usuário os campos para alteração de um interesse</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Interesse>> Put(int id, Interesse interesse) {
            if (id != interesse.IdInteresse) {
                return BadRequest();
            }

            try {
                return await _interesseRepository.Put(interesse);
            }
            catch (DbUpdateException ex) {
                var interesseValido = await _interesseRepository.Get(id);

                if (interesseValido == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }

            }
        }
        /// <summary>
        /// Deleta um interesse
        /// </summary>
        /// <param name="id">Recebe o id de interesse que será deletado</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Interesse>> Delete(int id) {
            try {
                Interesse interesseRetornado = await _interesseRepository.Get(id);

                if (interesseRetornado == null) {
                    return NotFound();
                }

                await _interesseRepository.Delete(interesseRetornado);

                return interesseRetornado;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
