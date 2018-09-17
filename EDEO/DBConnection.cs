using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EDEO
{
    public class DBConnection
    {
        public string conString = "Data Source=DESKTOP-SN74R0B;Initial Catalog=Boneage;User ID=sa;Password=2015000175";


        public SqlConnection DataBase_Connection()
        {
            SqlConnection con = new SqlConnection(conString);
            return con;
        }

    }
}