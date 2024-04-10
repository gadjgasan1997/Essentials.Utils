using System.Text;

namespace Essentials.Utils.IO.Writers;

/// <summary>
/// Райтер с возможностью проставления кодировки
/// </summary>
public class StringWriterWithEncoding : StringWriter
{
    /// <summary>
    /// Кодировка
    /// </summary>
    public override Encoding Encoding { get; }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="encoding">Кодировка</param>
    public StringWriterWithEncoding(Encoding encoding)
    {
        Encoding = encoding;
    }
}