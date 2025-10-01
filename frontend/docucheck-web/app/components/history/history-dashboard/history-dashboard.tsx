import React, {type ReactElement, useEffect, useState} from "react";
import type {HistoryRecord} from "~/components/history/history-table/models/history-record";
import DesktopTable from "~/components/history/history-table/desktop-table/desktop-table";
import {API_ROOT} from "~/config";
import MobileTable from "~/components/history/history-table/mobile-table/mobile-table";

type Request = {
    pageNumber: number;
    pageSize: number;
}

type Response = {
    totalCount: number;
    currentPage: number;
    pageSize: number;
    items: HistoryRecord[];
}

export default function HistoryDashboard() : ReactElement {
    const [response, setResponse] = useState<Response>({totalCount: 0, pageSize: 10, currentPage: 1, items: []});
    const [request, setRequest] = useState<Request>({pageNumber: 1, pageSize: 10});

    useEffect(() => {
        const abortController = new AbortController();
        async function fetchRecords() {
            try {
                const uri = new URL(`${API_ROOT}api/documents/history?pageNumber=${request.pageNumber}&pageSize=${request.pageSize}`);
                const response = await fetch(uri, {signal: abortController.signal});

                if (!response.ok) {
                    throw new Error(`Request failed with status: ${response.status}\nmessage: ${response.statusText}`);
                }
                const data = await response.json() as Response;
                setResponse(data);
            } catch (error) {
                console.error('Error fetching history records:', error);
            }
        }

        fetchRecords();

        return () => {
            abortController.abort();
        }
    }, [request]);

    function handleRequestChanged(newPage: number) {
        setRequest((old) => ({...old, pageNumber: newPage}));
    }
  return (
    <>
      <DesktopTable records={response.items} currentPage={request.pageNumber} onPageChange={handleRequestChanged} totalPages={Math.ceil(response.totalCount / request.pageSize)} />
      <MobileTable records={response.items}  currentPage={request.pageNumber} onPageChange={handleRequestChanged} totalPages={Math.ceil(response.totalCount / request.pageSize)} />
    </>
  );
}
