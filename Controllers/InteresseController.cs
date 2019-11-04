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

        [HttpPost("insert")]
        public async Task<ActionResult<Interesse>> Post(Interesse interesse) {
            try {
                return await _interesseRepository.Post(interesse);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

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
