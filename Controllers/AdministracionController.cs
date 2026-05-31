using System.Collections;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Primitives;
using ProyectoUniversidad.Models;

namespace ProyectoUniversidad.Controllers
{
    [Authorize]
    public class AdministracionController : Controller
    {
        private const int MaxRowsPerTable = 100;
        private readonly AppDbContext _context;
        private readonly ILogger<AdministracionController> _logger;

        public AdministracionController(AppDbContext context, ILogger<AdministracionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(string? entity)
        {
            if (!TryResolveEntity(entity, out var definition))
            {
                return NotFound();
            }

            var fields = GetFields(definition, includeKey: false);
            var totalCount = 0;
            IReadOnlyList<object> records = [];
            var statusMessage = TempData["StatusMessage"] as string ?? string.Empty;

            try
            {
                var query = GetQueryable(definition.EntityType);
                totalCount = CountRows(query, definition.EntityType);
                records = TakeRows(query, definition.EntityType, MaxRowsPerTable);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "No se pudo leer la tabla {TableName}.", definition.Key);
                statusMessage = $"La tabla {definition.PluralName} aun no esta disponible en la base de datos. Reinicia la aplicacion para aplicar migraciones o valida la conexion.";
            }

            var model = new AdminTableViewModel
            {
                Entity = definition,
                Entities = AdminEntityCatalog.Entities,
                Fields = fields,
                TotalCount = totalCount,
                Rows = records.Select(record => BuildRow(record, definition, fields)).ToList(),
                StatusMessage = statusMessage
            };

            return View(model);
        }

        public IActionResult Create(string? entity)
        {
            if (!TryResolveEntity(entity, out var definition))
            {
                return NotFound();
            }

            return View("Form", BuildFormModel(definition, null, "Crear", nameof(Create), false, null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string? entity, IFormCollection form)
        {
            if (!TryResolveEntity(entity, out var definition))
            {
                return NotFound();
            }

            var instance = Activator.CreateInstance(definition.EntityType);
            if (instance == null)
            {
                return BadRequest();
            }

            InitializeKey(instance, definition);
            ApplyFormValues(instance, definition, form);

            if (!ModelState.IsValid)
            {
                return View("Form", BuildFormModel(definition, instance, "Crear", nameof(Create), false, form));
            }

            try
            {
                _context.Add(instance);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "No se pudo guardar un registro en {TableName}.", definition.Key);
                ModelState.AddModelError(string.Empty, $"No se pudo guardar el registro. Valida que la tabla {definition.PluralName} exista en la base de datos.");
                return View("Form", BuildFormModel(definition, instance, "Crear", nameof(Create), false, form));
            }

            TempData["StatusMessage"] = $"{definition.SingularName} guardado correctamente.";
            return RedirectToAction(nameof(Index), new { entity = definition.Key });
        }

        public async Task<IActionResult> Edit(string? entity, string? id)
        {
            if (!TryResolveEntity(entity, out var definition))
            {
                return NotFound();
            }

            var record = await FindRecordAsync(definition, id);
            if (record == null)
            {
                return NotFound();
            }

            return View("Form", BuildFormModel(definition, record, "Editar", nameof(Edit), true, null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string? entity, string? id, IFormCollection form)
        {
            if (!TryResolveEntity(entity, out var definition))
            {
                return NotFound();
            }

            var record = await FindRecordAsync(definition, id);
            if (record == null)
            {
                return NotFound();
            }

            ApplyFormValues(record, definition, form);

            if (!ModelState.IsValid)
            {
                return View("Form", BuildFormModel(definition, record, "Editar", nameof(Edit), true, form));
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "No se pudo actualizar un registro en {TableName}.", definition.Key);
                ModelState.AddModelError(string.Empty, $"No se pudo actualizar el registro. Valida que la tabla {definition.PluralName} exista en la base de datos.");
                return View("Form", BuildFormModel(definition, record, "Editar", nameof(Edit), true, form));
            }

            TempData["StatusMessage"] = $"{definition.SingularName} actualizado correctamente.";
            return RedirectToAction(nameof(Index), new { entity = definition.Key });
        }

        public async Task<IActionResult> Delete(string? entity, string? id)
        {
            if (!TryResolveEntity(entity, out var definition))
            {
                return NotFound();
            }

            var record = await FindRecordAsync(definition, id);
            if (record == null)
            {
                return NotFound();
            }

            var fields = GetFields(definition, includeKey: false);
            var model = new AdminDeleteViewModel
            {
                Entity = definition,
                Fields = fields,
                KeyValue = GetKeyValue(record, definition),
                Values = fields.ToDictionary(field => field.Name, field => FormatValue(GetPropertyValue(record, field.Name)))
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? entity, string? id)
        {
            if (!TryResolveEntity(entity, out var definition))
            {
                return NotFound();
            }

            var record = await FindRecordAsync(definition, id);
            if (record == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(record);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "No se pudo eliminar un registro en {TableName}.", definition.Key);
                TempData["StatusMessage"] = $"No se pudo eliminar el registro. Valida que la tabla {definition.PluralName} exista en la base de datos.";
                return RedirectToAction(nameof(Index), new { entity = definition.Key });
            }

            TempData["StatusMessage"] = $"{definition.SingularName} eliminado correctamente.";
            return RedirectToAction(nameof(Index), new { entity = definition.Key });
        }

        private bool TryResolveEntity(string? entity, out AdminEntityDefinition definition)
        {
            if (string.IsNullOrWhiteSpace(entity))
            {
                definition = AdminEntityCatalog.Entities.First();
                return true;
            }

            var match = AdminEntityCatalog.Find(entity);
            if (match == null)
            {
                definition = AdminEntityCatalog.Entities.First();
                return false;
            }

            definition = match;
            return true;
        }

        private AdminFormViewModel BuildFormModel(
            AdminEntityDefinition definition,
            object? record,
            string mode,
            string actionName,
            bool isEdit,
            IFormCollection? form)
        {
            var fields = GetFields(definition, includeKey: false);

            return new AdminFormViewModel
            {
                Entity = definition,
                Fields = fields,
                Mode = mode,
                ActionName = actionName,
                IsEdit = isEdit,
                KeyValue = record == null ? string.Empty : GetKeyValue(record, definition),
                Values = fields.ToDictionary(
                    field => field.Name,
                    field => GetInputValue(record, field, form))
            };
        }

        private AdminRowViewModel BuildRow(
            object record,
            AdminEntityDefinition definition,
            IReadOnlyList<AdminFieldViewModel> fields)
        {
            return new AdminRowViewModel
            {
                KeyValue = GetKeyValue(record, definition),
                Values = fields.ToDictionary(field => field.Name, field => FormatValue(GetPropertyValue(record, field.Name)))
            };
        }

        private IReadOnlyList<AdminFieldViewModel> GetFields(AdminEntityDefinition definition, bool includeKey)
        {
            var entityType = GetEntityType(definition);
            var keyNames = entityType.FindPrimaryKey()?.Properties.Select(property => property.Name).ToHashSet()
                ?? new HashSet<string>();

            return entityType.GetProperties()
                .Where(property => property.PropertyInfo != null && !property.IsShadowProperty())
                .Where(property => includeKey || !keyNames.Contains(property.Name))
                .Select(property => new AdminFieldViewModel
                {
                    Name = property.Name,
                    Label = GetDisplayName(property.PropertyInfo!),
                    InputType = GetInputType(property.ClrType),
                    IsKey = keyNames.Contains(property.Name),
                    IsEditable = !keyNames.Contains(property.Name),
                    IsBoolean = GetNonNullableType(property.ClrType) == typeof(bool),
                    IsRequired = !property.IsNullable && GetNonNullableType(property.ClrType) != typeof(bool)
                })
                .ToList();
        }

        private void ApplyFormValues(object record, AdminEntityDefinition definition, IFormCollection form)
        {
            foreach (var field in GetFields(definition, includeKey: false).Where(field => field.IsEditable))
            {
                var property = definition.EntityType.GetProperty(field.Name);
                if (property == null || !property.CanWrite)
                {
                    continue;
                }

                try
                {
                    var parsedValue = ConvertFormValue(property.PropertyType, form[field.Name]);
                    property.SetValue(record, parsedValue);
                }
                catch
                {
                    ModelState.AddModelError(field.Name, $"El valor de {field.Label} no es valido.");
                }
            }
        }

        private void InitializeKey(object record, AdminEntityDefinition definition)
        {
            var key = GetKey(definition);
            if (key?.PropertyInfo == null || !key.PropertyInfo.CanWrite)
            {
                return;
            }

            if (GetNonNullableType(key.ClrType) == typeof(Guid))
            {
                key.PropertyInfo.SetValue(record, Guid.NewGuid());
            }
        }

        private async Task<object?> FindRecordAsync(AdminEntityDefinition definition, string? id)
        {
            var key = GetKey(definition);
            if (key == null || string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            try
            {
                var keyValue = ConvertTextValue(key.ClrType, id);
                return await _context.FindAsync(definition.EntityType, keyValue);
            }
            catch
            {
                return null;
            }
        }

        private IProperty? GetKey(AdminEntityDefinition definition)
        {
            return GetEntityType(definition).FindPrimaryKey()?.Properties.SingleOrDefault();
        }

        private IEntityType GetEntityType(AdminEntityDefinition definition)
        {
            return _context.Model.FindEntityType(definition.EntityType)
                ?? throw new InvalidOperationException($"La entidad {definition.SingularName} no esta registrada en el contexto.");
        }

        private IQueryable GetQueryable(Type entityType)
        {
            var method = typeof(DbContext).GetMethods()
                .Single(candidate => candidate.Name == nameof(DbContext.Set)
                    && candidate.IsGenericMethod
                    && candidate.GetParameters().Length == 0);

            return (IQueryable)method.MakeGenericMethod(entityType).Invoke(_context, null)!;
        }

        private static int CountRows(IQueryable query, Type entityType)
        {
            var method = typeof(Queryable).GetMethods()
                .Single(candidate => candidate.Name == nameof(Queryable.Count)
                    && candidate.GetParameters().Length == 1);

            return (int)method.MakeGenericMethod(entityType).Invoke(null, [query])!;
        }

        private static IReadOnlyList<object> TakeRows(IQueryable query, Type entityType, int count)
        {
            var takeMethod = typeof(Queryable).GetMethods()
                .Single(candidate => candidate.Name == nameof(Queryable.Take)
                    && candidate.GetParameters().Length == 2
                    && candidate.GetParameters()[1].ParameterType == typeof(int))
                .MakeGenericMethod(entityType);

            var toListMethod = typeof(Enumerable).GetMethods()
                .Single(candidate => candidate.Name == nameof(Enumerable.ToList)
                    && candidate.GetParameters().Length == 1)
                .MakeGenericMethod(entityType);

            var limitedQuery = (IQueryable)takeMethod.Invoke(null, [query, count])!;
            var list = (IEnumerable)toListMethod.Invoke(null, [limitedQuery])!;
            return list.Cast<object>().ToList();
        }

        private static object? ConvertFormValue(Type propertyType, StringValues values)
        {
            var targetType = GetNonNullableType(propertyType);

            if (targetType == typeof(bool))
            {
                return values.Any(value =>
                    string.Equals(value, "true", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(value, "on", StringComparison.OrdinalIgnoreCase));
            }

            var rawValue = values.FirstOrDefault();

            if (targetType == typeof(string))
            {
                return rawValue ?? string.Empty;
            }

            if (string.IsNullOrWhiteSpace(rawValue))
            {
                if (Nullable.GetUnderlyingType(propertyType) != null)
                {
                    return null;
                }

                throw new FormatException();
            }

            return ConvertTextValue(propertyType, rawValue);
        }

        private static object ConvertTextValue(Type propertyType, string rawValue)
        {
            var targetType = GetNonNullableType(propertyType);

            if (targetType == typeof(Guid))
            {
                return Guid.Parse(rawValue);
            }

            if (targetType == typeof(int))
            {
                return int.Parse(rawValue, CultureInfo.InvariantCulture);
            }

            if (targetType == typeof(long))
            {
                return long.Parse(rawValue, CultureInfo.InvariantCulture);
            }

            if (targetType == typeof(decimal))
            {
                return decimal.Parse(rawValue, CultureInfo.InvariantCulture);
            }

            if (targetType == typeof(double))
            {
                return double.Parse(rawValue, CultureInfo.InvariantCulture);
            }

            if (targetType == typeof(DateTime))
            {
                return DateTime.Parse(rawValue, CultureInfo.InvariantCulture);
            }

            return rawValue;
        }

        private static Type GetNonNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        private static string GetInputType(Type type)
        {
            var targetType = GetNonNullableType(type);

            if (targetType == typeof(bool))
            {
                return "checkbox";
            }

            if (targetType == typeof(int)
                || targetType == typeof(long)
                || targetType == typeof(decimal)
                || targetType == typeof(double)
                || targetType == typeof(float))
            {
                return "number";
            }

            if (targetType == typeof(DateTime))
            {
                return "date";
            }

            return "text";
        }

        private static string GetDisplayName(PropertyInfo property)
        {
            return property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
                ?? property.GetCustomAttribute<DisplayAttribute>()?.Name
                ?? SplitName(property.Name);
        }

        private static string SplitName(string name)
        {
            return name.Replace("_", " ");
        }

        private static object? GetPropertyValue(object record, string propertyName)
        {
            return record.GetType().GetProperty(propertyName)?.GetValue(record);
        }

        private static string GetKeyValue(object record, AdminEntityDefinition definition)
        {
            var keyProperty = definition.EntityType.GetProperties()
                .FirstOrDefault(property => property.GetCustomAttribute<KeyAttribute>() != null)
                ?? definition.EntityType.GetProperties().First();

            return Convert.ToString(keyProperty.GetValue(record), CultureInfo.InvariantCulture) ?? string.Empty;
        }

        private static string GetInputValue(object? record, AdminFieldViewModel field, IFormCollection? form)
        {
            if (form != null && form.TryGetValue(field.Name, out var formValues))
            {
                if (field.IsBoolean)
                {
                    return formValues.Any(value => string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                        ? "true"
                        : "false";
                }

                return formValues.FirstOrDefault() ?? string.Empty;
            }

            if (record == null)
            {
                return field.IsBoolean ? "false" : string.Empty;
            }

            var value = GetPropertyValue(record, field.Name);
            if (value is bool booleanValue)
            {
                return booleanValue ? "true" : "false";
            }

            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        private static string FormatValue(object? value)
        {
            return value switch
            {
                null => "Sin dato",
                bool booleanValue => booleanValue ? "Activo" : "Inactivo",
                DateTime dateTimeValue => dateTimeValue.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty
            };
        }
    }
}
