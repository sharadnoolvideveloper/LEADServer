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

public partial class Pages_Admin_AdminAnalyticalCharts : System.Web.UI.Page
{
    vmCookies cook = new vmCookies();
    public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
               
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<projectstatuscount> getManagerprojectcounts()
    {
        List<projectstatuscount> projectcount = new List<projectstatuscount>();

        DataTable dt = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_MANAGER_PROJECT_STATUS_COUNT_CHART";
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    projectcount.Add(new projectstatuscount
                    {
                        ManagerName = dt.Rows[i]["ManagerName"].ToString(),
                        Proposed = int.Parse(dt.Rows[i]["Proposed"].ToString()),
                        Approved = int.Parse(dt.Rows[i]["Approved"].ToString()),
                        Completed = int.Parse(dt.Rows[i]["Completed"].ToString()),
                        RequestForModification = int.Parse(dt.Rows[i]["RequestForModification"].ToString()),
                        RequestForCompletion = int.Parse(dt.Rows[i]["RequestForCompletion"].ToString()),
                    });
                }
            }

        }


        return projectcount;

    }

    public class projectstatuscount
    {
        public string ManagerName { get; set; }
        public int Proposed { get; set; }
        public int Approved { get; set; }
        public int Completed { get; set; }
        public int RequestForModification { get; set; }
        public int RequestForCompletion { get; set; }
        public int Total { get; set; }
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getManagersanctionamount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_MANAGER_WISE_AMOUNTLIST_CHART";
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
                        RequestedAmount = int.Parse(dt1.Rows[i]["RequestedAmount"].ToString()),
                        SanctionAmount = int.Parse(dt1.Rows[i]["SanctionAmount"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }

    public class getamountlist
    {
        public string ManagerName { get; set; }
        public int RequestedAmount { get; set; }
        public int SanctionAmount { get; set; }
        public string ProjectStatus { get; set; }
        public string ThemeName { get; set; }
        public string StateName { get; set; }
        public int ProjectCounts { get; set; }

        public int Totalstudent { get; set; }
        public int Totalamount { get; set; }
        public string AcademiCYear { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int TotalRegistration { get; set; }
        public int TotalActivestudent { get; set; }
        public int PresentYearRgistration { get; set; }
        public int TotalSanctionAmount { get; set; }
        public int TotalRequestAmount { get; set; }
        public string DistrictName { get; set; }
        public string Taluk_Name { get; set; }
        public string College_Name { get; set; }
        public int Registrations { get; set; }

        public int? Completed { get; set; }
        public int? Proposed { get; set; }

      
        public int Approved
        {
            get; set;
        }
       
        public int RequestForModification
        {
            get; set;
        }
        public int RequestForCompletion
        {
            get; set;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getprojectstatuscountlist()
    {
        List<getamountlist> projectcount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_PROJECT_STATUS_COUNT_CHART";
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectcount.Add(new getamountlist
                    {
                         Proposed = int.Parse(dt1.Rows[i]["Proposed"].ToString()),
                        Approved = int.Parse(dt1.Rows[i]["Approved"].ToString()),
                        Completed = int.Parse(dt1.Rows[i]["Completed"].ToString()),
                        RequestForModification = int.Parse(dt1.Rows[i]["RequestForModification"].ToString()),
                        RequestForCompletion = int.Parse(dt1.Rows[i]["RequestForCompletion"].ToString()),
                    });
                }
            }

        }


        return projectcount;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getstatewiseprojectcount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_STATE_WISE_PROJECTCOUNTS_CHARTS";
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        StateName = dt1.Rows[i]["StateName"].ToString(),
                        ProjectCounts = int.Parse(dt1.Rows[i]["ProjectCounts"].ToString()),


                    });
                }
            }

        }


        return projectamount;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getThemewiseprojectcount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_THEME_WISE_PROJECT_COUNT_CHARTS";
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
                        ProjectCounts = int.Parse(dt1.Rows[i]["ProjectCounts"].ToString()),


                    });
                }
            }

        }


        return projectamount;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getMaleandfemalecount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GENDER_COUNT_CHAER";
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        AcademiCYear = dt1.Rows[i]["AcademiCYear"].ToString(),
                        Male = int.Parse(dt1.Rows[i]["Male"].ToString()),
                        Female = int.Parse(dt1.Rows[i]["Female"].ToString()),


                    });
                }
            }

        }


        return projectamount;

    }


    //public static getamountlist Getstudentcounts()
    //{
    //    getamountlist projectamount = new getamountlist();

    //    DataTable dt1 = new DataTable();
    //    using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
    //    {
    //        con.Open();
    //        MySqlCommand cmd = new MySqlCommand();
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_TOTAL_STUDENT_COUNT";
    //        cmd.Connection = con;
    //       MySqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {
    //            if (dr.Read())
    //            {
    //                projectamount.TotalRegistration = int.Parse(dr["TotalRegistration "].ToString());
    //                projectamount.TotalActivestudent = int.Parse(dr["TotalActivestudent"].ToString());
    //                projectamount.PresentYearRgistration = int.Parse(dr["PresentYearRgistration"].ToString());

    //            }
    //        }

    //    }


    //    return projectamount;

    //}
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


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]

    public static List<getamountlist> GetTotalfundlistinlead()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_TOTAL_FUND_LIST_LEAD";

            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        Totalstudent = int.Parse(dt1.Rows[i]["Totalstudent"].ToString()),
                        TotalRequestAmount = int.Parse(dt1.Rows[i]["TotalRequestAmount"].ToString()),
                        TotalSanctionAmount = int.Parse(dt1.Rows[i]["TotalSanctionAmount"].ToString()),
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
    public static List<getamountlist> Getstudentcountss()
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> GetstudentandFundamount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_TOTAL_STUDENT_FUND_YEAR_CHAET";

            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        Totalstudent = int.Parse(dt1.Rows[i]["Totalstudent"].ToString()),
                        SanctionAmount = int.Parse(dt1.Rows[i]["SanctionAmount"].ToString()),
                        Totalamount = int.Parse(dt1.Rows[i]["Totalamount"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getTotalcollegecount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_TALUK_SISTRICT_STATE_COLLEGE_COUNT";

            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        College_Name = dt1.Rows[i]["College_Name"].ToString(),
                        Taluk_Name = dt1.Rows[i]["Taluk_Name"].ToString(),
                        DistrictName = dt1.Rows[i]["DistrictName"].ToString(),
                        StateName = dt1.Rows[i]["StateName"].ToString(),
                        Registrations = int.Parse(dt1.Rows[i]["Registrations"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getprojectproposedcountlist()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_TOP_COLLEGE_PROPOSED";

            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        College_Name = dt1.Rows[i]["College_Name"].ToString(),
                        Proposed = int.Parse(dt1.Rows[i]["Proposed"].ToString()),
                        // Completed = int.Parse(dt1.Rows[i]["Completed"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }




    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<getamountlist> getTotalcomplitedcount()
    {
        List<getamountlist> projectamount = new List<getamountlist>();

        DataTable dt1 = new DataTable();
        using (MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString()))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_TOP_COLLEGE_COMPLITED";

            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    projectamount.Add(new getamountlist
                    {
                        College_Name = dt1.Rows[i]["College_Name"].ToString(),
                        Completed = int.Parse(dt1.Rows[i]["Completed"].ToString()),

                    });
                }
            }

        }


        return projectamount;

    }

}