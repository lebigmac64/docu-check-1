import { Link } from "react-router";
import { useLocation } from "react-router";

export default function Header() {
  let { pathname } = useLocation();

  return (
    <header className="flex items-center sticky top-0 justify-between bg-[#313445] border-b border-[#3D4052] px-6 py-3 text-[#E6E6E6] font-sans">
      <div className="flex items-center gap-3">
        <span className="text-base font-semibold">Docu Check</span>
      </div>

      <nav className="flex gap-2">
        <Link
          to="/"
          className={`px-4 py-2 rounded-lg text-sm font-medium transition ${
            pathname === "/"
              ? "bg-[#21BFC2]/20 border border-[#21BFC2] text-[#E6E6E6]"
              : "text-[#B6BAC5] hover:text-[#E6E6E6] hover:bg-white/5"
          }`}
        >
          Home
        </Link>
        <Link
          to="/history"
          className={`px-4 py-2 rounded-lg text-sm font-medium transition ${
            pathname === "/history"
              ? "bg-[#21BFC2]/20 border border-[#21BFC2] text-[#E6E6E6]"
              : "text-[#B6BAC5] hover:text-[#E6E6E6] hover:bg-white/5"
          }`}
        >
          History
        </Link>
      </nav>
    </header>
  );
}
