namespace DocuCheck.Main.Extensions;

public static class HttpContextExtensions
{
    public static async Task WriteSseFrameAsync(this HttpContext context, string eventName, object data)
    {
        var sseFrame = data.ToSseFrame(eventName);
        await context.Response.WriteAsync(sseFrame.ToString());
        await context.Response.Body.FlushAsync();
    }
}