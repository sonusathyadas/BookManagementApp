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

- .NET 8 SDK
- SQLite database

### Installation

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

- The API can be accessed at `http://localhost:5000` (or the port specified in your configuration).
- Use tools like Postman or curl to interact with the API endpoints for managing books.

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