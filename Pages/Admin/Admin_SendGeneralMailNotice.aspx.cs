using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_SendGeneralMailNotice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSendMaild_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtMailId.Text == "")
            {
                string msg = "Please Fill MailId";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg.ToString() + "')", true);
            }
            else if (txtSubject.Text == "")
            {
                string msg = "Please Fill Subject";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg.ToString() + "')", true);
            }
            else if (txtMessage.Text == "")
            {
                string msg = "Please Fill Message";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg.ToString() + "')", true);
            }
            else
            {
                string MaildIds = txtMailId.Text.ToString();
                string MailID = "";
               

                string[] MobileList = Regex.Split(MaildIds, "\r\n");
                int count = MobileList.Count();

                for (int i = 0; i < count; i++)
                {
                    MailID = MobileList[i].ToString();

                    string MaildBody = "";
                    MaildBody = PopulateBody(MailID.ToString(),
                    "", "",
                    " " + txtMessage.Text.ToString() + " " +
                    "<br><br>");
                    SendHtmlFormattedEmailException(MailID.ToString(), txtSubject.Text.ToString(), MaildBody);
                    txtMailId.Focus();
                   
                }
                string msg = "Mail Sent Successfully";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg.ToString() + "')", true);
                txtSubject.Text = "";
                txtMessage.Text = "";
                txtMailId.Text = "";
            }

                
        }
        catch(Exception ex)
        {
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + ex.Message.ToString() + "')", true);
        }
    }
    private string PopulateBody(string userName, string title, string url, string description)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("/Pages/EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);
        body = body.Replace("{Title}", title);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Description}", description);
        return body;
    }

    private void SendHtmlFormattedEmailException(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("leadmis@dfmail.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            //  mailMessage.CC.Add(new MailAddress(lblManagerEmailId.Text.ToString()));
            mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
            mailMessage.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
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
}