using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;

public partial class Pages_Admin_YuvaSummit_Certificate : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BLobj.FillAademicYearWithTop(ddlAcademicYear);
            BLobj.FillYuvaCertificateCollege(ddlCollegeName,ddlType,ddlAcademicYear.SelectedValue.ToString());
            DataTable dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(),ddlCollegeName.SelectedItem.Text,"[All]", ddlType.SelectedItem.Text.ToString());
            rptStudents.DataSource = dt;
            rptStudents.DataBind();
        }
    }

  
    protected void SendEmail(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirm();", true);


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SlapDest = "";
            string Source = "";
            string Promotion = "";
            if (ddlType.SelectedItem.Text.ToString() == "YS")
            {
                Source = "~/Reports/yuva_certificate.pdf";
            }
            else if (ddlType.SelectedItem.Text.ToString() == "Star_1")
            {
                Source = "~/Reports/Start_1.pdf";
                Promotion = "Successfully completed project";
            }
            else if (ddlType.SelectedItem.Text.ToString() == "Star_2")
            {
                Source = "~/Reports/Start_1.pdf";
                Promotion = "Successfully completed project";
            }
            else if (ddlType.SelectedItem.Text.ToString() == "Star_3")
            {
                Source = "~/Reports/Start_1.pdf";
                Promotion = "Successfully completed project";
            }

            foreach (RepeaterItem ri in rptStudents.Items)
            {

                CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                if (ChkCertificate.Checked == true)
                {
                    string StudentName = ChkCertificate.Text;
                    Label lblEmailId = (Label)ri.FindControl("lblEmailId");
                    Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");
                    Label lblSlno = (Label)ri.FindControl("lblSlno");
                    Label lblInstituteName = (Label)ri.FindControl("lblInstituteName");
                    Label lblStatus = (Label)ri.FindControl("lblStatus");
                    Label lblLead_Id = (Label)ri.FindControl("lblLead_Id");
                    Label lblType = (Label)ri.FindControl("lblType");
                    SlapDest = lblType.Text.ToString()+"_"+ StudentName + "_" + lblEmailId.Text.ToString() + ".pdf";

                    var reader = new PdfReader(Server.MapPath(Source));

                    FileStream fs = null;
                    
                    if (lblType.Text.ToString() == "YS")
                    {
                        fs = new System.IO.FileStream(Server.MapPath("~/Certificate/YuvaSummit/" + SlapDest), FileMode.Create, FileAccess.Write);
                    }
                    else
                    {
                        fs = new System.IO.FileStream(Server.MapPath("~/Certificate/Appreciate/" + SlapDest), FileMode.Create, FileAccess.Write);
                    }
                 
                         
              

                    var document = new Document(reader.GetPageSizeWithRotation(1));
                    var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                    document.Open();
                    document.NewPage();

                    var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                    var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);


                    var importedPage = writer.GetImportedPage(reader, 1);

                    var contentByte = writer.DirectContent;
                 
                 
                    if (lblType.Text == "YS")
                    {
                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont, 30);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 420, 190, 0);

                        contentByte.SetFontAndSize(baseFont, 13);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "From " + lblInstituteName.Text.ToString() + " participated in",400, 220, 0);
                        //contentByte.SetTextMatrix(30, 220);
                        int length = ("From" + lblInstituteName.Text.ToString() + "participated in").ToString().Length;

                        if ((length >= 26) && (length <= 67))
                        {
                            contentByte.SetTextMatrix(200, 220);
                        }
                        else if ((length >= 68) && (length <= 80))
                        {
                            contentByte.SetTextMatrix(100, 220);
                        }
                        if ((length >= 81) && (length <= 90))
                        {
                            contentByte.SetTextMatrix(80, 220);
                        }
                        else if ((length >= 91) && (length <= 125))
                        {
                            contentByte.SetTextMatrix(40, 220);
                        }
                        contentByte.SetFontAndSize(baseFont1, 20);
                        //       contentByte.ShowText("From ");
                        contentByte.SetFontAndSize(baseFont, 13);
                        //    contentByte.ShowText(lblInstituteName.Text.ToString());
                        contentByte.SetFontAndSize(baseFont1, 20);

                        //     contentByte.ShowText("  participated in");
                        //  create new graphics state and assign opacity
                        PdfGState graphicsState = new PdfGState();
                        graphicsState.BlendMode = PdfGState.BM_DARKEN;
                        //  graphicsState.FillOpacity = 0.6F;
                        //set graphics state to pdfcontentbyte


                        contentByte.SetGState(graphicsState);

                        string imageURL = Server.MapPath("~/CSS/Images/Signature.png");
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                        //Resize image depend upon your need
                        jpg.ScaleToFit(150f, 150f);
                        //Give space before image
                        jpg.SpacingBefore = 10f;
                        //Give some space after the image
                        jpg.SpacingAfter = 250f;
                        jpg.SetAbsolutePosition(80, 60);

                        jpg.Alignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                        //   document.Add(jpg);

                        contentByte.EndText();
                        contentByte.AddTemplate(importedPage, 0, 0);
                        document.Close();
                        writer.Close();
                    }
                    else
                    {
                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont1, 20);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 420, 200, 0);

                        contentByte.SetFontAndSize(baseFont1, 13);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblInstituteName.Text.ToString(), 420, 165, 0);

                        contentByte.SetFontAndSize(baseFont1, 15);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Promotion.ToString(), 420, 90, 0);


                        contentByte.SetFontAndSize(baseFont, 10);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, System.DateTime.Now.ToString("dd-MM-yyyy"), 85, 50, 0);

                        contentByte.SetFontAndSize(baseFont, 10);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblLead_Id.Text.ToString(), 85, 25, 0);


                        PdfGState graphicsState = new PdfGState();
                        graphicsState.BlendMode = PdfGState.BM_DARKEN;
                        contentByte.SetGState(graphicsState);


                        contentByte.EndText();
                        contentByte.AddTemplate(importedPage, 0, 0);
                        document.Close();
                        writer.Close();

                    }

                    if (lblStatus.Text!="2")
                    {
                        BLobj.UpdateCertificateStatus(lblSlno.Text.ToString(), ddlAcademicYear.SelectedValue.ToString(), 1);
                    }
                }
            }
            DataTable dt;
            if (ddlCollegeName.SelectedItem.Text == "[All]")
            {
                dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlCollegeName.SelectedItem.Text, "[All]", ddlType.SelectedItem.Text.ToString());
            }
            else
            {
                dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlCollegeName.SelectedItem.Text, "", ddlType.SelectedItem.Text.ToString());
            }

            rptStudents.DataSource = dt;
            rptStudents.DataBind();
            string msg = "Certificates Generated Successfully!!!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch (Exception ex)
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
                    Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");
                    Label lblInstituteName = (Label)ri.FindControl("Institute_Name");
                    Label lblStatus = (Label)ri.FindControl("lblStatus");
                    if ((lblStatus.Text == "1") || (lblStatus.Text == "2"))
                    {
                        using (MailMessage mm = new MailMessage("leadmis@dfmail.org", lblEmailId.Text.ToString()))
                        {
                            string body = "Dear" + " " + StudentName.ToString() + " " + "Participant of LEAD Yuva Summit 2020,<br/><br/> Please find the Participation Certificate of 10th Yuva Summit, hosted on 1st of February 2020.";
                            //string body = PopulateBody(StudentName.ToString()+" "+ "Participant of LEAD Yuva Summit 2019,", "", "", "Please find the Participation Certificate of 9th Yuva Summit, hosted on 2nd of February 2019.");
                            mm.Subject = "Yuva Summit Certificate";
                            Attachment imgCertificate = new Attachment(Server.MapPath("~/Certificate/YuvaSummit/" + StudentName + "_" + lblEmailId.Text.ToString() + ".pdf"));
 
                            mm.Attachments.Add(imgCertificate);
                            
                            mm.Body = body.ToString();
                            mm.IsBodyHtml = true;
                            mm.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
                           // mm.Bcc.Add(new MailAddress("abhinandan.k@dfmail.org"));
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
                                smtp.Port = 587;
                                smtp.EnableSsl = true;
                                smtp.Send(mm);
                                if(lblStatus.Text!="2")
                                {
                                    BLobj.UpdateCertificateStatus(lblSlno.Text.ToString(), ddlAcademicYear.SelectedValue.ToString(), 2);
                                }                                
                            }
                        }
                    }
                }
            }
            DataTable dt;
            if (ddlCollegeName.SelectedItem.Text == "[All]")
            {
                dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlCollegeName.SelectedItem.Text, "[All]",ddlType.SelectedItem.Text.ToString());
            }
            else
            {
                dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlCollegeName.SelectedItem.Text, "", ddlType.SelectedItem.Text.ToString());
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
            if (ddlCollegeName.SelectedItem.Text=="[All]")
            {
                dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlCollegeName.SelectedItem.Text, "[All]",ddlType.SelectedItem.Text.ToString());
            }
            else
            {
               dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlCollegeName.SelectedItem.Text, "", ddlType.SelectedItem.Text.ToString());
            }
           
            rptStudents.DataSource = dt;
            rptStudents.DataBind();
        }
        catch(Exception)
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
            
                if(lblStatus.Text.ToString() == "0")
                {

                    lblNewStatus.Text = "No Certificate";
                    lblNewStatus.Attributes.Add("class", "label label-warning");
                }
            else if(lblStatus.Text.ToString()=="1")
                {
                    lblNewStatus.Text = "Certificate Generated";
                    lblNewStatus.Attributes.Add("class", "label label-primary");
                }
             else if(lblStatus.Text.ToString()=="2")
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
            string Slno="";
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
            if((Status=="1") || (Status == "2"))
            {
               
                string FilePath ="~/Certificate/YuvaSummit/" + StudentName + "_" + MailId.ToString() + ".pdf";
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
        catch(Exception)
        {

        }
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

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.FillYuvaCertificateCollege(ddlCollegeName, ddlType, ddlAcademicYear.SelectedValue.ToString());
            DataTable dt = BLobj.GetYuvaSummitCertificate(ddlAcademicYear.SelectedValue.ToString(), ddlCollegeName.SelectedItem.Text, "[All]", ddlType.SelectedItem.Text.ToString());
            rptStudents.DataSource = dt;
            rptStudents.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
}