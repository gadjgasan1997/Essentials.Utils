using System.Reflection;
using Essentials.Utils.Extensions;
using Essentials.Utils.Reflection.Extensions;

namespace Essentials.Utils.Reflection.Helpers;

/// <summary>
/// Хелперы для работы со сборками
/// </summary>
public static class AssemblyHelpers
{
    /// <summary>
    /// Возвращает список всех сборок с зависимостями
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAllAssembliesWithReferences()
    {
        var entryAssembly = Assembly.GetEntryAssembly().CheckNotNull("Ошибка получения сборки");
        var executingAssembly = Assembly.GetExecutingAssembly();
        var callingAssembly = Assembly.GetCallingAssembly();
        
        return new[] { entryAssembly, executingAssembly, callingAssembly }
            .Concat(entryAssembly.LoadReferencedAssemblies())
            .Concat(executingAssembly.LoadReferencedAssemblies())
            .Concat(callingAssembly.LoadReferencedAssemblies())
            .Distinct();
    }

    /// <summary>
    /// Возвращает список всех сборок текущего домена
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetCurrentDomainAssemblies() =>
        AppDomain.CurrentDomain.GetAssemblies().Distinct();
}