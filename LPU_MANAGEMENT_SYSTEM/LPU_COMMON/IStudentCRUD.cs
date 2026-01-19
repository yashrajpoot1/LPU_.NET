
using LPU_ENTITY;
using LPU_EXCEPTION;

namespace LPU_Common
{
    public interface IStudentCRUD
    {

        Student SearchStudentByName(string name);
        Student SearchStudentById(int id);

        bool EnrollStudent(Student sObj);

        bool UpdateStudentDetails(int id, Student newObj);

        bool DropStudentDetails(int id);
    }
}