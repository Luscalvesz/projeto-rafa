using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ImagemController : ControllerBase {
        ImagemRepository _imagemRepository = new ImagemRepository();

        /// <summary>
        /// Lista de todas as imagens
        /// </summary>
        /// <returns>Retorna as imagens do produto</returns>
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Imagem>>> Get() {
            try {
                List<Imagem> lstImagem = await _imagemRepository.Get();
                
                if (lstImagem == null) {
                    NotFound();
                }

                return lstImagem;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Retorno de imagem específica
        /// </summary>
        /// <param name="id">Recebe o id da imagem </param>
        /// <returns>Retorna a imagem referente ao id informado</returns>
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Imagem>> Get(int id) {
            try {
                Imagem imagem = await _imagemRepository.Get(id);

                if (imagem == null) {
                    return NotFound();
                }

                return imagem;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        /// <summary>
        /// Inclusão de imagem
        /// </summary>
        /// <param name="imagem">Parâmetro que rececbe uma nova imagem</param>
        /// <returns>Retorna os campos para a inserção de imagem</returns>
        [HttpPost("insert")]
        public async Task<ActionResult<Imagem>> Post(Imagem imagem) {
            try {
                return await _imagemRepository.Post(imagem);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        /// <summary>
        /// Altera imagem específica
        /// </summary>
        /// <param name="id">Recebe o id específico de imagem</param>
        /// <param name="imagem">Recebe a imagem a ser alterada</param>
        /// <returns>Retorna o campo para alteração de imagem</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Imagem>> Put(int id, Imagem imagem) {
            if (id == imagem.IdImagem) {
                return BadRequest();
            }

            try {
                return await _imagemRepository.Put(imagem);
            }
            catch (DbUpdateException ex) {
                var imagemValida = await _imagemRepository.Get(id);

                if (imagemValida == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deleta uma imagem
        /// </summary>
        /// <param name="id">Recebe o id da imagem a ser deletada</param>
        /// <returns>Retorna a imagem deletada</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Imagem>> Delete(int id) {
            try {
                Imagem imagemRetornada = await _imagemRepository.Get(id);

                if (imagemRetornada == null) {
                    return NotFound();
                }

                await _imagemRepository.Delete(imagemRetornada);

                return imagemRetornada;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}