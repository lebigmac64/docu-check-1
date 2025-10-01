using System.Globalization;
using System.Text.Json;
using DocuCheck.Application.Common;
using DocuCheck.Application.Services.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Main.Contracts.GetDocumentCheckHistory;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DocumentCheckResultDto = DocuCheck.Main.Contracts.CheckDocument.DocumentCheckResultDto;

namespace DocuCheck.Main.Endpoints.Documents
{
    public static class DocumentEndpoints
    {
        public static void MapDocumentEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("api/documents/check/{documentNumber}",
                async
                    ([FromRoute] string documentNumber, 
                        HttpContext ctx, 
                        IDocumentService documentService, 
                        CancellationToken cancellationToken = default) =>
                {
                    ctx.Response.ContentType = "text/event-stream";
                    ctx.Response.Headers.CacheControl = "no-cache";

                    var total = Enum.GetValues<DocumentType>().Length;
                    var sseInit = new SseFrame(
                        Id: Guid.NewGuid().ToString(),
                        Event: "total",
                        Data: new { Total = total }
                    );
                    await ctx.Response.WriteAsync(sseInit.ToString(), cancellationToken: cancellationToken);
                    await ctx.Response.Body.FlushAsync(cancellationToken);
                    
                    await foreach (var result in documentService.CheckDocumentAsync(documentNumber).WithCancellation(cancellationToken))
                    {
                        var dto = MapCheckResultDocumentCheckResultDto(result);
                        var sseFrame = new SseFrame(
                            Id: Guid.NewGuid().ToString(),
                            Event: "checkResult",
                            Data: dto
                        ).ToString();
                        await ctx.Response.WriteAsync(sseFrame.ToString(), cancellationToken: cancellationToken);
                        await ctx.Response.Body.FlushAsync(cancellationToken);
                    }

                    var sseDone = new SseFrame(
                        Id: Guid.NewGuid().ToString(),
                        Event: "done",
                        Data: new { Message = "Document check completed" }
                    );
                    await ctx.Response.WriteAsync(sseDone.ToString(), cancellationToken: cancellationToken);
                    await ctx.Response.Body.FlushAsync(cancellationToken);
                });
            
            routeBuilder.MapGet("api/documents/history",            
                async (IDocumentService documentService, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) =>
                {
                    var history = await documentService.GetDocumentCheckHistoryAsync(pageNumber, pageSize);
                    var dto = MapCheckHistoryGetDocumentCheckHistoryDto(history);
                    
                    return Results.Ok(dto);
                });
        }

        private record SseFrame(string Id, string Event, object Data)
        {
            public override string ToString()
            {
                return $"id: {Id}\nevent: {Event}\ndata: {JsonSerializer.Serialize(Data)}\n\n";
            }
        };
        
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