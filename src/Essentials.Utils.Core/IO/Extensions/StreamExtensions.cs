// ReSharper disable MustUseReturnValue
// ReSharper disable MemberCanBePrivate.Global

namespace Essentials.Utils.IO.Extensions;

/// <summary>
/// Методы расширения для <see cref="Stream" />
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Преобразует поток в массив байтов
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <returns></returns>
    public static byte[] ToArray(this Stream stream)
    {
        if (!stream.CanRead)
            throw new AccessViolationException("Stream cannot be read");

        return stream.CanSeek ? stream.ToArrayBytesDirect() : stream.ToArrayWithMemoryStream();
    }
    
    /// <summary>
    /// Преобразует поток в массив байтов
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public static async Task<byte[]> ToArrayAsync(this Stream stream, CancellationToken? token = null)
    {
        if (!stream.CanRead)
            throw new AccessViolationException("Stream cannot be read");

        return stream.CanSeek
            ? await stream.ToArrayBytesDirectAsync(token)
            : await stream.ToArrayWithMemoryStreamAsync(token);
    }

    /// <summary>
    /// Преобразует поток в Memory{byte}
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <returns></returns>
    public static Memory<byte> AsMemory(this Stream stream) => stream.ToArray();

    /// <summary>
    /// Преобразует поток в Memory{byte}
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public static async Task<Memory<byte>> AsMemoryAsync(this Stream stream, CancellationToken? token = null) =>
        await stream.ToArrayAsync(token);

    /// <summary>
    /// Преобразует поток в массив байтов с помощью создания массива и чтения в него потока
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <returns>Массив байтов</returns>
    /// <exception cref="ArgumentException"></exception>
    private static byte[] ToArrayBytesDirect(this Stream stream)
    {
        if (stream.Position > 0)
            throw new ArgumentException("Stream is not at the start");
        
        var buffer = new byte[stream.Length];
        stream.Read(buffer, 0, (int) stream.Length);
        return buffer;
    }

    /// <summary>
    /// Преобразует поток в массив байтов с помощью создания массива и чтения в него потока
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Массив байтов</returns>
    /// <exception cref="ArgumentException"></exception>
    private static async Task<byte[]> ToArrayBytesDirectAsync(this Stream stream, CancellationToken? token = null)
    {
        if (stream.Position > 0)
            throw new ArgumentException("Stream is not at the start");
        
        var buffer = new byte[stream.Length];
        await stream.ReadAsync(buffer.AsMemory(0, (int) stream.Length), token ?? CancellationToken.None);
        return buffer;
    }

    /// <summary>
    /// Преобразует поток в массив байтов с помощью создания <see cref="MemoryStream" />
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <returns>Массив байтов</returns>
    private static byte[] ToArrayWithMemoryStream(this Stream stream)
    {
        if (stream is MemoryStream memoryStream)
            return memoryStream.ToArray();
        
        using var newStream = new MemoryStream();
        stream.CopyTo(newStream);
        return newStream.ToArray();
    }

    /// <summary>
    /// Преобразует поток в массив байтов с помощью создания <see cref="MemoryStream" />
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Массив байтов</returns>
    private static async Task<byte[]> ToArrayWithMemoryStreamAsync(this Stream stream, CancellationToken? token = null)
    {
        if (stream is MemoryStream memoryStream)
            return memoryStream.ToArray();
        
        using var newStream = new MemoryStream();
        await stream.CopyToAsync(newStream, token ?? CancellationToken.None);
        return newStream.ToArray();
    }
}