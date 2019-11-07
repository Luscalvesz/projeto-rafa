using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces {
    interface IImageRepository {
        Task<List<Imagem>> Get();
        Task<Imagem> Get(int id);
        Task<Imagem> Post(Imagem imagem);
        Task<Imagem> Put(Imagem imagem);
        Task<Imagem> Delete(Imagem imagemRetornada);
    }
}
