
using EntityFramework_Introduction.Data;
using EntityFramework_Introduction.Models;
using Microsoft.EntityFrameworkCore;

using SoftUniContext context = new SoftUniContext();

Console.WriteLine(context.Employees
    .Where(e => e.ManagerId == 185)
    .Count(e => e.FirstName.Length > 2));
    


   
