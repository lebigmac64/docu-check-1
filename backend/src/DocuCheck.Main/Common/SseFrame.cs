using System.Text.Json;

namespace DocuCheck.Main.Common;

public record SseFrame(string Id, string Event, object Data)
{
    public override string ToString()
    {
        return $"id: {Id}\nevent: {Event}\ndata: {JsonSerializer.Serialize(Data)}\n\n";
    }
};