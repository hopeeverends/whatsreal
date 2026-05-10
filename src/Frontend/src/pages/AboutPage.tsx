const roadmap = [
  'Swap mock repositories for EF Core and Dapper-backed PostgreSQL adapters',
  'Add Redis output cache and distributed rate-limit counters',
  'Integrate Keycloak or another OIDC provider for JWT roles and admin access',
  'Move images to S3-compatible storage such as MinIO with CDN headers',
  'Introduce event handlers for inquiries, analytics, and recommendations',
];

export default function AboutPage() {
  return (
    <div className="bg-slate-50 py-12 dark:bg-slate-950">
      <div className="mx-auto max-w-5xl px-4 sm:px-6 lg:px-8">
        <h1 className="text-3xl font-bold text-slate-950 dark:text-white">About WhatsReal</h1>
        <p className="mt-4 text-lg leading-8 text-slate-600 dark:text-slate-300">
          WhatsReal is a production-shaped rental listing platform built as a modular monolith. Phase 1 uses static mock data, while the API, contracts, and UI are already organized for database, cache, storage, authentication, AI, and cloud migration.
        </p>

        <div className="mt-8 grid gap-4 md:grid-cols-3">
          <Stat label="Mock listings" value="25+" />
          <Stat label="Agents" value="5" />
          <Stat label="Architecture" value="CQRS" />
        </div>

        <section className="mt-10 border border-slate-200 bg-white p-6 dark:border-slate-700 dark:bg-slate-900">
          <h2 className="text-xl font-bold text-slate-950 dark:text-white">Scalability roadmap</h2>
          <ul className="mt-4 space-y-3 text-slate-600 dark:text-slate-300">
            {roadmap.map((item) => <li key={item} className="border-l-2 border-teal-600 pl-3">{item}</li>)}
          </ul>
        </section>
      </div>
    </div>
  );
}

function Stat({ label, value }: { label: string; value: string }) {
  return (
    <div className="border border-slate-200 bg-white p-5 dark:border-slate-700 dark:bg-slate-900">
      <p className="text-2xl font-bold text-slate-950 dark:text-white">{value}</p>
      <p className="mt-1 text-sm text-slate-500 dark:text-slate-400">{label}</p>
    </div>
  );
}

