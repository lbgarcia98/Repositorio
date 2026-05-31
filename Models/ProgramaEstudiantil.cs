using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class ProgramaEstudiantil
    {
        [Key]
        public Guid Id_Programa { get; set; }

        [DisplayName("Nombre")]
        public string NombrePrograma { get; set; }

        [DisplayName("Nivel")]
        public string NivelPrograma { get; set; }

        [DisplayName("Duracion de Periodos")]
        public string DuracionPeriodos { get; set; }

        [ForeignKey("Id Escuela")]
        public string Id_Escuela
        {
            get; set;
        }
    }
}
