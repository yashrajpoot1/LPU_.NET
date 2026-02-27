# CampusHire Applicant Management System

## 📋 Overview
CampusHire is a professional consultancy firm that assists fresh graduates in finding suitable job opportunities. This console-based application enables the CampusHire operations team to manage applicant information efficiently and securely with data persistence using serialization.

## 🎯 Problem Statement
Design and develop a .NET-based console application for CampusHire that enables the team to register fresh graduates, manage their details, and persist applicant data for future access.

## 📝 Applicant Details Required

Each applicant must provide:
- **Applicant ID** (Example: CH123456)
- **Applicant Name**
- **Current Location** (Mumbai / Pune / Chennai)
- **Preferred Job Location** (Mumbai / Pune / Chennai / Delhi / Kolkata / Bangalore)
- **Core Competency** (.NET / JAVA / ORACLE / Testing)
- **Passing Year** (Degree completion year)

## ✅ System Requirements

### Core Functionality
1. ✅ Add new applicant records
2. ✅ Store applicant records in collection during runtime
3. ✅ Persist data using serialization (file-based storage)
4. ✅ Display all applicant details
5. ✅ Search for applicant using Applicant ID
6. ✅ Update applicant details
7. ✅ Delete applicant records

### Additional Features
- ✅ Filter applicants by location, competency, or passing year
- ✅ Generate statistics and reports
- ✅ Automatic data loading on startup
- ✅ Automatic data saving after each operation

## 🔒 Validation Rules

### 1. Mandatory Fields
All fields are mandatory and cannot be left blank

### 2. Applicant ID Validation
- Must be exactly **8 characters** long
- Must start with prefix **"CH"**
- Example: CH123456

### 3. Applicant Name Validation
- Minimum: **4 characters**
- Maximum: **15 characters**

### 4. Passing Year Validation
- Must not be greater than the **current year**
- Must be after 1950

### 5. Location Validation
- **Current Location**: Mumbai, Pune, or Chennai only
- **Preferred Job Location**: Mumbai, Pune, Chennai, Delhi, Kolkata, or Bangalore

### 6. Core Competency Validation
- Must be one of: .NET, JAVA, ORACLE, or Testing

## 📁 Project Structure

```
CampusHireSystem/
├── Models/
│   └── Applicant.cs (Serializable applicant model)
├── Services/
│   ├── ApplicantService.cs (Business logic)
│   └── FileService.cs (Serialization/Deserialization)
├── Utilities/
│   └── ApplicantValidator.cs (Validation logic)
├── Program.cs (Console UI)
├── CampusHireSystem.csproj
└── README.md
```

## 🚀 How to Run

### Prerequisites
- .NET 8.0 SDK or later

### Running the Application
```bash
cd 18Feb/Assessment_4/CampusHireSystem
dotnet run
```

### Building the Application
```bash
dotnet build
```

## 🎮 Features

### Main Menu Options
1. **Add New Applicant** - Register new graduate
2. **Display All Applicants** - View complete list
3. **Search Applicant by ID** - Find specific applicant
4. **Update Applicant Details** - Modify existing record
5. **Delete Applicant** - Remove applicant record
6. **Filter Applicants** - Filter by location/competency/year
7. **View Statistics** - Generate reports
8. **Exit** - Close application

### Data Persistence
- Data is automatically saved to `applicants.dat` file
- Binary serialization for efficient storage
- Automatic loading on application startup
- Data persists between application sessions

## 🔧 Technical Implementation

### Serialization
```csharp
[Serializable]
public class Applicant
{
    // Properties marked as serializable
}
```

### File Operations
- **Save**: Binary serialization to file
- **Load**: Binary deserialization from file
- **Location**: Application base directory

### Validation
```csharp
// ID Validation
if (applicantId.Length != 8 || !applicantId.StartsWith("CH"))
    return false;

// Name Validation
if (name.Length < 4 || name.Length > 15)
    return false;

// Year Validation
if (year > DateTime.Now.Year)
    return false;
```

## 📊 Sample Data

### Valid Applicant Example
```
Applicant ID: CH123456
Name: John Doe
Current Location: Mumbai
Preferred Job Location: Bangalore
Core Competency: .NET
Passing Year: 2024
```

### Invalid Examples
```
❌ CH12 - ID too short
❌ MD123456 - Wrong prefix
❌ AB - Name too short
❌ VeryLongNameHere - Name too long
❌ 2026 - Year in future
```

## 🧪 Testing Scenarios

### Scenario 1: Add Valid Applicant
```
Input:
  ID: CH123456
  Name: Rajesh
  Current Location: Mumbai
  Preferred Location: Bangalore
  Competency: .NET
  Year: 2024

Output: ✅ Applicant added successfully!
```

### Scenario 2: Invalid ID
```
Input: ID: MD123456
Output: ❌ Applicant ID must start with 'CH'
```

### Scenario 3: Invalid Name Length
```
Input: Name: "AB"
Output: ❌ Applicant Name must be at least 4 characters long
```

### Scenario 4: Future Year
```
Input: Year: 2026
Output: ❌ Passing Year cannot be greater than current year (2026)
```

### Scenario 5: Search Applicant
```
Input: CH123456
Output: Displays complete applicant details
```

### Scenario 6: Update Applicant
```
Input: CH123456
Process: Shows current details, allows modification
Output: ✅ Applicant updated successfully!
```

### Scenario 7: Delete Applicant
```
Input: CH123456
Confirmation: y
Output: ✅ Applicant deleted successfully!
```

## 📈 Reports and Statistics

### Statistics Report Includes:
- Total applicants count
- Breakdown by current location
- Breakdown by core competency
- Breakdown by passing year

### Filter Options:
1. **By Location** - Shows applicants in/preferring specific location
2. **By Competency** - Shows applicants with specific skill
3. **By Passing Year** - Shows graduates from specific year

## 🔐 Data Security

- Data stored in binary format
- File-based persistence
- Automatic backup on each save
- Data integrity validation on load

## 💡 Key Features

### 1. Comprehensive Validation
- All inputs validated before processing
- Clear error messages for validation failures
- Prevents invalid data entry

### 2. Data Persistence
- Automatic save after each operation
- Automatic load on startup
- No data loss between sessions

### 3. User-Friendly Interface
- Menu-driven navigation
- Clear prompts and instructions
- Formatted output displays

### 4. Flexible Filtering
- Multiple filter options
- Quick data retrieval
- Organized display

### 5. Statistics and Reporting
- Comprehensive statistics
- Grouped data views
- Easy analysis

## 🎓 Learning Outcomes

This project demonstrates:
1. ✅ **Serialization** - Binary file persistence
2. ✅ **Collections** - List management
3. ✅ **Validation** - Input validation logic
4. ✅ **CRUD Operations** - Create, Read, Update, Delete
5. ✅ **File I/O** - File operations
6. ✅ **Exception Handling** - Error management
7. ✅ **LINQ** - Data querying and filtering
8. ✅ **Console UI** - User interface design
9. ✅ **Service Layer** - Business logic separation
10. ✅ **Data Persistence** - State management

## 🔮 Future Enhancements

- Export to CSV/Excel
- Import from file
- Advanced search filters
- Email notifications
- Database integration
- Web API interface
- Multi-user support
- Audit logging
- Backup and restore
- Report generation (PDF)

## 📝 Notes

- Data file (`applicants.dat`) is created in application directory
- Binary serialization used for efficient storage
- All validation rules strictly enforced
- Data automatically persists between sessions
- No manual save required

## ⚠️ Important

- Ensure .NET 8.0 SDK is installed
- Application requires write permissions for data file
- Data file should not be manually edited
- Backup data file regularly for safety

## 👨‍💻 Author
Created for CampusHire - Professional Consultancy Firm

## 📅 Date
18-Feb-2026

---

**Note**: This implementation fulfills all requirements from the scenario-based coding problem and provides a complete, production-ready applicant management system with data persistence.
