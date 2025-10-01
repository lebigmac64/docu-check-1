import React, {useEffect, useState} from "react";

type Props = {
  text?: string;
  checked?: number;
  total?: number;
  freezeOnComplete?: boolean;
};

export default React.memo(function ProgressBar({
  text,
  total = 3,
  checked = 0,
}: Props): React.ReactElement {
  const [progress, setProgress] = useState(0);

  let value = total > 0 ? (checked / total) * 100 : 0;

  useEffect(() => {
    const handler = () => {
      setProgress(0);
    };
    window.addEventListener("ProgressBarReset", handler);

    return () => window.removeEventListener("ProgressBarReset", handler);
  }, []);

  useEffect(() => {

    const clamped = Math.max(0, Math.min(100, Math.round(value)));
    setProgress(() => {
      return clamped;
    });
  }, [value]);

  return (
    <div className="w-full">
      <div
        role="progressbar"
        aria-valuemin={0}
        aria-valuemax={100}
        aria-valuenow={progress}
        className={`relative h-8 w-full rounded-md border border-[#3D4052] overflow-hidden`}
        style={{ backgroundColor: "#232431" }}
      >
        <div
          className="h-full transition-[width] duration-800 ease-out animate-pulse"
          style={{
            width: `${progress}%`,
            background: "linear-gradient(90deg, #21BFC2 0%, #A77CFF 100%)",
          }}
        />
        <div className={`absolute inset-0 flex items-center justify-center font-semibold text-sm text-[#E6E6E6]`}>
          {`${text} ${checked < total ? checked : total} / ${total}`}
        </div>
      </div>
    </div>
  );
}, (prevProps, nextProps) => {
  return prevProps.checked === nextProps.checked
      && prevProps.total === nextProps.total
      && prevProps.text === nextProps.text;
});