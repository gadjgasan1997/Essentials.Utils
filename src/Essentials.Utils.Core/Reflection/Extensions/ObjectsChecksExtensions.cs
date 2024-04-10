using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Essentials.Utils.Extensions;
using Essentials.Utils.Reflection.Attributes;
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable OutParameterValueIsAlwaysDiscarded.Global

namespace Essentials.Utils.Reflection.Extensions;

/// <summary>
/// Методы расширения для валидации объектов с использованием рефлексии
/// </summary>
public static class ObjectsChecksExtensions
{
    /// <summary>
    /// Проверяет обязательные свойства на заполненность
    /// </summary>
    /// <param name="instance">Сущность</param>
    /// <param name="emptyProperties">Список с пустыми свойствами</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool CheckRequiredProperties<T>(
        this T instance,
        out List<PropertyInfo> emptyProperties)
    {
        emptyProperties = new List<PropertyInfo>();
        
        if (instance is null)
            return true;

        var allProperties = instance.GetType().GetProperties();

        if (!instance.CheckPropertiesWithRequiredAttribute(allProperties, out emptyProperties))
            return false;

        if (!instance.CheckPropertiesWithRequiredWhenAttribute(allProperties, ref emptyProperties))
            return false;

        return true;
    }
    
    /// <summary>
    /// Проверяет обязательные свойства на заполненность
    /// </summary>
    /// <param name="instance">Сущность</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool CheckRequiredProperties<T>(this T instance) => instance.CheckRequiredProperties(out _);

    /// <summary>
    /// Проверяет обязательные свойства, имеющие аттрибут <see cref="RequiredAttribute" /> на заполненность
    /// </summary>
    /// <param name="instance">Сущность</param>
    /// <param name="allProperties">Список всех свойств</param>
    /// <param name="emptyProperties">Список с пустыми свойствами</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static bool CheckPropertiesWithRequiredAttribute<T>(
        this T instance,
        IReadOnlyCollection<PropertyInfo> allProperties,
        out List<PropertyInfo> emptyProperties)
    {
        emptyProperties = allProperties
            .GetPropertiesWithAttr<RequiredAttribute>(attribute => attribute.AllowEmptyStrings)
            .Where(info => PropertyIsNull(instance, info))
            .ToList();
        
        if (emptyProperties.Any())
            return false;

        emptyProperties = allProperties
            .GetPropertiesWithAttr<RequiredAttribute>(attribute => !attribute.AllowEmptyStrings)
            .Where(info => PropertyIsNullOrEmpty(instance, info))
            .ToList();

        if (emptyProperties.Any())
            return false;

        return true;
    }

    /// <summary>
    /// Проверяет обязательные свойства, имеющие аттрибут <see cref="RequiredWhenAttribute" /> на заполненность
    /// </summary>
    /// <param name="instance">Сущность</param>
    /// <param name="allProperties">Список всех свойств</param>
    /// <param name="emptyProperties">Список с пустыми свойствами</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static bool CheckPropertiesWithRequiredWhenAttribute<T>(
        this T instance,
        IReadOnlyCollection<PropertyInfo> allProperties,
        ref List<PropertyInfo> emptyProperties)
    {
        var propertiesToCheck = allProperties.GetPropertiesWithAttr<RequiredWhenAttribute>();

        foreach (var propertyToCheck in propertiesToCheck)
        {
            var attribute = propertyToCheck.GetCustomAttribute<RequiredWhenAttribute>().CheckNotNull();

            var targetProperty = allProperties.FirstOrDefault(p => p.Name == attribute.MemberName);
            if (targetProperty?.GetValue(instance, null) is not bool value)
                continue;
            
            if (value != attribute.MemberValue)
                continue;
            
            if (attribute.AllowEmptyStrings && !PropertyIsNull(instance, propertyToCheck))
                continue;

            if (!PropertyIsNullOrEmpty(instance, propertyToCheck))
                continue;
            
            emptyProperties.Add(propertyToCheck);
        }
        
        return !emptyProperties.Any();
    }

    /// <summary>
    /// Проверяет свойство на заполненность
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="propertyInfo">Свойство</param>
    /// <returns></returns>
    private static bool PropertyIsNull<T>(T instance, PropertyInfo propertyInfo) =>
        propertyInfo.GetValue(instance, null) is null;

    /// <summary>
    /// Проверяет свойство на заполненность
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="propertyInfo">Свойство</param>
    /// <returns></returns>
    private static bool PropertyIsNullOrEmpty<T>(T instance, PropertyInfo propertyInfo)
    {
        var value = propertyInfo.GetValue(instance, null);
        return value switch
        {
            null => true,
            string @string when string.IsNullOrWhiteSpace(@string) => true,
            _ => false
        };
    }
}