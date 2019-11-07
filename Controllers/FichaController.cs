using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class FichaController : ControllerBase {
        FichaRepository _fichaRepository = new FichaRepository();

        /// <summary>
        /// Lista de ficha técnica 
        /// </summary>
        /// <returns> Retorna ao usuário uma lista com a ficha técnica do produto</returns>
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Ficha>>> Get() {
            try {
                List<Ficha> lstFicha = await _fichaRepository.Get();

                if (lstFicha == null) {
                    return NotFound();
                }

                return lstFicha;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        /// <summary>
        /// Lista de uma ficha técnica específica
        /// </summary>
        /// <param name="id"> Recebe o id da ficha técnica do produto que informado</param>
        /// <returns>Retorna ao usuário a ficha técnica do produrto informado </returns>
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Ficha>> Get(int id) {
            try {
                Ficha ficha = await _fichaRepository.Get(id);

                if (ficha == null) {
                    return NotFound();
                }

                return ficha;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        /// <summary>
        /// Inclusão de nova ficha técnica de um produto 
        /// </summary>
        /// <param name="ficha">Parâmetro recebe uma nova ficha técnica de um produto</param>
        /// <returns>Retorna a ficha técnica inserida</returns>
        [HttpPost("insert")]
        public async Task<ActionResult<Ficha>> Post(Ficha ficha) {
            try {
                return await _fichaRepository.Post(ficha);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Alteração das informações de uma ficha técnica específica
        /// </summary>
        /// <param name="id">Recebe o id específico de uma ficha técnica</param>
        /// <param name="ficha">Recebe as informações que serão alteradas</param>
        /// <returns>Retorna ao usuário os campos para alteração de uma ficha técnica</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Ficha>> Put(int id, Ficha ficha) {
            if (id == ficha.IdFicha) {
                return BadRequest();
            }

            try {
                return await _fichaRepository.Put(ficha);
            }
            catch (DbUpdateException ex) {
                var fichaValida = _fichaRepository.Get(id);

                if (fichaValida == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deleta  uma ficha técnica
        /// </summary>
        /// <param name="id"> Recebe o id da ficha técnica que será deletada</param>
        /// <returns>Retorna para o usuário a informação de exclusão</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Ficha>> Delete(int id) {
            try {
                Ficha fichaRetornada = await _fichaRepository.Get(id);

                if (fichaRetornada == null) {
                    return NotFound();
                }

                await _fichaRepository.Delete(fichaRetornada);

                return fichaRetornada;
            }
            catch (Exception ex) {

                throw ex;
            }
        }
    }
}