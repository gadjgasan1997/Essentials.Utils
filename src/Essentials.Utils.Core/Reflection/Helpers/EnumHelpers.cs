using System.Reflection;
using Essentials.Utils.Extensions;
// ReSharper disable LoopCanBeConvertedToQuery

namespace Essentials.Utils.Reflection.Helpers;

/// <summary>
/// Хелперы для работы с перечислениями
/// </summary>
public static class EnumHelpers
{
    /// <summary>
    /// Возвращает список значений перечисления
    /// </summary>
    /// <typeparam name="T">Тип перечисления</typeparam>
    /// <returns>Список значений перечисления</returns>
    public static IEnumerable<T> GetValues<T>()
        where T : Enum
    {
        foreach(var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var value = field.GetRawConstantValue().CheckNotNull();
            yield return (T) value;
        }
    }
}