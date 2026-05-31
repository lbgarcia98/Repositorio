using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Seccion
    {
        [Key]
        public Guid Id_Seccion { get; set; }

        [DisplayName("Codigo Seccion")]
        public string CodigoSeccion { get; set; }

        [DisplayName("Cupos")]
        public string Cupos { get; set; }

        [ForeignKey("Id Asignatura")]
        public string Id_Asignatura
        {
            get; set;
        }

        [ForeignKey("Id Periodo")]
        public string Id_PeriodoAcademico
        {
            get; set;
        }
    }
}
