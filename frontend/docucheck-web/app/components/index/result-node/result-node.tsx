import {
  type CheckResult,
  getInvalidDocTypes,
  ResultType,
} from "~/components/index/document-form/document-form.module";
import React from "react";

export default function CheckResults({checkResults}: {checkResults: CheckResult[]}) {
  const invalid = checkResults.find((r) => r.ResultType === ResultType.Invalid);
  if (invalid !== undefined) {
    return (
      <div className="flex items-center gap-3 bg-red-900/30 border-l-4 w-full max-w-md border-red-500 p-3 rounded shadow transition-all duration-300">
        <div>
          <p className="text-red-400 font-semibold">
            Dokument evidován v databázi
          </p>
          <p className="text-red-400 font-semibold">
            Neplatný od: {invalid.RecordedAt}
          </p>
          <p className="text-red-200">
            Typy:{" "}
            <span className="font-bold">
              {getInvalidDocTypes(checkResults)}
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
