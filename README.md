# User Permission System

This repository contains two projects: a .NET Core 6 Web API for managing user permissions and a corresponding xUnit project for unit testing. The system provides functionalities for requesting, modifying, and retrieving user permissions. Each service persists permission records in an Elasticsearch index. It uses the repository and unit of work patterns.

## Overview

This project consists of two main components:

1. **UserPermissionApi**: This is a .NET Core 6 Web API that provides HTTP endpoints for managing user permissions. It uses SQL Server and Entity Framework Core for data persistence and Elasticsearch for indexing permission records. The API supports the following actions:

   - **Request Permission**: Allows users to request permissions.
   - **Modify Permission**: Allows users to modify existing permissions.
   - **Get Permissions**: Provides the ability to retrieve user permissions.

2. **UserPermissionUnitTest**: This project contains unit tests for the UserPermissionApi project. It ensures that the API functions as expected and handles requests and modifications correctly.

The UserPermissionApi project is intended to be consumed by a React.js application available [here](https://github.com/jm-ramirez/UserPermissionApplication). The API must be running to enable the React application to interact with it.

## Getting Started

### Prerequisites

Before getting started, make sure you have the following prerequisites installed:

- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started)
- [Sql Server](https://www.microsoft.com/es-ar/sql-server/sql-server-downloads)

### Setting Up the Database

1. Configure the database connection string in `UserPermissionApi/appsettings.json`. Update the `"DefaultConnection"` value as needed:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=<your-database-server>;Database=UserPermission;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   
2. Use Entity Framework Core's Code First approach to create the database and tables. Open a terminal in the UserPermissionApi directory and run:

   ```bash
   dotnet ef database update
   ```

### Running the Projects

1. Build and run the UserPermissionApi project:

  ```bash
  cd UserPermissionApi
  dotnet run
  ```

2. Build and run the UserPermissionUnitTest project in a Docker container:

  ```bash
  docker build -t userpermissionunittest-image -f UserPermissionUnitTest/Dockerfile .
  docker run userpermissionunittest-image
  ```
The API should now be running at http://localhost:5000.

### Testing
The unit tests for the UserPermissionApi project can be found in the UserPermissionUnitTest project. These tests ensure that the API functions correctly. You can run the tests using the following command within the UserPermissionUnitTest directory:

  ```bash
  dotnet test
  ```

### Running with Docker

UserPermissionApi
Build the Docker Image:

To create a Docker image for the UserPermissionApi, navigate to the directory where your Dockerfile is located (root of your UserPermissionApi project) and run the following command:

  ```bash
  docker build -t userpermissionapi-image .
  ```

2. Run the Docker Container:

After building the image, you can run the UserPermissionApi in a Docker container using the following command:

  ```bash
  docker run -d -p 5000:80 --name userpermissionapi-container userpermissionapi-image
  ```
-d: Runs the container in detached mode.

-p 5000:80: Maps port 5000 on your local machine to port 80 in the container. Adjust the ports as needed.

--name userpermissionapi-container: Assigns a name to the container for easy management.


3. Access the API:

You can now access the API at http://localhost:5000. The API should be up and running.

UserPermissionUnitTest
Build the Docker Image:

1. Navigate to the directory where your Dockerfile for the UserPermissionUnitTest is located (usually the root of your UserPermissionUnitTest project) and run the following command:

  ```bash
  docker build -t userpermissionunittest-image .
  ```

2. Run the Docker Container:

After building the image, run the UserPermissionUnitTest in a Docker container with this command:

  ```bash
  docker run userpermissionunittest-image
  ```
This will execute the unit tests in the container.

That's it! You now have both the UserPermissionApi and the UserPermissionUnitTest projects running in Docker containers. The UserPermissionApi should be accessible at http://localhost:5000, and the unit tests will run in the UserPermissionUnitTest container.

