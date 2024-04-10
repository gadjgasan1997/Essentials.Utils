namespace Essentials.Utils.Dictionaries;

/// <summary>
/// Причины исключения кода из расчета процента покрытия тестами
/// </summary>
public static class TestsCoverageExcludeReasons
{
    /// <summary>
    /// Метод, осуществляющий исключительно логирование
    /// </summary>
    public const string LOG_METHOD_REASON =
        "Метод содержит исключительно логирование, поэтому был исключен из расчета процента покрытия тестами";
    
    /// <summary>
    /// Метод, осуществляющий исключительно конфигурацию сервисов
    /// </summary>
    public const string CONFIGURE_SERVICES_METHOD_REASON =
        "Метод содержит исключительно конфигурацию сервисов, поэтому был исключен из расчета процента покрытия тестами";
    
    /// <summary>
    /// Класс, осуществляющий исключительно конфигурацию сервисов
    /// </summary>
    public const string CONFIGURE_SERVICES_CLASS_REASON =
        "Класс содержит исключительно конфигурацию сервисов, поэтому был исключен из расчета процента покрытия тестами";
    
    /// <summary>
    /// Класс, являющийся точкой входа в приложение
    /// </summary>
    public const string ENTRYPOINT_CLASS_REASON =
        "Класс является точкой входа в приложение, поэтому был исключен из расчета процента покрытия тестами";
    
    /// <summary>
    /// Класс, являющийся миграцией
    /// </summary>
    public const string MIGRATION_CLASS_REASON =
        "Класс является миграцией, поэтому был исключен из расчета процента покрытия тестами";
}