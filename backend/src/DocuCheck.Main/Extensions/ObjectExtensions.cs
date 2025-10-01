using System.Text.Json;
using DocuCheck.Main.Common;

namespace DocuCheck.Main.Extensions;

public static class ObjectExtensions
{
    public static SseFrame ToSseFrame(this object obj, string eventName)
    {
        return new SseFrame(
            Id: Guid.NewGuid().ToString(),
            Event: eventName,
            Data: JsonSerializer.Serialize(obj)
        );
    }
}