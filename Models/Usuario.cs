#nullable disable
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Usuario
    {
        [Key]
        public Guid UsuarioId { get; set; }

        [DisplayName ("Nombre Usuario")]
        public string NombreUsuario { get; set; }

        [DisplayName ("Cargo")]
        public string Cargo {  get; set; }

        [ForeignKey ("Universidad ID")]
        public string UniversidadId { get; set; }

    }
}
