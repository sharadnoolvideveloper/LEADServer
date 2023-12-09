using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Manager_DashBoard : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    string FromDate = "", ToDate = "";
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                string vwType = "";
                if (cook.Manager_Id() != "")
                {
                    string ManagerID = cook.Manager_Id();
                    string AcademicCode = cook.Manager_AcademicYear();
                    if ((cook.Temp_Selected_Year() == "") || (cook.Temp_Selected_Year() == null))
                    {
                        Set_Selected_Academic_Year(AcademicCode);
                    }

                    vmManager vmManager = new vmManager();
                    BLobj.Manager_GetManagerDetails(vmManager, ManagerID.ToString());
                    lblManagerNameLeft.Text = vmManager.ManagerName.ToString();
                    imgManagerProfilePicLeft.ImageUrl = vmManager.Image_path.ToString();
                    if (Request.QueryString["vwType"].ToString() != "")
                    {
                        vwType = Request.QueryString["vwType"].ToString();
                        if (vwType == "DashBoard")
                        {
                            BLobj.FillAademicYear(ddlAllProjectAcademicYear);
                            ddlAllProjectAcademicYear.SelectedIndex = ddlAllProjectAcademicYear.Items.IndexOf(ddlAllProjectAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddlAllProjectAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftDashboard.Attributes.Add("class", "list-group-item active");
                            ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                            rptAllProjects.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlAllProjectAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlAllRecordCount.SelectedValue.ToString());
                            rptAllProjects.DataBind();
                            System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlAllProjectAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                            foreach (DataRow dr in dt.Rows)
                            {
                                lblAllProjectsCount.Text = dr[0].ToString();
                                lblProposedProjectsCount.Text = dr[1].ToString();
                                lblApprovedCount.Text = dr[2].ToString();
                                lblRejectedProjectCount.Text = dr[3].ToString();
                                lblRequestForModificationCount.Text = dr[4].ToString();
                                lblRequestForCompletionCount.Text = dr[5].ToString();
                                lblCompletedProjectsCount.Text = dr[7].ToString();
                                lbldraftedProjectCount.Text = dr[6].ToString();


                                lblTotalProjectLeft.Text = dr[0].ToString();
                                lblProposedProjectLeft.Text = dr[1].ToString();
                                lblApprovedProjectsLeft.Text = dr[2].ToString();
                                lblRejectedProjectLeft.Text = dr[3].ToString();
                                lblRequestForModification.Text = dr[4].ToString();
                                lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                                lblCompletedProjectsLeft.Text = dr[7].ToString();
                                lbldraftedprojectsleft.Text = dr[6].ToString();
                            }
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlAllProjectAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwDashboard);
                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                            //string msg = "Welcome";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                        }
                        else if (vwType == "Proposed")
                        {
                            BLobj.FillAademicYear(ddlProposedAcademicYear);
                            ddlProposedAcademicYear.SelectedIndex = ddlProposedAcademicYear.Items.IndexOf(ddlProposedAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddlProposedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftProposed.Attributes.Add("class", "list-group-item active");
                            ddlProposedRecordCount.SelectedIndex = ddlProposedRecordCount.Items.IndexOf(ddlProposedRecordCount.Items.FindByValue(cook.Manager_Record()));
                            rptProposedProjectsList.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlProposedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlProposedRecordCount.SelectedValue.ToString());
                            rptProposedProjectsList.DataBind();
                            SetLableCount(ddlProposedAcademicYear.SelectedValue.ToString());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlProposedAcademicYear.SelectedValue.ToString());


                            MainView.SetActiveView(vwProposedProjects);
                        }
                        else if (vwType == "Approved")
                        {
                            BLobj.FillAademicYear(ddlApprovedAcademicYear);
                            ddlApprovedAcademicYear.SelectedIndex = ddlApprovedAcademicYear.Items.IndexOf(ddlApprovedAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddlApprovedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftApproved.Attributes.Add("class", "list-group-item active");
                            ddlApprovedRecordCount.SelectedIndex = ddlApprovedRecordCount.Items.IndexOf(ddlApprovedRecordCount.Items.FindByValue(cook.Manager_Record()));
                            rptApprovedProjects.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlApprovedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlApprovedRecordCount.SelectedValue.ToString());
                            rptApprovedProjects.DataBind();
                            SetLableCount(ddlApprovedAcademicYear.SelectedValue.ToString());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlApprovedAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwApprovedProjects);
                        }
                        else if (vwType == "RequestForCompletion")
                        {
                            BLobj.FillAademicYear(ddlRequestForCompletionSearchAcademicYear);
                            ddlRequestForCompletionSearchAcademicYear.SelectedIndex = ddlRequestForCompletionSearchAcademicYear.Items.IndexOf(ddlRequestForCompletionSearchAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftRequestForCompletion.Attributes.Add("class", "list-group-item active");
                            ddlRCRecordCount.SelectedIndex = ddlRCRecordCount.Items.IndexOf(ddlRCRecordCount.Items.FindByValue(cook.Manager_Record()));
                            rptRequestForCompletion.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlRequestForCompletionSearchAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRCRecordCount.SelectedValue.ToString());
                            rptRequestForCompletion.DataBind();
                            SetLableCount(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwRequestForCompletion);
                        }
                        else if (vwType == "RequestForModification")
                        {
                            BLobj.FillAademicYear(ddlRequestForModificationAcademicYear);
                            ddlRequestForModificationAcademicYear.SelectedIndex = ddlRequestForModificationAcademicYear.Items.IndexOf(ddlRequestForModificationAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddlRequestForModificationAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftRequestForModification.Attributes.Add("class", "list-group-item active");
                            ddlRMRcordCount.SelectedIndex = ddlRMRcordCount.Items.IndexOf(ddlRMRcordCount.Items.FindByValue(cook.Manager_Record()));
                            rptRequestForModifiation.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlRequestForModificationAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRMRcordCount.SelectedValue.ToString());
                            rptRequestForModifiation.DataBind();
                            SetLableCount(ddlRequestForModificationAcademicYear.SelectedValue.ToString());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlRequestForModificationAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwRequestForModification);
                        }
                        else if (vwType == "Completed")
                        {
                            BLobj.FillAademicYear(ddlCompletionAcademicYear);
                            ddlCompletionAcademicYear.SelectedIndex = ddlCompletionAcademicYear.Items.IndexOf(ddlCompletionAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddlCompletionAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftCompleted.Attributes.Add("class", "list-group-item active");
                            ddlCompletedRecordCount.SelectedIndex = ddlCompletedRecordCount.Items.IndexOf(ddlCompletedRecordCount.Items.FindByValue(cook.Manager_Record()));
                            rptCompletedProject.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlCompletionAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlCompletedRecordCount.SelectedValue.ToString());
                            rptCompletedProject.DataBind();
                            SetLableCount(ddlCompletionAcademicYear.SelectedValue.ToString());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlCompletionAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwCompletedProjects);
                        }
                        else if (vwType == "Draft")
                        {
                            BLobj.FillAademicYear(ddldraftedAcademicYear);
                            ddldraftedAcademicYear.SelectedIndex = ddldraftedAcademicYear.Items.IndexOf(ddldraftedAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddldraftedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftdrafted.Attributes.Add("class", "list-group-item active");
                            ddldraftedRecordCount.SelectedIndex = ddldraftedRecordCount.Items.IndexOf(ddldraftedRecordCount.Items.FindByValue(cook.Manager_Record()));
                            rptdraftedProject.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddldraftedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddldraftedRecordCount.SelectedValue.ToString());
                            rptdraftedProject.DataBind();
                            SetLableCount(ddldraftedAcademicYear.SelectedValue.ToString());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddldraftedAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwdraftedProjects);
                        }
                        else if (vwType == "Rejected")
                        {
                            BLobj.FillAademicYear(ddlRejectedProjectAcademicYear);
                            ddlRejectedProjectAcademicYear.SelectedIndex = ddlRejectedProjectAcademicYear.Items.IndexOf(ddlRejectedProjectAcademicYear.Items.FindByValue(cook.Temp_Selected_Year()));
                            BLobj.GetDatesFromAcademicYear(ddlRejectedProjectAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                            leftRejected.Attributes.Add("class", "list-group-item active");
                            ddlRejectedRecordCount.SelectedIndex = ddlRejectedRecordCount.Items.IndexOf(ddlRejectedRecordCount.Items.FindByValue(cook.Manager_Record()));
                            rptRejected.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlRejectedProjectAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRejectedRecordCount.SelectedValue.ToString());
                            rptRejected.DataBind();
                            SetLableCount(ddlRejectedProjectAcademicYear.SelectedValue.ToString());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlRejectedProjectAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwRejectedProjects);
                        }

                        else if (vwType == "DeActivate")
                        {

                            MainView.SetActiveView(vwStudentDeactivation);
                        }
                        else if (vwType == "Registration")
                        {
                            BLobj.Manager_FillCollegeByManagerCode(cook.Manager_Id(), ddlRegistrationCollegeSearch);
                            BLobj.GetDatesFromAcademicYear(BLobj.GetTop1AademicCode(), out FromDate, out ToDate);
                            ddlRegistrationCollegeSearch.Visible = true;
                            System.Data.DataTable dt = BLobj.Manager_GetFeesPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
                            rptisPaid.DataSource = dt;
                            rptisPaid.DataBind();
                            lblTotalPaidStrenth.Text = dt.Rows.Count.ToString();
                            dt = BLobj.Manager_GetFeesUnPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
                            rptisNotPaid.DataSource = dt;
                            rptisNotPaid.DataBind();

                            lblTotalUnPaidStrenth.Text = dt.Rows.Count.ToString();
                            SetLableCount(BLobj.GetTop1AademicCode());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlRejectedProjectAcademicYear.SelectedValue.ToString());
                            MainView.SetActiveView(vwRegistration);
                        }
                        else if (vwType == "Tshirt")
                        {
                            BLobj.GetDatesFromAcademicYear(BLobj.GetTop1AademicCode(), out FromDate, out ToDate);
                            BLobj.Manager_FillCollegeByManagerCode(cook.Manager_Id(), ddlTshirtCollege);
                            ddlTshirtCollege.Visible = true;
                            SetTshirtCountForLabels();
                            SetLableCount(BLobj.GetTop1AademicCode());
                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            rptTshirtAll.DataSource = BLobj.Manager_GetTshirtAllList(cook.Manager_Id(), ddlTshirtCollege.SelectedValue.ToString());
                            rptTshirtAll.DataBind();
                            TshirtAll.Visible = true;
                            Sanction.Visible = false;
                            rejected.Visible = false;
                            tshirtapprovelabels.Visible = false;
                            rptTshirtRequestedList.Visible = false;
                            rptTshirtSanctionedList.Visible = false;
                            MainView.SetActiveView(vwTshirtList);
                        }
                        else if (vwType == "FundAmount")
                        {
                            BLobj.FillAademicYearWithTop(ddlFundAmountAcademicYear);
                            BLobj.GetDatesFromAcademicYear(BLobj.GetTop1AademicCode(), out FromDate, out ToDate);
                            // ddlFundAmountRecordCount.SelectedIndex = ddlFundAmountRecordCount.Items.IndexOf(ddlFundAmountRecordCount.Items.FindByValue(cook.Manager_Record()));

                            SetLableCount(BLobj.GetTop1AademicCode());
                            lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());

                            SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                            lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                            DataTable dtFund = BLobj.FillFundingAmount(cook.Manager_Id(), ddlFundAmountAcademicYear.SelectedValue.ToString());
                            rptFundAmount.DataSource = dtFund;
                            rptFundAmount.DataBind();
                            lblFundAmountCount.Text = "Count=" + " " + dtFund.Rows.Count.ToString();
                            MainView.SetActiveView(vwFundAmount);

                        }
                    }
                    else
                    {
                        BLobj.FillAademicYear(ddlAllProjectAcademicYear);


                        leftDashboard.Attributes.Add("class", "list-group-item active");
                        ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                        rptAllProjects.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), vwType.ToString(), ddlAllProjectAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlAllRecordCount.SelectedValue.ToString());
                        rptAllProjects.DataBind();
                        System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlAllProjectAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                        foreach (DataRow dr in dt.Rows)
                        {
                            lblAllProjectsCount.Text = dr[0].ToString();
                            lblProposedProjectsCount.Text = dr[1].ToString();
                            lblApprovedCount.Text = dr[2].ToString();
                            lblRejectedProjectCount.Text = dr[3].ToString();
                            lblRequestForModificationCount.Text = dr[4].ToString();
                            lblRequestForCompletionCount.Text = dr[5].ToString();
                            lblCompletedProjectsCount.Text = dr[6].ToString();
                            lblTotalProjectLeft.Text = dr[0].ToString();
                            lblProposedProjectLeft.Text = dr[1].ToString();
                            lblApprovedProjectsLeft.Text = dr[2].ToString();
                            lblRejectedProjectLeft.Text = dr[3].ToString();
                            lblRequestForModification.Text = dr[4].ToString();
                            lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                            lblCompletedProjectsLeft.Text = dr[6].ToString();
                        }
                        MainView.SetActiveView(vwDashboard);
                    }

                }
                else
                {
                    Response.Redirect("~/Default.aspx?SessionTimeOut=True");
                }
            }
        }
        catch (Exception)
        {

        }
    }
    public void Set_Selected_Academic_Year(string Year)
    {
        try
        {
            HttpCookie Selected_Year = new HttpCookie("Selected_Year");
            Selected_Year.Value = Year.ToString();
            Selected_Year.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Selected_Year);
        }
        catch (Exception)
        {

        }
    }
    protected void rptTeamMembers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                DropDownList ddlGender = (e.Item.FindControl("ddlGender") as DropDownList);
                Label slno = (e.Item.FindControl("lblSlno") as Label);
                string Gender = BLobj.Student_GetProjectMembersGender(slno.Text.ToString());
                ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(Gender.ToString()));
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnLogoutDashboard_Click(object sender, EventArgs e)
    {
        try
        {
            Request.Cookies.Remove("Manager_UserName");
            Request.Cookies.Remove("Manager_Id");
            Request.Cookies.Remove("Manager_AcademicYear");
            Request.Cookies.Remove("Users_Role");

            Response.Redirect("~/Default.aspx");
        }
        catch (Exception)
        {

        }
    }
    protected void rptAllProjects_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {

                string btnPressType = "";
                string PDId = "";
                string ProjectStatus = "";
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 2; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        ProjectStatus = itemlist[1].ToString();
                    }
                    else if (i == 2)
                    {
                        btnPressType = itemlist[2].ToString();
                    }
                }
                if (btnPressType == "Payee")
                {
                    if ((ProjectStatus == "Approved") || (ProjectStatus == "Completed") || (ProjectStatus == "RequestForCompletion"))
                    {
                        lblPop_ScheduleTitle.Text = "Dispersing of Amount";
                        string ManagerID = cook.Manager_Id();

                        PDId = (e.Item.FindControl("lblPDId") as Label).Text;
                        lblPop_SchedulePDId.Text = PDId.ToString();
                        lblPop_ScheduleLeadId.Text = (e.Item.FindControl("lblLeadId") as Label).Text;

                        BLobj.Manager_GetFundingDetails(PDId.ToString(), ManagerID.ToString(), rptScheduledDetails, lblSchedule_StudentName, lblSchedule_ProjectTitle, lblSchedule_RequestedAmount, lblSchedule_SanctionAmount, lblSchedule_TotalGivenAmount, lblSchedule_BalanceAmount);

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SchedulePay();", true);
                    }

                }
                else
                {
                    string ManagerID = cook.Manager_Id();
                    PDId = (e.Item.FindControl("lblPDId") as Label).Text;
                    string LeadId = (e.Item.FindControl("lblLeadId") as Label).Text;
                }

            }
        }
        catch (Exception)
        {

        }
    }
    protected void rptAllProjects_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //Label ProjectStatus = (e.Item.FindControl("lblProjectStatus") as Label);
                LinkButton btnPayee = (e.Item.FindControl("btnPayee") as LinkButton);
                Label lblPDId = (e.Item.FindControl("lblPDId") as Label);
                Label lblLeadId = (e.Item.FindControl("lblLeadId") as Label);
                Label lblDispersedAmount = (e.Item.FindControl("lblDisperseAmt") as Label);
                Label lblBalanceAmount = (e.Item.FindControl("lblBalance") as Label);
                Label lblSacntionAmount = (e.Item.FindControl("lblSacntionAmount") as Label);

                //  BLobj.Manager_GetFundingDetailWithDataBound(lblPDId.Text.ToString(), lblLeadId.Text.ToString(), btnPayee, cook.Manager_Id(), lblBalanceAmount, lblDispersedAmount);

                //lblDispersedAmount.Text = BLobj.Student_GetProjectTotalFunded(lblPDId.Text, lblLeadId.Text.ToString());
                //lblBalanceAmount.Text = Convert.ToString(int.Parse(lblSacntionAmount.Text) - int.Parse(lblDispersedAmount.Text));


            }
        }
        catch (Exception)
        {

        }

    }

    protected void rptApprovedProjects_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string PDId = (e.Item.FindControl("lblPDId") as Label).Text;
                string LeadId = (e.Item.FindControl("lblLeadId") as Label).Text;
                string ProjectStatus = "Approved";
                Server.Transfer("Manager_ApproveProject.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString() + "&Lead_Id=" + LeadId.ToString(), false);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }

        }
        catch (Exception)
        {

        }
    }




    protected void rptCompletedProject_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblPDId = (e.Item.FindControl("lblPDId") as Label);
                Label lblLeadId = (e.Item.FindControl("lblLeadId") as Label);
                Label lblDispersedAmount = (e.Item.FindControl("lblDispAmount") as Label);
                Label lblBalanceAmount = (e.Item.FindControl("lblBalAmount") as Label);
                Label lblSanctionAmount = (e.Item.FindControl("lblSanctionAmount") as Label);


                //  BLobj.Manager_GetFundingDetailWithDataBound(lblPDId.Text.ToString(), lblLeadId.Text.ToString(), btnPayee, cook.Manager_Id(), lblBalanceAmount, lblDispersedAmount);

                lblDispersedAmount.Text = BLobj.Student_GetProjectTotalFunded(lblPDId.Text, lblLeadId.Text.ToString());
                lblBalanceAmount.Text = Convert.ToString(int.Parse(lblSanctionAmount.Text) - int.Parse(lblDispersedAmount.Text));
            }
        }
        catch (Exception)
        {

        }
    }
    protected void rptRequestForCompletion_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblPDId = (e.Item.FindControl("lblPDId") as Label);
                Label lblLeadId = (e.Item.FindControl("lblLeadId") as Label);
                Label lblDispersedAmount = (e.Item.FindControl("lblDispAmount") as Label);
                Label lblBalanceAmount = (e.Item.FindControl("lblBalAmount") as Label);
                Label lblSanctionAmount = (e.Item.FindControl("lblSanctionAmount") as Label);


                //  BLobj.Manager_GetFundingDetailWithDataBound(lblPDId.Text.ToString(), lblLeadId.Text.ToString(), btnPayee, cook.Manager_Id(), lblBalanceAmount, lblDispersedAmount);

                lblDispersedAmount.Text = BLobj.Student_GetProjectTotalFunded(lblPDId.Text, lblLeadId.Text.ToString());
                lblBalanceAmount.Text = Convert.ToString(int.Parse(lblSanctionAmount.Text) - int.Parse(lblDispersedAmount.Text));
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlAllProjectAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlAllProjectAcademicYear.SelectedItem.Text != "[All]")
                {

                    BLobj.GetDatesFromAcademicYear(ddlAllProjectAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlAllProjectAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }

                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptAllProjects.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "DashBoard", ddlAllProjectAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlAllRecordCount.SelectedValue.ToString());
                rptAllProjects.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlAllProjectAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lbldraftedProjectCount.Text = dr[6].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[7].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lbldraftedprojectsleft.Text = dr[6].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[7].ToString();
                }
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlAllProjectAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }

    }


    protected void ddlProposedAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlProposedAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlProposedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlProposedAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                // ddlProposedRecordCount.SelectedIndex = ddlProposedRecordCount.Items.IndexOf(ddlProposedRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptProposedProjectsList.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Proposed", ddlProposedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlProposedRecordCount.SelectedValue.ToString());
                rptProposedProjectsList.DataBind();
                SetLableCount(ddlProposedAcademicYear.SelectedValue.ToString());
                SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                //lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlProposedAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }

    }

    // added function 
    protected void ddldraftAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddldraftedAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddldraftedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddldraftedAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                // ddlProposedRecordCount.SelectedIndex = ddlProposedRecordCount.Items.IndexOf(ddlProposedRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptdraftedProject.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Draft", ddldraftedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddldraftedRecordCount.SelectedValue.ToString());
                rptdraftedProject.DataBind();
                SetLableCount(ddldraftedAcademicYear.SelectedValue.ToString());
                SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                //lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddldraftedAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }

    }


    protected void ddlApprovedAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlApprovedAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlApprovedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlApprovedAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                // ddlApprovedRecordCount.SelectedIndex = ddlApprovedRecordCount.Items.IndexOf(ddlApprovedRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptApprovedProjects.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Approved", ddlApprovedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlApprovedRecordCount.SelectedValue.ToString());
                rptApprovedProjects.DataBind();
                SetLableCount(ddlApprovedAcademicYear.SelectedValue.ToString());
                SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                // lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlProposedAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }

    }

    protected void ddlRequestForModificationAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlRequestForModificationAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlRequestForModificationAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlRequestForModificationAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                //ddlRMRcordCount.SelectedIndex = ddlRMRcordCount.Items.IndexOf(ddlRMRcordCount.Items.FindByValue(cook.Manager_Record()));
                rptRequestForModifiation.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "RequestForModification", ddlRequestForModificationAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRMRcordCount.SelectedValue.ToString());
                rptRequestForModifiation.DataBind();
                SetLableCount(ddlRequestForModificationAcademicYear.SelectedValue.ToString());
                SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                //lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlRequestForModificationAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception)
        {

        }

    }

    protected void ddlRequestForCompletionSearchAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlRequestForCompletionSearchAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                // ddlRCRecordCount.SelectedIndex = ddlRCRecordCount.Items.IndexOf(ddlRCRecordCount.Items.FindByValue(cook.Manager_Record()));

                SetLableCount(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString());
                rptRequestForCompletion.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "RequestForCompletion", ddlRequestForCompletionSearchAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRCRecordCount.SelectedValue.ToString());
                rptRequestForCompletion.DataBind();
                SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                //lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlCompletionAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlCompletionAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlCompletionAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlCompletionAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                //ddlCompletedRecordCount.SelectedIndex = ddlCompletedRecordCount.Items.IndexOf(ddlCompletedRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptCompletedProject.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Completed", ddlCompletionAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlCompletedRecordCount.SelectedValue.ToString());
                rptCompletedProject.DataBind();
                SetLableCount(ddlCompletionAcademicYear.SelectedValue.ToString());
                SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                //lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlCompletionAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }
        }
        catch (Exception)
        {

        }
    }
    protected void ddlRejectedProjectAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlRejectedProjectAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlRejectedProjectAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlRejectedProjectAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                // ddlRejectedRecordCount.SelectedIndex = ddlRejectedRecordCount.Items.IndexOf(ddlRejectedRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptRejected.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Rejected", ddlRejectedProjectAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRejectedRecordCount.SelectedValue.ToString());
                rptRejected.DataBind();

                SetLableCount(ddlRejectedProjectAcademicYear.SelectedValue.ToString());
                SetRegistrationCount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
                //  lblTotalTshirt.Text = setTshirtRequestedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
                lblFundAmountLeft.Text = setFundingAmountCount(cook.Manager_Id(), ddlRejectedProjectAcademicYear.SelectedValue.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }
        }
        catch (Exception)
        {

        }
    }

    public void SetLableCount(string AcademicCode)
    {
        try
        {

            System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(AcademicCode.ToString(), FromDate.ToString(), ToDate.ToString(), cook.Manager_Id());
            foreach (DataRow dr in dt.Rows)
            {
                lblTotalProjectLeft.Text = dr[0].ToString();
                lblProposedProjectLeft.Text = dr[1].ToString();
                lblApprovedProjectsLeft.Text = dr[2].ToString();
                lblRejectedProjectLeft.Text = dr[3].ToString();
                lblRequestForModification.Text = dr[4].ToString();
                lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                lbldraftedprojectsleft.Text = dr[6].ToString();
                lblCompletedProjectsLeft.Text = dr[7].ToString();
            }
        }
        catch (Exception)
        {

        }
    }
    public void SetRegistrationCount(string ManagerId, string FromDate, string ToDate)
    {
        try
        {
            System.Data.DataTable dt = BLobj.GetDashboardRegistrationCount(ManagerId.ToString(), FromDate.ToString(), ToDate.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                lblRegistration.Text = dr[0].ToString();

            }
        }
        catch (Exception)
        {

        }
    }
    public String setTshirtRequestedCount(string ManagerId, string AcademicCode)
    {
        try
        {
            string count = BLobj.Manager_DashboardGetTshirtRequestCount(ManagerId.ToString(), AcademicCode.ToString());
            return count;
        }
        catch (Exception)
        {
            return "0";
        }
    }
    public string setFundingAmountCount(string ManagerId, string AcademicCode)
    {
        try
        {
            string count = BLobj.Manager_DashboardGetFundingAmountCount(ManagerId.ToString(), AcademicCode.ToString());
            return count;
        }
        catch (Exception)
        {
            return "0";
        }
    }
    protected void btnSchedule_SaveAmount_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblSchedule_RequestedAmount.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SchedulePay();", true);
                string msg = "Amount Not Requested..!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (lblSchedule_SanctionAmount.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SchedulePay();", true);
                string msg = "Sanction is not done..!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (int.Parse(txtSchedule_GivingAmount.Text) > int.Parse(lblSchedule_SanctionAmount.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SchedulePay();", true);
                string msg = "Amount Exiding Sanction Amount!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (int.Parse(lblSchedule_SanctionAmount.Text) == (int.Parse(lblSchedule_TotalGivenAmount.Text)))
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SchedulePay();", true);
                string msg = "All Settlements Done.!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (int.Parse(txtSchedule_GivingAmount.Text) > (int.Parse(lblSchedule_BalanceAmount.Text)))
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SchedulePay();", true);
                string msg = "Giving More Amount.!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

            else if ((int.Parse(txtSchedule_GivingAmount.Text) == 0) && (int.Parse(lblSchedule_BalanceAmount.Text) > 0))
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SchedulePay();", true);
                string msg = "Value Cannot be Zero for sanction Amount.!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                BLobj.Manager_SaveFundDetails(lblPop_SchedulePDId.Text, cook.Manager_Id(), lblPop_ScheduleLeadId.Text.ToString(), txtSchedule_GivingAmount, txtSchedule_ManagerRemark);
                //string msg = "All Settlements Done.!!";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), cook.LeadId(), lblSchedule_ProjectTitle.Text.ToString() + ",Amount:" + txtSchedule_GivingAmount.Text.ToString(), "FundDetails(Manager)", "");
                string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                GCMNotification.AndroidPush(DeviceID.ToString(), "Congratulations, Your project" + " " + lblSchedule_ProjectTitle.Text.ToString() + "  Amount has been Credited", "Student", "Empty");

                Response.Redirect("DashBoard.aspx?vwType=DashBoard");

            }
        }
        catch (Exception)
        {

        }
    }


    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            string Path = BLobj.Manager_GetManagerProfilePicPath(cook.Manager_Id());
            if (Path != "")
            {
                ImgManagerProfilePic.ImageUrl = Path.ToString();

            }
            else
            {
                ImgManagerProfilePic.ImageUrl = "../CSS/Images/NoImage.png";
            }
            lblChangePasswordTitle.Text = "Manager Password Change Form";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "Pop_ChangePassword();", true);
        }
        catch (Exception)
        {

        }
    }
    protected void btnSaveProfileImage_Click(object sender, EventArgs e)
    {
        try
        {


            if (ProfilePic.PostedFile != null)
            {
                string ManagerId = cook.Manager_Id();

                string FilePath = ConfigurationManager.AppSettings["ProfilePicPath"].ToString();

                string FileExtenssion = System.IO.Path.GetExtension(ProfilePic.PostedFile.FileName);
                string FileName = ManagerId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    BLobj.Manager_UpdateOnlyManagerProfileImage(ManagerId.ToString(), FilePath + FileName);
                    ProfilePic.SaveAs(Server.MapPath(FilePath + FileName));
                    Response.Redirect("DashBoard.aspx?vwType=DashBoard");

                }
            }
        }
        catch (Exception)
        {

        }
    }



    protected void btnChangePasswordEvent_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";



            if (ProfilePic.PostedFile != null)
            {
                string LeadId = cook.LeadId();

                string FilePath = ConfigurationManager.AppSettings["ProfilePicPath"].ToString();

                string FileExtenssion = System.IO.Path.GetExtension(ProfilePic.PostedFile.FileName);
                string FileName = LeadId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    BLobj.Manager_UpdateOnlyManagerProfileImage(cook.Manager_Id(), FilePath + FileName);
                    ProfilePic.SaveAs(Server.MapPath(FilePath + FileName));
                    msg = "Profile Pic Updated!!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                }
            }
            string Result = BLobj.Manager_UpdatePassword(cook.Manager_Id(), txtOldPassword.Text.ToString(), txtNewPassword.Text.ToString());
            if (Result == "Updated")
            {
                Response.Redirect("~/Default.aspx?ResetPassword=Successs");
            }
            else if (Result == "Password Not Match")
            {
                msg = "Old Password Not Correct";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }



        }
        catch (Exception)
        {

        }
    }

    protected void ddlRegistrationCollegeSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            System.Data.DataTable dt = BLobj.Manager_GetFeesUnPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
            rptisNotPaid.DataSource = dt;
            rptisNotPaid.DataBind();
            lblTotalUnPaidStrenth.Text = dt.Rows.Count.ToString();
            dt = BLobj.Manager_GetFeesPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
            rptisPaid.DataSource = dt;
            rptisPaid.DataBind();
            lblTotalPaidStrenth.Text = dt.Rows.Count.ToString();
        }
        catch (Exception)
        {

        }
    }


    protected void btnupdateToPaid_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rptisNotPaid.Items)
            {
                Label lblLead_Id = item.FindControl("lblLeadId") as Label;

                System.Web.UI.WebControls.CheckBox Chk = item.FindControl("ChkPaid") as System.Web.UI.WebControls.CheckBox;
                if (Chk.Checked == true)
                {
                    BLobj.Manager_UpdateUnPaidToPaid(cook.Manager_Id(), lblLead_Id.Text.ToString());
                    string DeviceID = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), "Registration Completed, Fees Paid", "Student", "Empty");
                    }

                }
            }
            System.Data.DataTable dt = BLobj.Manager_GetFeesPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
            rptisPaid.DataSource = dt;
            rptisPaid.DataBind();
            lblTotalPaidStrenth.Text = dt.Rows.Count.ToString();
            dt = BLobj.Manager_GetFeesUnPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
            rptisNotPaid.DataSource = dt;
            rptisNotPaid.DataBind();
            lblTotalUnPaidStrenth.Text = dt.Rows.Count.ToString();

            string msg = "Successfully completed Registration Process";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch (Exception)
        {
            string msg = "some thing went wrong";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }

    protected void btnUpdateToUnpaid_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rptisPaid.Items)
            {
                Label lblLead_Id = item.FindControl("lblLeadId") as Label;

                System.Web.UI.WebControls.CheckBox Chk = item.FindControl("ChkUnPaid") as System.Web.UI.WebControls.CheckBox;
                if (Chk.Checked == true)
                {
                    BLobj.Manager_UpdatePaidToUnPaid(cook.Manager_Id(), lblLead_Id.Text.ToString());
                }
            }
            System.Data.DataTable dt = BLobj.Manager_GetFeesPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
            rptisPaid.DataSource = dt;
            rptisPaid.DataBind();
            lblTotalPaidStrenth.Text = dt.Rows.Count.ToString();
            dt = BLobj.Manager_GetFeesUnPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());
            rptisNotPaid.DataSource = dt;
            rptisNotPaid.DataBind();
            lblTotalUnPaidStrenth.Text = dt.Rows.Count.ToString();
            string msg = "Successfully shifted student to unpaid";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch (Exception)
        {
            string msg = "some thing went wrong";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }

    protected void ddlTshirtCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetTshirtCountForLabels();
        rptTshirtAll.DataSource = BLobj.Manager_GetTshirtAllList(cook.Manager_Id(), ddlTshirtCollege.SelectedValue.ToString());
        rptTshirtAll.DataBind();
        TshirtAll.Visible = true;
        Sanction.Visible = false;
        rejected.Visible = false;
        tshirtapprovelabels.Visible = false;
        rptTshirtRequestedList.Visible = false;
        rptTshirtSanctionedList.Visible = false;
    }

    protected void btnTshirtApproval_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int AllotedCount = 0;
            int UsedCount = 0;
            int TotalStock = 0;

            if (lblClickedSize.Text == "S")
            {
                AllotedCount = int.Parse(lblSTshirtAlloted.Text.ToString());
                UsedCount = int.Parse(lblSTshirtUsedCount.Text.ToString());
            }
            else if (lblClickedSize.Text == "M")
            {
                AllotedCount = int.Parse(lblMTshirtAllotedCount.Text.ToString());
                UsedCount = int.Parse(lblMTshirtUsedCount.Text.ToString());
            }
            else if (lblClickedSize.Text == "L")
            {
                AllotedCount = int.Parse(lblLTshirtAllotedCount.Text.ToString());
                UsedCount = int.Parse(lblLTshirtCountUsedCount.Text.ToString());
            }
            else if (lblClickedSize.Text == "XL")
            {
                AllotedCount = int.Parse(lblXLTshirtAllotedCount.Text.ToString());
                UsedCount = int.Parse(lblXLTshirtCountUsedCount.Text.ToString());
            }
            else if (lblClickedSize.Text == "XXL")
            {
                AllotedCount = int.Parse(lblXXLTshirtAllotedCount.Text.ToString());
                UsedCount = int.Parse(lblXXLTshirtCountUsedCount.Text.ToString());
            }
            TotalStock = AllotedCount - UsedCount;
            foreach (RepeaterItem item in rptTshirtRequestedList.Items)
            {
                CheckBox Chk = item.FindControl("chkTshirtApprove") as CheckBox;
                if (Chk.Checked == true)
                {
                    count++;
                }
            }

            if (count <= TotalStock)
            {
                foreach (RepeaterItem item in rptTshirtRequestedList.Items)
                {
                    CheckBox Chk = item.FindControl("chkTshirtApprove") as CheckBox;
                    if (Chk.Checked == true)
                    {
                        Label lblRequestedId = item.FindControl("lblRequestedId") as Label;
                        Label lblLead_Id = item.FindControl("lblLeadId") as Label;
                        BLobj.Manager_ApprovalOfStudentTshirt(lblRequestedId.Text, lblLead_Id.Text, cook.Manager_Id(), lblClickedSize.Text, BLobj.GetTop1AademicCode());
                        string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                        if (DeviceID != "")
                        {
                            FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request approved kindly collect it from your Mentor", "Request", "Empty");
                        }

                    }
                }

                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;




                Response.Redirect("Dashboard.aspx?vwType=Tshirt");
            }
            else
            {
                string msg = "Selected More than Stock Select Only " + AllotedCount.ToString() + " " + "Students";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnTshirtNonApproval_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rptTshirtSanctionedList.Items)
            {
                CheckBox Chk = item.FindControl("chkTshirtSanctionReturn") as CheckBox;
                if (Chk.Checked == true)
                {
                    Label lblRequestedId = item.FindControl("lblRequestedId") as Label;
                    Label lblLead_Id = item.FindControl("lblLeadId") as Label;
                    BLobj.Manager_TshirtApprovalRollBack(lblRequestedId.Text, lblLead_Id.Text, cook.Manager_Id(), lblClickedSize.Text, BLobj.GetTop1AademicCode());
                }
            }
            tshirtapprovelabels.Visible = true;
            rptTshirtRequestedList.Visible = true;
            rptTshirtSanctionedList.Visible = true;

            Response.Redirect("Dashboard.aspx?vwType=Tshirt");
        }
        catch (Exception)
        {

        }
    }

    protected void btnAllTshirtList_Click(object sender, EventArgs e)
    {
        try
        {
            rptTshirtAll.DataSource = BLobj.Manager_GetTshirtAllList(cook.Manager_Id(), ddlTshirtCollege.SelectedValue.ToString());
            rptTshirtAll.DataBind();
            TshirtAll.Visible = true;
            Sanction.Visible = false;
            rejected.Visible = false;
            tshirtapprovelabels.Visible = false;
            rptTshirtRequestedList.Visible = false;
            rptTshirtSanctionedList.Visible = false;
        }
        catch (Exception)
        {

        }
    }
    protected void btnSTshirtList_Click(object sender, EventArgs e)
    {
        try
        {

            if ((lblSTshirtAlloted.Text != "0") && (int.Parse(lblSTshirtAlloted.Text) != int.Parse(lblSTshirtUsedCount.Text)))
            {
                lblClickedSize.Text = "S";
                rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtRequestedList(cook.Manager_Id(), "S", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtRequestedList.DataBind();
                lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();

                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "S", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();

                Sanction.Visible = true;
                rejected.Visible = false;
                TshirtAll.Visible = false;
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;

            }
            else
            {
                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "S", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                string msg = "S - Size T-Shirt is Out of Stock";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnMTshirtList_Click(object sender, EventArgs e)
    {
        try
        {
            if ((lblMTshirtAllotedCount.Text != "0") && (int.Parse(lblMTshirtAllotedCount.Text) != int.Parse(lblMTshirtUsedCount.Text)))
            {
                lblClickedSize.Text = "M";
                rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtRequestedList(cook.Manager_Id(), "M", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtRequestedList.DataBind();
                lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();


                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "M", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();
                Sanction.Visible = true;
                rejected.Visible = false;
                TshirtAll.Visible = false;
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
            }
            else
            {
                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "M", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                string msg = "M - Size T-Shirt is Out of Stock";
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnLTshirtList_Click(object sender, EventArgs e)
    {
        try
        {
            if ((lblLTshirtAllotedCount.Text != "0") && (int.Parse(lblLTshirtAllotedCount.Text) != int.Parse(lblLTshirtCountUsedCount.Text)))
            {
                lblClickedSize.Text = "L";
                rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtRequestedList(cook.Manager_Id(), "L", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtRequestedList.DataBind();
                lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();

                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "L", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();

                Sanction.Visible = true;
                rejected.Visible = false;
                TshirtAll.Visible = false;
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
            }
            else
            {
                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "L", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();
                string msg = "L - Size T-Shirt is Out of Stock";
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnXLTshirtList_Click(object sender, EventArgs e)
    {
        try
        {
            if ((lblXLTshirtAllotedCount.Text != "0") && (int.Parse(lblXLTshirtAllotedCount.Text) != int.Parse(lblXLTshirtCountUsedCount.Text)))
            {
                lblClickedSize.Text = "XL";
                rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtRequestedList(cook.Manager_Id(), "XL", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtRequestedList.DataBind();
                lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();

                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "XL", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();

                Sanction.Visible = true;
                rejected.Visible = false;
                TshirtAll.Visible = false;
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;

            }
            else
            {
                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "XL", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();
                string msg = "XL - Size T-Shirt is Out of Stock";
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnXXLTshirtList_Click(object sender, EventArgs e)
    {
        try
        {
            if ((lblXXLTshirtAllotedCount.Text != "0") && (int.Parse(lblXXLTshirtAllotedCount.Text) != int.Parse(lblXXLTshirtCountUsedCount.Text)))
            {
                lblClickedSize.Text = "XXL";
                rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtRequestedList(cook.Manager_Id(), "XXL", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtRequestedList.DataBind();
                lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();

                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "XXL", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();

                Sanction.Visible = true;
                rejected.Visible = false;
                TshirtAll.Visible = false;
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
            }
            else
            {
                rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "XXL", ddlTshirtCollege.SelectedValue.ToString());
                rptTshirtSanctionedList.DataBind();
                lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();
                string msg = "XXL - Size T-Shirt is Out of Stock";
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }
    protected void btnRejectedTshirtList_Click(object sender, EventArgs e)
    {
        try
        {
            rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), "XXL", ddlTshirtCollege.SelectedValue.ToString());
            rptTshirtRequestedList.DataBind();
            lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();

            rptTshirtRejected.DataSource = BLobj.Manager_GetStudentTshirtRejectedList(cook.Manager_Id(), ddlTshirtCollege.SelectedValue.ToString());
            rptTshirtRejected.DataBind();

            Sanction.Visible = false;
            rejected.Visible = true;
            TshirtAll.Visible = false;
            tshirtapprovelabels.Visible = false;
            rptTshirtRequestedList.Visible = true;
            rptTshirtSanctionedList.Visible = false;
        }
        catch (Exception)
        {

        }
    }

    protected void btnTshirtReject_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton button = (sender as LinkButton);
            RepeaterItem item = button.NamingContainer as RepeaterItem;
            Label lblRequestedId = (item.FindControl("lblRequestedId") as Label);
            CheckBox ChkSize = (item.FindControl("chkTshirtApprove") as CheckBox);
            lblRejectedTshirtPOPSize.Text = ChkSize.Text.ToString();
            lblRejectedTshirtPOPRequestedId.Text = lblRequestedId.Text.ToString();
            txtTshirtRejectReson.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_TshirtReject();", true);
        }
        catch (Exception)
        {

        }
    }
    protected void btnRejectRequestedTshirt_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Manager_UpdateRejectTshirtStudentRequest(lblRejectedTshirtPOPRequestedId.Text, txtTshirtRejectReson.Text.ToString());

            BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), cook.LeadId(), lblRejectedTshirtPOPSize.Text + " " + ":T-shirt request Rejected(manager)", "Rejected", "");
            string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
            GCMNotification.AndroidPush(DeviceID.ToString(), "T-shirt request Rejected(manager)", "Request", "Empty");

            SetTshirtCountForLabels();
            rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtRequestedList(cook.Manager_Id(), lblRejectedTshirtPOPSize.Text, ddlTshirtCollege.SelectedValue.ToString());
            rptTshirtRequestedList.DataBind();
            lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();

            rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), lblRejectedTshirtPOPSize.Text, ddlTshirtCollege.SelectedValue.ToString());
            rptTshirtSanctionedList.DataBind();
            lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();
            tshirtapprovelabels.Visible = false;
            rptTshirtRequestedList.Visible = true;
            rptTshirtSanctionedList.Visible = false;
            string msg = "Rejected Successfully";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);

        }
        catch (Exception)
        {

        }
    }
    protected void btnExchange_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton button = (sender as LinkButton);
            RepeaterItem item = button.NamingContainer as RepeaterItem;
            Label lblRequestedId = (item.FindControl("lblRequestedId") as Label);
            CheckBox ChkSize = (item.FindControl("chkTshirtSanctionReturn") as CheckBox);
            lblTshirtExchangeSize.Text = ChkSize.Text.ToString();
            lblTshirtExchangeRequestedId.Text = lblRequestedId.Text.ToString();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_TshirtExchange();", true);
        }
        catch (Exception)
        {

        }
    }
    protected void btnTshirtExChangeByManager_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = BLobj.Manager_UpdateExchangedTshirtSize(lblTshirtExchangeRequestedId.Text, lblTshirtExchangeSize.Text, BLobj.GetTop1AademicCode(), cook.Manager_Id(), ddlTshirtExchangeSize.SelectedValue.ToString(), ddlTshirtExchangeReson.SelectedValue.ToString());
            if (Status == "Exchange is Done")
            {
                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), cook.LeadId(), ddlTshirtExchangeSize.SelectedValue.ToString() + " " + ":T-shirt request ExChange(manager)", "ExChange", "");
                string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                if (DeviceID != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request ExChange(manager)", "Request", "Empty");
                }


                //SetTshirtCountForLabels();
                //rptTshirtRequestedList.DataSource = BLobj.Manager_GetStudentTshirtRequestedList(cook.Manager_Id(), lblTshirtExchangeSize.Text, ddlTshirtCollege.SelectedValue.ToString());
                //rptTshirtRequestedList.DataBind();
                //lblRequestedTshirtCount.Text = rptTshirtRequestedList.Items.Count.ToString();

                //rptTshirtSanctionedList.DataSource = BLobj.Manager_GetStudentTshirtSanctionList(cook.Manager_Id(), lblTshirtExchangeSize.Text, ddlTshirtCollege.SelectedValue.ToString());
                //rptTshirtSanctionedList.DataBind();
                //lblApprovedTshirtCount.Text = rptTshirtSanctionedList.Items.Count.ToString();
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                string msg = Status;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_TshirtExchange();", true);

                string msg = Status + " " + "Please Select Different Size";
                tshirtapprovelabels.Visible = true;
                rptTshirtRequestedList.Visible = true;
                rptTshirtSanctionedList.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

            Response.Redirect("Dashboard.aspx?vwType=Tshirt");
        }
        catch (Exception)
        {

        }
    }
    public void SetTshirtCountForLabels()
    {
        DataTable dt = BLobj.Manager_GetTshirtAllotedUsedCount(cook.Manager_Id(), BLobj.GetTop1AademicCode());
        if (dt.Rows.Count > 0)
        {
            lblTotalTshirtUsed.Text = dt.Rows[0].ItemArray[0].ToString();
            lblTotalTshirtAlloted.Text = dt.Rows[0].ItemArray[1].ToString();

            lblSTshirtUsedCount.Text = dt.Rows[0].ItemArray[2].ToString();
            lblSTshirtAlloted.Text = dt.Rows[0].ItemArray[3].ToString();

            lblMTshirtUsedCount.Text = dt.Rows[0].ItemArray[4].ToString();
            lblMTshirtAllotedCount.Text = dt.Rows[0].ItemArray[5].ToString();

            lblLTshirtCountUsedCount.Text = dt.Rows[0].ItemArray[6].ToString();
            lblLTshirtAllotedCount.Text = dt.Rows[0].ItemArray[7].ToString();

            lblXLTshirtCountUsedCount.Text = dt.Rows[0].ItemArray[8].ToString();
            lblXLTshirtAllotedCount.Text = dt.Rows[0].ItemArray[9].ToString();

            lblXXLTshirtCountUsedCount.Text = dt.Rows[0].ItemArray[10].ToString();
            lblXXLTshirtAllotedCount.Text = dt.Rows[0].ItemArray[11].ToString();

        }

        lblTopSTshirtCollegeWiseCount.Text = "0";
        lblTopMTshirtCollegeWiseCount.Text = "0";
        lblTopLTshirtCollegeWiseCount.Text = "0";
        lblTopXLTshirtCollegeWiseCount.Text = "0";
        lblTopXXLTshirtCollegeWiseCount.Text = "0";
        dt = BLobj.Manager_GetTshirtCollegeWiseRequestCount(ddlTshirtCollege.SelectedValue.ToString(), cook.Manager_Id());
        int TotalCount = 0;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i].ItemArray[0].ToString() == "S")
                {
                    lblTopSTshirtCollegeWiseCount.Text = dt.Rows[i].ItemArray[1].ToString();
                    TotalCount += int.Parse(dt.Rows[i].ItemArray[1].ToString());
                }
                else if (dt.Rows[i].ItemArray[0].ToString() == "M")
                {
                    lblTopMTshirtCollegeWiseCount.Text = dt.Rows[i].ItemArray[1].ToString();
                    TotalCount += int.Parse(dt.Rows[i].ItemArray[1].ToString());
                }
                else if (dt.Rows[i].ItemArray[0].ToString() == "L")
                {
                    lblTopLTshirtCollegeWiseCount.Text = dt.Rows[i].ItemArray[1].ToString();
                    TotalCount += int.Parse(dt.Rows[i].ItemArray[1].ToString());
                }
                else if (dt.Rows[i].ItemArray[0].ToString() == "XL")
                {
                    lblTopXLTshirtCollegeWiseCount.Text = dt.Rows[i].ItemArray[1].ToString();
                    TotalCount += int.Parse(dt.Rows[i].ItemArray[1].ToString());
                }
                else if (dt.Rows[i].ItemArray[0].ToString() == "XXL")
                {
                    lblTopXXLTshirtCollegeWiseCount.Text = dt.Rows[i].ItemArray[1].ToString();
                    TotalCount += int.Parse(dt.Rows[i].ItemArray[1].ToString());
                }
            }
            lblTopAllTshirtCollegeWiseCount.Text = TotalCount.ToString();
        }
        else
        {
            lblTopSTshirtCollegeWiseCount.Text = "0";
            lblTopMTshirtCollegeWiseCount.Text = "0";
            lblTopLTshirtCollegeWiseCount.Text = "0";
            lblTopXLTshirtCollegeWiseCount.Text = "0";
            lblTopXXLTshirtCollegeWiseCount.Text = "0";
            lblTopAllTshirtCollegeWiseCount.Text = "0";
        }
        dt = BLobj.Manager_GetStudentTshirtRejectedCount(cook.Manager_Id(), ddlTshirtCollege.SelectedValue.ToString());
        if (dt.Rows.Count > 0)
        {
            lblTopRejectedTshirtCollegeWiseCount.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblTopRejectedTshirtCollegeWiseCount.Text = "0";
        }

    }



    protected void ddlAllRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlAllProjectAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlAllProjectAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                    Set_Selected_Academic_Year(ddlAllProjectAcademicYear.SelectedValue.ToString());
                }
                else
                {
                    Set_Selected_Academic_Year("[All]");
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptAllProjects.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "DashBoard", ddlAllProjectAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlAllRecordCount.SelectedValue.ToString());
                rptAllProjects.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlAllProjectAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[6].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[6].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlProposedRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlProposedAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlProposedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptProposedProjectsList.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Proposed", ddlProposedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlProposedRecordCount.SelectedValue.ToString());
                rptProposedProjectsList.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlProposedAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[6].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[6].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }

    // added function for draft record count
    protected void ddldraftRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddldraftedAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddldraftedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptdraftedProject.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Draft", ddldraftedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddldraftedRecordCount.SelectedValue.ToString());
                rptdraftedProject.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddldraftedAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[5].ToString();
                    lblRequestForCompletionCount.Text = dr[6].ToString();
                    lblCompletedProjectsCount.Text = dr[7].ToString();
                    lbldraftedProjectCount.Text = dr[4].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[5].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[6].ToString();
                    lblCompletedProjectsLeft.Text = dr[7].ToString();
                    lblCompletedProjectsCount.Text = dr[4].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }


    protected void ddlApprovedRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlApprovedAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlApprovedAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptApprovedProjects.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Approved", ddlApprovedAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlApprovedRecordCount.SelectedValue.ToString());
                rptApprovedProjects.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlApprovedAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[6].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[6].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlRMRcordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlRequestForCompletionSearchAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptRequestForModifiation.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "RequestForModification", ddlRequestForCompletionSearchAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRMRcordCount.SelectedValue.ToString());
                rptRequestForModifiation.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[6].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[6].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlRCRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlRequestForCompletionSearchAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptRequestForCompletion.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "RequestForCompletion", ddlRequestForCompletionSearchAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRCRecordCount.SelectedValue.ToString());
                rptRequestForCompletion.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlRequestForCompletionSearchAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[6].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[6].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlCompletedRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlCompletionAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlCompletionAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptCompletedProject.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Completed", ddlCompletionAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlCompletedRecordCount.SelectedValue.ToString());
                rptCompletedProject.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlCompletionAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[6].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[6].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlRejectedRecordCount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string ManagerID = cook.Manager_Id();
                if (ddlRejectedProjectAcademicYear.SelectedItem.Text != "[All]")
                {
                    BLobj.GetDatesFromAcademicYear(ddlRejectedProjectAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
                }
                // ddlAllRecordCount.SelectedIndex = ddlAllRecordCount.Items.IndexOf(ddlAllRecordCount.Items.FindByValue(cook.Manager_Record()));
                rptRejected.DataSource = BLobj.Manager_GetProjectDetail(ManagerID.ToString(), "Rejected", ddlRejectedProjectAcademicYear, FromDate.ToString(), ToDate.ToString(), ddlRejectedRecordCount.SelectedValue.ToString());
                rptRejected.DataBind();
                System.Data.DataTable dt = BLobj.GetDashBoardProjectCount(ddlRejectedProjectAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerID.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    lblAllProjectsCount.Text = dr[0].ToString();
                    lblProposedProjectsCount.Text = dr[1].ToString();
                    lblApprovedCount.Text = dr[2].ToString();
                    lblRejectedProjectCount.Text = dr[3].ToString();
                    lblRequestForModificationCount.Text = dr[4].ToString();
                    lblRequestForCompletionCount.Text = dr[5].ToString();
                    lblCompletedProjectsCount.Text = dr[6].ToString();

                    lblTotalProjectLeft.Text = dr[0].ToString();
                    lblProposedProjectLeft.Text = dr[1].ToString();
                    lblApprovedProjectsLeft.Text = dr[2].ToString();
                    lblRejectedProjectLeft.Text = dr[3].ToString();
                    lblRequestForModification.Text = dr[4].ToString();
                    lblRequestForCompletionProjectsLeft.Text = dr[5].ToString();
                    lblCompletedProjectsLeft.Text = dr[6].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnFeesUnpaidExcelSheet_Click(object sender, EventArgs e)
    {
        try
        {

            GridView grd = new GridView();
            grd.DataSource = BLobj.Manager_ExcelGetFeesUnPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());

            grd.DataBind();
            if (grd.Rows.Count > 0)
            {


                string name = cook.ManagerName() + "_" + System.DateTime.Now.ToString() + "_UnpaidStudentList";
                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grd.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                string msg = "No Data Found";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnFeePaidExcelSheet_Click(object sender, EventArgs e)
    {
        try
        {

            GridView grd = new GridView();
            grd.DataSource = BLobj.Manager_ExcelGetFeesPaidStudentList(cook.Manager_Id(), ddlRegistrationCollegeSearch.SelectedValue.ToString());

            grd.DataBind();
            if (grd.Rows.Count > 0)
            {


                string name = cook.ManagerName() + "_" + System.DateTime.Now.ToString() + "_paidStudentList";
                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grd.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                string msg = "No Data Found";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnDashboardSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string vwType = "";
            if (Request.QueryString["vwType"].ToString() != "")
            {
                vwType = Request.QueryString["vwType"].ToString();
                rptAllProjects.DataSource = BLobj.Manager_SearchGetProjectDetail(cook.Manager_Id(), vwType.ToString(), txtStudentSearch.Text.ToString(), ddlDashboardSearchOn.SelectedValue.ToString(), ddlAllProjectAcademicYear, ddlAllRecordCount.SelectedValue.ToString());
                rptAllProjects.DataBind();
            }
        }
        catch (Exception)
        {


        }
    }

    protected void btnSearchProposed_Click(object sender, EventArgs e)
    {
        try
        {
            string vwType = "";
            if (Request.QueryString["vwType"].ToString() != "")
            {
                vwType = Request.QueryString["vwType"].ToString();
                rptProposedProjectsList.DataSource = BLobj.Manager_SearchGetProjectDetail(cook.Manager_Id(), vwType.ToString(), txtProposedSearch.Text.ToString(), ddlProposedSearchOn.SelectedValue.ToString(), ddlProposedAcademicYear, ddlProposedRecordCount.SelectedValue.ToString());
                rptProposedProjectsList.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnSearchApproved_Click(object sender, EventArgs e)
    {
        try
        {
            string vwType = "";
            if (Request.QueryString["vwType"].ToString() != "")
            {
                vwType = Request.QueryString["vwType"].ToString();
                rptApprovedProjects.DataSource = BLobj.Manager_SearchGetProjectDetail(cook.Manager_Id(), vwType.ToString(), txtApprovedSearch.Text.ToString(), ddlApprovedSearchOn.SelectedValue.ToString(), ddlApprovedAcademicYear, ddlApprovedRecordCount.SelectedValue.ToString());
                rptApprovedProjects.DataBind();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearchRequestForModification_Click(object sender, EventArgs e)
    {
        try
        {
            string vwType = "";
            if (Request.QueryString["vwType"].ToString() != "")
            {
                vwType = Request.QueryString["vwType"].ToString();
                rptRequestForModifiation.DataSource = BLobj.Manager_SearchGetProjectDetail(cook.Manager_Id(), vwType.ToString(), txtRequestForModificationSearch.Text.ToString(), ddlRequestForModificationSearchOn.SelectedValue.ToString(), ddlRequestForModificationAcademicYear, ddlRMRcordCount.SelectedValue.ToString());
                rptRequestForModifiation.DataBind();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearchRequestForCompletion_Click(object sender, EventArgs e)
    {
        try
        {
            string vwType = "";
            if (Request.QueryString["vwType"].ToString() != "")
            {
                vwType = Request.QueryString["vwType"].ToString();
                rptRequestForCompletion.DataSource = BLobj.Manager_SearchGetProjectDetail(cook.Manager_Id(), vwType.ToString(), txtRequestForCompletionSearch.Text.ToString(), ddlRequestForCompletionSearchOn.SelectedValue.ToString(), ddlRequestForCompletionSearchAcademicYear, ddlRCRecordCount.SelectedValue.ToString());
                rptRequestForCompletion.DataBind();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearchCompletion_Click(object sender, EventArgs e)
    {
        try
        {
            string vwType = "";
            if (Request.QueryString["vwType"].ToString() != "")
            {
                vwType = Request.QueryString["vwType"].ToString();
                rptCompletedProject.DataSource = BLobj.Manager_SearchGetProjectDetail(cook.Manager_Id(), vwType.ToString(), txtCompletionSearch.Text.ToString(), ddlCompletionSearchOn.SelectedValue.ToString(), ddlCompletionAcademicYear, ddlCompletedRecordCount.SelectedValue.ToString());
                rptCompletedProject.DataBind();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearchRejected_Click(object sender, EventArgs e)
    {
        try
        {
            string vwType = "";
            if (Request.QueryString["vwType"].ToString() != "")
            {
                vwType = Request.QueryString["vwType"].ToString();
                rptRejected.DataSource = BLobj.Manager_SearchGetProjectDetail(cook.Manager_Id(), vwType.ToString(), txtRejectedProjectSearch.Text.ToString(), ddlRejectSearchOn.SelectedValue.ToString(), ddlRejectedProjectAcademicYear, ddlRejectedRecordCount.SelectedValue.ToString());
                rptRejected.DataBind();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnFundingAmountWithoutComments_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(txtFundingAmount.Text) > int.Parse(lblFundingAmountBalance.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_FundAmount();", true);
                string msg = "Amount is more than balance";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                lblFundingAmountBalance.Visible = false;
                lblFundingAmountPDID.Visible = false;
                lblFundingLeadId.Visible = false;
                lblFundingProjectTitle.Visible = false;
            }
            else
            {
                BLobj.Manager_SaveFundDetails(lblFundingAmountPDID.Text, cook.Manager_Id(), lblFundingLeadId.Text.ToString(), txtFundingAmount, txtFundComments);
                //string msg = "All Settlements Done.!!";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), lblFundingLeadId.Text.ToString(), lblSchedule_ProjectTitle.Text.ToString() + ",Amount:" + txtFundingAmount.Text.ToString(), "FundDetails(Manager)", "");
                string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                if (DeviceID != "")
                {
                    GCMNotification.AndroidPush(DeviceID.ToString(), "Congratulations, Your project" + " " + lblSchedule_ProjectTitle.Text.ToString() + "  Amount has been Credited", "Student", "Empty");
                }
                Response.Redirect(Request.Url.AbsoluteUri);
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void rptFundAmount_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Label lblBalanceAmount = (e.Item.FindControl("lblBalanceAmount") as Label);
                Label lblReleaseAmount = (e.Item.FindControl("lblReleaseAmount") as Label);
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("FundingPOP");
                if (int.Parse(lblBalanceAmount.Text) != 0)
                {
                    //tr.Attributes.Add("style", "background-color:aliceblue;");

                }
                else if (int.Parse(lblReleaseAmount.Text) > 0)
                {
                    tr.Attributes.Add("style", "background-color:cornsilk;");

                }
                else if (int.Parse(lblBalanceAmount.Text) == 0)
                {
                    tr.Attributes.Add("style", "background-color:lightgreen;");
                }




            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlFundAmountAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = BLobj.FillFundingAmount(cook.Manager_Id(), ddlFundAmountAcademicYear.SelectedValue.ToString());

            rptFundAmount.DataSource = dt;
            rptFundAmount.DataBind();
            if (dt.Rows.Count >= 1)
            {
                lblFundAmountCount.Text = "Count :" + dt.Rows.Count.ToString();
            }


        }
        catch (Exception)
        {


        }
    }
    protected void btnChat_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string PDID = "";
            if (cook.Manager_Id() != "")
            {
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('^');
                int i;
                for (i = 0; i <= 2; i++)
                {
                    if (i == 0)
                    {
                        PDID = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        lblChatProjectTitle.Text = itemlist[1].ToString();
                    }
                    else if (i == 2)
                    {
                        lblChatProjectStatus.Text = itemlist[2].ToString();
                    }

                }
                lblChatPDID.Text = PDID.ToString();
                BLobj.Student_UpdateChatRead(PDID.ToString(), "Student");
                DataTable dt = BLobj.Student_GetProjectDiscussionForum(PDID.ToString());
                rptDiscussionForum.DataSource = dt;
                rptDiscussionForum.DataBind();
                txtChatMessage.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Chat();", true);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnSaveCommentChat_Click(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                BLobj.Manager_SaveProjectDiscussionForum(lblChatPDID.Text.ToString(), txtChatMessage.Text.ToString(), cook.Manager_Id(), "Discussion", lblChatProjectStatus.Text.ToString());
                txtChatMessage.Text = "";
                string msg = "Message Sent!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Chat();", false);
                //DataTable dt = BLobj.Student_GetProjectDiscussionForum(lblChatPDID.Text.ToString());
                //rptDiscussionForum.DataSource = dt;
                //rptDiscussionForum.DataBind();
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = BLobj.Student_GetProjectDiscussionForum(lblChatPDID.Text.ToString());
    //        rptDiscussionForum.DataSource = dt;
    //        rptDiscussionForum.DataBind();
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

    //}
    protected void btnProposedProject_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string PDId = "";
            string LeadId = "";
            if (cook.Manager_Id() != "")
            {
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        LeadId = itemlist[1].ToString();
                    }
                }
                string ProjectStatus = "Proposed";
                Server.Transfer("Manager_ApproveProject.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString() + "&Lead_Id=" + LeadId.ToString(), false);

            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnApproveProject_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string PDId = "";
            string LeadId = "";
            if (cook.Manager_Id() != "")
            {
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        LeadId = itemlist[1].ToString();
                    }

                }
                string ProjectStatus = "Approved";
                Server.Transfer("Manager_ApproveProject.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString() + "&Lead_Id=" + LeadId.ToString(), false);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }

        }
        catch (Exception)
        {

        }
    }
    protected void btnRequestForModification_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string PDId = "";
            string LeadId = "";
            if (cook.Manager_Id() != "")
            {
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        LeadId = itemlist[1].ToString();
                    }

                }

                string ProjectStatus = "RequestForModification";
                Server.Transfer("Manager_ApproveProject.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString() + "&Lead_Id=" + LeadId.ToString(), false);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void lblProjectStatus_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string PDId = "";
            string LeadId = "";
            if (cook.Manager_Id() != "")
            {
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        LeadId = itemlist[1].ToString();
                    }
                }
                string ProjectStatus = "RequestForCompletion";
                Server.Transfer("Manager_CompletionProject.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString() + "&Lead_Id=" + LeadId.ToString(), false);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btndraftedProjectStatus(object sender, CommandEventArgs e)
    {
        try
        {

            string PDId = "";
            string LeadId = "";
            if (cook.Manager_Id() != "")
            {
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        LeadId = itemlist[1].ToString();
                    }
                }
                string ProjectStatus = "Draft";
                Server.Transfer("Manager_CompletionProject.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString() + "&Lead_Id=" + LeadId.ToString(), false);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnCompletedProjectStatus_Command(object sender, CommandEventArgs e)
    {
        try
        {

            string PDId = "";
            string LeadId = "";
            if (cook.Manager_Id() != "")
            {
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        LeadId = itemlist[1].ToString();
                    }
                }
                string ProjectStatus = "Completed";
                Server.Transfer("Manager_CompletionProject.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString() + "&Lead_Id=" + LeadId.ToString(), false);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }

        }
        catch (Exception)
        {

        }
    }
}

public class Meterials
{
    public string MeterialName { get; set; }
    public string MeterialCost { get; set; }
    public string Slno { get; set; }

}