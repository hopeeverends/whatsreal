# WhatsReal - Real Estate Rental & Property Listing Platform

A production-grade, scalable, and secure real estate rental platform built with modern technologies. This is Phase 1 of the project, using mock data with full architecture designed for future database migration, cloud integration, and microservices without major refactoring.

## 🎯 Key Features

### Public Features (Phase 1)
- ✅ **Property Search & Discovery**: Browse thousands of properties with advanced filtering
- ✅ **Property Details**: Rich property information with image galleries
- ✅ **Agent Directory**: View all agents and their listings
- ✅ **Contact Form**: Reach out to agents about properties
- ✅ **Dark/Light Mode**: User theme preference
- ✅ **Saved Properties**: Save favorites locally
- ✅ **Recently Viewed**: Auto-tracked property history
- ✅ **Responsive Design**: Mobile-first, works on all devices
- ✅ **SEO Friendly**: Proper meta tags and semantic HTML

### Architecture Ready (Design Phase)
- 🔐 JWT Authentication (design ready for Phase 3)
- 👮 Role-Based Authorization (Admin, Agent, User)
- 📊 Admin Dashboard (layout ready for Phase 4)
- 🖼️ Image Upload System (design ready)
- 💾 Real Database (PostgreSQL ready for Phase 2)
- ⚡ Caching Layer (Redis abstraction ready)
- 🔔 Background Jobs (abstraction ready)
- 🤖 AI Features (vector search ready for Phase 6)

## 🛠️ Tech Stack

### Backend
- **.NET 10** - Latest stable runtime
- **ASP.NET Core Minimal APIs** - Lightweight, modern, microservices-ready
- **MediatR** - CQRS pattern implementation
- **FluentValidation** - Input validation
- **AutoMapper** - Object mapping
- **Serilog** - Structured logging
- **Swagger/OpenAPI** - API documentation
- **Mock Repositories** - Phase 1 data (transitioning to EF Core)

### Frontend
- **React 18** - UI library
- **Vite** - Fast build tool and dev server
- **TypeScript** - Type safety
- **TailwindCSS** - Utility-first CSS
- **React Router** - Client-side routing
- **TanStack Query** - Server state management
- **Zustand** - Client state management
- **React Hook Form** - Form handling
- **Zod** - Schema validation
- **Axios** - HTTP client

### DevOps & Infrastructure
- **Docker & Docker Compose** - Containerization
- **GitHub Actions** - CI/CD pipelines
- **Nginx** - Reverse proxy & static file serving
- **PostgreSQL** - Database (Phase 2)
- **Redis** - Caching (Phase 2)

## 🚀 Quick Start

### Prerequisites
- **.NET SDK 10.0** or later
- **Node.js 20** or later
- **Docker & Docker Compose** (optional, for containerized setup)
- **Git**

### Development Setup

#### Backend API

1. **Clone & Setup**
```bash
git clone https://github.com/yourusername/whatsreal.git
cd whatsreal
dotnet restore
```

2. **Run API**
```bash
cd src/Api
dotnet run
```

API will be available at: `http://localhost:5000`
Swagger UI: `http://localhost:5000/swagger`
Health Check: `http://localhost:5000/health`

#### Frontend React App

1. **Setup**
```bash
cd src/Frontend
npm install
```

2. **Development Server**
```bash
npm run dev
```

Frontend will be available at: `http://localhost:3000`

3. **Build for Production**
```bash
npm run build
npm run preview
```

### Docker Setup

**Run everything with docker-compose:**

```bash
docker-compose -f docker/docker-compose.yml up -d
```

Services will be available at:
- API: http://localhost:5000
- Frontend: http://localhost:80
- PostgreSQL: localhost:5432
- Redis: localhost:6379

## 📁 Project Structure

```
whatsreal/
├── .github/workflows/           # CI/CD pipelines
├── docker/                      # Docker configuration
│   ├── Dockerfile              # Multi-stage build
│   ├── docker-compose.yml      # Full stack orchestration
│   └── nginx.conf              # Reverse proxy config
├── docs/                        # Documentation
├── src/
│   ├── Api/                    # ASP.NET Core entry point (Minimal APIs)
│   ├── Application/            # Business logic (MediatR handlers)
│   ├── Domain/                 # Core entities & interfaces
│   ├── Infrastructure/         # Repositories & external services
│   ├── Shared/                 # DTOs & common utilities
│   ├── Frontend/               # React + Vite application
│   └── Tests/                  # Unit & integration tests
└── WhatsReal.sln              # Solution file
```

## 🏗️ Architecture

### Clean Architecture Layers

**Domain Layer** (`WhatsReal.Domain`)
- Core business entities: Property, Agent, PropertyInquiry
- Domain interfaces: IPropertyRepository, IAgentRepository
- Business rules and validation logic
- *No external dependencies*

**Application Layer** (`WhatsReal.Application`)
- MediatR queries and commands
- Business logic (handlers)
- FluentValidation validators
- AutoMapper profiles (Entity ↔ DTO)
- *Dependencies: Domain, MediatR, FluentValidation*

**Infrastructure Layer** (`WhatsReal.Infrastructure`)
- Mock repository implementations (Phase 1)
- Data seeding with realistic mock data (25 properties, 5 agents)
- Ready for EF Core migration (Phase 2)
- Ready for Redis integration
- *Dependencies: Domain, Application*

**API Layer** (`WhatsReal.Api`)
- ASP.NET Core Minimal API endpoints
- Global exception handling middleware
- Request/response logging
- CORS, secure headers, rate limiting
- Swagger/OpenAPI documentation
- Dependency injection setup
- *Dependencies: all layers*

**Shared Layer** (`WhatsReal.Shared`)
- Data Transfer Objects (DTOs)
- Generic response wrappers (`ApiResponse<T>`, `PagedResponse<T>`)
- Pagination models
- Constants and utilities

### Design Patterns

- **Repository Pattern**: Easy transition from mock to EF Core
- **Query/Command Separation (CQRS-light)**: Clear intent, optimized access patterns
- **Result Pattern**: Consistent error handling
- **Dependency Injection**: Loosely coupled, highly testable
- **AutoMapper Profiles**: Domain ↔ DTO transformation

## 📊 API Endpoints

### Properties
- `GET /api/v1/properties` - List all properties (paginated)
- `GET /api/v1/properties/{id}` - Get property details
- `GET /api/v1/properties/search` - Search with filters (price, type, location, bedrooms, furnished)

### Agents
- `GET /api/v1/agents` - List all agents
- `GET /api/v1/agents/{id}` - Get agent details with properties

### Inquiries
- `POST /api/v1/inquiries` - Create property inquiry

### System
- `GET /health` - Health check endpoint

**Documentation**: Available at `/swagger` when API is running

## 🔄 Data Flow

```
Frontend (React)
    ↓
TanStack Query (data fetching)
    ↓
Axios (HTTP client)
    ↓
API (Minimal APIs)
    ↓
MediatR (command/query dispatcher)
    ↓
Handlers (business logic)
    ↓
Mock Repositories (in-memory storage)
    ↓
Response (serialized JSON)
```

## 🧪 Testing

```bash
# Backend tests
dotnet test

# Frontend tests (when added)
cd src/Frontend
npm test
```

## 🔒 Security

Phase 1 focuses on architecture design. Phase 3 will implement:
- JWT authentication
- Role-based authorization
- Secure password hashing
- CORS policies
- Rate limiting
- Input validation

Current Phase 1 includes:
- ✅ Input validation (FluentValidation)
- ✅ Secure headers middleware
- ✅ CORS configuration
- ✅ Global exception handling
- ✅ Request logging

## 📈 Performance

- **Response Caching**: API responses include cache headers
- **Pagination**: Default 20 items/page, customizable
- **Lazy Loading**: React components code-split by route
- **Gzip Compression**: Nginx compresses responses
- **Database Ready**: Indexing strategy ready for Phase 2

## 🚢 Deployment

### Local Development
```bash
docker-compose -f docker/docker-compose.yml up -d
```

### VPS Deployment (Ubuntu)
See [DEPLOYMENT.md](docs/DEPLOYMENT.md) for complete guide
- Cost: $25-50/month for small instance
- Includes setup scripts for production

### Cloud Deployment
- **Azure**: App Service deployment guide included
- **AWS**: EC2/ECS deployment guide included

## 📚 Documentation

- [SETUP.md](SETUP.md) - Detailed setup instructions
- [ARCHITECTURE.md](docs/ARCHITECTURE.md) - Architecture deep dive
- [API-DESIGN.md](docs/API-DESIGN.md) - API design decisions
- [DEPLOYMENT.md](docs/DEPLOYMENT.md) - Deployment guides
- [DATABASE-MIGRATION.md](docs/DATABASE-MIGRATION.md) - Phase 2 migration plan

## 🗺️ Roadmap

### Phase 1 (Current) ✅
Real estate platform with mock data and responsive UI

### Phase 2 (Weeks 3-4)
- [ ] Real database (PostgreSQL + EF Core)
- [ ] Database migrations
- [ ] Query optimization
- [ ] Connection pooling

### Phase 3 (Week 5)
- [ ] Authentication (JWT + Keycloak/Auth0)
- [ ] Authorization roles
- [ ] Admin endpoints

### Phase 4 (Weeks 6-7)
- [ ] Admin dashboard implementation
- [ ] CRUD operations
- [ ] Image uploads

### Phase 5 (Week 8)
- [ ] Redis caching
- [ ] Background job processing
- [ ] Performance optimization

### Phase 6 (Weeks 9-10)
- [ ] AI property recommendations
- [ ] Vector search
- [ ] Chat support

### Phase 7 (Week 11+)
- [ ] Microservices migration
- [ ] Event-driven architecture
- [ ] Mobile app (React Native)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit changes: `git commit -m 'Add amazing feature'`
4. Push to branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

## 📝 License

MIT License - See LICENSE file for details

## 👥 Team

Built as a production-grade platform for enterprise development teams.

## 📞 Support

For issues and questions:
- GitHub Issues: [Create an issue](https://github.com/yourusername/whatsreal/issues)
- Documentation: [docs/](docs/)
- Email: support@whatsreal.com

---

**Ready to start?** Follow the [Quick Start](#-quick-start) section or see [SETUP.md](SETUP.md) for detailed instructions.
