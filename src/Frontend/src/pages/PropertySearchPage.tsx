import { FormEvent, useMemo, useState } from 'react';
import PropertyCard from '@components/PropertyCard';
import { PropertyCardSkeleton } from '@components/Skeletons';
import { useSearchProperties } from '@hooks/useProperties';
import { useDebounce } from '@hooks/useDebounce';
import { useAppStore } from '@stores/appStore';

const propertyTypes = ['', 'Apartment', 'House', 'Townhouse', 'Condo', 'Studio', 'Villa', 'Penthouse'];
const locations = ['', 'New York', 'Brooklyn', 'Beverly Hills', 'Santa Monica', 'Miami', 'San Francisco', 'Seattle', 'Boston', 'Chicago'];

export default function PropertySearchPage() {
  const [pageNumber, setPageNumber] = useState(1);
  const [filters, setFilters] = useState({
    search: '',
    city: '',
    type: '',
    minPrice: '',
    maxPrice: '',
    bedrooms: '',
    bathrooms: '',
    furnished: '',
    sortBy: 'date',
    sortDirection: 'desc',
  });
  const debouncedFilters = useDebounce(filters, 300);
  const { savedProperties, recentlyViewed } = useAppStore();

  const queryParams = useMemo(() => {
    const params: Record<string, string | number | boolean> = {
      pageNumber,
      pageSize: 9,
      sortBy: debouncedFilters.sortBy,
      sortDirection: debouncedFilters.sortDirection,
    };

    Object.entries(debouncedFilters).forEach(([key, value]) => {
      if (!value || key === 'sortBy' || key === 'sortDirection') return;
      params[key] = key === 'furnished' ? value === 'true' : value;
    });

    return params;
  }, [debouncedFilters, pageNumber]);

  const { data, isLoading, isFetching } = useSearchProperties(queryParams);

  const updateFilter = (name: keyof typeof filters, value: string) => {
    setFilters((current) => ({ ...current, [name]: value }));
    setPageNumber(1);
  };

  const clearFilters = (event: FormEvent) => {
    event.preventDefault();
    setFilters({
      search: '',
      city: '',
      type: '',
      minPrice: '',
      maxPrice: '',
      bedrooms: '',
      bathrooms: '',
      furnished: '',
      sortBy: 'date',
      sortDirection: 'desc',
    });
    setPageNumber(1);
  };

  return (
    <div className="bg-slate-50 py-8 dark:bg-slate-950">
      <div className="mx-auto grid max-w-7xl gap-6 px-4 sm:px-6 lg:grid-cols-[300px_1fr] lg:px-8">
        <aside className="h-fit border border-slate-200 bg-white p-4 dark:border-slate-700 dark:bg-slate-900">
          <div className="flex items-center justify-between">
            <h1 className="text-xl font-bold text-slate-950 dark:text-white">Search</h1>
            <button type="button" onClick={clearFilters} className="text-sm font-semibold text-teal-700 dark:text-teal-300">
              Reset
            </button>
          </div>

          <form className="mt-5 space-y-4">
            <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
              Keyword
              <input
                value={filters.search}
                onChange={(event) => updateFilter('search', event.target.value)}
                className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950"
                placeholder="Loft, beach, downtown"
              />
            </label>

            <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
              Location
              <select value={filters.city} onChange={(event) => updateFilter('city', event.target.value)} className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950">
                {locations.map((location) => <option key={location} value={location}>{location || 'Any location'}</option>)}
              </select>
            </label>

            <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
              Type
              <select value={filters.type} onChange={(event) => updateFilter('type', event.target.value)} className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950">
                {propertyTypes.map((type) => <option key={type} value={type}>{type || 'Any type'}</option>)}
              </select>
            </label>

            <div className="grid grid-cols-2 gap-3">
              <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
                Min price
                <input value={filters.minPrice} onChange={(event) => updateFilter('minPrice', event.target.value)} type="number" min="0" className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </label>
              <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
                Max price
                <input value={filters.maxPrice} onChange={(event) => updateFilter('maxPrice', event.target.value)} type="number" min="0" className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </label>
            </div>

            <div className="grid grid-cols-2 gap-3">
              <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
                Beds
                <input value={filters.bedrooms} onChange={(event) => updateFilter('bedrooms', event.target.value)} type="number" min="0" className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </label>
              <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
                Baths
                <input value={filters.bathrooms} onChange={(event) => updateFilter('bathrooms', event.target.value)} type="number" min="0" className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </label>
            </div>

            <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
              Furnishing
              <select value={filters.furnished} onChange={(event) => updateFilter('furnished', event.target.value)} className="mt-1 w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950">
                <option value="">Any</option>
                <option value="true">Furnished</option>
                <option value="false">Unfurnished</option>
              </select>
            </label>
          </form>

          <div className="mt-6 border-t border-slate-200 pt-4 text-sm text-slate-500 dark:border-slate-700 dark:text-slate-400">
            <p>{savedProperties.length} saved</p>
            <p>{recentlyViewed.length} recently viewed</p>
          </div>
        </aside>

        <section>
          <div className="mb-4 flex flex-col justify-between gap-3 sm:flex-row sm:items-center">
            <div>
              <h2 className="text-2xl font-bold text-slate-950 dark:text-white">Available rentals</h2>
              <p className="text-sm text-slate-500 dark:text-slate-400">
                {data ? `${data.totalCount} matching properties` : 'Loading properties'}
                {isFetching && !isLoading ? ' - refreshing' : ''}
              </p>
            </div>
            <select
              value={`${filters.sortBy}:${filters.sortDirection}`}
              onChange={(event) => {
                const [sortBy, sortDirection] = event.target.value.split(':');
                setFilters((current) => ({ ...current, sortBy, sortDirection }));
              }}
              className="w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-900 sm:w-48"
            >
              <option value="date:desc">Newest</option>
              <option value="price:asc">Price low</option>
              <option value="price:desc">Price high</option>
              <option value="bedrooms:desc">Most bedrooms</option>
            </select>
          </div>

          <div className="grid gap-5 sm:grid-cols-2 xl:grid-cols-3">
            {isLoading
              ? Array.from({ length: 9 }, (_, index) => <PropertyCardSkeleton key={index} />)
              : data?.items.map((property) => <PropertyCard key={property.id} property={property} />)}
          </div>

          {data && data.items.length === 0 && (
            <div className="border border-dashed border-slate-300 bg-white p-10 text-center dark:border-slate-700 dark:bg-slate-900">
              <p className="font-semibold text-slate-900 dark:text-white">No properties match those filters.</p>
              <p className="mt-1 text-sm text-slate-500 dark:text-slate-400">Try a wider price range or a different location.</p>
            </div>
          )}

          {data && data.totalPages > 1 && (
            <div className="mt-8 flex items-center justify-center gap-3">
              <button disabled={!data.hasPreviousPage} onClick={() => setPageNumber((page) => page - 1)} className="border border-slate-300 px-4 py-2 text-sm font-semibold disabled:opacity-40 dark:border-slate-700">
                Prev
              </button>
              <span className="text-sm text-slate-600 dark:text-slate-300">Page {data.pageNumber} of {data.totalPages}</span>
              <button disabled={!data.hasNextPage} onClick={() => setPageNumber((page) => page + 1)} className="border border-slate-300 px-4 py-2 text-sm font-semibold disabled:opacity-40 dark:border-slate-700">
                Next
              </button>
            </div>
          )}
        </section>
      </div>
    </div>
  );
}

