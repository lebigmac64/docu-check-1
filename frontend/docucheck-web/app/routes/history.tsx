import type { Route } from "../../.react-router/types/app/routes/+types/history";
import HistoryDashboard from "~/components/history/history-dashboard/history-dashboard";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Historie kontrol" },
    { name: "description", content: "Historie kontrol dokumentů" },
  ];
}

export default function History() {
  return <HistoryDashboard />;
}
