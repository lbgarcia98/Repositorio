using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Aula
    {
        [Key]
        public Guid Id_Aula { get; set; }

        [DisplayName("Identificador de Aula")]
        public string IdentificadorAula { get; set; }

        [DisplayName("Edificio")]
        public string EdificioAula { get; set; }

        [DisplayName("Capacidad del Aula")]
        public string CapacidadAula { get; set; }
    }
}
