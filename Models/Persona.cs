#nullable disable
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Persona
    {
        [Key]
        public Guid PersonaId { get; set; }

        [DisplayName("Nombres")]
        public string Nombre { get; set; }
        [DisplayName("Apellidos")]
        public string Apellido { get; set; }

        [DisplayName("Dirección de persona")]
        public string? Direccion { get; set; }

        [ForeignKey("Id")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [ScaffoldColumn(false)]
        public bool Inactivo { get; set; }

    }
}
