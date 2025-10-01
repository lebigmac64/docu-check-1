import DocumentInput from "~/components/index/document-input/document-input";
import React, {useState} from "react";
import type {FullResult} from "~/components/index/document-form/document-form.module";
import ResultNode from "~/components/index/result-node/result-node";

export default function DocumentForm(): React.ReactElement {
    const [results, setResults] = useState([] as FullResult[]);

  return (
      <>
    <div className="flex flex-col m-20 items-center justify-center  bg-[#2B2D3A] text-[#E6E6E6] font-sans">
      <div className="bg-[#313445] p-8 rounded-xl m-4 gap-5 max-w-md shadow-2xl border border-[#3D4052]">
        <DocumentInput onResultsChanged={setResults}/>
          <ul>
              {results.map((result, index) => (
                  <li key={index} className="mb-4 p-6 border min-w-80 border-[#3D4052] rounded-xl bg-gradient-to-br from-[#232431] to-[#1a1b22] shadow-lg">
                      <div className="flex items-center justify-between mb-2">
                          <h5 className="text-sm font-bold text-[#e0e0e0]">
                              Výsledek kontroly #{results.length - index}
                          </h5>
                          <span className="text-xs text-gray-400">
                {new Date(result.CheckedAt).toLocaleString("cs-CZ")}
              </span>
                      </div>
                      <h5 className="text-sm mb-3">Č. {result.DocumentNumber}</h5>
                      <ResultNode checkResults={result.CheckResults} />
                  </li>
              ))}
          </ul>
      </div>
    </div>
    </>
  );
}
