using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_SanboxWiseManagerList : System.Web.UI.Page
{
    Reports rpt = new Reports();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rpt.Admin_Fillprogramddl(ddlprogramId, cook.Admin_Id());
            rptSandboxList.DataSource = rpt.GetSandboxDetails();
            rptSandboxList.DataBind();
        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rptSandboxList.DataSource = rpt.GetSandboxDetails();
            rptSandboxList.DataBind();
        }
        catch (Exception ex)
        {
        }
    }


    protected void rptSandboxList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSandbox = (e.Item.FindControl("SandboxName") as Label);
                Label lblAllProjectsTop = (e.Item.FindControl("lblAllProjectTop") as Label);
                Label lblProposedTop = (e.Item.FindControl("lblProposedTop") as Label);
                Label lblApprovedTop = (e.Item.FindControl("lblApprovedTop") as Label);
                Label lblRMTop = (e.Item.FindControl("lblRMTop") as Label);
                Label lblRCTop = (e.Item.FindControl("lblRCTop") as Label);
                Label lblCompletedTop = (e.Item.FindControl("lblCompletedTop") as Label);
                Label lblRejectedTop = (e.Item.FindControl("lblRejectedTop") as Label);
                Repeater rptManagerList = e.Item.FindControl("rptManagerList") as Repeater;

                rptManagerList.DataSource = rpt.FillManagerSandboxWise(lblSandbox.Text.ToString(), cook.Admin_Id(), ddlprogramId.SelectedValue.ToString());
                rptManagerList.DataBind();

                //   DataTable dt = rpt.GetSandboxWiseProjectDetailsTOP(lblSandbox.Text.ToString(), ddlprogramId.SelectedValue.ToString());
                DataTable dt = rpt.GetSandboxWiseProjectDetailsTOP(ddlprogramId.SelectedValue.ToString());
                int count = 0;
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[0].ToString() == lblSandbox.Text.ToString())
                    {
                        if (dt.Rows[i].ItemArray[2].ToString() == "Approved")
                        {
                            lblApprovedTop.Text = dt.Rows[i].ItemArray[1].ToString();
                            count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                            lblAllProjectsTop.Text = count.ToString();
                        }
                        else if (dt.Rows[i].ItemArray[2].ToString() == "Completed")
                        {
                            lblCompletedTop.Text = dt.Rows[i].ItemArray[1].ToString();
                            count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                            lblAllProjectsTop.Text = count.ToString();
                        }
                        else if (dt.Rows[i].ItemArray[2].ToString() == "Proposed")
                        {
                            lblProposedTop.Text = dt.Rows[i].ItemArray[1].ToString();
                            count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                            lblAllProjectsTop.Text = count.ToString();
                        }
                        else if (dt.Rows[i].ItemArray[2].ToString() == "Rejected")
                        {
                            lblRejectedTop.Text = dt.Rows[i].ItemArray[1].ToString();
                            count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                            lblAllProjectsTop.Text = count.ToString();
                        }
                        else if (dt.Rows[i].ItemArray[2].ToString() == "RequestForCompletion")
                        {
                            lblRCTop.Text = dt.Rows[i].ItemArray[1].ToString();
                            count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                            lblAllProjectsTop.Text = count.ToString();
                        }
                        else if (dt.Rows[i].ItemArray[2].ToString() == "RequestForModification")
                        {
                            lblRMTop.Text = dt.Rows[i].ItemArray[1].ToString();
                            count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                            lblAllProjectsTop.Text = count.ToString();
                        }
                    }

                }

            }
        }
        catch (Exception)
        {

        }
    }
    protected void rptManagerList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblManagerId = (e.Item.FindControl("lblManagerId") as Label);
                Label lblAllProjectsList = (e.Item.FindControl("lblAllProjectList") as Label);
                Label lblProposedList = (e.Item.FindControl("lblProposedList") as Label);
                Label lblApprovedList = (e.Item.FindControl("lblApprovedList") as Label);
                Label lblRMList = (e.Item.FindControl("lblRMList") as Label);
                Label lblRCList = (e.Item.FindControl("lblRCList") as Label);
                Label lblCompletedList = (e.Item.FindControl("lblCompletedList") as Label);
                Label lblRejectedList = (e.Item.FindControl("lblRejectedList") as Label);

                DataTable dt = rpt.GetSandboxManagerWiseProjectDetails(ddlprogramId.SelectedValue.ToString(), lblManagerId.Text.ToString());
                int count = 0;
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[2].ToString() == "Approved")
                    {
                        lblApprovedList.Text = dt.Rows[i].ItemArray[1].ToString();
                        count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                        lblAllProjectsList.Text = count.ToString();
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == "Completed")
                    {
                        lblCompletedList.Text = dt.Rows[i].ItemArray[1].ToString();
                        count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                        lblAllProjectsList.Text = count.ToString();
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == "Proposed")
                    {
                        lblProposedList.Text = dt.Rows[i].ItemArray[1].ToString();
                        count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                        lblAllProjectsList.Text = count.ToString();
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == "Rejected")
                    {
                        lblRejectedList.Text = dt.Rows[i].ItemArray[1].ToString();
                        count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                        lblAllProjectsList.Text = count.ToString();
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == "RequestForCompletion")
                    {
                        lblRCList.Text = dt.Rows[i].ItemArray[1].ToString();
                        count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                        lblAllProjectsList.Text = count.ToString();
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == "RequestForModification")
                    {
                        lblRMList.Text = dt.Rows[i].ItemArray[1].ToString();
                        count = count + int.Parse(dt.Rows[i].ItemArray[1].ToString());
                        lblAllProjectsList.Text = count.ToString();
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=RepeaterExport.xls");
            Response.Charset = "sharad noolvi";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            rptSandboxList.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}