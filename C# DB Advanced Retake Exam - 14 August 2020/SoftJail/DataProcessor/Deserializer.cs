namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto.Department;
    using SoftJail.DataProcessor.ImportDto.Prisoners;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        private const string SuccessfulMessageDepartment = "Imported {0} with {1} cells";

        private const string SuccessfulMessagePrisoner =
            "Imported {0} {1} years old";


        private const string SuccessfulMessageOfficer = "Imported {0} ({1} prisoners)";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var departmentsWithCells = JsonConvert
                .DeserializeObject<IEnumerable<ImportDepartmentAndCellDto>>(jsonString);

            var validDepartments = new List<Department>();

            foreach (var departmentWithCells in departmentsWithCells)
            {
                if (!IsValid(departmentWithCells))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var department = new Department()
                {
                    Name = departmentWithCells.Name
                };

                bool departmenthasInvalidCell = false;

                foreach (var cell in departmentWithCells.Cells)
                {
                    if (!IsValid(cell))
                    {
                        sb.AppendLine(ErrorMessage);
                        departmenthasInvalidCell = true;
                        break;
                    }
                    else
                        department.Cells.Add(new Cell
                        {
                            CellNumber = cell.CellNumber,
                            HasWindow = cell.HasWindow
                        });
                }

                if (department.Cells.Count == 0)
                    continue;

                if (departmenthasInvalidCell)
                    continue;

                validDepartments.Add(department);
                sb.AppendLine(string.Format(SuccessfulMessageDepartment, department.Name, department.Cells.Count));

            }

            context.AddRange(validDepartments);
            context.SaveChanges();


            return sb.ToString();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<ImportPrisonerMailDto> prisonerMailDtos =
                JsonConvert.DeserializeObject<IEnumerable<ImportPrisonerMailDto>>(jsonString);

            ICollection<Prisoner> validPrisoners = new HashSet<Prisoner>();

            foreach (var dto in prisonerMailDtos)
            {

                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (dto.Mails.Any(mail => !IsValid(mail)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                var validPrisoner = Mapper.Map<Prisoner>(dto);
                ICollection<Mail> validMails = new HashSet<Mail>();
                foreach (var mail in dto.Mails)
                {
                    if (!IsValid(mail))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    var validMail = Mapper.Map<Mail>(mail);
                    validMails.Add(validMail);
                }
                validPrisoner.Mails.Add(validMails);
             
            }
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}