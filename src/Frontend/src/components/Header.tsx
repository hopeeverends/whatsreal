import { Link, NavLink } from 'react-router-dom';
import { useTheme } from '@hooks/useTheme';

const navItems = [
  { to: '/properties', label: 'Properties' },
  { to: '/agents', label: 'Agents' },
  { to: '/about', label: 'About' },
  { to: '/faq', label: 'FAQ' },
];

export default function Header() {
  const { isDarkMode, toggleTheme } = useTheme();

  return (
    <header className="sticky top-0 z-50 border-b border-slate-200 bg-white/95 backdrop-blur dark:border-slate-800 dark:bg-slate-950/95">
      <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div className="flex h-16 items-center justify-between gap-4">
          <Link to="/" className="flex items-center gap-2 text-slate-950 hover:text-slate-950 dark:text-white dark:hover:text-white">
            <span className="flex h-9 w-9 items-center justify-center bg-teal-700 text-sm font-black text-white">WR</span>
            <span className="text-lg font-bold">WhatsReal</span>
          </Link>

          <nav className="hidden items-center gap-6 md:flex">
            {navItems.map((item) => (
              <NavLink
                key={item.to}
                to={item.to}
                className={({ isActive }) =>
                  `text-sm font-semibold ${isActive ? 'text-teal-700 dark:text-teal-300' : 'text-slate-600 hover:text-slate-950 dark:text-slate-300 dark:hover:text-white'}`
                }
              >
                {item.label}
              </NavLink>
            ))}
          </nav>

          <div className="flex items-center gap-2">
            <Link to="/properties" className="hidden bg-slate-950 px-4 py-2 text-sm font-bold text-white hover:bg-teal-700 dark:bg-white dark:text-slate-950 dark:hover:bg-teal-200 sm:inline-flex">
              Search
            </Link>
            <button
              onClick={toggleTheme}
              className="h-10 w-10 border border-slate-300 text-sm font-bold text-slate-700 hover:border-teal-600 hover:text-teal-700 dark:border-slate-700 dark:text-slate-200 dark:hover:border-teal-300 dark:hover:text-teal-300"
              aria-label="Toggle theme"
              title="Toggle theme"
            >
              {isDarkMode ? 'L' : 'D'}
            </button>
          </div>
        </div>

        <nav className="flex gap-4 overflow-x-auto pb-3 md:hidden">
          {navItems.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) =>
                `shrink-0 text-sm font-semibold ${isActive ? 'text-teal-700 dark:text-teal-300' : 'text-slate-600 dark:text-slate-300'}`
              }
            >
              {item.label}
            </NavLink>
          ))}
        </nav>
      </div>
    </header>
  );
}

