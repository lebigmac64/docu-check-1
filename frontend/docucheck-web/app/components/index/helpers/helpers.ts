import {ResultType} from "~/models/result-type";
import type {DocumentCheckState} from "~/components/hooks/use-document-check";
import type {FullResult} from "~/models/full-result";
import type {CheckResult} from "~/models/check-result";

export const calculateResult = (docState: DocumentCheckState, currentResults: CheckResult[]) : FullResult => {
    let resultType: number;
    let recordedAt: string | undefined = undefined;
    let documentType: number | undefined = undefined;

    if (currentResults.some((r) => r.ResultType === ResultType.Error)) {
        resultType = ResultType.Error;
    } else {
        const invalidResult = currentResults.find((r) => r.ResultType === ResultType.Invalid);
        if (invalidResult) {
            resultType = ResultType.Invalid;
            recordedAt = invalidResult.RecordedAt;
            documentType = invalidResult.Type;
        } else {
            resultType = ResultType.Valid;
        }
    }

    return {
        DocumentNumber: docState.docNumber,
        ResultType: resultType,
        CheckedAt: new Date().toISOString(),
        RecordedAt: recordedAt,
        DocumentType: documentType
    };
}