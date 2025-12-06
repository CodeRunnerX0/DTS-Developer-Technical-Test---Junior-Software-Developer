# Developer Technical Test - Task Management Application

This solution comprises of two projects:

1. TaskApi: An ASP.NET Core Web API (C#) for handling task data (Backend).

2. task-frontend: A React application (JavaScript) for the user interface (Frontend).

## 1. Start Instructions

The two projects must be run concurrently.

A. Start the Backend API (C#)

1. Open your terminal and navigate to the API directory:

cd TaskApi

2. Restore the NuGet packages and build the project:

dotnet restore

3. Run the API. 

dotnet run

The API should now be listening on http://localhost:5281.

B. Start the Frontend Application (React)

1. Open a new terminal window and navigate to the frontend directory:

cd task-frontend

2. Install dependencies:

npm install

3. Start the React server.

npm start

The application will automatically open in your browser at http://localhost:3000

## 2. Technical Overview

This solution was designed with clean separation and attention to data integrity:

* Architecture: Network logic is isolated in a Service Layer (taskApiService.js) separate from the main Task Form component.

* Data Handling: The form uses the following to ensure compatibility with the C# data model:

   1. Due Date: Uses the <input type="datetime-local" /> to capture the full date and time required by the backend's DateTime field.
   2. Status: A simple Boolean checkbox is used to capture the task completion status.

* Styling: A clean CSS design was used to prioritize readability and accessibility.

## 3. Troubleshooting & Environment Notes 

The primary setup challenge has been resolved.

* CORS Fix: If the frontend fails to connect, the API is configured with a specific CORS policy in Program.cs that allows access from http://localhost:3000, preventing connection errors.

* Error Handling: The frontend is configured try...catch blocks to display a error messages if the API is offline or returns a failure status.