import { useParams } from 'react-router-dom';
import PropertyCard from '@components/PropertyCard';
import { useAgent } from '@hooks/useProperties';

export default function AgentDetailPage() {
  const { id } = useParams();
  const { data: agent, isLoading } = useAgent(id);

  if (isLoading) {
    return <div className="mx-auto max-w-7xl px-4 py-10 text-slate-600 dark:text-slate-300">Loading agent...</div>;
  }

  if (!agent) {
    return <div className="mx-auto max-w-7xl px-4 py-10 text-slate-600 dark:text-slate-300">Agent not found.</div>;
  }

  return (
    <div className="bg-slate-50 py-10 dark:bg-slate-950">
      <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <section className="border border-slate-200 bg-white p-6 dark:border-slate-700 dark:bg-slate-900">
          <div className="flex flex-col gap-5 sm:flex-row sm:items-center">
            <img
              src={agent.imageUrl || `https://ui-avatars.com/api/?name=${agent.firstName}+${agent.lastName}&background=0f766e&color=fff`}
              alt={`${agent.firstName} ${agent.lastName}`}
              className="h-28 w-28 rounded-full object-cover"
            />
            <div>
              <h1 className="text-3xl font-bold text-slate-950 dark:text-white">{agent.firstName} {agent.lastName}</h1>
              <p className="mt-2 text-slate-600 dark:text-slate-300">{agent.bio}</p>
              <div className="mt-4 flex flex-wrap gap-3 text-sm text-slate-600 dark:text-slate-300">
                <span>{agent.email}</span>
                <span>{agent.phoneNumber}</span>
                <span>{agent.propertyCount} listings</span>
              </div>
            </div>
          </div>
        </section>

        <section className="mt-8">
          <h2 className="text-2xl font-bold text-slate-950 dark:text-white">Listings</h2>
          <div className="mt-5 grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
            {agent.properties.map((property) => <PropertyCard key={property.id} property={property} />)}
          </div>
        </section>
      </div>
    </div>
  );
}

