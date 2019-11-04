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

        [HttpPost("insert")]
        public async Task<ActionResult<Produto>> Post(Produto produto) {
            try {
                return await _produtoRepository.Post(produto);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

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
