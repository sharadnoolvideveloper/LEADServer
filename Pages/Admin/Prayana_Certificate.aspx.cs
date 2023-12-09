using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Prayana_Certificate : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BLobj.FillAademicYearWithTop(ddlAcademicYear);
                BLobj.FillPrayanaSandbox(ddlSandbox);
                DataTable dt = BLobj.GetPrayanaCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlSandbox.SelectedValue.ToString(), "[All]");
                rptStudents.DataSource = dt;
                rptStudents.DataBind();
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
            string SlapDest = "";
            string Source = "";
            Source = "~/Reports/General.pdf";
            if(ddlSandbox.SelectedItem.ToString()!="[All]")
            {
                foreach (RepeaterItem ri in rptStudents.Items)
                {
                  

                    CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                    if (ChkCertificate.Checked == true)
                    {
                        string StudentName = ChkCertificate.Text;
                        Label lblEmailId = (Label)ri.FindControl("lblEmailId");
                        Label lblSlno = (Label)ri.FindControl("lblSlno");
                        Label lblStatus = (Label)ri.FindControl("lblStatus");
                        SlapDest = StudentName.ToString() + "_" + lblEmailId.Text.ToString() + ".pdf";
                        var reader = new PdfReader(Server.MapPath(Source));
                        var document = new Document(reader.GetPageSizeWithRotation(1));
                        var fileStream = new System.IO.FileStream(Server.MapPath("~/Certificate/Prayana/" + SlapDest), FileMode.Create, FileAccess.Write);
                        var writer = PdfWriter.GetInstance(document, fileStream);
                        document.Open();
                        document.NewPage();

                        var contentByte = writer.DirectContent;
                        PdfGState graphicsState = new PdfGState();

                        var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                        var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);
                        contentByte.SetColorFill(BaseColor.BLACK);

                        //  var importedPage = writer.GetImportedPage(reader, 1);

                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont, 18);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, StudentName.ToString(), 330, 295, 0);
                        graphicsState.BlendMode = PdfGState.BM_NORMAL;
                        contentByte.SetGState(graphicsState);
                        contentByte.EndText();
                        PdfImportedPage page = writer.GetImportedPage(reader, 1);
                        contentByte.AddTemplate(page, 0, 0);
                        graphicsState.BlendMode = PdfGState.BM_DARKEN;
                        contentByte.SetGState(graphicsState);
                        string imagepath = "";
                        if (ddlSandbox.SelectedValue == "1") // Hubli sandbox
                        {
                            imagepath = (Server.MapPath("~/Reports/prayana_HB.jpg"));
                        }
                        else if (ddlSandbox.SelectedValue == "2") // Kakatiya sandbox
                        {
                            imagepath = (Server.MapPath("~/Reports/prayana_KT.jpg"));
                        }
                        else if (ddlSandbox.SelectedValue == "3") // Eksoch sandbox
                        {
                            imagepath = (Server.MapPath("~/Reports/prayana_ES.jpg"));
                        }
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagepath);
                        image.ScaleToFit(850f, 850f);
                        image.SetAbsolutePosition(-2, -4);
                        image.Alignment = Element.ALIGN_LEFT;
                        // image.ScalePercent(22f);
                        document.Add(image);
                        document.Close();
                        fileStream.Close();
                        writer.Close();
                        reader.Close();
                        if (lblStatus.Text != "2")
                        {
                            BLobj.UpdatePrayanaCertificateStatus(lblSlno.Text.ToString(), ddlAcademicYear.SelectedValue.ToString(), 1);
                        }

                    }
                }
                DataTable dt;
                if (ddlSandbox.SelectedItem.Text == "[All]")
                {
                    dt = BLobj.GetPrayanaCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlSandbox.SelectedValue.ToString(), "[All]");
                }
                else
                {
                    dt = BLobj.GetPrayanaCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlSandbox.SelectedValue.ToString(), "");
                }
                rptStudents.DataSource = dt;
                rptStudents.DataBind();
            }
            else
            {
                string msg = "Select the Sandbox";
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

            string StudentName = "";
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
                    StudentName = itemlist[0].ToString();
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

                string FilePath = "~/Certificate/Prayana/" + StudentName.ToString() + "_" + MailId.ToString()+ ".pdf"; ;
                lblCertificateUserName.Text = StudentName.ToString();
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

            foreach (RepeaterItem ri in rptStudents.Items)
            {

                CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                if (ChkCertificate.Checked == true)
                {
                    string StudentName = ChkCertificate.Text;
                    Label lblEmailId = (Label)ri.FindControl("lblEmailId");
                    Label lblSlno = (Label)ri.FindControl("lblSlno");
                 
                    Label lblStatus = (Label)ri.FindControl("lblStatus");
                    if ((lblStatus.Text == "1") || (lblStatus.Text == "2"))
                    {
                        using (MailMessage mm = new MailMessage("leadmis@dfmail.org", lblEmailId.Text.ToString()))
                        {
                            string body = PopulateBody(StudentName.ToString(),"", "Please Find a Below Attached Prayana Certificate", "");
                           // SendHtmlFormattedEmail(Mailid.ToString(), "Project Proposal Details", body);

                            mm.Subject = "Prayana Certificate";
                            mm.Body = body.ToString();
                            Attachment imgCertificate = new Attachment(Server.MapPath("~/Certificate/Prayana/" + StudentName.ToString() + "_" + lblEmailId.Text.ToString() + ".pdf"));
                            Attachment DocumentAttach = new Attachment(Server.MapPath("~/CSS/Images/Initiative_Projects.docx"));

                            mm.Attachments.Add(imgCertificate);
                            mm.Attachments.Add(DocumentAttach);
                            mm.IsBodyHtml = true;
                            //mm.Bcc.Add(new MailAddress("abhinandan.k@dfmail.org"));
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
                                    BLobj.UpdatePrayanaCertificateStatus(lblSlno.Text.ToString(), ddlAcademicYear.SelectedValue.ToString(), 2);
                                }

                            }
                        }
                    }

                }
            }
            DataTable dt;
            if (ddlSandbox.SelectedItem.Text == "[All]")
            {
                dt =  BLobj.GetPrayanaCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlSandbox.SelectedValue.ToString(), "[All]");
            }
            else
            {
               dt = BLobj.GetPrayanaCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlSandbox.SelectedValue.ToString(), "");
            }
            rptStudents.DataSource = dt;
            rptStudents.DataBind();
            string msg = "Certificate Sent SuccessFully!!!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            if (ddlSandbox.SelectedItem.Text == "[All]")
            {
                dt = BLobj.GetPrayanaCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlSandbox.SelectedValue.ToString(), "[All]");
            }
            else
            {
                dt = BLobj.GetPrayanaCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlSandbox.SelectedValue.ToString(), "");
            }
            rptStudents.DataSource = dt;
            rptStudents.DataBind();
        }
        catch(Exception)
        {

        }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirm();", true);
    }
    private string PopulateBody(string userName, string title, string url, string description)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("/Pages/Prayana_EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);
        body = body.Replace("{Title}", title);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Description}", description);
        return body;
    }
}