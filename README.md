# 🏥 Pharmacy Management System

A comprehensive web-based pharmacy management system built with ASP.NET Core MVC, featuring user authentication, inventory management, shopping cart functionality, and secure payment processing with Stripe integration.

## ✨ Features

### 🔐 User Management
- **Multi-role Authentication**: Admin, Trader, and Customer roles
- **Secure Login/Logout**: Session-based authentication
- **User Dashboard**: Role-specific dashboards for different user types

### 💊 Medicine Management
- **Inventory Tracking**: Complete medicine catalog with details
- **Stock Management**: Real-time stock level monitoring
- **Medicine Categories**: Organized medicine classification
- **Image Upload**: Support for medicine images

### 🛒 Shopping Experience
- **Shopping Cart**: Add/remove items with quantity management
- **Order Processing**: Complete order lifecycle management
- **Order History**: Track past orders and their status

### 💳 Payment Integration
- **Stripe Payment Gateway**: Secure online payment processing
- **Payment Success/Failure Handling**: Comprehensive payment flow
- **Order Confirmation**: Email notifications and order tracking

### 👥 Customer Management
- **Customer Profiles**: Detailed customer information management
- **Order Tracking**: Customer-specific order history
- **Prescription Management**: Digital prescription handling

### 📊 Sales & Analytics
- **Sales Tracking**: Complete sales record management
- **Purchase Management**: Supplier and purchase order tracking
- **Financial Reports**: Sales and inventory analytics

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 9.0 MVC
- **Database**: Oracle Database with Entity Framework Core
- **Frontend**: HTML5, CSS3, Bootstrap 5, JavaScript
- **Payment**: Stripe API Integration
- **Authentication**: Session-based authentication
- **UI Framework**: Bootstrap with custom styling

## 📋 Prerequisites

- .NET 9.0 SDK
- Oracle Database
- Stripe Account (for payment processing)

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/pharmacy-management-system.git
cd pharmacy-management-system
```

### 2. Database Setup
1. Install Oracle Database
2. Create a new database instance
3. Update connection string in `appsettings.json`

### 3. Configuration
1. Update `appsettings.json` with your database connection string:
```json
{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=your-oracle-connection-string;"
  }
}
```

2. Configure Stripe settings:
```json
{
  "Stripe": {
    "SecretKey": "your-stripe-secret-key",
    "PublishableKey": "your-stripe-publishable-key"
  }
}
```

### 4. Database Migration
```bash
dotnet ef database update
```

### 5. Run the Application
```bash
dotnet run
```

The application will be available at `https://localhost:5001`

## 📁 Project Structure

```
website/
├── Controllers/          # MVC Controllers
│   ├── CartController.cs
│   ├── CustomerController.cs
│   ├── MedicineController.cs
│   ├── OrderController.cs
│   ├── PaymentController.cs
│   └── UserAccountController.cs
├── Models/              # Entity Models
│   ├── ApplicationDbContext.cs
│   ├── Cart.cs
│   ├── Customer.cs
│   ├── Medicine.cs
│   └── UserAccount.cs
├── Views/               # Razor Views
│   ├── Cart/
│   ├── Customer/
│   ├── Medicine/
│   ├── Order/
│   └── UserAccount/
├── wwwroot/            # Static Files
│   ├── css/
│   ├── js/
│   └── images/
└── ViewModel/          # View Models
```

## 🔧 Key Components

### Controllers
- **HomeController**: Landing page and error handling
- **MedicineController**: Medicine CRUD operations
- **CartController**: Shopping cart management
- **OrderController**: Order processing and management
- **PaymentController**: Stripe payment integration
- **CustomerController**: Customer profile management
- **UserAccountController**: Authentication and user management

### Models
- **ApplicationDbContext**: Database context with Oracle configuration
- **Medicine**: Medicine entity with inventory tracking
- **Cart/CartItem**: Shopping cart functionality
- **Customer**: Customer profile management
- **Order/OrderDetail**: Order processing
- **UserAccount**: User authentication and roles

## 🎨 UI/UX Features

- **Responsive Design**: Mobile-friendly interface
- **Modern UI**: Clean, professional pharmacy interface
- **Bootstrap 5**: Latest Bootstrap framework
- **Custom Styling**: Pharmacy-themed design
- **User Experience**: Intuitive navigation and workflows

## 🔒 Security Features

- **Session Management**: Secure session handling
- **Input Validation**: Server-side validation
- **SQL Injection Prevention**: Entity Framework protection
- **HTTPS Enforcement**: Secure communication
- **Role-based Access**: Authorization controls

## 💳 Payment Integration

The system integrates with Stripe for secure payment processing:
- **Credit Card Processing**: Secure payment gateway
- **Payment Success/Failure Handling**: Comprehensive error handling
- **Order Confirmation**: Automatic order status updates
- **Receipt Generation**: Digital receipt creation

## 📱 Features Overview

| Feature | Description |
|---------|-------------|
| **User Authentication** | Multi-role login system |
| **Medicine Management** | Complete inventory control |
| **Shopping Cart** | Add/remove items functionality |
| **Order Processing** | Complete order lifecycle |
| **Payment Processing** | Stripe integration |
| **Customer Management** | Customer profile system |
| **Sales Tracking** | Comprehensive sales records |
| **Admin Dashboard** | Administrative controls |

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Your Name**
- LinkedIn: [https://www.linkedin.com/in/muhammed-tallat-a440881b7/]
- GitHub: [https://github.com/MhmdTalat]
- Email: muhammedtallat4@gmail.com

## 🙏 Acknowledgments

- Bootstrap for the UI framework
- Stripe for payment processing
- Oracle for database management
- Microsoft for ASP.NET Core framework

---

⭐ **Star this repository if you find it helpful!** 
