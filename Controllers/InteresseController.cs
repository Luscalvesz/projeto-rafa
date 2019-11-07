using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Diagnostics;

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
                var x = await _interesseRepository.Put(interesse);

                string titulo = "Parabéns " + interesse.FkIdUsuarioNavigation.NomeUsuario + " você foi selecionado - Você acaba de adquirir " + interesse.FkIdAnuncioNavigation.FkIdProdutoNavigation.NomeProduto;

                //Construct the alternate body as HTML

                string corpo = System.IO.File.ReadAllText(path: @"ConteudoEmail.html");

                string anexo = @"C:\Users\fic\Desktop\apostila.pdf";
                EnvioEmail(interesse.FkIdUsuarioNavigation.EmailUsuario, titulo, corpo, anexo);

                return x;
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

        /// <summary>
        /// Método que faz o envio do e-mail 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="titulo"></param>
        /// <param name="corpo"></param>
        /// <param name="anexo"></param>
        /// <returns></returns>
        private bool EnvioEmail(string email, string titulo, string corpo, string anexo) {
            try {
                //Instancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();

                //Remetente
                _mailMessage.From = new MailAddress("linx@gmail.com");//email da empresa

                //Destinatario seta noo método abaixo

                //Constrói o MailMessage
                _mailMessage.CC.Add(email);
                _mailMessage.Subject = titulo;
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = corpo;
                _mailMessage.Attachments.Add(new Attachment(anexo));

                //Configuração com Conta
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("linx@gmail.com", "12345");
                _smtpClient.EnableSsl = true;
                _smtpClient.Send(_mailMessage);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
