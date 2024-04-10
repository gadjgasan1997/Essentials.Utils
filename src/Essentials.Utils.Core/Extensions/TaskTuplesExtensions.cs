using System.Runtime.CompilerServices;

namespace Essentials.Utils.Extensions;

/// <summary>
/// Методы расширений для <see cref="Task" />
/// </summary>
public static class TaskTuplesExtensions
{
    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2)> GetAwaiter<T, T2>(this (Task<T>, Task<T2>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2)> CombineTasksAsync()
        {
            var (task1, task2) = tasks;
            await Task.WhenAll(task1, task2);
            return (task1.Result, task2.Result);
        }
    }
    
    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3)> GetAwaiter<T, T2, T3>(
        this (Task<T>, Task<T2>, Task<T3>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3)> CombineTasksAsync()
        {
            var (task1, task2, task3) = tasks;
            await Task.WhenAll(task1, task2, task3);
            return (task1.Result, task2.Result, task3.Result);
        }
    }
    
    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <typeparam name="T4">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3, T4)> GetAwaiter<T, T2, T3, T4>(
        this (Task<T>, Task<T2>, Task<T3>, Task<T4>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3, T4)> CombineTasksAsync()
        {
            var (task1, task2, task3, task4) = tasks;
            await Task.WhenAll(task1, task2, task3, task4);
            return (task1.Result, task2.Result, task3.Result, task4.Result);
        }
    }
    
    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <typeparam name="T4">Тип результата задачи</typeparam>
    /// <typeparam name="T5">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3, T4, T5)> GetAwaiter<T, T2, T3, T4, T5>(
        this (Task<T>, Task<T2>, Task<T3>, Task<T4>, Task<T5>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3, T4, T5)> CombineTasksAsync()
        {
            var (task1, task2, task3, task4, task5) = tasks;
            await Task.WhenAll(task1, task2, task3, task4, task5);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result);
        }
    }
    
    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <typeparam name="T4">Тип результата задачи</typeparam>
    /// <typeparam name="T5">Тип результата задачи</typeparam>
    /// <typeparam name="T6">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3, T4, T5, T6)> GetAwaiter<T, T2, T3, T4, T5, T6>(
        this (Task<T>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3, T4, T5, T6)> CombineTasksAsync()
        {
            var (task1, task2, task3, task4, task5, task6) = tasks;
            await Task.WhenAll(task1, task2, task3, task4, task5, task6);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result);
        }
    }
    
    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <typeparam name="T4">Тип результата задачи</typeparam>
    /// <typeparam name="T5">Тип результата задачи</typeparam>
    /// <typeparam name="T6">Тип результата задачи</typeparam>
    /// <typeparam name="T7">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3, T4, T5, T6, T7)> GetAwaiter<T, T2, T3, T4, T5, T6, T7>(
        this (Task<T>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3, T4, T5, T6, T7)> CombineTasksAsync()
        {
            var (task1, task2, task3, task4, task5, task6, task7) = tasks;
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result);
        }
    }

    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <typeparam name="T4">Тип результата задачи</typeparam>
    /// <typeparam name="T5">Тип результата задачи</typeparam>
    /// <typeparam name="T6">Тип результата задачи</typeparam>
    /// <typeparam name="T7">Тип результата задачи</typeparam>
    /// <typeparam name="T8">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3, T4, T5, T6, T7, T8)> GetAwaiter<T, T2, T3, T4, T5, T6, T7, T8>(
        this (Task<T>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>, Task<T8>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3, T4, T5, T6, T7, T8)> CombineTasksAsync()
        {
            var (task1, task2, task3, task4, task5, task6, task7, task8) = tasks;
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result);
        }
    }

    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <typeparam name="T4">Тип результата задачи</typeparam>
    /// <typeparam name="T5">Тип результата задачи</typeparam>
    /// <typeparam name="T6">Тип результата задачи</typeparam>
    /// <typeparam name="T7">Тип результата задачи</typeparam>
    /// <typeparam name="T8">Тип результата задачи</typeparam>
    /// <typeparam name="T9">Тип результата задачи</typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3, T4, T5, T6, T7, T8, T9)> GetAwaiter<T, T2, T3, T4, T5, T6, T7, T8, T9>(
        this (Task<T>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>, Task<T8>, Task<T9>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3, T4, T5, T6, T7, T8, T9)> CombineTasksAsync()
        {
            var (task1, task2, task3, task4, task5, task6, task7, task8, task9) = tasks;
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result);
        }
    }

    /// <summary>
    /// Ожидает выполнения переданных задач, возвращая результат их выполнения в виде кортежа
    /// </summary>
    /// <param name="tasks">Кортеж задач</param>
    /// <typeparam name="T">Тип результата задачи</typeparam>
    /// <typeparam name="T2">Тип результата задачи</typeparam>
    /// <typeparam name="T3">Тип результата задачи</typeparam>
    /// <typeparam name="T4">Тип результата задачи</typeparam>
    /// <typeparam name="T5">Тип результата задачи</typeparam>
    /// <typeparam name="T6">Тип результата задачи</typeparam>
    /// <typeparam name="T7">Тип результата задачи</typeparam>
    /// <typeparam name="T8">Тип результата задачи</typeparam>
    /// <typeparam name="T9">Тип результата задачи</typeparam>
    /// <typeparam name="T10">Тип результата задачи></typeparam>
    /// <returns>Кортеж с результатами</returns>
    public static TaskAwaiter<(T, T2, T3, T4, T5, T6, T7, T8, T9, T10)> GetAwaiter<T, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        this (Task<T>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>, Task<T7>, Task<T8>, Task<T9>, Task<T10>) tasks)
    {
        return CombineTasksAsync().GetAwaiter();

        async Task<(T, T2, T3, T4, T5, T6, T7, T8, T9, T10)> CombineTasksAsync()
        {
            var (task1, task2, task3, task4, task5, task6, task7, task8, task9, task10) = tasks;
            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result, task9.Result, task10.Result);
        }
    }
}