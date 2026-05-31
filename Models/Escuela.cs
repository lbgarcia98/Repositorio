using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Escuela
    {
        [Key]
        public Guid Id_Escuela { get; set; }

        [DisplayName("Nombre")]
        public string NombreEscuela { get; set; }

        [DisplayName("Descripcion")]
        public string DescripcionEscuela { get; set; }

        [ForeignKey("Id Facultad")]
        public string Id_Facultad
        {
            get; set;
        }
    }
}
