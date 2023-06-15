# Library Web Application

A simple CRUD application that allow to manage a library only as a employee.

## Frontend - LibraryClient
**Stack:** Vue3, Bootstrap

**Libraries**<br>
Vue3 Router

## Backend - LibraryApi
**Stack:** ASP.NET Core (C#)

**Libraries**<br>
AutoMapper<br>
LINQ<br>
EntityFramework<br>
NLOG<br>
FluentValidation

## How to start?
 Requirements:
  - Microsoft .NET SDK 6.0 https://dotnet.microsoft.com/en-us/download/dotnet/6.0
  - Nodejs + npm https://nodejs.org/en/download
	<br>
	First: Create database named 'Library'<br>
	To run BackendAPI go to /LibraryAPI/LibraryAPI and run command:<br>
	  - 'dotnet restore'
	  - 'dotnet ef database update'
	  - 'dotnet run'
	<br><br>
To run FrontendClient go to /LibraryClient and run command <br>
	  - 'npm install'
	  - 'npm run dev'
  	website: http://localhost:5173 <br>

## To do
- Backend tests
- Frontend tests
- Docker
