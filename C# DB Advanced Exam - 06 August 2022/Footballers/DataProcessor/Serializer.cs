namespace Footballers.DataProcessor
{
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            throw new NotImplementedException();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var top5Teams = context.Teams.Where(f => f.TeamsFootballers.Any(tf => tf.Footballer.ContractStartDate >= date))
                .ToArray()
                .OrderByDescending(f => f.TeamsFootballers.Count)
                .ThenBy(f => f.Name)
                .Select(t => new
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers
                    .Where(t => t.Footballer.ContractStartDate >= date)
                    .OrderByDescending(f => f.Footballer.ContractStartDate)
                    .ThenBy(f => f.Footballer.ContractEndDate)
                    .Select(tf => new
                    {
                        FootballerName = tf.Footballer.Name,
                        ContractStartDate = tf.Footballer.ContractStartDate.ToString("d"),
                        ContractEndDate = tf.Footballer.ContractEndDate.ToString("d"),
                        BestSkillType = tf.Footballer.BestSkillType,
                        PositionType = tf.Footballer.PositionType,
                    })
                    .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Count())
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();


            return JsonConvert.SerializeObject(top5Teams, Formatting.Indented);

        }
    }
}
