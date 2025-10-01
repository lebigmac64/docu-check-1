namespace DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

public class DocumentNumber
{
    private DocumentNumber()
    {
        // For EF Core
    }
   
    private DocumentNumber(string value)
    {
        Value = value;
    }

    public string Value { get; private set; } = string.Empty;
    
    public static DocumentNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Document number cannot be null or empty.", nameof(value));
        }
        return new DocumentNumber(value);
    }
}