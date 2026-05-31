using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoUniversidad.Migrations
{
    /// <inheritdoc />
    public partial class agregarEntidadesAcademicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrativos",
                columns: table => new
                {
                    Id_Admin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaIngreso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrativos", x => x.Id_Admin);
                });

            migrationBuilder.CreateTable(
                name: "Asignaturas",
                columns: table => new
                {
                    Id_Asignatura = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreAsignatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creditos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Escuela = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaturas", x => x.Id_Asignatura);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id_Aula = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentificadorAula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EdificioAula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacidadAula = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id_Aula);
                });

            migrationBuilder.CreateTable(
                name: "CiclosAcademicos",
                columns: table => new
                {
                    CicloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Anio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Semestre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UniversidadId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiclosAcademicos", x => x.CicloId);
                });

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    Id_Docente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo_Docente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaIngresoDocente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<bool>(type: "bit", nullable: false),
                    UniversidadId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonaId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.Id_Docente);
                });

            migrationBuilder.CreateTable(
                name: "Escuelas",
                columns: table => new
                {
                    Id_Escuela = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreEscuela = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionEscuela = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Facultad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuelas", x => x.Id_Escuela);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarnetEstudiante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carrera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversidadId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteId);
                });

            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    Id_Facultad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreFacultad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionFacu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decano = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.Id_Facultad);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id_Horario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraFin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Seccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Aula = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id_Horario);
                });

            migrationBuilder.CreateTable(
                name: "PeriodosAcademicos",
                columns: table => new
                {
                    Id_PeriodoAcademico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombrePeriodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicioPeriodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaFinalizaPeriodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivoPeriodo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodosAcademicos", x => x.Id_PeriodoAcademico);
                });

            migrationBuilder.CreateTable(
                name: "ProgramasEstudiantiles",
                columns: table => new
                {
                    Id_Programa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombrePrograma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelPrograma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuracionPeriodos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Escuela = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramasEstudiantiles", x => x.Id_Programa);
                });

            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    Id_Seccion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoSeccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cupos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Asignatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_PeriodoAcademico = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.Id_Seccion);
                });

            migrationBuilder.CreateTable(
                name: "Universidades",
                columns: table => new
                {
                    UniversidadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Sede = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universidades", x => x.UniversidadId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversidadId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Administrativos");
            migrationBuilder.DropTable(name: "Asignaturas");
            migrationBuilder.DropTable(name: "Aulas");
            migrationBuilder.DropTable(name: "CiclosAcademicos");
            migrationBuilder.DropTable(name: "Docentes");
            migrationBuilder.DropTable(name: "Escuelas");
            migrationBuilder.DropTable(name: "Estudiantes");
            migrationBuilder.DropTable(name: "Facultades");
            migrationBuilder.DropTable(name: "Horarios");
            migrationBuilder.DropTable(name: "PeriodosAcademicos");
            migrationBuilder.DropTable(name: "ProgramasEstudiantiles");
            migrationBuilder.DropTable(name: "Secciones");
            migrationBuilder.DropTable(name: "Universidades");
            migrationBuilder.DropTable(name: "Usuarios");
        }
    }
}
