# Library CRUD demo (.NET Core + Vue.js)

A coding test project with a .NET Core API and a Vuejs web client.

## Project Setup

### Prerequisites

Follow the installation instructions for each dependency on their respective official web pages. (link over the dependency name)

[Windows compatible with .NET Core 3.1 runtime or any other compatible system](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=netcore31)

[.NET 5.0 LTS (SDK and Runtime)](https://dotnet.microsoft.com/download/dotnet/5.0)

[SQL Server 2019 (Developer or Express)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

[Node package manager (npm == lts~v14.15.4)](https://nodejs.org/es/download/package-manager/)

### Manual development environment setup

In the root directory of the repository. Open a command prompt of your choice and enter the following commands:

#### 1. _install ef and create the database_

> skip this step if you want to use in-memory database.

```bash
  cd Source/
  dotnet tool install --global dotnet-ef
  dotnet build
  dotnet ef database update --project ./Library.Api
```

#### 2. _install node modules_

```bash
  cd ..
  cd Library.Vuejs/
  npm install
```

### Running the projects

To run the API and Vue.js projects, you will need to open a command prompt for each one. Go to the repository path, from there run:

#### 1. _Running the API_

```bash
  cd Source/
  dotnet run --project ./Library.Api
```

also you can pass an application argument `migrate` to make the initial migration and run the API.

```bash
  dotnet run --project ./Library.Api -- migrate
```

additionally you can pass the argument `--configuration` to use `SQLIte` instead of `SQLServer` data provider.

```bash
  dotnet run --project ./Library.Api -c MOCK -- migrate
```

if you are using **Visual Studio Code** you can launch the debugger by pressing:

<kbd>F5</kbd>

#### 2. _Running the Web Client_

```bash
  cd Source/Library.Vuejs/
  npm run serve
```

if you are using **Visual Studio Code** you can run the task `npm: serve - Source/Library.Vuejs`, by using.

<kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>P</kbd> then select `Tasks: Run Task` > `npm: serve - Source/Library.Vuejs`.

## Project details

### General information

This is a demo application using .NET 5.0 and Vuejs v3. Consist on Library web site that allows you to borrow and devolve a book as well to filter by title autor, category and availability. Can add new books to the list and edit then.

Here are some information about the architectures, code principles, design patterns and libraries used in this project.

#### Architectures

| API      | Web Client      |
| :------- | :-------------- |
| N-Layers | Component-Based |
| MVC      |                 |
| Onion    |                 |

#### Code principles

- Clean
- SOLID
- Defensive Coding
- CQRS

#### Design patterns

- Command
- Factory
- Builder
- Dependency Injection
- Repository
- Unit of Work

#### Libraries

| API                 | Web Client          |
| :------------------ | :------------------ |
| EntityFrameworkCore | axios               |
| FluentValidations   | vee-validate        |
| AutoMapper          | pikaday             |
| Serilog             | vue-router          |
| GuardClauses        | bootstrap-vue       |
| OpenApi             | vue-class-component |
| Newtonsoft.Json     | guid-typescript     |

#### Have a great time

I hope you find this material very educational and useful. :-)
