import type { HistoryRecord } from "~/components/history/history-table/models/history-record";
import StatusChip from "~/components/history/status-chip/status-chip";
import React from "react";
import PaginationBar from "~/components/pagination/pagination-bar";

export default function DesktopTable({
  records, currentPage, totalPages, onPageChange
}: {
    records: HistoryRecord[];
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
}) {
  return (
    <div className="hidden md:block bg-[#313445] p-6 rounded-xl m-4 shadow-2xl border border-[#3D4052]">
      <div className="flex items-center justify-center mb-4">
        <h2 className="text-lg font-semibold text-[#E6E6E6]">
          Historie kontrol
        </h2>
        <div />
      </div>
        <PaginationBar currentPage={currentPage} totalPages={totalPages} onPageChange={onPageChange} />
      <div className="rounded-lg border border-[#3D4052]">
        <table className="min-w-full text-sm text-left">
          <thead className="bg-[#2A2C39] text-[#B6BAC5]">
            <tr>
                <th className="px-4 py-3 text-left font-medium border-b border-[#3D4052]">
                    Datum kontroly
                </th>
              <th className="px-4 py-3 text-left font-medium border-b border-[#3D4052]">
                Číslo dokladu
              </th>
              <th className="px-4 py-3 text-left font-medium border-b border-[#3D4052]">
                Výsledek
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-[#3D4052]">
            {records.length === 0 ? (
              <tr>
                <td
                  colSpan={4}
                  className="px-4 py-6 text-center text-[#B6BAC5]"
                >
                  Žádné záznamy k zobrazení
                </td>
              </tr>
            ) : (
              records.map((r) => (
                <tr key={r.id} className="hover:bg-[#2B2D3A] transition-colors">
                    <td className="px-4 py-3 text-[#B6BAC5]">
                        {new Date(r.checkedAt).toLocaleString("cs-CZ")}
                    </td>
                  <td className="px-4 py-3 text-[#E6E6E6] font-medium">
                    {r.documentNumber}
                  </td>
                  <td className="px-4 py-3">
                    <StatusChip resultType={r.resultType} />
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
        <PaginationBar currentPage={currentPage} totalPages={totalPages} onPageChange={onPageChange} />
    </div>
  );
}
