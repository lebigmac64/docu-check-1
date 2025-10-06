import React from "react";
import {ResultType} from "~/models/result-type";
import type {CheckResult} from "~/models/check-result";


export default function InfoCard({results} :{ results: CheckResult[]})
    {
    if (results.some(x => x.ResultType === ResultType.Error))
        return (<div
            className="flex items-center gap-3 bg-red-900/30 border-l-4 border-red-400 p-3 rounded shadow transition-all duration-300">
            <p>
                Nastala chyba při kontrole dokumentu
            </p>
        </div>)

    const invalid = results.find(x => x.ResultType === ResultType.Error);
    let type = "Neznámý typ dokumentu";
    switch (invalid?.Type) {
        case 0:
            type = "Občanský průkaz";
            break;
        case 4: type = "Pas";
            break;
        case 6: type = "Zbrojní průkaz";
            break;
        default: break;
    }
    if (invalid)
        return <div
            className="flex items-center gap-3 bg-red-900/30 border-l-4 border-amber-300 p-3 rounded shadow transition-all duration-300">
            <p>
                Nalezen evidovaný dokument typu:
            </p>
            <p>
                {type}
            </p>
        </div>

    return <></>
}