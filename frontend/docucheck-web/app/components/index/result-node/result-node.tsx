import React from "react";
import {ResultType} from "~/models/result-type";
import type {FullResult} from "~/models/full-result";

export default function ResultNode({result}: {result: FullResult}) {
    if (result.ResultType === ResultType.Error)
        return (
            <div className="flex items-center gap-3 bg-red-900/30 border-l-4 w-full max-w-md border-red-500 p-3 rounded shadow transition-all duration-300">
                <div>
                    <p className="text-red-400 font-semibold">
                        Nastala chyba při kontrole dokladu
                    </p>
                </div>
            </div>
        );

  if (result.ResultType === ResultType.Invalid) {
    let type;
    console.log(result.DocumentType)
      switch (result.DocumentType) {
          case 0:
              type = "Občanský průkaz";
              break;
          case 4:
              type = "Pas";
              break;
          case 6:
              type = "Zbrojní průkaz";
              break;
      }
    return (
      <div className="flex items-center gap-3 bg-yellow-500/30 border-l-4 w-full max-w-md border-amber-500 p-3 rounded shadow transition-all duration-300">
        <div>
          <p className="text-white-400 font-semibold">
            Dokument evidován v databázi
          </p>
          <p className="text-white-400 font-semibold">
            Neplatný od: {result.RecordedAt ?? ""}
          </p>
          <p className="text-red-200">
            Typy:
            <span className="font-bold">
              {type}
            </span>
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="flex items-center gap-3 bg-green-900/30 border-l-4 border-green-500 p-3 rounded">
      <p className="text-green-300 font-semibold">Nenalezen v databázi</p>
    </div>
  );
}
