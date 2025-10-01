export type CheckResult = {
  ResultType: number;
  Type: number;
  CheckedAt: string;
  RecordedAt?: string;
};

export type FullResult = {
  DocumentNumber?: string;
  CheckResults: CheckResult[];
  CheckedAt: string;
};

export const ResultType = {
  Invalid: 0,
  Valid: 1,
};

export const getDocumentType = (type: number): string => {
  switch (type) {
    case 0:
      return "Občanský průkaz";
    case 4:
      return "Pas";
    case 6:
      return "Zbrojní průkaz";
  }

  return "Neznámý typ dokumentu";
};

export const getResultText = (result: CheckResult): string => {
  switch (result.ResultType) {
    case ResultType.Invalid:
      return `Neplatný dokument \n(${getDocumentType(result.Type)})`;
    case ResultType.Valid:
      return `Platný dokument \n(${getDocumentType(result.Type)})`;
    default:
      return "Neznámý výsledek";
  }
};

export const parseDate = (dateString: string): string => {
  return new Date(dateString).toLocaleString(undefined, {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  });
};

export function getInvalidDocTypes(results: CheckResult[]): string[] {
  const invalidDocs = results.filter((r) => r.ResultType === 0);
  return invalidDocs.map((doc) => getDocumentType(doc.Type));
}
