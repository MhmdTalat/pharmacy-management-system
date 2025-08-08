# 🚀 Quick Start Guide - Pharmacy Management System

Get your pharmacy management system up and running in minutes!

## 📋 Prerequisites

- .NET 9.0 SDK
- Oracle Database (or Oracle Cloud)
- Stripe Account (for payments)
- Git

## ⚡ Quick Setup

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/pharmacy-management-system.git
cd pharmacy-management-system
```

### 2. Configure Database
Update `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=your-oracle-connection-string;"
  },
  "Stripe": {
    "SecretKey": "your-stripe-secret-key",
    "PublishableKey": "your-stripe-publishable-key"
  }
}
```

### 3. Run Database Migrations
```bash
dotnet ef database update
```

### 4. Start the Application
```bash
dotnet run
```

### 5. Access the Application
Open your browser and navigate to: `https://localhost:5001`

## 🐳 Docker Setup (Alternative)

### Using Docker Compose
```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down
```

### Using Docker
```bash
# Build the image
docker build -t pharmacy-app .

# Run the container
docker run -d -p 8080:80 pharmacy-app
```

## 🔧 Configuration

### Environment Variables
```bash
# Database
ConnectionStrings__OracleConnection=your-connection-string

# Stripe
Stripe__SecretKey=your-stripe-secret
Stripe__PublishableKey=your-stripe-publishable

# Application
ASPNETCORE_ENVIRONMENT=Development
```

### Database Setup
1. Install Oracle Database
2. Create database user
3. Run migrations
4. Seed initial data

## 📱 Default Users

### Admin Account
- **Username**: admin
- **Password**: admin123
- **Role**: Administrator

### Trader Account
- **Username**: trader
- **Password**: trader123
- **Role**: Trader

### Customer Account
- **Username**: customer
- **Password**: customer123
- **Role**: Customer

## 🎯 Key Features to Test

### 1. User Authentication
- Login with different roles
- Access role-specific dashboards
- Test session timeout

### 2. Medicine Management
- Add new medicines
- Update inventory
- Upload medicine images
- View medicine catalog

### 3. Shopping Cart
- Add items to cart
- Update quantities
- Remove items
- View cart total

### 4. Order Processing
- Create new orders
- Process payments
- Track order status
- View order history

### 5. Payment Integration
- Test Stripe payment flow
- Verify payment success/failure
- Check order confirmation

## 🔍 Troubleshooting

### Common Issues

**Database Connection Failed**
```bash
# Check Oracle service
sqlplus / as sysdba

# Verify connection string
# Test connection from application
```

**Application Won't Start**
```bash
# Check .NET version
dotnet --version

# Restore packages
dotnet restore

# Clear cache
dotnet clean
```

**Payment Issues**
- Verify Stripe keys
- Check HTTPS configuration
- Test webhook endpoints

### Log Locations
- **Application Logs**: `logs/pharmacy-app.log`
- **Docker Logs**: `docker-compose logs`
- **Database Logs**: Oracle alert log

## 📚 Next Steps

1. **Customize Configuration**: Update settings for your environment
2. **Add Sample Data**: Seed database with test data
3. **Configure SSL**: Set up HTTPS certificates
4. **Set Up Monitoring**: Configure logging and monitoring
5. **Deploy to Production**: Follow deployment guide

## 🆘 Support

- **Documentation**: [Full Documentation](docs/)
- **Issues**: [GitHub Issues](https://github.com/yourusername/pharmacy-management-system/issues)
- **Wiki**: [Project Wiki](https://github.com/yourusername/pharmacy-management-system/wiki)

## 🎉 Success!

Your pharmacy management system is now running! 

- **Local URL**: https://localhost:5001
- **Admin Dashboard**: https://localhost:5001/UserAccount/AdminDashboard
- **API Documentation**: https://localhost:5001/docs

---

**Need help? Check our [full documentation](docs/) or [create an issue](https://github.com/yourusername/pharmacy-management-system/issues) on GitHub.** 