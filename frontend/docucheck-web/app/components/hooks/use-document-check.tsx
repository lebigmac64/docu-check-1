import {useCallback, useState} from "react";
import type {CheckResult} from "~/components/index/document-form/document-form.module";

type DocumentCheckState = {
    docNumber: string;
    isSubmitting: boolean;
    currentResults: CheckResult[];
    total: number;
};

export default function useDocumentCheck() {
    const [state, setState] = useState<DocumentCheckState>({
        docNumber: "",
        isSubmitting: false,
        currentResults: [] as CheckResult[],
        total: 0,
    });

    const setDocNumber = useCallback((docNumber: string) => {
        setState((prevState) => (
            {
                ...prevState,
                docNumber
            }));
    }, []);

    const startCheck = useCallback((docNumber: string) => {
        setState((prevState) => (
            {
                ...prevState,
                isSubmitting: true,
                docNumber
            }));
    }, []);

    const resetCheck = useCallback(() => {
        window.dispatchEvent(new CustomEvent("ProgressBarReset"));
        setState({
            docNumber: "",
            isSubmitting: false,
            currentResults: [],
            total: 0,
        });
    }, []);

    const setCurrentResults = useCallback((updater: (prev: CheckResult[]) => CheckResult[]) => {
        setState((prevState) => ({
            ...prevState,
            currentResults: updater(prevState.currentResults),
        }));
    }, []);

    const setTotal = useCallback((total: number) => {
        setState((prevState) => ({
            ...prevState,
            total,
        }));
    }, []);

    return [state, startCheck, resetCheck, setCurrentResults, setTotal, setDocNumber] as const;
}