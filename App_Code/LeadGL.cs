using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for LeadGL
/// </summary>
/// 

public class vmLoginValidation
{
    public vmLoginValidation()
    {

    }
    public int LoginId { get; set; }
    public string UserId { get; set; }
    public string Role { get; set; }
    public int Status { get; set; }
    public string msg { get; set; }
}
public class vmDigitalDocument
{
   public string LeadId { get; set; }
   public int PDId { get; set; }
   public int Document_Id { get; set; }
   public string  Document_Path { get; set; }
   public string Document_Type { get; set; }
   public string Created_Date { get; set; }
   public int Created_By { get; set; }
}
public class vmResponseCompletionSave
{
    public string LeadId { get; set; }
    public string PDId { get; set; }
    public string ActualBeneficier { get; set; }
    public string placeofimplement { get; set; }
    public string Theme { get; set; }
    public string challenge { get; set; }
    public string Learning { get; set; }
    public string AsAStory { get; set; }
    public string Resource { get; set; }
    public string TotalResourses { get; set; }
    public string rating { get; set; }
    public string InnovativeRating { get; set; }
    public string LeadershipRating { get; set; }
    public string RiskTaenRating { get; set; }
    public string ImpactRating { get; set; }
    public string FundRaisedRating { get; set; }
    public string ManagerComments { get; set; }

    public string ManagerId { get; set; }

    public string Title { get; set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }
    

    public string HoursSpend
    {
        get; set;
    }
    public string StudentLevel
    {
        get; set;
    }

    public string SDG_Goals
    {
        get; set;
    }
    public string Collaboration_Supported
    {
        get; set;
    }
    public string Permission_And_Activities
    {
        get; set;
    }
    public string Experience_Of_Initiative
    {
        get; set;
    }
    public string Lacking_initiative
    {
        get; set;
    }
    public string Against_Tide
    {
        get; set;
    }
    public string Cross_Hurdles
    {
        get; set;
    }
    public string Entrepreneurial_Venture
    {
        get; set;
    }
    public string Government_Awarded
    {
        get; set;
    }
    public string Leadership_Roles
    {
        get; set;
    }
    public string ResourceUtilizedAmount
    {
        get; set;
    }
}
public class WEB_StudentProjectCompletion
{
    public string Pdid
    {
        get; set;
    }
    public string PlaceofImplementation
    {
        get; set;
    }
    public double FundRaised
    {
        get; set;
    }
    public string Challenges
    {
        get; set;
    }
    public string Learning
    {
        get; set;
    }
    public string AsAStory
    {
        get; set;
    }
    public string Lead_Id
    {
        get; set;
    }
    public string Resources
    {
        get; set;
    }
    public int HoursSpent
    {
        get; set;
    }
    public string SDG_Goals
    {
        get; set;
    }
    public string Collaboration_Supported
    {
        get; set;
    }
    public string Permission_And_Activities
    {
        get; set;
    }
    public string Experience_Of_Initiative
    {
        get; set;
    }
    public string Lacking_initiative
    {
        get; set;
    }
    public string Against_Tide
    {
        get; set;
    }
    public string Cross_Hurdles
    {
        get; set;
    }
    public string Entrepreneurial_Venture
    {
        get; set;
    }
    public string Government_Awarded
    {
        get; set;
    }
    public string Leadership_Roles
    {
        get; set;
    }
    public string Projectstatus
    {
        get; set;
    }
    public string CompletionProgress
    {
        get; set;
    }
    public long ResourcesWorthAmount
    {
        get; set;
    }
}
public class vmStudent_Web
{
    public long RegistrationId { get; set; }
    public string Lead_Id { get; set; }
    public string ManagerCode { get; set; }
    public string StudentName { get; set; }
    public string DOB { get; set; }
    public string RegistrationDate { get; set; }
    public int isProfileEdit { get; set; }
    public int ActiveStatus { get; set; }
    public int AlertStatus { get; set; }
    public string AlertDate { get; set; }
    public int StateCode { get; set; }
    public int DistrictCode { get; set; }
    public int TalukaCode { get; set; }
    public int CollegeCode { get; set; }
    public int StreamCode { get; set; }
    public int CourseCode { get; set; }
    public string SemCode { get; set; }
    public long AadharNo { get; set; }
    public string Address { get; set; }
    public string PermanentAddress { get; set; }
    public long MobileNo { get; set; }
    public string MailId { get; set; }
    public long AlternativeMobileNo { get; set; }
    public string AlternateMailId { get; set; }
    public string Gender { get; set; }
    public string BloodGroup { get; set; }
    public string FacebookId { get; set; }
    public string LinkedInId { get; set; }
    public long CreatedBy { get; set; }
    public string CreateDate { get; set; }
    public string AcademicCode { get; set; }
    public string Student_Type { get; set; }
    public string Bank_Name { get; set; }
    public string Branch_Name { get; set; }
    public string Account_No { get; set; }
    public string IFSC_code { get; set; }

    public string AccountHolderName { get; set; }
    public string Status { get; set; }
    public string Student_Image { get; set; }

    public string GetDay { get; set; }
    public string GetMonth { get; set; }
    public string GetYear { get; set; }

    public string MyTalent { get; set; }
}
public class vmManager
{
    public string ManagerID { get; set; }
    public string ManagerName { get; set; }
    public string MobileNo { get; set; }
    public string MailId { get; set; }
    public string Image_path { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string InstaGram { get; set; }
    public int? RecordCount { get; set; }
    public string User_Type
    {
        get; set;
    }

}
public class vmMailNotification
{
    public string LeadId { get; set; }
    public string StudentName { get; set; }
    public string StudentMobileNo { get; set; }
    public string StudentMailId { get; set; }
    public string CollegeName { get; set; }
    public string Title { get; set; }
    public string BeneficiaryNo { get; set; }
    public string ActionPlan { get; set; }
    public string PlaceofImplement { get; set; }
    public string Beneficiaries { get; set; }
    public string Objectives { get; set; }
    public string PlaceOfImplementation { get; set; }
    public string SanctionAmount { get; set; }
    public string FundsReceived { get; set; }
    public string FundsRaised { get; set; }
    public string Challenges { get; set; }

    public string Learning { get; set; }
    public string AsAStory { get; set; }
    public string ProjectStatus { get; set; }
    public string ManagerComments { get; set; }
    public string Duration { get; set; }
    public string CurrentSituation { get; set; }

    public string ProposedDate { get; set; }
    public string ApprovedDate { get; set; }
    public string CompletedDate { get; set; }
    public string RequestForModificationDate { get; set; }
    public string RequestForCompletionDate { get; set; }
    public string RejectedDate { get; set; }

    public string RequestedAmount { get; set; }
   
}
public class vmForgotPassword
{
    public string ExistingMailId { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public string UserID { get; set; }
    public string Password { get; set; }

}
public class vmCookies
{

    //-------Student Cookies----------
    public string LeadId()
    {
        try
        {
            HttpCookie Lead_LeadId = HttpContext.Current.Request.Cookies["Student_LeadId"];
            return HttpContext.Current.Server.HtmlEncode(Lead_LeadId.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }
    public string Student_MobileNo()
    {
        try
        {
            HttpCookie Student_MobileNo = HttpContext.Current.Request.Cookies["Student_MobileNo"];
            return HttpContext.Current.Server.HtmlEncode(Student_MobileNo.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }
    public string RegistrationId()
    {
        try
        {
            HttpCookie Registration_Id = HttpContext.Current.Request.Cookies["Registration_Id"];
            return HttpContext.Current.Server.HtmlEncode(Registration_Id.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string ManagerId()
    {
        try
        {
            HttpCookie Student_ManagerId = HttpContext.Current.Request.Cookies["Student_ManagerId"];
            return HttpContext.Current.Server.HtmlEncode(Student_ManagerId.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string ManagerName()
    {
        try
        {
            HttpCookie Manager_Name = HttpContext.Current.Request.Cookies["Manager_Name"];
            return HttpContext.Current.Server.HtmlEncode(Manager_Name.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Manager_Record()
    {
        try
        {
            HttpCookie Manager_RecordCount = HttpContext.Current.Request.Cookies["Manager_RecordCount"];
            return HttpContext.Current.Server.HtmlEncode(Manager_RecordCount.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Manager_MailId()
    {
        try
        {
            HttpCookie Manager_MailId = HttpContext.Current.Request.Cookies["Manager_MailId"];
            return HttpContext.Current.Server.HtmlEncode(Manager_MailId.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string AcademicYear()
    {
        try
        {
            HttpCookie Student_AcademicYear = HttpContext.Current.Request.Cookies["Student_AcademicYear"];
            return HttpContext.Current.Server.HtmlEncode(Student_AcademicYear.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string isProfileCompleted()
    {
        try
        {
            HttpCookie Lead_isProfileCompleted = HttpContext.Current.Request.Cookies["Student_isProfileCompleted"];
            return HttpContext.Current.Server.HtmlEncode(Lead_isProfileCompleted.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    //-------------Manager Cookies---------------


    public string Manager_UserName()
    {
        try
        {
            HttpCookie Manager_UserName = HttpContext.Current.Request.Cookies["Manager_UserName"];
            return HttpContext.Current.Server.HtmlEncode(Manager_UserName.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string Manager_Id()
    {
        try
        {
            HttpCookie Manager_Id = HttpContext.Current.Request.Cookies["Manager_Id"];
            return HttpContext.Current.Server.HtmlEncode(Manager_Id.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string Manager_AcademicYear()
    {
        try
        {
            HttpCookie Manager_AcademicYear = HttpContext.Current.Request.Cookies["Manager_AcademicYear"];
            return HttpContext.Current.Server.HtmlEncode(Manager_AcademicYear.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Manager_Users_Role()
    {
        try
        {
            HttpCookie Manager_Users_Role = HttpContext.Current.Request.Cookies["Users_Role"];
            return HttpContext.Current.Server.HtmlEncode(Manager_Users_Role.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string Manager_program()
    {
        try
        {
            HttpCookie Program_id = HttpContext.Current.Request.Cookies["Manager_program"];
            return HttpContext.Current.Server.HtmlEncode(Program_id.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Admin_Id()
    {
        try
        {
            HttpCookie Admin_Id = HttpContext.Current.Request.Cookies["Admin_Id"];
            return HttpContext.Current.Server.HtmlEncode(Admin_Id.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Admin_program()
    {
        try
        {
            HttpCookie Program_id = HttpContext.Current.Request.Cookies["Admin_program"];
            return HttpContext.Current.Server.HtmlEncode(Program_id.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Admin_UserName()
    {
        try
        {
            HttpCookie Admin_UserName = HttpContext.Current.Request.Cookies["Admin_UserName"];
            return HttpContext.Current.Server.HtmlEncode(Admin_UserName.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    
    public string Admin_AcademicYear()
    {
        try
        {
            HttpCookie Admin_AcademicYear = HttpContext.Current.Request.Cookies["Admin_AcademicYear"];
            return HttpContext.Current.Server.HtmlEncode(Admin_AcademicYear.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Admin_Users_Role()
    {
        try
        {
            HttpCookie Admin_Users_Role = HttpContext.Current.Request.Cookies["Admin_Users_Role"];
            return HttpContext.Current.Server.HtmlEncode(Admin_Users_Role.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string User_Type()
    {
        try
        {
            HttpCookie User_Type = HttpContext.Current.Request.Cookies["User_Type"];
            return HttpContext.Current.Server.HtmlEncode(User_Type.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    //College Login Cookies

    public string CollegeLogin_Id()
    {
        try
        {
            HttpCookie CollegeLogin_Id = HttpContext.Current.Request.Cookies["CollegeLogin_Id"];
            return HttpContext.Current.Server.HtmlEncode(CollegeLogin_Id.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string Temp_Selected_Year()
    {
        try
        {
            HttpCookie Selected_Year = HttpContext.Current.Request.Cookies["Selected_Year"];
            return HttpContext.Current.Server.HtmlEncode(Selected_Year.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }
    public string Account_Id()
    {
        try
        {
            HttpCookie Account_Id = HttpContext.Current.Request.Cookies["Account_Id"];
            return HttpContext.Current.Server.HtmlEncode(Account_Id.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Account_UserName()
    {
        try
        {
            HttpCookie Account_UserName = HttpContext.Current.Request.Cookies["Account_UserName"];
            return HttpContext.Current.Server.HtmlEncode(Account_UserName.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Account_AcademicYear()
    {
        try
        {
            HttpCookie Account_AcademicYear = HttpContext.Current.Request.Cookies["Account_AcademicYear"];
            return HttpContext.Current.Server.HtmlEncode(Account_AcademicYear.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Account_Users_Role()
    {
        try
        {
            HttpCookie Account_Users_Role = HttpContext.Current.Request.Cookies["Account_Users_Role"];
            return HttpContext.Current.Server.HtmlEncode(Account_Users_Role.Value);

        }
        catch (Exception)
        {
            return "";
        }
    }

}

public class vmProgramme
{
    public string Content { get; set; }
    public string Event_Type { get; set; }
    public string tableName { get; set; }
    public int Status { get; set; }
    public string Type { get; set; }

}
public class vmGeneral
{

    public string DecryptString(string cipherText)
    {
        string _appSettings = ConfigurationManager.AppSettings["SecurityKey"].ToString();
        //URL Decrytion Avoid Reserved Characters
        cipherText = cipherText.Replace("-2F-", "/");
        cipherText = cipherText.Replace("-21-", "!");
        cipherText = cipherText.Replace("-23-", "#");
        cipherText = cipherText.Replace("-24-", "$");
        cipherText = cipherText.Replace("-26-", "&");
        cipherText = cipherText.Replace("-27-", "'");
        cipherText = cipherText.Replace("-28-", "(");
        cipherText = cipherText.Replace("-29-", ")");
        cipherText = cipherText.Replace("-2A-", "*");
        cipherText = cipherText.Replace("-2B-", "+");
        cipherText = cipherText.Replace("-2C-", ",");
        cipherText = cipherText.Replace("-3A-", ":");
        cipherText = cipherText.Replace("-3B-", ";");
        cipherText = cipherText.Replace("-3D-", "=");
        cipherText = cipherText.Replace("-3F-", "?");
        cipherText = cipherText.Replace("-40-", "@");
        cipherText = cipherText.Replace("-5B-", "[");
        cipherText = cipherText.Replace("-5D-", "]");

        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_appSettings);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    
}
public class vmLEAD_Login
{
    public string Manager_Id
    {
        get; set;
    }
    public string Manager_UserName
    {
        get; set;
    }
    public string Manager_Name
    {
        get; set;
    }
    public string Manager_AcademicYear
    {
        get; set;
    }
    public string Users_Role
    {
        get; set;
    }
    public string User_Type
    {
        get; set;
    }
    public string Manager_RecordCount
    {
        get; set;
    }
    public string Manager_MailId
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
}


