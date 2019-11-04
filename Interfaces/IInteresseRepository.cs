using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface IInteresseRepository {
        Task<List<Interesse>> Get();
        Task<Interesse> Get(int id);
        Task<Interesse> Post(Interesse interesse);
        Task<Interesse> Put(Interesse interesse);
        Task<Interesse> Delete(Interesse interesseRetornado);
    }
}
