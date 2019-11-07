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
        Task<List<Anuncio>> BuscaPorPrecoSet(decimal precoMin, decimal precoMax);
        Task<List<Anuncio>> BuscaFabricanteConservacao(string fabricante, string conservacao);
        Task<List<Anuncio>> OrdenarPorData();
        Task<List<Anuncio>> BuscaPorCampo(string campoDesejado);
        Task<Anuncio> Cadastrar(Anuncio anuncio);
        Task<Anuncio> Atualizar(Anuncio anuncio);
        Task<Anuncio> Deletar(Anuncio anuncioRetornado);
    }
}
