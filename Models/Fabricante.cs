using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {
    public partial class Fabricante {
        public Fabricante() {
            Produto = new HashSet<Produto>();
        }

        [Key]
        [Column("idFabricante")]
        public int IdFabricante { get; set; }
        [Required]
        [Column("nomeFabricante")]
        [StringLength(50)]
        public string NomeFabricante { get; set; }

        [InverseProperty("FkIdFabricanteNavigation")]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
