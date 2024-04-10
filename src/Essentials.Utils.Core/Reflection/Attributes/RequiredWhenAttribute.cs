using System.Diagnostics.CodeAnalysis;
using Essentials.Utils.Extensions;

namespace Essentials.Utils.Reflection.Attributes;

/// <summary>
/// Атрибут обязательного свойства по условию
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class RequiredWhenAttribute : Attribute
{
    /// <summary>
    /// Название свойства, которое должно иметь значение <see cref="MemberValue" />,
    /// чтобы свойство, помеченное атрибутом <see cref="RequiredWhenAttribute"/> являлось обязательным
    /// </summary>
    public string MemberName { get; }
    
    /// <summary>
    /// Значение, которое должно иметь свойство с названием <see cref="MemberName" />,
    /// чтобы свойство, помеченное атрибутом <see cref="RequiredWhenAttribute"/> являлось обязательным
    /// </summary>
    public bool MemberValue { get; }
    
    /// <summary>
    /// Признак, что разрешены пустые строки
    /// </summary>
    public bool AllowEmptyStrings { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="memberName">
    /// Название свойства, которое должно иметь значение <see cref="MemberValue" />,
    /// чтобы свойство, помеченное атрибутом <see cref="RequiredWhenAttribute"/> являлось обязательным
    /// </param>
    /// <param name="memberValue">
    /// Значение, которое должно иметь свойство с названием <see cref="MemberName" />,
    /// чтобы свойство, помеченное атрибутом <see cref="RequiredWhenAttribute"/> являлось обязательным
    /// </param>
    /// <param name="allowEmptyStrings">Признак, что разрешены пустые строки</param>
    public RequiredWhenAttribute(
        string memberName,
        bool memberValue,
        bool allowEmptyStrings = false)
    {
        MemberName = memberName.CheckNotNullOrEmpty();
        MemberValue = memberValue;
        AllowEmptyStrings = allowEmptyStrings;
    }
}