# Hikru.Position.Backend – RESTful API in .NET 8

This project is the backend portion of the Full Stack Lead technical assessment, developed using ASP.NET Core 8. It follows clean architecture principles, the CQRS pattern, unit testing, error handling, and CI/CD deployment.

## Implemented Features

- ASP.NET Core 8 Web API
- Layered architecture (Controller, Application, Domain, Infrastructure)
- CQRS pattern using MediatR
- Dependency Injection throughout the application
- Unit testing with xUnit and Moq
- RESTful endpoints with proper structure and naming
- Custom middleware for centralized error handling
- GitHub Actions for CI/CD pipeline
- Automated deployment to Azure Web App

## Available Endpoints

Base service URL: `https://hikru-api-cdgga0g0cxdvg0e6.centralus-01.azurewebsites.net`

(GET) /api/positions: Retrieve all job positions
(GET) /api/positions/{id}: Retrieve position details by ID
(POST) /api/positions: Create a new position
(PUT) /api/positions/{id}: Update an existing position
(DELETE) /api/positions/{id}: Delete a position
(POST) /api/auth/login: Basic login with JWT token
(GET) /api/recruiters: Retrieve all recruiters
(GET) /api/departments: Retrieve all departments


## Data Model

Each job position includes the following fields:

- Title (required, max 100 characters)
- Description (required, max 1000 characters)
- Location (required)
- Status (draft, open, closed, archived)
- RecruiterId (required)
- DepartmentId (required)
- Budget (required)
- ClosingDate (optional)

## Error Handling

The API returns standard error codes:

- 400 – Bad Request (invalid fields, missing data)
- 401 – Unauthorized (missing token)
- 403 – Forbidden (invalid or unauthorized token)
- 404 – Not Found (e.g., non-existent position)

## Testing

- Unit tests have been implemented using xUnit and Moq for all handlers (Create, Update, Delete, GetById, GetAll).
- Integration tests are not included due to time constraints.

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or newer (Community Edition is sufficient)

## How to Run Locally

1. Open the solution file `Hikru.Position.Backend.sln` using Visual Studio Community.

2. Press `F5` to run the application. This will launch the API in your default browser.

3. Swagger documentation will be available at `https://localhost:{port}/swagger`, where you can test all endpoints interactively.

> Note: No need to apply database migrations. The solution connects directly to the SQL Server instance hosted in Azure.

## Troubleshooting: SQL Server Access

The application connects to an Azure SQL Server instance, which may block access if your IP is not authorized.

### If You Cannot Connect to Azure SQL Server:

1. **Option A (Recommended)** – Use a local SQL Server:
   - Install SQL Server Express or SQL Server Developer Edition.
   - Update the connection string in `appsettings.json` with your local instance.
   - Open the Package Manager Console and run:
     ```powershell
     Update-Database
     ```
   - Then press `F5` to launch the application.

2. **Option B (Advanced)** – Request Azure IP access:
   - Ask the developer to add your public IP to the Azure SQL Server firewall.
   - This will allow direct access to the production database.

## Production Deployment

The API has been deployed to Azure Web App and is accessible at:

```
https://hikru-api-cdgga0g0cxdvg0e6.centralus-01.azurewebsites.net
```

You can consume the endpoints from any HTTP client or connected frontend.

## Final Notes

This backend was built with a focus on maintainability, separation of concerns, and test coverage. All requirements from the technical assessment have been implemented, except for integration testing, which was left out due to time constraints.

For any questions or technical details, feel free to contact the developer of this solution.
