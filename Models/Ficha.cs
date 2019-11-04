using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {
    public partial class Ficha {
        public Ficha() {
            Produto = new HashSet<Produto>();
        }

        [Key]
        [Column("idFicha")]
        public int IdFicha { get; set; }
        [Required]
        [Column("sist_opFicha")]
        [StringLength(100)]
        public string SistOpFicha { get; set; }
        [Required]
        [Column("processadorFicha")]
        [StringLength(100)]
        public string ProcessadorFicha { get; set; }
        [Required]
        [Column("placa_videoFicha")]
        [StringLength(100)]
        public string PlacaVideoFicha { get; set; }
        [Required]
        [Column("audioFicha")]
        [StringLength(100)]
        public string AudioFicha { get; set; }
        [Required]
        [Column("telaFicha")]
        [StringLength(100)]
        public string TelaFicha { get; set; }
        [Required]
        [Column("memoriaFicha")]
        [StringLength(100)]
        public string MemoriaFicha { get; set; }
        [Required]
        [Column("armazenamentoFicha")]
        [StringLength(100)]
        public string ArmazenamentoFicha { get; set; }

        [InverseProperty("FkIdFichaNavigation")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
