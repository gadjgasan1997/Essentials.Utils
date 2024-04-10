namespace Essentials.Utils.Extensions;

/// <summary>
/// Методы расширения для объектов
/// </summary>
public static class ObjectsExtensions
{
    /// <summary>
    /// Атомарно выполняет некоторое действие
    /// </summary>
    /// <param name="instance">Объект</param>
    /// <param name="isInvoked">Признак, было ли вызвано данное действие</param>
    /// <param name="action">Действие</param>
    /// <returns>Объект</returns>
    public static T AtomicInvokeAction<T>(this T instance, ref uint isInvoked, Action action)
    {
        if (Interlocked.Exchange(ref isInvoked, 1) == 1)
            return instance;

        action();
        return instance;
    }
}