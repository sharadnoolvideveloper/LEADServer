using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Student_Dashboard : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                int ProjectCount = 0, TotalMonths = 0, TotalMasterLeaderMonths = 0;
                string StudenType = "";
                BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptProjectList);
                BLobj.Student_GetEventDetail(rptEvent);
                
                DataTable dt = new DataTable();
                dt = BLobj.Student_GetMasterLeaderLevelUpdate(cook.LeadId());
                ProjectCount = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                TotalMonths = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                StudenType = dt.Rows[0].ItemArray[2].ToString();
                TotalMasterLeaderMonths = int.Parse(dt.Rows[0].ItemArray[3].ToString());
                if ((ProjectCount == 0) && (StudenType == "Student"))
                {
                    pnlVisibility(true, false, false, false);
                }
                else if ((ProjectCount >= 4) && (TotalMonths >= 3) && (StudenType == "Leader"))
                {
                    pnlVisibility(false, true, false, false);
                }
                else if ((StudenType == "Master Leader") && (TotalMasterLeaderMonths >= 6))
                {
                    pnlVisibility(false, false, true, false);
                }
                else
                {
                    pnlVisibility(false, false, false, false);
                }
                ProjectCount = 0; TotalMonths = 0; TotalMasterLeaderMonths = 0;
                StudenType = "";


                ProjectCount = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                TotalMonths = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                StudenType = dt.Rows[0].ItemArray[2].ToString();
                TotalMasterLeaderMonths = int.Parse(dt.Rows[0].ItemArray[3].ToString());
               
                if(TotalMonths==0)
                {
                    TotalMonths = 3;
                }
                else if (TotalMonths == 1)
                {
                    TotalMonths = 2;
                }
                else if (TotalMonths == 2)
                {
                    TotalMonths = 1;
                }
                else if (TotalMonths == 3)
                {
                    TotalMonths = 0;
                }
                else if (TotalMonths > 1)
                {
                   // string DisplayMsg = "Apply For Master Leader";
                }

                if (StudenType=="Student")
                {
                    FromStudentToLeader.Attributes.Add("style", "max-width: 10%");
                    FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                    FromStudentToLeader.Attributes["data-placement"] = "top";
                    FromStudentToLeader.Attributes["title"] = "1 Project To Reach Leader";
                    lblFromStudentToLeader.Text = ProjectCount.ToString() + " /1";
                    //-------step1----------

                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:0%");
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");
                }
                else if((StudenType == "Leader") && (ProjectCount == 1))
                {
                    FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                    FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                    FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                    FromStudentToLeader.Attributes["data-placement"] = "top";
                    lblFromStudentToLeader.Text = ProjectCount.ToString() + " /1";


                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:0%");
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");
                }
                else if((StudenType=="Leader") && (ProjectCount>1) && (ProjectCount<=5))
                {
                    int remain = 0;
                    remain = 5 - ProjectCount;
                    lblFromLeaderToMasterLeader.Text = ProjectCount.ToString() + " /5";
                    ProjectCount = ProjectCount * 15;
                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:" + ProjectCount + "%");
                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";
                    if(remain==0)
                    {
                        FromLeaderToMasterLeader.Attributes["title"] = "Projects Completion is Reached Successfully";
                    }
                    else
                    {
                        FromLeaderToMasterLeader.Attributes["title"] = "Total" + " " + remain + " " + "Project Remain to Reach Master Leader ";
                    }
                   


                   
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");
                    FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                    FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                    FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                    FromStudentToLeader.Attributes["data-placement"] = "top";
                }

                else if ((StudenType == "Leader") && (ProjectCount > 4) && (TotalMonths >= 1) && (TotalMonths <= 3))
                {
                  
                    lblFromLeaderToMasterLeader.Text = ProjectCount.ToString();
                    if(TotalMonths==3)
                    {
                        FromLeaderToMasterLeader.Attributes.Add("style", "max-width:81%");
                    }
                    else if (TotalMonths == 2)
                    {
                        FromLeaderToMasterLeader.Attributes.Add("style", "max-width:88%");
                    }
                    else if (TotalMonths == 1)
                    {
                        FromLeaderToMasterLeader.Attributes.Add("style", "max-width:90%");
                    }

                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";
                    if (TotalMonths == 0)
                    {
                        FromLeaderToMasterLeader.Attributes["title"] = "Last Month To Reach Master Leader";
                        FromLeaderToMasterLeader.Attributes.Add("style", "max-width:92%");
                    }
                    else
                    {
                        FromLeaderToMasterLeader.Attributes["title"] = "Total" + " " + TotalMonths + " " + "Months Remain to Reach Master Leader ";
                    }
                   
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");


                    FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                    FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                    FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                    FromStudentToLeader.Attributes["data-placement"] = "top";
                }
                else if((StudenType == "Leader") && (TotalMonths>3))
                {
                    lblFromLeaderToMasterLeader.Text = ProjectCount.ToString();
                   
                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:96%");
                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";
                  
                        FromLeaderToMasterLeader.Attributes["title"] = "Apply for Master Leader";
                   
                  
                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");


                    FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                    FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                    FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                    FromStudentToLeader.Attributes["data-placement"] = "top";
                }
                if(StudenType=="Master Leader")
                {
                    lblFromLeaderToMasterLeader.Text = ProjectCount.ToString();

                    FromLeaderToMasterLeader.Attributes.Add("style", "max-width:100%");
                    FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                    FromLeaderToMasterLeader.Attributes["data-placement"] = "top";

                    FromLeaderToMasterLeader.Attributes["title"] = "Completed Master Leader Challenges";


                    FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");


                    FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                    FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                    FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                    FromStudentToLeader.Attributes["data-placement"] = "top";

                    if (TotalMasterLeaderMonths == 0)
                    {
                        TotalMasterLeaderMonths = 6;
                    }
                    else if (TotalMasterLeaderMonths == 1)
                    {
                        TotalMasterLeaderMonths = 5;
                    }
                    else if (TotalMasterLeaderMonths == 2)
                    {
                        TotalMasterLeaderMonths = 4;
                    }
                    else if (TotalMasterLeaderMonths == 3)
                    {
                        TotalMasterLeaderMonths = 3;
                    }
                    else if (TotalMasterLeaderMonths == 4)
                    {
                        TotalMasterLeaderMonths = 2;
                    }
                    else if (TotalMasterLeaderMonths ==5)
                    {
                        TotalMasterLeaderMonths = 1;
                    }
                    else if (TotalMasterLeaderMonths == 6)
                    {
                        TotalMasterLeaderMonths = 0;
                    }

                    //----------- From Master Leader To Lead Ambassador----------

                    if ((StudenType == "Master Leader") && (TotalMasterLeaderMonths<=6))
                    {
                        if(TotalMasterLeaderMonths!=0)
                        {
                            int remainmonth = TotalMasterLeaderMonths * 12;

                            lblFromMasterLeaderToAmbassedor.Text = ProjectCount.ToString();
                            FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:"+remainmonth+"%");
                            FromMasterLeaderToAmbassedor.Attributes["data-toggle"] = "tooltip";
                            FromMasterLeaderToAmbassedor.Attributes["data-placement"] = "top";
                            FromLeaderToMasterLeader.Attributes["title"] = remainmonth+ " Month Remains to Reach Lead Ambassador";
                        }
                        else if(TotalMasterLeaderMonths==0)
                        {
                            lblFromMasterLeaderToAmbassedor.Text = ProjectCount.ToString();
                            FromMasterLeaderToAmbassedor.Attributes.Add("style", "80%");
                            FromMasterLeaderToAmbassedor.Attributes["data-toggle"] = "tooltip";
                            FromMasterLeaderToAmbassedor.Attributes["data-placement"] = "top";
                            FromLeaderToMasterLeader.Attributes["title"] = "Last Month To Reach Lead Ambassador";
                        }
                             
                       
                       

                        //---------Leader to Master Leader---------
                        FromLeaderToMasterLeader.Attributes.Add("style", "max-width:100%");
                        FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                        FromLeaderToMasterLeader.Attributes["data-placement"] = "top";

                        FromLeaderToMasterLeader.Attributes["title"] = "Completed Master Leader Challenges";
                                             
                        //---------Student to lead---------

                        FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                        FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                        FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                        FromStudentToLeader.Attributes["data-placement"] = "top";
                    }
                    else if((StudenType == "Master Leader") && (TotalMasterLeaderMonths > 6))
                    {
                        lblFromMasterLeaderToAmbassedor.Text = ProjectCount.ToString();
                        FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:95%");
                        FromMasterLeaderToAmbassedor.Attributes["data-toggle"] = "tooltip";
                        FromMasterLeaderToAmbassedor.Attributes["data-placement"] = "top";
                        FromLeaderToMasterLeader.Attributes["title"] = "Apply for Lead Ambassador";

                        //---------Leader to Master Leader---------
                        FromLeaderToMasterLeader.Attributes.Add("style", "max-width:100%");
                        FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                        FromLeaderToMasterLeader.Attributes["data-placement"] = "top";

                        FromLeaderToMasterLeader.Attributes["title"] = "Completed Master Leader Challenges";

                        //---------Student to lead---------

                        FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                        FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                        FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                        FromStudentToLeader.Attributes["data-placement"] = "top";
                    }
                    if(StudenType=="Lead Ambassador")
                    {
                        lblFromMasterLeaderToAmbassedor.Text = ProjectCount.ToString();
                        FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:100%");
                        FromMasterLeaderToAmbassedor.Attributes["data-toggle"] = "tooltip";
                        FromMasterLeaderToAmbassedor.Attributes["data-placement"] = "top";
                        FromLeaderToMasterLeader.Attributes["title"] = "Completed Lead Ambassador Succussfully";

                        //---------Leader to Master Leader---------
                        FromLeaderToMasterLeader.Attributes.Add("style", "max-width:100%");
                        FromLeaderToMasterLeader.Attributes["data-toggle"] = "tooltip";
                        FromLeaderToMasterLeader.Attributes["data-placement"] = "top";

                        FromLeaderToMasterLeader.Attributes["title"] = "Completed Master Leader Challenges";

                        //---------Student to lead---------

                        FromStudentToLeader.Attributes.Add("style", "max-width: 100%");
                        FromStudentToLeader.Attributes["title"] = "Completed Leader Challenges";
                        FromStudentToLeader.Attributes["data-toggle"] = "tooltip";
                        FromStudentToLeader.Attributes["data-placement"] = "top";
                    }


                }
                

            }

              
        }
        catch(Exception)
        {
            FromStudentToLeader.Attributes.Add("style", "max-width: 0%");
            FromLeaderToMasterLeader.Attributes.Add("style", "max-width:0%");
            FromMasterLeaderToAmbassedor.Attributes.Add("style", "max-width:0%");
        }
    }
    protected void pnlVisibility(bool Student, bool Master, bool Ambassador, bool Intern)
    {
        pnlStudent.Visible = Student;
        pnlMasterLeader.Visible = Master;
        pnlLeadAmbassador.Visible = Ambassador;
      //  pnlLeadIntern.Visible = Intern;
       
    }

    protected void rptProjectList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Label ProjectStatus = (e.Item.FindControl("lblProjectStatus") as Label);
                Label Rating = (e.Item.FindControl("lblRating") as Label);
               // LinkButton btnEdit = (e.Item.FindControl("btnEditProject") as LinkButton);

                if (ProjectStatus.Text == "Proposed")
                {

                    //btnEdit.Text = "<span class='fa fa-edit'></span>";
                    //btnEdit.Attributes.Add("class", "btn btn-default btn-floating");
                    ProjectStatus.Attributes.Add("class", "label label-default");
                }
                else if (ProjectStatus.Text == "Approved")
                {
                   // btnEdit.Text = "<span class='fa fa-share'></span>";
                  //  btnEdit.Attributes.Add("class", "btn btn-info btn-floating");
                    ProjectStatus.Attributes.Add("class", "label label-primary");

                }
                else if (ProjectStatus.Text == "RequestForModification")
                {
                  //  btnEdit.Text = "<span class='fa fa-reply'></span>";
                  //  btnEdit.Attributes.Add("class", "btn btn-primary btn-floating");
                    ProjectStatus.Attributes.Add("class", "label label-warning text-danger");

                }
                else if (ProjectStatus.Text == "RequestForCompletion")
                {
                   // btnEdit.Text = "<span class='fa fa-edit'></span>";
                  //  btnEdit.Attributes.Add("class", "btn btn-warning btn-floating");
                    ProjectStatus.Attributes.Add("class", "label label-warning text-danger");

                }
                else if (ProjectStatus.Text == "Completed")
                {
                    ProjectStatus.Attributes.Add("class", "label label-success");
                   // btnEdit.Enabled = false;

                   // btnEdit.Attributes.Add("class", "btn btn-info btn-floating");
                   // btnEdit.Text = "<span class='fa fa-thumbs-up fa-2x text-primary'></span>";
                    Rating.Text = BLobj.GetRatingStarts(Rating.Text);
                    Rating.Visible = true;
                    ProjectStatus.Visible = false;
                }
                else if (ProjectStatus.Text == "Rejected")
                {
                   // btnEdit.Text = "<span class='fa fa-remove fa-2x text-danger'></span>";
                  //  btnEdit.Attributes.Add("class", "btn btn-danger btn-floating text-danger");
                  //  btnEdit.Enabled = false;
                    ProjectStatus.Attributes.Add("class", "label label-danger");
                }

            }
        }
        catch (Exception)
        {

        }
    }
  
    protected void rptEvent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {
                string LeadId = cook.LeadId();

                string EventId = "";
              
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 0; i++)
                {
                    if (i == 0)
                    {
                        EventId = itemlist[0].ToString();
                    }
                   
                }
                Response.Redirect("EventDetails.aspx?EventId=" + EventId.ToString());

            }
        }
        catch (Exception)
        {

        }
    }
}