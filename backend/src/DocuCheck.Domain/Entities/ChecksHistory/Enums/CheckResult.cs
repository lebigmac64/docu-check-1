
namespace DocuCheck.Domain.Entities.ChecksHistory.Enums;

public record CheckResult
{
    private CheckResult(
        ResultType ResultType,
        DocumentType Type,
        string RecordedAtRaw = "")
    {
        this.ResultType = ResultType;
        this.Type = Type;
        this.RecordedAtRaw = RecordedAtRaw;
    }
    
    public ResultType ResultType { get; init; }
    public DocumentType Type { get; init; }
    public string RecordedAtRaw { get; init; }  

    public static CheckResult Error(DocumentType type, string message) => new(ResultType.Error, type);
    public static CheckResult Valid(DocumentType type) => new(ResultType.Valid, type);
    public static CheckResult Invalid(DocumentType type, string recordedAtRaw) => new(ResultType.Invalid, type, recordedAtRaw);
}