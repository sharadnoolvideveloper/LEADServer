using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML;
using ClosedXML.Excel;

public partial class Pages_Admin_Admin_Consoliated_Report : System.Web.UI.Page
{
    Reports rpt = new Reports();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MultiView1.SetActiveView(vmTop);
            rpt.Admin_Fillprogramddl(ddlprogramId, cook.Admin_Id());
        }
    }




    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            lblContentTitle.Text = "consolidated Report From " + txtFromDate.Text.ToString() + " " + "to " + txtToDate.Text.ToString();

            // Process to Database
            rpt.Admin_ConsoliatedReport(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), cook.Admin_Id(),ddlprogramId.SelectedValue.ToString());

            //Bind the Consoliated Table to repeater
            DataTable dt = rpt.Bind_AdminConsoliatedReport(cook.Admin_Id(), ddlprogramId.SelectedValue.ToString());


            rptConsoliatedReport.DataSource = dt;
            rptConsoliatedReport.DataBind();
            if (dt.Rows.Count > 0)
            {
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalPaid") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["paidcount"])).ToString();

                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalUnPaid") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["unpaidcount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalProposed") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["ProposedCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalApproved") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["ApprovedCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalCompleted") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["CompltedCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalRejected") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["RejectedCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalRM") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["RequestForModification"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalRC") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["RequestForCompletion"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalDraft") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["Drafted"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalProjects") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["GrandTotal"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalPInitiator") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["p_InitiatorCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalPCM") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["p_ChangeMakerCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalpLeader") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["p_LeaderCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalpImpact") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["p_impactCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalLInitiator") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["L_InitiatorCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalLCM") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["L_ChangeMakerCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalL_Leader") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["L_LeaderCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalL_ML") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["LMasterLeaderCount"])).ToString();
                (rptConsoliatedReport.Controls[rptConsoliatedReport.Controls.Count - 1].Controls[0].FindControl("lblTotalL_LA") as Label).Text = dt.Select().Sum(p => Convert.ToInt32(p["L_AmbassadorCount"])).ToString();
            }


            MultiView1.SetActiveView(vmContent);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {


            MultiView1.SetActiveView(vmTop);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            string fileName = "Consoliated_From_" + txtFromDate.Text.ToString() + " " + "To_" + txtToDate.Text.ToString();
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    rptConsoliatedReport.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {


        }
    }



    protected void btnMultipleSheets_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = rpt.Bind_AdminConsoliatedReport(cook.Admin_Id(), cook.Admin_program());
            DataTable dt2 = rpt.Admin_GetProject_List(txtFromDate.Text.ToString(), txtToDate.Text.ToString());
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);
            ds.Tables[0].TableName = "Consoliated Summary";
            ds.Tables[1].TableName = "Project Details";
            string fileName = "Multisheet_Summary_" + txtFromDate.Text.ToString() + " " + "To_" + txtToDate.Text.ToString();
            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(ds);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName.ToString() + ".xls");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}