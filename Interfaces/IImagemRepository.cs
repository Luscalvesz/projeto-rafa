using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IImagemRepository
    {
        Task<List<Imagem>> Get();
        Task<Imagem> Get(int id);
        Task<Imagem> Post(Imagem imagem);
        Task<Imagem> Put(Imagem imagem);
        Task<Imagem> Delete(Imagem imagemRetornada);        
    }
}