using System.Reflection;
using Essentials.Utils.Extensions;

// ReSharper disable MemberCanBePrivate.Global

namespace Essentials.Utils.Reflection.Extensions;

/// <summary>
/// Методы расширения для <see cref="Assembly" />
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// Загружает список сборок, на которые ссылается текущая
    /// </summary>
    /// <param name="assembly">Текущая сборка</param>
    /// <returns>Загруженнный список сборок</returns>
    public static List<Assembly> LoadReferencedAssemblies(this Assembly assembly) =>
        assembly
            .GetReferencedAssemblies()
            .Select(Assembly.Load)
            .ToList();

    /// <summary>
    /// Загружает сборку, на которые ссылается текущая
    /// </summary>
    /// <param name="assembly">Текущая сборка</param>
    /// <param name="targetAssemblyFullName">Название требуемой сборки</param>
    /// <returns>Загруженнная сборка</returns>
    public static Assembly LoadReferencedAssembly(this Assembly assembly, string targetAssemblyFullName)
    {
        var assemblyName = assembly
            .GetReferencedAssemblies()
            .Single(name => name.FullName == targetAssemblyFullName);

        return Assembly.Load(assemblyName);
    }

    /// <summary>
    /// Возвращает тип по названию из сборки
    /// </summary>
    /// <param name="assembly">Сборка</param>
    /// <param name="targetTypeName">Название типа</param>
    /// <param name="stringComparison"></param>
    /// <returns>Тип</returns>
    public static Type GetTypeByName(
        this Assembly assembly,
        string targetTypeName,
        StringComparison stringComparison = StringComparison.CurrentCulture)
    {
        return assembly
            .GetTypes()
            .FirstOrDefault(type => string.Equals(type.FullName, targetTypeName, stringComparison))
            .CheckNotNull(
                $"Тип с названием '{targetTypeName}' не найден в сборке '{assembly.FullName}'",
                "targetType");
    }

    /// <summary>
    /// Возвращает тип по названию из сборок
    /// </summary>
    /// <param name="assemblies">Сборки</param>
    /// <param name="targetTypeName">Название типа</param>
    /// <param name="stringComparison"></param>
    /// <returns>Тип</returns>
    public static Type GetTypeByName(
        this Assembly[] assemblies,
        string targetTypeName,
        StringComparison stringComparison = StringComparison.CurrentCulture)
    {
        return assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(type => string.Equals(type.FullName, targetTypeName, stringComparison))
            .CheckNotNull(
                $"Тип с названием '{targetTypeName}' " +
                $"не найден в сборках '{string.Join(", ", assemblies.Select(assembly => assembly.FullName))}'",
                "targetType");
    }

    /// <summary>
    /// Возвращает типы, реализующие указанный интерфейс
    /// </summary>
    /// <param name="assembly">Сборка</param>
    /// <param name="interfaceType">Тип интерфейса</param>
    /// <returns>Список типов, реализующих указанный интерфейс</returns>
    public static List<TypeInfo> GetImplementationsTypes(this Assembly assembly, Type interfaceType) =>
        assembly.DefinedTypes.Where(type => type.IsImplements(interfaceType)).ToList();
}