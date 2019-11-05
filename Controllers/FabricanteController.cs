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
    public class FabricanteController : ControllerBase {
        FabricanteRepository _fabricanteRepository = new FabricanteRepository();

        [HttpGet("tolist")]
        public async Task<ActionResult<List<Fabricante>>> Get() {
            try {
                List<Fabricante> lstFabricante = await _fabricanteRepository.Get();

                if (lstFabricante == null) {
                    return NotFound();
                }

                return lstFabricante;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpGet("search/{id}")]
        public async Task<ActionResult<Fabricante>> Get(int id) {
            try {
                Fabricante fabricante = await _fabricanteRepository.Get(id);

                if (fabricante == null) {
                    return NotFound();
                }

                return fabricante;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Fabricante>> Post(Fabricante fabricante) {
            try {
                return await _fabricanteRepository.Post(fabricante);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Fabricante>> Put(int id, Fabricante fabricante) {
            if (id == fabricante.IdFabricante) {
                return BadRequest();
            }

            try {
                return await _fabricanteRepository.Put(fabricante);
            }
            catch (DbUpdateException ex) {
                var fabricanteValida = _fabricanteRepository.Get(id);

                if (fabricanteValida == null) {
                    return NotFound();
                }
                else {
                    throw ex;
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Fabricante>> Delete(int id) {
            try {
                Fabricante fabricanteRetornada = await _fabricanteRepository.Get(id);

                if (fabricanteRetornada == null) {
                    return NotFound();
                }

                await _fabricanteRepository.Delete(fabricanteRetornada);

                return fabricanteRetornada;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
