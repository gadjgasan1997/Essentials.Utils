using System.Reflection;
using Essentials.Utils.Extensions;

namespace Essentials.Utils.Reflection.Models;

/// <summary>
/// Название сборки
/// </summary>
public readonly record struct EssentialsAssemblyName
{
    private EssentialsAssemblyName(string value, bool isShortFormat)
    {
        Value = value;
        IsShortFormat = isShortFormat;
    }
    
    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Признак, что название сборки было сделано в кратком формате
    /// </summary>
    public bool IsShortFormat { get; }
    
    /// <summary>
    /// Создает краткое название сборки
    /// </summary>
    /// <param name="assembly">Сборка</param>
    /// <returns>Краткое название сборки</returns>
    public static EssentialsAssemblyName CreateShortName(Assembly assembly)
    {
        var name = GetAssemblyName(assembly).Name;
        name.CheckNotNullOrEmpty("Название сборки не может быть пустым");
        
        return new EssentialsAssemblyName(name, isShortFormat: true);
    }
    
    /// <summary>
    /// Создает полное название сборки
    /// </summary>
    /// <param name="assembly">Сборка</param>
    /// <returns>Полное название сборки</returns>
    public static EssentialsAssemblyName CreateFullName(Assembly assembly)
    {
        var name = GetAssemblyName(assembly).FullName;
        name.CheckNotNullOrEmpty("Название сборки не может быть пустым");
        
        return new EssentialsAssemblyName(name, isShortFormat: false);
    }
    
    /// <summary>
    /// Возвращает название сборки
    /// </summary>
    /// <param name="assembly">Сборка</param>
    /// <returns>Название сборки</returns>
    private static AssemblyName GetAssemblyName(Assembly assembly)
    {
        assembly.CheckNotNull($"Сборка для создания объекта '{nameof(EssentialsAssemblyName)}' не может быть пустой");
        return assembly.GetName();
    }
}