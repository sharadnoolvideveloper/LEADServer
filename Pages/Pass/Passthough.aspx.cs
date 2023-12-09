using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Script.Services;
public partial class Pages_Pass_Passthough : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string callfrom1 = Request.QueryString["From"];
        string callid = Request.QueryString["CallSid"];
        string calltime = Request.QueryString["StartTime"];
        PassBL BLobj = new PassBL();
        string callfrom = "";
        if (callfrom1 != null)
        {

            callfrom = callfrom1.Substring(1, 10);  // to remove 0
           // callfrom = "+91" + callfrom;

        }
        string MobileNo = callfrom;
        SendEmail Email = new SendEmail();
        if (callfrom != null)
        {
            try
            {
                BLobj.SaveMissCallLog(callid.ToString(), callfrom.ToString(), calltime.ToString());
                BLobj.GenerateLeadId(MobileNo.ToString());
            }
            catch (Exception ex)
            {
                Email.SendMailException("PassThrough", ex.ToString(), "Passthrough.aspx", MobileNo, MobileNo);
            }
        }
        else
            Response.Write("Invalid Call");

    }

    
}