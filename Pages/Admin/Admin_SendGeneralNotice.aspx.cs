using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_SendGeneralNotice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {          
                lblSMSCount.Text = GetSMSBalance();            
        }
    }
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
       
        if(txtMessage.Text=="")
        {
            string msg = "Please Fill Message";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg.ToString() + "')", true);
        }
        else if(txtMobileNo.Text=="")
        {
            string msg = "Please Fill MobileNo";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg.ToString() + "')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirm();", true);
        }
    }
    public string Send_Multiple_SMS(string MobileNo, string Message)
    {
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.campusconnect.co/api/web2sms.php?workingkey=A6a2f9a0287c81d9dfa2c15cdfb489987&to=" + MobileNo + "&sender=LCLEAD&message=" + Message);

            //NEW API HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.campusconnect.co/api/web2sms.php?workingkey=A6a2f9a0287c81d9dfa2c15cdfb489987&to=" + MobileNo + "&sender=LCLEAD&message=" + Message);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            return responseString;
        }
        catch (Exception)
        {
           
            return "";
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
            string mobilenos = txtMobileNo.Text.ToString();
            string SMSMobileNo = "";
            string Response = "";
            if ((txtMobileNo.Text != "") && (txtMessage.Text != ""))
            {
                string[] MobileList = Regex.Split(mobilenos, "\r\n");
                int count = MobileList.Count();

                for (int i = 0; i < count; i++)
                {
                    SMSMobileNo = MobileList[i].ToString();
                    Response = Send_Multiple_SMS(SMSMobileNo.ToString(), txtMessage.Text.ToString());
                }

                string msg = "Send SMS Successfully";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg.ToString() + "')", true);

                lblSMSCount.Text = GetSMSBalance();
                txtMessage.Text = "";
                txtMobileNo.Text = "";
            }
            else
            {
                string msg = "Please Fill MobileNo and Message";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg.ToString() + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }
    public string GetSMSBalance()
    {

        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.campusconnect.co/api/credits.php?workingkey=A6a2f9a0287c81d9dfa2c15cdfb489987");
        //NEW API HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alert.campusconnect.co/api/credits.php?workingkey=A6a2f9a0287c81d9dfa2c15cdfb489987");
        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        string responseString = respStreamReader.ReadToEnd();       
        respStreamReader.Close();
        myResp.Close();
        return responseString;
    }
}