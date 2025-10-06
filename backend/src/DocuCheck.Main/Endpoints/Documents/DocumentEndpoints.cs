using System.Globalization;
using DocuCheck.Application.Common;
using DocuCheck.Application.Handlers.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Main.Contracts.CheckDocument;
using DocuCheck.Main.Contracts.GetDocumentCheckHistory;
using DocuCheck.Main.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocuCheck.Main.Endpoints.Documents
{
    public static class DocumentEndpoints
    {
        public static void MapDocumentEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("api/documents/check/{documentNumber}",
                async ([FromRoute] string documentNumber, 
                        HttpContext ctx, 
                        IDocumentHandler documentHandler) =>
                {
                    SetSseResponseHeaders(ctx);
                    var total = Enum.GetValues<DocumentType>().Length;
                    await ctx.WriteSseFrameAsync("total", new { Total = total });
                    
                    await foreach (var result in documentHandler.CheckDocumentAsync(documentNumber).WithCancellation(ctx.RequestAborted))
                    {
                        var dto = MapCheckResultDocumentCheckResultDto(result);
                        await ctx.WriteSseFrameAsync("checkResult", dto);
                    }

                    var sseDone = new { Message = "Document check completed" }.ToSseFrame("done");
                    await ctx.WriteSseFrameAsync("done", sseDone);
                });
            
            routeBuilder.MapGet("api/documents/history",            
                async (IDocumentHandler documentHandler, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) =>
                {
                    var history = await documentHandler.GetDocumentCheckHistoryAsync(pageNumber, pageSize);
                    var dto = MapCheckHistoryGetDocumentCheckHistoryDto(history);
                    
                    return Results.Ok(dto);
                });
        }

        private static void SetSseResponseHeaders(HttpContext ctx)
        {
            ctx.Response.ContentType = "text/event-stream";
            ctx.Response.Headers.CacheControl = "no-cache";
        }

        private static GetDocumentCheckHistoryDto MapCheckHistoryGetDocumentCheckHistoryDto(PagedResult<CheckHistory> pagedHistory)
        {
            var response = new GetDocumentCheckHistoryDto(
                TotalCount: pagedHistory.TotalCount.Value,
                PageNumber: pagedHistory.PageNumber.Value,
                PageSize: pagedHistory.PageSize.Value,
                    Items: [.. pagedHistory.Items.Select(ch => new GetDocumentCheckHistoryItemDto(
                    Id: ch.Id.ToString(),
                    CheckedAt: ch.CheckedAt.ToString(CultureInfo.InvariantCulture),
                    DocumentNumber: ch.Number.Value,
                    ResultType: (int)ch.ResultType))]);

            return response;
        }

        private static DocumentCheckResultDto MapCheckResultDocumentCheckResultDto(CheckResult result)
        {
            var data = new DocumentCheckResultDto(
                ResultType: (byte)result.ResultType,
                Type: (byte)result.Type,
                CheckedAt: DateTime.UtcNow,
                RecordedAt: result.RecordedAtRaw);

            return data;
        }
    }
}