using static System.DateTime;

namespace Essentials.Utils.Date.Helpers;

/// <summary>
/// Хелперы для работы с датой и временем
/// </summary>
public static class DateTimeHelpers
{
    /// <summary>
    /// Возвращает первый день месяца
    /// </summary>
    /// <param name="month">Месяц</param>
    /// <returns>Первый день месяца</returns>
    public static DateTime GetMonthFirstDay(DateTime month) =>
        new DateTime(month.Year, month.Month, 1, month.Hour, month.Minute, month.Second).Date;

    /// <summary>
    /// Определяет, что дата находится в текущем месяце
    /// </summary>
    /// <param name="dateTime">Дата</param>
    /// <returns>Признак, находится ли дата в текущем месяце</returns>
    public static bool IsCurrentMonth(DateTime dateTime) =>
        dateTime.Year == Now.Year && dateTime.Month == Now.Month;
    
    /// <summary>
    /// Возвращает название месяца в предложном падеже
    /// </summary>
    /// <param name="dateTime">Текущая дата</param>
    /// <returns>Название месяца</returns>
    public static string GetPrepositionalMonthName(DateTime dateTime) =>
        dateTime.Month switch
        {
            1 => "Январе",
            2 => "Феврале",
            3 => "Марте",
            4 => "Апреле",
            5 => "Мае",
            6 => "Июне",
            7 => "Июле",
            8 => "Августе",
            9 => "Сентябре",
            10 => "Октябре",
            11 => "Ноябре",
            12 => "Декабре",
            _ => throw new KeyNotFoundException("Дата имеет неизвестный месяц")
        };
}