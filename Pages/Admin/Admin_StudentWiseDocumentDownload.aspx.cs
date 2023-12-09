using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Pages_Admin_Admin_StudentWiseDocumentDownload : System.Web.UI.Page
{
    Reports rpt = new Reports();
    vmCookies cook = new vmCookies();
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rpt.Admin_Fillprogramddl(ddlprogramId, cook.Admin_Id());
            rpt.Admin_FillManagerByprogram(ddlprogramId.SelectedValue.ToString(), ddlManagerId);
            //rpt.Admin_FillManagerWithSelect(ddlManagerId, cook.Admin_Id());

        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rpt.Admin_FillManagerByprogram(ddlprogramId.SelectedValue.ToString(), ddlManagerId);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            LeadBL BLobj = new LeadBL();
            if ((txtFromDate.Text != "") && (txtToDate.Text != "") && (ddlManagerId.SelectedValue.ToString() != "[Select]"))
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                dt = rpt.Manager_GetListingReportFolderWise(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlManagerId.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString());

                lblCount.Text = dt.Rows.Count.ToString();
                if (dt != null)
                {
                    rptAllProjects.DataSource = dt;
                    rptAllProjects.DataBind();
                }
            }
            else
            {
                if (txtFromDate.Text == "")
                {
                    string msg = "Select From Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtFromDate.Focus();
                }
                else if (txtToDate.Text == "")
                {
                    string msg = "Select To Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtToDate.Focus();
                }
                else if (ddlManagerId.SelectedValue.ToString() == "[Select]")
                {
                    string msg = "Select Particular Manager";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    ddlManagerId.Focus();
                }
            }
        }
        catch (Exception ex)
        {

            string msg = "Error : " + ex.Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }
    protected void rptAllProjects_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            Label LeadID = (e.Item.FindControl("lblLeadId") as Label);
            if (rptAllProjects.Items.Count > 0)
            {
                int i = 0;
                string sourcePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
                bool IsExists = System.IO.Directory.Exists(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString()));
                if (!IsExists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString()));
                }

                foreach (string directory in Directory.GetDirectories(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString())))
                {
                    foreach (string directory2 in Directory.GetDirectories(directory))
                    {
                        foreach (string file in Directory.GetFiles(directory2))
                        {
                            File.Delete(file);
                        }
                        Directory.Delete(directory2);
                    }
                    Directory.Delete(directory);
                }
                string files = System.IO.Directory.Exists(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString())).ToString();


                DataTable dt = rpt.Common_LeadIdWithProjectsListFolderCreating(ddlManagerId.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), LeadID.Text.ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    bool isTitleExists = System.IO.Directory.Exists(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + LeadID.Text.ToString() + dr[1].ToString().Trim()));

                    if (!isTitleExists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + "/" + LeadID.Text.ToString() + "/" + dr[1].ToString().Trim()));
                        System.Data.DataTable dtfiles = rpt.Manager_StudentWithDigitalDocuments(dr[0].ToString());
                        string dest = Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + "/" + LeadID.Text.ToString() + "/" + dr[1].ToString().Trim());
                        foreach (DataRow dr1 in dtfiles.Rows)
                        {
                            string fileName = System.IO.Path.GetFileName(Server.MapPath(dr1[1].ToString()));
                            bool isImgExists = File.Exists(Server.MapPath(sourcePath + fileName));
                            if (isImgExists == true)
                            {
                                string destFile = System.IO.Path.Combine(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + "/" + LeadID.Text.ToString() + "/" + dr[1].ToString().Trim()), fileName);
                                System.IO.File.Copy(Server.MapPath(dr1[1].ToString()), destFile, true);
                            }

                        }
                    }
                    CreateWordDocuments(LeadID.Text.ToString(), dr[0].ToString(), dr[1].ToString().Trim());
                    i++;
                }

                dt = rpt.Manager_GetListingReportFolderWise(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlManagerId.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString());
                rptAllProjects.DataSource = dt;
                rptAllProjects.DataBind();

                if (i > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        Response.Clear();
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "filename=" + ddlManagerId.SelectedValue.ToString() + "_" + cook.ManagerName() + "_Lead.zip");
                        // zip.AddDirectory(Server.MapPath("/FilesDownload/26"));
                        zip.AddDirectory(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString()));
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }

                }
                else
                {
                    string msg = "No Data Fount Selected Dates OR Students Not Selected";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {
                string msg = "Please Select From And To Date";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

            }


        }
        catch (Exception)
        {

        }
    }

    protected void rptAllProjects_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblLeadId = (e.Item.FindControl("lblLeadId") as Label);
                LinkButton btnDownload = (e.Item.FindControl("btnDownload") as LinkButton);
                string isFolderDownload = rpt.Common_ReturnFolderDownload(lblLeadId.Text.ToString());
                if (isFolderDownload.ToString() == "0")
                {
                    btnDownload.Text = "<span class='fa fa-download'></span>";
                    btnDownload.Attributes.Add("class", "btn btn-warning btn-floating");
                }
                else
                {
                    btnDownload.Text = "<span class='fa fa-download'></span>";
                    btnDownload.Attributes.Add("class", "btn btn-success btn-floating");

                }
            }
        }
        catch (Exception)
        {

        }

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            if (rptAllProjects.Items.Count > 0)
            {
                int i = 0;
                string sourcePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();


                bool IsExists = System.IO.Directory.Exists(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString()));
                if (!IsExists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString()));
                }
                foreach (string directory in Directory.GetDirectories(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString())))
                {
                    foreach (string directory2 in Directory.GetDirectories(directory))
                    {
                        foreach (string file in Directory.GetFiles(directory2))
                        {
                            File.Delete(file);
                        }
                        Directory.Delete(directory2);
                    }
                    Directory.Delete(directory);
                }
                foreach (RepeaterItem ri in rptAllProjects.Items)
                {

                    CheckBox Chk = (CheckBox)ri.FindControl("ChkStudentSelect");
                    if (Chk.Checked == true)
                    {
                        Label LeadID = (Label)ri.FindControl("lblLeadId");

                        string files = System.IO.Directory.Exists(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString())).ToString();



                        DataTable dt = rpt.Common_LeadIdWithProjectsListFolderCreating(ddlManagerId.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), LeadID.Text.ToString());
                        foreach (DataRow dr in dt.Rows)
                        {


                            bool isTitleExists = System.IO.Directory.Exists(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + LeadID.Text.ToString() + dr[1].ToString()));

                            if (!isTitleExists)
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + "/" + LeadID.Text.ToString() + "/" + dr[1].ToString().Trim()));
                                System.Data.DataTable dtfiles = rpt.Manager_StudentWithDigitalDocuments(dr[0].ToString());
                                string dest = Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + "/" + LeadID.Text.ToString() + "/" + dr[1].ToString());
                                foreach (DataRow dr1 in dtfiles.Rows)
                                {
                                    string fileName = System.IO.Path.GetFileName(Server.MapPath(dr1[1].ToString().Trim()));
                                    bool isImgExists = File.Exists(Server.MapPath(sourcePath + fileName));
                                    if (isImgExists == true)
                                    {
                                        string destFile = System.IO.Path.Combine(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + "/" + LeadID.Text.ToString() + "/" + dr[1].ToString().Trim()), fileName);
                                        System.IO.File.Copy(Server.MapPath(dr1[1].ToString()), destFile, true);
                                    }

                                }
                            }
                            CreateWordDocuments(LeadID.Text.ToString(), dr[0].ToString(), dr[1].ToString().Trim());
                        }
                        i++;
                    }
                }
                DataTable dt1 = rpt.Manager_GetListingReportFolderWise(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlManagerId.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString());
                rptAllProjects.DataSource = dt1;
                rptAllProjects.DataBind();
                if (i > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        Response.Clear();
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "filename=" + ddlManagerId.SelectedValue.ToString() + "_" + ddlManagerId.SelectedItem.Text.ToString() + "_Lead.zip");
                        // zip.AddDirectory(Server.MapPath("/FilesDownload/26"));
                        zip.AddDirectory(Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString()));
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }

                }
                else
                {
                    string msg = "No Data Fount Select Different Dates";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {
                string msg = "Please Select From And To Date";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

            }
        }
        catch (Exception ex)
        {
            string msg = "Some thing Went Wrong" + "-" + ex.Message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

        }

    }
    public void CreateWordDocuments(string Lead_Id, string PDId, string Title)
    {
        Reports rpt = new Reports();
        string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
        string MainFolder = Server.MapPath(targetPath);
        string filename = Server.MapPath(targetPath + ddlManagerId.SelectedValue.ToString() + "/" + Lead_Id.ToString() + "/" + Title.ToString() + "/" + Title.ToString().Trim() + ".docx");
        rpt.CreateWordDocuments(PDId, targetPath, MainFolder, filename);
    }
}