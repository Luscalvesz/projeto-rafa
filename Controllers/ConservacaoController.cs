using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ConservacaoController : ControllerBase
    {
        ConservacaoRepository _conservacaoRepository = new ConservacaoRepository();

        /// <summary>
        /// Listagem de todos os estados de conservação
        /// </summary>
        /// <returns>Retorna ao usuário todos os estados de conservação</returns>
        [Authorize(Roles = "Administracao")]
        [HttpGet("tolist")]
        public async Task<ActionResult<List<Conservacao>>> Get()
        {
            try
            {
                List<Conservacao> lstConservacao = await _conservacaoRepository.Get();

                if (lstConservacao == null)
                {
                    return NotFound();
                }

                return lstConservacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Lista de uma conservação específica
        /// </summary>
        /// <param name="id"> Recebe o id da conservação informada</param>
        /// <returns> Retorna ao usuário as informações da conservação informada </returns>
        [HttpGet("search/{id}")]
        public async Task<ActionResult<Conservacao>> Get(int id)
        {
            try
            {
                Conservacao conservacao = await _conservacaoRepository.Get(id);

                if (conservacao == null)
                {
                    return NotFound();
                }

                return conservacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Incusão de nova categoria
        /// </summary>
        /// <param name="conservacao">Parâmetro recebe uma nova conservação</param>
        /// <returns> Retorna ao usuário os campos para criar uma nova conservação </returns>
        [HttpPost("insert")]
        public async Task<ActionResult<Conservacao>> Post(Conservacao conservacao)
        {
            try
            {
                return await _conservacaoRepository.Post(conservacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Alteração de uma conservação específica
        /// </summary>
        /// <param name="id"> Recebe o id específico da conservação</param>
        /// <param name="conservacao"> Recebe as informações que serão alteradas </param>
        /// <returns>Retorna ao usuário os campos para alteração de uma conservação</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Conservacao>> Put(int id, Conservacao conservacao)
        {
            if (id == conservacao.IdConservacao)
            {
                return BadRequest();
            }

            try
            {
                return await _conservacaoRepository.Put(conservacao);
            }
            catch (DbUpdateException ex)
            {
                var conservacaoValida = await _conservacaoRepository.Get(id);

                if (conservacaoValida == null)
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deleta uma condição
        /// </summary>
        /// <param name="id"> Recebe o id da conservação que será deletada</param>
        /// <returns>Retorna ao usuário a informação de exclusão</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Conservacao>> Delete(int id)
        {
            try
            {
                Conservacao conservacaoRetornado = await _conservacaoRepository.Get(id);

                if (conservacaoRetornado == null)
                {
                    return NotFound();
                }

                await _conservacaoRepository.Delete(conservacaoRetornado);

                return conservacaoRetornado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
