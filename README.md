# Book Management API

This project is a .NET 8 Web API for managing book operations. It utilizes Entity Framework with SQLite for database operations and implements the Repository pattern along with Data Transfer Objects (DTOs) for data handling. The API is secured with JWT (JSON Web Token) authentication.

## Project Structure

The project is organized into three main projects:

- **BookManagementAPI.API**: Contains the API controllers, DTOs, and configuration files.
- **BookManagementAPI.Core**: Contains the core business logic, models, and interfaces.
- **BookManagementAPI.Infrastructure**: Contains the data access layer, including the database context and repository implementations.

## Features

- JWT authentication for secure API access
- User registration and login
- Manage books with operations to create, read, update, and delete book entries.
- Use of DTOs to transfer data between the client and server.
- Repository pattern for data access, promoting separation of concerns.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQLite database
- Docker (optional, for containerized deployment)

### Installation

#### Option 1: Running with Docker (Recommended)

1. Clone the repository:
   ```
   git clone <repository-url>
   cd BookManagementAPI
   ```

2. Build and run using Docker Compose:
   ```
   docker-compose up -d
   ```

3. The API will be accessible at `http://localhost:5000`

#### Option 2: Running Locally

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

4. Apply database migrations:
   ```
   dotnet ef database update --project BookManagementAPI.Infrastructure/BookManagementAPI.Infrastructure.csproj --startup-project BookManagementAPI.API/BookManagementAPI.API.csproj
   ```

5. Run the application:
   ```
   dotnet run --project BookManagementAPI.API/BookManagementAPI.API.csproj
   ```

### Docker Configuration

The application is fully containerized with Docker support. The following environment variables can be configured:

- `ASPNETCORE_ENVIRONMENT`: Set the environment (Development, Staging, Production)
- `ASPNETCORE_URLS`: Configure the URLs the application listens on (default: http://+:8080)
- `ConnectionStrings__DefaultConnection`: Database connection string

To customize environment variables:

1. Copy `.env.example` to `.env`:
   ```
   cp .env.example .env
   ```

2. Edit `.env` with your configuration

3. Run with docker-compose:
   ```
   docker-compose up -d
   ```

Alternatively, build and run the Docker image manually:

```bash
# Build the image
docker build -t bookmanagement-api .

# Run the container
docker run -d -p 5000:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__DefaultConnection="Data Source=/app/data/books.db" \
  -v book-data:/app/data \
  --name bookmanagement-api \
  bookmanagement-api
```

### Usage

- The API can be accessed at `http://localhost:5000` (or the port specified in your configuration).
- Use tools like Postman or curl to interact with the API endpoints for managing books.
- Swagger UI is available at `http://localhost:5000/swagger` in development mode.

### Authentication

The API uses JWT (JSON Web Token) authentication. To access protected endpoints:

1. Register a new user:
   ```bash
   curl -X POST http://localhost:5000/api/auth/register \
     -H "Content-Type: application/json" \
     -d '{
       "username": "youruser",
       "password": "yourpassword",
       "firstname": "John",
       "lastname": "Doe",
       "email": "john@example.com"
     }'
   ```

2. Login to get a JWT token:
   ```bash
   curl -X POST http://localhost:5000/api/auth/login \
     -H "Content-Type: application/json" \
     -d '{
       "username": "youruser",
       "password": "yourpassword"
     }'
   ```

3. Use the returned token to access protected endpoints:
   ```bash
   curl -X GET http://localhost:5000/api/books \
     -H "Authorization: Bearer YOUR_JWT_TOKEN"
   ```

### Endpoints

#### Authentication Endpoints
- `POST /api/auth/register`: Register a new user
- `POST /api/auth/login`: Login and receive a JWT token

#### Book Management Endpoints (Require Authentication)
- `GET /api/books`: Retrieve all books.
- `GET /api/books/{id}`: Retrieve a book by its ID.
- `GET /api/books/category/{category}`: Retrieve books by category.
- `POST /api/books`: Create a new book.
- `PUT /api/books/{id}`: Update an existing book.
- `DELETE /api/books/{id}`: Delete a book by its ID.

## Security Considerations

⚠️ **Important**: The current implementation uses plain text password storage for simplicity. In a production environment, you should:

1. Implement password hashing using BCrypt, Argon2, or PBKDF2
2. Store the JWT secret in environment variables or a secure key vault
3. Add password complexity requirements
4. Implement rate limiting for authentication endpoints
5. Add account lockout after failed login attempts

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.