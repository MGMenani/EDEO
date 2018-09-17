using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EDEO
{
    public partial class MedicoPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataList();
            }
        }

        protected void BindDataList()
        {
            DBConnection connection = new DBConnection();
            SqlConnection con = connection.DataBase_Connection();
            con.Open();
            SqlCommand command = new SqlCommand("SELECT Cedula as 'Cédula', Nombre, Apellidos, convert(varchar, FechaNacimiento, 120) as 'Fecha de nacimiento (AAAA-DD-MM)' " +
                                                "FROM Expediente", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv_Pacientes.DataSource = dt;
            gv_Pacientes.DataBind();
            con.Close();

           
        }

        protected void gv_Pacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Pacientes.PageIndex = e.NewPageIndex;
            gv_Pacientes.DataBind();
            BindDataList();
        }
    }
}