using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models {
    public class UsuarioLogin : Usuario{
        [Column("nomeUsuario")]
        [StringLength(50)]
        public new string NomeUsuario { get; set; }
    }
}
