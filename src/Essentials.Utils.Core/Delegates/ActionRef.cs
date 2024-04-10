namespace Essentials.Utils.Delegates;

/// <summary>
/// Делегат, принимающий на вход объект по ссылке
/// </summary>
/// <typeparam name="T">Тип объекта</typeparam>
public delegate void ActionRef<T>(ref T arg);