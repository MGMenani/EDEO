using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project_EDEO
{
    public class DBConnection
    {
        // Connection string to local Data Base
        private static readonly string ConnectionString = "Data Source=DESKTOP-SN74R0B;Initial Catalog=Boneage;User ID=sa;Password=2015000175";

        public SqlConnection DataBase_Connection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            return connection;
        }
    }
}