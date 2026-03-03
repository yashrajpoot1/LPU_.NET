# ✈️ Flight Search Engine - ASP.NET Core MVC Application

A comprehensive flight search application built with ASP.NET Core MVC that allows users to search for flights and flight+hotel packages.

## 📋 Features

- Search flights by source and destination
- Search flight + hotel packages
- Dynamic dropdown population from database
- Real-time validation
- Responsive Bootstrap UI
- Stored procedures for all database operations
- Clean MVC architecture

## 🛠️ Technology Stack

- ASP.NET Core MVC (.NET 10.0)
- SQL Server
- Bootstrap 5
- jQuery & jQuery Validation
- Font Awesome Icons

## 📁 Project Structure

```
Flight_Application/
├── Controllers/
│   ├── FlightController.cs      # Main flight search controller
│   └── HomeController.cs         # Home page controller
├── Models/
│   ├── SearchViewModel.cs        # Search form model
│   ├── FlightResult.cs           # Flight search results
│   └── FlightHotelResult.cs      # Package search results
├── Views/
│   ├── Flight/
│   │   ├── Index.cshtml          # Search form
│   │   └── Results.cshtml        # Results display
│   └── Shared/
│       └── _Layout.cshtml        # Main layout
├── Data/
│   └── DatabaseHelper.cs         # Database operations
├── Database/
│   └── FlightSearchDB.sql        # Database setup script
└── appsettings.json              # Configuration
```

## 🚀 Setup Instructions

### Prerequisites

- Visual Studio 2022 or later
- SQL Server 2019 or later (or SQL Server Express)
- .NET 10.0 SDK

### Step 1: Database Setup

1. Open SQL Server Management Studio (SSMS)
2. Open the file `Database/FlightSearchDB.sql`
3. Execute the entire script to:
   - Create the `FlightSearchDB` database
   - Create `Flights` and `Hotels` tables
   - Insert sample data (16 flights, 7 hotels)
   - Create 4 stored procedures

### Step 2: Configure Connection String

1. Open `appsettings.json`
2. Update the connection string based on your SQL Server setup:

**For Windows Authentication (Recommended):**
```json
"DefaultConnection": "Server=localhost;Database=FlightSearchDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

**For SQL Server Authentication:**
```json
"DefaultConnection": "Server=localhost;Database=FlightSearchDB;User Id=your_username;Password=your_password;TrustServerCertificate=True;"
```

**For SQL Server Express:**
```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FlightSearchDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

### Step 3: Install NuGet Packages

The project requires the following package:

```bash
dotnet add package Microsoft.Data.SqlClient
```

Or via Package Manager Console:
```powershell
Install-Package Microsoft.Data.SqlClient
```

### Step 4: Build and Run

1. Open the solution in Visual Studio
2. Build the solution (Ctrl + Shift + B)
3. Run the application (F5)
4. The application will open at `https://localhost:xxxx/Flight`

## 📊 Database Schema

### Flights Table
| Column | Type | Description |
|--------|------|-------------|
| FlightId | INT | Primary key, auto-increment |
| FlightName | NVARCHAR(100) | Flight name/number |
| FlightType | NVARCHAR(50) | Domestic/International |
| Source | NVARCHAR(100) | Departure city |
| Destination | NVARCHAR(100) | Arrival city |
| PricePerSeat | DECIMAL(18,2) | Price per person |

### Hotels Table
| Column | Type | Description |
|--------|------|-------------|
| HotelId | INT | Primary key, auto-increment |
| HotelName | NVARCHAR(100) | Hotel name |
| HotelType | NVARCHAR(50) | Hotel category |
| Location | NVARCHAR(100) | City location |
| PricePerDay | DECIMAL(18,2) | Price per day |

## 🔧 Stored Procedures

1. **sp_GetSources** - Returns distinct source cities
2. **sp_GetDestinations** - Returns distinct destination cities
3. **sp_SearchFlights** - Searches flights with total cost calculation
4. **sp_SearchFlightsWithHotels** - Searches flight+hotel packages

## 💡 Usage

1. **Select Source Location** - Choose departure city from dropdown
2. **Select Destination Location** - Choose arrival city from dropdown
3. **Enter Number of Persons** - Enter travelers count (1-10)
4. **Search Options:**
   - Click "Search Flights Only" for flight-only results
   - Click "Search Flight + Hotel" for package deals

## ✅ Validation Rules

- Source and Destination are required
- Number of persons must be between 1 and 10
- Source and Destination cannot be the same
- Client-side and server-side validation implemented

## 🎨 UI Features

- Responsive Bootstrap design
- Font Awesome icons
- Loading spinners during search
- Color-coded flight types (Domestic/International)
- Formatted currency display (₹)
- Breadcrumb navigation
- "No results" handling

## 📝 Sample Data

The database includes:
- **16 Flights**: Domestic (Mumbai, Delhi, Bangalore, Chennai) and International (Dubai, Singapore, Doha)
- **7 Hotels**: One hotel per city with varying price ranges

## 🧪 Testing Checklist

- [ ] Dropdowns populate correctly
- [ ] Validation messages display properly
- [ ] Flight search returns results
- [ ] Flight+Hotel search returns results
- [ ] Same source/destination validation works
- [ ] Number range validation (1-10) works
- [ ] Loading spinners appear on search
- [ ] Results display correctly formatted
- [ ] "No results" message shows when appropriate
- [ ] Back to search button works

## 🐛 Troubleshooting

### Connection Issues
- Verify SQL Server is running
- Check connection string in appsettings.json
- Ensure database exists and has data
- Check firewall settings

### No Results
- Verify stored procedures exist
- Check if sample data is inserted
- Ensure source and destination have matching flights

### Build Errors
- Ensure Microsoft.Data.SqlClient package is installed
- Check .NET 10.0 SDK is installed
- Clean and rebuild solution

## 📸 Screenshots

Take screenshots of:
1. Search page with populated dropdowns
2. Flight-only search results
3. Flight+Hotel search results
4. Validation error messages

## 🎓 Learning Outcomes

This project demonstrates:
- MVC architecture pattern
- Stored procedures usage
- Async/await programming
- Model validation
- Razor views and partial views
- Bootstrap integration
- jQuery validation
- Error handling
- Database connectivity

## 📄 License

This is an educational project for ASP.NET Core MVC learning purposes.

## 👨‍💻 Author

Created as part of ASP.NET Core MVC Assessment

---

**Note:** Make sure to update the connection string before running the application!
