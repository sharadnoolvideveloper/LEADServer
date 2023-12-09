using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Pages_Manager_TshirtReport : System.Web.UI.Page
{
    Reports rpt = new Reports();
    vmCookies cook = new vmCookies();
    GridView gd = new GridView();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (!Page.IsPostBack)
            {
                rpt.Manager_FillCollegeByManagerCode(cook.Manager_Id(), ddlCollege);
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
           rptTshirt.DataSource= rpt.Manager_FillTshirtGridview(cook.Manager_Id(), txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollege.SelectedValue.ToString(), ddlSize.SelectedValue.ToString());
           rptTshirt.DataBind();
        }
        catch (Exception)
        {
        }
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {

            GridView gd = new GridView();
            gd.DataSource = rpt.Manager_FillTshirtGridview(cook.Manager_Id(), txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollege.SelectedValue.ToString(), ddlSize.SelectedValue.ToString());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {
                string name = cook.ManagerName() + "_" + System.DateTime.Now.ToString();
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

    protected void btnPDFReport_Click(object sender, EventArgs e)
    {
        try
        {

            GridView gd = new GridView();
            gd.DataSource = rpt.Manager_FillTshirtGridview(cook.Manager_Id(), txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollege.SelectedValue.ToString(), ddlSize.SelectedValue.ToString());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gd.AllowPaging = false;
                gd.DataBind();
                gd.RenderControl(hw);
                string name = cook.ManagerName() + "_" + System.DateTime.Now.ToString();

                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".pdf");
                Response.ContentType = "application/pdf";
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new Document(PageSize.LEGAL, 1f, 1f, 1f, 0f);
                pdfDoc.SetPageSize(iTextSharp.text.PageSize.LEGAL.Rotate());



                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
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