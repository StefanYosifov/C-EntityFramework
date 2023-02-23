namespace SoftUni;
using SoftUni.Data;
using SoftUni.Models;
using System.Text;

public class StartUp
{
        static void Main(string[] args)
        {

            SoftUniContext dbContext = new SoftUniContext();
            Console.WriteLine(GetEmployeesFullInformation(dbContext));
        }

    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        StringBuilder result = new StringBuilder();
        Employee[] employees = context.Employees.OrderBy(e=>e.EmployeeId).ToArray();
        
        foreach(var employee in employees)
        {
            result.AppendLine($"{employee.FirstName} {employee.MiddleName} {employee.LastName} {employee.JobTitle} {employee.Salary:F2}");
        }

        return result.ToString().Trim();
    }
}
