using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Services;
using System.Configuration;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Submit(object sender, EventArgs e)
    {
        string customerName = Request.Form[txtSearch.UniqueID];
        string customerId = Request.Form[hfCustomerId.UniqueID];
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + customerName + "\\nID: " + customerId + "');", true);
    }

    [WebMethod]
    public static string[] GetSearchDetails(string prefix,string searchtype)
    {
        List<string> LEAD = new List<string>();
        DataTable dt = new DataTable();
        dt = GetLead_Id(prefix, searchtype);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (searchtype == "Lead_Id")
                {
                    LEAD.Add(string.Format("{0}-{1}", dt.Rows[i]["Lead_Id"], dt.Rows[i]["RegistrationId"]));
                }
                else if (searchtype == "Student_Name")
                {
                    LEAD.Add(string.Format("{0}-{1}", dt.Rows[i]["StudentName"], dt.Rows[i]["RegistrationId"]));
                }
                else if (searchtype == "Projects")
                {
                    LEAD.Add(string.Format("{0}-{1}", dt.Rows[i]["Title"], dt.Rows[i]["PDId"]));
                }
                else if (searchtype == "Mobile_No")
                {
                    LEAD.Add(string.Format("{0}-{1}", dt.Rows[i]["MobileNo"], dt.Rows[i]["RegistrationId"]));
                }

            }
        }


        return LEAD.ToArray();
     
    }
    public static DataTable GetLead_Id(string prefixText,string SearchType)
    {
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();
        DataTable dt = new DataTable();
        using (MySqlConnection con = new MySqlConnection(connection))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WEB_LEAD_ID_PROGRESSION_SEARCH";
            cmd.Parameters.AddWithValue("P_PRETEXT", prefixText.ToString());
            cmd.Parameters.AddWithValue("p_SearchType", SearchType.ToString());
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_LEAD_ID_PROGRESSION_SEARCH";
                cmd.Parameters.AddWithValue("P_PRETEXT", TextBox2.Text.ToString());
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                ddlLead_Id.DataSource = dt;
                ddlLead_Id.DataTextField = "Lead_Id";
                ddlLead_Id.DataValueField = "RegistrationId";
                ddlLead_Id.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
}