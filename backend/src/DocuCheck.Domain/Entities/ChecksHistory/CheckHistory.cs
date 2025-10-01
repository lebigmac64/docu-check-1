using System.Diagnostics.CodeAnalysis;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

namespace DocuCheck.Domain.Entities.ChecksHistory
{
    public class CheckHistory
    {
        [SuppressMessage(
            "Design", 
            "CS8618:Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.", 
            Justification = "For EF Core")]
        #pragma warning disable CS8618
        private CheckHistory() { }
        #pragma warning restore CS8618

        private CheckHistory(
            Guid id,
            DateTime checkedAt,
            DocumentNumber number,
            ResultType resultType)
        {
            Id = id;
            CheckedAt = checkedAt;
            Number = number;
            ResultType = resultType;
        }

        public Guid Id { get; private set; }
        public DateTime CheckedAt { get; private set; }
        public DocumentNumber Number { get; private set; }
        public ResultType ResultType { get; private set; }

        public static CheckHistory Create(
            DateTime checkedAt,
            DocumentNumber number,
            ResultType resultType)
        {
            return new CheckHistory(
                Guid.NewGuid(), 
                checkedAt, 
                number, 
                resultType);
        }
    }
}