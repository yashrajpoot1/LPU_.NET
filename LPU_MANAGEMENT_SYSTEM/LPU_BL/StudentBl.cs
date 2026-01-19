using LPU_Common;
using LPU_DAL;
using LPU_ENTITY;
namespace LPU_BL
{
    public class StudentBl : IStudentCRUD
    {
        StudentDAO studentDAO =null;
        public StudentBl()
        {
            studentDAO=new StudentDAO();
        }

        public bool DropStudentDetails(int id)
        {
            throw new NotImplementedException();
        }

        public bool EnrollStudent(Student sObj)
        {
            throw new NotImplementedException();
        }
        public Student SearchStudentByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStudentDetails(int id, Student newObj)
        {
            throw new NotImplementedException();
        }

        public Student SearchStudentById(int id)
        {
            Student s1= studentDAO.SearchStudentById(id);
            return s1;
        }
    }
}
