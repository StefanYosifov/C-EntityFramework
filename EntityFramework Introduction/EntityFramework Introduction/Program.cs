
using EntityFramework_Introduction.Data;
using EntityFramework_Introduction.Models;
using Microsoft.EntityFrameworkCore;

using SoftUniContext context = new SoftUniContext();
//DateTime oldEmployees = new DateTime(2000, 1, 1);


//List<Employee> employees= await 
//    context.Employees.
//    Where(e=>e.HireDate<oldEmployees).
//    ToListAsync();

 

//foreach(var employee in employees)
//{
//    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Salary} {employee.HireDate} {employee.JobTitle}");
//}


//Employee? foundEmployee = await context.Employees.FindAsync(30);
//Console.WriteLine($"{foundEmployee.FirstName} {foundEmployee.LastName}");


var richestEmployee=await context.Employees.
    AsNoTracking().
    Select(e=>new Employee 
    { 
    FirstName=e.FirstName,
    LastName=e.LastName,
    Salary=e.Salary
    }).
    OrderByDescending(e=>e.Salary).FirstOrDefaultAsync();

Console.WriteLine($"{richestEmployee.FirstName} {richestEmployee.Salary}");



int pages = 10;
for(int i = 0; i < pages; i++)
{
    var employees = await context.Employees.AsNoTracking().
                  OrderBy(e => e.FirstName).
                  ThenBy(e => e.LastName).
                  Select(e => new
                  {
                      FirstName = e.FirstName,
                      LastName = e.LastName,
                      Salary = e.Salary
                  }).Skip(i * pages)
                    .Take(pages).ToListAsync();



    foreach(var employee in employees)
    {
        Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Salary}");
    }


    Console.ReadLine();
}