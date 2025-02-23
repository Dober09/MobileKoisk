# Mobile Kiosk API

A comprehensive ASP.NET Core API for a mobile kiosk system, featuring user authentication, product management, shopping basket functionality, and transaction processing.

## Features

- 🔐 JWT Authentication
- 🛍️ Product Management
- 🛒 Shopping Basket
- Wishlist
- 📝 Receipt Generation
- 📱 Push Notifications
- 💳 Transaction Processing

## Technologies

- ASP.NET Core 8.0
- Entity Framework Core
- SQLite Database
- JWT Bearer Authentication
- Swagger/OpenAPI Documentation

## Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- SQLite

## Getting Started

1. Clone the repository:
```bash
git clone https://github.com/yourusername/mobile-kiosk-api.git
cd mobile-kiosk-api
```

2. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=mobilekiosk.db"
  },
  "Jwt": {
    "Key": "your-secret-key-here",
    "Issuer": "your-issuer",
    "Audience": "your-audience"
  }
}
```

3. Run migrations:
```bash
dotnet ef database update
```

4. Run the application:
```bash
dotnet run
```

The API will be available at `http://localhost:5171`

## API Endpoints

### Authentication
- POST `/api/Auth/register` - Register new user
- POST `/api/Auth/login` - Login user

### Products
- GET `/api/Product` - Get all products
- GET `/api/Product/{barcode}` - Get product by barcode
- GET `/api/Product/category/{category}` - Get products by category
- POST `/api/Product` - Create new product (Admin only)

### Basket
- GET `/api/Basket/{userId}` - Get user's basket
- POST `/api/Basket` - Create new basket
- POST `/api/Basket/{basketId}/items` - Add item to basket

### Receipts
- GET `/api/Receipt/user/{userId}` - Get user's receipts
- GET `/api/Receipt/{id}` - Get specific receipt
- POST `/api/Receipt` - Create receipt

### Notifications
- GET `/api/Notification/user/{userId}` - Get user's notifications
- POST `/api/Notification` - Create notification
- PUT `/api/Notification/{id}/read` - Mark notification as read

### Transactions
- POST `/api/Transaction/checkout` - Process checkout

## Authentication

The API uses JWT Bearer authentication. Include the token in the Authorization header:
```
Authorization: Bearer your-token-here
```

## Models

### User
```csharp
public class UserData
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
}
```

### Product
```csharp
public class Product
{
    public string from_date { get; set; }
    public int section { get; set; }
    public object barcode { get; set; }
    public string item_description { get; set; }
    public double selling_price { get; set; }
    public double quantity { get; set; }
    public string category { get; set; }
}
```

### BasketItem
```csharp
public class BasketItem
{
    public string Barcode { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
```

## Development Setup

### Visual Studio
1. Open the solution file
2. Restore NuGet packages
3. Update database using Package Manager Console:
```
Update-Database
```

### VS Code
1. Install C# extension
2. Restore dependencies:
```bash
dotnet restore
```
3. Update database:
```bash
dotnet ef database update
```

## Testing

Use Swagger UI for testing endpoints:
- Development: `http://localhost:5171/swagger`

Example curl commands:

1. Register User:
```bash
curl -X POST http://localhost:5171/api/Auth/register \
-H "Content-Type: application/json" \
-d '{
  "name": "John",
  "surname": "Doe",
  "email": "john@example.com",
  "password": "Password123!",
  "dateOfBirth": "1990-01-01",
  "phoneNumber": "1234567890"
}'
```

2. Login:
```bash
curl -X POST http://localhost:5171/api/Auth/login \
-H "Content-Type: application/json" \
-d '{
  "email": "john@example.com",
  "password": "Password123!"
}'
```

## Error Handling

The API returns standard HTTP status codes:
- 200: Success
- 400: Bad Request
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 500: Internal Server Error

## Security

- Passwords are hashed using BCrypt
- JWT tokens are required for protected endpoints
- CORS is configured for development
- HTTPS is enforced in production

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support, email support@mobilekiosk.com or open an issue in the repository.

## Acknowledgments

- ASP.NET Core team
- Entity Framework Core team
- SQLite team
