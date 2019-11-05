using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;

namespace API.Repositories {
    public class AnuncioRepository : IAnuncioRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<Anuncio>> Listar() {
            return await _context.Anuncio
                .Include(c => c.FkIdConservacaoNavigation)
                .Include(p => p.FkIdProdutoNavigation).ToListAsync();
        }

        public async Task<Anuncio> BuscaPorId(int id) {
            return await _context.Anuncio
                .Include(c => c.FkIdConservacaoNavigation)
                .Include(p => p.FkIdProdutoNavigation).FirstOrDefaultAsync(x => x.IdAnuncio == id);
        }

        public async Task<List<Anuncio>> BuscaPorPreco(decimal preco) {
            List<Anuncio> lstAnuncio = await _context.Anuncio
                /*.Include(a => a.FkIdProdutoNavigation)
                .Include(a => a.FkIdProdutoNavigation.FkIdFabricanteNavigation)
                .Include(a => a.FkIdProdutoNavigation.FkIdFichaNavigation)
                .Include(a => a.FkIdProdutoNavigation.FkIdUsuarioNavigation)
                .Include(a => a.FkIdConservacaoNavigation)*/
                .Where(a => a.PrecoAnuncio >= preco).ToListAsync();

            return lstAnuncio;
        }

        public async Task<List<Anuncio>> BuscaFabricanteConservacao(string fabricante, string conservacaoRecebida) {

            List<Anuncio> listaAnuncio = await _context.Anuncio
                .Include(x => x.FkIdProdutoNavigation.FkIdFabricanteNavigation)
                .Include(y => y.FkIdConservacaoNavigation)
                .Where(a => a.FkIdProdutoNavigation.FkIdFabricanteNavigation.NomeFabricante == fabricante && a.FkIdConservacaoNavigation.EstadoConservacao == conservacaoRecebida).ToListAsync();

            return listaAnuncio;
        }

        public async Task<List<Anuncio>> BuscaPorCampo(string campoDesejado) {
            List<Anuncio> lstAnuncio = await _context.Anuncio.Where(a => a.FkIdProdutoNavigation.ModeloProduto.StartsWith(campoDesejado) || campoDesejado == null).OrderBy(a => a.FkIdProdutoNavigation.ModeloProduto).ToListAsync();

            return lstAnuncio;
        }

        public async Task<Anuncio> Post(Anuncio anuncio) {
            await _context.Anuncio.AddAsync(anuncio);
            await _context.SaveChangesAsync();

            return anuncio;
        }

        public async Task<Anuncio> Put(Anuncio anuncio) {
            _context.Entry(anuncio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return anuncio;
        }

        public async Task<Anuncio> Delete(Anuncio anuncioRetornado) {
            _context.Anuncio.Remove(anuncioRetornado);
            await _context.SaveChangesAsync();

            return anuncioRetornado;
        }
    }
}
