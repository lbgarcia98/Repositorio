namespace ProyectoUniversidad.Models
{
    public sealed record AdminEntityDefinition(
        string Key,
        Type EntityType,
        string SingularName,
        string PluralName,
        string Description,
        string IconCss,
        string AccentCss);

    public static class AdminEntityCatalog
    {
        public static IReadOnlyList<AdminEntityDefinition> Entities { get; } =
        [
            new("Universidades", typeof(Universidad), "Universidad", "Universidades", "Datos principales de sedes y contacto.", "mdi mdi-domain", "accent-blue"),
            new("Personas", typeof(Persona), "Persona", "Personas", "Registro base de usuarios vinculados.", "mdi mdi-account-multiple", "accent-green"),
            new("Estudiantes", typeof(Estudiante), "Estudiante", "Estudiantes", "Carnets, carreras y datos academicos.", "mdi mdi-school", "accent-cyan"),
            new("Docentes", typeof(Docente), "Docente", "Docentes", "Codigo, ingreso y categoria docente.", "mdi mdi-teach", "accent-purple"),
            new("Administrativos", typeof(Administrativo), "Administrativo", "Administrativos", "Cargos y fechas de ingreso.", "mdi mdi-briefcase-account", "accent-orange"),
            new("Facultades", typeof(Facultad), "Facultad", "Facultades", "Facultades, decanos y descripcion.", "mdi mdi-bank", "accent-red"),
            new("Escuelas", typeof(Escuela), "Escuela", "Escuelas", "Escuelas relacionadas con facultades.", "mdi mdi-office-building", "accent-teal"),
            new("ProgramasEstudiantiles", typeof(ProgramaEstudiantil), "Programa", "Programas estudiantiles", "Niveles, duracion y escuela base.", "mdi mdi-book-open-variant", "accent-gold"),
            new("Asignaturas", typeof(Asignatura), "Asignatura", "Asignaturas", "Cursos, creditos y escuela.", "mdi mdi-book-education", "accent-indigo"),
            new("PeriodosAcademicos", typeof(PeriodoAcademico), "Periodo academico", "Periodos academicos", "Fechas de inicio, cierre y estado.", "mdi mdi-calendar-range", "accent-lime"),
            new("CiclosAcademicos", typeof(CicloAcademico), "Ciclo academico", "Ciclos academicos", "Anio, semestre y ciclo activo.", "mdi mdi-calendar-clock", "accent-pink"),
            new("Secciones", typeof(Seccion), "Seccion", "Secciones", "Codigos, cupos, asignatura y periodo.", "mdi mdi-google-classroom", "accent-navy"),
            new("Aulas", typeof(Aula), "Aula", "Aulas", "Edificios, identificadores y capacidad.", "mdi mdi-door-open", "accent-slate"),
            new("Horarios", typeof(Horario), "Horario", "Horarios", "Dias, horas, secciones y aulas.", "mdi mdi-clock-outline", "accent-brown"),
            new("Usuarios", typeof(Usuario), "Usuario interno", "Usuarios internos", "Usuarios operativos por universidad.", "mdi mdi-account-cog", "accent-gray")
        ];

        public static AdminEntityDefinition? Find(string? key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            return Entities.FirstOrDefault(entity =>
                string.Equals(entity.Key, key, StringComparison.OrdinalIgnoreCase));
        }
    }
}
