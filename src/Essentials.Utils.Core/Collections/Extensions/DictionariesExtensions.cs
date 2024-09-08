using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Essentials.Utils.Delegates;
using Essentials.Utils.Extensions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable OutParameterValueIsAlwaysDiscarded.Global

namespace Essentials.Utils.Collections.Extensions;

/// <summary>
/// Методы расширений для справочников
/// </summary>
public static class DictionariesExtensions
{
    /// <summary>
    /// Преобразует справочник в справочник только для чтения 
    /// </summary>
    /// <param name="dictionary">Справочник</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    /// <returns></returns>
    public static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        return new ReadOnlyDictionary<TKey, TValue>(dictionary);
    }
    
    /// <summary>
    /// Выполняет действие для всех элементов справочника, удовлетворяющих условию
    /// </summary>
    /// <param name="self">Справочник</param>
    /// <param name="predicate">Условие</param>
    /// <param name="action">Действие</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип события</typeparam>
    /// <returns></returns>
    public static void ForEach<TKey, TValue>(
        this Dictionary<TKey, TValue> self,
        Func<KeyValuePair<TKey, TValue>, bool> predicate,
        Action<KeyValuePair<TKey, TValue>> action)
        where TKey : notnull
    {
        foreach (var pair in self.Where(predicate))
            action(pair);
    }
    
    /// <summary>
    /// Объединяет справочники
    /// </summary>
    /// <param name="dictionaries">Справочники</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    /// <returns>Результирующий справочник</returns>
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        this IEnumerable<Dictionary<TKey, TValue>> dictionaries)
        where TKey : notnull
    {
        return dictionaries
            .SelectMany(dictionary => dictionary)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
    
    /// <summary>
    /// Объединяет справочники
    /// </summary>
    /// <param name="sourceDictionary">Исходный справочник</param>
    /// <param name="newDictionary">Новый справочник</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    /// <returns>Результирующий справочник</returns>
    public static Dictionary<TKey, TValue> Concat<TKey, TValue>(
        this Dictionary<TKey, TValue> sourceDictionary,
        Dictionary<TKey, TValue> newDictionary)
        where TKey : notnull
    {
        return new [] {sourceDictionary, newDictionary}.Merge();
    }
    
    /// <summary>
    /// Добавляет элемент в справочник или заменяет его по ключу
    /// </summary>
    /// <param name="dictionary">Справочник</param>
    /// <param name="key">Ключ</param>
    /// <param name="value">Значение</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    public static void AddOrUpdate<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary,
        TKey key,
        ref TValue value)
        where TKey : notnull
    {
        key.CheckKeyNotEmpty();

        ref var existsOrNewValue = ref dictionary.GetValueRefOrAddDefault(key, out _);
        existsOrNewValue = value;
    }

    /// <summary>
    /// Пытается изменить элемент
    /// </summary>
    /// <param name="dictionary">Справочник</param>
    /// <param name="key">Ключ</param>
    /// <param name="updateValueAction">Действие обновления элемента, если он был найден</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    /// <returns>Признак, удалось ли найти элемент</returns>
    public static bool TryUpdate<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary,
        TKey key,
        ActionRef<TValue> updateValueAction)
        where TKey : notnull
    {
        key.CheckKeyNotEmpty();
        
        ref var existsOrNullValue = ref dictionary.GetValueRefOrNullRef(key);
        if (Unsafe.IsNullRef(ref existsOrNullValue))
            return false;

        updateValueAction(ref existsOrNullValue);
        return true;
    }

    /// <summary>
    /// Возвращает ссылку на значение в справочнике или null ссылку, если такого значения нет
    /// </summary>
    /// <param name="dictionary">Справочник</param>
    /// <param name="key">Ключ</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    /// <returns>Ссылка на значение</returns>
    public static ref TValue GetValueRefOrNullRef<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary,
        TKey key)
        where TKey : notnull
    {
        key.CheckKeyNotEmpty();
        return ref CollectionsMarshal.GetValueRefOrNullRef(dictionary, key);
    }

    /// <summary>
    /// Возвращает ссылку на значение в справочнике, добавляя новый элемент со значением по-умолчанию при его отсутствии
    /// </summary>
    /// <param name="dictionary">Справочник</param>
    /// <param name="key">Ключ</param>
    /// <param name="exists">Признак, что элемент имеется в справочнике</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    /// <returns>Ссылка на значение</returns>
    public static ref TValue? GetValueRefOrAddDefault<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary,
        TKey key,
        out bool exists)
        where TKey : notnull
    {
        key.CheckKeyNotEmpty();
        return ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, key, out exists);
    }
    
    /// <summary>
    /// Проверяет что ключ не пустой
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    private static void CheckKeyNotEmpty<TKey>(this TKey key) where TKey : notnull =>
        key.CheckNotNull("Ключ не должен быть пустым (key != null)");
}