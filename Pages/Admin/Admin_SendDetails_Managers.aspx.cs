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

public partial class Pages_Admin_Admin_SendDetails_Managers : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            BLobj.Admin_FillManagerForDetailsSending(rptManagers,ddlSandbox.SelectedItem.Text.ToString());
            lblTotalManagersCount.Text = rptManagers.Items.Count.ToString();
        }
    }
    protected void btnMailMain_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerForDetailsSending(rptManagers, ddlSandbox.SelectedItem.Text.ToString());
            lblTotalManagersCount.Text = rptManagers.Items.Count.ToString();
            // lblTotalStudentsCount.Text = rptStudentDetail.Items.Count.ToString();
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
            BLobj.Admin_FillManagerForDetailsSending(rptManagers, ddlSandbox.SelectedItem.Text.ToString());
            lblTotalManagersCount.Text = rptManagers.Items.Count.ToString();
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
            StringBuilder sbManagerMailId = new StringBuilder();
            foreach (RepeaterItem ri in rptManagers.Items)
            {
                CheckBox ChkManagerSelect = (CheckBox)ri.FindControl("ChkManager");
                if (ChkManagerSelect.Checked == true)
                {
                    Label lblMailId = (Label)ri.FindControl("lblMailId");

                    Label lblManagerCode = (Label)ri.FindControl("lblManagerCode");

                    sbManagerMailId.Append(lblMailId.Text.ToString().Trim() + ",");
                    sbManagerMailId.AppendLine().Replace("\r\n", "");

                    BLobj.Manager_SaveNotificationLog(cook.Admin_Id(), lblManagerCode.Text.ToString(), txtSMSMessage.Text.ToString(), "Mail", "");

                }

            }
            string ManagerMailId = sbManagerMailId.ToString().TrimEnd(BLobj.trimChar);

            if (ManagerMailId.ToString().Contains(','))
            {
                string[] tos = ManagerMailId.ToString().Split(',');
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
                msg.To.Add(new MailAddress(ManagerMailId.ToString().Trim()));
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

                txtToSMS.Text = "";
                txtSubject.Text = "";
                txtSMSMessage.Text = "";
                txtMailMessage.Text = "";
                string msg1 = "Mail Sent Successfully..!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg1 + "')", true);
            }
            catch (Exception ex)
            {
                lblMres.Text = "Error sending email.!!!";
                BLobj.SendMailException("btnSendMail_Click", ex.ToString(), "ManagerMailSending.aspx", cook.Manager_Id(), "");
            }
        
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
            BLobj.SendMailException("btnSendSMS_Click", ex.ToString(), "Admin_SendDetails_Managers.aspx", cook.Admin_Id(), "");
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
                foreach (RepeaterItem ri in rptManagers.Items)
                {

                    CheckBox ChkManagerSelect = (CheckBox)ri.FindControl("ChkManager");
                   
                    if (ChkManagerSelect.Checked == true)
                    {
                        int i = 0;
                        Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");
                        Label lblManagerCode = (Label)ri.FindControl("lblManagerCode");
                        string DeviceId = BLobj.Common_GetDeviceID(lblMobileNo.Text.ToString());


                        string ServerResponse = GCMNotification.AndroidPush(DeviceId.ToString(), txtSMSMessage.Text.ToString(), "Student", "Empty");
                        BLobj.Manager_SaveNotificationLog(cook.Admin_Id(), lblManagerCode.Text.ToString(), txtSMSMessage.Text.ToString(), "Notification", ServerResponse.ToString());
                        if (i == 0)
                        {

                            string Subject = "Notification" + "-" + "Lead Sending Notification to Manager";

                            GCMNotification.SendMailGeneral(txtSMSMessage.Text.ToString(), Subject.ToString(), "leadmis@dfmail.org");
                        }
                        i++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BLobj.SendMailException("BtnSendNotification_Click", ex.ToString(), "Admin_SendDetails_Managers.aspx notification", cook.Admin_Id(), "");
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
            foreach (RepeaterItem ri in rptManagers.Items)
            {
                int i = 0;

                CheckBox ChkManager = (CheckBox)ri.FindControl("ChkManager");
                if (ChkManager.Checked == true)
                {
                    Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");

                    Label lblManagerCode = (Label)ri.FindControl("lblManagerCode");

                    string ServerResponse = BLobj.Send_Multiple_SMS(lblMobileNo.Text.ToString(), txtSMSMessage.Text.ToString().Trim(), lblManagerCode.Text.ToString(), "Manager");
                    BLobj.Manager_SaveNotificationLog(cook.Admin_Id(), lblManagerCode.Text.ToString(), txtSMSMessage.Text.ToString(), "SMS", ServerResponse.ToString());
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

    protected void ddlSandbox_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerForDetailsSending(rptManagers, ddlSandbox.SelectedItem.Text.ToString());
            lblTotalManagersCount.Text = rptManagers.Items.Count.ToString();
        }
        catch(Exception)
        {

        }
    }
}