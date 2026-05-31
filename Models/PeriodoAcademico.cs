using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class PeriodoAcademico
    {
        [Key]
        public Guid Id_PeriodoAcademico { get; set; }

        [DisplayName("Nombre")]
        public string NombrePeriodo { get; set; }

        [DisplayName("Fecha de Inicio de Periodo")]
        public string FechaInicioPeriodo { get; set; }

        [DisplayName("Fecha de Finalizacon de Periodo")]
        public string FechaFinalizaPeriodo { get; set; }

        [DisplayName("Activo")]
        public string ActivoPeriodo { get; set; }
    }
}
