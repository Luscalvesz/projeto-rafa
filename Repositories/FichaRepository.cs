﻿using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories {
    public class FichaRepository {
        Time2EOLContext _context = new Time2EOLContext();

        public async Task<List<Ficha>> Get() {
            return await _context.Ficha.ToListAsync();
        }

        public async Task<Ficha> Get(int id) {
            return await _context.Ficha.FindAsync(id);
        }

        public async Task<Ficha> Post(Ficha ficha) {
            await _context.Ficha.AddAsync(ficha);
            await _context.SaveChangesAsync();

            return ficha;
        }
        public async Task<Ficha> Put(Ficha ficha) {
            _context.Entry(ficha).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return ficha;
        }

        public async Task<Ficha> Delete(Ficha fichaRetornada) {
            _context.Ficha.Remove(fichaRetornada);
            await _context.SaveChangesAsync();

            return fichaRetornada;
        }
    }
}
