using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface IAnuncioRepository {
        Task<List<Anuncio>> Listar();
        Task<Anuncio> BuscaPorId(int id);
        Task<List<Anuncio>> BuscaPorPreco(decimal preco);
        Task<List<Anuncio>> BuscaFabricanteConservacao(string fabricante, string conservacao);
        Task<List<Anuncio>> BuscaPorCampo(string campoDesejado);
        Task<Anuncio> Post(Anuncio anuncio);
        Task<Anuncio> Put(Anuncio anuncio);
        Task<Anuncio> Delete(Anuncio anuncioRetornado);
    }
}
