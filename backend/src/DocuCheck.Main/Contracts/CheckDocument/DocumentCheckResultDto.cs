namespace DocuCheck.Main.Contracts.CheckDocument;

public record DocumentCheckResultDto(byte ResultType, byte Type, DateTime CheckedAt, string RecordedAt);