using Employee_Managment_System.Data;
using Employee_Managment_System.Models;
using Employee_Managment_System.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment_System.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EMSDbContext emsDbContext;

        public EmployeesController(EMSDbContext emsDbContext)
        {
            this.emsDbContext = emsDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await emsDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };
    
            await emsDbContext.Employees.AddAsync(employee); 
            await emsDbContext.SaveChangesAsync();
            return RedirectToAction("Index");  
        }

        [HttpGet]   
        public async Task<IActionResult> View(Guid id)
        {
            var employee =  await emsDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel model)
        {
            var employee = await emsDbContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await emsDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await emsDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                emsDbContext.Employees.Remove(employee);
                await emsDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");   
        }
    }
}
