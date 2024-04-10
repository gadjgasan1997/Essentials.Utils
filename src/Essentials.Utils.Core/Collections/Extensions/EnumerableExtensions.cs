using System.Diagnostics.CodeAnalysis;

namespace Essentials.Utils.Collections.Extensions;

/// <summary>
/// Методы расширения для коллекций
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class EnumerableExtensions
{
    /// <summary>
    /// Выполняет действие для всех элементов коллекции
    /// </summary>
    /// <param name="enumerable">Коллекция</param>
    /// <param name="action">Действие</param>
    /// <typeparam name="T"></typeparam>
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) =>
        enumerable.ToList().ForEach(action);

    /// <summary>
    /// Выполняет действие для всех элементов коллекции, удовлетворяющих условию
    /// </summary>
    /// <param name="enumerable">Коллекция</param>
    /// <param name="predicate">Условие</param>
    /// <param name="action">Действие</param>
    /// <typeparam name="T"></typeparam>
    public static void ForEach<T>(this List<T> enumerable, Func<T, bool> predicate, Action<T> action) =>
        enumerable.Where(predicate).ForEach(action);

    /// <summary>
    /// Выполняет действие для всех элементов коллекции, удовлетворяющих условию
    /// </summary>
    /// <param name="enumerable">Коллекция</param>
    /// <param name="predicate">Условие</param>
    /// <param name="action">Действие</param>
    /// <typeparam name="T"></typeparam>
    public static void ForEach<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, Action<T> action) =>
        enumerable.ToList().ForEach(predicate, action);

    /// <summary>
    /// Возвращает первый найденный по условию элемент или null
    /// </summary>
    /// <param name="source">Исходная коллекция</param>
    /// <param name="predicate">Условие поиска</param>
    /// <typeparam name="TSource">Тип элементов коллекции</typeparam>
    /// <returns>Элемент</returns>
    public static TSource? FirstOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        where TSource : struct
    {
        foreach (var element in source)
        {
            if (predicate(element))
                return element;
        }

        return null;
    }
}