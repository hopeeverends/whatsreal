import { Link } from 'react-router-dom';

export default function NotFoundPage() {
  return (
    <div className="flex min-h-[70vh] flex-col items-center justify-center bg-slate-50 px-4 text-center dark:bg-slate-950">
      <h1 className="text-6xl font-bold text-teal-700 dark:text-teal-300">404</h1>
      <p className="mt-3 text-2xl font-semibold text-slate-950 dark:text-white">Page not found</p>
      <p className="mt-2 text-slate-600 dark:text-slate-300">The listing path you opened does not exist.</p>
      <Link to="/properties" className="mt-7 bg-teal-700 px-5 py-3 text-sm font-bold text-white hover:bg-teal-800">
        Browse properties
      </Link>
    </div>
  );
}

