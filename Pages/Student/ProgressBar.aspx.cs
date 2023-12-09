using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_ProgressBar : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!Page.IsPostBack)
            {
                int ProjectCount = 0, TotalMonths = 0, TotalMasterLeaderMonths = 0;
                string StudenType = "";
                DataTable dt = new DataTable();
                dt = BLobj.Student_GetMasterLeaderLevelUpdate(cook.LeadId());

                ProjectCount = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                TotalMonths = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                StudenType = dt.Rows[0].ItemArray[2].ToString();
                TotalMasterLeaderMonths = int.Parse(dt.Rows[0].ItemArray[3].ToString());
                //DataList1.DataSource = dt;
                //DataList1.DataBind();

                if ((ProjectCount == 0) && (StudenType == "Student"))
                {
                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";
                    FromLeaderToMasterLeader.Attributes["title"] = "1 Project To Reach Leader";
                    FromStudentToLeader.Attributes.Add("style", "max-width: 1%");
                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:0%");
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");
                    lblFromStudentToLeader.Text = ProjectCount.ToString() + " /1";

                   
                }
                else if ((ProjectCount > 0) && (ProjectCount <= 4) && (StudenType == "Leader"))
                {
                    int remain = 0;
                    remain = 4 - ProjectCount;
                    lblFromLeaderToMasterLeader.Text = ProjectCount.ToString()+ "/4";
                    ProjectCount = ProjectCount * 25;
                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";
                    FromLeaderToMasterLeader.Attributes["title"] = remain + " " + "More Project To Reach Lead Master";
                    FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:"+ProjectCount+"%");
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");
                    
                   
                    
                }
                else if ((TotalMonths <= 3) && (StudenType == "Leader"))
                {
                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";
                    FromLeaderToMasterLeader.Attributes["title"] = TotalMonths + " " + "Months to Reach Lead Master";
                    lblFromLeaderToMasterLeader.Text = ProjectCount.ToString();


                }
                else if ((StudenType == "Master Leader") && (TotalMasterLeaderMonths >= 6))
                {
                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";
                    FromLeaderToMasterLeader.Attributes["title"] = TotalMasterLeaderMonths + " " + "Months to Reach Lead Ambassador";
                    FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:100%");
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:"+ ProjectCount +"%");
                    lblFromMasterLeaderToAmbassedor.Text = ProjectCount.ToString();

                }
                else
                {
                   // FromStudentToLeader.Attributes.Add("style", "max-width: 1");
                    //FromLeaderToMasterLeader.Attributes.Add("style", "max-width:"+ProjectCount+"%");
                   
                }
            }
        }
        catch(Exception)
        {

        }
       
    }
}