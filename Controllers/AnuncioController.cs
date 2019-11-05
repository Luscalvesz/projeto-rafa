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
    public class AnuncioController : ControllerBase {
        AnuncioRepository _anuncioRepository = new AnuncioRepository();

        /// <summary>
        /// Listagem de todos os anúncios
        /// </summary>
        /// 
        /// <returns>Retorna ao usuário uma lista com todos anúncios</returns>
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Anuncio>>> Get() {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.Listar();

                if (lstAnuncio == null) {
                    return NotFound();
                }

                foreach (var item in lstAnuncio) {
                    item.FkIdProdutoNavigation.Anuncio = null;
                    item.FkIdConservacaoNavigation.Anuncio = null;
                }

                return lstAnuncio;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Lista de anúncio específico
        /// </summary>
        /// <param name="id">Recebe o id do anúncio informada</param>
        /// <returns>Retorna ao usuário as informações do anúncio informado</returns>
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Anuncio>> Get(int id) {
            try {
                Anuncio anuncio = await _anuncioRepository.BuscaPorId(id);

                if (anuncio == null) {
                    return NotFound();
                }

                return anuncio;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de preço
        /// </summary>
        /// <param name="preco">Parâmetro recebe o valor de preço</param>
        /// <returns>Retorna ao usuário lista de anúncios com o preço informado</returns>
        [HttpGet("search/filter/price/{preco}")]
        public async Task<ActionResult<List<Anuncio>>> Get(decimal preco) {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.BuscaPorPreco(preco);

                if (lstAnuncio == null) {
                    return NotFound();
                }

                return lstAnuncio;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de conservação do produto e fabricante
        /// </summary>
        /// <param name="fabricante">Recebe o fabricante do produto informado</param>
        /// <param name="conservacaoRecebida">Recebe o estado de conservação do produto informado</param>
        /// <returns>Retorna ao usuário os anúncios com as condições de produto e estado de conservação informados</returns>
        [HttpGet("search/filter/manufacturerEconservation/{fabricante}/{conservacaoRecebida}")]
        public async Task<ActionResult<List<Anuncio>>> Get(string fabricante, string conservacaoRecebida) {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.BuscaFabricanteConservacao(fabricante, conservacaoRecebida);

                if (lstAnuncio == null) {
                    return NotFound();
                }

                return lstAnuncio;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de busca
        /// </summary>
        /// <param name="campoDesejado">Recebe uma informação como titulo ou palavras que contenham na descrição</param>
        /// <returns>Retorna ao usuário os anúncios que possuem aquela palavra ou texto</returns>
        [HttpGet("search/filter/field/{campoDesejado}")]
        public async Task<ActionResult<List<Anuncio>>> Get(string campoDesejado) {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.BuscaPorCampo(campoDesejado);

                if (lstAnuncio == null) {
                    return NotFound();
                }

                return lstAnuncio;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Incusão de novo anúncio
        /// </summary>
        /// <param name="anuncio">Parâmetro recebe um novo anúncio</param>
        /// <returns>Retorna ao usuário os campos para criar um novo anúncio</returns>
        [HttpPost("insert")]
        public async Task<ActionResult<Anuncio>> Post(Anuncio anuncio) {
            try {
                return await _anuncioRepository.Post(anuncio);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Alteração de anúncio específico
        /// </summary>
        /// <param name="id">Recebe o id específico do anúncio </param>
        /// <param name="anuncio"> Recebe as informações que serão alteradas </param>
        /// <returns>Retorna ao usuário os campos para alteração de um anúncio</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Anuncio>> Put(int id, Anuncio anuncio) {
            if (id != anuncio.IdAnuncio) {
                return BadRequest();
            }

            try {
                return await _anuncioRepository.Put(anuncio);
            }
            catch (DbUpdateException ex) {
                var anuncioValida = _anuncioRepository.BuscaPorId(id);

                if (anuncioValida == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deleta um anúncio
        /// </summary>
        /// <param name="id">Recebe o id do anúncio que será deletado</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Anuncio>> Delete(int id) {
            try {
                Anuncio anuncioRetornado = await _anuncioRepository.BuscaPorId(id);

                if (anuncioRetornado == null) {
                    return NotFound();
                }

                await _anuncioRepository.Delete(anuncioRetornado);

                return anuncioRetornado;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
