import type { Route } from "../../.react-router/types/app/routes/+types/home";
import DocumentForm from "~/components/index/document-form/document-form";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Kontrola platnosti dokumentu" },
    { name: "description", content: "Ověřte platnost dokumentu" },
  ];
}

export default function Home() {
  return <DocumentForm />;
}
