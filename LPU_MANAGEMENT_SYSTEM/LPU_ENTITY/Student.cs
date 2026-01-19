namespace LPU_ENTITY
{
    //Advantage of enum --> student can assign courses among them only


    public enum CourseType
    {
        Mechanical=12,
        Electrical=13,
        Civil=14,
        CSE=15,
        IT=16
    }

    public class Student
    {
        //Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType Course { get; set; } //enum type property
        public string Address { get; set; }
    }

}
