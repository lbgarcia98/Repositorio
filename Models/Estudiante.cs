#nullable disable
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Estudiante
    {
        [Key]
        public Guid EstudianteId { get; set; }

        [DisplayName("Carnet Estudiante")]
        public string CarnetEstudiante { get; set; }

        [DisplayName("Nombre Completo Estudiante")]
        public string Nombre { get; set; }

        [DisplayName ("Carrera Universitaria")]
        public string Carrera { get; set; }

        [ForeignKey("Universidad ID")]
        public string UniversidadId { get; set; }
    }
}
