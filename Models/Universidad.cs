#nullable disable
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProyectoUniversidad.Models
{
    public class Universidad
    {
        [Key]
        public Guid UniversidadId { get; set; }

        [DisplayName ("Nombre")]
        public string Nombre { get; set; }

        [DisplayName ("Telefono")]
        public int Telefono { get; set; }

        [DisplayName ("Sede")]
        public string Sede { get; set; }

        [DisplayName ("Direccion")]
        public string Direccion { get; set; }
    }
}
