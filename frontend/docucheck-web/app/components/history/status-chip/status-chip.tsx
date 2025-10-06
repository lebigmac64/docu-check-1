import React from "react";

const map: Record<number, { label: string; cls: string }> = {
    0: {
        label: "Evidovaný",
        cls: "bg-red-500/15 text-amber-300 border border-yellow-500/40",
    },
    1: {
        label: "Nenalezen",
        cls: "bg-emerald-500/15 text-emerald-300 border border-emerald-500/40",
    },
    5: {
        label: "Error",
        cls: "bg-yellow-500/15 text-red-300 border border-red-500/40",
    },
};

export const StatusChip = React.memo(function StatusChip({ resultType }: { resultType: number }) {
  const { label, cls } = map[resultType] ?? map[0];
  return (
    <span className={`px-2 py-1 rounded-md text-xs font-medium ${cls}`}>
      {label}
    </span>
  );
}, (prevProps, nextProps) =>
    prevProps.resultType === nextProps.resultType);