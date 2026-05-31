namespace ProyectoUniversidad.Models
{
    public sealed class AdminTableViewModel
    {
        public AdminEntityDefinition Entity { get; init; } = null!;
        public IReadOnlyList<AdminEntityDefinition> Entities { get; init; } = [];
        public IReadOnlyList<AdminFieldViewModel> Fields { get; init; } = [];
        public IReadOnlyList<AdminRowViewModel> Rows { get; init; } = [];
        public int TotalCount { get; init; }
        public string StatusMessage { get; init; } = string.Empty;
    }

    public sealed class AdminRowViewModel
    {
        public string KeyValue { get; init; } = string.Empty;
        public IReadOnlyDictionary<string, string> Values { get; init; } = new Dictionary<string, string>();
    }

    public sealed class AdminFormViewModel
    {
        public AdminEntityDefinition Entity { get; init; } = null!;
        public IReadOnlyList<AdminFieldViewModel> Fields { get; init; } = [];
        public IReadOnlyDictionary<string, string> Values { get; init; } = new Dictionary<string, string>();
        public string Mode { get; init; } = "Crear";
        public string ActionName { get; init; } = "Create";
        public string KeyValue { get; init; } = string.Empty;
        public bool IsEdit { get; init; }
    }

    public sealed class AdminDeleteViewModel
    {
        public AdminEntityDefinition Entity { get; init; } = null!;
        public IReadOnlyList<AdminFieldViewModel> Fields { get; init; } = [];
        public IReadOnlyDictionary<string, string> Values { get; init; } = new Dictionary<string, string>();
        public string KeyValue { get; init; } = string.Empty;
    }

    public sealed class AdminFieldViewModel
    {
        public string Name { get; init; } = string.Empty;
        public string Label { get; init; } = string.Empty;
        public string InputType { get; init; } = "text";
        public bool IsKey { get; init; }
        public bool IsEditable { get; init; } = true;
        public bool IsBoolean { get; init; }
        public bool IsRequired { get; init; }
    }
}
