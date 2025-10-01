using System.Xml.Serialization;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;

namespace DocuCheck.Infrastructure.Clients.Responses;

[XmlRoot("doklady_neplatne")]
public class CheckDocumentValidityDto
{
    [XmlAttribute("posl_zmena")] 
    public string? LastChangeRaw { get; set; }

    [XmlAttribute("pristi_zmeny")] 
    public string? NextChangeRaw { get; set; }
    
    [XmlElement("chyba")] 
    public ErrorNode? Error { get; set; }
    
    [XmlElement("dotaz")] 
    public QueryNode? Query { get; set; }
    
    [XmlElement("odpoved")] 
    public AnswerNode? Answer { get; set; }
    
    public CheckResult ParseCheckResult(DocumentType type)
    {
        return this switch
        {
            { Error: not null } => CheckResult.Error(type, Error.Message ?? "Unknown error"),
            { Answer.IsRecorded: true } => CheckResult.Invalid(type, Answer.RecordedAtRaw ?? ""),
            { Answer.IsRecorded: false } => CheckResult.Valid(type),
            _ => throw new InvalidOperationException("Unexpected response from Ministry of Interior API")
        };
    }
}

public class ErrorNode
{
    [XmlText] 
    public string? Message { get; set; }
}

public class QueryNode
{
    [XmlAttribute("typ")] 
    public string? Type { get; set; }

    [XmlAttribute("cislo")] 
    public string? Number { get; set; }

    [XmlAttribute("serie")] 
    public string? Series { get; set; }
}

public class AnswerNode
{
    [XmlAttribute("aktualizovano")] 
    public string? UpdatedRaw { get; set; }

    [XmlAttribute("evidovano_od")]
    public string? RecordedAtRaw { get; set; }
    
    [XmlAttribute("evidovano")] 
    public string? RecordedRaw { get; set; }

    [XmlIgnore]
    public bool IsRecorded => string.Equals(RecordedRaw, "ano",
        StringComparison.OrdinalIgnoreCase);
}