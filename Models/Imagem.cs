using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {
    public partial class Imagem {
        public Imagem() {
            AnuncioFkIdImagem02Navigation = new HashSet<Anuncio>();
            AnuncioFkIdImagem03Navigation = new HashSet<Anuncio>();
            AnuncioFkIdImagemNavigation = new HashSet<Anuncio>();
        }

        [Key]
        [Column("idImagem")]
        public int IdImagem { get; set; }
        [Column("imagem")]
        [StringLength(150)]
        public string Imagem1 { get; set; }
        [Column("altImagem")]
        [StringLength(150)]
        public string AltImagem { get; set; }

        [InverseProperty(nameof(Anuncio.FkIdImagem02Navigation))]
        public virtual ICollection<Anuncio> AnuncioFkIdImagem02Navigation { get; set; }
        [InverseProperty(nameof(Anuncio.FkIdImagem03Navigation))]
        public virtual ICollection<Anuncio> AnuncioFkIdImagem03Navigation { get; set; }
        [InverseProperty(nameof(Anuncio.FkIdImagemNavigation))]
        public virtual ICollection<Anuncio> AnuncioFkIdImagemNavigation { get; set; }
    }
}
