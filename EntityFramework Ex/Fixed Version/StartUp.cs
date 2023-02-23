namespace SoftUni
{
    using SoftUni.Data;
    using SoftUni.Models;
    using System.Text;

    public class StartUp
    {
        static void Main(string[] args)
        {
            using SoftUniContext context = new SoftUniContext();
            string thirdProblem = GetEmployeesFullInformation(context);
            Console.WriteLine(thirdProblem);
            Console.WriteLine('\n');

            string fourthProblem = GetEmployeesWithSalaryOver50000(context);
            Console.WriteLine(fourthProblem);
            Console.WriteLine('\n');

            string fifthProblem = GetEmployeesFromResearchAndDevelopment(context);
            Console.WriteLine(fifthProblem);
            Console.WriteLine('\n');

        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder output=new StringBuilder();
            var employees=context.Employees.OrderBy(x=>x.EmployeeId).Select(e=>new
            {
                e.FirstName,
                e.MiddleName,
                e.LastName,
                e.JobTitle,
                e.Salary,
            }).ToList();

            foreach(var e in employees)
            {
                output.AppendLine($"{e.FirstName} {e.MiddleName} {e.LastName} {e.JobTitle} {e.Salary:F2}");
            }
            return output.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            int minimumSalary = 50000;
            StringBuilder output = new StringBuilder();
            var employees = context.Employees.Select(e => new
            {
                e.FirstName,
                e.Salary
            }).Where(e => e.Salary > minimumSalary).OrderBy(e=>e.FirstName).ToList();

            foreach(var e in employees)
            {
                output.AppendLine($"{e.FirstName} - {e.Salary:F2}");
            }

          return  output.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder output = new StringBuilder();
            string departmentQuota = "Research and Development";

            var employees = context.Employees.Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.Department,
                e.Salary
            }).Where(e => e.Department.Name == departmentQuota)
              .OrderBy(e=>e.Salary).ThenByDescending(e=>e.FirstName);

            foreach(var e in employees)
            {
                output.AppendLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:F2}");
            }

            return output.ToString().TrimEnd();
        }
    }
}