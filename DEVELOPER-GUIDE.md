# WhatsReal - Developer Quick Reference

## 🚀 Getting Started in 5 Minutes

### Start Backend
```bash
cd src/Api
dotnet run
# Open: http://localhost:5000/swagger
```

### Start Frontend  
```bash
cd src/Frontend
npm install  # First time only
npm run dev
# Open: http://localhost:3000
```

## 📁 Key Directories

| Directory | Purpose |
|-----------|---------|
| `src/Api` | ASP.NET Core entry point (Minimal APIs) |
| `src/Application` | Business logic + MediatR handlers |
| `src/Domain` | Core entities + interfaces |
| `src/Infrastructure` | Repositories + mock data |
| `src/Shared` | DTOs + response wrappers |
| `src/Frontend` | React + Vite application |
| `docker/` | Docker configs + Nginx |
| `docs/` | Architecture & deployment docs |

## 🛠️ Adding a New Feature

### Backend - New API Endpoint

1. **Create Entity** (if needed)
   - `src/Domain/Entities/MyEntity.cs`

2. **Create Interfaces**
   - `src/Domain/Interfaces/IMyRepository.cs`

3. **Create DTOs**
   - `src/Shared/DTOs/MyDtos.cs`

4. **Create Handlers**
   - `src/Application/Features/MyFeature/Queries/GetMyQuery.cs`
   - `src/Application/Features/MyFeature/Commands/CreateMyCommand.cs`

5. **Create Endpoints**
   - `src/Api/Endpoints/MyEndpoints.cs`
   - Map in `Program.cs`

6. **Implement Repository**
   - `src/Infrastructure/Repositories/MockMyRepository.cs`

### Frontend - New Page

1. **Create Page Component**
   - `src/Frontend/src/pages/MyPage.tsx`

2. **Create Hook** (if data needed)
   - `src/Frontend/src/hooks/useMyData.ts`

3. **Create API Method**
   - Add to `src/Frontend/src/services/apiClient.ts`

4. **Add Route**
   - Update `src/Frontend/src/App.tsx`

5. **Update Navigation**
   - Add link to `src/Frontend/src/components/Header.tsx`

## 📊 API Testing

### Using Swagger UI
- Open: http://localhost:5000/swagger
- All endpoints documented with request/response examples

### Using cURL
```bash
# Get properties
curl http://localhost:5000/api/v1/properties

# Search with filters
curl "http://localhost:5000/api/v1/properties/search?city=Boston&bedrooms=2"

# Get agent
curl http://localhost:5000/api/v1/agents

# Create inquiry
curl -X POST http://localhost:5000/api/v1/inquiries \
  -H "Content-Type: application/json" \
  -d '{
    "propertyId": "guid",
    "contactName": "John",
    "email": "john@example.com",
    "phoneNumber": "+12125551234",
    "message": "Interested in this property"
  }'
```

## 🎨 Frontend Development

### Component Locations
- **UI Components**: `src/components/` (reusable)
- **Pages**: `src/pages/` (route components)
- **Hooks**: `src/hooks/` (custom logic)
- **Types**: `src/types/` (TypeScript types)
- **Services**: `src/services/` (API calls)
- **Stores**: `src/stores/` (Zustand state)

### Common Patterns

**Fetch Data**
```tsx
import { useQuery } from '@tanstack/react-query';

const { data, isLoading, error } = useQuery({
  queryKey: ['properties'],
  queryFn: () => apiClient.getAllProperties(),
  select: (response) => response.data
});
```

**Use Theme**
```tsx
import { useTheme } from '@hooks/useTheme';

const { isDarkMode, toggleTheme } = useTheme();
```

**Store State**
```tsx
import { useAppStore } from '@stores/appStore';

const { savedProperties, addSavedProperty } = useAppStore();
```

## 🧪 Testing

```bash
# Backend unit tests
dotnet test

# Run specific test project
dotnet test src/Tests/Domain.Tests/

# With coverage (install coverlet first)
dotnet test /p:CollectCoverage=true
```

## 🐳 Docker Commands

```bash
# Start all services
docker-compose -f docker/docker-compose.yml up -d

# View logs
docker-compose -f docker/docker-compose.yml logs -f api

# Stop all
docker-compose -f docker/docker-compose.yml down

# Rebuild
docker-compose -f docker/docker-compose.yml build --no-cache
```

## 📝 Code Style

### C# Naming
- Classes: `PascalCase` (e.g., `PropertySearchQuery`)
- Methods: `PascalCase` (e.g., `GetPropertyById`)
- Properties: `PascalCase` (e.g., `PropertyType`)
- Variables: `camelCase` (e.g., `propertyCount`)
- Constants: `UPPER_SNAKE_CASE` (e.g., `MAX_PAGE_SIZE`)

### React/TypeScript Naming
- Components: `PascalCase` (e.g., `PropertyCard`)
- Functions: `camelCase` (e.g., `getPropertyStatus`)
- Variables: `camelCase` (e.g., `propertyCount`)
- Interfaces: `PascalCase` (e.g., `PropertyDto`)

## 🔑 Important URLs

| Service | URL | Purpose |
|---------|-----|---------|
| API | http://localhost:5000 | Backend API |
| Swagger | http://localhost:5000/swagger | API Documentation |
| Health | http://localhost:5000/health | Health Check |
| Frontend | http://localhost:3000 | React App |
| Postgres | localhost:5432 | Database (Phase 2) |
| Redis | localhost:6379 | Cache (Phase 2) |

## 📚 Documentation

- **Architecture**: `docs/ARCHITECTURE.md`
- **API Design**: `docs/API-DESIGN.md`
- **Deployment**: `docs/DEPLOYMENT.md`
- **Database**: `docs/DATABASE-MIGRATION.md`
- **Setup**: `SETUP.md`
- **README**: `README.md`

## 🐛 Debugging

### Backend
```csharp
// Add logging
_logger.LogInformation("Processing property {PropertyId}", propertyId);

// Debug breakpoints in Visual Studio Code
// Create .vscode/launch.json (example in docs)
```

### Frontend
```typescript
// Use browser DevTools (F12)
console.log('Debug info:', data);

// React DevTools Browser Extension
// Redux DevTools (Zustand has middleware)
```

## 🚀 Build & Deploy

```bash
# Build backend
dotnet build -c Release

# Publish backend
dotnet publish -c Release

# Build frontend
cd src/Frontend
npm run build

# Build Docker images
docker build -f docker/Dockerfile -t whatsreal-api:latest .
docker build -f docker/Dockerfile.frontend -t whatsreal-ui:latest .
```

## 💾 Database (Phase 2)

```bash
# Create migration
dotnet ef migrations add AddMyTable -p src/Infrastructure

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Script migrations (for deployment)
dotnet ef migrations script -o migrations.sql
```

## 🔐 Security Notes

- Secrets: Use Environment Variables or User Secrets
- API Keys: Store in appsettings.json (not in repo)
- Passwords: Hash with BCrypt (Phase 3)
- Auth: JWT tokens (Phase 3)
- CORS: Configured per environment

## 📞 Common Issues

| Issue | Solution |
|-------|----------|
| Port 5000 in use | Change in `appsettings.json` or use `--urls` flag |
| npm install fails | `npm cache clean --force` then `npm install` |
| Docker build fails | Check `.dockerignore` and rebuild with `--no-cache` |
| CORS errors | Ensure API is running and check CORS config |
| TypeScript errors | Run `npm install @types/packagename` or check tsconfig |

## 🎯 Next Phase Checklist

- [ ] Complete all Phase 1 features
- [ ] 80%+ test coverage
- [ ] Performance benchmarks
- [ ] Security audit
- [ ] Documentation review
- [ ] Team sign-off for Phase 2

---

**Need help?** Check `docs/` folder or create a GitHub issue.
