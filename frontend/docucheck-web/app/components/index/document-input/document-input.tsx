import ProgressBar from "~/components/index/progress-bar/progress-bar";
import {
    type CheckResult,
    type FullResult,
    getInvalidDocTypes,
    ResultType
} from "~/components/index/document-form/document-form.module";
import React, {useEffect, useRef} from "react";
import {API_ROOT} from "~/config";
import useDocumentCheck from "~/components/hooks/use-document-check";

export default function DocumentInput({onResultsChanged}: {
    onResultsChanged: React.Dispatch<React.SetStateAction<FullResult[]>>;
}): React.ReactElement {
    const [docState, startCheck, resetCheck, setCurrentResults, setTotal, setDocNumber] = useDocumentCheck();
    const esRef = useRef<EventSource | null>(null);
    const currentResultsRef = useRef<CheckResult[]>([]);

    useEffect(() => {
        currentResultsRef.current = docState.currentResults;
    }, [docState.currentResults]);

    useEffect(() => {
        if (!docState.isSubmitting) return;

        const url = new URL(
            `${API_ROOT}api/documents/check/${docState.docNumber}`,
        );
        const es = new EventSource(url);
        esRef.current = es;

        es.onerror = (err) => {
            console.error("EventSource failed:", err);
            es.close();
            resetCheck();
        }

        es.addEventListener("total", (e: MessageEvent) => {
            const parsed = JSON.parse(e.data) as { total: number };
            setTotal(parsed.total);
        });

        es.addEventListener("checkResult", (e: MessageEvent) => {
            const parsed = JSON.parse(e.data) as CheckResult;
            setCurrentResults((last) => [parsed, ...last]);
        });

        es.addEventListener("done", (e: MessageEvent) => {
            const fullResult = {
                DocumentNumber: docState.docNumber,
                CheckResults: currentResultsRef.current,
                CheckedAt: new Date().toISOString(),
            } as FullResult;

            es.close();
            onResultsChanged((last) => [fullResult, ...last]);
            resetCheck();
        });

        return () => {
            es.close();
        };
    }, [docState.isSubmitting]);

    const handleSubmit = async (
        e: React.MouseEvent<HTMLButtonElement>,
    ): Promise<void> => {
        e.preventDefault();
        startCheck(docState.docNumber.trim());
    };

    function handleCancel(e: React.MouseEvent<HTMLButtonElement>) {
        e.preventDefault();
        esRef.current?.close();
        resetCheck();
    }


    return <div className="mb-8">
        <h1 className="text-xl font-semibold mb-6 text-center">
            Ověření vůči databázi neplatných dokladů
        </h1>
        <form className="flex flex-col gap-6">
            <div className="flex flex-col gap-2">
                <label htmlFor="doc" className="text-sm text-[#B6BAC5]">
                    Číslo dokumentu
                </label>
                <input
                    disabled={docState.isSubmitting}
                    id="doc"
                    className="bg-[#2A2C39] border border-[#3D4052] text-[#E6E6E6] px-4 py-3 rounded-lg doc-number-input
                            focus:border-[#21BFC2] focus:ring-3 focus:ring-[#21BFC2]/30 outline-none transition"
                    placeholder="např. AB123456"
                    value={docState.docNumber}
                    onChange={(e) => setDocNumber(e.target.value)}
                />
            </div>
            {docState.isSubmitting && (
                <>
                    <ProgressBar
                        text={"Ověřuji..."}
                        checked={docState.currentResults.length + 1}
                        total={docState.total}
                    />
                </>
            )}
            <button
                disabled={!docState.docNumber.trim()}
                onClick={docState.isSubmitting ? handleCancel : handleSubmit}
                className="bg-[#A77CFF] text-[#0E1015] font-semibold py-3 rounded-lg shadow-md mt-4 transition hover:red-400 hover:pink-600"
            >
                {docState.isSubmitting ? "Zrušit" : "Ověřit"}
            </button>
            {docState.currentResults.some(
                (cr) => cr.ResultType === ResultType.Invalid,
            ) && (
                <div className="flex items-center gap-3 bg-red-900/30 border-l-4 border-red-400 p-3 rounded shadow transition-all duration-300">
                    <p>
                        Nalezen evidovaný dokument typu:
                    </p>
                    <p>
                        {getInvalidDocTypes(docState.currentResults)}
                    </p>
                </div>
            )}
        </form>
    </div>
}