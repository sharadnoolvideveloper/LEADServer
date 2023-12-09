using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_Suggestion_Feedback : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                BLobj.Admin_Fillprogramddl(ddlprogram, cook.Admin_Id());
                BLobj.Admin_FillManagerList(rptManagerList, ddlprogram.SelectedValue.ToString());
                // BLobj.Admin_FillManagerByprogram(ddlprogramId.SelectedValue.ToString(), ddlManager);
                BLobj.FillAademicYearWithTop(ddlAcademicYear);
                string Where = "And gsf.academiccode='" + ddlAcademicYear.SelectedValue.ToString() + "'";
                DataTable dt = BLobj.Admin_FillSuggestionFeedback(Where.ToString());
                rptSuggestionFeedback.DataSource = dt;
                rptSuggestionFeedback.DataBind();


            }
            catch (Exception)
            {

                throw;
            }

        }

    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerList(rptManagerList, ddlprogram.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
        }
    }

    protected void rptManagerList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            Label ManagerId = (e.Item.FindControl("lblManagerId") as Label);
            //   Session["ManagerId"] = ManagerId.Text.ToString();
            string Where = "And gsf.academiccode='" + ddlAcademicYear.SelectedValue.ToString() + "' and md.ManagerId=" + ManagerId.Text.ToString() + " ";
            rptSuggestionFeedback.DataSource = BLobj.Admin_FillSuggestionFeedback(Where.ToString());
            rptSuggestionFeedback.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Where = "And gsf.academiccode='" + ddlAcademicYear.SelectedValue.ToString() + "'";
            rptSuggestionFeedback.DataSource = BLobj.Admin_FillSuggestionFeedback(Where.ToString());
            rptSuggestionFeedback.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnAll_Click(object sender, EventArgs e)
    {
        try
        {
            string Where = "And gsf.academiccode='" + ddlAcademicYear.SelectedValue.ToString() + "'";
            DataTable dt = BLobj.Admin_FillSuggestionFeedback(Where);
            rptSuggestionFeedback.DataSource = dt;
            rptSuggestionFeedback.DataBind();
            GridView gd = new GridView();
            gd.DataSource = dt;
            gd.DataBind();
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
            string Where = "And gsf.academiccode='" + ddlAcademicYear.SelectedValue.ToString() + "'";
            DataTable dt = BLobj.Admin_FillSuggestionFeedback(Where);
            GridView gd = new GridView();
            gd.DataSource = dt;
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {
                string name = "Suggestion/Feedback" + "_" + System.DateTime.Now.ToString();
                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gd.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}