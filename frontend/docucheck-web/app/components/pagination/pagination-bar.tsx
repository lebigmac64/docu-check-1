import React from "react";

interface PaginationProps {
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
}

export default function PaginationBar({
                                          currentPage,
                                          totalPages,
                                          onPageChange,
                                      }: PaginationProps) {
    const handlePrevious = () => {
        if (currentPage > 1) {
            onPageChange(currentPage - 1);
        }
    };

    const handleNext = () => {
        if (currentPage < totalPages) {
            onPageChange(currentPage + 1);
        }
    };

    return (
        <div className="flex items-center justify-between space-x-4 mt-4">
            <button
                onClick={handlePrevious}
                disabled={currentPage === 1}
                className={"px-4 py-2 rounded bg-[#313445] text-stone-200"}
            >
                {"<"}
            </button>
            <span className="text-stone-200">
        Stránka {currentPage} ze {totalPages}
      </span>
            <button
                onClick={handleNext}
                disabled={currentPage === totalPages}
                className={"px-4 py-2 rounded bg-[#313445] text-stone-200"}
            >
                {">"}
            </button>
        </div>
    );
}