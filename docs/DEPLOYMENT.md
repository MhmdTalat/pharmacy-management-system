# Deployment Guide - Pharmacy Management System

This guide provides step-by-step instructions for deploying the Pharmacy Management System to various environments.

## Prerequisites

Before deploying, ensure you have the following installed:

- .NET 9.0 SDK
- Oracle Database (or access to Oracle cloud)
- Stripe Account (for payment processing)
- Web Server (IIS, Apache, or cloud platform)

## Environment Setup

### 1. Development Environment

#### Local Development
```bash
# Clone the repository
git clone https://github.com/yourusername/pharmacy-management-system.git
cd pharmacy-management-system

# Install dependencies
dotnet restore

# Configure database connection
# Update appsettings.Development.json with your Oracle connection string

# Run database migrations
dotnet ef database update

# Run the application
dotnet run
```

#### Configuration Files
Create the following configuration files:

**appsettings.Development.json:**
```json
{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=your-oracle-connection-string;"
  },
  "Stripe": {
    "SecretKey": "your-stripe-secret-key",
    "PublishableKey": "your-stripe-publishable-key"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 2. Production Environment

#### Azure Deployment

1. **Create Azure App Service:**
   ```bash
   # Install Azure CLI
   az login
   az group create --name pharmacy-rg --location eastus
   az appservice plan create --name pharmacy-plan --resource-group pharmacy-rg --sku B1
   az webapp create --name pharmacy-app --resource-group pharmacy-rg --plan pharmacy-plan --runtime "DOTNET|9.0"
   ```

2. **Configure Application Settings:**
   ```bash
   az webapp config appsettings set --name pharmacy-app --resource-group pharmacy-rg --settings \
     "ConnectionStrings__OracleConnection"="your-production-oracle-connection" \
     "Stripe__SecretKey"="your-production-stripe-secret" \
     "Stripe__PublishableKey"="your-production-stripe-publishable"
   ```

3. **Deploy Application:**
   ```bash
   # Build the application
   dotnet publish -c Release -o ./publish

   # Deploy to Azure
   az webapp deployment source config-zip --resource-group pharmacy-rg --name pharmacy-app --src ./publish.zip
   ```

#### AWS Deployment

1. **Create Elastic Beanstalk Environment:**
   ```bash
   # Install AWS CLI
   aws configure

   # Create application
   aws elasticbeanstalk create-application --application-name pharmacy-app

   # Create environment
   aws elasticbeanstalk create-environment \
     --application-name pharmacy-app \
     --environment-name pharmacy-prod \
     --solution-stack-name "64bit Amazon Linux 2 v3.4.0 running .NET Core"
   ```

2. **Configure Environment Variables:**
   ```bash
   aws elasticbeanstalk update-environment \
     --environment-name pharmacy-prod \
     --option-settings \
     Namespace=aws:elasticbeanstalk:application:environment,OptionName=ConnectionStrings__OracleConnection,Value=your-production-oracle-connection \
     Namespace=aws:elasticbeanstalk:application:environment,OptionName=Stripe__SecretKey,Value=your-production-stripe-secret
   ```

#### Docker Deployment

1. **Create Dockerfile:**
   ```dockerfile
   FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
   WORKDIR /app
   EXPOSE 80
   EXPOSE 443

   FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
   WORKDIR /src
   COPY ["website.csproj", "./"]
   RUN dotnet restore "website.csproj"
   COPY . .
   WORKDIR "/src"
   RUN dotnet build "website.csproj" -c Release -o /app/build

   FROM build AS publish
   RUN dotnet publish "website.csproj" -c Release -o /app/publish

   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "website.dll"]
   ```

2. **Build and Run Docker Container:**
   ```bash
   # Build the image
   docker build -t pharmacy-app .

   # Run the container
   docker run -d -p 8080:80 -e ConnectionStrings__OracleConnection="your-connection-string" pharmacy-app
   ```

## Database Setup

### Oracle Database Configuration

1. **Install Oracle Database:**
   - Download Oracle Database from Oracle's official website
   - Install with default settings
   - Note down the connection details

2. **Create Database Schema:**
   ```sql
   -- Create tablespace
   CREATE TABLESPACE pharmacy_data
   DATAFILE 'pharmacy_data.dbf'
   SIZE 100M
   AUTOEXTEND ON;

   -- Create user
   CREATE USER pharmacy_user IDENTIFIED BY your_password
   DEFAULT TABLESPACE pharmacy_data
   QUOTA UNLIMITED ON pharmacy_data;

   -- Grant privileges
   GRANT CONNECT, RESOURCE, DBA TO pharmacy_user;
   ```

3. **Run Entity Framework Migrations:**
   ```bash
   dotnet ef database update
   ```

### Connection String Format

**Oracle Connection String:**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=your-host)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=your-service-name)));User Id=pharmacy_user;Password=your_password;
```

## Environment Variables

Set the following environment variables for production:

```bash
# Database
ConnectionStrings__OracleConnection=your-production-connection-string

# Stripe
Stripe__SecretKey=your-production-stripe-secret
Stripe__PublishableKey=your-production-stripe-publishable

# Application
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://localhost:5000
```

## SSL/HTTPS Configuration

### Development SSL Certificate
```bash
dotnet dev-certs https --trust
```

### Production SSL

#### Azure App Service
- SSL certificates are automatically managed by Azure
- Custom domains can be configured in the Azure portal

#### IIS
1. Install SSL certificate
2. Configure binding in IIS Manager
3. Update web.config for HTTPS redirect

#### Apache/Nginx
```nginx
server {
    listen 443 ssl;
    server_name your-domain.com;
    
    ssl_certificate /path/to/certificate.crt;
    ssl_certificate_key /path/to/private.key;
    
    location / {
        proxy_pass http://localhost:5000;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

## Monitoring and Logging

### Application Insights (Azure)
```bash
# Add Application Insights package
dotnet add package Microsoft.ApplicationInsights.AspNetCore

# Configure in appsettings.json
{
  "ApplicationInsights": {
    "InstrumentationKey": "your-instrumentation-key"
  }
}
```

### Logging Configuration
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "LogLevel": {
        "Default": "Information"
      }
    },
    "File": {
      "Path": "logs/pharmacy-app.log",
      "LogLevel": {
        "Default": "Information"
      }
    }
  }
}
```

## Performance Optimization

### Production Build
```bash
dotnet publish -c Release -o ./publish --self-contained true -r win-x64
```

### Database Optimization
- Enable connection pooling
- Configure appropriate timeout values
- Use parameterized queries (already implemented with Entity Framework)

### Caching
```csharp
// Add caching services
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
```

## Security Checklist

- [ ] HTTPS enabled
- [ ] Environment variables for sensitive data
- [ ] Database connection secured
- [ ] Stripe keys configured
- [ ] Input validation enabled
- [ ] SQL injection prevention
- [ ] CSRF protection
- [ ] Session management configured
- [ ] Error pages configured
- [ ] Logging enabled

## Troubleshooting

### Common Issues

1. **Database Connection Failed:**
   - Verify connection string
   - Check Oracle service is running
   - Ensure firewall allows connections

2. **Stripe Payment Issues:**
   - Verify Stripe keys are correct
   - Check webhook endpoints
   - Ensure HTTPS is enabled

3. **Application Won't Start:**
   - Check .NET version compatibility
   - Verify all dependencies are installed
   - Check application logs

### Log Locations

- **Windows:** `%APPDATA%\pharmacy-app\logs\`
- **Linux:** `/var/log/pharmacy-app/`
- **Azure:** Application Insights
- **AWS:** CloudWatch Logs

## Backup and Recovery

### Database Backup
```bash
# Oracle backup
expdp pharmacy_user/password@database DIRECTORY=backup_dir DUMPFILE=pharmacy_backup.dmp

# Restore
impdp pharmacy_user/password@database DIRECTORY=backup_dir DUMPFILE=pharmacy_backup.dmp
```

### Application Backup
- Backup configuration files
- Backup uploaded images
- Backup database regularly
- Document deployment procedures

## Support

For deployment issues:
1. Check application logs
2. Verify configuration settings
3. Test database connectivity
4. Review security settings
5. Contact development team

## Additional Resources

- [ASP.NET Core Deployment](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/)
- [Oracle Database Documentation](https://docs.oracle.com/en/database/)
- [Stripe Integration Guide](https://stripe.com/docs)
- [Azure App Service Documentation](https://docs.microsoft.com/en-us/azure/app-service/) 