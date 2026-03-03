# Student Portal Management System

A modern ASP.NET Core MVC application for managing students, courses, and enrollments with a clean, responsive UI.

## Features

- **Student Management**: Create, read, update, and delete student records
- **Course Management**: Manage courses with duration, fees, and difficulty levels
- **Enrollment Tracking**: Track student enrollments in courses
- **Responsive UI**: Bootstrap 5 with modern design and icons
- **Form Validation**: Client and server-side validation
- **Date Picker**: HTML5 date input with validation
- **Email Validation**: Built-in email format validation
- **Repository Pattern**: Clean architecture with service and repository layers

## Technology Stack

- **Framework**: ASP.NET Core 10.0 MVC
- **Database**: SQL Server with Entity Framework Core 10.0.3
- **UI**: Bootstrap 5 with Bootstrap Icons
- **Validation**: Data Annotations with jQuery Validation
- **Architecture**: Repository and Service Pattern

## Prerequisites

- .NET 10.0 SDK
- SQL Server Express (or SQL Server)
- Visual Studio 2022 or VS Code

## Database Schema

### Students Table
- StudentId (Primary Key)
- FullName (required, 2-120 characters)
- Email (required, unique, valid email format)
- Phone (optional, valid phone format)
- Status (Active/Inactive/Suspended)
- JoinDate (required, date)
- CreatedAt (auto-generated)

### Courses Table
- CourseId (Primary Key)
- Title (required, 3-150 characters)
- DurationDays (1-365 days)
- Fee (decimal, currency)
- Level (Beginner/Intermediate/Advanced)
- IsActive (boolean)
- CreatedAt (auto-generated)

### Enrollments Table
- EnrollmentId (Primary Key)
- StudentId (Foreign Key)
- CourseId (Foreign Key)
- EnrollDate (date)
- PaymentStatus (Pending/Paid/Partial)
- PaidAmount (decimal)
- CreatedAt (auto-generated)

### TblLog Table
- LogId (Primary Key)
- StudentId (Foreign Key)
- Info (varchar 2000)

## Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd StudentPortal
```

### 2. Update Connection String
Edit `appsettings.json` and update the connection string if needed:
```json
{
  "ConnectionStrings": {
    "defaultconnection": "Server=.\\SQLExpress;Database=StudentPortal;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### 3. Restore NuGet Packages
```bash
dotnet restore
```

### 4. Create Database
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Build the Project
```bash
dotnet build
```

### 6. Run the Application
```bash
dotnet run
```

The application will start at `http://localhost:5013`

## Project Structure

```
StudentPortal/
├── Controllers/
│   ├── StudentsController.cs
│   ├── CoursesController.cs
│   └── HomeController.cs
├── Models/
│   ├── Student.cs
│   ├── Course.cs
│   ├── Enrollment.cs
│   ├── TblLog.cs
│   ├── ErrorViewModel.cs
│   └── StudentPortalDbContext.cs
├── Repositories/
│   ├── IStudentRepository.cs
│   └── StudentRepository.cs
├── Services/
│   ├── IStudentService.cs
│   └── StudentService.cs
├── Views/
│   ├── Students/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Details.cshtml
│   │   └── Delete.cshtml
│   ├── Courses/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Details.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
│       └── _Layout.cshtml
├── wwwroot/
├── appsettings.json
└── Program.cs
```

## Usage

### Managing Students

1. Navigate to the Students page from the navigation menu
2. Click "Add New Student" to create a new student
3. Fill in the form:
   - Full Name (required)
   - Email (required, must be unique)
   - Phone (optional)
   - Join Date (required, cannot be future date)
4. Click "Create Student" to save
5. Use action buttons to View, Edit, or Delete students

### Managing Courses

1. Navigate to the Courses page from the navigation menu
2. Click "Add New Course" to create a new course
3. Fill in the form:
   - Title (required)
   - Duration in Days (1-365)
   - Fee (required, currency format)
   - Level (Beginner/Intermediate/Advanced)
4. Click "Create Course" to save
5. Courses are automatically set as Active

## Validation Rules

### Student Form
- Full Name: 2-120 characters, required
- Email: Valid email format, unique, required
- Phone: Valid phone format, optional
- Join Date: Cannot be in the future, required

### Course Form
- Title: 3-150 characters, required
- Duration: 1-365 days, required
- Fee: Positive decimal value, required
- Level: Must select from dropdown, required

## Features Highlights

### UI/UX
- Clean, modern Bootstrap 5 design
- Responsive layout for mobile and desktop
- Bootstrap Icons for visual clarity
- Color-coded status badges
- Card-based layouts
- Hover effects on tables

### Form Features
- HTML5 date picker with max date validation
- Real-time client-side validation
- Server-side validation fallback
- Clear error messages
- Input placeholders and hints

### Data Management
- Automatic timestamp generation
- Default status values
- Unique email constraint
- Foreign key relationships
- Soft delete capability

## Development

### Adding New Migrations
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Rollback Migration
```bash
dotnet ef database update PreviousMigrationName
```

### Remove Last Migration
```bash
dotnet ef migrations remove
```

## Troubleshooting

### Database Connection Issues
- Verify SQL Server is running
- Check connection string in appsettings.json
- Ensure TrustServerCertificate=True for local development

### Migration Issues
- Delete Migrations folder and recreate
- Drop database and recreate: `dotnet ef database drop`

### Build Errors
- Clean and rebuild: `dotnet clean && dotnet build`
- Restore packages: `dotnet restore`

## License

This project is for educational purposes.

## Author

Student Portal Management System - 2026
