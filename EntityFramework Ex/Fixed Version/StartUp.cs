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

            string sixthProblem = AddNewAddressToEmployee(context);
            Console.WriteLine(sixthProblem);
            Console.WriteLine('\n');


            string seventhProblem = GetEmployeesInPeriod2(context);
            Console.WriteLine(seventhProblem);
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

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder output = new StringBuilder();

            Address newAddress=new Address();
            newAddress.AddressText= "Vitoshka 15";
            newAddress.TownId = 4;

            string employeeNameToFind = "Nakov";
            Employee? findEmployee = context.Employees.FirstOrDefault(e => e.LastName == employeeNameToFind);
            if (findEmployee == null)
            {
                return "Cannot find the employee";
            }
            findEmployee.Address = newAddress;
            context.SaveChanges();


            var employeesAddress = context.Employees.Select(e => new
            {
                e.AddressId,
                e.Address
            }).OrderByDescending(e=>e.AddressId).Take(10).ToList();


            foreach(var e in employeesAddress)
            {
                output.AppendLine($"{e.Address.AddressText}");
            }


            return output.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder output = new StringBuilder();

            DateTime minDate = new DateTime(2001, 1, 1);
            DateTime maxDate = new DateTime(2003, 12, 31);


            var employees = context.Employees.
                Where(e => e.EmployeesProjects.Any
                (ep => ep.Project.StartDate.Year >= minDate.Year && ep.Project.StartDate.Year <= minDate.Year))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects
                    .Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt"),
                        EndDate = ep.Project.EndDate.HasValue ?
                        ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished"
                    }).ToList()
                }).Take(10)
                .ToList();


            foreach (var employee in employees)
            {
                output.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.FirstName} {employee.LastName}");

                foreach (var project in employee.Projects)
                {
                    var startDate = project.StartDate;
                    var endDate=project.EndDate;

                    output.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");
                }
            }


            return output.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod2(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        ProjectStartDate = ep.Project.StartDate,
                        ProjectEndDate = ep.Project.EndDate
                    })
                }).Take(10);

            StringBuilder employeeManagerResult = new StringBuilder();

            foreach (var employee in employees)
            {
                employeeManagerResult.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    var startDate = project.ProjectStartDate.ToString("M/d/yyyy h:mm:ss tt");
                    var endDate = project.ProjectEndDate.HasValue ? project.ProjectEndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished";

                    employeeManagerResult.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");
                }
            }
            return employeeManagerResult.ToString().TrimEnd();
        }
    }
}
    
