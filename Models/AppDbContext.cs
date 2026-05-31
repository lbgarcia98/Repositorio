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
        public DbSet<Administrativo> Administrativos { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<CicloAcademico> CiclosAcademicos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Facultad> Facultades { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<PeriodoAcademico> PeriodosAcademicos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<ProgramaEstudiantil> ProgramasEstudiantiles { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Universidad> Universidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
