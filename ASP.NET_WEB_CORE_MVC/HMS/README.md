# Hospital Management System (HMS)

## Architecture Overview

This project follows a **Layered Architecture** pattern with clear separation of concerns:

```
HMS/
├── Domain/                    # Domain Layer (Core Business Entities)
│   ├── Entities/
│   │   ├── Patient.cs
│   │   ├── Doctor.cs
│   │   └── Appointment.cs
│   └── Interfaces/
│       ├── IPatientRepository.cs
│       ├── IDoctorRepository.cs
│       └── IAppointmentRepository.cs
│
├── Application/               # Application Layer (Business Logic)
│   └── Services/
│       ├── PatientService.cs
│       ├── DoctorService.cs
│       └── AppointmentService.cs
│
├── Infrastructure/            # Infrastructure Layer (Data Access)
│   ├── Data/
│   │   └── HospitalDbContext.cs
│   └── Repositories/
│       ├── PatientRepository.cs
│       ├── DoctorRepository.cs
│       └── AppointmentRepository.cs
│
└── Controllers/               # Presentation Layer (MVC Controllers)
    ├── HomeController.cs
    ├── PatientsController.cs
    ├── DoctorsController.cs
    └── AppointmentsController.cs
```

## Technology Stack

- **Framework**: ASP.NET Core 10.0 MVC
- **ORM**: Entity Framework Core 9.0
- **Database**: SQL Server (LocalDB)
- **Architecture**: Layered Architecture with Repository Pattern

## Layer Responsibilities

### 1. Domain Layer
- Contains core business entities (Patient, Doctor, Appointment)
- Defines repository interfaces
- No dependencies on other layers
- Pure business logic and rules

### 2. Application Layer
- Business logic services
- Orchestrates domain entities
- Implements use cases
- Depends only on Domain layer

### 3. Infrastructure Layer
- EF Core DbContext
- Repository implementations
- Database configurations
- External service integrations
- Depends on Domain and Application layers

### 4. Presentation Layer
- MVC Controllers
- Views (Razor)
- ViewModels
- User interface logic
- Depends on Application layer

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 or VS Code

### Setup Instructions

1. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

2. **Update Connection String** (if needed)
   Edit `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "HospitalDb": "Server=(localdb)\\mssqllocaldb;Database=HospitalManagementDB;Trusted_Connection=true"
   }
   ```

3. **Create Database Migration**
   ```bash
   dotnet ef migrations add InitialCreate
   ```

4. **Update Database**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

## Database Schema

### Patients Table
- PatientId (PK)
- Name
- DateOfBirth
- Gender
- Phone
- Email
- Address
- RegistrationDate

### Doctors Table
- DoctorId (PK)
- Name
- Specialization
- Phone
- Email
- JoinDate
- IsActive

### Appointments Table
- AppointmentId (PK)
- PatientId (FK)
- DoctorId (FK)
- AppointmentDate
- Reason
- Status (Scheduled/Completed/Cancelled)
- Notes

## Features

### Patient Management
- Register new patients
- View all patients
- Update patient information
- Delete patients
- Search patients by name

### Doctor Management
- Add new doctors
- View all doctors
- Update doctor information
- Delete doctors
- Filter by specialization

### Appointment Management
- Book appointments
- View all appointments
- Update appointments
- Cancel appointments
- View by patient/doctor/date

## Next Steps

### Phase 1: Views (Current Phase)
- [ ] Create Razor views for Patients (Index, Create, Edit, Delete, Details)
- [ ] Create Razor views for Doctors (Index, Create, Edit, Delete, Details)
- [ ] Create Razor views for Appointments (Index, Create, Edit, Details)
- [ ] Update Home/Index.cshtml with dashboard

### Phase 2: Enhancements
- [ ] Add validation attributes to entities
- [ ] Implement DTOs (Data Transfer Objects)
- [ ] Add AutoMapper for entity-DTO mapping
- [ ] Implement error handling middleware
- [ ] Add logging (Serilog)

### Phase 3: Advanced Features
- [ ] Add authentication & authorization
- [ ] Implement search and filtering
- [ ] Add pagination
- [ ] Create dashboard with statistics
- [ ] Add appointment scheduling validation

### Phase 4: API Layer
- [ ] Create ASP.NET Core Web API project
- [ ] Implement REST endpoints
- [ ] Add Swagger documentation
- [ ] Implement JWT authentication

### Phase 5: Microservices (Future)
- [ ] Split into separate services (Doctor, Patient, Appointment)
- [ ] Implement API Gateway
- [ ] Add message queue (RabbitMQ)
- [ ] Containerize with Docker

## Architecture Benefits

✅ **Separation of Concerns**: Each layer has a single responsibility  
✅ **Testability**: Easy to unit test business logic  
✅ **Maintainability**: Changes in one layer don't affect others  
✅ **Scalability**: Can evolve to microservices  
✅ **Flexibility**: Easy to swap implementations (ADO.NET ↔ EF Core)

## Contributing

This is a learning project demonstrating enterprise architecture patterns.
