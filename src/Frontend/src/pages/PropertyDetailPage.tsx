import { useEffect, useState } from 'react';
import type { ReactNode } from 'react';
import { useForm } from 'react-hook-form';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { apiClient } from '@services/apiClient';
import { useProperty } from '@hooks/useProperties';
import { useAppStore } from '@stores/appStore';

interface InquiryForm {
  contactName: string;
  email: string;
  phoneNumber: string;
  message: string;
}

const formatPrice = (price: number) =>
  new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(price);

export default function PropertyDetailPage() {
  const { id } = useParams();
  const { data: property, isLoading } = useProperty(id);
  const [activeImage, setActiveImage] = useState(0);
  const { addToRecentlyViewed, addSavedProperty, removeSavedProperty, isSaved } = useAppStore();
  const { register, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm<InquiryForm>();

  useEffect(() => {
    if (!property) return;
    addToRecentlyViewed({
      id: property.id,
      title: property.title,
      price: property.price,
      city: property.city,
      thumbnailUrl: property.thumbnailUrl || property.imageUrls[0],
    });
    document.title = `${property.title} | WhatsReal`;
  }, [property, addToRecentlyViewed]);

  if (isLoading) {
    return <div className="mx-auto max-w-7xl px-4 py-10 text-slate-600 dark:text-slate-300">Loading property...</div>;
  }

  if (!property) {
    return <div className="mx-auto max-w-7xl px-4 py-10 text-slate-600 dark:text-slate-300">Property not found.</div>;
  }

  const saved = isSaved(property.id);
  const images = property.imageUrls.length > 0 ? property.imageUrls : [property.thumbnailUrl];

  const submitInquiry = async (values: InquiryForm) => {
    await apiClient.createInquiry({ propertyId: property.id, ...values });
    toast.success('Inquiry sent to the agent');
    reset();
  };

  return (
    <div className="bg-slate-50 py-8 dark:bg-slate-950">
      <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div className="mb-6 flex flex-col justify-between gap-4 lg:flex-row lg:items-end">
          <div>
            <p className="text-sm font-semibold uppercase tracking-wide text-teal-700 dark:text-teal-300">{property.propertyType}</p>
            <h1 className="mt-1 text-3xl font-bold text-slate-950 dark:text-white">{property.title}</h1>
            <p className="mt-2 text-slate-600 dark:text-slate-300">{property.address}, {property.city}, {property.state} {property.zipCode}</p>
          </div>
          <div className="flex items-center gap-3">
            <p className="text-3xl font-bold text-slate-950 dark:text-white">{formatPrice(property.price)}</p>
            <button
              type="button"
              onClick={() => saved ? removeSavedProperty(property.id) : addSavedProperty({ id: property.id, title: property.title, price: property.price, city: property.city, thumbnailUrl: images[0] })}
              className="border border-slate-300 px-4 py-2 text-sm font-semibold text-slate-700 dark:border-slate-700 dark:text-slate-200"
            >
              {saved ? 'Saved' : 'Save'}
            </button>
          </div>
        </div>

        <div className="grid gap-6 lg:grid-cols-[1fr_360px]">
          <section>
            <div className="overflow-hidden bg-slate-200 dark:bg-slate-800">
              <img src={images[activeImage]} alt={property.title} className="aspect-[16/9] w-full object-cover" />
            </div>
            <div className="mt-3 grid grid-cols-4 gap-3 sm:grid-cols-6">
              {images.map((image, index) => (
                <button key={image} type="button" onClick={() => setActiveImage(index)} className="border border-slate-200 dark:border-slate-700">
                  <img src={image} alt={`${property.title} ${index + 1}`} loading="lazy" className="aspect-[4/3] w-full object-cover" />
                </button>
              ))}
            </div>

            <div className="mt-6 grid gap-3 sm:grid-cols-4">
              <Metric label="Bedrooms" value={property.bedrooms.toString()} />
              <Metric label="Bathrooms" value={property.bathrooms.toString()} />
              <Metric label="Area" value={`${property.squareFeet} sqft`} />
              <Metric label="Furnishing" value={property.isFurnished ? 'Furnished' : 'Unfurnished'} />
            </div>

            <section className="mt-8 border border-slate-200 bg-white p-6 dark:border-slate-700 dark:bg-slate-900">
              <h2 className="text-xl font-bold text-slate-950 dark:text-white">Overview</h2>
              <p className="mt-3 leading-7 text-slate-600 dark:text-slate-300">{property.description}</p>
            </section>
          </section>

          <aside className="h-fit border border-slate-200 bg-white p-5 dark:border-slate-700 dark:bg-slate-900">
            {property.agent && (
              <div className="mb-5 border-b border-slate-200 pb-5 dark:border-slate-700">
                <p className="text-sm text-slate-500 dark:text-slate-400">Listed by</p>
                <p className="mt-1 text-lg font-bold text-slate-950 dark:text-white">{property.agent.firstName} {property.agent.lastName}</p>
                <p className="text-sm text-slate-600 dark:text-slate-300">{property.agent.email}</p>
                <p className="text-sm text-slate-600 dark:text-slate-300">{property.agent.phoneNumber}</p>
              </div>
            )}

            <h2 className="text-lg font-bold text-slate-950 dark:text-white">Contact owner</h2>
            <form className="mt-4 space-y-3" onSubmit={handleSubmit(submitInquiry)}>
              <Field label="Name" error={errors.contactName?.message}>
                <input {...register('contactName', { required: 'Name is required', maxLength: 100 })} className="w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </Field>
              <Field label="Email" error={errors.email?.message}>
                <input {...register('email', { required: 'Email is required', pattern: { value: /.+@.+\..+/, message: 'Enter a valid email' } })} className="w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </Field>
              <Field label="Phone" error={errors.phoneNumber?.message}>
                <input {...register('phoneNumber', { required: 'Phone is required' })} className="w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </Field>
              <Field label="Message" error={errors.message?.message}>
                <textarea {...register('message', { required: 'Message is required', minLength: { value: 10, message: 'Use at least 10 characters' } })} rows={4} className="w-full border border-slate-300 bg-white px-3 py-2 text-sm dark:border-slate-700 dark:bg-slate-950" />
              </Field>
              <button type="submit" disabled={isSubmitting} className="w-full bg-teal-700 px-4 py-3 text-sm font-bold text-white hover:bg-teal-800 disabled:opacity-60">
                {isSubmitting ? 'Sending...' : 'Send inquiry'}
              </button>
            </form>
          </aside>
        </div>
      </div>
    </div>
  );
}

function Metric({ label, value }: { label: string; value: string }) {
  return (
    <div className="border border-slate-200 bg-white p-4 dark:border-slate-700 dark:bg-slate-900">
      <p className="text-xs uppercase tracking-wide text-slate-500 dark:text-slate-400">{label}</p>
      <p className="mt-1 font-bold text-slate-950 dark:text-white">{value}</p>
    </div>
  );
}

function Field({ label, error, children }: { label: string; error?: string; children: ReactNode }) {
  return (
    <label className="block text-sm font-medium text-slate-700 dark:text-slate-200">
      {label}
      <div className="mt-1">{children}</div>
      {error && <span className="mt-1 block text-xs text-rose-600">{error}</span>}
    </label>
  );
}
