using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Docente
    {
        [Key]
        public Guid Id_Docente { get; set; }

        [DisplayName("Codigo Docente")]
        public string Codigo_Docente { get; set; }

        [DisplayName("Fecha de Ingreso")]
        public string FechaIngresoDocente { get; set; }

        [DisplayName("Categoria")]
        public bool Categoria { get; set; }

        [ForeignKey("Universidad ID")]
        public string UniversidadId
        {
            get; set;
        }
        [ForeignKey("Id Persona")]
        public string PersonaId
        {
            get; set;
        }
    }
}
