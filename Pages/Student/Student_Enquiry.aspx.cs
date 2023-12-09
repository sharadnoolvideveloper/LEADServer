using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_Student_Enquiry : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(cook.LeadId()!=null)
            {
                if (!Page.IsPostBack)
                {
                    BLobj.FillStudentRequestHeads(ddlRequestType);
                    BLobj.FillStudentProjectTitle(ddlProjectTitle,cook.LeadId());
                    
                    Project.Visible = false;
                    DataTable dt = BLobj.Student_FillRequestRepeater(cook.LeadId(), ddlRequestType.SelectedValue.ToString());
                    rptRequest.DataSource = dt;
                    rptRequest.DataBind();
                    txtWriteRequest.Text = "";
                    txtWriteRequest.Focus();
                }
            }          
            else
            {
                Response.Redirect("~/Default.aspx?SessionOut=true");
            }
        }
        catch (Exception)
        {          
        }
    }
    protected void btnNewRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRequestType.SelectedIndex == 0)
            {
                string msg = "Select RequestType";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if ((ddlRequestType.SelectedValue == "1") && (ddlProjectTitle.SelectedIndex == 0))
            {
                string msg = "Select Project Title";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                string PDID = "";
                if ((ddlRequestType.SelectedValue == "1") && (ddlProjectTitle.SelectedIndex != 0))
                {
                    PDID = ddlProjectTitle.SelectedValue.ToString();
                }
                else
                {
                    PDID = "0";
                }
                BLobj.Student_RequestInsertDelete(ddlRequestType.SelectedValue.ToString(), cook.LeadId(), cook.RegistrationId(), cook.ManagerId(), Regex.Replace(txtWriteRequest.Text, "'", "`").Trim(), ddlPriority.SelectedValue.ToString(), "NEW", "",PDID.ToString());
                DataTable dt = BLobj.Student_FillRequestRepeater(cook.LeadId(), ddlRequestType.SelectedValue.ToString());
                rptRequest.DataSource = dt;
                rptRequest.DataBind();
                txtWriteRequest.Text = "";
                txtWriteRequest.Focus();
                if (ddlRequestType.SelectedValue == "1")
                {
                    Project.Visible = true;
                }
                else
                {
                    Project.Visible = false;
                }
                Response.Redirect("Student_Enquiry.aspx");
                string msg = "Request Sent";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            }
        }
        catch (Exception)
        {
        }
    }

    protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.FillStudentProjectTitle(ddlProjectTitle,cook.LeadId());
            if (ddlRequestType.SelectedValue == "1")
            {
                Project.Visible = true;
            }
            else
            {
                Project.Visible = false;
            }
            DataTable dt = BLobj.Student_FillRequestRepeater(cook.LeadId(), ddlRequestType.SelectedValue.ToString());
                rptRequest.DataSource = dt;
                rptRequest.DataBind();
            txtWriteRequest.Text = "";
            txtWriteRequest.Focus();
            
        }
        catch (Exception)
        {


        }
    }
    
    protected void rptRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            Label lblStatus = (e.Item.FindControl("lblStatus") as Label);
            if (lblStatus.Text == "1")
            {
                Label lblStudentRequestNo = (e.Item.FindControl("lblStudentRequestNo") as Label);
                BLobj.Student_RequestInsertDelete("", "", "", "", "", "", "DELETE", lblStudentRequestNo.Text.ToString(), "");
                DataTable dt = BLobj.Student_FillRequestRepeater(cook.LeadId(), ddlRequestType.SelectedValue.ToString());
                rptRequest.DataSource = dt;
                rptRequest.DataBind();
                txtWriteRequest.Text = "";
                txtWriteRequest.Focus();
                string msg = "Request Deleted";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                Response.Redirect("Student_Enquiry.aspx");
            }
            else
            {
                string msg = "Ticket Already Closed";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            }
          
        }
        catch (Exception)
        {


        }
    }
    protected void rptRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Label lblStatus = (e.Item.FindControl("lblStatus") as Label);
                LinkButton btnDeleteRequest = (e.Item.FindControl("btnDeleteRequest") as LinkButton);
                Label lblRespondMessage = (e.Item.FindControl("lblRespondMessage") as Label);
                Label lblStudentPriority = (e.Item.FindControl("lblPriority") as Label);
                Label lblManagerPriority = (e.Item.FindControl("lblStudentRequestTime") as Label);

                if(lblStatus.Text=="1")
                {
                    //btnDeleteRequest.Visible = true;
                    btnDeleteRequest.Attributes["data-toggle"] = "tooltip";
                    btnDeleteRequest.Attributes["data-placement"] = "top";
                    btnDeleteRequest.Attributes["title"] = "Delete";
                    btnDeleteRequest.Attributes.Add("class", "fa fa-trash text-danger btn");
                    lblRespondMessage.Text = lblRespondMessage.Text + " " + "Response is Pending";
                    lblRespondMessage.Attributes.Add("class", "label label-default");
                }
                else if(lblStatus.Text=="2")
                {
                    //btnDeleteRequest.Visible = false;                   
                    btnDeleteRequest.Attributes["data-toggle"] = "tooltip";
                    btnDeleteRequest.Attributes["data-placement"] = "top";
                    btnDeleteRequest.Attributes["title"] = "Closed";
                    btnDeleteRequest.Attributes.Add("class", "fa fa-check  btn btn-success");
                    lblManagerPriority.Attributes.Add("class", "label label-success");
                }
              

               
            }
        }
        catch (Exception)
        {


        }
    }


}