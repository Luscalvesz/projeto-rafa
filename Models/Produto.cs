using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {
    public partial class Produto {
        public Produto() {
            Anuncio = new HashSet<Anuncio>();
        }

        [Key]
        [Column("idProduto")]
        public int IdProduto { get; set; }
        [Required]
        [Column("nomeProduto")]
        [StringLength(50)]
        public string NomeProduto { get; set; }
        [Required]
        [Column("modeloProduto")]
        [StringLength(50)]
        public string ModeloProduto { get; set; }
        [Column("dt_lancProduto", TypeName = "date")]
        public DateTime DtLancProduto { get; set; }
        [Column("FK_idFabricante")]
        public int FkIdFabricante { get; set; }
        [Column("FK_idFicha")]
        public int FkIdFicha { get; set; }
        [Column("FK_idUsuario")]
        public int FkIdUsuario { get; set; }

        [ForeignKey(nameof(FkIdFabricante))]
        [InverseProperty(nameof(Fabricante.Produto))]
        public virtual Fabricante FkIdFabricanteNavigation { get; set; }
        [ForeignKey(nameof(FkIdFicha))]
        [InverseProperty(nameof(Ficha.Produto))]
        public virtual Ficha FkIdFichaNavigation { get; set; }
        [ForeignKey(nameof(FkIdUsuario))]
        [InverseProperty(nameof(Usuario.Produto))]
        public virtual Usuario FkIdUsuarioNavigation { get; set; }
        [InverseProperty("FkIdProdutoNavigation")]
        public virtual ICollection<Anuncio> Anuncio { get; set; }
    }
}
