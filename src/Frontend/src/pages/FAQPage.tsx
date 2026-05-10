const faqs = [
  ['Is this using a real database?', 'Not in phase 1. The repository interfaces are implemented with static seed data so PostgreSQL, SQLite, or another store can replace them later.'],
  ['Can authentication be added without rewriting the app?', 'Yes. The API is structured for JWT bearer authentication and role-based authorization around future admin endpoints.'],
  ['How are saved properties stored?', 'They are stored in local storage through a small Zustand store, which keeps phase 1 free of user accounts.'],
  ['What happens to inquiries?', 'Inquiries are validated by FluentValidation and saved in an in-memory repository. A future adapter can persist them and publish notifications.'],
  ['Is the frontend mobile friendly?', 'Yes. The UI uses responsive Tailwind layouts, lazy images, skeleton states, and compact controls for repeated search workflows.'],
];

export default function FAQPage() {
  return (
    <div className="bg-slate-50 py-12 dark:bg-slate-950">
      <div className="mx-auto max-w-4xl px-4 sm:px-6 lg:px-8">
        <h1 className="text-3xl font-bold text-slate-950 dark:text-white">Frequently asked questions</h1>
        <div className="mt-8 divide-y divide-slate-200 border border-slate-200 bg-white dark:divide-slate-700 dark:border-slate-700 dark:bg-slate-900">
          {faqs.map(([question, answer]) => (
            <details key={question} className="group p-5">
              <summary className="cursor-pointer list-none font-semibold text-slate-950 dark:text-white">
                {question}
              </summary>
              <p className="mt-3 leading-7 text-slate-600 dark:text-slate-300">{answer}</p>
            </details>
          ))}
        </div>
      </div>
    </div>
  );
}

