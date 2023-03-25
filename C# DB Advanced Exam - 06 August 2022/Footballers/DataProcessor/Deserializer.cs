namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        private const string FormatDateTime
            = "dd/mm/yyyy";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCoachDto[]), new XmlRootAttribute("Coaches"));

            using StringReader reader = new StringReader(xmlString);
            ImportCoachDto[] coachDtos = (ImportCoachDto[])xmlSerializer.Deserialize(reader);

            ICollection<Coach> coaches = new HashSet<Coach>();

            foreach (var coachDto in coachDtos)
            {
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!IsValid(coachDto.Nationality))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach coach = new Coach()
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality,
                };

                foreach (var footballer in coachDto.ImportFootballerDtos)
                {
                    if (!IsValid(footballer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(footballer.Name))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime footballerStartDate;
                    bool isFootballerStartDateValid =
                        DateTime.TryParseExact
                        (footballer.ContractStartDate, FormatDateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out footballerStartDate);

                    if (isFootballerStartDateValid == false)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime fotballerEndDate;
                    bool isFootballerEndDateValid =
                        DateTime.TryParseExact(footballer.ContractEndDate, FormatDateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out fotballerEndDate);

                    if (isFootballerEndDateValid == false)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (fotballerEndDate <= footballerStartDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer player = new Footballer()
                    {
                        Name = footballer.Name,
                        ContractStartDate = footballerStartDate,
                        ContractEndDate = fotballerEndDate,
                        BestSkillType = (BestSkillType)footballer.BestSkillType,
                        PositionType = (PositionType)footballer.PositionType
                    };

                    coach.Footballers.Add(player);
                }
                coaches.Add(coach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }
            context.Coaches.AddRange(coaches);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {

            StringBuilder sb = new StringBuilder();
            ImportTeamDto[] teamDtos = JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString);
            HashSet<Team> teams = new HashSet<Team>();


            foreach (var team in teamDtos)
            {
                if (!IsValid(team))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (string.IsNullOrEmpty(team.Name) || string.IsNullOrEmpty(team.Nationality))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (team.Trophies <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Team validTeam = new Team()
                {
                    Name = team.Name,
                    Nationality = team.Nationality,
                    Trophies = team.Trophies,
                };

                foreach (int footballerId in team.Footballers.Distinct())
                {
                    Footballer footballerLegit = context.Footballers.FirstOrDefault(f=>f.Id==footballerId);
                    if (footballerLegit == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    TeamFootballer teamFootballer = new TeamFootballer()
                    {
                        Footballer = footballerLegit,
                    };
                    validTeam.TeamsFootballers.Add(teamFootballer);
                }
                teams.Add(validTeam);
                sb.AppendLine(string.Format(SuccessfullyImportedTeam, validTeam.Name, validTeam.TeamsFootballers.Count));
            }

            context.Teams.AddRange(teams);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
