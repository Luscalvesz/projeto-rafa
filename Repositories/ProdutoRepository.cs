using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;

namespace API.Repositories {
    public class ProdutoRepository : IProdutoRepository {
        Time2EOLContext _context = new Time2EOLContext();
        public async Task<List<Produto>> Get() {
            return await _context.Produto.Include(c => c.FkIdFabricanteNavigation).Include(u => u.FkIdUsuarioNavigation).ToListAsync();
        }

        public async Task<Produto> Get(int id) {
            return await _context.Produto.Include(c => c.FkIdFabricanteNavigation).Include(u => u.FkIdUsuarioNavigation).FirstOrDefaultAsync(x => x.IdProduto == id);
        }

        public async Task<Produto> Post(Produto produto) {
            await _context.Produto.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> Put(Produto produto) {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> Delete(Produto produtoRetornado) {
            _context.Produto.Remove(produtoRetornado);
            await _context.SaveChangesAsync();

            return produtoRetornado;
        }
    }
}
