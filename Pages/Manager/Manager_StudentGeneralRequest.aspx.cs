using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.text.html.simpleparser;

public partial class Pages_Manager_Manager_GeneralRequest : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!Page.IsPostBack)
            {
                BLobj.FillAademicYearWithTop(ddlAcademicYear);
              
                DataTable dt= BLobj.Manager_FillStudentRequestRepeater(GetWhereConditions());                
                rptStudentRequest.DataSource = dt;
                rptStudentRequest.DataBind();
                lblRequestCount.Text ="Count :"+" "+ dt.Rows.Count.ToString();
               
            }
        }
        catch (Exception)
        {

            
        }
    }
    public string GetWhereConditions()
    {
        try
        {
            string Where = "";
            if(ddlRequestStatus.SelectedItem.ToString()=="Closed")
            {
                Where = "RD.Manager_Id=" + cook.Manager_Id() + " and RD.AcademicCode=" + ddlAcademicYear.SelectedValue.ToString() + " and RD.Status=2";
            }
            else if(ddlRequestStatus.SelectedItem.ToString() != "[All]")
            {
                Where = "RD.Manager_Id=" + cook.Manager_Id() + " and RD.AcademicCode=" + ddlAcademicYear.SelectedValue.ToString() + " and Request_Priority='" + ddlRequestStatus.SelectedItem.Text.ToString() + "'" + " " +
                "and RD.Status=1";
            }
            else if (ddlRequestStatus.SelectedItem.ToString() == "[All]")
            {
                Where = "RD.Manager_Id=" + cook.Manager_Id() + " and RD.AcademicCode=" + ddlAcademicYear.SelectedValue.ToString() + "";
            }
            
            return Where;
        }
        catch (Exception)
        {
            return "";
            
        }
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
           
            DataTable dt = BLobj.Manager_FillStudentRequestRepeater(GetWhereConditions());
            rptStudentRequest.DataSource = dt;
            rptStudentRequest.DataBind();
            lblRequestCount.Text = "Count :" + " " + dt.Rows.Count.ToString();
        }
        catch (Exception)
        {


        }
    }

    

    protected void ddlRequestStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
                      
            DataTable dt = BLobj.Manager_FillStudentRequestRepeater(GetWhereConditions());
            rptStudentRequest.DataSource = dt;
            rptStudentRequest.DataBind();
            lblRequestCount.Text = "Count :" + " " + dt.Rows.Count.ToString();
        }
        catch (Exception)
        {


        }
    }

    
    protected void btnCloseTicket_Click(object sender, EventArgs e)
    {
        try
        {
            int isDocCreated = 0;
            if (ChkCreateDoc.Checked == true)
            {
                isDocCreated = 1;
            }
            else
            {
                isDocCreated = 0;
            }
            if ((lblRequestHeadId_POP.Text == "1") && (ChkCreateDoc.Checked == true))
            {
                DateTime FromDate=new DateTime();
                DateTime ToDate= new DateTime();
                if (txtFromDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ManagerResponse();", true);
                    string msg = "Select From Date.";                  
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtFromDate.Focus();
                }
                else if(txtFromDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ManagerResponse();", true);
                    string msg = "Select To Date.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtToDate.Focus();
                }
                else if((txtFromDate.Text != "") && (txtFromDate.Text != ""))
                {
                    FromDate = DateTime.ParseExact(txtFromDate.Text.ToString().Substring(0, 10), "yyyy-M-d", CultureInfo.InvariantCulture);
                    ToDate = DateTime.ParseExact(txtToDate.Text.ToString().Substring(0, 10), "yyyy-M-d", CultureInfo.InvariantCulture);
                    int result = DateTime.Compare(FromDate, ToDate);
                    if (result >0) 
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ManagerResponse();", true);
                        string msg = "To Date is < From Date Check dates";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    }
                    else
                    {
                        string SlapDest = "";
                        string Source = "";
                        Source = "~/Reports/Letter_Size.pdf";
                        string P1 = "";
                        string StudentName = txtStudentName.Text.ToString();
                        string RequestId = lblRequestId_POP.Text.ToString();
                        string Lead_Id = lblLeadId_POP.Text.ToString();
                        string ProjectTitle = txtProjectTitle.Text.ToString();


                        SlapDest = Lead_Id.ToString() + "_" + "_" + RequestId.ToString() + "_" + txtMailId.Text.ToString() + ".pdf";
                        DeleteExistingFile(SlapDest);
                        var reader = new PdfReader(Server.MapPath(Source));
                        var document = new Document(reader.GetPageSizeWithRotation(1));
                        var fileStream = new System.IO.FileStream(Server.MapPath("~/Certificate/ApprovalLetter/" + SlapDest), FileMode.Create, FileAccess.Write);
                        var writer = PdfWriter.GetInstance(document, fileStream);
                        document.Open();
                        document.NewPage();

                        var contentByte = writer.DirectContent;
                        PdfGState graphicsState = new PdfGState();

                        var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                        var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);
                        var baseFont2 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/ARIAL.TTF"), BaseFont.IDENTITY_H, true);
                        contentByte.SetFontAndSize(baseFont, 10.5f);


                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont, 11);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, System.DateTime.Today.ToString("dd-MM-yyyy"), 510, 670, 0);


                        contentByte.SetFontAndSize(baseFont2, 10.5f);
                        P1 = "The below mentioned student is selected as a LEADer in the LEAD program to work along with his team for the self-";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 610, 0);

                        contentByte.SetFontAndSize(baseFont2, 10.5f);
                        P1 = "initiated community development leadership project to bring the positive change in the society. We expect and request ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 590, 0);

                        P1 = "for your best co-operation in execution of the Leadership initiative. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 570, 0);

                        P1 = "Leader’s Name    :" + " " + StudentName.ToString();
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 540, 0);

                        P1 = "Project Title         :" + " " + txtProjectTitle.Text.ToString();
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 520, 0);

                        P1 = "College                :" + " " + txtCollegeName.Text.ToString();
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 500, 0);

                        P1 = "Project Duration  :" + " " + FromDate.ToString("dd-MM-yyyy") + " " + "To" + " " + ToDate.ToString("dd-MM-yyyy");
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 480, 0);

                        P1 = "LEAD is an initiative of the Deshpande Foundation, Hubballi a program offering college students an opportunity to make ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 450, 0);

                        P1 = "change in their world. The aim of the program is to provide a platform for the students to explore their LEADership skills ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 430, 0);

                        P1 = "with practical exposure and take initiation in solving the problem in the society. The Program is striving to build the";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 410, 0);

                        P1 = "nation of motivated young LEADers. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 390, 0);

                        P1 = "The Deshpande Foundation strives to create social ecosystems of entrepreneurship and innovation throughout India. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 360, 0);

                        P1 = "Founded in 2007 by Mrs. Jaishree Deshpande and Dr. Gururaj 'Desh' Deshpande, the premise behind the Deshpande ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 340, 0);

                        P1 = "Foundation is to spread the belief that local leadership, innovation, and entrepreneurship can be catalysts of ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 320, 0);


                        P1 = "development. The aim of the Deshpande Foundation is to improve the quality of life for all. This means making quality ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 300, 0);

                        P1 = "education, healthy livelihoods, and access to healthcare available to every citizen. In pursuit of that goal, we have ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 280, 0);

                        P1 = "partnered with more than one hundred organizations engaged in the aforementioned fields. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 260, 0);

                        P1 = "For further information about the program kindly visit: www.leadcampus.org ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 230, 0);

                        P1 = "Warm Regards,";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 210, 0);



                        graphicsState.BlendMode = PdfGState.BM_NORMAL;
                        contentByte.SetGState(graphicsState);
                        contentByte.EndText();
                        PdfImportedPage page = writer.GetImportedPage(reader, 1);
                        contentByte.AddTemplate(page, 0, 0);
                        graphicsState.BlendMode = PdfGState.BM_DARKEN;
                        contentByte.SetGState(graphicsState);
                        string imagepath = "";
                        if (lblRequestHeadId_POP.Text == "1") // Project Approval Letter
                        {
                            imagepath = (Server.MapPath("~/Reports/ProjectApprovalLetter.jpg"));
                        }
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagepath);
                        image.ScaleToFit(820f, 810f);
                        image.SetAbsolutePosition(-2, -4);
                        image.Alignment = Element.ALIGN_LEFT;
                        // image.ScalePercent(22f);
                        document.Add(image);
                        document.Close();
                        fileStream.Close();
                        writer.Close();
                        reader.Close();
                        BLobj.Manager_UpdateRespondStudentRequest(RequestId.ToString(), Regex.Replace(txtResponse.Text.ToString(), "'", "`").Trim(), cook.Manager_Id(), isDocCreated);
                        string Subject = "Project Approval Letter For " + txtProjectTitle.Text.ToString();
                        string Body = BLobj.PopulateBody(Lead_Id.ToString(),
                       " <b style='font-size:larger'>Dear  " + txtStudentName.Text.ToString() + ", " + "Project Approval Letter is sent Please Find below Attached .pdf</b>", " Details below : ", "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>Name :</b> " + StudentName.ToString() + "<br><br></li><li><b>Project Title:</b> " + txtProjectTitle.Text.ToString() + "<br><br></li> " + "" +
                      "<li><b>Request Id</b> " + lblRequestId_POP.Text.ToString() + "<br><br><li><b>Request Message :</b> " + txtRequestMessage.Text.ToString() + "<br><br></li><li><b>Response Message :</b> " + txtResponse.Text.ToString() + "<br><br></li>");
                        SendHtmlFormattedEmail(txtMailId.Text.ToString(), cook.Manager_MailId(), Subject.ToString(), Body.ToString(), Server.MapPath("~/Certificate/ApprovalLetter/") + SlapDest.ToString());

                        DataTable dt = BLobj.Manager_FillStudentRequestRepeater(GetWhereConditions());
                        rptStudentRequest.DataSource = dt;
                        rptStudentRequest.DataBind();
                    }
                }               
                

            }
            else
            {
                string StudentName = txtStudentName.Text.ToString();
                string RequestId = lblRequestId_POP.Text.ToString();
                string Lead_Id = lblLeadId_POP.Text.ToString();
                string ProjectTitle = txtProjectTitle.Text.ToString();

                BLobj.Manager_UpdateRespondStudentRequest(RequestId.ToString(), Regex.Replace(txtResponse.Text.ToString(), "'", "`").Trim(), cook.Manager_Id(), 0);
                string Subject = "your request - "+" "+"["+ RequestId.ToString() + "]"+" "+  "Has been Closed";
                string Body = BLobj.PopulateBody(Lead_Id.ToString(),
               " <b style='font-size:larger'>Dear  " + txtStudentName.Text.ToString() + ", " + "Thanks for reaching out! the request has been closed</b>", " Details below : ", "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>Name :</b> " + StudentName.ToString() + "<br><br></li> " + "" +
              "<li><b>Request Id</b> " + lblRequestId_POP.Text.ToString() + "</li><br><br><li><b>Request Message :</b> " + txtRequestMessage.Text.ToString() + "<br><br></li><li><b>Response Message :</b> " + txtResponse.Text.ToString() + "<br><br></li>");
                SendHtmlFormattedEmail(txtMailId.Text.ToString(), cook.Manager_MailId(), Subject.ToString(), Body.ToString(),"");
             
                Response.Redirect("Manager_StudentGeneralRequest.aspx");
            }
        }
        catch (Exception)
        {
        }
    }
   
    public void SendHtmlFormattedEmail(string recepientEmail,string ManagerMailId, string subject, string body,string FilePath)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("leadmis@dfmail.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            if (ManagerMailId.ToString() != "")
            {
                mailMessage.CC.Add(new MailAddress(ManagerMailId.ToString()));
            }
            mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
            mailMessage.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
            if (FilePath.ToString() != "")
            {
                Attachment attCertificate = new Attachment(FilePath);
                mailMessage.Attachments.Add(attCertificate);
            }
           

            string senderID = "leadmis@dfmail.org";
            const string senderPassword = "leadcampusadmin";

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                Timeout = 30000,
            };

            smtp.Send(mailMessage);

        }
    }
   
    public void DeleteExistingFile(string FileName)
    {
        if (File.Exists(Path.Combine(Server.MapPath("~/Certificate/ApprovalLetter/"), FileName)))
        {
            // If file found, delete it    
            File.Delete(Path.Combine(Server.MapPath("~/Certificate/ApprovalLetter/"), FileName));
          }       
    }
    protected void rptStudentRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            Label lblStatus = (e.Item.FindControl("lblStatus") as Label);
            Label lblRequestPriority = (e.Item.FindControl("lblRequestPriority") as Label);
            Label lblRequestId = (e.Item.FindControl("lblRequestId") as Label);
            Label lblRequestHead_Id = (e.Item.FindControl("lblRequestHead_Id") as Label);
            Label lblPDID = (e.Item.FindControl("lblPDID") as Label);
       

         
            lblRequestId_POP.Text ="#"+ lblRequestId.Text.ToString();
            lblLeadId_POP.Text = (e.Item.FindControl("lblLeadId") as Label).Text.ToString();
            txtRequestMessage.Text = (e.Item.FindControl("lblRequestMessage") as Label).Text.ToString();
            txtRequestDate.Text = (e.Item.FindControl("lblRequestDate") as Label).Text.ToString();
            txtStudentName.Text= (e.Item.FindControl("lblStudentName") as Label).Text.ToString();
            txtMobileNo.Text= (e.Item.FindControl("lblMobileNo") as Label).Text.ToString();
            lblRequestHeadId_POP.Text = lblRequestHead_Id.Text.ToString();
            txtMailId.Text = (e.Item.FindControl("lblEmailId") as Label).Text;
            txtCollegeName.Text = (e.Item.FindControl("lblCollegeName") as Label).Text;
            txtFromDate.Text = "";
            txtToDate.Text = "";
        if (lblRequestHead_Id.Text == "1")
            {
                Project.Visible = true;
                ChkCreateDoc.Checked = true;
                txtProjectTitle.Text= BLobj.Manager_GetProjectName(lblPDID.Text.ToString());
                lblProjectId.Text = lblPDID.Text.ToString();
            }
            else
            {
                lblProjectId.Text = "";
                ChkCreateDoc.Checked = false;
                Project.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ManagerResponse();", true);
            txtRequestMessage.Focus();
        }
        catch (Exception)
        {


        }
    }

    protected void rptStudentRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Label lblStatus = (e.Item.FindControl("lblStatus") as Label);
                LinkButton btnResponse = (e.Item.FindControl("btnResponse") as LinkButton);
                Label lblRespondMessage = (e.Item.FindControl("lblRespondMessage") as Label);
                Label lblRequestPriority = (e.Item.FindControl("lblRequestPriority") as Label);
                Label lblRequestId = (e.Item.FindControl("lblRequestId") as Label);
                LinkButton btnRequest = (e.Item.FindControl("btnRqid") as LinkButton);

                if (lblStatus.Text == "Open")
                {
                    btnResponse.Enabled = true;
                    btnRequest.Enabled = true;
                    lblRespondMessage.Text = "";
                    //btnDeleteRequest.Attributes["data-toggle"] = "tooltip";
                    //btnDeleteRequest.Attributes["data-placement"] = "top";
                    //btnDeleteRequest.Attributes["title"] = "Delete";
                    lblRespondMessage.Text = "Response is Pending";
                    btnResponse.Attributes.Add("class", "btn btn-danger");
                    btnRequest.Attributes.Add("class", "btn btn-danger");
                    if (lblRequestPriority.Text == "Low")
                    {
                       
                        lblRequestPriority.Attributes.Add("class", "label label-default");
                        lblRequestPriority.Text = lblRequestPriority.Text + " " + "| Open";
                    }
                    else if (lblRequestPriority.Text == "Medium")
                    {
                       
                        lblRequestPriority.Attributes.Add("class", "label label-primary");
                        lblRequestPriority.Text = lblRequestPriority.Text + " " + "| Open";
                    }
                    else if (lblRequestPriority.Text == "High")
                    {
                     
                        lblRequestPriority.Attributes.Add("class", "label label-danger");
                        lblRequestPriority.Text = lblRequestPriority.Text + " " + "| Open";
                    }

                }
                else if (lblStatus.Text == "Close")
                {
                    btnResponse.Enabled= false;
                    btnRequest.Enabled = false;
                    btnRequest.Attributes.Add("class", "btn btn-success");
                    btnResponse.Attributes.Add("class", "btn btn-success");
                    if (lblRequestPriority.Text == "Low")
                    {
                        lblRequestPriority.Attributes.Add("class", "label label-default");

                        lblRequestPriority.Text = lblRequestPriority.Text + " " + "| Closed";
                   
                    }
                    else if (lblRequestPriority.Text == "Medium")
                    {
                        lblRequestPriority.Attributes.Add("class", "label label-primary");
                        lblRequestPriority.Text = lblRequestPriority.Text + " " + "| Closed";
                     
                    }
                    else if (lblRequestPriority.Text == "High")
                    {
                        lblRequestPriority.Attributes.Add("class", "label label-danger");
                        lblRequestPriority.Text = lblRequestPriority.Text + " " + "| Closed";
                      
                    }
                  
                }
            }
        }
        catch (Exception)
        {


        }
    }
    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = BLobj.Manager_FillStudentRequestRepeater(GetWhereConditions());
            rptStudentRequest.DataSource = dt;
            rptStudentRequest.DataBind();
            GridView gd = new GridView();
            gd.DataSource = dt;
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {
                string name = "TicketSystem Report" + "_" + System.DateTime.Now.ToString();
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