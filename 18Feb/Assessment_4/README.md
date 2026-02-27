# Assessment 4 - CampusHire Applicant Management System

## 📋 Overview
This assessment contains the CampusHire Applicant Management System, a comprehensive console-based application for managing fresh graduate applicant records with data persistence using serialization.

## 🎯 Project: CampusHire Applicant Management

**Location:** `CampusHireSystem/`

### Scenario
CampusHire is a professional consultancy firm that assists fresh graduates in finding suitable job opportunities. The system enables the operations team to manage applicant information efficiently and securely.

## ✅ System Requirements Implemented

### Core Functionality
1. ✅ Add new applicant records
2. ✅ Store records in collection during runtime
3. ✅ Persist data using binary serialization
4. ✅ Display all applicant details
5. ✅ Search applicant by ID
6. ✅ Update applicant details
7. ✅ Delete applicant records

### Additional Features
- ✅ Filter by location, competency, or passing year
- ✅ Generate statistics and reports
- ✅ Automatic data loading/saving
- ✅ Comprehensive validation

## 📝 Applicant Information

### Required Fields
- **Applicant ID**: CH + 6 digits (e.g., CH123456)
- **Applicant Name**: 4-15 characters
- **Current Location**: Mumbai / Pune / Chennai
- **Preferred Job Location**: Mumbai / Pune / Chennai / Delhi / Kolkata / Bangalore
- **Core Competency**: .NET / JAVA / ORACLE / Testing
- **Passing Year**: Not greater than current year

## 🔒 Validation Rules

| Field | Rule | Example |
|-------|------|---------|
| Applicant ID | Exactly 8 characters, starts with "CH" | CH123456 ✅ |
| Applicant Name | 4-15 characters | John Doe ✅ |
| Current Location | Mumbai/Pune/Chennai only | Mumbai ✅ |
| Preferred Location | 6 cities available | Bangalore ✅ |
| Core Competency | .NET/JAVA/ORACLE/Testing | .NET ✅ |
| Passing Year | ≤ Current Year | 2024 ✅ |

## 📁 Project Structure

```
CampusHireSystem/
├── Models/
│   └── Applicant.cs (Serializable model)
├── Services/
│   ├── ApplicantService.cs (Business logic)
│   └── FileService.cs (Serialization)
├── Utilities/
│   └── ApplicantValidator.cs (Validation)
├── Program.cs (Console UI)
├── CampusHireSystem.csproj
└── README.md
```

## 🚀 How to Run

```bash
cd 18Feb/Assessment_4/CampusHireSystem
dotnet run
```

## 🎮 Menu Options

1. **Add New Applicant** - Register fresh graduate
2. **Display All Applicants** - View complete list
3. **Search Applicant by ID** - Find specific record
4. **Update Applicant Details** - Modify existing data
5. **Delete Applicant** - Remove record
6. **Filter Applicants** - By location/competency/year
7. **View Statistics** - Generate reports
8. **Exit** - Close application

## 💾 Data Persistence

### Serialization Features
- **Format**: Binary serialization
- **File**: `applicants.dat`
- **Location**: Application base directory
- **Auto-save**: After each operation
- **Auto-load**: On application startup

### Benefits
- Data persists between sessions
- No manual save required
- Efficient storage
- Fast load/save operations

## 🧪 Testing Scenarios

### Valid Applicant
```
ID: CH123456
Name: Rajesh Kumar
Current Location: Mumbai
Preferred Location: Bangalore
Competency: .NET
Year: 2024
Result: ✅ Success
```

### Invalid ID (Wrong Prefix)
```
ID: MD123456
Result: ❌ Applicant ID must start with 'CH'
```

### Invalid Name (Too Short)
```
Name: "AB"
Result: ❌ Name must be at least 4 characters
```

### Invalid Year (Future)
```
Year: 2027
Result: ❌ Year cannot be greater than 2026
```

## 📊 Features Demonstration

### Add Applicant
```
Input: Complete applicant details
Validation: All fields checked
Output: Applicant added + saved to file
```

### Search Applicant
```
Input: CH123456
Output: Complete applicant details displayed
```

### Update Applicant
```
Input: CH123456 + new details
Process: Validates + updates + saves
Output: Updated details displayed
```

### Delete Applicant
```
Input: CH123456
Confirmation: Required
Output: Applicant removed + file updated
```

### Filter Applicants
```
Options:
1. By Location (Current/Preferred)
2. By Core Competency
3. By Passing Year
Output: Filtered list displayed
```

### Statistics Report
```
Shows:
- Total applicants
- Breakdown by location
- Breakdown by competency
- Breakdown by passing year
```

## 🔧 Technical Implementation

### Serialization
```csharp
[Serializable]
public class Applicant
{
    // All properties serializable
}
```

### Validation
```csharp
// ID: Exactly 8 chars, starts with "CH"
ValidateApplicantId(id)

// Name: 4-15 characters
ValidateApplicantName(name)

// Year: Not greater than current
ValidatePassingYear(year)
```

### File Operations
```csharp
// Save
BinaryFormatter.Serialize(fileStream, applicants)

// Load
BinaryFormatter.Deserialize(fileStream)
```

## 📈 Statistics Example

```
Total Applicants: 15

By Current Location:
  Mumbai: 6
  Pune: 5
  Chennai: 4

By Core Competency:
  .NET: 7
  JAVA: 4
  ORACLE: 2
  Testing: 2

By Passing Year:
  2024: 8
  2023: 5
  2022: 2
```

## 🎓 Learning Outcomes

1. ✅ **Serialization** - Binary file persistence
2. ✅ **Collections** - List<T> management
3. ✅ **Validation** - Input validation rules
4. ✅ **CRUD Operations** - Complete data management
5. ✅ **File I/O** - Read/write operations
6. ✅ **Exception Handling** - Error management
7. ✅ **LINQ** - Data querying
8. ✅ **Service Layer** - Business logic separation
9. ✅ **Console UI** - User-friendly interface
10. ✅ **Data Persistence** - State management

## 💡 Key Highlights

- **Automatic Persistence**: No manual save needed
- **Comprehensive Validation**: All inputs validated
- **User-Friendly**: Clear menus and prompts
- **Flexible Filtering**: Multiple filter options
- **Statistics**: Detailed reporting
- **Error Handling**: Graceful error management
- **Data Integrity**: Validation on load/save

## 🔮 Future Enhancements

- Export to CSV/Excel
- Import from file
- Advanced search
- Email notifications
- Database integration
- Web interface
- Multi-user support
- Audit logging

## 📝 Important Notes

- Data file created automatically
- Binary serialization for efficiency
- All validation rules enforced
- Data persists between sessions
- Requires write permissions

## ⚠️ Requirements

- .NET 8.0 SDK
- Write permissions for data file
- Do not manually edit data file

---

**Created:** 18-Feb-2026  
**Target Framework:** .NET 8.0  
**Purpose:** Applicant management with data persistence
