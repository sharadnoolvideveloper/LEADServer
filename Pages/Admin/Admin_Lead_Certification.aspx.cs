using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_Lead_Certification : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                pnlUpload.Visible = false;
             
                BLobj.FillAademicYearWithTop(ddlAcademicYear);
                FillGridView();
             }
        }
        catch (Exception)
        {

        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DataLL DL = new DataLL();
            DataTable dt = DL.Get_Trainer_Certificate_List(ddlAcademicYear.SelectedValue.ToString(), ddlLevel.SelectedValue.ToString());
            rptStudents.DataSource = dt;
            rptStudents.DataBind();
            if (dt.Rows.Count > 0)
            {
                GridView gd = new GridView();
                gd.DataSource = dt;
                gd.DataBind();

                string name = "";
              
                    name = ddlLevel.SelectedValue.ToString() + "_" + ddlAcademicYear.SelectedItem.Text.ToString() + "StaffCerficate";
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
                string msg = "Data not Available";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void rptStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //Label ProjectStatus = (e.Item.FindControl("lblProjectStatus") as Label);
                // LinkButton btnPayee = (e.Item.FindControl("btnPayee") as LinkButton);
                Label lblStatus = (e.Item.FindControl("lblStatus") as Label);
                Label lblNewStatus = (e.Item.FindControl("lblNewStatus") as Label);

                if (lblStatus.Text.ToString() == "0")
                {

                    lblNewStatus.Text = "No Certificate";
                    lblNewStatus.Attributes.Add("class", "label label-warning");
                }
                else if (lblStatus.Text.ToString() == "1")
                {
                    lblNewStatus.Text = "Certificate Generated";
                    lblNewStatus.Attributes.Add("class", "label label-primary");
                }
                else if (lblStatus.Text.ToString() == "2")
                {
                    lblNewStatus.Text = "Mail Sent";
                    lblNewStatus.Attributes.Add("class", "label label-info");
                }
                else if (lblStatus.Text.ToString() == "3")
                {
                    lblNewStatus.Text = "Error on Mailsent";
                    lblNewStatus.Attributes.Add("class", "label label-danger");
                }


            }
        }
        catch (Exception)
        {

        }
    }

    protected void rptStudents_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            string TrainerName = "";
            string MailId = "";
            string Status = "";
            string Slno = "";
            string code = e.CommandArgument.ToString();
            string[] itemlist = code.Split('_');
            int i;
            for (i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    TrainerName = itemlist[0].ToString();
                }
                else if (i == 1)
                {
                    MailId = itemlist[1].ToString();
                }
                else if (i == 2)
                {
                    Status = itemlist[2].ToString();
                }
                else if (i == 3)
                {
                    Slno = itemlist[3].ToString();
                }
            }
            if ((Status == "1") || (Status == "2"))
            {
                
                string FilePath = "~/Certificate/TrainersCertificate/" + TrainerName.ToString() + "_" + MailId.ToString() +"_"+ddlLevel.SelectedValue.ToString()+ ".pdf"; ;
                lblCertificateUserName.Text = TrainerName.ToString();
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"600px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl(FilePath));
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_PDFView();", true);
            }
            else
            {
                string msg = "Certificate Not Generated!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            DataLL DL = new DataLL();
            vmCookies cook = new vmCookies();
            foreach (RepeaterItem ri in rptStudents.Items)
            {

                CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                if (ChkCertificate.Checked == true)
                {
                    string StudentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ChkCertificate.Text.ToLower());;
                    Label lblEmailId = (Label)ri.FindControl("lblEmailId");
                    Label lblSlno = (Label)ri.FindControl("lblSlno");

                    Label lblStatus = (Label)ri.FindControl("lblStatus");
                    if ((lblStatus.Text == "1") || (lblStatus.Text == "2"))
                    {
                        using (MailMessage mm = new MailMessage("leadmis@dfmail.org", lblEmailId.Text.ToString()))
                        {
                           
                            string body = PopulateBody(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(StudentName.ToString().ToLower()), "", "Please Find a Below Attached Trainer Certificate", "");
                            // SendHtmlFormattedEmail(Mailid.ToString(), "Project Proposal Details", body);

                            mm.Subject = txtMail_Subject.Text.ToString();
                            mm.Body = body.ToString();
                            Attachment imgCertificate = new Attachment(Server.MapPath("~/Certificate/TrainersCertificate/" + StudentName.ToString() + "_" + lblEmailId.Text.ToString() +"_"+ddlLevel.SelectedValue.ToString()+ ".pdf"));
                            //Attachment DocumentAttach = new Attachment(Server.MapPath("~/CSS/Images/Initiative_Projects.docx"));

                            mm.Attachments.Add(imgCertificate);
                            //mm.Attachments.Add(DocumentAttach);
                            mm.IsBodyHtml = true;
                          //  mm.Bcc.Add(new MailAddress("abhinandan.k@dfmail.org")); 
                            mm.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
                            using (SmtpClient smtp = new SmtpClient())
                            {
                                smtp.Host = "smtp.gmail.com";
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = new NetworkCredential
                                {
                                    UserName = "leadmis@dfmail.org",
                                    Password = "leadcampusadmin"
                                };
                                mm.Attachments.Add(imgCertificate);
                                smtp.Port = 587;
                                smtp.EnableSsl = true;
                                smtp.Send(mm);
                                if (lblStatus.Text != "2")
                                {
                                    DL.Upload_Trainer_Certificate_Progress(long.Parse(lblSlno.Text.ToString()), int.Parse(cook.Admin_Id()), 2);
                                    
                                }

                            }
                        }
                    }

                }
            }
            string msg = "Certificate Sent SuccessFully!!!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            FillGridView();
        }
        catch (Exception)
        {

        }
    }

    
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            foreach (RepeaterItem ri in rptStudents.Items)
            {
                CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                if (ChkCertificate.Checked == true)
                {
                    count =count+ 1;
                }
            }
            if (count > 0)
            {
               
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirm();", true);
            }
            else
            {
                string msg = "Select the Recipients";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception ex)
        {
            string msg = "Error : "+ ex.Message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

        }
      
    }
    private string PopulateBody(string userName, string title, string url, string description)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("/Pages/Appreciation_EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);
        return body;
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DataLL DL = new DataLL();
        vmCookies cook = new vmCookies();
        try
        {
            string filepath = Server.MapPath("~/Certificate/lead_Certification.xls");
            DataTable dt = new DataTable();
       

            string SlapDest = "";
            string Source = "";
            string Trainer_Name = "";
            string Lead_Id = "";
            int count = 0;
            if(ddlLevel.SelectedValue== "Level_1")
            {
                Source = "~/Reports/Trainer_Certificate_1.pdf";
            }
            else if (ddlLevel.SelectedValue == "Level_2")
            {
                Source = "~/Reports/Trainer_Certificate_2.pdf";
            }
            else if (ddlLevel.SelectedValue == "Level_3")
            {
                Source = "~/Reports/Trainer_Certificate_3.pdf";
            }

            
             foreach (RepeaterItem ri in rptStudents.Items)
            {
                CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                if (ChkCertificate.Checked == true)
                {
                    count = 1;

                    Label lblLead_Id = (Label)ri.FindControl("lblLead_Id");
                    Label lblSlno = (Label)ri.FindControl("lblSlno");
                    Label lblMailId = (Label)ri.FindControl("lblEmailId");
                    Trainer_Name = "LEAD" + "er "+ CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ChkCertificate.Text.ToLower());
                    SlapDest = Trainer_Name +"_"+lblMailId.Text.ToString()+ "_" + ddlLevel.SelectedValue.ToString() + ".pdf";

                    var reader = new PdfReader(Server.MapPath(Source));

                    FileStream fs = null;
                    fs = new System.IO.FileStream(Server.MapPath("~/Certificate/TrainersCertificate/" + SlapDest), FileMode.Create, FileAccess.Write);
                    var document = new Document(reader.GetPageSizeWithRotation(1));
                    var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                    document.Open();
                    document.NewPage();
                    var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                    // var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);
                    var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/fonts/Lora-Italic.ttf"), BaseFont.IDENTITY_H, true);

                    var importedPage = writer.GetImportedPage(reader, 1);
                    var contentByte = writer.DirectContent;
                    contentByte.BeginText();
                    contentByte.SetFontAndSize(baseFont1, 26);
                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Trainer_Name.ToString(), 300, 560, 0);

                    //int length = StudentName.ToString().ToString().Length;
                    //contentByte.SetFontAndSize(baseFont1, 18);
                    //if (length >= 50)
                    //{
                    //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 465, 165, 0);
                    //}
                    //else
                    //{
                    //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 420, 165, 0);
                    //}

                    contentByte.SetFontAndSize(baseFont, 11);
                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblLead_Id.Text.ToString(), 175, 301, 0);

                    contentByte.SetFontAndSize(baseFont, 11);
                    //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, System.DateTime.Now.ToString("dd-MM-yyyy"), 155, 271, 0);
                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtCertificateDate.Text, 155, 271, 0);
                    PdfGState graphicsState = new PdfGState();
                    graphicsState.BlendMode = PdfGState.BM_DARKEN;
                    contentByte.SetGState(graphicsState);
                    contentByte.EndText();
                    contentByte.AddTemplate(importedPage, 0, 0);
                    document.Close();
                    writer.Close();

                    DL.Upload_Trainer_Certificate_Progress(long.Parse(lblSlno.Text), int.Parse(cook.Admin_Id()), 1);
                }
            }
            FillGridView();
        }
        catch (Exception ex)
        {

        }
    }


    private DataTable GetExcelData(string filepath)
    {
        string oledbConnectionString = string.Empty;
        OleDbConnection conn = null;
        oledbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filepath + "; Extended Properties=Excel 8.0;";
        conn = new OleDbConnection(oledbConnectionString);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        OleDbCommand command = new OleDbCommand("Select * from [Sheet1$]", conn);
        OleDbDataAdapter objAdapter = new OleDbDataAdapter();
        objAdapter.SelectCommand = command;
        DataSet objDataset = new DataSet();
        objAdapter.Fill(objDataset, "TrainerExcel");
        conn.Close();
        return objDataset.Tables[0];
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DataLL DL = new DataLL();
        vmCookies cook = new vmCookies();
        string FileName = "";
        try
        {
                DataTable dt = new DataTable();
                string filepath = Server.MapPath("~/FileUploader");
            
                    System.IO.DirectoryInfo di = new DirectoryInfo(filepath);
                    foreach (FileInfo file in di.GetFiles())
                    {
                     FileName= file.Name.ToString();
                    }
                  //  Uploader.SaveAs(filepath + "\\" + Uploader.FileName.Split('\\')[Uploader.FileName.Split('\\').Length - 1]);
                    filepath = Server.MapPath("~/FileUploader\\" + FileName.ToString());

                    dt = GetExcelData(filepath);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string Trainer_Name = dr[1].ToString().Trim().ToLower();
                            string Lead_Id = dr[2].ToString().ToUpper();
                            string Mail_Id = dr[3].ToString();
                            string status = DL.Upload_Trainer_Certificate(Trainer_Name.ToString(), Lead_Id.ToString(), Mail_Id.ToString(), ddlLevel.SelectedValue.ToString(),int.Parse(cook.Admin_Id()));
                        }
                FillGridView();
                pnlUpload.Visible = false;
                ddlLevel.Enabled = true;
            }
             
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void FillGridView()
    {
        try
        {
            DataLL DL = new DataLL();
            DataTable dt = DL.Get_Trainer_Certificate_List(ddlAcademicYear.SelectedValue.ToString(), ddlLevel.SelectedValue.ToString());
            rptStudents.DataSource = dt;
            rptStudents.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
     
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        string existingDetails = "";
        try
        {
            DataTable dt = new DataTable();
            DataLL DL = new DataLL();
            string FileExtenssion = System.IO.Path.GetExtension(Uploader.FileName);
            string filepath = Server.MapPath("~/FileUploader");
            if ((FileExtenssion == ".xls") || (FileExtenssion == ".XLS"))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(filepath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                Uploader.SaveAs(filepath + "\\" + Uploader.FileName.Split('\\')[Uploader.FileName.Split('\\').Length - 1]);
                filepath = Server.MapPath("~/FileUploader\\" + Uploader.FileName.ToString());

                dt = GetExcelData(filepath);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string Trainer_Name = dr[1].ToString().Trim().ToLower();
                        string Lead_Id = dr[2].ToString().ToUpper();
                        string Mail_Id = dr[3].ToString();
                        dt = DL.Get_Check_Trainer_Certificate_Exists(Lead_Id.ToString(), ddlLevel.SelectedValue.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            existingDetails = existingDetails + "," + Lead_Id + "-" + ddlLevel.SelectedValue.ToString() + "<br />";
                           
                        }
                    }
                    if (existingDetails != "")
                    {
                        pnlUpload.Visible = false;
                     
                        lblExisting_Users.Text = string.Format(existingDetails.TrimEnd(',').TrimStart(','));
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Existing();", true);
                    }
                    else
                    {
                        ddlLevel.Enabled = false;
                        pnlUpload.Visible = true;
                       
                    }
                }
            }
            else
            {
                pnlUpload.Visible = false;
               
                lblError_Message.Text = "Selected file format should be .xls OR .XLS";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Error();", true);
              
            }
        }
        catch (Exception ex)
        {
            pnlUpload.Visible = false;
            lblError_Message.Text = "Error : " + ex.Message.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Error();", true);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            string filepath = Server.MapPath("~/FileUploader");
            pnlUpload.Visible = false;
            ddlLevel.Enabled = true;
            System.IO.DirectoryInfo di = new DirectoryInfo(filepath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
          
        }
        catch (Exception)
        {

       
        }
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridView();
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridView();
    }
}

    