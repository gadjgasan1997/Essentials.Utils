using System.Buffers;
using System.Buffers.Text;
using static System.Text.Encoding;
// ReSharper disable ArrangeAccessorOwnerBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConvertToAutoProperty

namespace Essentials.Utils;

/// <summary>
/// Хелперы для работы с пулом массивов
/// </summary>
public static class ArrayPoolHelper
{
    /// <summary>
    /// Возвращает буфер, с минимальной длиной, равной <paramref name="minimumLength" />
    /// </summary>
    /// <param name="minimumLength">Минимальная требуемая длина</param>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <returns>Буфер</returns>
    public static SharedObject<T> Rent<T>(int minimumLength) => new(minimumLength);
    
    /// <summary>
    /// Преобразует строку в формате base64 в массив байтов
    /// </summary>
    /// <param name="value">Строка</param>
    /// <param name="bytesWritten">Количество записанных байтов</param>
    /// <returns>Массив</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static SharedObject<byte> FromBase64String(string value, out int bytesWritten)
    {
        using var buffer = Rent<byte>(UTF8.GetMaxByteCount(value.Length));
        var bufferSize = UTF8.GetBytes(value, buffer.Value);
        var decodedBuffer = Rent<byte>(Base64.GetMaxDecodedFromUtf8Length(value.Length));
        
        try
        {
            Base64.DecodeFromUtf8(
                buffer.Value.AsSpan(0, bufferSize),
                decodedBuffer.Value,
                out _,
                out bytesWritten);
            
            if (bytesWritten == 0)
                throw new InvalidOperationException("Error writing to buffer");
        }
        catch
        {
            decodedBuffer.Dispose();
            throw;
        }
        
        return decodedBuffer;
    }
    
    /// <summary>
    /// Преобразует массив байтов в base64 строку
    /// </summary>
    /// <param name="value">Массив</param>
    /// <returns>Строка</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string ToBase64String(ReadOnlySpan<byte> value)
    {
        using var encodedBuffer = Rent<byte>(Base64.GetMaxEncodedToUtf8Length(value.Length));
        Base64.EncodeToUtf8(value, encodedBuffer.Value, out _, out var bytesWritten);
        
        if (bytesWritten == 0)
            throw new InvalidOperationException("Error writing to buffer");

        return UTF8.GetString(encodedBuffer.Value.AsSpan(0, bytesWritten));
    }
    
    /// <summary>
    /// Буфер
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public readonly struct SharedObject<T> : IDisposable
    {		
        private readonly T[] _value;

        internal SharedObject(int minimumLength)
        {
            _value = ArrayPool<T>.Shared.Rent(minimumLength);
        }

        /// <summary>
        /// Значение
        /// </summary>
        public T[] Value
        {
            get
            {
                return _value;
            }
        }
		
        /// <inheritdoc cref="IDisposable.Dispose" />
        public void Dispose() => ArrayPool<T>.Shared.Return(Value);
    }
}