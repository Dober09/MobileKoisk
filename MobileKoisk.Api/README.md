# MobileKiosk API

A .NET API backend for the Mobile Kiosk application, providing authentication and product management capabilities.

## Configuration Overview

### API Endpoints
The API runs on the following endpoints in development:
- HTTP: http://0.0.0.0:5171
- HTTPS: https://0.0.0.0:7171

### Authentication
The API uses JWT (JSON Web Token) authentication with the following configuration:
- Bearer token authentication scheme
- Token validation for issuer, audience, lifetime, and signing key
- Tokens are generated via the JwtService

### Database
- Uses SQLite with Entity Framework Core
- Connection string is configured in appsettings.json
- Manages user data and authentication information

### CORS Configuration
In development mode, CORS is configured to allow:
- Any origin
- Any method
- Any header

## API Endpoints

### Authentication Controller (`/api/Auth`)

#### Register
- **Endpoint**: POST `/api/Auth/register`
- **Purpose**: Creates a new user account
- **Request Body**:
  ```json
  {
    "name": "string",
    "surname": "string",
    "email": "string",
    "password": "string",
    "dateOfBirth": "date",
    "phoneNumber": "string"
  }
  ```

#### Login
- **Endpoint**: POST `/api/Auth/login`
- **Purpose**: Authenticates a user
- **Request Body**:
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```

## Security Features

1. Password Hashing
   - Uses BCrypt for secure password hashing
   - Passwords are never stored in plain text

2. JWT Authentication
   - Tokens include validation for:
     - Issuer
     - Audience
     - Lifetime
     - Signing key

3. User Account Protection
   - Email uniqueness validation
   - Account activation status checking
   - Last login tracking

## Development Setup

1. Install Dependencies:
   ```bash
   dotnet restore
   ```

2. Configure Environment:
   - Set up your JWT configuration in appsettings.json:
     ```json
     {
       "Jwt": {
         "Key": "your-secret-key",
         "Issuer": "your-issuer",
         "Audience": "your-audience"
       }
     }
     ```

3. Run Migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

## Mobile Client Integration

The mobile client (MAUI application) connects to this API using:
- Base URL: http://10.0.2.2:5171/api/Auth/ (Android emulator)
- Timeout: 15 seconds
- Content Type: application/json

## Development Tools

- Swagger UI available in development at `/swagger`
- Debug logging enabled in development mode
- CORS enabled for development testing

## Dependencies

- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore
- BCrypt.Net-Next
- Microsoft.EntityFrameworkCore.Sqlite

## Security Notes

1. HTTPS redirection is configurable (currently disabled in development)
2. Debug certificate validation is bypassed in development mode
3. Proper middleware order is maintained for security:
   - UseRouting
   - UseCors
   - UseAuthentication
   - UseAuthorization
