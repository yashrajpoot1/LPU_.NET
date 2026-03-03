# University Management System (UMS)

A mini University Management System built with ASP.NET Core following Clean Architecture principles.

## What is This Project?

This is a simple web application that helps manage university data like students, courses, and enrollments. You can view lists of students, see what courses are available, and track which students are enrolled in which courses.

## Clean Architecture Structure

This project follows Clean Architecture, which means the code is organized in layers:

### 1. Domain Layer (Domain/Entities/)
- Contains the core business objects (Student, Course, Enrollment)
- These are simple classes that represent real-world things
- No dependencies on other layers

### 2. Application Layer (Application/)
- **Interfaces/** - Defines what operations we can do (like getting all students)
- **Services/** - Implements those operations (the actual code that gets students from database)
- This layer contains business logic

### 3. Infrastructure Layer (Data/)
- **ApplicationDbContext.cs** - Connects to the database
- Handles all database operations
- Contains sample data to get started

### 4. Presentation Layer (Controllers/ and Views/)
- **Controllers/** - Handle web requests and return responses
- **Views/** - The HTML pages users see
- This is what users interact with

## Database Tables

The system has 3 main tables:

### Students Table
Stores student information:
- ID (unique number)
- First Name
- Last Name
- Email
- Phone Number
- Date of Birth
- Enrollment Date

### Courses Table
Stores course information:
- ID (unique number)
- Course Code (like CS101)
- Course Name
- Description
- Credits
- Instructor Name

### Enrollments Table
Links students to courses:
- ID (unique number)
- Student ID (which student)
- Course ID (which course)
- Enrollment Date
- Grade
- Status (Active, Completed, or Dropped)

## How to Run the Project

### Step 1: Create the Database
Open a terminal in the project folder and run:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

This creates the database with sample data already included.

### Step 2: Run the Application
```bash
dotnet run
```

The application will start and show you a URL (like https://localhost:5001)

### Step 3: Open in Browser
Open your web browser and go to the URL shown in the terminal.

## What You Can Do

### Home Page
- Shows three cards with links to Students, Courses, and Enrollments

### Students Page
- View all students in a table
- Click "Details" to see a specific student's information and their enrolled courses

### Courses Page
- View all available courses
- See how many students are enrolled in each course
- Click "Details" to see course information and enrolled students

### Enrollments Page
- View all enrollments
- See which students are taking which courses
- Check grades and enrollment status

## Sample Data Included

The system comes with sample data:

**3 Students:**
- John Doe
- Jane Smith
- Mike Johnson

**3 Courses:**
- CS101: Introduction to Programming
- CS201: Data Structures
- MATH101: Calculus I

**5 Enrollments:**
- Various students enrolled in different courses with different statuses

## Technologies Used

- ASP.NET Core 10.0 - Web framework
- Entity Framework Core - Database access
- SQL Server - Database
- Bootstrap 5 - Styling
- Razor Pages - HTML templates

## Project Structure

```
UMS/
├── Domain/
│   └── Entities/          # Student, Course, Enrollment classes
├── Application/
│   ├── Interfaces/        # Service interfaces
│   └── Services/          # Service implementations
├── Data/
│   └── ApplicationDbContext.cs  # Database configuration
├── Controllers/           # Web request handlers
├── Views/                 # HTML pages
│   ├── Students/
│   ├── Courses/
│   ├── Enrollments/
│   └── Shared/
└── Program.cs            # Application startup
```

## Why Clean Architecture?

Clean Architecture makes the code:
- **Easy to understand** - Each layer has a clear purpose
- **Easy to test** - You can test business logic without a database
- **Easy to change** - You can swap the database or UI without changing business logic
- **Maintainable** - New developers can understand the structure quickly

## Next Steps

You can extend this project by:
- Adding create/edit/delete functionality
- Adding search and filtering
- Adding authentication and authorization
- Adding more entities (like Departments, Teachers, etc.)
- Adding reports and statistics (User Management System)

An ASP.NET Core MVC web application with Identity authentication and Entity Framework Core for user management.

## Technology Stack

- ASP.NET Core 10.0
- Entity Framework Core 10.0.3
- ASP.NET Core Identity
- SQL Server (LocalDB/SQLExpress)
- Razor Pages & MVC

## Features

- User authentication and authorization using ASP.NET Core Identity
- User registration with email confirmation
- Secure login/logout functionality
- SQL Server database integration
- Entity Framework Core migrations
- MVC architecture with Razor views

## Project Structure

```
UMS/
├── Controllers/        # MVC Controllers
├── Models/            # Data models and view models
├── Views/             # Razor views
├── Data/              # Database context and migrations
├── Areas/             # Identity UI areas
├── wwwroot/           # Static files (CSS, JS, images)
└── Program.cs         # Application entry point
```

## Prerequisites

- .NET 10.0 SDK
- SQL Server Express or SQL Server LocalDB
- Visual Studio 2022 or VS Code

## Configuration

### Database Connection

The application uses SQL Server with the following connection string (configured in `appsettings.json`):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLExpress;Database=Trial_LBU_DB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Update this connection string based on your SQL Server instance.

## Setup Instructions

1. Clone or navigate to the project directory
2. Update the connection string in `appsettings.json` if needed
3. Run database migrations:
   ```bash
   dotnet ef database update
   ```
4. Build the project:
   ```bash
   dotnet build
   ```
5. Run the application:
   ```bash
   dotnet run
   ```

## Running the Application

The application will start on HTTPS by default. Navigate to:
- `https://localhost:5001` (or the port shown in the console)

## Key Dependencies

- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` - Identity framework
- `Microsoft.EntityFrameworkCore.SqlServer` - SQL Server provider
- `Microsoft.AspNetCore.Identity.UI` - Pre-built Identity UI
- `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` - EF Core diagnostics

## Development Notes

- The application requires email confirmation for new user accounts
- HTTPS redirection is enabled by default
- Development environment uses migrations endpoint for database errors
- Production environment uses custom error handling

## Database Migrations

To create a new migration:
```bash
dotnet ef migrations add MigrationName
```

To update the database:
```bash
dotnet ef database update
```

## Security Features

- Password hashing and salting
- Anti-forgery tokens
- HTTPS enforcement
- Secure authentication cookies
- Email confirmation requirement
