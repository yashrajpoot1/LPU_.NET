using System;
using System.Collections.Generic;
using LPU_Common;
using LPU_ENTITY;
using LPU_EXCEPTION;

namespace LPU_DAL
{
    public class StudentDAO : IStudentCRUD
    {
        static List<Student> studentList = null;

        public StudentDAO()
        {
            if (studentList == null)
            {
                studentList = new List<Student>()
                {
                    new Student()
                    {
                        Id = 12210582,
                        Name = "Mahesh Singh",
                        Course = CourseType.CSE,
                        Address = "Ludhiana"
                    }
                };
            }
        }

        public bool EnrollStudent(Student sObj)
        {
            if (sObj == null)
                throw new Lpu_Exception("Student object is null");

            studentList.Add(sObj);
            return true;
        }

        public bool DropStudentDetails(int id)
        {
            if (id == 0)
                throw new Lpu_Exception("Invalid Student Id");

            Student student = studentList.Find(x => x.Id == id);
            if (student == null)
                throw new Lpu_Exception("Student not found");

            studentList.Remove(student);
            return true;
        }

        public Student SearchStudentById(int id)
        {
            if (id == 0)
                throw new Lpu_Exception("Invalid Student Id");

            Student student = studentList.Find(x => x.Id == id);
            if (student == null)
                throw new Lpu_Exception("Student not found");

            return student;
        }

        public Student SearchStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Lpu_Exception("Student name is required");

            Student student = studentList.Find(
                x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
            );

            if (student == null)
                throw new Lpu_Exception("Student not found");

            return student;
        }

        public bool UpdateStudentDetails(int id, Student newObj)
        {
            if (id == 0 || newObj == null)
                throw new Lpu_Exception("Invalid input");

            Student student = studentList.Find(x => x.Id == id);
            if (student == null)
                throw new Lpu_Exception("Student not found");

            student.Name = newObj.Name;
            student.Course = newObj.Course;
            student.Address = newObj.Address;

            return true;
        }
    }
}
