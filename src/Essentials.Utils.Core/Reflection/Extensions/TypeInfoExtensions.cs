using System.Reflection;
// ReSharper disable MemberCanBePrivate.Global

namespace Essentials.Utils.Reflection.Extensions;

/// <summary>
/// Методы расширения для <see cref="TypeInfo" />
/// </summary>
public static class TypeInfoExtensions
{
    /// <summary>
    /// Определяет, что тип реализует определенный интерфейс
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <param name="implementedInterfaceType">Тип интерфейса</param>
    /// <returns></returns>
    public static bool IsImplements(this TypeInfo typeInfo, Type implementedInterfaceType) =>
        !typeInfo.IsAbstract && typeInfo.ImplementedInterfaces.Any(type => type == implementedInterfaceType);
    
    /// <summary>
    /// Определяет, что тип реализует определенный интерфейс
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <typeparam name="T">Тип интерфейса</typeparam>
    /// <returns></returns>
    public static bool IsImplements<T>(this TypeInfo typeInfo) => typeInfo.IsImplements(typeof(T));

    /// <summary>
    /// Определяет, что тип реализует определенный Generic интерфейс
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <param name="implementedInterfaceType">Тип интерфейса</param>
    /// <returns></returns>
    public static bool IsImplementsGeneric(this TypeInfo typeInfo, Type implementedInterfaceType) =>
        !typeInfo.IsAbstract
        && typeInfo.ImplementedInterfaces.Any(type =>
            type.IsGenericType && type.GetGenericTypeDefinition() == implementedInterfaceType);
    
    /// <summary>
    /// Определяет, что тип реализует определенный Generic интерфейс
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <typeparam name="T">Тип интерфейса</typeparam>
    /// <returns></returns>
    public static bool IsImplementsGeneric<T>(this TypeInfo typeInfo) => typeInfo.IsImplementsGeneric(typeof(T));

    /// <summary>
    /// Определяет, что тип наследуется от определенного
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <param name="baseType">Базовый тип</param>
    /// <returns></returns>
    public static bool IsInheritedFrom(this TypeInfo typeInfo, Type baseType) =>
        !typeInfo.IsAbstract && typeInfo.BaseType == baseType;

    /// <summary>
    /// Определяет, что тип наследуется от определенного
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <typeparam name="T">Базовый тип</typeparam>
    /// <returns></returns>
    public static bool IsInheritedFrom<T>(this TypeInfo typeInfo) => typeInfo.IsInheritedFrom(typeof(T));

    /// <summary>
    /// Возвращает Generic интерфейсы типа
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <returns></returns>
    public static IEnumerable<Type> GetGenericInterfaces(this TypeInfo typeInfo) =>
        typeInfo.GetInterfaces().Where(type => type.IsGenericType);
    
    /// <summary>
    /// Возвращает Generic интерфейсы типа, соответствующие типу <paramref name="interfaceType" />
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <param name="interfaceType">Тип желаемого интерфейса</param>
    /// <returns></returns>
    public static IEnumerable<Type> GetGenericInterfaces(this TypeInfo typeInfo, Type interfaceType) =>
        typeInfo.GetGenericInterfaces().Where(type => type.GetGenericTypeDefinition() == interfaceType);
    
    /// <summary>
    /// Возвращает Generic интерфейсы типа, соответствующие типу <typeparamref name="T" />
    /// </summary>
    /// <param name="typeInfo">Тип</param>
    /// <typeparam name="T">Тип желаемого интерфейса</typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetGenericInterfaces<T>(this TypeInfo typeInfo) => typeInfo.GetGenericInterfaces(typeof(T));
}