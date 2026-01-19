# Exam Scheduler System - Scenario-Based Assessment

**Course:** Object-Oriented Programming with C#  
**Topic:** Advanced OOP Concepts, Collections, Inheritance, and Role-Based Systems  
**Difficulty Level:** Advanced  
**Estimated Time:** 3-4 hours  
**Date:** December 27, 2025

---

## 📋 Table of Contents
1. [Learning Objectives](#learning-objectives)
2. [Scenario Overview](#scenario-overview)
3. [Business Requirements](#business-requirements)
4. [Technical Requirements](#technical-requirements)
5. [Complete Solution](#complete-solution)
6. [Key Concepts Demonstrated](#key-concepts-demonstrated)
7. [Testing & Validation](#testing--validation)

---

## 🎯 Learning Objectives

By completing this scenario, you will be able to:
- Design and implement a complex role-based system using inheritance
- Apply abstract classes and understand their use cases
- Implement encapsulation and data protection patterns
- Use collections effectively (List, Dictionary)
- Implement LINQ queries for data filtering and sorting
- Design Data Transfer Objects (DTOs) for view layers
- Apply separation of concerns principle
- Implement dependency management between objects

---

## 📖 Scenario Overview

### **Real-World Context**

You are hired as a software developer at **XYZ University** to build an **Exam Scheduler Management System**. The university's examination department is overwhelmed with manual exam scheduling processes and needs an automated system to:

1. **Manage Academic Structure**: Handle departments, sections, and student enrollments
2. **Schedule Examinations**: Allow HODs to schedule exams with dates and venues
3. **Assign Examiners**: Enable systematic assignment of examiners to specific examinations
4. **Generate Schedules**: Provide personalized schedules for students, examiners, and department heads

### **Key Stakeholders**

- **Head of Department (HOD)**: Manages exam scheduling, examiner assignments, and section management
- **Examiners**: Faculty members assigned to supervise examinations
- **Students**: Enrolled in sections and need to view their exam schedules
- **Academic Administration**: Requires comprehensive oversight of all examination activities

---

## 📝 Business Requirements

### **Requirement 1: Role Management System**
- Multiple people can have different roles (HOD, Examiner)
- A person's roles should be tracked and accessible
- Roles should have access to person's basic information

### **Requirement 2: Department & Section Management**
- Each department has multiple sections across different semesters
- Sections belong to specific semesters and departments
- Students enroll in sections
- Prevent duplicate student enrollments

### **Requirement 3: Examination Scheduling**
- HOD can create and schedule examinations
- Each exam has: ID, name, semester, date, venue
- Exams are assigned to sections
- Exams have assigned examiners

### **Requirement 4: Schedule Retrieval**
- **Student View**: Students see exams for their enrolled sections
- **Examiner View**: Examiners see all exams they're supervising
- **HOD View**: HOD sees complete departmental exam overview

---

## 🔧 Technical Requirements

### **Object-Oriented Design Principles**
1. ✅ Use inheritance for role hierarchy
2. ✅ Apply abstract classes where appropriate
3. ✅ Implement proper encapsulation (private fields, public properties)
4. ✅ Use read-only collections for external access
5. ✅ Implement null-safety and validation

### **Data Structures**
- Dictionary for entity lookups (O(1) access)
- List for collection management
- LINQ for querying and filtering

### **Design Patterns**
- Service layer pattern (ExamSchedulerService)
- DTO pattern for data transfer
- Role pattern for person capabilities

---

## 💡 Complete Solution

### **Solution Architecture**

The solution is organized into logical regions:
1. **Enumerations**: Semester enum
2. **Core Entities**: Person, Role hierarchy (HOD, Examiner)
3. **Academic Entities**: Department, Student, Section
4. **Examination**: Exam management
5. **Services**: Central coordinator service
6. **DTOs**: View models for different stakeholders
7. **Program**: Demonstration and testing

---

### **Implementation**

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamScheduler
{
    #region Enumerations
    /// <summary>
    /// Specifies the academic semester within a standard multi-semester program.
    /// </summary>
    /// <remarks>This enumeration is typically used to represent the progression of a student or course
    /// through up to eight sequential semesters. The values are ordered from the first semester (Sem1) to the eighth
    /// semester (Sem8).</remarks>
    public enum Semester
    {
        Sem1, Sem2, Sem3, Sem4, Sem5, Sem6, Sem7, Sem8
    }
    #endregion

    #region Core: People and Roles
    /// <summary>
    /// Represents a person with a unique identifier, name, and associated roles.
    /// </summary>
    /// <remarks>The Person class is an immutable representation of an individual, except for the collection of
    /// roles, which can be modified using the AddRole method. Instances of this class are intended to be used as part of
    /// systems that require tracking of people and their roles within an organization or application.</remarks>
    public class Person
    {
        public int PersonId { get; }
        public string Name { get; }

        private readonly List<Role> roles = new();

        public Person(int id, string name)
        {
            PersonId = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public IReadOnlyCollection<Role> Roles => roles;

        public void AddRole(Role role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            roles.Add(role);
        }
    }

    /// <summary>
    /// Represents an abstract role associated with a person. Serves as a base class for defining specific roles that
    /// can be assigned to a person.
    /// </summary>
    /// <remarks>This class provides access to the associated person's identifier and name. Derived classes
    /// should implement additional behavior or properties relevant to specific role types. A role is always linked to a
    /// valid person instance.</remarks>
    public abstract class Role
    {
        protected Person Person { get; }

        protected Role(Person person)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
            person.AddRole(this);
        }

        public int PersonId => Person.PersonId;
        public string PersonName => Person.Name;
    }

    /// <summary>
    /// Represents the head of a department, providing operations for managing departmental examinations and examiners.
    /// </summary>
    /// <remarks>The HOD class extends the Role class to encapsulate responsibilities specific to department
    /// heads, such as scheduling exams and assigning examiners. Instances of this class are associated with a specific
    /// department.</remarks>
    public class HOD : Role
    {
        public Department Department { get; }

        public HOD(Person person, Department department)
            : base(person)
        {
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }

        public void ScheduleExam(Examination exam, DateTime examDate, string venue)
        {
            if (exam == null) throw new ArgumentNullException(nameof(exam));
            exam.Schedule(examDate, venue);
        }

        public void AssignExaminer(Examination exam, Examiner examiner)
        {
            if (exam == null) throw new ArgumentNullException(nameof(exam));
            if (examiner == null) throw new ArgumentNullException(nameof(examiner));
            exam.AssignExaminer(examiner);
        }

        public void AssignExamToSection(Examination exam, Section section)
        {
            if (exam == null) throw new ArgumentNullException(nameof(exam));
            if (section == null) throw new ArgumentNullException(nameof(section));
            exam.AssignToSection(section);
        }
    }

    /// <summary>
    /// Represents an examiner who is responsible for overseeing assigned examinations.
    /// </summary>
    /// <remarks>An Examiner is associated with a Person and maintains a schedule of examinations to
    /// supervise. Use the GetSchedule method to retrieve the current list of assigned examinations.</remarks>
    public class Examiner : Role
    {
        private readonly List<Examination> assignedExams = new();

        public Examiner(Person person)
            : base(person) { }

        internal void AssignExam(Examination exam)
        {
            if (exam == null) throw new ArgumentNullException(nameof(exam));
            if (!assignedExams.Contains(exam))
            {
                assignedExams.Add(exam);
            }
        }

        public IReadOnlyCollection<Examination> GetSchedule()
        {
            return assignedExams.AsReadOnly();
        }
    }
    #endregion

    #region Academic Entities
    /// <summary>
    /// Represents a department within an organization, including its unique identifier and name.
    /// </summary>
    public class Department
    {
        public int DepartmentId { get; }
        public string DepartmentName { get; }

        public Department(int id, string name)
        {
            DepartmentId = id;
            DepartmentName = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

    /// <summary>
    /// Represents a student with a unique identifier and name.
    /// </summary>
    public class Student
    {
        public int StudentId { get; }
        public string StudentName { get; }

        public Student(int id, string name)
        {
            StudentId = id;
            StudentName = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

    /// <summary>
    /// Represents an academic section within a department for a specific semester, including its students and
    /// examinations.
    /// </summary>
    /// <remarks>A section groups students enrolled in a particular course offering for a given semester and
    /// department. Use this class to manage student membership and access associated examinations for the section.
    /// Instances are typically created with all required information at construction and are immutable with respect to
    /// their identity properties.</remarks>
    public class Section
    {
        public int SectionId { get; }
        public string SectionName { get; }
        public Semester Semester { get; }
        public Department Department { get; }

        private readonly List<Student> students = new();
        private readonly List<Examination> exams = new();

        public Section(int id, string name, Semester semester, Department department)
        {
            SectionId = id;
            SectionName = name ?? throw new ArgumentNullException(nameof(name));
            Semester = semester;
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }

        public void AddStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (!students.Any(s => s.StudentId == student.StudentId))
            {
                students.Add(student);
            }
        }

        internal void AssignExam(Examination exam)
        {
            if (exam == null) throw new ArgumentNullException(nameof(exam));
            if (!exams.Contains(exam))
            {
                exams.Add(exam);
            }
        }

        public IReadOnlyCollection<Student> Students => students.AsReadOnly();
        public IReadOnlyCollection<Examination> Examinations => exams.AsReadOnly();
    }
    #endregion

    #region Examination
    /// <summary>
    /// Represents an academic examination, including its identifying information, scheduling details, and associated
    /// examiner and section.
    /// </summary>
    /// <remarks>An Examination instance encapsulates the core details required to manage and track an exam
    /// within an academic system. It provides properties for the exam's identity, scheduling, and assignment to
    /// examiners and sections. Use the provided methods to schedule the exam and assign it to the appropriate examiner
    /// and section. Instances are typically created with the required exam information and then further configured as
    /// scheduling and assignments are determined.</remarks>
    public class Examination
    {
        public int ExamId { get; }
        public string ExamName { get; }
        public Semester Semester { get; }
        public DateTime? ExamDate { get; private set; }
        public string? Venue { get; private set; }
        public Examiner? AssignedExaminer { get; private set; }
        public Section? AssignedSection { get; private set; }

        public Examination(int id, string name, Semester semester)
        {
            ExamId = id;
            ExamName = name ?? throw new ArgumentNullException(nameof(name));
            Semester = semester;
        }

        public void Schedule(DateTime date, string venue)
        {
            ExamDate = date;
            Venue = venue ?? throw new ArgumentNullException(nameof(venue));
        }

        public void AssignExaminer(Examiner examiner)
        {
            AssignedExaminer = examiner ?? throw new ArgumentNullException(nameof(examiner));
            examiner.AssignExam(this);
        }

        public void AssignToSection(Section section)
        {
            AssignedSection = section ?? throw new ArgumentNullException(nameof(section));
            section.AssignExam(this);
        }
    }
    #endregion

    #region Services
    /// <summary>
    /// Provides services for managing departments, people, sections, students, examinations, and exam scheduling within
    /// an academic institution.
    /// </summary>
    /// <remarks>The ExamSchedulerService acts as a central coordinator for exam-related operations, including
    /// adding departments and people, assigning roles, enrolling students, scheduling exams, and retrieving schedules
    /// for students, examiners, and heads of departments (HODs). This service is intended for use in academic
    /// administration systems where exam scheduling and role management are required. Thread safety is not guaranteed;
    /// if used concurrently from multiple threads, external synchronization is required.</remarks>
    public class ExamSchedulerService
    {
        private readonly Dictionary<int, Department> departments = new();
        private readonly Dictionary<int, Person> people = new();
        private readonly Dictionary<int, HOD> hods = new();
        private readonly Dictionary<int, Examiner> examiners = new();
        private readonly Dictionary<int, Section> sections = new();
        private readonly Dictionary<int, Student> students = new();
        private readonly Dictionary<int, Examination> examinations = new();

        public Department AddDepartment(int departmentId, string departmentName)
        {
            var dept = new Department(departmentId, departmentName);
            departments[departmentId] = dept;
            return dept;
        }

        public Person AddPerson(int personId, string name)
        {
            var person = new Person(personId, name);
            people[personId] = person;
            return person;
        }

        public HOD AddHod(int personId, int departmentId)
        {
            var person = GetPerson(personId);
            var dept = GetDepartment(departmentId);
            var hod = new HOD(person, dept);
            hods[personId] = hod;
            return hod;
        }

        public Examiner AddExaminer(int personId)
        {
            var person = GetPerson(personId);
            var examiner = new Examiner(person);
            examiners[personId] = examiner;
            return examiner;
        }

        public Section AddSection(int sectionId, string name, Semester semester, int departmentId)
        {
            var dept = GetDepartment(departmentId);
            var section = new Section(sectionId, name, semester, dept);
            sections[sectionId] = section;
            return section;
        }

        public Student AddStudent(int studentId, string name)
        {
            var student = new Student(studentId, name);
            students[studentId] = student;
            return student;
        }

        public void EnrollStudentToSection(int studentId, int sectionId)
        {
            var student = GetStudent(studentId);
            var section = GetSection(sectionId);
            section.AddStudent(student);
        }

        public Examination AddExamination(int examId, string name, Semester semester)
        {
            var exam = new Examination(examId, name, semester);
            examinations[examId] = exam;
            return exam;
        }

        public void AssignExamToSection(int examId, int sectionId)
        {
            var exam = GetExamination(examId);
            var section = GetSection(sectionId);
            exam.AssignToSection(section);
        }

        public void AssignExaminerToExam(int examId, int examinerPersonId)
        {
            var exam = GetExamination(examId);
            var examiner = GetExaminer(examinerPersonId);
            exam.AssignExaminer(examiner);
        }

        public void ScheduleExam(int examId, DateTime date, string venue)
        {
            var exam = GetExamination(examId);
            exam.Schedule(date, venue);
        }

        /// <summary>
        /// Retrieves the examination schedule for a specified student, including all exams for the sections in which
        /// the student is enrolled.
        /// </summary>
        /// <param name="studentId">The unique identifier of the student whose exam schedule is to be retrieved. Must correspond to an existing
        /// student.</param>
        /// <returns>A read-only list of exam schedule items for the specified student. The list is ordered by exam date, then by
        /// exam name. Returns an empty list if the student is not enrolled in any sections or has no scheduled exams.</returns>
        public IReadOnlyList<StudentExamScheduleItem> GetStudentSchedule(int studentId)
        {
            var student = GetStudent(studentId);

            var studentSections = sections.Values.Where(s => s.Students.Any(st => st.StudentId == student.StudentId));
            var items = new List<StudentExamScheduleItem>();

            foreach (var section in studentSections)
            {
                foreach (var exam in section.Examinations)
                {
                    items.Add(new StudentExamScheduleItem
                    {
                        ExamId = exam.ExamId,
                        ExamName = exam.ExamName,
                        Semester = exam.Semester,
                        Date = exam.ExamDate,
                        Venue = exam.Venue,
                        SectionId = section.SectionId,
                        SectionName = section.SectionName,
                        DepartmentId = section.Department.DepartmentId,
                        DepartmentName = section.Department.DepartmentName
                    });
                }
            }

            return items.OrderBy(i => i.Date ?? DateTime.MaxValue).ThenBy(i => i.ExamName).ToList();
        }

        /// <summary>
        /// Retrieves the schedule of exams assigned to the specified examiner.
        /// </summary>
        /// <param name="examinerPersonId">The unique identifier of the examiner whose schedule is to be retrieved.</param>
        /// <returns>A read-only list of schedule items representing the exams assigned to the examiner. The list is ordered by
        /// exam date and exam name. Returns an empty list if the examiner has no scheduled exams.</returns>
        public IReadOnlyList<ExaminerScheduleItem> GetExaminerSchedule(int examinerPersonId)
        {
            var examiner = GetExaminer(examinerPersonId);
            var items = new List<ExaminerScheduleItem>();

            foreach (var exam in examiner.GetSchedule())
            {
                items.Add(new ExaminerScheduleItem
                {
                    ExamId = exam.ExamId,
                    ExamName = exam.ExamName,
                    Semester = exam.Semester,
                    Date = exam.ExamDate,
                    Venue = exam.Venue,
                    SectionId = exam.AssignedSection?.SectionId,
                    SectionName = exam.AssignedSection?.SectionName,
                    DepartmentId = exam.AssignedSection?.Department.DepartmentId,
                    DepartmentName = exam.AssignedSection?.Department.DepartmentName
                });
            }

            return items.OrderBy(i => i.Date ?? DateTime.MaxValue).ThenBy(i => i.ExamName).ToList();
        }

        /// <summary>
        /// Retrieves an overview of all examinations for the department managed by the specified Head of Department
        /// (HOD).
        /// </summary>
        /// <remarks>Each overview item contains details about the examination, including its name,
        /// semester, date, venue, assigned examiner, and section information. If the HOD manages a department with no
        /// examinations, the returned list will be empty.</remarks>
        /// <param name="hodPersonId">The unique identifier of the Head of Department whose department's examinations are to be retrieved. Must
        /// correspond to a valid HOD.</param>
        /// <returns>A read-only list of overview items, each representing an examination associated with the HOD's department.
        /// The list is ordered by semester and examination date.</returns>
        public IReadOnlyList<HodExamOverviewItem> GetHodExamOverview(int hodPersonId)
        {
            var hod = GetHod(hodPersonId);
            var deptId = hod.Department.DepartmentId;

            var deptSections = sections.Values.Where(s => s.Department.DepartmentId == deptId);
            var items = new List<HodExamOverviewItem>();

            foreach (var section in deptSections)
            {
                foreach (var exam in section.Examinations)
                {
                    items.Add(new HodExamOverviewItem
                    {
                        ExamId = exam.ExamId,
                        ExamName = exam.ExamName,
                        Semester = exam.Semester,
                        Date = exam.ExamDate,
                        Venue = exam.Venue,
                        ExaminerPersonId = exam.AssignedExaminer?.PersonId,
                        ExaminerName = exam.AssignedExaminer?.PersonName,
                        SectionId = section.SectionId,
                        SectionName = section.SectionName
                    });
                }
            }

            return items.OrderBy(i => i.Semester).ThenBy(i => i.Date ?? DateTime.MaxValue).ToList();
        }

        private Department GetDepartment(int departmentId)
        {
            if (!departments.TryGetValue(departmentId, out var dept))
            {
                throw new KeyNotFoundException($"Department {departmentId} not found.");
            }
            return dept;
        }

        private Person GetPerson(int personId)
        {
            if (!people.TryGetValue(personId, out var person))
            {
                throw new KeyNotFoundException($"Person {personId} not found.");
            }
            return person;
        }

        private HOD GetHod(int personId)
        {
            if (!hods.TryGetValue(personId, out var hod))
            {
                throw new KeyNotFoundException($"HOD person {personId} not found.");
            }
            return hod;
        }

        private Examiner GetExaminer(int personId)
        {
            if (!examiners.TryGetValue(personId, out var examiner))
            {
                throw new KeyNotFoundException($"Examiner person {personId} not found.");
            }
            return examiner;
        }

        private Section GetSection(int sectionId)
        {
            if (!sections.TryGetValue(sectionId, out var section))
            {
                throw new KeyNotFoundException($"Section {sectionId} not found.");
            }
            return section;
        }

        private Student GetStudent(int studentId)
        {
            if (!students.TryGetValue(studentId, out var student))
            {
                throw new KeyNotFoundException($"Student {studentId} not found.");
            }
            return student;
        }

        private Examination GetExamination(int examId)
        {
            if (!examinations.TryGetValue(examId, out var exam))
            {
                throw new KeyNotFoundException($"Examination {examId} not found.");
            }
            return exam;
        }
    }
    #endregion

    #region DTOs
    /// <summary>
    /// Represents a scheduled exam for a student, including exam details, timing, and associated academic information.
    /// </summary>
    /// <remarks>This class is typically used to display or manage a student's exam timetable within an
    /// academic system. It includes information about the exam, the semester, the scheduled date and venue, as well as
    /// the section and department to which the exam belongs.</remarks>
    public class StudentExamScheduleItem
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public DateTime? Date { get; set; }
        public string? Venue { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a scheduled exam assignment for an examiner, including exam details, timing, and associated
    /// organizational information.
    /// </summary>
    /// <remarks>This class is typically used to convey scheduling information for examiners, such as which
    /// exam they are assigned to, the scheduled date and venue, and related section or department details. All
    /// properties are intended for data transfer and may be null if the information is not applicable or
    /// unavailable.</remarks>
    public class ExaminerScheduleItem
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public DateTime? Date { get; set; }
        public string? Venue { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }

    /// <summary>
    /// Represents an overview of an exam for Head of Department (HOD) review, including exam details, scheduling, and
    /// examiner information.
    /// </summary>
    public class HodExamOverviewItem
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public DateTime? Date { get; set; }
        public string? Venue { get; set; }
        public int? ExaminerPersonId { get; set; }
        public string? ExaminerName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
    }
    #endregion
}
    #endregion
}
```

---

## 🔍 Key Concepts Demonstrated

### **1. Abstract Classes and Inheritance**
```csharp
public abstract class Role
{
    protected Person Person { get; }
    
    protected Role(Person person)
    {
        Person = person ?? throw new ArgumentNullException(nameof(person));
        person.AddRole(this); // Self-registration pattern
    }
}
```

**Why Abstract?**
- Role cannot exist independently; it must be HOD, Examiner, etc.
- Provides common functionality to all derived roles
- Forces derived classes to be specific role types

### **2. Encapsulation with Read-Only Collections**
```csharp
private readonly List<Role> roles = new();
public IReadOnlyCollection<Role> Roles => roles;
```

**Benefits:**
- External code cannot modify the internal collection
- Maintains data integrity
- Controlled access through methods like AddRole()

### **3. Bidirectional Relationships**
```csharp
public void AssignExaminer(Examiner examiner)
{
    AssignedExaminer = examiner ?? throw new ArgumentNullException(nameof(examiner));
    examiner.AssignExam(this); // Two-way linking
}
```

### **4. Service Layer Pattern**
The `ExamSchedulerService` acts as a facade, simplifying complex operations and managing all entities centrally.

### **5. LINQ for Data Querying**
```csharp
return items.OrderBy(i => i.Date ?? DateTime.MaxValue)
           .ThenBy(i => i.ExamName)
           .ToList();
```

---

## 🧪 Testing & Validation

### **Test Scenario Implementation**

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamScheduler
{
    public class Program
    {
        #region Plan and Pseudocode
        // Plan:
        // - Initialize department, sections, and students.
        // - Initialize people and assign roles (HOD and Examiners). Avoid duplicate AddRole calls since Role adds itself.
        // - Create examinations for semesters.
        // - Schedule exams with date and venue.
        // - Assign examiners to exams.
        // - Assign exams to relevant sections.
        // - Output a simple system summary.
        // - Add schedule views:
        //   - Student view: pick a specific student, find their section, list exams assigned to that section with date, venue, and examiner.
        //   - Examiner view: pick a specific examiner, list exams they are assigned with date, venue, and section(s) the exam is assigned to.
        //   - HOD view: show overview of all scheduled exams with semester, date, venue, examiner, and sections assigned.
        // - Implement minimal helper functions inside Program to format and print schedules without changing domain classes.

        // Pseudocode:
        // - Main():
        //   #region Initialization
        //     - Create Department
        //     - Create Sections for semesters
        //     - Populate Students per section
        //     - Create Persons and derive HOD/Examiners
        //     - Create Examinations
        //   #endregion
        //   #region Scheduling
        //     - HOD schedules exams (date, venue)
        //     - HOD assigns examiners
        //   #endregion
        //   #region Assignment
        //     - HOD assigns exams to sections
        //   #endregion
        //   #region Views
        //     - Student view -> PrintStudentSchedule
        //     - Examiner view -> PrintExaminerSchedule
        //     - HOD overview -> PrintHodOverview
        //   #endregion
        // - Helper methods:
        //   #region Helpers
        //     - PrintStudentSchedule
        //     - PrintExaminerSchedule
        //     - PrintHodOverview
        //   #endregion
        #endregion

        static void Main()
        {
            #region Initialization

            Department cse = new Department(1, "Computer Science");

            Section sem1Section = new Section(1, "CSE-Sem1-A", Semester.Sem1, cse);
            Section sem2Section = new Section(2, "CSE-Sem2-A", Semester.Sem2, cse);

            for (int i = 1; i <= 5; i++)
            {
                sem1Section.AddStudent(new Student(i, $"Student-S1-{i}"));
            }

            for (int i = 6; i <= 10; i++)
            {
                sem2Section.AddStudent(new Student(i, $"Student-S2-{i - 5}"));
            }

            Person hodPerson = new Person(1, "Dr. Rao");

            Person p1 = new Person(2, "Examiner-1");
            Person p2 = new Person(3, "Examiner-2");
            Person p3 = new Person(4, "Examiner-3");
            Person p4 = new Person(5, "Examiner-4");
            Person p5 = new Person(6, "Examiner-5");

            HOD hod = new HOD(hodPerson, cse);

            Examiner e1 = new Examiner(p1);
            Examiner e2 = new Examiner(p2);
            Examiner e3 = new Examiner(p3);
            Examiner e4 = new Examiner(p4);
            Examiner e5 = new Examiner(p5);

            Examination exam1 = new Examination(101, "Maths", Semester.Sem1);
            Examination exam2 = new Examination(102, "Programming", Semester.Sem1);
            Examination exam3 = new Examination(103, "DSA", Semester.Sem2);
            Examination exam4 = new Examination(104, "DBMS", Semester.Sem2);
            Examination exam5 = new Examination(105, "Operating Systems", Semester.Sem2);

            #endregion

            #region Scheduling

            hod.ScheduleExam(exam1, new DateTime(2025, 3, 10), "Hall A");
            hod.ScheduleExam(exam2, new DateTime(2025, 3, 12), "Hall B");
            hod.ScheduleExam(exam3, new DateTime(2025, 4, 5), "Hall C");
            hod.ScheduleExam(exam4, new DateTime(2025, 4, 8), "Hall D");
            hod.ScheduleExam(exam5, new DateTime(2025, 4, 12), "Hall E");

            hod.AssignExaminer(exam1, e1);
            hod.AssignExaminer(exam2, e2);
            hod.AssignExaminer(exam3, e3);
            hod.AssignExaminer(exam4, e4);
            hod.AssignExaminer(exam5, e5);

            #endregion

            #region Assignment

            // =========================
            // ASSIGN EXAMS TO SECTIONS
            // =========================
            hod.AssignExamToSection(exam1, sem1Section);
            hod.AssignExamToSection(exam2, sem1Section);

            hod.AssignExamToSection(exam3, sem2Section);
            hod.AssignExamToSection(exam4, sem2Section);
            hod.AssignExamToSection(exam5, sem2Section);

            #endregion

            #region Views

            // =========================
            // VIEW SCHEDULES
            // =========================

            // Student view: pick Student-S1-3 from Sem1 section
            Student targetStudent = sem1Section.Students.FirstOrDefault(s => s.StudentName == "Student-S1-3");
            PrintStudentSchedule(targetStudent, new[] { sem1Section, sem2Section });

            // Examiner view: show schedule for Examiner-3 (assigned to DSA)
            PrintExaminerSchedule(e3, new[] { exam1, exam2, exam3, exam4, exam5 }, new[] { sem1Section, sem2Section });

            // HOD view: show overview of all exams
            PrintHodOverview(hod, new[] { exam1, exam2, exam3, exam4, exam5 }, new[] { sem1Section, sem2Section });

            #endregion
        }

        #region Helpers

        private static void PrintStudentSchedule(Student student, IEnumerable<Section> allSections)
        {
            if (student == null)
            {
                Console.WriteLine("Student schedule: No student selected.");
                return;
            }

            Section section = allSections.FirstOrDefault(sec => sec.Students.Any(s => s.StudentId == student.StudentId));
            Console.WriteLine($"--- Student Schedule ---");
            Console.WriteLine($"Student: {student.StudentName} (Id: {student.StudentId})");
            Console.WriteLine($"Section: {section?.SectionName ?? "N/A"}");

            if (section == null)
            {
                Console.WriteLine("No section found for the student.");
                Console.WriteLine();
                return;
            }

            var exams = section.Examinations;
            if (exams == null || !exams.Any())
            {
                Console.WriteLine("No exams scheduled for this section.");
                Console.WriteLine();
                return;
            }

            foreach (var exam in exams.OrderBy(e => e.ExamDate ?? DateTime.MaxValue))
            {
                string examinerName = exam.AssignedExaminer?.PersonName ?? "Unassigned";
                Console.WriteLine($"{exam.ExamName} | {exam.Semester} | {(exam.ExamDate.HasValue ? exam.ExamDate.Value.ToString("yyyy-MM-dd") : "TBD")} | Venue: {exam.Venue ?? "TBD"} | Examiner: {examinerName}");
            }

            Console.WriteLine();
        }

        private static void PrintExaminerSchedule(Examiner examiner, IEnumerable<Examination> allExams, IEnumerable<Section> allSections)
        {
            if (examiner == null)
            {
                Console.WriteLine("Examiner schedule: No examiner selected.");
                return;
            }

            Console.WriteLine($"--- Examiner Schedule ---");
            Console.WriteLine($"Examiner: {examiner.PersonName}");

            var exams = allExams.Where(e => e.AssignedExaminer?.PersonId == examiner.PersonId)
                                .OrderBy(e => e.ExamDate ?? DateTime.MaxValue)
                                .ToList();

            if (!exams.Any())
            {
                Console.WriteLine("No exams assigned.");
                Console.WriteLine();
                return;
            }

            foreach (var exam in exams)
            {
                var assignedSections = allSections.Where(s => s.Examinations.Any(se => se.ExamId == exam.ExamId))
                                                  .Select(s => s.SectionName)
                                                  .ToArray();
                string sections = assignedSections.Length > 0 ? string.Join(", ", assignedSections) : "No section assigned";
                Console.WriteLine($"{exam.ExamName} | {exam.Semester} | {(exam.ExamDate.HasValue ? exam.ExamDate.Value.ToString("yyyy-MM-dd") : "TBD")} | Venue: {exam.Venue ?? "TBD"} | Sections: {sections}");
            }

            Console.WriteLine();
        }

        private static void PrintHodOverview(HOD hod, IEnumerable<Examination> allExams, IEnumerable<Section> allSections)
        {
            Console.WriteLine($"--- HOD Overview ---");
            Console.WriteLine($"HOD: {hod.PersonName} | Department: {hod.Department.DepartmentName}");

            var exams = allExams.OrderBy(e => e.ExamDate ?? DateTime.MaxValue).ToList();
            if (!exams.Any())
            {
                Console.WriteLine("No exams scheduled.");
                Console.WriteLine();
                return;
            }

            foreach (var exam in exams)
            {
                string examinerName = exam.AssignedExaminer?.PersonName ?? "Unassigned";
                var assignedSections = allSections.Where(s => s.Examinations.Any(se => se.ExamId == exam.ExamId))
                                                  .Select(s => s.SectionName)
                                                  .ToArray();
                string sections = assignedSections.Length > 0 ? string.Join(", ", assignedSections) : "No section assigned";

                Console.WriteLine($"{exam.ExamName} | {exam.Semester} | {(exam.ExamDate.HasValue ? exam.ExamDate.Value.ToString("yyyy-MM-dd") : "TBD")} | Venue: {exam.Venue ?? "TBD"} | Examiner: {examinerName} | Sections: {sections}");
            }

            Console.WriteLine();
        }

        #endregion
    }
}
```

### **Expected Output**

```
--- Student Schedule ---
Student: Student-S1-3 (Id: 3)
Section: CSE-Sem1-A
Maths | Sem1 | 2025-03-10 | Venue: Hall A | Examiner: Examiner-1
Programming | Sem1 | 2025-03-12 | Venue: Hall B | Examiner: Examiner-2

--- Examiner Schedule ---
Examiner: Examiner-3
DSA | Sem2 | 2025-04-05 | Venue: Hall C | Sections: CSE-Sem2-A

--- HOD Overview ---
HOD: Dr. Rao | Department: Computer Science
Maths | Sem1 | 2025-03-10 | Venue: Hall A | Examiner: Examiner-1 | Sections: CSE-Sem1-A
Programming | Sem1 | 2025-03-12 | Venue: Hall B | Examiner: Examiner-2 | Sections: CSE-Sem1-A
DSA | Sem2 | 2025-04-05 | Venue: Hall C | Examiner: Examiner-3 | Sections: CSE-Sem2-A
DBMS | Sem2 | 2025-04-08 | Venue: Hall D | Examiner: Examiner-4 | Sections: CSE-Sem2-A
Operating Systems | Sem2 | 2025-04-12 | Venue: Hall E | Examiner: Examiner-5 | Sections: CSE-Sem2-A
```

---

## 📊 Analysis & Best Practices

### **Design Decisions**

| Decision | Rationale |
|----------|-----------|
| Abstract Role class | Prevents instantiation of generic roles; enforces specific role types |
| Self-registration in Role constructor | Ensures bidirectional relationship consistency |
| Dictionary in Service layer | Provides O(1) lookup for entities by ID |
| Read-only collections | Protects internal state while allowing safe access |
| DTOs for schedules | Separates domain logic from presentation concerns |

### **Error Handling**
- ArgumentNullException for null parameters
- KeyNotFoundException for missing entities
- Validation in constructors and methods

### **Performance Considerations**
- Dictionary lookups: O(1) time complexity
- LINQ deferred execution
- AsReadOnly() for collection wrapping (no copying)

---

## 🎓 Extension Exercises

### **Level 1: Basic Enhancements**
1. Add a `RemoveStudentFromSection` method
2. Implement exam cancellation functionality
3. Add validation to prevent scheduling conflicts

### **Level 2: Intermediate Features**
1. Add exam results tracking (marks/grades)
2. Implement section capacity limits
3. Add examiner workload balancing

### **Level 3: Advanced Challenges**
1. Implement conflict detection (examiner/venue/student)
2. Add notification system for schedule changes
3. Implement reporting with various filters and groupings
4. Add persistence layer (file/database storage)

---

## 📚 Related Concepts to Study

- **Design Patterns**: Factory, Repository, Observer
- **SOLID Principles**: Especially Single Responsibility and Open/Closed
- **Advanced LINQ**: GroupBy, Aggregate, SelectMany
- **Async Programming**: For database operations
- **Unit Testing**: Mock objects, test fixtures
- **Dependency Injection**: For better testability

---

## ✅ Assessment Checklist

- [ ] All classes properly encapsulated
- [ ] Abstract class used appropriately
- [ ] Collections are read-only from external access
- [ ] Null validation implemented
- [ ] LINQ queries are efficient
- [ ] Code is well-documented
- [ ] No duplicate enrollments/assignments
- [ ] Schedule outputs are correctly formatted
- [ ] Bidirectional relationships maintained

---

## 📖 Summary

This scenario demonstrates a **real-world exam management system** incorporating:
- Advanced OOP concepts (inheritance, abstraction, encapsulation)
- Role-based access patterns
- Service layer architecture
- Data transfer objects for view separation
- Collection management with LINQ
- Proper error handling and validation

The solution showcases **production-ready code** with comprehensive documentation, proper naming conventions, and scalable architecture suitable for enterprise applications.

---

**End of Document**