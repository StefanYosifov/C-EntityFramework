namespace ADO.NET.Config
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Config
    {
        public Config()
        {

        }


        public string SQLConnection = "Server=.\\SQLEXPRESS;Database=SoftUni;Integrated Security=true";

    }
}
