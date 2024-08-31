using System.Diagnostics.CodeAnalysis;
using static System.Enum;

namespace Essentials.Utils.Extensions;

/// <summary>
/// Методы расширения для строк
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Проверяет что строка состоит из пробелов
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns></returns>
    public static bool IsWhiteSpace(this string value) => value.All(char.IsWhiteSpace);

    /// <summary>
    /// Удаляет все пробелы из строки
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns></returns>
    public static string? FullTrim([NotNullIfNotNull(nameof(value))] this string? value) =>
        value?.Replace(" ", string.Empty);

    /// <summary>
    /// Обрезает строку вначале и в конце
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns></returns>
    public static string? TrimStartAndEnd([NotNullIfNotNull(nameof(value))] this string? value) =>
        value?.TrimStart().TrimEnd();
    
    /// <summary>
    /// Пытается распарсить Enum из строки
    /// </summary>
    /// <param name="value">Строка</param>
    /// <param name="result">Результат</param>
    /// <typeparam name="TEnum">Тип перечисления</typeparam>
    /// <returns>Признак, прошел ли парсинг успешно</returns>
    public static bool TryParseEnum<TEnum>(
        this string value,
        [NotNullWhen(true)] out TEnum? result)
        where TEnum : struct, Enum
    {
        if (TryParse<TEnum>(value, true, out var parsed) && IsDefined(parsed))
        {
            result = parsed;
            return true;
        }

        result = null;
        return false;
    }
}