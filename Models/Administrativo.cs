using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Administrativo
    {
        [Key]
        public Guid Id_Admin { get; set; }

        [DisplayName("Cargo")]
        public string Cargo { get; set; }

        [DisplayName("Fecha de Ingreso")]
        public string FechaIngreso { get; set; }
      
        [ForeignKey("Id Persona")]
        public string PersonaId
        {
            get; set;
        }
    }
}
