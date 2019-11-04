using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface IConservacaoRepository {
        Task<List<Conservacao>> Get();
        Task<Conservacao> Get(int id);
        Task<Conservacao> Post(Conservacao conservacao);
        Task<Conservacao> Put(Conservacao conservacao);
        Task<Conservacao> Delete(Conservacao conservacaoRetornada);
    }
}
