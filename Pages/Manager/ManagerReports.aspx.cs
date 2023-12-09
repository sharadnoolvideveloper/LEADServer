using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Ionic.Zip;
using System.Text;


public partial class Pages_Manager_ManagerReports : System.Web.UI.Page
{
    vmCookies cook = new vmCookies();
    LeadBL BLobj = new LeadBL();
    Reports rpt = new Reports();
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BLobj.FillAademicYear(ddlAcademicYear);
            ddlAcademicYear.SelectedIndex = 1;
            ll.Visible = false;
           
        }
    }

    protected void btnGenerateListingReports_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gd = new GridView();
            gd.DataSource = rpt.Manager_GetStudentListWithFunded(cook.Manager_Id(),ddlAcademicYear.SelectedValue.ToString());
            gd.DataBind();
            Download_report(gd);
        }
        catch(Exception)
        {

        }
    }
    public void Download_report(GridView gd)
    {
        try
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
        catch (Exception)
        {

        }
    }


    protected void btnGenerateProjectStatusWiseReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gd = new GridView();
            gd.DataSource = rpt.Manager_AcademicYearAndProjectStatusWiseReport(cook.Manager_Id(), ddlAcademicYear.SelectedValue.ToString(),ddlProjectType.SelectedValue.ToString());
            gd.DataBind();
            Download_report(gd);
        }
        catch (Exception)
        {

        }
    }

    protected void btnGenerateFolderWiseData_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtFolderFromDate.Text != "") && (txtFolderToDate.Text != ""))
            {
                string sourcePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
                // Use Path class to manipulate file and directory paths.
                //string sourceFile = System.IO.Path.Combine(Server.MapPath(sourcePath), fileName);
                //string destFile = System.IO.Path.Combine(Server.MapPath(targetPath), fileName);
                System.Data.DataTable dt = rpt.Manager_StudentWithProjectsFolderCreating(cook.Manager_Id(), txtFolderFromDate.Text, txtFolderToDate.Text);

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                foreach (DataRow dr in dt.Rows)
                {
                    string files = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id())).ToString();
                    bool IsExists = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id()));
                    if (!IsExists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + cook.Manager_Id()));
                    }
                    //foreach (string directory in Directory.GetDirectories(Server.MapPath(targetPath + cook.Manager_Id())))
                    //{
                    //    foreach (string file in Directory.GetFiles(directory))
                    //    {
                    //        File.Delete(file);
                    //    }

                    //    Directory.Delete(directory);
                    //}

                    bool isTitleExists = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id() + dr[0].ToString() + dr[2].ToString()));

                    if (!isTitleExists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + cook.Manager_Id() + "/" + dr[0].ToString() + "/" + dr[2].ToString()));
                        System.Data.DataTable dtfiles = rpt.Manager_StudentWithDigitalDocuments(dr[1].ToString());
                        string dest = Server.MapPath(targetPath + cook.Manager_Id() + "/" + dr[0].ToString() + "/" + dr[2].ToString());
                        foreach (DataRow dr1 in dtfiles.Rows)
                        {
                            string fileName = System.IO.Path.GetFileName(Server.MapPath(dr1[1].ToString()));
                            bool isImgExists = File.Exists(Server.MapPath(sourcePath + fileName));
                            if (isImgExists == true)
                            {
                                string destFile = System.IO.Path.Combine(Server.MapPath(targetPath + cook.Manager_Id() + "/" + dr[0].ToString() + "/" + dr[2].ToString()), fileName);
                                System.IO.File.Copy(Server.MapPath(dr1[1].ToString()), destFile, true);
                            }

                        }
                    }
                    CreateWordDocuments(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

                    //System.IO.File.Copy(sourceFile, Server.MapPath(targetPath+dt.Rows[0].ItemArray[0].ToString()), true);
                }
                if (dt.Rows.Count > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        Response.Clear();
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "filename=" + cook.Manager_Id() + "_" + cook.ManagerName() + "_Lead.zip");
                        // zip.AddDirectory(Server.MapPath("/FilesDownload/26"));
                        zip.AddDirectory(Server.MapPath(targetPath + cook.Manager_Id()));
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }
                }
                else
                {
                    msg = "No Data Fount Select Different Dates";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {
                msg = "Please Select From And To Date";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

            }
        }
        catch (Exception ex)
        {
            msg= "Some thing Went Wrong" + "-" + ex.Message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            ll.Text = ex.Message.ToString();
        }
    }
    public void CreateWordDocuments(string Lead_Id,string PDId,string Title)
    {
        Reports rpt = new Reports();
        string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
        string MainFolder = Server.MapPath(targetPath);
        string filename = Server.MapPath(targetPath + cook.Manager_Id() + "/" + Lead_Id.ToString() + "/" + Title.ToString() + "/" + Title.ToString() + ".docx");
        rpt.CreateWordDocuments(PDId, targetPath, MainFolder, filename);
    }
    protected void btnGenerateDocuments_Click(object sender, EventArgs e)
    {
        
    }
   


    protected void btnGenerateFundingBetweenDates_Click(object sender, EventArgs e)
    {
        try
        {
            if((txtFromDate.Text.ToString()=="") || (txtToDate.Text==""))
            {
                msg = "Please Select From Date and To Date";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                GridView gd = new GridView();
                gd.DataSource = rpt.Manager_GetFundingDetailsBetweenDates(cook.Manager_Id(), txtFromDate.Text.ToString(), txtToDate.Text.ToString());
                gd.DataBind();
                Download_report(gd);

            }

           
        }
        catch(Exception)
        {

        }
    }
}