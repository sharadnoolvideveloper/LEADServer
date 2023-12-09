using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Pages_Admin_Admin_ChartsZone : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    //Get connection string from web.config
    public static string conString = System.Configuration.ConfigurationManager.AppSettings["conString"].ToString();
    //MySqlConnection conObj = new MySqlConnection(ConfigurationManager.ConnectionStrings[conString].ConnectionString.ToString());
    public string connection = System.Configuration.ConfigurationManager.ConnectionStrings[conString].ToString();

    vmCookies cook = new vmCookies();

  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if(cook.Admin_Id()!="")
            {
                BindChart();
            }
            else
            {
                Response.Redirect("Default.aspx?SessionOut=true");
            }
            
        }
    }

    private DataTable GetData()
    {
        using (MySqlConnection conObj = new MySqlConnection(connection))
        {
            DataTable dt = new DataTable();
            string cmd = "SELECT COUNT(PD.PDId) AS PDId, mstr_state.StateName FROM project_description AS PD INNER JOIN"+" "+
            "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN mstr_state ON MD.StateCode = mstr_state.code"+" "+
            "GROUP BY MD.ManagerName";
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd, conObj);
            adp.Fill(dt);
            return dt;
        }
    }

    private void BindChart()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = GetData();

            str.Append(@"<script type='text/javascript' src='https://www.google.com/jsapi'></script>
                        <script type='text/javascript'>
                         google.load('visualization', '1', {'packages': ['geochart']});
                         google.setOnLoadCallback(drawRegionsMap);


                          function drawRegionsMap() {
                            var data = google.visualization.arrayToDataTable([
                              ['StateName', 'PDId'],");


            int count = dt.Rows.Count - 1;

            for (int i = 0; i <= count; i++)
            {
                str.Append("['" + dt.Rows[i]["StateName"].ToString() + "',  " + dt.Rows[i]["PDId"].ToString() + "],");
                if (i == count)
                {
                    str.Append("['" + dt.Rows[i]["StateName"].ToString() + "',  " + dt.Rows[i]["PDId"].ToString() + "]]);");
                }
            }

            str.Append(" var options = {region: 'IN',displayMode: 'regions',resolution: 'provinces',width: 640,height: 480}; var chart = new google.visualization.GeoChart(document.getElementById('chart_div'));");
            str.Append("chart.draw(data, options); };");

            str.Append("</script>");
            lt.Text = str.ToString();
        }
        catch
        {
        }
    }
}