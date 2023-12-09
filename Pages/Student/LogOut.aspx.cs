using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LeadBL BLobj = new LeadBL();
        vmCookies cook = new vmCookies();
        //string User=Request.QueryString[""].ToString();
        if (cook.Manager_Id() != "")
        {
            BLobj.Manager_Update_Login_Log(cook.Manager_Id(), "Manager");
        }
        if (cook.Admin_Id() != "")
        {
            BLobj.Manager_Update_Login_Log(cook.Admin_Id(), "Admin");
        }
        if (cook.LeadId() != "")
        {
            BLobj.Student_Update_Login_Log(cook.LeadId());
        }
    }
}