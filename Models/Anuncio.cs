using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {
    public partial class Anuncio {
        public Anuncio() {
            Interesse = new HashSet<Interesse>();
        }

        [Key]
        [Column("idAnuncio")]
        public int IdAnuncio { get; set; }
        [Column("precoAnuncio", TypeName = "decimal(7, 2)")]
        public decimal PrecoAnuncio { get; set; }
        [Column("dt_finalAnuncio", TypeName = "date")]
        public DateTime DtFinalAnuncio { get; set; }
        [Required]
        [Column("statusAnuncio")]
        [StringLength(15)]
        public string StatusAnuncio { get; set; }
        [Column("FK_idConservacao")]
        public int FkIdConservacao { get; set; }
        [Column("FK_idProduto")]
        public int FkIdProduto { get; set; }
        [Column("fk_idImagem")]
        public int FkIdImagem { get; set; }
        [Column("fk_idImagem02")]
        public int FkIdImagem02 { get; set; }
        [Column("fk_idImagem03")]
        public int FkIdImagem03 { get; set; }

        [ForeignKey(nameof(FkIdConservacao))]
        [InverseProperty(nameof(Conservacao.Anuncio))]
        public virtual Conservacao FkIdConservacaoNavigation { get; set; }
        [ForeignKey(nameof(FkIdImagem02))]
        [InverseProperty(nameof(Imagem.AnuncioFkIdImagem02Navigation))]
        public virtual Imagem FkIdImagem02Navigation { get; set; }
        [ForeignKey(nameof(FkIdImagem03))]
        [InverseProperty(nameof(Imagem.AnuncioFkIdImagem03Navigation))]
        public virtual Imagem FkIdImagem03Navigation { get; set; }
        [ForeignKey(nameof(FkIdImagem))]
        [InverseProperty(nameof(Imagem.AnuncioFkIdImagemNavigation))]
        public virtual Imagem FkIdImagemNavigation { get; set; }
        [ForeignKey(nameof(FkIdProduto))]
        [InverseProperty(nameof(Produto.Anuncio))]
        public virtual Produto FkIdProdutoNavigation { get; set; }
        [InverseProperty("FkIdAnuncioNavigation")]
        public virtual ICollection<Interesse> Interesse { get; set; }
    }
}
