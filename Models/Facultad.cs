using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Facultad
    {
        [Key]
        public Guid Id_Facultad { get; set; }

        [DisplayName("Nombre")]
        public string NombreFacultad { get; set; }

        [DisplayName("Descripcion")]
        public string DescripcionFacu { get; set; }

        [DisplayName("Decano")]
        public string Decano { get; set; }

    }
}
