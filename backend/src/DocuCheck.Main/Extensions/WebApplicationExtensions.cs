using System.Net;
using DocuCheck.Main.Endpoints;
using DocuCheck.Main.Endpoints.Documents;
using Microsoft.AspNetCore.Diagnostics;

namespace DocuCheck.Main.Extensions;

public static class WebApplicationExtensions
{
    internal static void ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.ConfigureExceptionHandler();
        app.MapDocumentEndpoints();
    }

    private static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                        title = "An error occurred while processing your request.",
                        status = context.Response.StatusCode,
                        detail = contextFeature.Error.Message
                    });
                }
            });
        });
    }
}