using Essentials.Utils.Extensions;
using System.Collections.Concurrent;

namespace Essentials.Utils.Tasks;

/// <summary>
/// Сервис ожидания задачи с помощью <see cref="TaskCompletionSource{T}" />
/// </summary>
/// <typeparam name="T">Тип результата, возвращаемого задачей</typeparam>
public static class TasksAwaiter<T>
{
    private static readonly ConcurrentDictionary<string, TaskCompletionSource<T>> _tasks = [];
    
    /// <summary>
    /// Создает объект <see cref="TaskCompletionSource{T}" />
    /// </summary>
    /// <param name="taskId">Id задачи</param>
    /// <param name="token">Токен отмены задачи</param>
    /// <returns>Объект <see cref="TaskCompletionSource{T}" /></returns>
    public static TaskCompletionSource<T> CreateTaskSource(string taskId, CancellationToken? token = null)
    {
        taskId.CheckNotNullOrEmpty("Id задачи не может быть пустым");
        token?.Register(() =>
        {
            if (!_tasks.TryRemove(taskId, out var existingTask))
                throw new InvalidOperationException($"Не удалось удалить задачу по taskId: '{taskId}'");
            
            var exception = new TimeoutException($"Таймаут получения ответа по taskId: '{taskId}'");
            if (!existingTask.TrySetException(exception))
                existingTask.SetCanceled();
        });
        
        return _tasks.AddOrUpdate(
            taskId,
            addValue: new TaskCompletionSource<T>(TaskCreationOptions.None),
            updateValueFactory: (_, source) => source);
    }
    
    /// <summary>
    /// Проставляет результат задачи
    /// </summary>
    /// <param name="taskId">Id задачи</param>
    /// <param name="result">Результат</param>
    public static void SetTaskResult(string taskId, T result)
    {
        taskId.CheckNotNullOrEmpty("Id задачи не может быть пустым");
        if (!_tasks.TryGetValue(taskId, out var source))
        {
            throw new KeyNotFoundException(
                $"Не удалось найти задачу с Id, равным '{taskId}' для проставления результата");
        }
        
        if (!source.TrySetResult(result))
        {
            throw new InvalidOperationException(
                $"Не удалось проставить результат для задачи с Id, равным '{taskId}'");
        }
    }
}