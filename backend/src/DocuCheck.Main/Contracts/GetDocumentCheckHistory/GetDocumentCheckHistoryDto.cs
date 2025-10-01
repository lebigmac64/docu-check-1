namespace DocuCheck.Main.Contracts.GetDocumentCheckHistory;

public record GetDocumentCheckHistoryDto(
    int TotalCount,
    int PageNumber,
    int PageSize,
    GetDocumentCheckHistoryItemDto[] Items);
    
    public record GetDocumentCheckHistoryItemDto(
        string Id,
        string CheckedAt,
        string DocumentNumber,
        int ResultType);