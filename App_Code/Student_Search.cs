using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;
/// <summary>
/// Summary description for Student_Search
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Student_Search : System.Web.Services.WebService
{

    public Student_Search()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

    }

    [WebMethod]
    public string[] AutoCompleteAjaxRequest(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = GetLead_Id(prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["Lead_Id"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    public DataTable GetLead_Id(string prefixText)
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
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
           da.Fill(dt);
            return dt;
        }
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

}
