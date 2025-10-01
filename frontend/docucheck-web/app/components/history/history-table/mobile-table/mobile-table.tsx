import StatusChip from "~/components/history/status-chip/status-chip";
import { getDocumentType } from "~/components/index/document-form/document-form.module";
import React from "react";
import type { HistoryRecord } from "~/components/history/history-table/models/history-record";
import PaginationBar from "~/components/pagination/pagination-bar";

export default function MobileTable({
                                        records, currentPage, totalPages, onPageChange
                                    }: {
    records: HistoryRecord[];
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
}){
return (
    <div  className="md:hidden">
    <PaginationBar currentPage={currentPage} totalPages={totalPages} onPageChange={onPageChange} />
    <div className="space-y-4 mx-3 mt-4">
      {records.length === 0 ? (
        <div className="text-[#B6BAC5] text-sm">Žádné záznamy k zobrazení</div>
      ) : (
        records.map((r) => (
          <div
            key={r.id}
            className="bg-[#2A2C39] border border-[#3D4052] rounded-lg p-4 text-left"
          >
              <div className="text-xs text-[#B6BAC5]">Zkontrolováno</div>
              <div className="text-[#B6BAC5] text-sm">
                  {new Date(r.checkedAt).toLocaleString("cs-CZ")}
              </div>
            <div className="text-xs text-[#B6BAC5]">Dokument</div>
            <div className="text-[#E6E6E6] font-medium mb-2">
              {r.documentNumber}
            </div>

            <div className="text-xs text-[#B6BAC5]">Výsledek</div>
            <div className="mb-2">
              <StatusChip resultType={r.resultType} />
            </div>
          </div>
        ))
      )}
    </div>
        <PaginationBar currentPage={currentPage} totalPages={totalPages} onPageChange={onPageChange} />
    </div>
  );
}
