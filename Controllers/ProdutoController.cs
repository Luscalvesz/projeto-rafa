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
    public class ProdutoController : ControllerBase {
        ProdutoRepository _produtoRepository = new ProdutoRepository();

        /// <summary>
        /// Listagem de todos os produtos
        /// </summary>
        /// <returns> Retorna ao usuário todos os produtos </returns>
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Produto>>> Get() {
            try {
                List<Produto> lstProduto = await _produtoRepository.Get();

                if (lstProduto == null) {
                    return NotFound();
                }

                foreach (var item in lstProduto) {
                    item.FkIdFabricanteNavigation.Produto = null;
                    item.FkIdUsuarioNavigation.Produto = null;
                }

                return lstProduto;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Lista de um produto específico
        /// </summary>
        /// <param name="id">Recebe o id de um produto informado</param>
        /// <returns>Retorna ao usuário as informações do produto informado</returns>
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Produto>> Get(int id) {
            try {
                Produto produto = await _produtoRepository.Get(id);

                if (produto == null) {
                    return NotFound();
                }

                return produto;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Incusão de novo produto
        /// </summary>
        /// <param name="produto">Parâmetro recebe um novo produto </param>
        /// <returns> Retorna ao usuário os campos para criar um novo produto</returns>
        [HttpPost("insert")]
        public async Task<ActionResult<Produto>> Post(Produto produto) {
            try {
                return await _produtoRepository.Post(produto);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Alteração de um produto específico
        /// </summary>
        /// <param name="id">Recebe o id específico do produto</param>
        /// <param name="produto">Recebe as informações que serão alteradas</param>
        /// <returns>Retorna ao usuário os campos para alteração de um produto</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Produto>> Put(int id, Produto produto) {
            if (id != produto.IdProduto) {
                return BadRequest();
            }

            try {
                return await _produtoRepository.Put(produto);
            }
            catch (DbUpdateException ex) {
                var produtoValido = await _produtoRepository.Get(id);

                if (produtoValido == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Deleta um produto
        /// </summary>
        /// <param name="id">Recebe o id do produto que será deletado</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Produto>> Delete(int id) {
            try {
                Produto produtoRetornado = await _produtoRepository.Get(id);

                if (produtoRetornado == null) {
                    return NotFound();
                }

                await _produtoRepository.Delete(produtoRetornado);

                return produtoRetornado;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
