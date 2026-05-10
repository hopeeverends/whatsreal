export function PropertyCardSkeleton() {
  return (
    <div className="border border-slate-200 bg-white p-3 dark:border-slate-700 dark:bg-slate-900">
      <div className="skeleton aspect-[4/3] w-full" />
      <div className="mt-4 space-y-3">
        <div className="skeleton h-4 w-4/5" />
        <div className="skeleton h-3 w-2/5" />
        <div className="flex gap-2">
          <div className="skeleton h-7 w-14" />
          <div className="skeleton h-7 w-14" />
          <div className="skeleton h-7 w-24" />
        </div>
      </div>
    </div>
  );
}

