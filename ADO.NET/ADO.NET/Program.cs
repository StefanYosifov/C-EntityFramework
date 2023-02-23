

using ADO.NET.Config;
using Microsoft.Data.SqlClient;


Config config = new Config();
SqlConnection connection = new SqlConnection(config.SQLConnection);


