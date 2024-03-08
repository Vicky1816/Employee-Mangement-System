namespace Employee_Managment_System.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }  // Guid-->Unique identifier

        public string Name { get; set; }

        public string Email { get; set; }

        public long Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Department { get; set; }
    }
}
