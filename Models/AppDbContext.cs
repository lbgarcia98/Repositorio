using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoUniversidad.Models
{
        public class AppDbContext : IdentityDbContext<IdentityUser>
        {
            public AppDbContext
                (DbContextOptions<AppDbContext> options)
                : base(options)
            {
                   
            }

        //PERSONAS ES CLASE Y PERSONAS ES LA TABLA EN LA BASE DE DATOS
        public DbSet<Persona> Personas { get; set; }
    }
}
