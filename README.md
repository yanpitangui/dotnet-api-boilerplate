# dotnet-api-boilerplate
<p align="center">
  <span>English</span> |
  <a href="https://github.com/yanpitangui/dotnet-api-boilerplate/tree/main/translations/pt-br/README.md">Português</a>
</p>

A ``.Net 9.0`` WebApi boilerplate / template project. MediatR, Swagger, ~~AutoMapper~~ Mapster, Serilog and more implemented. 

[![Build](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml/badge.svg)](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=yanpitangui_dotnet-api-boilerplate&metric=coverage)](https://sonarcloud.io/dashboard?id=yanpitangui_dotnet-api-boilerplate)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=yanpitangui_dotnet-api-boilerplate&metric=alert_status)](https://sonarcloud.io/dashboard?id=yanpitangui_dotnet-api-boilerplate)

The goal of this project is to be a kickstart to your .Net WebApi, implementing the most common used patterns
and technologies for a restful API in .net, making your work easier.

# How to run
- Use this template(github) or clone/download to your local workplace.
- Download the latest .Net SDK and Visual Studio/Code/Rider.

## Standalone
1. You may need a running instance of Postgres, with appropriate migrations initialized.
	- You can run just the DB on docker. For that, run the following command: ``docker-compose up -d db-server``. Doing that, the application will be able to reach the container of the db server.
2. Go to the src/Boilerplate.Api folder and run ``dotnet run``, or, in visual studio set the api project as startup and run as console/docker/IIS.
3. Visit http://localhost:7122/api-docs or https://localhost:7123/api-docs to access the application's swagger.

## Docker
1. Run ``docker-compose up -d`` in the root directory, or, in visual studio, set the docker-compose project as startup and run. This should start the application and DB.
 - 1. For docker-compose, you should run this command on the root folder: ``dotnet dev-certs https -ep https/aspnetapp.pfx -p yourpassword``
		Replace "yourpassword" with something else in this command and the docker-compose.override.yml file.
This creates the https certificate.
2. Visit http://localhost:7122/api-docs or https://localhost:7123/api-docs to access the application's swagger.

## Running tests
**Important**: You need to have docker up and running. The integration tests will launch a Postgres container and use it to test the API.

In the root folder, run ``dotnet test``. This command will try to find all test projects associated with the sln file.
If you are using Visual Studio, you can also access the Test Menu and open the Test Explorer, where you can see all tests and run all of them or one specifically. 

## Authentication
In this project, some routes requires authentication/authorization. For that, you will have to use the ``api/identity/register`` route to create an account.
After that, you can login using the ``/api/identity/login`` without using cookies and then use received accessToken on the lock (if using swagger) or via the Authorization header on a http request.
For more information, please take a look on swagger documentation.

# This project contains:
- SwaggerUI
- EntityFramework
- Postgres
- Minimal apis
- Strongly Typed Ids
- Test coverage collection
- ~~AutoMapper~~ Mapster
- MediatR
- Feature slicing
- Serilog with request logging and easily configurable sinks
- .Net Dependency Injection
- Resource filtering
- Response compression
- Response pagination
- CI (Github Actions)
- Authentication
- Authorization
- Unit tests
- Integration tests with testcontainers
- Container support with [docker](src/Boilerplate.Api/dockerfile) and [docker-compose](docker-compose.yml)
- OpenTelemetry support (with OLTP as default exporter)
- NuGet Central package management (CPM)

# Project Structure
1. Services
	- This folder stores your apis and any project that sends data to your users.
	1. Boilerplate.Api
		- This is the main api project. Here are all the controllers and initialization for the api that will be used.
	2. docker-compose
		- This project exists to allow you to run docker-compose with Visual Studio. It contains a reference to the docker-compose file and will build all the projects dependencies and run it.
2. Application
	-  This folder stores all data transformations between your api and your domain layer. It also contains your business logic.
	1. Auth
		- This folder contains the login Session implementation.
3. Domain
	- This folder contains your business models, enums and common interfaces.
	1. Boilerplate.Domain
		- Contains business models and enums.
		1. Auth
			- This folder contains the login Session Interface.
4. Infra
	- This folder contains all data access configuration, database contexts, anything that reaches for outside data.
	1. Boilerplate.Infrastructure
		- This project contains the dbcontext, entities configuration and migrations.


# Adopting to your project
1. Remove/Rename all hero related stuff to your needs.
2. Rename solution, projects, namespaces, and ruleset to your use.
3. Change the dockerfile and docker-compose.yml to your new csproj/folder names.
3. Give this repo a star!

# Migrations
To run migrations on this project, you need the dotnet-ef tool.
- Run ``dotnet tool install --global dotnet-ef``
- Now, depending on your OS, you have different commands:
    1. For windows: ``dotnet ef migrations add InitialCreate --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\``
    2. For linux/mac: ``dotnet ef migrations add InitialCreate --startup-project ./src/Boilerplate.Api/ --project ./src/Boilerplate.Infrastructure/``
# If you like it, give it a Star
If this template was useful for you, or if you learned something, please give it a Star! :star:

# Thanks
This project has great influence of https://github.com/lkurzyniec/netcore-boilerplate and https://github.com/EduardoPires/EquinoxProject. If you have time, please visit these repos, and give them a star, too!

# About
This boilerplate/template was developed by Yan Pitangui under [MIT license](LICENSE).
