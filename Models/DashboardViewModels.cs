namespace ProyectoUniversidad.Models
{
    public sealed class DashboardViewModel
    {
        public IReadOnlyList<DashboardMetricViewModel> PrimaryMetrics { get; init; } = [];
        public IReadOnlyList<DashboardMetricViewModel> AcademicMetrics { get; init; } = [];
        public IReadOnlyList<EntitySummaryViewModel> EntitySummaries { get; init; } = [];
        public int TotalRecords { get; init; }
        public int ActiveCycles { get; init; }
        public int TotalAcademicOffer { get; init; }
        public string UserName { get; init; } = string.Empty;
    }

    public sealed class DashboardMetricViewModel
    {
        public string Title { get; init; } = string.Empty;
        public string Value { get; init; } = "0";
        public string Hint { get; init; } = string.Empty;
        public string IconCss { get; init; } = "mdi mdi-chart-box";
        public string AccentCss { get; init; } = "accent-blue";
        public string EntityKey { get; init; } = string.Empty;
    }

    public sealed class EntitySummaryViewModel
    {
        public string EntityKey { get; init; } = string.Empty;
        public string PluralName { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string IconCss { get; init; } = string.Empty;
        public string AccentCss { get; init; } = string.Empty;
        public int Count { get; init; }
    }
}
