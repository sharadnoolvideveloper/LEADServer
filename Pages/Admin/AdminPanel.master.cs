using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_AdminPanel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        vmCookies cook = new vmCookies();
        if (cook.Admin_Id() == "")
        {
            Response.Redirect("~/Default.aspx?SessionTimeOut=true");
        }
        if (!Page.IsPostBack)
        {
            if((cook.Admin_Id()=="23") || (cook.Admin_Id() == "1"))
            {
                WorkDiary1.Visible = true;
            }
            else
            {
                WorkDiary1.Visible = false;
            }
        }
       
       

    }

    protected void btnLogoutTopMenu_Click(object sender, EventArgs e)
    {
        try
        {
          
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("Admin_Id", ""));
            Response.Cookies.Add(new HttpCookie("Lead_Role", ""));
         
            Response.Redirect("~/Default.aspx");
        }
        catch(Exception)
        {

        }
    }
}
