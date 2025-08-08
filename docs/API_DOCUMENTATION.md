# API Documentation - Pharmacy Management System

## Overview

This document provides comprehensive API documentation for the Pharmacy Management System built with ASP.NET Core MVC. The system provides RESTful endpoints for managing pharmacy operations including user authentication, medicine management, shopping cart functionality, and payment processing.

## Base URL

```
https://localhost:5001
```

## Authentication

The system uses session-based authentication. Users must be logged in to access protected endpoints.

### Login
```
POST /UserAccount/Login
```

**Request Body:**
```json
{
  "Username": "string",
  "Password": "string"
}
```

**Response:**
- Success: Redirects to appropriate dashboard
- Failure: Returns login page with error message

## Controllers

### 1. Home Controller

#### Get Home Page
```
GET /Home/Index
```
**Description:** Returns the main landing page of the pharmacy website.

#### Get Privacy Policy
```
GET /Home/Privacy
```
**Description:** Returns the privacy policy page.

#### Get Error Page
```
GET /Home/Error
```
**Description:** Returns the error page for handling exceptions.

### 2. Medicine Controller

#### Get All Medicines
```
GET /Medicine/Index
```
**Description:** Returns a list of all available medicines.

#### Get Medicine Details
```
GET /Medicine/Details/{id}
```
**Parameters:**
- `id` (int): Medicine ID

**Description:** Returns detailed information about a specific medicine.

#### Create Medicine Form
```
GET /Medicine/Create
```
**Description:** Returns the form for creating a new medicine.

#### Create Medicine
```
POST /Medicine/Create
```
**Request Body:**
```json
{
  "Name": "string",
  "Description": "string",
  "Price": "decimal",
  "StockQuantity": "int",
  "Category": "string",
  "ImageUrl": "string"
}
```

#### Edit Medicine Form
```
GET /Medicine/Edit/{id}
```
**Parameters:**
- `id` (int): Medicine ID

**Description:** Returns the form for editing a medicine.

#### Update Medicine
```
POST /Medicine/Edit/{id}
```
**Parameters:**
- `id` (int): Medicine ID

**Request Body:** Same as Create Medicine

#### Delete Medicine Form
```
GET /Medicine/Delete/{id}
```
**Parameters:**
- `id` (int): Medicine ID

**Description:** Returns the confirmation page for deleting a medicine.

#### Delete Medicine
```
POST /Medicine/DeleteConfirmed/{id}
```
**Parameters:**
- `id` (int): Medicine ID

### 3. Cart Controller

#### Get Cart
```
GET /Cart/Index
```
**Description:** Returns the current user's shopping cart.

#### Get Cart Details
```
GET /Cart/Details/{id}
```
**Parameters:**
- `id` (int): Cart ID

**Description:** Returns detailed information about a specific cart.

#### Create Cart
```
POST /Cart/Create
```
**Request Body:**
```json
{
  "CustomerId": "int",
  "CreatedDate": "DateTime"
}
```

#### Edit Cart
```
POST /Cart/Edit/{id}
```
**Parameters:**
- `id` (int): Cart ID

#### Delete Cart
```
POST /Cart/DeleteConfirmed/{id}
```
**Parameters:**
- `id` (int): Cart ID

### 4. Cart Item Controller

#### Get Cart Items
```
GET /CartItem/Index/{cartId}
```
**Parameters:**
- `cartId` (int): Cart ID

**Description:** Returns all items in a specific cart.

#### Add Item to Cart
```
GET /CartItem/Create/{cartId}
```
**Parameters:**
- `cartId` (int): Cart ID

**Description:** Returns the form for adding an item to cart.

#### Create Cart Item
```
POST /CartItem/Create
```
**Request Body:**
```json
{
  "CartId": "int",
  "MedicineId": "int",
  "Quantity": "int",
  "Price": "decimal"
}
```

#### Edit Cart Item
```
POST /CartItem/Edit/{id}
```
**Parameters:**
- `id` (int): Cart Item ID

#### Remove from Cart
```
POST /CartItem/DeleteConfirmed/{id}
```
**Parameters:**
- `id` (int): Cart Item ID

### 5. Customer Controller

#### Get All Customers
```
GET /Customer/Index
```
**Description:** Returns a list of all customers.

#### Get Customer Details
```
GET /Customer/Details/{id}
```
**Parameters:**
- `id` (int): Customer ID

**Description:** Returns detailed information about a specific customer.

#### Create Customer Form
```
GET /Customer/Create
```
**Description:** Returns the form for creating a new customer.

#### Create Customer
```
POST /Customer/Create
```
**Request Body:**
```json
{
  "Name": "string",
  "Email": "string",
  "Phone": "string",
  "Address": "string"
}
```

#### Edit Customer
```
POST /Customer/Edit/{id}
```
**Parameters:**
- `id` (int): Customer ID

#### Delete Customer
```
POST /Customer/DeleteConfirmed/{id}
```
**Parameters:**
- `id` (int): Customer ID

### 6. Order Controller

#### Get All Orders
```
GET /Order/Index
```
**Description:** Returns a list of all orders.

#### Get Order Details
```
GET /Order/Details/{id}
```
**Parameters:**
- `id` (int): Order ID

**Description:** Returns detailed information about a specific order.

#### Create Order Form
```
GET /Order/Create
```
**Description:** Returns the form for creating a new order.

#### Create Order
```
POST /Order/Create
```
**Request Body:**
```json
{
  "CustomerId": "int",
  "OrderDate": "DateTime",
  "TotalAmount": "decimal",
  "Status": "string"
}
```

#### Edit Order
```
POST /Order/Edit/{id}
```
**Parameters:**
- `id` (int): Order ID

#### Delete Order
```
POST /Order/DeleteConfirmed/{id}
```
**Parameters:**
- `id` (int): Order ID

### 7. Payment Controller

#### Process Payment
```
POST /Payment/ProcessPayment
```
**Request Body:**
```json
{
  "OrderId": "int",
  "Amount": "decimal",
  "PaymentMethod": "string"
}
```

#### Payment Success
```
GET /Payment/Success
```
**Description:** Handles successful payment completion.

#### Payment Cancel
```
GET /Payment/Cancel
```
**Description:** Handles payment cancellation.

### 8. User Account Controller

#### Get User Dashboard
```
GET /UserAccount/Index
```
**Description:** Returns the user dashboard based on role.

#### Login Form
```
GET /UserAccount/Login
```
**Description:** Returns the login form.

#### Login
```
POST /UserAccount/Login
```
**Request Body:**
```json
{
  "Username": "string",
  "Password": "string"
}
```

#### Logout
```
POST /UserAccount/Logout
```
**Description:** Logs out the current user and clears session.

## Data Models

### Medicine
```json
{
  "Id": "int",
  "Name": "string",
  "Description": "string",
  "Price": "decimal",
  "StockQuantity": "int",
  "Category": "string",
  "ImageUrl": "string",
  "CreatedDate": "DateTime"
}
```

### Customer
```json
{
  "Id": "int",
  "Name": "string",
  "Email": "string",
  "Phone": "string",
  "Address": "string",
  "CreatedDate": "DateTime"
}
```

### Cart
```json
{
  "Id": "int",
  "CustomerId": "int",
  "CreatedDate": "DateTime",
  "TotalAmount": "decimal"
}
```

### CartItem
```json
{
  "Id": "int",
  "CartId": "int",
  "MedicineId": "int",
  "Quantity": "int",
  "Price": "decimal"
}
```

### Order
```json
{
  "Id": "int",
  "CustomerId": "int",
  "OrderDate": "DateTime",
  "TotalAmount": "decimal",
  "Status": "string"
}
```

### UserAccount
```json
{
  "Id": "int",
  "Username": "string",
  "Password": "string",
  "Role": "string",
  "Email": "string"
}
```

## Error Handling

The API returns appropriate HTTP status codes:

- `200 OK`: Request successful
- `201 Created`: Resource created successfully
- `400 Bad Request`: Invalid request data
- `401 Unauthorized`: Authentication required
- `403 Forbidden`: Access denied
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

## Security

- All sensitive endpoints require authentication
- Session-based authentication
- HTTPS enforcement
- Input validation and sanitization
- SQL injection prevention through Entity Framework
- CSRF protection

## Rate Limiting

Currently, no rate limiting is implemented. Consider implementing rate limiting for production use.

## Versioning

This API is currently at version 1.0. Future versions will be documented with version numbers in the URL path.

## Support

For API support and questions, please create an issue in the GitHub repository or contact the development team. 