# ShopMate

A modern retail point-of-sale (POS) and inventory management system built with WinUI 3 and .NET, featuring a clean architecture and cloud-based data storage.

## 📋 Overview

ShopMate is a comprehensive shop management solution designed for retail businesses. It provides separate interfaces for administrators and sales personnel, enabling efficient management of products, customers, employees, and sales transactions.

## ✨ Features

### Admin Features
- **Dashboard** - Overview of business operations with quick access to management tools
- **Employee Management** - Add, edit, delete, and view employee records with user account creation
- **Customer Management** - Complete customer database with CRUD operations
- **Product Management** - Inventory control with stock tracking and expiry date management
- **User Management** - Role-based access control (Admin/Salesperson)

### Salesperson Features
- **Bill Generation** - Create itemized bills with customer and product selection
- **KPI Dashboard** - Real-time metrics including:
  - Total sales today
  - Number of bills generated
  - Average bill value
  - Low stock alerts
- **Quick Add** - Fast customer and product registration
- **Inventory View** - Browse available products with stock levels

### General Features
- **Secure Authentication** - SHA256 password hashing
- **Cloud Database** - Supabase PostgreSQL integration
- **Responsive UI** - Modern WinUI 3 interface with smooth animations
- **Report Generation** - Export customer, product, and sales reports
- **Real-time Updates** - Live data synchronization across the application

## 🏗️ Architecture

The application follows a three-tier architecture:

```
┌─────────────────┐
│   GUI Layer     │  - WinUI 3 Pages & Controls
│   (Presentation)│  - User interaction & validation
└────────┬────────┘
         │
┌────────▼────────┐
│  Business Logic │  - Data processing
│     (BL)        │  - Business rules
└────────┬────────┘
         │
┌────────▼────────┐
│   Data Layer    │  - Supabase API integration
│     (DL)        │  - Database operations
└─────────────────┘
```

### Project Structure

```
ShopMate/
├── BL/                          # Business Logic Layer
│   ├── BillManagementBL.cs
│   ├── CustomerServiceBL.cs
│   ├── EmployManagementBL.cs
│   ├── LoginBL.cs
│   ├── ProductManagementBL.cs
│   └── UserManagementBL.cs
│
├── DL/                          # Data Layer
│   ├── BillManagementDL.cs
│   ├── CustomerServiceDL.cs
│   ├── EmployManagementDL.cs
│   ├── LoginDL.cs
│   ├── ProductManagementDL.cs
│   ├── UserManagementDL.cs
│   └── SupabaseInitializer.cs
│
├── DTOs/                        # Data Transfer Objects
│   ├── BillDTO.cs
│   ├── BillItemDTO.cs
│   ├── BillItemVM.cs
│   ├── CustomerDTO.cs
│   ├── EmployeeDTO.cs
│   ├── LoginDTO.cs
│   ├── ProductDTO.cs
│   └── UserDTO.cs
│
├── GUI/                         # User Interface
│   ├── AdminGUI/               # Admin-specific pages
│   ├── SalesPersonGUI/         # Salesperson pages
│   ├── Controls/               # Reusable controls
│   │   ├── AdminSidebar.xaml
│   │   └── SalesSidebar.xaml
│   ├── Utils/                  # UI utilities
│   └── LoginPage.xaml
│
├── App.xaml                     # Application resources
└── GlobalSession.cs             # Session management
```

## 🗄️ Database Schema

### Tables

**users**
- userID (PK)
- Username
- passwordHash
- roleID (7=Admin, 8=Salesperson)
- employeeID (FK)

**salesPersons**
- salesPersonID (PK)
- name
- phoneNumber
- address

**customers**
- customerID (PK)
- customerName
- phoneNumber
- customerGender
- customerAddress
- customerAge

**products**
- id (PK)
- name
- description
- price
- stock
- expirydate
- lowstocklimit

**bills**
- id (PK)
- customerid (FK)
- userid (FK)
- createdat
- subtotal
- tax
- discount
- total
- paymenttype
- note

**billitems**
- id (PK)
- billid (FK)
- productid (FK)
- productname
- unitprice
- quantity
- linetotal

## 🚀 Getting Started

### Prerequisites

- Windows 10 version 1809 (build 17763) or later
- Visual Studio 2022 with:
  - .NET Desktop Development workload
  - Windows App SDK
  - WinUI 3 templates
- .NET 6.0 SDK or later

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/shopmate.git
   cd shopmate
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Configure Supabase** (if needed)
   
   Update the connection details in `DL/SupabaseInitializer.cs`:
   ```csharp
   string url = "your-supabase-url";
   string key = "your-supabase-anon-key";
   ```

4. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

### Default Login Credentials

The application requires existing user accounts in the database. Create users with the following role IDs:
- **Admin**: roleID = 7
- **Salesperson**: roleID = 8

## 🔧 Configuration

### Database Connection

The application uses Supabase for cloud database storage. Configuration is in `DL/SupabaseInitializer.cs`.

### Session Management

User sessions are managed through the `GlobalSession` static class, storing:
- Current user information
- Current employee details
- Display username

## 📊 Key Features Explained

### Bill Generation
1. Select customer from dropdown
2. Add products with quantities
3. System automatically:
   - Calculates line totals
   - Updates subtotals
   - Deducts stock quantities
   - Creates bill and bill items records

### KPI Metrics
- **Total Sales Today**: Sum of all bills created today
- **Total Bills**: Count of bills generated today
- **Average Bill Value**: Mean transaction value
- **Low Stock Products**: Products at or below threshold

### Report Generation
Export data in text format:
- Customer lists
- Product inventory
- Sales summaries
- Date range filtering

## 🎨 UI Components

### Reusable Controls
- **AdminSidebar**: Navigation for admin users
- **SalesSidebar**: Navigation for sales personnel
- Both include animated collapse/expand functionality

### Styling
- Modern card-based layouts
- Color-coded sections (Blue, Green, Orange, Red)
- Smooth animations and transitions
- Responsive grid layouts

## 🔒 Security

- **Password Hashing**: SHA256 algorithm
- **Role-Based Access**: Separate interfaces for different user types
- **Session Management**: Secure user session handling
- **Cloud Security**: Supabase row-level security (RLS) compatible

## 📦 Dependencies

```xml
<PackageReference Include="Supabase" Version="latest" />
<PackageReference Include="Microsoft.WindowsAppSDK" Version="1.x" />
<PackageReference Include="Microsoft.Windows.SDK.BuildTools" Version="10.x" />
<PackageReference Include="Npgsql" Version="latest" />
```

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is available for educational and commercial use. Please provide attribution when using this code.

## 👥 Authors

Developed as a comprehensive retail management solution.

## 🐛 Known Issues

- Reports are currently exported to local Downloads folder only
- Date pickers in report generation require manual validation
- Some validation messages could be more descriptive

## 🔮 Future Enhancements

- [ ] Receipt printing functionality
- [ ] Barcode scanning support
- [ ] Advanced analytics and charts
- [ ] Multi-store support
- [ ] Email receipt functionality
- [ ] Backup and restore features
- [ ] Mobile companion app
- [ ] Tax calculation customization

## 📞 Support

For issues and questions:
- Open an issue on GitHub
- Check existing documentation
- Review code comments

---

**Built with ❤️ using WinUI 3 and .NET**
