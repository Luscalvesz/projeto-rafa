using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface IFabricanteRepository {
        Task<List<Fabricante>> Get();
        Task<Fabricante> Get(int id);
        Task<Fabricante> Post(Fabricante fabricante);
        Task<Fabricante> Put(Fabricante fabricante);
        Task<Fabricante> Delete(Fabricante fabricanteRetornado);
    }
}
