# Book Management API

This project is a .NET 8 Web API for managing book operations. It utilizes Entity Framework with SQLite for database operations and implements the Repository pattern along with Data Transfer Objects (DTOs) for data handling.

## Project Structure

The project is organized into three main projects:

- **BookManagementAPI.API**: Contains the API controllers, DTOs, and configuration files.
- **BookManagementAPI.Core**: Contains the core business logic, models, and interfaces.
- **BookManagementAPI.Infrastructure**: Contains the data access layer, including the database context and repository implementations.

## Features

- Manage books with operations to create, read, update, and delete book entries.
- Use of DTOs to transfer data between the client and server.
- Repository pattern for data access, promoting separation of concerns.

## Getting Started

### Prerequisites

#### For local development:
- .NET 8 SDK
- SQLite database

#### For Docker deployment:
- Docker
- Docker Compose (optional)

### Installation

#### Option 1: Run with Docker (Recommended)

1. Clone the repository:
   ```
   git clone <repository-url>
   cd BookManagementAPI
   ```

2. Build and run with Docker Compose:
   ```
   docker-compose up -d
   ```

   Or build and run with Docker directly:
   ```
   docker build -t bookmanagement-api .
   docker run -p 8080:8080 \
     -e ConnectionStrings__DefaultConnection="Data Source=/app/data/books.db" \
     -v $(pwd)/data:/app/data \
     bookmanagement-api
   ```

3. The API will be accessible at `http://localhost:8080`

#### Option 2: Run locally with .NET

1. Clone the repository:
   ```
   git clone <repository-url>
   cd BookManagementAPI
   ```

2. Restore the NuGet packages:
   ```
   dotnet restore
   ```

3. Update the database connection string in `BookManagementAPI.API/appsettings.json` if necessary.

4. Run the application:
   ```
   dotnet run --project BookManagementAPI.API/BookManagementAPI.API.csproj
   ```

### Usage

- When using Docker, the API can be accessed at `http://localhost:8080`.
- When running locally, the API can be accessed at `http://localhost:5000` (or the port specified in your configuration).
- Use tools like Postman or curl to interact with the API endpoints for managing books.

### Environment Variables

The following environment variables can be configured:

- `ASPNETCORE_ENVIRONMENT`: Set the application environment (Development, Production). Default: Production
- `ASPNETCORE_URLS`: Set the URLs the application listens on. Default: http://+:8080
- `ConnectionStrings__DefaultConnection`: Database connection string. Default: Data Source=books.db

### Endpoints

- `GET /api/books`: Retrieve all books.
- `GET /api/books/{id}`: Retrieve a book by its ID.
- `POST /api/books`: Create a new book.
- `PUT /api/books/{id}`: Update an existing book.
- `DELETE /api/books/{id}`: Delete a book by its ID.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.