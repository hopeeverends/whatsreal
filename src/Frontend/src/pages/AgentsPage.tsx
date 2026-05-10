import { Link } from 'react-router-dom';
import { useAgents } from '@hooks/useProperties';

export default function AgentsPage() {
  const { data: agents, isLoading } = useAgents();

  return (
    <div className="bg-slate-50 py-10 dark:bg-slate-950">
      <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div className="mb-7">
          <h1 className="text-3xl font-bold text-slate-950 dark:text-white">Agents</h1>
          <p className="mt-2 max-w-2xl text-slate-600 dark:text-slate-300">Browse local experts connected to each static listing. The same contract can later be served by CRM or identity-backed agent profiles.</p>
        </div>

        <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
          {isLoading && Array.from({ length: 6 }, (_, index) => <div key={index} className="skeleton h-56" />)}
          {agents?.map((agent) => (
            <Link key={agent.id} to={`/agents/${agent.id}`} className="border border-slate-200 bg-white p-5 text-inherit hover:border-teal-500 dark:border-slate-700 dark:bg-slate-900">
              <div className="flex items-center gap-4">
                <img
                  src={agent.imageUrl || `https://ui-avatars.com/api/?name=${agent.firstName}+${agent.lastName}&background=0f766e&color=fff`}
                  alt={`${agent.firstName} ${agent.lastName}`}
                  className="h-16 w-16 rounded-full object-cover"
                  loading="lazy"
                />
                <div>
                  <h2 className="font-bold text-slate-950 dark:text-white">{agent.firstName} {agent.lastName}</h2>
                  <p className="text-sm text-slate-500 dark:text-slate-400">{agent.propertyCount} listings</p>
                </div>
              </div>
              <div className="mt-4 space-y-1 text-sm text-slate-600 dark:text-slate-300">
                <p>{agent.email}</p>
                <p>{agent.phoneNumber}</p>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </div>
  );
}

