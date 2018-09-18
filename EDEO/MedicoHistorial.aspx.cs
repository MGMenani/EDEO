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
    public partial class MedicoHistorial : System.Web.UI.Page
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
            SqlCommand command = new SqlCommand("EXEC Seleccionar_Diagnostico", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv_Historial.DataSource = dt;
            gv_Historial.DataBind();
            con.Close();

        }

        protected void gv_Pacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Historial.PageIndex = e.NewPageIndex;
            gv_Historial.DataBind();
            BindDataList();
        }
    }
}