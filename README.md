JapTask1# Normative Calculator App

This application allows user to login and create recipes based on ingredients already seeded to database. All ingredients have name, quantity and price in database. App calculate cost of each recipe and gives user overview of costs of each recipe based on chosen ingredients. 


## Technologies

- .NET CORE 5
- Entity Framework with Microsoft SQL Server Express
- JWT for authentication

## Requirements

Please make sure you have .NET 5, Microsoft SQL Server Express installed on your local machine as application back-end will use IIS and MS SQL Server.

## Installation

- Clone git repository on your local machine
- CD into norm-calc folder

First you need to migrate and create database as follows: 

- Open project in Visual Studio
- Open Package manager console
- Make sure that default selected project is JapTask1.Database
- Enter to command line:

```
Add-Migration InitialCreate_FirstStartup
```

```
Update-Database
```

-Hit F5 to run the project

Please note that this is the backend part of the project and could be tested in swagger or Postman. Frontend part of the app is available in separated github repository.

## Version
Version: 1.0