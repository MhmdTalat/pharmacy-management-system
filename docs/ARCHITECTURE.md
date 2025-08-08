# Architecture Documentation - Pharmacy Management System

## Overview

The Pharmacy Management System is built using ASP.NET Core MVC architecture with a layered approach to ensure maintainability, scalability, and separation of concerns. The system follows the Model-View-Controller (MVC) pattern and implements modern web development practices.

## System Architecture

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                       │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐        │
│  │   Views     │  │ Controllers │  │ ViewModels  │        │
│  └─────────────┘  └─────────────┘  └─────────────┘        │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                    Business Logic Layer                     │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐        │
│  │   Models    │  │  Services   │  │  Helpers    │        │
│  └─────────────┘  └─────────────┘  └─────────────┘        │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                    Data Access Layer                        │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐        │
│  │DbContext    │  │ Repositories│  │ Migrations  │        │
│  └─────────────┘  └─────────────┘  └─────────────┘        │
└─────────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────────┐
│                    Database Layer                           │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐        │
│  │   Oracle    │  │   Tables    │  │   Indexes   │        │
│  │  Database   │  │             │  │             │        │
│  └─────────────┘  └─────────────┘  └─────────────┘        │
└─────────────────────────────────────────────────────────────┘
```

## Technology Stack

### Backend Technologies
- **Framework**: ASP.NET Core 9.0 MVC
- **Language**: C# 12.0
- **Database**: Oracle Database with Entity Framework Core
- **ORM**: Entity Framework Core 9.0
- **Authentication**: Session-based authentication
- **Payment**: Stripe API integration

### Frontend Technologies
- **UI Framework**: Bootstrap 5
- **CSS Preprocessor**: SCSS
- **JavaScript**: Vanilla JavaScript with jQuery
- **Icons**: Icomoon icon font
- **Animations**: AOS (Animate On Scroll)

### Development Tools
- **IDE**: Visual Studio 2022 / Visual Studio Code
- **Version Control**: Git
- **Package Manager**: NuGet
- **Build Tool**: MSBuild

## Layer Architecture

### 1. Presentation Layer

#### Controllers
Controllers handle HTTP requests and responses, implementing the MVC pattern:

```csharp
public class MedicineController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public MedicineController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Medicines.ToListAsync());
    }
}
```

**Key Controllers:**
- `HomeController`: Landing page and error handling
- `MedicineController`: Medicine CRUD operations
- `CartController`: Shopping cart management
- `OrderController`: Order processing
- `PaymentController`: Stripe payment integration
- `CustomerController`: Customer management
- `UserAccountController`: Authentication

#### Views
Views are Razor pages that render the UI:

```html
@model IEnumerable<Medicine>
@{
    ViewData["Title"] = "Medicines";
}

<div class="container">
    <h2>Available Medicines</h2>
    @foreach (var medicine in Model)
    {
        <div class="medicine-card">
            <h3>@medicine.Name</h3>
            <p>@medicine.Description</p>
            <span class="price">$@medicine.Price</span>
        </div>
    }
</div>
```

#### ViewModels
ViewModels provide strongly-typed data to views:

```csharp
public class AddToCartViewModel
{
    public int MedicineId { get; set; }
    public string MedicineName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
```

### 2. Business Logic Layer

#### Models
Models represent the domain entities:

```csharp
public class Medicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

**Key Models:**
- `Medicine`: Medicine inventory
- `Customer`: Customer information
- `Cart/CartItem`: Shopping cart functionality
- `Order/OrderDetail`: Order processing
- `UserAccount`: User authentication
- `Sale/SaleDetail`: Sales tracking
- `Purchase/PurchaseDetail`: Purchase management

#### Services
Services contain business logic and external integrations:

```csharp
public interface IPaymentService
{
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
}

public class StripePaymentService : IPaymentService
{
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // Stripe payment processing logic
    }
}
```

### 3. Data Access Layer

#### DbContext
Entity Framework Core DbContext manages database operations:

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity configurations
        modelBuilder.Entity<Medicine>()
            .Property(m => m.Price)
            .HasColumnType("decimal(18,2)");
    }
}
```

#### Repositories
Repository pattern for data access abstraction:

```csharp
public interface IMedicineRepository
{
    Task<IEnumerable<Medicine>> GetAllAsync();
    Task<Medicine> GetByIdAsync(int id);
    Task<Medicine> AddAsync(Medicine medicine);
    Task UpdateAsync(Medicine medicine);
    Task DeleteAsync(int id);
}
```

## Database Design

### Entity Relationship Diagram

```
┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│   Customer  │    │     Cart    │    │   Medicine  │
├─────────────┤    ├─────────────┤    ├─────────────┤
│ Id (PK)     │    │ Id (PK)     │    │ Id (PK)     │
│ Name        │    │ CustomerId  │    │ Name        │
│ Email       │◄───┤ CreatedDate │    │ Description │
│ Phone       │    │ TotalAmount │    │ Price       │
│ Address     │    └─────────────┘    │ StockQty    │
│ CreatedDate │           │           │ Category     │
└─────────────┘           │           │ ImageUrl     │
                          │           │ CreatedDate  │
                          │           └─────────────┘
                          │                   │
                          │                   │
                    ┌─────────────┐    ┌─────────────┐
                    │  CartItem   │    │    Order    │
                    ├─────────────┤    ├─────────────┤
                    │ Id (PK)     │    │ Id (PK)     │
                    │ CartId (FK) │    │ CustomerId  │
                    │ MedicineId  │    │ OrderDate   │
                    │ Quantity    │    │ TotalAmount │
                    │ Price       │    │ Status      │
                    └─────────────┘    └─────────────┘
                              │               │
                              │               │
                              │         ┌─────────────┐
                              │         │OrderDetail  │
                              │         ├─────────────┤
                              │         │ Id (PK)     │
                              │         │ OrderId (FK)│
                              │         │ MedicineId  │
                              │         │ Quantity    │
                              │         │ Price       │
                              │         └─────────────┘
                              │
                    ┌─────────────┐
                    │UserAccount  │
                    ├─────────────┤
                    │ Id (PK)     │
                    │ Username    │
                    │ Password    │
                    │ Role        │
                    │ Email       │
                    └─────────────┘
```

### Database Schema

#### Core Tables

**Medicine Table:**
```sql
CREATE TABLE Medicines (
    Id NUMBER PRIMARY KEY,
    Name NVARCHAR2(100) NOT NULL,
    Description NVARCHAR2(500),
    Price NUMBER(18,2) NOT NULL,
    StockQuantity NUMBER DEFAULT 0,
    Category NVARCHAR2(50),
    ImageUrl NVARCHAR2(255),
    CreatedDate DATE DEFAULT SYSDATE
);
```

**Customer Table:**
```sql
CREATE TABLE Customers (
    Id NUMBER PRIMARY KEY,
    Name NVARCHAR2(100) NOT NULL,
    Email NVARCHAR2(100) UNIQUE,
    Phone NVARCHAR2(20),
    Address NVARCHAR2(255),
    CreatedDate DATE DEFAULT SYSDATE
);
```

**Cart Table:**
```sql
CREATE TABLE Carts (
    Id NUMBER PRIMARY KEY,
    CustomerId NUMBER REFERENCES Customers(Id),
    CreatedDate DATE DEFAULT SYSDATE,
    TotalAmount NUMBER(18,2) DEFAULT 0
);
```

## Security Architecture

### Authentication & Authorization

```csharp
// Session-based authentication
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
```

### Security Measures

1. **HTTPS Enforcement**
   ```csharp
   app.UseHttpsRedirection();
   ```

2. **Input Validation**
   ```csharp
   [Required]
   [StringLength(100)]
   public string Name { get; set; }
   ```

3. **SQL Injection Prevention**
   - Entity Framework Core parameterized queries
   - Input sanitization

4. **CSRF Protection**
   - Anti-forgery tokens in forms

## Integration Architecture

### External Services

#### Stripe Payment Integration
```csharp
public class PaymentController : Controller
{
    [HttpPost]
    public async Task<IActionResult> ProcessPayment(PaymentRequest request)
    {
        var options = new ChargeCreateOptions
        {
            Amount = (long)(request.Amount * 100),
            Currency = "usd",
            Source = request.Token,
            Description = "Pharmacy Order Payment"
        };
        
        var service = new ChargeService();
        var charge = await service.CreateAsync(options);
        
        return RedirectToAction("Success");
    }
}
```

### Third-Party Libraries

- **Stripe.net**: Payment processing
- **Oracle.EntityFrameworkCore**: Oracle database provider
- **Bootstrap**: UI framework
- **jQuery**: JavaScript library

## Performance Considerations

### Caching Strategy
```csharp
// Memory caching for frequently accessed data
builder.Services.AddMemoryCache();

// Response caching for static content
builder.Services.AddResponseCaching();
```

### Database Optimization
- Connection pooling
- Indexed queries
- Parameterized queries
- Lazy loading for related entities

### Frontend Optimization
- Minified CSS and JavaScript
- Image optimization
- CDN for static assets
- Lazy loading for images

## Scalability Architecture

### Horizontal Scaling
- Stateless application design
- Session storage in distributed cache
- Load balancer ready

### Vertical Scaling
- Database connection pooling
- Memory optimization
- CPU utilization monitoring

## Monitoring & Logging

### Application Insights
```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

### Logging Configuration
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Deployment Architecture

### Environment Configuration
- Development: Local SQL Server/Oracle
- Staging: Azure App Service
- Production: Azure App Service with Oracle Cloud

### CI/CD Pipeline
1. Code commit to Git
2. Automated testing
3. Build and package
4. Deploy to staging
5. Manual approval
6. Deploy to production

## Future Enhancements

### Planned Features
- Microservices architecture
- API-first approach
- Real-time notifications
- Advanced analytics
- Mobile application
- AI-powered recommendations

### Technical Improvements
- Containerization with Docker
- Kubernetes orchestration
- GraphQL API
- Event-driven architecture
- CQRS pattern implementation

## Conclusion

The Pharmacy Management System follows modern web development practices with a clean, maintainable architecture. The layered approach ensures separation of concerns while the MVC pattern provides a structured way to handle user interactions. The system is designed to be scalable, secure, and maintainable for future enhancements. 