import { Link } from 'react-router-dom';
import clsx from 'clsx';
import { PropertyDto } from '@types/api';
import { useAppStore } from '@stores/appStore';

interface PropertyCardProps {
  property: PropertyDto;
  compact?: boolean;
}

const formatPrice = (price: number) =>
  new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    maximumFractionDigits: 0,
  }).format(price);

export default function PropertyCard({ property, compact = false }: PropertyCardProps) {
  const { addSavedProperty, removeSavedProperty, isSaved } = useAppStore();
  const saved = isSaved(property.id);

  const toggleSaved = () => {
    if (saved) {
      removeSavedProperty(property.id);
      return;
    }

    addSavedProperty({
      id: property.id,
      title: property.title,
      price: property.price,
      thumbnailUrl: property.thumbnailUrl,
      city: property.city,
    });
  };

  return (
    <article className="overflow-hidden border border-slate-200 bg-white shadow-sm transition hover:-translate-y-0.5 hover:shadow-md dark:border-slate-700 dark:bg-slate-900">
      <Link to={`/properties/${property.id}`} className="block text-inherit hover:text-inherit">
        <div className={clsx('relative bg-slate-100 dark:bg-slate-800', compact ? 'aspect-[16/10]' : 'aspect-[4/3]')}>
          <img
            src={property.thumbnailUrl}
            alt={property.title}
            loading="lazy"
            className="h-full w-full object-cover"
          />
          <span className="absolute left-3 top-3 bg-white/95 px-2 py-1 text-xs font-semibold text-slate-800 shadow-sm dark:bg-slate-950/90 dark:text-slate-100">
            {property.propertyType}
          </span>
        </div>
      </Link>

      <div className="space-y-3 p-4">
        <div className="flex items-start justify-between gap-3">
          <div>
            <Link to={`/properties/${property.id}`} className="text-base font-semibold text-slate-950 hover:text-teal-700 dark:text-white dark:hover:text-teal-300">
              {property.title}
            </Link>
            <p className="mt-1 text-sm text-slate-500 dark:text-slate-400">{property.city}</p>
          </div>
          <button
            type="button"
            onClick={toggleSaved}
            className={clsx(
              'h-9 w-9 shrink-0 border text-sm font-bold transition',
              saved
                ? 'border-rose-300 bg-rose-50 text-rose-700 dark:border-rose-900 dark:bg-rose-950 dark:text-rose-300'
                : 'border-slate-200 bg-white text-slate-500 hover:text-rose-600 dark:border-slate-700 dark:bg-slate-900 dark:text-slate-300',
            )}
            aria-label={saved ? 'Remove from saved properties' : 'Save property'}
            title={saved ? 'Saved' : 'Save'}
          >
            {saved ? 'S' : '+'}
          </button>
        </div>

        <div className="flex flex-wrap gap-2 text-xs text-slate-600 dark:text-slate-300">
          <span className="border border-slate-200 px-2 py-1 dark:border-slate-700">{property.bedrooms} bd</span>
          <span className="border border-slate-200 px-2 py-1 dark:border-slate-700">{property.bathrooms} ba</span>
          <span className="border border-slate-200 px-2 py-1 dark:border-slate-700">
            {property.isFurnished ? 'Furnished' : 'Unfurnished'}
          </span>
        </div>

        <div className="flex items-end justify-between gap-3">
          <p className="text-xl font-bold text-slate-950 dark:text-white">{formatPrice(property.price)}</p>
          <Link to={`/properties/${property.id}`} className="text-sm font-semibold text-teal-700 hover:text-teal-800 dark:text-teal-300">
            Details
          </Link>
        </div>
      </div>
    </article>
  );
}

