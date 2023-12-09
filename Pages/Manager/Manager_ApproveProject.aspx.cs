using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_Manager_ApproveProject : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();

    vmCookies cook = new vmCookies();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                if ((Request.QueryString["PDID"].ToString() != null) || (Request.QueryString["PDID"].ToString() != "")
                    && (Request.QueryString["ProjectStatus"].ToString() != null) || (Request.QueryString["ProjectStatus"].ToString() != "")
                    && (Request.QueryString["Lead_Id"].ToString() != null) || (Request.QueryString["Lead_Id"].ToString()) != "")
                {

                    lblPDID.Text = Request.QueryString["PDID"].ToString();
                    lblProjectStatus.Text = Request.QueryString["ProjectStatus"].ToString();
                    lblLead_Id.Text = Request.QueryString["Lead_Id"].ToString();

                    if (cook.Manager_Id() != "")
                    {
                        string ManagerID = cook.Manager_Id();

                        BLobj.FillThemeMaster(ddlTheme);

                        txtProposedStartDate.Text = "";
                        txtProposedEndDate.Text = "";
                        DataTable dt = BLobj.Manager_GetProjectDetailAndModify_NEW(ManagerID.ToString(), lblProjectStatus.Text.ToString(), lblPDID.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                DateTime EndDate;
                                DateTime StartDate;
                                lblProjectId.Text = dr[0].ToString();
                                txtProjectTitle.Text = dr[3].ToString();
                                txtStudentName.Text = dr[4].ToString();
                                txtCollegeName.Text = dr[6].ToString();
                                txtMobileNo.Text = dr[8].ToString();
                                txtBudget.Text = dr[9].ToString();
                                txtSemName.Text = dr["SemName"].ToString();
                                txtTotalBeneficiaries.Text = dr[11].ToString();
                                txtBeneficiaries.Text = dr[12].ToString();
                                txtDuration.Text = dr[13].ToString();
                                txtAmount.Text = dr[14].ToString();
                                ddlTheme.SelectedIndex = ddlTheme.Items.IndexOf(ddlTheme.Items.FindByValue(dr[15].ToString()));
                                txtCurrentSituation.Text = dr[16].ToString();
                                txProjectObjective.Text = dr[17].ToString();
                                txtActionPlan.Text = dr[18].ToString();
                                txtManagerComments.Text = dr[19].ToString();
                                txtPlaceofImplement.Text = dr[25].ToString();
                                if ((dr[26].ToString() != "0") || (dr[27].ToString() != "0"))
                                {
                                    StartDate = DateTime.Parse(dr[26].ToString());
                                    EndDate = DateTime.Parse(dr[27].ToString());
                                    int Days = int.Parse((EndDate.Date - StartDate.Date).TotalDays.ToString());
                                    if (Days == 0)
                                    {
                                        Days = 1;
                                    }
                                    else
                                    {
                                        Days = Days + 1;
                                    }
                                    lblProposedProjectTargetDays.Text = Days.ToString();
                                    txtProposedStartDate.Text = dr[26].ToString();
                                    txtProposedEndDate.Text = dr[27].ToString();
                                }
                                if (dr[28].ToString() != "0")
                                {
                                    ChkIsImpact.Checked = true;
                                }
                                else
                                {
                                    ChkIsImpact.Checked = false;
                                }
                                lblTotalAmount.Text = dr[29].ToString();
                            }
                            BLobj.Manager_GetMeterialDetails_NEW(lblPDID.Text.ToString(), rptMeterial);
                            BLobj.Manager_GetTeamMemberDetails_NEW(lblPDID.Text.ToString(), rptTeamMembers);
                        }
                        else
                        {
                            lblError.Text = "No Data Found";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                        }
                        if (lblProjectStatus.Text == "Proposed")
                        {
                            if (int.Parse(txtBudget.Text.ToString()) == 0)
                            {
                                txtAmount.Text = "0";
                                txtAmount.Enabled = false;
                                btnAddMeterial.Enabled = false;

                            }
                            else
                            {
                                txtAmount.Text = "";
                                txtAmount.Enabled = true;
                                btnAddMeterial.Enabled = true;

                            }
                            lblProjectStatus.Text = "Proposed";
                        }
                        else if (lblProjectStatus.Text == "Approved")
                        {
                            if (int.Parse(txtBudget.Text.ToString()) == 0)
                            {
                                txtAmount.Text = "0";

                            }
                            lblProjectId.Enabled = false;
                            txtProjectTitle.Enabled = false;
                            txtStudentName.Enabled = false;
                            txtCollegeName.Enabled = false;
                            txtMobileNo.Enabled = false;
                            txtBudget.Enabled = false;
                            txtTotalBeneficiaries.Enabled = false;
                            txtBeneficiaries.Enabled = false;
                            txtDuration.Enabled = false;
                            txtAmount.Enabled = false;
                            ddlTheme.Enabled = false;
                            txtPlaceofImplement.Enabled = false;
                            txtCurrentSituation.Enabled = false;
                            txProjectObjective.Enabled = false;
                            txtActionPlan.Enabled = false;
                            txtManagerComments.Enabled = true;
                            btnAddMeterial.Visible = false;
                            btnApproved.Visible = true;
                            btnRequestModification.Visible = false;
                            btnReject.Visible = true;
                            lblProjectStatus.Text = "Approved";
                        }
                        else if (lblProjectStatus.Text == "RequestForModification")
                        {
                            if (int.Parse(txtBudget.Text.ToString()) == 0)
                            {
                                txtAmount.Text = "0";

                            }
                            lblProjectId.Enabled = false;
                            txtProjectTitle.Enabled = false;
                            txtStudentName.Enabled = false;
                            txtCollegeName.Enabled = false;
                            txtMobileNo.Enabled = false;
                            txtBudget.Enabled = false;
                            txtProposedStartDate.Enabled = false;
                            txtProposedEndDate.Enabled = false;
                            txtTotalBeneficiaries.Enabled = false;
                            txtBeneficiaries.Enabled = false;
                            txtDuration.Enabled = false;
                            txtAmount.Enabled = false;
                            ddlTheme.Enabled = false;
                            txtPlaceofImplement.Enabled = false;
                            txtCurrentSituation.Enabled = false;
                            txProjectObjective.Enabled = false;
                            txtActionPlan.Enabled = false;
                            txtManagerComments.Enabled = true;
                            btnAddMeterial.Visible = false;
                            btnApproved.Visible = false;
                            btnRequestModification.Visible = false;
                            btnReject.Visible = false;
                            btnAddMeterial.Enabled = false;
                            ChkIsImpact.Enabled = false;
                            lblProjectStatus.Text = "RequestForModification";
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx?SessionTimeOut=True");
                    }


                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void btnApproved_Click(object sender, EventArgs e)
    {
        try
        {
            if (cook.Manager_Id() != "")
            {
                string AcademicCode = cook.Manager_AcademicYear();
                string ManagerID = cook.Manager_Id();
                
                BLobj.Manager_UpdateProjectDetails(lblLead_Id.Text.ToString(), ManagerID.ToString(), lblPDID.Text.ToString(), "Approved", txtStudentName.Text.ToString(), txtProjectTitle.Text.ToString(), txtCurrentSituation.Text.ToString(), txtPlaceofImplement.Text.ToString(), txtTotalBeneficiaries.Text.ToString(), txtBeneficiaries.Text.ToString(), txtBudget.Text.ToString(), txtDuration.Text.ToString(), txtAmount.Text.ToString(), txProjectObjective.Text.ToString(), txtActionPlan.Text.ToString(), ddlTheme.SelectedValue.ToString(), txtManagerComments.Text.ToString(), AcademicCode.ToString(), txtProposedStartDate, txtProposedEndDate);
                BLobj.Manager_SaveProjectDiscussionForum(lblPDID.Text.ToString(), txtManagerComments.Text.ToString(), ManagerID.ToString(), "Comments","Approved");
                if (ChkIsImpact.Checked == true)
                {
                    BLobj.Manager_UpdateIsImapactProject(lblPDID.Text.ToString(), 1,lblLead_Id.Text.ToString(),txtProjectTitle.Text.ToString(),ManagerID.ToString());
                }
                else
                {
                    BLobj.Manager_UpdateIsImapactProject(lblPDID.Text.ToString(), 0, lblLead_Id.Text.ToString(), txtProjectTitle.Text.ToString(), ManagerID.ToString());
                }
                string DeviceID =BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                if (DeviceID != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Congratulations, Your project" + " " + txtProjectTitle.Text.ToString() + "  is Approved successfully", "Student", "Empty");
                }
             
               BLobj. Manager_SaveNotificationLog(ManagerID.ToString(), lblLead_Id.Text.ToString(), txtProjectTitle.Text.ToString(), "Approved(Manager)", "");
                BLobj.Manager_UpdateMeterialDetails(lblPDID.Text.ToString(), lblLead_Id.Text.ToString(), rptMeterial);
                Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
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

    protected void btnRequestModification_Click(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string AcademicCode = cook.Manager_AcademicYear();
                string ManagerID = cook.Manager_Id();
                BLobj.Manager_UpdateProjectDetails(lblLead_Id.Text.ToString(), ManagerID.ToString(), lblPDID.Text.ToString(), "RequestForModification", txtStudentName.Text.ToString(), txtProjectTitle.Text.ToString(), txtCurrentSituation.Text.ToString(), txtPlaceofImplement.Text.ToString(), txtTotalBeneficiaries.Text.ToString(), txtBeneficiaries.Text.ToString(), txtBudget.Text.ToString(), txtDuration.Text.ToString(), txtAmount.Text.ToString(), txProjectObjective.Text.ToString(), txtActionPlan.Text.ToString(), ddlTheme.SelectedValue.ToString(), txtManagerComments.Text.ToString(), AcademicCode.ToString(), txtProposedStartDate, txtProposedEndDate);
                BLobj.Manager_SaveProjectDiscussionForum(lblPDID.Text.ToString(), txtManagerComments.Text.ToString(), ManagerID.ToString(),"Comments", "RequestForModification");
                BLobj.Manager_UpdateMeterialDetails(lblPDID.Text.ToString(), lblLead_Id.Text.ToString(), rptMeterial);
                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), lblLead_Id.Text.ToString(), txtProjectTitle.Text.ToString() + ",Reason:" + txtManagerComments.Text.ToString(), "RequestForModification(Manager)", "");
                string DeviceID = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                if (DeviceID.ToString() != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your project" + " " + txtProjectTitle.Text.ToString() + " is RequestForModification", "Student", "Empty");
                }

                Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
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

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string AcademicCode = cook.Manager_AcademicYear();
                string ManagerID = cook.Manager_Id();
                BLobj.Manager_UpdateProjectDetails(lblLead_Id.Text.ToString(), ManagerID.ToString(), lblPDID.Text.ToString(), "Rejected", txtStudentName.Text.ToString(), txtProjectTitle.Text.ToString(), txtCurrentSituation.Text.ToString(), txtPlaceofImplement.Text.ToString(), txtTotalBeneficiaries.Text.ToString(), txtBeneficiaries.Text.ToString(), txtBudget.Text.ToString(), txtDuration.Text.ToString(), txtAmount.Text.ToString(), txProjectObjective.Text.ToString(), txtActionPlan.Text.ToString(), ddlTheme.SelectedValue.ToString(), txtManagerComments.Text.ToString(), AcademicCode.ToString(), txtProposedStartDate, txtProposedEndDate);
                BLobj.Manager_SaveProjectDiscussionForum(lblPDID.Text.ToString(), txtManagerComments.Text.ToString(), ManagerID.ToString(), "Comments", "Rejected");
                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), lblLead_Id.Text.ToString(), txtProjectTitle.Text.ToString() + ",Reason:" + txtManagerComments.Text.ToString(), "Rejected(Manager)", "");
                string DeviceID = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                if (DeviceID.ToString() != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your project" + " " + txtProjectTitle.Text.ToString() + " is Rejected", "Student", "Empty");
                }

                Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
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
    protected void btnRejectConfirm_Click(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string AcademicCode = cook.Manager_AcademicYear();
                string ManagerID = cook.Manager_Id();
                BLobj.Manager_RejectProject(lblLead_Id.Text.ToString(), ManagerID.ToString(), lblPDID.Text.ToString(), "Rejected", txtStudentName.Text.ToString(), txtProjectTitle.Text.ToString(), txtPlaceofImplement.Text.ToString(), txtTotalBeneficiaries.Text.ToString(), txtBeneficiaries.Text.ToString(), txtBudget.Text.ToString(), txtAmount.Text.ToString(), txProjectObjective.Text.ToString(), ddlTheme.SelectedItem.Text.ToString(), txtRCRejectComments.Text.ToString(), AcademicCode.ToString(), txtProposedStartDate, txtProposedEndDate);
                BLobj.Manager_SaveProjectDiscussionForum(lblPDID.Text.ToString(), txtRCRejectComments.Text.ToString(), ManagerID.ToString(), "Comments", "Rejected");
                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), lblLead_Id.Text.ToString(), txtProjectTitle.Text.ToString() + ",Reason:" + txtRCRejectComments.Text.ToString(), "Rejected(Manager)", "");
                string DeviceID = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                if (DeviceID.ToString() != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your project" + " " + txtProjectTitle.Text.ToString() + " is Rejected", "Student", "Empty");
                }

                Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
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

    List<Meterials> dataList = new List<Meterials>();
    protected void btnAddMeterial_Click(object sender, EventArgs e)
    {
        try
        {
            //-- add all existing values to a list
            foreach (RepeaterItem item in rptMeterial.Items)
            {
                dataList.Add(new Meterials()
                {
                    MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text,
                    MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text,
                    Slno = (item.FindControl("lblSlno") as Label).Text,

                });
            }
            //-- add a blank row to list to show a new row added
            dataList.Add(new Meterials());

            //-- bind repeater
            rptMeterial.DataSource = dataList;
            rptMeterial.DataBind();

        }
        catch (Exception)
        {
            lblError.Text = "Exception Caught";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
        }
    }

    protected void btnRemoveRow_Click(object sender, EventArgs e)
    {
        try
        {
            //int RequestedAmount = int.Parse(txtBudget.Text.ToString());

            ImageButton button = (sender as ImageButton);
            foreach (RepeaterItem item in rptMeterial.Items)
            {
                dataList.Add(new Meterials()
                {
                    MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text,
                    MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text,

                    Slno = (item.FindControl("lblSlno") as Label).Text,

                });
                // txtBudget.Text=Convert.ToString(RequestedAmount-int.Parse(dataList.))
            }

            int count = rptMeterial.Items.Count;
            RepeaterItem item1 = button.NamingContainer as RepeaterItem;

            //Get the repeater item index
            int index = item1.ItemIndex;
            if ((item1.FindControl("txtMeterialCost") as TextBox).Text != "")
            {
                lblTotalAmount.Text = (int.Parse(lblTotalAmount.Text) - int.Parse((item1.FindControl("txtMeterialCost") as TextBox).Text)).ToString();
            }

            dataList.RemoveAt(index);
            rptMeterial.DataSource = dataList;
            rptMeterial.DataBind();
            dataList.Clear();
        }
        catch (Exception)
        {
            lblError.Text = "Exception Caught";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
        }
    }
    protected void txtMeterialCost_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int sum = 0;
            for (int index = 0; index < this.rptMeterial.Items.Count; index++)
            {
                TextBox textBox = this.rptMeterial.Items[index].FindControl("txtMeterialCost") as TextBox;
                sum += textBox.Text.Length > 0 ? int.Parse(textBox.Text) : 0;
            }
            lblTotalAmount.Text = sum.ToString();

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public class Meterials
    {
        public string MeterialName
        {
            get; set;
        }
        public string MeterialCost
        {
            get; set;
        }
        public string Slno
        {
            get; set;
        }

    }
}