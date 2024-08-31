namespace Essentials.Utils.Reflection.Extensions;

/// <summary>
/// Методы расширения для <see cref="Type" />
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Возвращает название типа вместе с пространством имен (Не FullName)
    /// </summary>
    /// <param name="type">Тип</param>
    /// <returns>Название типа вместе с пространством имен</returns>
    public static string GetNameWithNamespace(this Type type) =>
        !string.IsNullOrWhiteSpace(type.Namespace)
            ? $"{type.Namespace}.{type.Name}"
            : $"{type.Name}";
}