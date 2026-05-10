# WhatsReal - Complete Setup Guide

Step-by-step guide to get WhatsReal running on your local machine or deploy to production.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Local Development Setup](#local-development-setup)
3. [Docker Setup](#docker-setup)
4. [Verification](#verification)
5. [Troubleshooting](#troubleshooting)

## Prerequisites

### Required Software

- **Git**: Version control
  - Download: https://git-scm.com/download
  - Verify: `git --version`

- **.NET SDK 10.0** or later
  - Download: https://dotnet.microsoft.com/download/dotnet/10.0
  - Verify: `dotnet --version`

- **Node.js 20** or later (LTS recommended)
  - Download: https://nodejs.org/en/download
  - Verify: `node --version` and `npm --version`

- **Docker & Docker Compose** (for containerized setup)
  - Download: https://www.docker.com/products/docker-desktop
  - Verify: `docker --version` and `docker-compose --version`

### System Requirements

- **OS**: Windows 10+, macOS, or Linux
- **RAM**: Minimum 8GB (16GB recommended)
- **Disk**: At least 10GB free space
- **Ports**: 5000, 5001, 3000, 80, 443 (for development and Docker)

## Local Development Setup

### Step 1: Clone Repository

```bash
git clone https://github.com/yourusername/whatsreal.git
cd whatsreal
```

### Step 2: Restore Dependencies

#### Backend

```bash
# Restore NuGet packages
dotnet restore WhatsReal.sln
```

#### Frontend

```bash
cd src/Frontend
npm install
cd ../..
```

### Step 3: Run Backend API

**Terminal 1 - API Server:**

```bash
cd src/Api
dotnet run
```

You should see output similar to:
```
info: WhatsReal.Api[0]
      WhatsReal API starting...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

**Verify API is running:**
- Open browser: http://localhost:5000/health
- Response: `{"status":"Healthy"}`

**View API Documentation:**
- Open browser: http://localhost:5000/swagger
- You should see interactive API documentation

### Step 4: Run Frontend

**Terminal 2 - React Development Server:**

```bash
cd src/Frontend
npm run dev
```

You should see output like:
```
VITE v5.3.1  ready in 234 ms

➜  Local:   http://localhost:3000/
➜  press h to show help
```

**Access Frontend:**
- Open browser: http://localhost:3000
- You should see WhatsReal home page

### Step 5: Test Full Stack

1. **Test Property Search**
   - Click "Browse Properties" or navigate to http://localhost:3000/properties
   - You should see the property search page

2. **Test API Endpoints**
   - Use Swagger UI at http://localhost:5000/swagger
   - Try: GET `/api/v1/properties`
   - You should see a list of 25 properties

3. **Check Console**
   - Frontend console (F12) should have no errors
   - API logs should show requests

## Docker Setup

### Option 1: Full Stack with Docker Compose

**Easiest way to run the entire application:**

```bash
# From project root
docker-compose -f docker/docker-compose.yml up -d
```

This will start:
- API on port 5000
- Frontend on port 80 (through Nginx)
- PostgreSQL database on port 5432
- Redis cache on port 6379
- Nginx reverse proxy on port 80

**Verify Services:**

```bash
# Check container status
docker-compose -f docker/docker-compose.yml ps

# View logs
docker-compose -f docker/docker-compose.yml logs -f api
```

**Access Services:**
- Frontend: http://localhost
- API: http://localhost:5000
- API Docs: http://localhost:5000/swagger

**Stop Services:**

```bash
docker-compose -f docker/docker-compose.yml down
```

### Option 2: Build & Run Docker Image Manually

**Build API Docker image:**

```bash
docker build -f docker/Dockerfile -t whatsreal-api:latest .
```

**Run API container:**

```bash
docker run -d -p 5000:5000 --name whatsreal-api whatsreal-api:latest
```

**Verify container is running:**

```bash
docker ps
docker logs whatsreal-api
```

## Verification

### Checklist - Local Development

- [ ] `dotnet build` succeeds with no errors
- [ ] `dotnet run` (from Api folder) starts without errors
- [ ] Browser: http://localhost:5000/health returns `{"status":"Healthy"}`
- [ ] Browser: http://localhost:5000/swagger shows API documentation
- [ ] Browser: http://localhost:3000 loads home page
- [ ] Browser Console (F12) has no errors
- [ ] API logs show incoming requests

### Checklist - Docker Setup

- [ ] `docker-compose ps` shows 5 running containers
- [ ] `docker-compose logs api` shows healthy startup
- [ ] Browser: http://localhost/health returns healthy status
- [ ] Browser: http://localhost/api/v1/properties returns property list
- [ ] Browser: http://localhost shows home page

### Test Data

The application seeds with:
- **25 Properties** across 7 US cities
- **5 Agents** with diverse specialties
- **Sample Property Inquiries** (empty, ready for user submissions)

**Example Test Cases:**

1. **Search by Price Range**
   - API: `GET /api/v1/properties/search?minPrice=500000&maxPrice=2000000`
   - Should return apartments and condos in price range

2. **Search by City**
   - API: `GET /api/v1/properties/search?city=New York`
   - Should return 4 New York properties

3. **Get Agent Details**
   - API: `GET /api/v1/agents/11111111-1111-1111-1111-111111111111`
   - Should return agent with property list

4. **Create Inquiry**
   - API: `POST /api/v1/inquiries`
   - Body: `{"propertyId":"...", "contactName":"John","email":"john@example.com","phoneNumber":"+1234567890","message":"I'm interested..."}`
   - Should return inquiry confirmation

## Troubleshooting

### Backend Issues

#### Port 5000 already in use

```bash
# Find process using port 5000
netstat -tlnp | grep 5000  # Linux/Mac
netstat -ano | findstr :5000  # Windows

# Kill the process or use different port
dotnet run --urls "http://localhost:5001"
```

#### NuGet restore fails

```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore again
dotnet restore
```

#### Build fails with "SDK not found"

```bash
# Check .NET version
dotnet --version

# Install .NET 10.0
# Visit: https://dotnet.microsoft.com/download/dotnet/10.0
```

### Frontend Issues

#### npm install fails

```bash
# Clear npm cache
npm cache clean --force

# Delete node_modules and package-lock.json
rm -rf node_modules package-lock.json

# Reinstall
npm install
```

#### Vite port 3000 already in use

```bash
# Use different port
npm run dev -- --port 3001
```

#### API calls failing (CORS errors)

Ensure:
- Backend API is running on port 5000
- Frontend is making requests to `http://localhost:5000`
- CORS is enabled in API (should be by default)

Check `vite.config.ts` proxy configuration

### Docker Issues

#### Containers won't start

```bash
# Check logs
docker-compose logs api

# Rebuild images
docker-compose build --no-cache

# Start fresh
docker-compose down -v  # Remove volumes too
docker-compose up -d
```

#### Port conflicts

Modify `docker-compose.yml`:
```yaml
api:
  ports:
    - "5000:5000"  # Change first number to 5001, 5002, etc.
```

#### Permission denied errors

```bash
# On Linux, add your user to docker group
sudo usermod -aG docker $USER
newgrp docker  # Apply group changes
```

### Database Connectivity

If using PostgreSQL (Phase 2+):

```bash
# Connect to database
psql -h localhost -U postgres -d whatsreal

# Check tables
\dt

# Exit
\q
```

## Environment Configuration

### Backend Environment Variables

Create `src/Api/appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  },
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=whatsreal;User Id=postgres;Password=postgres;"
  },
  "Jwt": {
    "Key": "your-secret-key-min-32-chars-long",
    "Issuer": "whatsreal-api",
    "Audience": "whatsreal-users"
  }
}
```

### Frontend Environment Variables

Create `src/Frontend/.env.local`:

```env
VITE_API_URL=http://localhost:5000/api/v1
```

## Common Commands

### Backend

```bash
# Build
dotnet build

# Run
dotnet run

# Run specific project
dotnet run --project src/Api

# Run tests
dotnet test

# Clean
dotnet clean
```

### Frontend

```bash
# Install dependencies
npm install

# Start dev server
npm run dev

# Build for production
npm run build

# Preview build
npm run preview

# Lint code
npm run lint

# Fix linting issues
npm run lint:fix
```

### Docker

```bash
# Build images
docker-compose build

# Start services
docker-compose up -d

# Stop services
docker-compose down

# View logs
docker-compose logs -f

# Remove everything (including volumes)
docker-compose down -v
```

## Next Steps

1. **Explore the Code**
   - Review architecture in `/docs/ARCHITECTURE.md`
   - Check API design in `/docs/API-DESIGN.md`

2. **Run Tests**
   - `dotnet test` for backend unit tests
   - Frontend tests coming in Phase 2

3. **Modify & Extend**
   - Add new properties to seed data
   - Create new API endpoints
   - Add React components

4. **Deploy**
   - See `/docs/DEPLOYMENT.md` for production deployment
   - Docker images ready for cloud platforms

## Getting Help

- **Documentation**: See `/docs` folder
- **GitHub Issues**: Report bugs and request features
- **API Documentation**: http://localhost:5000/swagger (when running locally)

## Phase-Specific Setup

### Phase 2 - Database Migration

When ready to migrate from mock data to PostgreSQL:

1. Run database migrations: `dotnet ef database update`
2. Update connection string in appsettings
3. Switch repositories from Mock to EF Core
4. See `docs/DATABASE-MIGRATION.md`

### Phase 3 - Authentication

When adding JWT authentication:

1. Configure JWT settings in appsettings
2. Update startup configuration
3. Add `[Authorize]` attributes to endpoints
4. See `docs/SECURITY.md`

---

**Congratulations!** Your WhatsReal development environment is ready. Start building amazing features! 🚀
