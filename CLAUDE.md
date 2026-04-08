# Assignment: CI/CD Pipeline & Cloud Deployment

## Assignment Summary
School assignment to set up a fully automated CI/CD pipeline and deploy an application to a cloud service. No manual steps allowed in the pipeline.

**Grading:** IG or G  
**Option chosen:** Option 2 (fullstack with backend API + frontend)

### Requirements
- [ ] At least 3 unit tests (not integration tests)
- [ ] Pipeline: Test → Build (Docker image) → Deploy (automatic, no manual steps)
- [ ] Application accessible on a public URL after deployment
- [ ] README with link to live site + project description

---

## Stack
- **Backend:** .NET 10 ASP.NET Core API (`CompanyTracker.Api`)
- **Database:** PostgreSQL (via `docker-compose.yml` locally, managed Render Postgres in production)
- **Tests:** xUnit — currently only integration tests, unit tests still needed
- **Frontend:** Minimal static HTML page served by the .NET API (plain HTML + fetch calls)
- **Containerization:** Docker (multi-stage Dockerfile — done)
- **Cloud:** Render (free tier, supports Docker + managed Postgres)
- **CI/CD:** GitHub Actions

---

## Project Structure
```
Api_ex_2/
  CompanyTracker.Api/       # .NET 9 API
  CompanyTracker.Tests/     # xUnit tests (integration tests currently)
  docker-compose.yml        # Local Postgres setup
  .github/
    workflows/
      build.yml             # Builds, tests, publishes, uploads artifact on push to main
      dotnet.yml            # Redundant default template — to be deleted
      version-bump.yml      # Manual patch version bump — to be replaced
    release.yml             # Changelog category config
```

---

## Pipeline Design (target)
```
PR to main   → CI:  build + test (fail fast, no deploy)
Push to main → CD:  build Docker image → push to registry → Render auto-redeploys
```

GitHub Releases triggered on push to main (after PR merge).
Releases use semantic versioning tags (vX.X.X) with automatic patch increment (e.g. v1.0.0 → v1.0.1).

---

## Current Workflow Files — Status
| File | Status | Action |
|---|---|---|
| `build.yml` | CI/CD overlap, solid base | Refactor into CI-only |
| `dotnet.yml` | Redundant, wrong .NET version (8) | Delete |
| `version-bump.yml` | Manual only | Replace with automated release workflow |
| `release.yml` | Changelog config, keep | Keep as-is |

---

## Progress Checklist
- [ ] Delete `dotnet.yml`
- [x] Create `Dockerfile` (multi-stage, .NET 10)
- [ ] Add minimal frontend (static HTML served by API)
- [ ] Set up Render account + service + managed Postgres
- [ ] Refactor `build.yml` into CI workflow (PR to main: build + test)
- [ ] Create CD workflow (push to main: build Docker image → push → Render deploys)
- [ ] Add at least 3 unit tests
- [ ] Update README with live URL + project description

---

## Guiding Principles (from user)
- Teach/guide the user, don't just hand over code
- Frontend must be minimal
- User is learning — explain the *why*, not just the *what*
- Keep responses concise, no bloat