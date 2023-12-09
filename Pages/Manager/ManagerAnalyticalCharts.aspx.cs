using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;


public partial class Pages_Manager_ManagerAnalyticalCharts : System.Web.UI.Page
{
    public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        // GetChartData();
    }


    [WebMethod]
    public static List<object> GetChartData()
    {
        vmCookies cook = new vmCookies();
        string query = "select PD.ProjectStatus,Count(PD.ProjectStatus) as ProjectCounts from project_description PD inner join student_registration as SR "+" "+
       "on PD.Student_Id=SR.RegistrationId  where PD.ManagerId="+cook.Manager_Id()+" Group By PD.ProjectStatus";
        string constr = ConfigurationManager.ConnectionStrings["LEADMIS"].ConnectionString;
        List<object> chartData = new List<object>();
        chartData.Add(new object[]
        {
            "ProjectStatus", "ProjectCounts"
        });
        using (MySqlConnection con = new MySqlConnection(constr))
        {
            using (MySqlCommand cmd = new MySqlCommand(query))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (MySqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        chartData.Add(new object[]
                    {
                        sdr["ProjectStatus"], sdr["ProjectCounts"]
                    });
                    }
                }
                con.Close();
                return chartData;
            }
        }
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getManagersanctionamount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();
        vmCookies cook = new vmCookies();
        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WEB_CHART_MANAGER_FOUNT_COUNT";
            cmd.Parameters.AddWithValue("ManagerId", cook.Manager_Id());
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        AcademicCode = dt1.Rows[i]["AcademicCode"].ToString(),
                        RequestAmount = int.Parse(dt1.Rows[i]["RequestAmount"].ToString()),
                        SanctionAmount = int.Parse(dt1.Rows[i]["SanctionAmount"].ToString()),
                        fundRelised = int.Parse(dt1.Rows[i]["fundRelised"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getManagerprojectstatuscount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_MANAGER_SELECT_PROJECT_COUNTS";
            cmd.Parameters.AddWithValue("p_Id", 2);
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        ProjectStatus = dt1.Rows[i]["ProjectStatus"].ToString(),
                        Counts = int.Parse(dt1.Rows[i]["Counts"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getManagerThemewiseprojectcount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_MANAGE_THEMEWISE_PROGRAM_COUNT";
            cmd.Parameters.AddWithValue("P_ManagerCode", 8);
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        ThemeName = dt1.Rows[i]["ThemeName"].ToString(),
                        Counts = int.Parse(dt1.Rows[i]["Counts"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getTotalRegistration()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_STUDENT_REG";

            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        ManagerName = dt1.Rows[i]["ManagerName"].ToString(),
                        TotalRegistration = int.Parse(dt1.Rows[i]["TotalRegistration"].ToString()),
                        PresentYearRgistration = int.Parse(dt1.Rows[i]["PresentYearRgistration"].ToString()),
                    });
                }
            }

        }


        return projectamount;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static getamountlist Getstudentcounts1()
    {
        getamountlist projectamount = new getamountlist();

        //  DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_TOTAL_STUDENT_COUNT";
            cmd.Connection = con;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    projectamount.TotalRegistration = int.Parse(dr["TotalRegistration "].ToString());
                    projectamount.TotalActivestudent = int.Parse(dr["TotalActivestudent"].ToString());
                    projectamount.PresentYearRgistration = int.Parse(dr["PresentYearRgistration"].ToString());

                }
            }

        }


        return projectamount;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> Getstudentcounts()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_TOTAL_STUDENT_COUNT";

            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        TotalRegistration = int.Parse(dt1.Rows[i]["TotalRegistration"].ToString()),
                        TotalActivestudent = int.Parse(dt1.Rows[i]["TotalActivestudent"].ToString()),
                        PresentYearRgistration = int.Parse(dt1.Rows[i]["PresentYearRgistration"].ToString()),
                    });
                }
            }

        }


        return projectamount;

    }


    public class getamountlist
    {
        public string ManagerName { get; set; }
        public int RequestAmount { get; set; }
        public int SanctionAmount { get; set; }
        public int fundRelised { get; set; }
        public string ProjectStatus { get; set; }
        public string ThemeName { get; set; }
        public string StateName { get; set; }
        public int ProjectCounts { get; set; }
        public string AcademicCode { get; set; }
        public string AcademiCYear { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int TotalRegistration { get; set; }
        public int TotalActivestudent { get; set; }
        public int PresentYearRgistration { get; set; }
        public int Counts { get; set; }

    }
}