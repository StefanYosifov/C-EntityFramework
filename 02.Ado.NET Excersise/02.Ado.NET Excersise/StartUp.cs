namespace _02.Ado.NET_Excersise
{
    using Microsoft.Data.SqlClient;
    using System.Text;

    internal class StartUp
    {
        static async Task Main(string[] args)
        {


            await using SqlConnection connection = new SqlConnection(Config.ConnectionString);
            await connection.OpenAsync();



            //First problem
            string getAllVilliansAndCount = @"  SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
            FROM Villains AS v 
            JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
            GROUP BY v.Id, v.Name 
            HAVING COUNT(mv.VillainId) > 3 
            ORDER BY COUNT(mv.VillainId)";
            string result=await getAllVilliansAndCountQuery(getAllVilliansAndCount,connection);
            Console.WriteLine(result);

            //Second problem

            string secondProblem = @"SELECT Name FROM Villains WHERE Id = @Id";


            //4th problem

            string getTownIdByName =
                @"SELECT Id FROM Villains WHERE Name = @Name";
            string 
        }

        static async Task<string> getAllVilliansAndCountQuery(string query,SqlConnection connection)
        {               
            SqlCommand command = new SqlCommand(query,connection);
            using SqlDataReader reader= await command.ExecuteReaderAsync();
            StringBuilder sb = new StringBuilder();

            while (reader.Read())
            {
                string villianName = (string)reader["Name"];
                int minionsCount = (int)(reader["MinionsCount"]);
                sb.AppendLine($"{villianName} - {minionsCount}");
            }

            return sb.ToString();
        }

        static async Task<string> AddNewMinionAsync
            (SqlConnection connection,string minionInfo,string villianName,string getTownIDByName)
        {


            string[] minionArgs = minionInfo.Split(' ',StringSplitOptions.RemoveEmptyEntries).ToArray();
            string minionName=minionArgs[0];
            int minionAge = int.Parse(minionArgs[1]);
            string townName = minionArgs[2];

            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                SqlCommand getTownIdCommand = new SqlCommand(getTownIDByName);
                getTownIdCommand.Parameters.AddWithValue("Name", getTownIDByName);

                object? townId=await getTownIdCommand.ExecuteScalarAsync();

                if (townId == null)
                {
                    SqlCommand addNewCommand = new SqlCommand();
                }
            }
            catch(Exception e)
            {

            }

        }
    }
}