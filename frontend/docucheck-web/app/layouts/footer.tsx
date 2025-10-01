import React from "react";

export default function Footer() {
  return (
    <footer className="w-full  bg-[#313445] border-t border-[#3D4052] text-[#B6BAC5] text-sm py-4 font-sans">
      <div className="max-w-6xl mx-auto flex flex-col md:flex-row items-center justify-between gap-3">
        <div></div>
        <div className="text-xs text-[#8D90A0]">
          &copy; {new Date().getFullYear()} DocuCheck
        </div>
        <div></div>
      </div>
    </footer>
  );
}
