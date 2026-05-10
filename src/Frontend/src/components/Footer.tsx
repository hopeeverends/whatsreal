import { Link } from 'react-router-dom';

export default function Footer() {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="border-t border-slate-800 bg-slate-950 text-slate-100">
      <div className="mx-auto max-w-7xl px-4 py-10 sm:px-6 lg:px-8">
        <div className="grid gap-8 md:grid-cols-4">
          <div>
            <h3 className="text-lg font-bold">WhatsReal</h3>
            <p className="mt-3 text-sm leading-6 text-slate-400">A clean-architecture real estate rental platform built for static data today and production services tomorrow.</p>
          </div>
          <FooterLinks title="Explore" links={[['Properties', '/properties'], ['Agents', '/agents'], ['About', '/about'], ['FAQ', '/faq']]} />
          <div>
            <h4 className="font-semibold">Operations</h4>
            <p className="mt-3 text-sm leading-6 text-slate-400">Docker-ready, cloud-ready, cache-ready, auth-ready, and documented for future teams.</p>
          </div>
          <div>
            <h4 className="font-semibold">Contact</h4>
            <p className="mt-3 text-sm text-slate-400">info@whatsreal.com</p>
            <p className="mt-1 text-sm text-slate-400">+1 555 123 4567</p>
          </div>
        </div>
        <div className="mt-8 border-t border-slate-800 pt-6 text-sm text-slate-500">
          Copyright {currentYear} WhatsReal. All rights reserved.
        </div>
      </div>
    </footer>
  );
}

function FooterLinks({ title, links }: { title: string; links: [string, string][] }) {
  return (
    <div>
      <h4 className="font-semibold">{title}</h4>
      <ul className="mt-3 space-y-2 text-sm">
        {links.map(([label, href]) => (
          <li key={href}>
            <Link to={href} className="text-slate-400 hover:text-white">{label}</Link>
          </li>
        ))}
      </ul>
    </div>
  );
}

