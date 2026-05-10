import { Link } from 'react-router-dom';
import PropertyCard from '@components/PropertyCard';
import { PropertyCardSkeleton } from '@components/Skeletons';
import { useProperties } from '@hooks/useProperties';
import { useAppStore } from '@stores/appStore';

export default function HomePage() {
  const { data, isLoading } = useProperties(1, 6);
  const { recentlyViewed } = useAppStore();

  return (
    <div className="bg-slate-50 dark:bg-slate-950">
      <section className="relative min-h-[520px] overflow-hidden bg-slate-950 text-white">
        <img
          src="https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=1800&q=80"
          alt="Modern rental home exterior"
          className="absolute inset-0 h-full w-full object-cover opacity-55"
          loading="eager"
        />
        <div className="absolute inset-0 bg-slate-950/45" />
        <div className="relative mx-auto flex min-h-[520px] max-w-7xl flex-col justify-center px-4 pb-24 pt-20 sm:px-6 lg:px-8">
          <p className="text-sm font-semibold uppercase tracking-wide text-teal-200">Rental and property listings</p>
          <h1 className="mt-3 max-w-3xl text-4xl font-bold sm:text-5xl lg:text-6xl">WhatsReal</h1>
          <p className="mt-5 max-w-2xl text-lg leading-8 text-slate-100">
            Discover verified rental homes, compare agents, save shortlists, and contact owners from a fast, mobile-ready listing experience.
          </p>
          <div className="mt-8 flex flex-wrap gap-3">
            <Link to="/properties" className="bg-teal-600 px-5 py-3 text-sm font-bold text-white hover:bg-teal-700">
              Browse properties
            </Link>
            <Link to="/agents" className="border border-white/70 px-5 py-3 text-sm font-bold text-white hover:bg-white hover:text-slate-950">
              Meet agents
            </Link>
          </div>
        </div>
      </section>

      <section className="mx-auto max-w-7xl px-4 py-12 sm:px-6 lg:px-8">
        <div className="mb-6 flex items-end justify-between gap-4">
          <div>
            <h2 className="text-2xl font-bold text-slate-950 dark:text-white">Featured properties</h2>
            <p className="mt-1 text-sm text-slate-600 dark:text-slate-300">Static data today, repository-backed for a future database.</p>
          </div>
          <Link to="/properties" className="text-sm font-semibold text-teal-700 dark:text-teal-300">View all</Link>
        </div>

        <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
          {isLoading
            ? Array.from({ length: 6 }, (_, index) => <PropertyCardSkeleton key={index} />)
            : data?.items.map((property) => <PropertyCard key={property.id} property={property} />)}
        </div>
      </section>

      {recentlyViewed.length > 0 && (
        <section className="border-y border-slate-200 bg-white py-10 dark:border-slate-800 dark:bg-slate-900">
          <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
            <h2 className="text-xl font-bold text-slate-950 dark:text-white">Recently viewed</h2>
            <div className="mt-4 grid gap-3 sm:grid-cols-2 lg:grid-cols-4">
              {recentlyViewed.slice(0, 4).map((property) => (
                <Link key={property.id} to={`/properties/${property.id}`} className="flex gap-3 border border-slate-200 p-3 text-inherit hover:border-teal-500 dark:border-slate-700">
                  <img src={property.thumbnailUrl} alt={property.title} className="h-16 w-20 object-cover" loading="lazy" />
                  <div>
                    <p className="line-clamp-2 text-sm font-semibold text-slate-950 dark:text-white">{property.title}</p>
                    <p className="mt-1 text-xs text-slate-500 dark:text-slate-400">{property.city}</p>
                  </div>
                </Link>
              ))}
            </div>
          </div>
        </section>
      )}

      <section className="mx-auto max-w-7xl px-4 py-12 sm:px-6 lg:px-8">
        <div className="grid gap-6 md:grid-cols-3">
          <Value title="Secure by design" text="Rate limits, validation, secure headers, structured logs, and auth-ready boundaries are in place from day one." />
          <Value title="Fast search" text="Cached API responses, paginated queries, lazy images, and debounced filters keep the browsing path responsive." />
          <Value title="Cloud ready" text="Docker, Nginx, CI workflow, and clean infrastructure adapters prepare the system for PostgreSQL, Redis, MinIO, and Keycloak." />
        </div>
      </section>
    </div>
  );
}

function Value({ title, text }: { title: string; text: string }) {
  return (
    <div className="border border-slate-200 bg-white p-6 dark:border-slate-700 dark:bg-slate-900">
      <h3 className="font-bold text-slate-950 dark:text-white">{title}</h3>
      <p className="mt-2 text-sm leading-6 text-slate-600 dark:text-slate-300">{text}</p>
    </div>
  );
}

