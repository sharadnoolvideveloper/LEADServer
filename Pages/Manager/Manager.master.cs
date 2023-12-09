using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_Manager : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            vmCookies cook = new vmCookies();

            if (cook.Manager_Id() != "")
            {
                LeadBL BLobj = new LeadBL();
                string ManagerID = cook.Manager_Id();
              //  BLobj.Student_SetManagerProfileImage(ManagerID, imgTopManager);
                vmManager vmManager = new vmManager();
                BLobj.Manager_GetManagerDetails(vmManager, ManagerID.ToString());
                lblManagerTopName.Text = vmManager.ManagerName.ToString();
                imgTopManager.ImageUrl = vmManager.Image_path.ToString();
                if (cook.User_Type() == "3")
                {
                    isAdmin.Visible = true;
                }
                else
                {
                    isAdmin.Visible = false;
                }



            }
        }
            //lblDateToday.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
        
    }
    //protected void TimerTime_Tick(object sender, EventArgs e)
    //{

    //    lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
    //}
    protected void btnTopLogout_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("Manager_UserName", ""));
            Response.Cookies.Add(new HttpCookie("Manager_Id", ""));
            Response.Cookies.Add(new HttpCookie("Manager_AcademicYear", ""));
            Response.Cookies.Add(new HttpCookie("Manager_Users_Role", ""));
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception)
        {

        }
    }
}
