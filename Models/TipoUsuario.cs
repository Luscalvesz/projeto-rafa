using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {
    public partial class TipoUsuario {
        public TipoUsuario() {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        [Column("idTipoUsuario")]
        public int IdTipoUsuario { get; set; }
        [Required]
        [Column("permissaoTipoUsuario")]
        [StringLength(20)]
        public string PermissaoTipoUsuario { get; set; }

        [InverseProperty("FkIdTipoUsuarioNavigation")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
