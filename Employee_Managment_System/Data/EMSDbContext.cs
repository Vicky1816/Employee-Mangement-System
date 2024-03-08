using Employee_Managment_System.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment_System.Data
{
    public class EMSDbContext : DbContext
    {
        public EMSDbContext(DbContextOptions options) : base(options)
        {
        }


        //create a property- it used to access the tables that entity framework will create in our database
        public DbSet<Employee> Employees { get; set; } //Employees --> same name used in database
    }
}
