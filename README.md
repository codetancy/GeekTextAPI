# GeekTextAPI

GeekText is Web API in .NET for an online bookstore.

## Technologies

- .NET 5
- Entity Framework Core
- Swagger
- XUnit
- FluentValidation
- Docker
- SQL Server

## Overview

The source code of this project is split into two directories `src` and `tests`.
The former contains the web API project and class libraries, whereas the latter 
contains benchmarks and both unit and integration tests. Currently, we only have 
one Web project, which will eventually be separated into smaller ones.

### Web

- **Contracts:** API contracts separated by version.
- **Controllers:** API controllers separated by version.
- **Data:** Entity Framework data context, model configurations, and migrations.
- **Models:** Business level entities.
- **Options:** Classes that map our `appsettings.json` configurations.
- **Repositories:** Data access services (interfaces and implementations).
- **Validators:** Model validation rules.

## License

This project is licensed with the [MIT license](/LICENSE).