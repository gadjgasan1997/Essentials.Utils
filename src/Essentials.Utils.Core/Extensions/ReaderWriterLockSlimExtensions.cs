namespace Essentials.Utils.Extensions;

/// <summary>
/// Методы расширения для <see cref="ReaderWriterLockSlim" />
/// </summary>
public static class ReaderWriterLockSlimExtensions
{
    /// <summary>
    /// Использовать блокировку для чтения
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <returns></returns>
    public static DisposeActionWrapper UseReadLock(this ReaderWriterLockSlim readerWriterLockSlim)
    {
        readerWriterLockSlim.CheckNotNull();

        readerWriterLockSlim.EnterReadLock();
        return new DisposeActionWrapper(readerWriterLockSlim.ExitReadLock);
    }

    /// <summary>
    /// Использовать блокировку для чтения
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static DisposeActionWrapper? TryUseReadLock(
        this ReaderWriterLockSlim readerWriterLockSlim,
        TimeSpan timeout)
    {
        readerWriterLockSlim.CheckNotNull();

        return readerWriterLockSlim.TryEnterReadLock(timeout)
            ? new DisposeActionWrapper(readerWriterLockSlim.ExitReadLock)
            : null;
    }
    
    /// <summary>
    /// Использовать блокировку для записи
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <returns></returns>
    public static DisposeActionWrapper UseWriteLock(this ReaderWriterLockSlim readerWriterLockSlim)
    {
        readerWriterLockSlim.CheckNotNull();
            
        readerWriterLockSlim.EnterWriteLock();
        return new DisposeActionWrapper(readerWriterLockSlim.ExitWriteLock);
    }

    /// <summary>
    /// Использовать блокировку для записи
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static DisposeActionWrapper? TryUseWriteLock(
        this ReaderWriterLockSlim readerWriterLockSlim,
        TimeSpan timeout)
    {
        readerWriterLockSlim.CheckNotNull();
            
        return readerWriterLockSlim.TryEnterWriteLock(timeout)
            ? new DisposeActionWrapper(readerWriterLockSlim.ExitWriteLock)
            : null;
    }
    
    /// <summary>
    /// Использовать блокировку для чтения с возможностью повышения до записи
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <returns></returns>
    public static DisposeActionWrapper UseUpgradeableReadLock(this ReaderWriterLockSlim readerWriterLockSlim)
    {
        readerWriterLockSlim.CheckNotNull();

        readerWriterLockSlim.EnterUpgradeableReadLock();
        return new DisposeActionWrapper(readerWriterLockSlim.ExitUpgradeableReadLock);
    }

    /// <summary>
    /// Использовать блокировку для чтения с возможностью повышения до записи
    /// </summary>
    /// <param name="readerWriterLockSlim"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static DisposeActionWrapper? TryUseUpgradeableReadLock(
        this ReaderWriterLockSlim readerWriterLockSlim,
        TimeSpan timeout)
    {
        readerWriterLockSlim.CheckNotNull();

        return readerWriterLockSlim.TryEnterUpgradeableReadLock(timeout)
            ? new DisposeActionWrapper(readerWriterLockSlim.ExitUpgradeableReadLock)
            : null;
    }
}