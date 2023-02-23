namespace _02.Ado.NET_Excersise
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class Config
    {

        public const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MinionsDB;Integrated Security=TRUE";
        
    }
}
