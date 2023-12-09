using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_Manager_Scheduling_Task : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string managerid = cook.Manager_Id();
            if ((cook.Manager_Id() == null) || (cook.Manager_Id() == ""))
            {
                Response.Redirect("~/Default.aspx?Logout=Session Out");
            }
            else
            {
                BLobj.FillAademicYearWithTop(ddlAcademicYear);
                BLobj.Manager_FillCollegeByManagerCode(cook.Manager_Id(), ddlCollege);
                DataTable dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "Schedule", ddlAcademicYear.SelectedValue.ToString());
                rptSchedules.DataSource = dt;
                rptSchedules.DataBind();
                dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "CompletedSchedule", ddlAcademicYear.SelectedValue.ToString());
                rptCompletedSchedule.DataSource = dt;
                rptCompletedSchedule.DataBind();
                txtScheduleDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                btnReShedule.Visible = false;

                ddlProjectStatus.SelectedIndex = ddlProjectStatus.Items.IndexOf(ddlProjectStatus.Items.FindByValue("Approved"));
                
                lstStudentType.Items[0].Selected = true;
                lstScheduleType.Items[2].Selected = true;
            }
        }
    }

    protected void rptSchedules_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != null)
            {

      
                string ScheduleId = "";
                string btnType = "";

                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                lstStudentType.ClearSelection();
                lstScheduleType.ClearSelection();
                int i;
                for (i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        ScheduleId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        btnType = itemlist[1].ToString();
                    }


                }
                if (btnType == "Edit")
                {
                    string tempHH = "";
                    DataTable dtEdit = BLobj.Manager_GetScheduleforEdit(cook.Manager_Id(), ScheduleId.ToString());
                    string[] strStudentType = dtEdit.Rows[0].ItemArray[0].ToString().Split(new char[] { ',' });
                    foreach (string item in strStudentType)
                    {
                        lstStudentType.Items.FindByValue(item.ToString()).Selected = true;
                    }

                  
                    ddlProjectStatus.SelectedIndex = ddlProjectStatus.Items.IndexOf(ddlProjectStatus.Items.FindByValue(dtEdit.Rows[0].ItemArray[1].ToString()));

                    DateTime ScheduleDate = DateTime.Parse(dtEdit.Rows[0].ItemArray[2].ToString());
                    txtScheduleDate.Text = ScheduleDate.ToString("yyyy-MM-dd");
                    tempHH = ScheduleDate.Hour.ToString();
                    string AmPm = "";
                    AmPm = ScheduleDate.ToString("%r");
                    if (int.Parse(tempHH.ToString()) < 12)
                    {
                        if (ScheduleDate.Hour < 10)
                        {
                            tempHH = "0" + ScheduleDate.Hour.ToString();
                        }
                        else
                        {
                            tempHH = ScheduleDate.Hour.ToString();
                        }
                       
                    }
                    else
                    {                       

                        tempHH =(ScheduleDate.Hour-12).ToString();
                        if(ScheduleDate.Hour - 12 < 10)
                        {
                            tempHH = "0" + (ScheduleDate.Hour - 12).ToString();
                        }
                        else
                        {
                            tempHH = (ScheduleDate.Hour - 12).ToString();
                        }
                    }
                 
                    ddlHours.SelectedIndex = ddlHours.Items.IndexOf(ddlHours.Items.FindByValue(tempHH.ToString()));
                    ddlMinutes.SelectedIndex = ddlMinutes.Items.IndexOf(ddlMinutes.Items.FindByValue(ScheduleDate.Minute.ToString()));

                    string[] strScheduleType = dtEdit.Rows[0].ItemArray[3].ToString().Split(new char[] { ',' });
                    foreach (string item in strScheduleType)
                    {
                        lstScheduleType.Items.FindByValue(item.ToString()).Selected = true;
                    }
                    if (dtEdit.Rows[0].ItemArray[4].ToString() == "0")
                    {
                        ddlCollege.SelectedIndex = ddlCollege.Items.IndexOf(ddlCollege.Items.FindByValue("[All]"));
                    }
                    else
                    {
                        ddlCollege.SelectedIndex = ddlCollege.Items.IndexOf(ddlCollege.Items.FindByValue(dtEdit.Rows[0].ItemArray[4].ToString()));
                    }
                    txtScheduleDescription.Text = dtEdit.Rows[0].ItemArray[5].ToString();
                    txtSchedule_Message.Text = dtEdit.Rows[0].ItemArray[6].ToString();
                    ddlAMPM.SelectedIndex = ddlAMPM.Items.IndexOf(ddlAMPM.Items.FindByValue(dtEdit.Rows[0].ItemArray[7].ToString()));
                    btnReShedule.Visible = true;
                    lblScheduleId.Text = ScheduleId.ToString();
                    lblActionType.Text = "Edit";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "Message_Schedule();", true);
                }
                else if (btnType == "Delete")
                {
                    BLobj.Manager_DisableScheduleMessage(cook.Manager_Id(), ScheduleId.ToString());
                    DataTable dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "Schedule", ddlAcademicYear.SelectedValue.ToString());
                    rptSchedules.DataSource = dt;
                    rptSchedules.DataBind();
                    dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "CompletedSchedule", ddlAcademicYear.SelectedValue.ToString());
                    rptCompletedSchedule.DataSource = dt;
                    rptCompletedSchedule.DataBind();
                }



            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void rptSchedules_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblStatus = (e.Item.FindControl("lblStatus") as Label);

                Label lblScheduleId = (e.Item.FindControl("lblScheduleId") as Label);
                Label lblProgress = (e.Item.FindControl("lblProgress") as Label);
                LinkButton btnStatus = (e.Item.FindControl("btnStatus") as LinkButton);

                Panel pnl = (e.Item.FindControl("Panel1") as Panel);

                btnStatus.Text = "<span class='fa fa-remove'></span>";
                btnStatus.Attributes.Add("class", "btn btn-default btn-floating");


                btnStatus.Attributes["data-toggle"] = "tooltip";
                btnStatus.Attributes["data-placement"] = "top";
                btnStatus.Attributes["title"] = "Delete";

                if (lblStatus.Text == "1")
                {
                    lblProgress.ForeColor = System.Drawing.Color.Red;
                    lblProgress.Text = "In progress..";
                    pnl.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
        catch (Exception)
        {

        }

    }
    protected void rptCompletedSchedule_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblStatus = (e.Item.FindControl("lblStatus") as Label);

                Label lblScheduleId = (e.Item.FindControl("lblScheduleId") as Label);
                Label lblProgress = (e.Item.FindControl("lblProgress") as Label);
                LinkButton btnStatus = (e.Item.FindControl("btnStatus") as LinkButton);
                btnStatus.Text = "<span class='fa fa-remove'></span>";
                btnStatus.Attributes.Add("class", "btn btn-default btn-floating");
                btnStatus.Attributes["data-toggle"] = "tooltip";
                btnStatus.Attributes["data-placement"] = "top";
                btnStatus.Attributes["title"] = "Delete";
                if (lblStatus.Text == "2")
                {
                    lblProgress.Text = "Sent";
                    lblProgress.ForeColor = System.Drawing.Color.Green;

                }


            }
        }
        catch (Exception)
        {

        }

    }
    protected void btnScheduleSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSchedule_Message.Text == "")
            {
                txtSchedule_Message.Focus();
                string msg = "Enter Schedule Message";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (lstScheduleType.SelectedIndex < 0)
            {
                lstScheduleType.Focus();
                string msg = "Select Schedule Type";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                string ScheduleType = "";
                string StudentType = "";
                string CollegeId = "";
                string ScheduleDate = "";
                if (lblActionType.Text == "")
                {
                    lblActionType.Text = "NEW";
                }
                else if (lblActionType.Text == "Edit")
                {
                    lblActionType.Text = "EDIT";
                }

                int hh = 0;
                hh = int.Parse(ddlHours.SelectedItem.Text.ToString());

                if (ddlAMPM.SelectedValue == "AM")
                {
                    int temphh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                    if (temphh < 12)
                    {
                        hh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                    }
                    else
                    {
                        hh = 0;
                    }
                 
                }
                else
                {
                    int temphh= int.Parse(ddlHours.SelectedItem.Text.ToString());
                    if (temphh == 12)
                    {
                        hh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                    }
                    else
                    {
                        hh = int.Parse(ddlHours.SelectedItem.Text.ToString())+12;
                    }
                   
                }

                ScheduleDate = txtScheduleDate.Text + " " + hh + ":" + ddlMinutes.SelectedItem.Text.ToString() + ":00";

                if (ddlCollege.SelectedValue.ToString() == "[All]")
                {
                    CollegeId = "0";
                }
                else
                {
                    CollegeId = ddlCollege.SelectedValue.ToString();
                }
                foreach (ListItem item in lstScheduleType.Items)
                {
                    if (item.Selected)
                    {
                        ScheduleType += item.Value + ",";
                    }
                }
                foreach (ListItem item in lstStudentType.Items)
                {
                    if (item.Selected)
                    {
                        StudentType += item.Value + ",";
                    }
                }
                BLobj.Manager_SaveScheduleMessaging(lblScheduleId.Text.ToString(), Regex.Replace(txtScheduleDescription.Text, "'", "`").Trim(), ScheduleType.ToString().TrimEnd(','), Regex.Replace(txtSchedule_Message.Text, "'", "`").Trim(),
               ddlProjectStatus.SelectedValue.ToString(), StudentType.ToString().TrimEnd(','), ScheduleDate.ToString(), cook.Manager_Id(), lblActionType.Text.ToString(), CollegeId.ToString());
                lblScheduleId.Text = "";
                txtScheduleDescription.Text = "";
              
                txtScheduleDate.Text = DateTime.Now.ToString("yyyy-mm-dd");
             
                txtSchedule_Message.Text = "";
                if (lblActionType.Text.ToString() == "NEW")
                {
                    string msg = "Schedule Created";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                }
                else
                {
                    string msg = "Schedule Updated";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                }
                Response.Redirect("Manager_Scheduling_Task.aspx");
           
            }
        }
        catch (Exception)
        {


        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "Schedule", ddlAcademicYear.SelectedValue.ToString());
            rptSchedules.DataSource = dt;
            rptSchedules.DataBind();
            dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "CompletedSchedule", ddlAcademicYear.SelectedValue.ToString());
            rptCompletedSchedule.DataSource = dt;
            rptCompletedSchedule.DataBind();
        }
        catch (Exception)
        {


        }
    }

    protected void btnReShedule_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtSchedule_Message.Text == "")
            {
                txtSchedule_Message.Focus();
                string msg = "Enter Schedule Message";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (lstScheduleType.SelectedIndex < 0)
            {
                lstScheduleType.Focus();
                string msg = "Select Schedule Type";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                string ScheduleType = "";
                string StudentType = "";
                string CollegeId = "";
                string ScheduleDate = "";
                if (lblActionType.Text == "")
                {
                    lblActionType.Text = "NEW";
                }


                int hh = 0;
                hh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                if (ddlAMPM.SelectedValue == "AM")
                {
                    int temphh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                    if (temphh < 12)
                    {
                        hh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                    }
                    else
                    {
                        hh = 0;
                    }

                }
                else
                {
                    int temphh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                    if (temphh == 12)
                    {
                        hh = int.Parse(ddlHours.SelectedItem.Text.ToString());
                    }
                    else
                    {
                        hh = int.Parse(ddlHours.SelectedItem.Text.ToString()) + 12;
                    }
                }
                ScheduleDate = txtScheduleDate.Text + " " + hh + ":" + ddlMinutes.SelectedItem.Text.ToString() + ":00";

                if (ddlCollege.SelectedValue.ToString() == "[All]")
                {
                    CollegeId = "0";
                }
                else
                {
                    CollegeId = ddlCollege.SelectedValue.ToString();
                }
                foreach (ListItem item in lstStudentType.Items)
                {
                    if (item.Selected)
                    {
                        StudentType += item.Value + ",";
                    }
                }
                foreach (ListItem item in lstScheduleType.Items)
                {
                    if (item.Selected)
                    {
                        ScheduleType += item.Value + ",";
                    }
                }
                BLobj.Manager_SaveScheduleMessaging(lblScheduleId.Text.ToString(), Regex.Replace(txtScheduleDescription.Text, "'", "`").Trim(), ScheduleType.ToString().TrimEnd(','), Regex.Replace(txtSchedule_Message.Text, "'", "`").Trim(),
                ddlProjectStatus.SelectedValue.ToString(), StudentType.ToString().TrimEnd(','), ScheduleDate.ToString(), cook.Manager_Id(), "NEW", CollegeId.ToString());
                lblScheduleId.Text = "";
                txtScheduleDescription.Text = "";
                txtScheduleDate.Text = DateTime.Now.ToString("yyyy-mm-dd");
                txtSchedule_Message.Text = "";
                lstStudentType.ClearSelection();
                lstScheduleType.ClearSelection();
                lstStudentType.Items[0].Selected = true;
                lstScheduleType.Items[2].Selected = true;
                string msg = "Re-Schedule Successfully";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                lblActionType.Text = "";
                DataTable dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "Schedule", ddlAcademicYear.SelectedValue.ToString());
                rptSchedules.DataSource = dt;
                rptSchedules.DataBind();
                dt = BLobj.Manager_GetScheduleMessage(cook.Manager_Id(), "CompletedSchedule", ddlAcademicYear.SelectedValue.ToString());
                rptCompletedSchedule.DataSource = dt;
                rptCompletedSchedule.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }
}