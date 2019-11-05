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
    public class ConservacaoController : ControllerBase {
        ConservacaoRepository _conservacaoRepository = new ConservacaoRepository();

        [Authorize(Roles = "Administracao")]
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Conservacao>>> Get() {
            try {
                List<Conservacao> lstConservacao = await _conservacaoRepository.Get();

                if (lstConservacao == null) {
                    return NotFound();
                }

                return lstConservacao;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpGet("search/{id}")]
        public async Task<ActionResult<Conservacao>> Get(int id) {
            try {
                Conservacao conservacao = await _conservacaoRepository.Get(id);

                if (conservacao == null) {
                    return NotFound();
                }

                return conservacao;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Conservacao>> Post(Conservacao conservacao) {
            try {
                return await _conservacaoRepository.Post(conservacao);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Conservacao>> Put(int id, Conservacao conservacao) {
            if (id == conservacao.IdConservacao) {
                return BadRequest();
            }

            try {
                return await _conservacaoRepository.Put(conservacao);
            }
            catch (DbUpdateException ex) {
                var conservacaoValida = await _conservacaoRepository.Get(id);

                if (conservacaoValida == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Conservacao>> Delete(int id) {
            try {
                Conservacao conservacaoRetornado = await _conservacaoRepository.Get(id);

                if (conservacaoRetornado == null) {
                    return NotFound();
                }

                await _conservacaoRepository.Delete(conservacaoRetornado);

                return conservacaoRetornado;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
