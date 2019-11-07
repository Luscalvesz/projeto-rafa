using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AnuncioController : ControllerBase {
        AnuncioRepository _anuncioRepository = new AnuncioRepository();

        /// <summary>
        /// Listagem de todos os anúncios
        /// </summary>
        /// <returns>Retorna ao usuário uma lista com todos anúncios</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Anuncio>>> ListarAnuncio() {
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
        public async Task<ActionResult<Anuncio>> BuscaId(int id) {
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
        public async Task<ActionResult<List<Anuncio>>> ListaPorPreco(decimal preco) {
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
        /// Filtro de preço pré setado
        /// </summary>
        /// <param name="precoMin">Parâmetro recebe o valor de preço mínimo</param>
        /// <param name="precoMax">Parâmetro recebe o valor de preço máximo</param>
        /// <returns>Retorna ao usuário lista de anúncios entre os preços informados</returns>
        [HttpGet("search/filter/price/{precoMin}/{precoMax}")]
        public async Task<ActionResult<List<Anuncio>>> ListaPrecoSet(decimal precoMin, decimal precoMax) {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.BuscaPorPrecoSet(precoMin, precoMax);

                if (lstAnuncio == null) {
                    return NotFound();
                }

                foreach (var item in lstAnuncio) {
                    item.FkIdConservacaoNavigation.Anuncio = null;
                    item.FkIdProdutoNavigation.FkIdFabricanteNavigation.Produto = null;
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
        public async Task<ActionResult<List<Anuncio>>> ListaFabricanteConservacao(string fabricante, string conservacaoRecebida) {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.BuscaFabricanteConservacao(fabricante, conservacaoRecebida);

                if (lstAnuncio == null) {
                    return NotFound();
                }

                foreach (var item in lstAnuncio) {
                    item.FkIdConservacaoNavigation.Anuncio = null;
                    item.FkIdProdutoNavigation.Anuncio = null;
                    item.FkIdProdutoNavigation.FkIdFabricanteNavigation.Produto = null;
                }

                return lstAnuncio;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Filtro de Data
        /// </summary>
        /// <returns>Retorna ao usuário os anúncios ordenado pela data</returns>
        [HttpGet("search/filter/date")]
        public async Task<ActionResult<List<Anuncio>>> OrdenarPorData() {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.OrdenarPorData();

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
        public async Task<ActionResult<List<Anuncio>>> BuscaCampo(string campoDesejado) {
            try {
                List<Anuncio> lstAnuncio = await _anuncioRepository.BuscaPorCampo(campoDesejado);

                if (lstAnuncio == null) {
                    return NotFound();
                }
                else if (lstAnuncio.Count == 0) {
                    return BadRequest();
                }

                foreach (var item in lstAnuncio) {
                    item.FkIdProdutoNavigation.FkIdFichaNavigation.Produto = null;
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
        public async Task<ActionResult<Anuncio>> CadastrarAnuncio(Anuncio anuncio) {
            try {
                return await _anuncioRepository.Cadastrar(anuncio);
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
        public async Task<ActionResult<Anuncio>> AtualizarAnuncio(int id, Anuncio anuncio) {
            if (id != anuncio.IdAnuncio) {
                return BadRequest();
            }

            try {
                return await _anuncioRepository.Atualizar(anuncio);
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
        public async Task<ActionResult<Anuncio>> DeletarAnuncio(int id) {
            try {
                Anuncio anuncioRetornado = await _anuncioRepository.BuscaPorId(id);

                if (anuncioRetornado == null) {
                    return NotFound();
                }

                await _anuncioRepository.Deletar(anuncioRetornado);

                return anuncioRetornado;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
