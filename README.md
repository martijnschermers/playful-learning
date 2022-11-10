# Playful Learning

School project that shows of knowledge of ASP.NET with some additional frameworks and tools. 
This project contains a website build following the MVC pattern and a web api that implements some RESTful and GraphQL endpoints. 

## Screenshots

![App Screenshot](https://i.imgur.com/broAZmy.png)


## Run Locally

This project uses Entity Framework Core with a SQL server to persist data. 
To run the project locally the SQL server already needs to be set up. 
[This](https://learn.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver16) article explains how to set up a SQL server on your machine.

Clone the project

```bash
  git clone https://github.com/martijnschermers/playful-learning.git
```

Go to the project directory

```bash
  cd playful-learning
```

Install client dependencies

```bash
  cd Portal
  libman restore
```

Install server dependencies
```bash
  dotnet restore
```

Migrate and update the database
```bash
  dotnet ef database update --context "DomainDbContext" --startup-project="Portal" --project="SqlServer.Infrastructure"
```

Start the server

```bash
  dotnet run --project=Portal
```


## Running Tests

To run tests, run the following command

```bash
  dotnet test
```


## Tech Stack

**Client:** Bootstrap

**Server:** C#, ASP.NET Core MVC

**Testing:** Moq, xUnit

**Deployment:** Azure DevOps
