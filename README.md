# NZWalks

**NZWalks** is an ASP.NET Core Web API project that provides CRUD functionality for hiking trails in New Zealand. This project includes:

- **Backend API**: A RESTful API built using ASP.NET Core.
- **Frontend UI**: An ASP.NET Core MVC application that interacts with the API.

##  Project Structure

- `NZWalks.API/` – The backend API project handling data access, authentication and authorization.
- `NZWalks.UI/` – The MVC frontend project providing the user interface.

##  Features

- Full CRUD operations for regions and Walks.
- Data access with Entity Framework Core.
- JWT-based authentication and authorization
- MVC frontend communicates with the API via `HttpClientFactory`.
- Includes custom error handling middleware and logging via Serilog.

