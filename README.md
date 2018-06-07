# Learning.Api
JWT Authentication using .NET Core

Summary
-----------------------------------------------------------------------------------
Learning.Api is for User Authentication using JWT in .Net Core. It is a REST API.

Prerequisites:
----------------------------------------------------------------------------------
Visual Studio 2017
.Net Core
MySQL

Development Environment Setup
----------------------------------------------------------------------------------
Clone or download project in VS 2017

MySQL setup: Learning.Api application requires a MySQL database to store its data. Make sure to update the file "appsettings.json" file, 
changing the connection string named "LearningDb" to reference your MySQL server.

Learning api application:
- On any terminal move to the "Learning.Api" folder (the folder containing the "Learning.Api.csproj" file) and execute these commands:

dotnet restore
dotnet build
dotnet ef database update
dotnet run

- The application will be listening on http://localhost:53142
- Now you can call the api using any tool, like Postman, Curl, etc 
