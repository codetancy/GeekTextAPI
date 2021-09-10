# GeekTextAPI

GeekText is Web API in .NET for an online bookstore.

## Technologies

- .NET 5
- Entity Framework Core
- Swagger
- XUnit
- Docker
- SQL Server

## Getting Started

***Note:** Before cloning the repository make sure to have both Docker and the 
.NET SDK installed in your machine. For a detailed guide on how to set up your 
environment, read the [Instructions](INSTRUCTIONS.md).*

### Running Locally

1. Clone the repository to your local machine. 
2. Open a new terminal instance and head to the project's root directory.
3. Start the database container with `docker-compose up -d`. 
4. Start the Web API with `dotnet run --project .\src\Web\Web.csproj`.
5. Navigate to `localhost:5000/swagger` to reach the API through swagger's UI.

## Overview

The source code of this project is split into two directories `src` and `tests`.
The former contains the web API project and class libraries, whereas the latter 
contains benchmarks and both unit and integration tests. Currently, we only have 
one Web project, which will eventually be separated into smaller ones.

### Web

- **Controllers:** Web API controllers separated by API version.
- **Data:** Entity Framework data context, model configurations, and migration history.
- **Models:** Business level entities.
- **Options:** Classes that map our `appsettings.json` configurations.
- **Repositories:** Data access services (interfaces and implementations).

## License

This project is licensed with the [MIT license](/LICENSE).
