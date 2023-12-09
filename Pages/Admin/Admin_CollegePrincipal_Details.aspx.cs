using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_CollegePrincipal_Details : System.Web.UI.Page
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


                if (cook.Admin_Id() != "")
                {
                    //BLobj.Admin_FillManagerddl(ddlManagerName,cook.Admin_Id());
                    BLobj.Admin_Fillprogramddl(ddlprogram, cook.Admin_Id());
                    BLobj.Admin_FillManagerByprogram(ddlprogram.SelectedValue.ToString(), ddlManagerName);
                    //  Where = "where clg.CollegeId = mc.CollegeCode and clg.Status = 1";
                    Where = "where clg.CollegeId = mc.CollegeCode and cp.college_id = clg.CollegeId and cp.program_id = " + ddlprogram.SelectedValue.ToString() + " and  clg.Status = 1";
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

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerByprogram(ddlprogram.SelectedValue.ToString(), ddlManagerName);
            Where = "where clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            DataTable dt = BLobj.Common_College_FillDetails(Where.ToString());
            rptCollegeDetails.DataSource = dt;
            rptCollegeDetails.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSaveCollegeDetails_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rptCollegeDetails.Items)
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
            if ((ddlManagerName.SelectedIndex > 0) && (ddlTaluka.SelectedIndex > 0))
            {
                Where = " Where mc.managercode = " + ddlManagerName.SelectedValue.ToString() + " and clg.talukid=" + ddlTaluka.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }
            else if ((ddlManagerName.SelectedIndex > 0) && (ddlTaluka.SelectedIndex == 0))
            {
                Where = "Where mc.managercode = " + ddlManagerName.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }
            else if ((ddlManagerName.SelectedIndex == 0) && (ddlTaluka.SelectedIndex > 0))
            {
                Where = "Where clg.talukid=" + ddlTaluka.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }
            else
            {
                Where = "Where clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }

            DataTable dt = BLobj.Common_College_FillDetails(Where.ToString());
            rptCollegeDetails.DataSource = dt;
            rptCollegeDetails.DataBind();
            string msg = "Principal Details Added Successfully";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg.ToString() + "')", true);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void ddlManagerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Where = "";
            if (ddlManagerName.SelectedIndex == 0)
            {
                Where = "";
            }
            else
            {
                Where = "MC.managercode=" + ddlManagerName.SelectedValue.ToString();
                BLobj.FillTalukaByManagerId(Where, ddlTaluka);

                Where = "where mc.managercode = " + ddlManagerName.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
                DataTable dt = BLobj.Common_College_FillDetails(Where.ToString());
                rptCollegeDetails.DataSource = dt;
                rptCollegeDetails.DataBind();
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
                Where = "where mc.managercode = " + ddlManagerName.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.talukid=" + ddlTaluka.SelectedValue.ToString() + " and clg.Status = 1";
            }
            else
            {
                Where = "where mc.managercode = " + ddlManagerName.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode  and clg.Status = 1";
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if ((ddlManagerName.SelectedIndex > 0) && (ddlTaluka.SelectedIndex > 0))
            {
                Where = " Where mc.managercode = " + ddlManagerName.SelectedValue.ToString() + " and clg.talukid=" + ddlTaluka.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }
            else if ((ddlManagerName.SelectedIndex > 0) && (ddlTaluka.SelectedIndex == 0))
            {
                Where = "Where mc.managercode = " + ddlManagerName.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }
            else if ((ddlManagerName.SelectedIndex == 0) && (ddlTaluka.SelectedIndex > 0))
            {
                Where = "Where clg.talukid=" + ddlTaluka.SelectedValue.ToString() + " and clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }
            else
            {
                Where = "Where clg.CollegeId = mc.CollegeCode and clg.Status = 1";
            }

            DataTable dt = BLobj.Common_College_FillDetails(Where.ToString());
            GridView gd = new GridView();
            gd.DataSource = dt;
            gd.DataBind();
            string name = "";
            if (gd.Rows.Count > 0)
            {
                if (ddlManagerName.SelectedIndex == 0)
                {
                    name = "All Managers Details " + DateTime.Now.ToString();
                }
                else
                {
                    name = ddlManagerName.SelectedItem.Text.ToString() + " " + DateTime.Now.ToString();
                }
                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gd.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                string msg = "No Data Found Check Between Dates";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {


        }
    }
}