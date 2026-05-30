#nullable disable
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class CicloAcademico
    {
        [Key]
        public Guid CicloId { get; set; }

        [DisplayName("Año academico")]
        public string Anio { get; set; }

        [DisplayName("Semestre")]
        public string Semestre { get; set; }

        [DisplayName("Activo")]
        public bool Activo { get; set; }

        [ForeignKey("Universidad ID")]
        public string UniversidadId { get; set; }
    }
}
