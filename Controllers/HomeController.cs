using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Models;

namespace ProyectoUniversidad.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var counts = new Dictionary<string, int>
            {
                ["Universidades"] = await SafeCountAsync(() => _context.Universidades.CountAsync(), "Universidades"),
                ["Personas"] = await SafeCountAsync(() => _context.Personas.CountAsync(), "Personas"),
                ["Estudiantes"] = await SafeCountAsync(() => _context.Estudiantes.CountAsync(), "Estudiantes"),
                ["Docentes"] = await SafeCountAsync(() => _context.Docentes.CountAsync(), "Docentes"),
                ["Administrativos"] = await SafeCountAsync(() => _context.Administrativos.CountAsync(), "Administrativos"),
                ["Facultades"] = await SafeCountAsync(() => _context.Facultades.CountAsync(), "Facultades"),
                ["Escuelas"] = await SafeCountAsync(() => _context.Escuelas.CountAsync(), "Escuelas"),
                ["ProgramasEstudiantiles"] = await SafeCountAsync(() => _context.ProgramasEstudiantiles.CountAsync(), "ProgramasEstudiantiles"),
                ["Asignaturas"] = await SafeCountAsync(() => _context.Asignaturas.CountAsync(), "Asignaturas"),
                ["PeriodosAcademicos"] = await SafeCountAsync(() => _context.PeriodosAcademicos.CountAsync(), "PeriodosAcademicos"),
                ["CiclosAcademicos"] = await SafeCountAsync(() => _context.CiclosAcademicos.CountAsync(), "CiclosAcademicos"),
                ["Secciones"] = await SafeCountAsync(() => _context.Secciones.CountAsync(), "Secciones"),
                ["Aulas"] = await SafeCountAsync(() => _context.Aulas.CountAsync(), "Aulas"),
                ["Horarios"] = await SafeCountAsync(() => _context.Horarios.CountAsync(), "Horarios"),
                ["Usuarios"] = await SafeCountAsync(() => _context.Usuarios.CountAsync(), "Usuarios")
            };

            var activeCycles = await SafeCountAsync(() => _context.CiclosAcademicos.CountAsync(ciclo => ciclo.Activo), "CiclosAcademicos");
            var academicOffer = counts["ProgramasEstudiantiles"] + counts["Asignaturas"] + counts["Secciones"];

            var model = new DashboardViewModel
            {
                UserName = User.Identity?.Name ?? "Usuario",
                ActiveCycles = activeCycles,
                TotalAcademicOffer = academicOffer,
                TotalRecords = counts.Values.Sum(),
                PrimaryMetrics =
                [
                    new DashboardMetricViewModel
                    {
                        Title = "Estudiantes",
                        Value = counts["Estudiantes"].ToString(),
                        Hint = "Registros estudiantiles activos en la plataforma.",
                        IconCss = "mdi mdi-school",
                        AccentCss = "accent-cyan",
                        EntityKey = "Estudiantes"
                    },
                    new DashboardMetricViewModel
                    {
                        Title = "Docentes",
                        Value = counts["Docentes"].ToString(),
                        Hint = "Equipo docente disponible para secciones.",
                        IconCss = "mdi mdi-teach",
                        AccentCss = "accent-purple",
                        EntityKey = "Docentes"
                    },
                    new DashboardMetricViewModel
                    {
                        Title = "Oferta academica",
                        Value = academicOffer.ToString(),
                        Hint = "Programas, asignaturas y secciones registradas.",
                        IconCss = "mdi mdi-book-open-variant",
                        AccentCss = "accent-gold",
                        EntityKey = "ProgramasEstudiantiles"
                    },
                    new DashboardMetricViewModel
                    {
                        Title = "Ciclos activos",
                        Value = activeCycles.ToString(),
                        Hint = "Ciclos marcados como activos para operacion.",
                        IconCss = "mdi mdi-calendar-check",
                        AccentCss = "accent-green",
                        EntityKey = "CiclosAcademicos"
                    }
                ],
                AcademicMetrics =
                [
                    new DashboardMetricViewModel
                    {
                        Title = "Facultades",
                        Value = counts["Facultades"].ToString(),
                        Hint = "Unidades academicas principales.",
                        IconCss = "mdi mdi-bank",
                        AccentCss = "accent-red",
                        EntityKey = "Facultades"
                    },
                    new DashboardMetricViewModel
                    {
                        Title = "Escuelas",
                        Value = counts["Escuelas"].ToString(),
                        Hint = "Escuelas asociadas a facultades.",
                        IconCss = "mdi mdi-office-building",
                        AccentCss = "accent-teal",
                        EntityKey = "Escuelas"
                    },
                    new DashboardMetricViewModel
                    {
                        Title = "Aulas",
                        Value = counts["Aulas"].ToString(),
                        Hint = "Espacios fisicos disponibles.",
                        IconCss = "mdi mdi-door-open",
                        AccentCss = "accent-slate",
                        EntityKey = "Aulas"
                    },
                    new DashboardMetricViewModel
                    {
                        Title = "Horarios",
                        Value = counts["Horarios"].ToString(),
                        Hint = "Bloques de clase registrados.",
                        IconCss = "mdi mdi-clock-outline",
                        AccentCss = "accent-orange",
                        EntityKey = "Horarios"
                    }
                ],
                EntitySummaries = AdminEntityCatalog.Entities
                    .Select(entity => new EntitySummaryViewModel
                    {
                        EntityKey = entity.Key,
                        PluralName = entity.PluralName,
                        Description = entity.Description,
                        IconCss = entity.IconCss,
                        AccentCss = entity.AccentCss,
                        Count = counts.TryGetValue(entity.Key, out var count) ? count : 0
                    })
                    .ToList()
            };

            return View(model);
        }

        private async Task<int> SafeCountAsync(Func<Task<int>> countOperation, string tableName)
        {
            try
            {
                return await countOperation();
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "No se pudo leer la tabla {TableName}. Se mostrara 0 en el dashboard.", tableName);
                return 0;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Se mostro la pagina de error para la solicitud {TraceIdentifier}.", HttpContext.TraceIdentifier);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
