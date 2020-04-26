# netcore-boilerplate-api
A `.Net Core 3.1` WebApi boilerplate / template project. Repositories, Swagger, Mapper, Serilog and more implemented. 


![Build and Test](https://github.com/yanpitangui/netcore-boilerplate-api/workflows/Build%20and%20Test/badge.svg)
[![License](https://img.shields.io/github/license/yanpitangui/netcore-boilerplate-api.svg)](LICENSE)

The goal of this project is to be a kickstart to your .Netcore WebApi, implementing the most common used patterns
and technologies for a restful API in .net core, making your work easier.

# How to run
- Use this template(github) or clone/download to your local workplace.
- Download the latest .NetCore SDK and Visual Studio/Code.

## Standalone
1. You may need a running instance of MsSQL, with appropriate migrations initialized.
	- If you want, you can change the DatabaseExtension to use UseInMemoryDatabase, instead of Mssql.
2. Go to the src/Boilerplate.Api folder and run ``dotnet run``, or, in visual studio set the api project as startup and run as console or docker (not IIS).
3. Visit http://localhost:5000/api-docs or https://localhost:5001/api-docs to access the application's swagger.

## Docker
1. Run ``docker-compuse up -d`` in the root directory, or, in visual studio, set the docker-compose project as startup and run. This should start the application, DB. ~~and Redis cache (optional)~~
2. Visit http://localhost:5000/api-docs or https://localhost:5001/api-docs to access the application's swagger.

## Running tests
In the root folder, run ``dotnet test``. This command will try to find all test projects associated with the sln file.

# This project contains:
- SwaggerUI
- EntityFramework Core
- Code quality ruleset
- AutoMapper
- Generic repository (to easily bootstrap a CRUD repository)
- Serilog with request logging and easily configurable sinks
- .NetCore Dependency Injection
- Resource filtering
- Response compression
- CI (Github Actions)
- Unit tests
- Integration tests
- Container support with [docker](src/Boilerplate.Api/dockerfile) and [docker-compose](docker-compose.yml)


# Project Structure
1. Services
	- This folder stores your apis and any project that sends data to your users.
	1. Boilerplate.Api
		- This is the main api project. Here are all the controllers and initialization for the api that will be used.
	2. docker-compose
		- This project exists to allow you to run docker-compose with Visual Studio. It contains a reference to the docker-compose file and will build all the projects dependencies and run it.
2. Application
	-  This folder stores all data transformations between your api and your domain layer. It also contains your business logic.
3. Domain
	- This folder contains your business models, enums and common interfaces.
	1. Boilerplate.Domain.Core
		- Contains the base entity for all other domain entities, as well as the interface for the repository implementation.
	1. Boilerplate.Domain
		- Contains business models and enums.
4. Infra
	- This folder contains all data access repositories, database contexts, anything that reaches for outside data.
	1. Boilerplate.Infrastructure
		- This project contains the dbcontext, an generic implementation of repository pattern and a Hero(domain class) repository.


# Adopting to your project
1. Remove/Rename all hero related stuff to your needs.
2. Rename solution, projects, namespaces, and ruleset to your use.
3. Change the dockerfile and docker-compose.yml to your new csproj/folder names.
3. Give this repo a star!

# TODO
- Finish implementation of integration tests
- Create an MSSQL DBcontext example
- Implement redis cache

# If you like it, give it a Star
If this template was useful for you, or if you learned something, please give it a Star! :star:

# Thanks
This project has great influence of https://github.com/lkurzyniec/netcore-boilerplate and https://github.com/EduardoPires/EquinoxProject. If you have time, please visit these repos, and give them a star, too!

# About
This boilerplate/template was developed by Yan Pitangui under [MIT license](LICENSE).
