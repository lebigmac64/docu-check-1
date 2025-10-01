using System.Xml.Serialization;

namespace DocuCheck.Infrastructure.Helpers;

public static class XmlHelper
{
    public static T? FromXml<T>(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return default;
        }

        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(value);
        return serializer.Deserialize(reader) is T result ? result : default;
    }
}
