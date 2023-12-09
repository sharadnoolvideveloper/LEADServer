using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LEAD_SSO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                if ((!string.IsNullOrEmpty(Request.QueryString["dfv"].ToString() as string)) || Request.QueryString["dfv"].ToString() != null)
                {
                    DataLL DLobj = new DataLL();
                    vmGeneral GL = new vmGeneral();
                    Clear_ManagerCookies();
                    Clear_AdminCookies();
                    string p_Email = GL.DecryptString(Request.QueryString["dfv"].ToString());
                    vmLEAD_Login vmLog = new vmLEAD_Login();
                    vmLog = DLobj.LEAD_LoginValidation(p_Email.ToString());
                    if (vmLog.Status == "Success")
                    {
                        if(vmLog.User_Type=="1")
                        {
                            Create_ManagerCookies(vmLog, DLobj);
                            Response.Redirect("Pages/Manager/DashBoard.aspx?vwType=Proposed");
                        }
                        else if (vmLog.User_Type == "2")
                        {
                            Create_AdminCookies(vmLog, DLobj);
                            Response.Redirect("Pages/Admin/AdminAnalyticalCharts.aspx");
                        }
                        if (vmLog.User_Type == "3")
                        {
                             Create_ManagerCookies(vmLog, DLobj);
                            Create_AdminCookies(vmLog, DLobj);
                            Response.Redirect("Pages/Manager/DashBoard.aspx?vwType=Proposed");
                        }
                    }
                    else
                    {
                        lblErroMessage.Text = "You are not valid user!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ErrorMsg();", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblErroMessage.Text = ex.Message.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ErrorMsg();", true);
        }
    }
    public void Clear_ManagerCookies()
    {
        if ((Request.Cookies["Manager_UserName"] != null))
        {
            Response.Cookies["Manager_UserName"].Expires = DateTime.Now.AddDays(-30);
        }
        if ((Request.Cookies["Manager_Id"] != null))
        {
            Response.Cookies["Manager_Id"].Expires = DateTime.Now.AddDays(-30);
        }
        if ((Request.Cookies["Manager_Name"] != null))
        {
            Response.Cookies["Manager_Name"].Expires = DateTime.Now.AddDays(-30);
        }
        if ((Request.Cookies["Manager_AcademicYear"] != null))
        {
            Response.Cookies["Manager_AcademicYear"].Expires = DateTime.Now.AddDays(-30);
        }
        if ((Request.Cookies["Manager_RecordCount"] != null))
        {
            Response.Cookies["Manager_RecordCount"].Expires = DateTime.Now.AddDays(-30);
        }
        if ((Request.Cookies["Manager_MailId"] != null))
        {
            Response.Cookies["Manager_MailId"].Expires = DateTime.Now.AddDays(-30);
        }
    }
    public void Create_ManagerCookies(vmLEAD_Login vmLog, DataLL DL)
    {
        try
        {
            LeadBL BLobj = new LeadBL();
            HttpCookie Manager_UserName = new HttpCookie("Manager_UserName");
            Manager_UserName.Value = vmLog.Manager_UserName.ToString();
            Manager_UserName.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_UserName);

            HttpCookie Manager_Id = new HttpCookie("Manager_Id");
            Manager_Id.Value = vmLog.Manager_Id.ToString();
            Manager_Id.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_Id);

            HttpCookie Manager_Name = new HttpCookie("Manager_Name");
            Manager_Name.Value = vmLog.Manager_Name.ToString();
            Manager_Name.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_Name);

            HttpCookie Manager_AcademicYear = new HttpCookie("Manager_AcademicYear");
            Manager_AcademicYear.Value = vmLog.Manager_AcademicYear.ToString();
            Manager_AcademicYear.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_AcademicYear);

            HttpCookie Users_Role = new HttpCookie("Users_Role");
            Users_Role.Value = "Manager";
            Users_Role.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Users_Role);

            HttpCookie Manager_RecordCount = new HttpCookie("Manager_RecordCount");
            Manager_RecordCount.Value = vmLog.Manager_RecordCount.ToString();
            Manager_RecordCount.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_RecordCount);

            HttpCookie Manager_MailId = new HttpCookie("Manager_MailId");
            Manager_MailId.Value = vmLog.Manager_MailId.ToString();
            Manager_MailId.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_MailId);

            HttpCookie User_Type = new HttpCookie("User_Type");
            User_Type.Value = vmLog.User_Type.ToString();
            User_Type.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(User_Type);

        }
        catch (Exception ex)
        {


        }
    }

    public void Clear_AdminCookies()
    {
        if ((Request.Cookies["Admin_Id"] != null))
        {
            Response.Cookies["Admin_UserName"].Expires = DateTime.Now.AddDays(-30);
        }
        if ((Request.Cookies["Admin_AcademicYear"] != null))
        {
            Response.Cookies["Admin_Users_Role"].Expires = DateTime.Now.AddDays(-30);
        }
   
    }
    public void Create_AdminCookies(vmLEAD_Login vmLog, DataLL DL)
    {
        try
        {
            LeadBL BLobj = new LeadBL();

            HttpCookie Admin_Id = new HttpCookie("Admin_Id");
            Admin_Id.Value = vmLog.Manager_Id.ToString();
            Admin_Id.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_Id);

            HttpCookie Admin_UserName = new HttpCookie("Admin_UserName");
            Admin_UserName.Value = vmLog.Manager_Name.ToString();
            Admin_UserName.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_UserName);

            HttpCookie Admin_AcademicYear = new HttpCookie("Admin_AcademicYear");
            Admin_AcademicYear.Value = vmLog.Manager_AcademicYear.ToString();
            Admin_AcademicYear.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_AcademicYear);

            HttpCookie Admin_Users_Role = new HttpCookie("Admin_Users_Role");
            Admin_Users_Role.Value = "Admin";
            Admin_Users_Role.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_Users_Role);

            HttpCookie User_Type = new HttpCookie("User_Type");
            User_Type.Value = vmLog.User_Type.ToString();
            User_Type.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(User_Type);

        }
        catch (Exception ex)
        {


        }
    }

}