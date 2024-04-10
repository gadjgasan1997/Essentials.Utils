using System.Reflection;

namespace Essentials.Utils.Reflection.Extensions;

/// <summary>
/// Методы расширений для свойств
/// </summary>
public static class PropertiesExtensions
{
    /// <summary>
    /// Возвращает список свойств, имеющих требуемый аттрибут
    /// </summary>
    /// <param name="properties">Исходный список свойств</param>
    /// <param name="predicate">Необязательное условие</param>
    /// <typeparam name="T">Тип аттрибута</typeparam>
    /// <returns>Отфильтрованный список свойств</returns>
    public static IEnumerable<PropertyInfo> GetPropertiesWithAttr<T>(
        this IEnumerable<PropertyInfo> properties,
        Func<T, bool>? predicate = null)
        where T : Attribute
    {
        return predicate is null
            ? properties.Where(info => info.GetCustomAttribute<T>() is not null)
            : properties.Where(info => info.GetCustomAttribute<T>() is { } attribute && predicate(attribute));
    }
}