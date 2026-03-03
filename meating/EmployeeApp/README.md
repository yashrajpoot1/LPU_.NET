# Employee Management Console Application

## 📋 Overview
A console-based application to manage employee records with the Employee class containing all required fields.

## 📝 Employee Class Fields

The Employee class contains the following fields:

- **EmployeeID** (Integer) - Unique employee identifier
- **FirstName** (String) - Employee's first name
- **LastName** (String) - Employee's last name
- **Title** (String) - Job title/designation
- **DOB** (Date) - Date of Birth
- **DOJ** (Date) - Date of Joining
- **City** (String) - Employee's city

## 🎯 Features

### Core Functionality
- ✅ Employee class with all required fields
- ✅ Add new employees
- ✅ Display all employees
- ✅ Search employee by ID
- ✅ Display detailed employee information
- ✅ Calculate age from DOB
- ✅ Calculate work experience from DOJ
- ✅ Generate statistics

### Additional Features
- ✅ Sample data pre-loaded
- ✅ Duplicate ID validation
- ✅ Formatted display
- ✅ Statistics by city and title
- ✅ Average age and experience calculation

## 📁 Project Structure

```
EmployeeApp/
├── Models/
│   └── Employee.cs (Employee class with all fields)
├── Program.cs (Main console application)
├── EmployeeApp.csproj
└── README.md
```

## 🚀 How to Run

### Prerequisites
- .NET 8.0 SDK or later

### Running the Application
```bash
cd meating/EmployeeApp
dotnet run
```

### Building the Application
```bash
dotnet build
```

## 🎮 Menu Options

1. **Add New Employee** - Register new employee with all details
2. **Display All Employees** - View list of all employees
3. **Search Employee by ID** - Find specific employee
4. **Display Employee Details** - View complete employee information
5. **Display Statistics** - View analytics and statistics
6. **Exit** - Close application

## 📊 Sample Data

The application comes pre-loaded with 5 sample employees:

| ID | Name | Title | DOB | DOJ | City |
|----|------|-------|-----|-----|------|
| 1 | Rajesh Kumar | Software Engineer | 15-May-1990 | 01-Jul-2015 | Mumbai |
| 2 | Priya Sharma | Senior Developer | 20-Aug-1988 | 15-Mar-2012 | Pune |
| 3 | Amit Patel | Team Lead | 10-Dec-1985 | 20-Jan-2010 | Bangalore |
| 4 | Sneha Singh | Project Manager | 25-Mar-1987 | 10-Jun-2011 | Delhi |
| 5 | Vikram Verma | Software Engineer | 08-Jul-1992 | 05-Sep-2018 | Mumbai |

## 💡 Employee Class Features

### Properties
```csharp
public int EmployeeID { get; set; }
public string FirstName { get; set; }
public string LastName { get; set; }
public string Title { get; set; }
public DateTime DOB { get; set; }
public DateTime DOJ { get; set; }
public string City { get; set; }
```

### Methods
- `DisplayDetails()` - Display complete employee information
- `CalculateAge()` - Calculate age from DOB
- `CalculateExperience()` - Calculate work experience from DOJ
- `GetFullName()` - Get full name (FirstName + LastName)
- `ToString()` - Formatted string representation

## 🧪 Usage Examples

### Adding an Employee
```
Enter Employee ID: 6
Enter First Name: Anita
Enter Last Name: Desai
Enter Title: Business Analyst
Enter Date of Birth (dd-MM-yyyy): 15-06-1991
Enter Date of Joining (dd-MM-yyyy): 10-04-2016
Enter City: Chennai

✅ Employee added successfully!
```

### Searching an Employee
```
Enter Employee ID to search: 1

✅ Employee Found:
Employee ID:         1
Name:                Rajesh Kumar
Title:               Software Engineer
Date of Birth:       15-May-1990
Date of Joining:     01-Jul-2015
City:                Mumbai
Age:                 35 years
Experience:          10 years
```

### Statistics Display
```
Total Employees: 5
Average Age: 36.2 years
Average Experience: 12.4 years

Employees by City:
  Mumbai: 2
  Pune: 1
  Bangalore: 1
  Delhi: 1

Employees by Title:
  Software Engineer: 2
  Senior Developer: 1
  Team Lead: 1
  Project Manager: 1
```

## 🔧 Technical Details

### Date Format
- Input format: `dd-MM-yyyy`
- Display format: `dd-MMM-yyyy`

### Calculations
- **Age**: Current Year - Birth Year (adjusted for day of year)
- **Experience**: Current Year - Joining Year (adjusted for day of year)

### Data Storage
- In-memory storage using `List<Employee>`
- No database or file persistence (runtime only)

## 🎓 Learning Outcomes

This application demonstrates:
1. ✅ **Class Design** - Creating a class with multiple properties
2. ✅ **Data Types** - Using Integer, String, and DateTime
3. ✅ **Collections** - Managing List<Employee>
4. ✅ **Date Operations** - Working with DateTime
5. ✅ **Methods** - Instance methods for calculations
6. ✅ **Console I/O** - User input and formatted output
7. ✅ **Validation** - Duplicate ID checking
8. ✅ **Statistics** - Data aggregation and analysis

## 📝 Notes

- All fields are required when adding an employee
- Employee ID must be unique
- Date format must be dd-MM-yyyy
- Sample data is loaded automatically on startup
- Data is stored in memory (not persisted to file)

## 🔮 Future Enhancements

- File persistence (serialization)
- Update employee details
- Delete employee records
- Advanced search filters
- Salary management
- Department management
- Export to CSV/Excel
- Database integration

---

**Created:** 18-Feb-2026  
**Target Framework:** .NET 8.0  
**Purpose:** Employee Management with Employee Class
