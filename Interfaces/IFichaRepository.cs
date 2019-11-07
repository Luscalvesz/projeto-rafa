using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces {
    interface IFichaRepository {
        Task<List<Ficha>> Get();
        Task<Ficha> Get(int id);
        Task<Ficha> Post(Ficha ficha);
        Task<Ficha> Put(Ficha ficha);
        Task<Ficha> Delete(Ficha fichaRetornada);
    }
}
