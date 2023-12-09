using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_ManagerCollege_PrincipalDetails : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    string Where = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (cook.Manager_Id() != "")
                {
                    Where = "MC.managercode=" +cook.Manager_Id();
                    BLobj.FillTalukaByManagerId(Where, ddlTaluka);

                    Where = "where mc.managercode = " + cook.Manager_Id() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
                    DataTable dt = BLobj.Common_College_FillDetails(Where.ToString());
                    rptCollegeDetails.DataSource = dt;
                    rptCollegeDetails.DataBind();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void ddlTaluka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTaluka.SelectedIndex > 0)
            {
                Where = "where mc.managercode = " + cook.Manager_Id() + " and clg.CollegeId = mc.CollegeCode and clg.talukid=" + ddlTaluka.SelectedValue.ToString() + " and clg.Status = 1";
            }
            else
            {
                Where = "where mc.managercode = " + cook.Manager_Id() + " and clg.CollegeId = mc.CollegeCode  and clg.Status = 1";
            }
            DataTable dt = BLobj.Common_College_FillDetails(Where.ToString());
            rptCollegeDetails.DataSource = dt;
            rptCollegeDetails.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSaveCollegeDetails_Click(object sender, EventArgs e)
    {
        try
        {
            foreach(RepeaterItem item in rptCollegeDetails.Items)
            {
                Label lblCollegeId = (item.FindControl("lblCollegeId") as Label);
                TextBox lblCollege_Name = (item.FindControl("lblCollege_Name") as TextBox);
                TextBox txtPrincipal_Name = (item.FindControl("txtPrincipal_Name") as TextBox);
                TextBox txtPrincipal_MobileNo = (item.FindControl("txtPrincipal_MobileNo") as TextBox);
                TextBox txtPrincipal_MailId = (item.FindControl("txtPrincipal_MailId") as TextBox);
                TextBox txtPrincipal_WhatsAppNo = (item.FindControl("txtPrincipal_WhatsAppNo") as TextBox);
                TextBox txtPrincipal_FacebookId = (item.FindControl("txtPrincipal_FacebookId") as TextBox);
                BLobj.Common_College_PrincipalSave(lblCollegeId.Text.ToString(), txtPrincipal_Name.Text.ToString(),
                    txtPrincipal_MobileNo.Text.ToString(), txtPrincipal_MailId.Text.ToString(), txtPrincipal_WhatsAppNo.Text.ToString(),
                    txtPrincipal_FacebookId.Text.ToString());
            }
            Where = "where mc.managercode = " + cook.Manager_Id() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            DataTable dt = BLobj.Common_College_FillDetails(Where.ToString());
            rptCollegeDetails.DataSource = dt;
            rptCollegeDetails.DataBind();
            string msg = "Principal Details Added Successfully";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg.ToString() + "')", true);
        }
        catch (Exception)
        {

            throw;
        }
    }
}