using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUniversidad.Models
{
    public class Asignatura
    {
        [Key]
        public Guid Id_Asignatura { get; set; }

        [DisplayName("Nombre de asignatura")]
        public string NombreAsignatura { get; set; }

        [DisplayName("Creditos")]
        public string Creditos { get; set; }

        [ForeignKey("Id Escuela")]
        public string Id_Escuela
        {
            get; set;
        }

    }
}
