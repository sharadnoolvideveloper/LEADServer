using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_SendDetails : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                BLobj.FillAademicYear(ddlAcademicYear);
                ddlAcademicYear.SelectedIndex = 1;
                /* BLobj.Admin_FillManagerddl(ddlManagerName, cook.Admin_Id());*/
                BLobj.Admin_Fillprogramddl(ddlprogram, cook.Admin_Id());
                BLobj.Admin_FillManagerByprogram(ddlprogram.SelectedValue.ToString(), ddlManagerName);
                //rptColleges.DataSource = BLobj.Manager_FillCollegeForSMS(cook.Manager_Id());
                //rptColleges.DataBind();
                //rptStudentDetail.DataSource = BLobj.Manager_FillStudentsCollegeWiseForSMSMail(ddlManagerName.SelectedValue.ToString(), "", ddlStudentType.SelectedValue.ToString(), ddlProjectStatus.SelectedValue.ToString(), ddlAcademicYear.SelectedValue.ToString());
                //rptStudentDetail.DataBind();
                //lblTotalStudentsCount.Text = rptStudentDetail.Items.Count.ToString();

            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerByprogram(ddlprogram.SelectedValue.ToString(), ddlManagerName);
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnMailMain_Click(object sender, EventArgs e)
    {
        try
        {

            StringBuilder str = new StringBuilder();
            foreach (RepeaterItem ri in rptColleges.Items)
            {
                CheckBox Chk = (CheckBox)ri.FindControl("ChkCollege");
                if (Chk.Checked == true)
                {
                    Label CollegeCode = (Label)ri.FindControl("lblCollegeCode");
                    Label TalukaCode = (Label)ri.FindControl("lblTalukaId");
                    str.Append("" + CollegeCode.Text.ToString().Trim() + "" + ",");
                    str.AppendLine().Replace("\r\n", "");
                }
            }
            string strCollegeCode = str.ToString().TrimEnd(BLobj.trimChar);
            rptStudentDetail.DataSource = BLobj.Manager_FillStudentsCollegeWiseForSMSMail(ddlManagerName.SelectedValue.ToString(), strCollegeCode.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectStatus.SelectedValue.ToString(), ddlAcademicYear.SelectedValue.ToString());
            rptStudentDetail.DataBind();
            lblTotalStudentsCount.Text = rptStudentDetail.Items.Count.ToString();
            MultiView1.SetActiveView(vwMail);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSMSMain_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder str = new StringBuilder();
            foreach (RepeaterItem ri in rptColleges.Items)
            {
                CheckBox Chk = (CheckBox)ri.FindControl("ChkCollege");
                if (Chk.Checked == true)
                {
                    Label CollegeCode = (Label)ri.FindControl("lblCollegeCode");
                    Label TalukaCode = (Label)ri.FindControl("lblTalukaId");
                    str.Append("" + CollegeCode.Text.ToString().Trim() + "" + ",");
                    str.AppendLine().Replace("\r\n", "");
                }
            }
            string strCollegeCode = str.ToString().TrimEnd(BLobj.trimChar);
            rptStudentDetail.DataSource = BLobj.Manager_FillStudentsCollegeWiseForSMSMail(ddlManagerName.SelectedValue.ToString(), strCollegeCode.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectStatus.SelectedValue.ToString(), ddlAcademicYear.SelectedValue.ToString());
            rptStudentDetail.DataBind();
            lblTotalStudentsCount.Text = rptStudentDetail.Items.Count.ToString();


            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.campusconnect.co/api/credits.php?workingkey=A6a2f9a0287c81d9dfa2c15cdfb489987");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            lblSMSCount.Text = responseString;
            respStreamReader.Close();
            myResp.Close();
            MultiView1.SetActiveView(vwSMS);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        if ((txtSubject.Text == "") || (txtMailMessage.Text == ""))
        {
            lblMres.Text = "Mail Not Sent. To/Subject/Message field is empty";
        }
        else
        {
            var msg = new MailMessage
            {
                Subject = "Email -" + txtSubject.Text.Trim(),
                Body = txtMailMessage.Text.Trim(),
                IsBodyHtml = true,
                From = new MailAddress(ConfigurationManager.AppSettings["DefaultFromMailId"].ToString())
            };
            StringBuilder sbStudentMailId = new StringBuilder();
            foreach (RepeaterItem ri in rptStudentDetail.Items)
            {


                CheckBox ChkStudentSelect = (CheckBox)ri.FindControl("ChkStudentSelect");
                if (ChkStudentSelect.Checked == true)
                {
                    Label lblMailId = (Label)ri.FindControl("lblMail");

                    Label lblLeadId = (Label)ri.FindControl("lblLeadId");

                    sbStudentMailId.Append(lblMailId.Text.ToString().Trim() + ",");
                    sbStudentMailId.AppendLine().Replace("\r\n", "");

                    BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), lblLeadId.Text.ToString(), txtSMSMessage.Text.ToString(), "Mail", "");

                }

            }
            string StudentMailId = sbStudentMailId.ToString().TrimEnd(BLobj.trimChar);

            if (StudentMailId.ToString().Contains(','))
            {
                string[] tos = StudentMailId.ToString().Split(',');
                for (int i = 0; i < tos.Length; i++)
                {
                    bool exists = BLobj.IsValidEmail(tos[i].ToString());
                    if (exists == true)
                    {
                        msg.Bcc.Add(new MailAddress(tos[i]));
                        if (i == 0)
                        {
                            msg.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
                            msg.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
                        }
                    }


                }
                //msg.CC.Add(new MailAddress(txt_default_mail.Text));
                //msg.To.Add(new MailAddress((Session["userInfo"] as User_Info).Email_Id));
            }
            else
            {
                msg.Bcc.Add(new MailAddress(StudentMailId.ToString().Trim()));
                msg.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
                msg.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
                //msg.To.Add(new MailAddress((Session["userInfo"] as User_Info).Email_Id));
                //msg.CC.Add(new MailAddress(txt_default_mail.Text));
            }

            MailMessage mail = new MailMessage();
            string senderID = "leadmis@dfmail.org";// use sender’s email id here..
            const string senderPassword = "leadcampusadmin"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };

                smtp.Send(msg);

                lblMres.Text = "Mail Sent Successfully..!!";
            }
            catch (Exception ex)
            {
                lblMres.Text = "Error sending email.!!!";
                BLobj.SendMailException("btnSendMail_Click", ex.ToString(), "ManagerMailSending.aspx", cook.Manager_Id(), "");
            }
            txtToSMS.Text = "";
            txtSubject.Text = "";
            txtSMSMessage.Text = "";
            txtMailMessage.Text = "";
        }
    }


    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSMSMessage.Text == "")
            {
                string msg = "Please Enter Message Body!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirm();", true);
            }
        }
        catch (Exception ex)
        {
            BLobj.SendMailException("btnSendSMS_Click", ex.ToString(), "Admin_SendDetails.aspx", cook.Admin_Id(), "");
        }
    }

    protected void BtnSendNotification_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtSMSMessage.Text == "")
            {
                lblMres.Text = "Message Not Sent. To/Message field is empty";
            }
            else
            {
                foreach (RepeaterItem ri in rptStudentDetail.Items)
                {


                    CheckBox ChkStudentSelect = (CheckBox)ri.FindControl("ChkStudentSelect");
                    if (ChkStudentSelect.Checked == true)
                    {
                        int i = 0;
                        Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");
                        Label lblLeadId = (Label)ri.FindControl("lblLeadId");
                        string DeviceId = BLobj.Common_GetDeviceID(lblLeadId.Text.ToString());


                        string ServerResponse = GCMNotification.AndroidPush(DeviceId.ToString(), txtSMSMessage.Text.ToString(), "Student", "Empty");
                        BLobj.Manager_SaveNotificationLog(cook.Admin_Id(), lblLeadId.Text.ToString(), txtSMSMessage.Text.ToString(), "Notification", ServerResponse.ToString());
                        if (i == 0)
                        {

                            string Subject = "Notification" + "-" + "Lead Sending Notification to Student";

                            GCMNotification.SendMailGeneral(txtSMSMessage.Text.ToString(), Subject.ToString(), "leadmis@dfmail.org");
                        }
                        i++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BLobj.SendMailException("BtnSendNotification_Click", ex.ToString(), "Admin_SendDetails.aspx notification", cook.Manager_Id(), "");
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
            foreach (RepeaterItem ri in rptStudentDetail.Items)
            {
                int i = 0;

                CheckBox ChkStudentSelect = (CheckBox)ri.FindControl("ChkStudentSelect");
                if (ChkStudentSelect.Checked == true)
                {
                    Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");

                    Label lblLeadId = (Label)ri.FindControl("lblLeadId");

                    string ServerResponse = BLobj.Send_Multiple_SMS(lblMobileNo.Text.ToString(), txtSMSMessage.Text.ToString().Trim(), lblLeadId.Text.ToString(), ddlStudentType.SelectedItem.Text.ToString());
                    BLobj.Manager_SaveNotificationLog(cook.Admin_Id(), lblLeadId.Text.ToString(), txtSMSMessage.Text.ToString(), "SMS", ServerResponse.ToString());
                    if (i == 0)
                    {
                        GCMNotification.SendMailGeneral(txtSMSMessage.Text.ToString(), "SMS", "leadmis@dfmail.org");
                    }
                    i++;
                }
            }
            txtSMSMessage.Text = "";
            string msg = "SMS sent Successfully!!!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch (Exception)
        {

        }
    }
}