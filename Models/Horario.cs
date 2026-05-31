using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Horario
    {
        [Key]
        public Guid Id_Horario { get; set; }

        [DisplayName("Dias de clases")]
        public string Dias { get; set; }

        [DisplayName("Hora de Inicio")]
        public string HoraInicio { get; set; }

        [DisplayName("Hora de Finalizacion")]
        public string HoraFin { get; set; }

        [ForeignKey("Id Seccion")]
        public string Id_Seccion
        {
            get; set;
        }

        [ForeignKey("Id Aula")]
        public string Id_Aula
        {
            get; set;
        }
    }
}
