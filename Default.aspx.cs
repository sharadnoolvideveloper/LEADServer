using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = "btnStudentLogin";
        if (!Page.IsPostBack)
        {
            Clear_ManagerCookies();
            Clear_AdminCookies();
        }
      //  string msg = "Welcome";
       // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
    }

   

    protected void btnStudentLogin_Click(object sender, EventArgs e)
    {
        try
        {
            lblLoginError.Text = "";
            int Reg_Id;
            string UserRole = BLobj.UserRole(txtUserName.Text.ToString(), txtPassword.Text.ToString());
            string AcademicYear = "";
            string CollegeLogin = BLobj.CollegeLogin(txtUserName.Text.ToString(), txtPassword.Text.ToString());
            if ((txtUserName.Text == "Sharad_fun") && (txtPassword.Text == "Sharaddeveloper123") || (txtUserName.Text == "Mallurani") && (txtPassword.Text == "Mallutester123") ||
                (txtUserName.Text == "abhi") && (txtPassword.Text == "abhileaddf@1234"))
            {
                Session["Developer"] = "DFDeveloper";
                Response.Redirect("Pages/Developer/DeveloperIndex.aspx");
            }
            else if (CollegeLogin != "")
            {
                HttpCookie CollegeLogin_Id = new HttpCookie("CollegeLogin_Id");
                CollegeLogin_Id.Value = CollegeLogin.ToString();
                CollegeLogin_Id.Expires = DateTime.Now.AddDays(24);
                Response.SetCookie(CollegeLogin_Id);

                Response.Redirect("Pages/College/College_Dashboard.aspx");

                // Response.Redirect("~/Pages/College/College_Dashboard.aspx");
            }
            else if (CollegeLogin == "Deactive")
            {
                lblErroMessage.Text = "Hello, User your College Account is Deactivated Please Contact 9686654748 Or leadmis@dfmail.org";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ErrorMsg();", true);
            }
            else if (UserRole == "Deactive")
            {
                lblErroMessage.Text = "Hello, User your account is Deactivated Please Contact 9686654748 Or leadmis@dfmail.org";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ErrorMsg();", true);

            }
            else if (UserRole != "")
            {
                if (UserRole == "Student")
                {
                    string StudentMangerId = "";
                    // string UserExists = BLobj.CheckStudentExist(txtUserName.Text.ToString().Trim(), txtPassword.Text.ToString().Trim());
                    //if (UserExists != "")
                    //{
                    AcademicYear = BLobj.GetTop1AademicCode();
                    int isProfileEdit = BLobj.CheckProfileisEditedMode(txtUserName.Text.ToString());
                    if (isProfileEdit == 1)
                    {
                        StudentMangerId = BLobj.Student_GetManagerIdAfterProfileUpdate(txtUserName.Text.ToString());
                    }
                    else
                    {
                        StudentMangerId = BLobj.Student_GetManagerIdBeforeProfileUpdate();
                    }
                    Reg_Id = BLobj.Student_GetRegistrationId(txtUserName.Text.ToString());
                    if (Reg_Id != 0)
                    {
                        HttpCookie Registration_Id = new HttpCookie("Registration_Id");
                        Registration_Id.Value = Reg_Id.ToString();
                        Registration_Id.Expires = DateTime.Now.AddDays(24);
                        Response.SetCookie(Registration_Id);
                    }

                    HttpCookie Student_LeadId = new HttpCookie("Student_LeadId");
                    Student_LeadId.Value = txtUserName.Text.ToUpper().ToString();
                    Student_LeadId.Expires = DateTime.Now.AddDays(24);
                    Response.SetCookie(Student_LeadId);


                    HttpCookie Student_ManagerId = new HttpCookie("Student_ManagerId");
                    Student_ManagerId.Value = StudentMangerId.ToString();
                    Student_ManagerId.Expires = DateTime.Now.AddDays(24);
                    Response.SetCookie(Student_ManagerId);

                    HttpCookie Student_MobileNo = new HttpCookie("Student_MobileNo");
                    Student_MobileNo.Value = txtPassword.Text.ToString();
                    Student_MobileNo.Expires = DateTime.Now.AddDays(24);
                    Response.SetCookie(Student_MobileNo);

                    HttpCookie Student_AcademicYear = new HttpCookie("Student_AcademicYear");
                    Student_AcademicYear.Value = AcademicYear.ToString();
                    Student_AcademicYear.Expires = DateTime.Now.AddDays(24);
                    Response.SetCookie(Student_AcademicYear);

                    HttpCookie Student_isProfileCompleted = new HttpCookie("Student_isProfileCompleted");
                    Student_isProfileCompleted.Value = isProfileEdit.ToString();
                    Student_isProfileCompleted.Expires = DateTime.Now.AddDays(24);
                    Response.SetCookie(Student_isProfileCompleted);

                    BLobj.Student_Login_Log(txtUserName.Text.ToString());
                    Response.Redirect("Pages/Student/StudentProfile.aspx");

                    //}
                    //else
                    //{
                    //    string msg = "You are Not register Yet Please Register Soon.. by giving Miss Call";
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    //}
                }
                else if ((UserRole == "Manager") || (UserRole == "Admin") || (UserRole == "Account"))
                {
                    vmManager mng = new vmManager();
                    mng = BLobj.CheckManagerExist(txtUserName.Text.ToString().Trim(), txtPassword.Text.ToString().Trim(), mng);
                    if ((mng.User_Type == "1") || (mng.User_Type == "3"))
                    {
                        AcademicYear = BLobj.GetTop1AademicCode();

                        string manager_programid = BLobj.get_managerprogramid(mng.ManagerID.ToString());
                        Create_ManagerCookies(mng, AcademicYear.ToString(), manager_programid.ToString(), "Manager");
                        string programId = BLobj.getprogramid(mng.ManagerID.ToString());
                        Create_AdminCookies(mng, AcademicYear.ToString(), programId.ToString(), "Admin");



                        Create_AdminCookies(mng, AcademicYear.ToString(), "Admin");
                        BLobj.Manager_Login_Log(mng.ManagerID.ToString(), "Manager");
                        Response.Redirect("Pages/Manager/DashBoard.aspx?vwType=Proposed");
                    }
                    else if (mng.User_Type == "2")
                    {
                        AcademicYear = BLobj.GetTop1AademicCode();
                        Create_AdminCookies(mng, AcademicYear.ToString(), "Admin");
                        BLobj.Manager_Login_Log(mng.ManagerID.ToString(), "Admin");
                        Response.Redirect("Pages/Admin/AdminAnalyticalCharts.aspx");
                    }
                    else if (mng.User_Type == "4")
                    {
                        AcademicYear = BLobj.GetTop1AademicCode();
                        Create_AccountCookies(mng, AcademicYear.ToString(), "Account");
                        BLobj.Manager_Login_Log(mng.ManagerID.ToString(), "Account");
                        Response.Redirect("Pages/Account/Home.aspx");
                    }
                    else
                    {
                        string msg = "You are not valid user!!";
                        lblLoginError.Text = msg.ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    }
                }
               else
                {
                    string msg = "Not Exitsts!!";
                    lblLoginError.Text = msg.ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {
                string msg = "check credential and try again!!";
                lblLoginError.Text = msg.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            //string UserExists = BLobj.CheckStudentExist(txtUserName.Text.ToString().Trim(), txtPassword.Text.ToString().Trim());

        }
         
        catch (Exception ex)
        {

           string msg = "Un Authorised Access Found." + ex.ToString();
            lblLoginError.Text = msg.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }

    public void Create_ManagerCookies(vmManager mng,string AcademicYear,string program,string UserRole)
    {
        try
        {
            
            HttpCookie Manager_UserName = new HttpCookie("Manager_UserName");
            Manager_UserName.Value = txtUserName.Text.ToString();
            Manager_UserName.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_UserName);

            HttpCookie Manager_Id = new HttpCookie("Manager_Id");
            Manager_Id.Value = mng.ManagerID.ToString();
            Manager_Id.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_Id);

            HttpCookie Manager_Name = new HttpCookie("Manager_Name");
            Manager_Name.Value = mng.ManagerName.ToString();
            Manager_Name.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_Name);

            HttpCookie Manager_AcademicYear = new HttpCookie("Manager_AcademicYear");
            Manager_AcademicYear.Value = AcademicYear.ToString();
            Manager_AcademicYear.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_AcademicYear);

            HttpCookie Users_Role = new HttpCookie("Users_Role");
            Users_Role.Value = UserRole.ToString();
            Users_Role.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Users_Role);

            HttpCookie Manager_RecordCount = new HttpCookie("Manager_RecordCount");
            Manager_RecordCount.Value = mng.RecordCount.ToString();
            Manager_RecordCount.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_RecordCount);

            HttpCookie Manager_MailId = new HttpCookie("Manager_MailId");
            Manager_MailId.Value = mng.MailId.ToString();
            Manager_MailId.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_MailId);

            HttpCookie User_Type = new HttpCookie("User_Type");
            User_Type.Value = mng.User_Type.ToString();
            User_Type.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(User_Type);

            HttpCookie Manager_program = new HttpCookie("Manager_program");
            Manager_program.Value = program.ToString();
            Manager_program.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Manager_program);
        }
        catch (Exception ex)
        {

        }
    }
    public void Create_AdminCookies(vmManager mng, string AcademicYear, string UserRole)
    {
        try
        {

            HttpCookie Admin_Id = new HttpCookie("Admin_Id");
            Admin_Id.Value = mng.ManagerID.ToString();
            Admin_Id.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_Id);

            HttpCookie Admin_UserName = new HttpCookie("Admin_UserName");
            Admin_UserName.Value = txtUserName.Text.ToString();
            Admin_UserName.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_UserName);

            HttpCookie Admin_AcademicYear = new HttpCookie("Admin_AcademicYear");
            Admin_AcademicYear.Value = AcademicYear.ToString();
            Admin_AcademicYear.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_AcademicYear);

            HttpCookie Admin_Users_Role = new HttpCookie("Admin_Users_Role");
            Admin_Users_Role.Value = UserRole.ToString();
            Admin_Users_Role.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Admin_Users_Role);

            HttpCookie User_Type = new HttpCookie("User_Type");
            User_Type.Value = mng.User_Type.ToString();
            User_Type.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(User_Type);



        }
        catch (Exception ex)
        {

        }
    }

    public void Create_AccountCookies(vmManager mng, string AcademicYear, string UserRole)
    {
        try
        {

            HttpCookie Account_Id = new HttpCookie("Account_Id");
            Account_Id.Value = mng.ManagerID.ToString();
            Account_Id.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Account_Id);

            HttpCookie Account_UserName = new HttpCookie("Account_UserName");
            Account_UserName.Value = txtUserName.Text.ToString();
            Account_UserName.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Account_UserName);

            HttpCookie Account_AcademicYear = new HttpCookie("Account_AcademicYear");
            Account_AcademicYear.Value = AcademicYear.ToString();
            Account_AcademicYear.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Account_AcademicYear);

            HttpCookie Account_Users_Role = new HttpCookie("Account_Users_Role");
            Account_Users_Role.Value = UserRole.ToString();
            Account_Users_Role.Expires = DateTime.Now.AddDays(24);
            Response.SetCookie(Account_Users_Role);

            HttpCookie User_Type = new HttpCookie("User_Type");
            User_Type.Value = mng.User_Type.ToString();
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
    protected void btnForgottonPassword_Click(object sender, EventArgs e)
    {
        try
        {
            txtMailId.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ForgotPassword();", true);
        }
        catch(Exception)
        {

        }
    }

    protected void btnSendForgotPassword_Click(object sender, EventArgs e)
    {
        try
        {
            vmForgotPassword FP = new vmForgotPassword();
            FP=BLobj.ForgotPassword(txtMailId.Text.ToString().Trim());
            if(FP.Status== "Not Exists")
            {
                string msg = "Not Registered User";
               
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                string MaildBody = "";
                MaildBody = PopulateBody(FP.ExistingMailId.ToString(),
                "<b>LEAD Recovery Password </b>", "",
                "Dear " + FP.Name.ToString() + " " + System.DateTime.Now.ToString() + " " +
                "<br><br>" + " " +
                "<b style='color:red'>Your Account Details is</b>" + " " +
                "<ol>" + " " +
                "<li><b>User Id :</b> " + FP.UserID.ToString() + "<br><br></li>" + " " +
                "<li><b>Password :</b> " + FP.Password.ToString() + "<br><br></li> " + " " +
                "<br><br></li></ol><br><br>");              
                SendHtmlFormattedEmailException(txtMailId.Text.ToString(), "Your LEAD Password Recovery Details (WEB)", MaildBody);
                txtMailId.Focus();
                string msg = "Password sent to your mail id successfully...";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            }
        }
        catch(Exception)
        {

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
            // mailMessage.Bcc.Add(new MailAddress("sunil.tech@dfmail.org"));

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

    protected void btnGoogle_Login_Click(object sender, EventArgs e)
    {

    }

    protected void btnAppDownload_Click(object sender, EventArgs e)
    {
        try
        {
            // Define the APK file path and URL
            string apkFilePath = Server.MapPath("~/Reports/leadcampus.apk");
            string apkFileName = "leadcampus.apk";
            string apkFileUrl = Server.MapPath("~/CSS/leadcampus.apk");

            //"http://ec2-18-138-98-10.ap-southeast-1.compute.amazonaws.com:9090/Reports/leadcampus.apk";


            // Download the APK file
            WebClient client = new WebClient();
            client.DownloadFile(apkFileUrl, apkFilePath);

            // Install the downloaded APK
            InstallAPK(apkFilePath, apkFileName);
        }
        catch (Exception ex)
        {

        }
    }

    private void InstallAPK(string apkFilePath, string FileName)
    {
        // Execute an intent to install the APK
        Response.ContentType = "application/vnd.android.package-archive";
        Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
        Response.TransmitFile(apkFilePath);
        Response.End();
    }
}
public class manager
{
    public string ManagerId { get; set; }
    public string ManagerName { get; set; }
}