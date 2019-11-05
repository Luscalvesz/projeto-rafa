using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    interface IFichaRepository
    {
         Task<List<Ficha>> Get();
         Task<Ficha> Get( int id);

         Task<Ficha> Post (Ficha ficha);

         Task<Ficha> Put (Ficha ficha);

         Task<Ficha> Delete (Ficha fichaRetornada);
    }
}