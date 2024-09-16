# ASP.NET + PostrgeSQl

This template should help get you started developing.

## Recommended IDE Setup

[JetBrains Rider](https://www.jetbrains.com/rider/) | [Visual Studio](https://visualstudio.microsoft.com)

## Required SDKs

[.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## The database used

[PostgreSQL](https://www.postgresql.org)

You need to create an account ```postgres``` with a password ```postgres```.

The database must be created independently the first time it is accessed.

## Project Setup

### Compile and Start

Run the following command in the ```Twaddle``` directory.
```sh
dotnet run
```
The backend and frontend should start at the same time.


### Configuration ```appsettings.json```
* **AppSettings** - token for jwt authorization encryption.
* **ConnectionStrings** - database connection string.
