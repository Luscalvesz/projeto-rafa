using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces {
    interface IProdutoRepository {
        Task<List<Produto>> Get();
        Task<Produto> Get(int id);
        Task<Produto> Post(Produto produto);
        Task<Produto> Put(Produto produto);
        Task<Produto> Delete(Produto produtoRetornado);
    }
}
