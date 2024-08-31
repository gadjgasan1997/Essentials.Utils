using System.Text;
using Essentials.Utils.Extensions;
using Essentials.Utils.Reflection.Extensions;

namespace Essentials.Utils.Reflection.Models;

/// <summary>
/// Название типа
/// </summary>
public readonly record struct TypeName
{
    private TypeName(string value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Создает краткое название типа
    /// </summary>
    /// <param name="assemblyName">Название сборки</param>
    /// <typeparam name="T">Тип</typeparam>
    /// <returns>Краткое название типа</returns>
    public static TypeName CreateShortName<T>(EssentialsAssemblyName? assemblyName = null) =>
        CreateShortName(typeof(T), assemblyName);

    /// <summary>
    /// Создает краткое название типа
    /// </summary>
    /// <param name="type">Тип</param>
    /// <param name="assemblyName">Название сборки</param>
    /// <returns>Краткое название типа</returns>
    public static TypeName CreateShortName(Type type, EssentialsAssemblyName? assemblyName = null)
    {
        type.CheckNotNull($"Тип для создания объекта '{nameof(TypeName)}' не может быть пустым");
        
        if (type.IsGenericType)
            return CreateShortGenericName(type, assemblyName);
        
        return new TypeName(CreateShortNameString(type, assemblyName));
    }
    
    /// <summary>
    /// Создает полное название типа
    /// </summary>
    /// <param name="assemblyName">Название сборки</param>
    /// <typeparam name="T">Тип</typeparam>
    /// <returns>Полное название типа</returns>
    public static TypeName CreateFullName<T>(EssentialsAssemblyName? assemblyName = null) =>
        CreateFullName(typeof(T), assemblyName);

    /// <summary>
    /// Создает полное название типа
    /// </summary>
    /// <param name="type">Тип</param>
    /// <param name="assemblyName">Название сборки</param>
    /// <returns>Полное название типа</returns>
    public static TypeName CreateFullName(Type type, EssentialsAssemblyName? assemblyName = null)
    {
        type.CheckNotNull($"Тип для создания объекта '{nameof(TypeName)}' не может быть пустым");
        
        if (type.IsGenericType)
            return CreateFullGenericName(type, assemblyName);

        if (type.IsGenericParameter)
            return CreateShortName(type, assemblyName);

        type.FullName.CheckNotNullOrEmpty($"Не указано полное название для типа '{type.Name}'");
        return new TypeName(CreateFullNameString(type, assemblyName));
    }
    
    /// <summary>
    /// Создает краткое название generic типа
    /// </summary>
    /// <param name="type">Тип</param>
    /// <param name="assemblyName">Название сборки</param>
    /// <returns>Полное краткое generic типа</returns>
    private static TypeName CreateShortGenericName(Type type, EssentialsAssemblyName? assemblyName = null)
    {
        var stringBuilder = new StringBuilder(type.Name);
        stringBuilder.Append('[');
        
        var genericArguments = type.GetGenericArguments();
        for (var i = 0; i < genericArguments.Length; i++)
        {
            var argument = genericArguments[i];
            
            stringBuilder.Append('[');
            
            var genericArgumentAssemblyName = CreateAssemblyNameForGenericArgument(assemblyName, argument);
            var genericArgumentTypeName = argument.IsGenericType
                ? CreateShortGenericName(argument, genericArgumentAssemblyName)
                : CreateShortName(argument, genericArgumentAssemblyName);

            stringBuilder.Append(genericArgumentTypeName.Value);
            stringBuilder.Append(']');

            if (i < genericArguments.Length - 1)
                stringBuilder.Append(',');
        }
        
        stringBuilder.Append(']');
        
        var typeName = stringBuilder.ToString();
        return new TypeName(ResolveTypeName(typeName, assemblyName));
    }
    
    /// <summary>
    /// Создает полное название generic типа
    /// </summary>
    /// <param name="type">Тип</param>
    /// <param name="assemblyName">Название сборки</param>
    /// <returns>Полное название generic типа</returns>
    private static TypeName CreateFullGenericName(Type type, EssentialsAssemblyName? assemblyName = null)
    {
        var stringBuilder = new StringBuilder(type.GetNameWithNamespace());
        stringBuilder.Append('[');
        
        var genericArguments = type.GetGenericArguments();
        for (var i = 0; i < genericArguments.Length; i++)
        {
            var argument = genericArguments[i];
            
            stringBuilder.Append('[');

            var genericArgumentAssemblyName = CreateAssemblyNameForGenericArgument(assemblyName, argument);
            var genericArgumentTypeName = argument.IsGenericType
                ? CreateFullGenericName(argument, genericArgumentAssemblyName)
                : CreateFullName(argument, genericArgumentAssemblyName);

            stringBuilder.Append(genericArgumentTypeName.Value);
            stringBuilder.Append(']');

            if (i < genericArguments.Length - 1)
                stringBuilder.Append(',');
        }
        
        stringBuilder.Append(']');
        
        var typeName = stringBuilder.ToString();
        return new TypeName(ResolveTypeName(typeName, assemblyName));
    }
    
    private static EssentialsAssemblyName? CreateAssemblyNameForGenericArgument(
        EssentialsAssemblyName? typeAssemblyName,
        Type argument)
    {
        return typeAssemblyName switch
        {
            { IsShortFormat: true } => EssentialsAssemblyName.CreateShortName(argument.Assembly),
            { IsShortFormat: false } => EssentialsAssemblyName.CreateFullName(argument.Assembly),
            _ => null
        };
    }

    private static string CreateShortNameString(Type type, EssentialsAssemblyName? assemblyName = null) =>
        ResolveTypeName(type.Name, assemblyName);
    
    private static string CreateFullNameString(Type type, EssentialsAssemblyName? assemblyName = null) =>
        ResolveTypeName(type.FullName!, assemblyName);

    private static string ResolveTypeName(string name, EssentialsAssemblyName? assemblyName = null) =>
        !assemblyName.HasValue ? name : $"{name}, {assemblyName.Value.Value}";
}