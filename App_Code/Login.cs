using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mail;

/// <summary>
/// Summary description for Login
/// </summary>
[WebService(Namespace = "http://mis.leadcampus.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Login : System.Web.Services.WebService
{

    public Login()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public vmLogin ValidateLogin(string Username, string Password)
    {
        vmLogin vmLg = null;
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_VALIDE_USERS";
                cmd.Parameters.AddWithValue("p_Username", Username);
                cmd.Parameters.AddWithValue("p_Password", Password);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        vmLg = new vmLogin();
                        vmLg.LoginId = long.Parse(dr["LoginId"].ToString());
                        vmLg.Username = dr["Username"].ToString();
                        vmLg.Role = dr["Role"].ToString();
                        vmLg.isProfileEdit = int.Parse(dr["isProfileEdit"].ToString());
                        vmLg.MobileNo = dr["MobileNo"].ToString();
                        vmLg.College_Name = dr["College_Name"].ToString();
                        vmLg.AcademicId = dr["AcademicId"].ToString();
                        vmLg.Name = dr["Name"].ToString();
                        vmLg.ManagerName = dr["ManagerName"].ToString();
                        vmLg.Lead_Id = dr["Lead_Id"].ToString();
                        vmLg.MailId = dr["MailId"].ToString();
                        vmLg.Location = dr["Location"].ToString();
                        vmLg.RegistrationId = int.Parse(dr["RegistrationId"].ToString());
                        vmLg.ManagerId = int.Parse(dr["ManagerId"].ToString());
                        vmLg.UserImage = dr["Image_Path"].ToString();
                        vmLg.Manager_Image_Path = dr["Manager_Image_Path"].ToString();
                        vmLg.Facebook = dr["Facebook"].ToString();
                        vmLg.Twitter = dr["Twitter"].ToString();
                        vmLg.InstaGram = dr["InstaGram"].ToString();
                        vmLg.Student_Type = dr["Student_Type"].ToString();
                        vmLg.Gender = dr["Gender"].ToString();
                        vmLg.StartCount = int.Parse(dr["NoOfStarts"].ToString());
                        vmLg.Student_Mobile_No = long.Parse(dr["Student_Mobile_No"].ToString());
                        vmLg.isFeePaid = int.Parse(dr["isFeePaid"].ToString());
                        vmLg.WhatsApp = dr["WhatsApp"].ToString();

                        vmLg.Status = "Success";
                    }
                }
                else
                {
                    vmLg = new vmLogin();
                    vmLg.Status = "Invalid Username or Password";
                }
            }
        }
        catch (Exception)
        {
            vmLg = new vmLogin();
            vmLg.Status = "Error";
        }
        return vmLg;
    }


    //public int WebLoginCheck(string Username, string Password)
    //{
    //    try
    //    {
    //        using (MySqlConnection con = new MySqlConnection(connection))
    //        {
    //            con.Open();
    //            MySqlCommand cmd = new MySqlCommand();
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_APP_VALIDE_USERS";
    //            cmd.Parameters.AddWithValue("p_Username", Username);
    //            cmd.Parameters.AddWithValue("p_Password", Password);
    //            cmd.Connection = con;
    //            MySqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.HasRows)
    //            {
    //                if (dr.Read())
    //                {
    //                    vmLg = new vmLogin();
    //                    vmLg.LoginId = long.Parse(dr["LoginId"].ToString());
    //                    vmLg.Username = dr["Username"].ToString();
    //                    vmLg.Role = dr["Role"].ToString();
    //                    vmLg.isProfileEdit = int.Parse(dr["isProfileEdit"].ToString());
    //                    vmLg.MobileNo = dr["MobileNo"].ToString();
    //                    vmLg.College_Name = dr["College_Name"].ToString();
    //                    vmLg.AcademicId = dr["AcademicId"].ToString();
    //                    vmLg.Name = dr["Name"].ToString();
    //                    vmLg.ManagerName = dr["ManagerName"].ToString();
    //                    vmLg.Lead_Id = dr["Lead_Id"].ToString();
    //                    vmLg.MailId = dr["MailId"].ToString();
    //                    vmLg.Location = dr["Location"].ToString();
    //                    vmLg.RegistrationId = int.Parse(dr["RegistrationId"].ToString());
    //                    vmLg.ManagerId = int.Parse(dr["ManagerId"].ToString());
    //                    vmLg.UserImage = dr["Image_Path"].ToString();
    //                    vmLg.Manager_Image_Path = dr["Manager_Image_Path"].ToString();
    //                    vmLg.Facebook = dr["Facebook"].ToString();
    //                    vmLg.Twitter = dr["Twitter"].ToString();
    //                    vmLg.InstaGram = dr["InstaGram"].ToString();
    //                    vmLg.Student_Type = dr["Student_Type"].ToString();
    //                    vmLg.Gender = dr["Gender"].ToString();
    //                    vmLg.StartCount = int.Parse(dr["NoOfStarts"].ToString());
    //                    vmLg.Student_Mobile_No = long.Parse(dr["Student_Mobile_No"].ToString());
    //                    vmLg.isFeePaid = int.Parse(dr["isFeePaid"].ToString());
    //                    vmLg.WhatsApp = dr["WhatsApp"].ToString();

    //                    vmLg.Status = "Success";
    //                }
    //            }
    //            else
    //            {
    //                vmLg = new vmLogin();
    //                vmLg.Status = "Invalid Username or Password";
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        vmLg = new vmLogin();
    //        vmLg.Status = "Error";
    //    }
    //    return vmLg;
    //}


    [WebMethod]
    public vmStudent GetStudentDetailss(string leadId)
    {
        vmStudent stud = new vmStudent();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SELECT_STUDENT_DETAILS_NEW";
                cmd.Parameters.AddWithValue("p_LeadId", leadId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        stud.RegistrationId = long.Parse(dr["RegistrationId"].ToString());
                        stud.Lead_Id = dr["Lead_Id"].ToString();
                        stud.StudentName = dr["StudentName"].ToString();
                        stud.DOB = dr["DOB"].ToString();
                        stud.RegistrationDate = dr["RegistrationDate"].ToString();
                        stud.isProfileEdit = int.Parse(dr["isProfileEdit"].ToString());
                        stud.StateCode = int.Parse(dr["StateCode"].ToString());
                        stud.DistrictCode = int.Parse(dr["DistrictCode"].ToString());
                        stud.TalukaCode = int.Parse(dr["TalukaCode"].ToString());
                        stud.CollegeCode = int.Parse(dr["CollegeCode"].ToString());
                        stud.CourseCode = int.Parse(dr["CourseCode"].ToString());
                        stud.StreamCode = int.Parse(dr["StreamCode"].ToString());
                        stud.SemCode = dr["SemCode"].ToString();
                        stud.StateName = dr["StateName"].ToString();
                        stud.DistrictName = dr["DistrictName"].ToString();
                        stud.Taluk_Name = dr["Taluk_Name"].ToString();
                        stud.College_Name = dr["College_Name"].ToString();
                        stud.programmeName = dr["programmeName"].ToString();
                        stud.CourseName = dr["CourseName"].ToString();
                        stud.SemName = dr["SemName"].ToString();
                        stud.AadharNo = long.Parse(dr["AadharNo"].ToString());
                        stud.Address = dr["Address"].ToString();
                        stud.MobileNo = long.Parse(dr["MobileNo"].ToString());
                        stud.MailId = dr["MailId"].ToString();
                        stud.AlternativeMobileNo = long.Parse(dr["AlternativeMobileNo"].ToString());
                        stud.AlternateMailId = dr["AlternateMailId"].ToString();
                        stud.Gender = dr["Gender"].ToString();
                        stud.BloodGroup = dr["BloodGroup"].ToString();
                        stud.FacebookId = dr["FacebookId"].ToString();
                        stud.LinkedInId = dr["LinkedInId"].ToString();
                        stud.AcademicCode = dr["AcademicCode"].ToString();
                        stud.Student_Type = dr["Student_Type"].ToString();
                        stud.Bank_Name = dr["Bank_Name"].ToString();
                        stud.Branch_Name = dr["Branch_Name"].ToString();
                        stud.Account_No = dr["Account_No"].ToString();
                        stud.IFSC_code = dr["IFSC_code"].ToString();
                        stud.AccountHolderName = dr["Account_HolderName"].ToString();
                        stud.Student_Image = dr["image_Path"].ToString();
                        stud.College_Name = dr["College_Name"].ToString();
                        stud.ManagerName = dr["ManagerName"].ToString();
                        stud.ManagerMailid = dr["ManagerMailid"].ToString();
                        stud.ManagerImagePath = dr["ManagerImagePath"].ToString();
                        stud.ManagerMobileNumber = long.Parse(dr["ManagerMobileNumber"].ToString());
                        stud.Manager_Facebook = dr["Manager_Facebook"].ToString();
                        stud.Manager_Twitter = dr["Manager_Twitter"].ToString();
                        stud.Manager_Instagram = dr["Manager_Instagram"].ToString();
                        stud.Manager_WhatsApp = dr["Manager_WhatsApp"].ToString();
                        stud.Status = "Success";
                        stud.MyTalent = dr["MyTalent"].ToString();

                    }
                }
                else
                {
                    stud.Status = "Invalid Lead Id";
                }
            }
        }
        catch (Exception)
        {
            stud.Status = "Error";
        }
        return stud;
    }


    [WebMethod]
    public string UpdateStudentProfilewithCompressTemporary(long RegistrationId, string Lead_Id, string StudentName, string DOB, int StateCode, int DistrictCode, int TalukaCode, int CollegeCode,
    int StreamCode, int CourseCode, int SemCode, long AadharNo, string Address, string MailId, long AlternativeMobileNo, string AlternateMailId, string Gender, string BloodGroup, string FacebookId,
     string LinkedInId, int AcademicCode, string Bank_Name, string Branch_Name, string Account_No, string IFSC_code, byte[] ProfileImage, string MyTalent,string AccountHolderName)
    {
        string status = "";
        try
        {
            //byte[] ProfileImage = null;
            string fileName = "";
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {

                    try
                    {
                        using (var ms = new MemoryStream(ProfileImage))
                        {
                            fileName = "~/ProfilePics/" + Lead_Id + "_" + "_" + Guid.NewGuid().ToString() + ".jpg";
                            System.Drawing.Image.FromStream(ms);
                            ms.Position = 0;
                            using (System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(ms))
                            {
                                System.Drawing.Imaging.ImageCodecInfo jpgEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);

                                //  Create an Encoder object based on the GUID
                                //   for the Quality parameter category.
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                                System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 50L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                bmp1.Save(Server.MapPath(fileName), jpgEncoder, myEncoderParameters);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        fileName = "";
                        status = ex.Message.ToString();
                    }
                }
                else
                    status = "Image is empty";
            }
            else
                status = "Image is null";

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_STUDENT_PROFILE_NEW_TESTBED";
                cmd.Parameters.AddWithValue("p_RegistrationId", RegistrationId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_StudentName", Regex.Replace(StudentName, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_DOB", DOB);
                cmd.Parameters.AddWithValue("p_StateCode", StateCode);
                cmd.Parameters.AddWithValue("p_DistrictCode", DistrictCode);
                cmd.Parameters.AddWithValue("p_TalukaCode", TalukaCode);
                cmd.Parameters.AddWithValue("p_CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("p_StreamCode", StreamCode);
                cmd.Parameters.AddWithValue("p_CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("p_SemCode", SemCode);
                cmd.Parameters.AddWithValue("p_AadharNo", AadharNo);
                cmd.Parameters.AddWithValue("p_Address", Regex.Replace(Address, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_MailId", Regex.Replace(MailId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AlternativeMobileNo", AlternativeMobileNo);
                cmd.Parameters.AddWithValue("p_AlternateMailId", Regex.Replace(AlternateMailId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Gender", Gender);
                cmd.Parameters.AddWithValue("p_BloodGroup", BloodGroup);
                cmd.Parameters.AddWithValue("p_FacebookId", Regex.Replace(FacebookId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_LinkedInId", Regex.Replace(LinkedInId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AcademicCode", AcademicCode);
                cmd.Parameters.AddWithValue("p_Bank_Name", Regex.Replace(Bank_Name, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Branch_Name", Regex.Replace(Branch_Name, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Account_No", Account_No);
                cmd.Parameters.AddWithValue("p_IFSC_code", IFSC_code);
                cmd.Parameters.AddWithValue("p_Account_Holdername", Regex.Replace(AccountHolderName, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Image_path", fileName);
                cmd.Parameters.AddWithValue("p_MyTalent", MyTalent);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0 && fileName != "")
                    status = "Profile Updated.";
                else if (i > 0 && fileName == "")
                    status = "Profile Updated but imaeg not saved.";
                else
                    status = "Unable to update Student Profile";


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_EMAIL_STUDENT_DETAILS";
                cmd.Parameters.AddWithValue("P_id", Lead_Id.ToString());
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    string body = PopulateBody(StudentName.ToString(), "<b>Edit Profile Successfully (MOB)</b>", "The details you entered are listed below:",
                        "<ol><br><p><b>UI Entered data</b></p><br><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + StudentName.ToString() + "<br><br></li><li><b>DOB:</b> " + DOB.ToString() + "<br><br></li><li><b>StateCode:</b> " + StateCode.ToString() + "<br><br></li><li><b>DistrictCode:</b> " + DistrictCode.ToString() + "<br><br></li><li><b>TalukaCode:</b> " + TalukaCode.ToString() +
                     "<br><br></li><li><b>CollegeCode:</b> " + CollegeCode.ToString() + "<br><br></li><li><b>StreamCode:</b> " + StreamCode.ToString() + "<br><br></li><li><b>CourseCode:</b> " + CourseCode.ToString() + "<br><br></li><li><b>p_SemCode:</b> " + SemCode.ToString() + "<br><br></li><li><b>AadharNo:</b> " + AadharNo.ToString() + "<br><br></li><li><b>MailId:</b> " + MailId.ToString() + "<br><br></li><li><b>Whatsapp:</b> " + AlternativeMobileNo.ToString() + "<br><br></li><li><b>Gender:</b> " + Gender.ToString() +
                     "<br><br></li><li><b>BloodGroup:</b> " + BloodGroup.ToString() + "<br><br></li><li><b>Account_No:</b> " + Account_No.ToString() +
                     "<br><br></li></ol><br><hr><p><b>Server data</b><p><br><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>Student_Type:</b> " + dt2.Rows[0]["Student_Type"].ToString() +
                       "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>DOB:</b> " + dt2.Rows[0]["DOB"].ToString() + "<br><br></li><li><b>StateCode:</b> " + dt2.Rows[0]["StateCode"].ToString() +
                         "<br> <br></li><li><b>DistrictCode:</b> " + dt2.Rows[0]["DistrictCode"].ToString() + "<br> <br></li><li><b>TalukaCode:</b> " + dt2.Rows[0]["TalukaCode"].ToString() + "<br> <br></li><li><b>Collegecode:</b> " + dt2.Rows[0]["Collegecode"].ToString() + "<br> <br></li><li><b>StreamCode:</b> " + dt2.Rows[0]["StreamCode"].ToString() +
                        "<br><br></li><li><b>CourseCode:</b> " + dt2.Rows[0]["CourseCode"].ToString() + "<br><br></li><li><b>SemCode:</b> " + dt2.Rows[0]["SemCode"].ToString() + "<br><br></li><li><b>AadharNo:</b> " + dt2.Rows[0]["AadharNo"].ToString() + "<br><br></li><li><b>isProfileEdit:</b> " + dt2.Rows[0]["isProfileEdit"].ToString() + "<br><br></li><li><b>Whatsapp:</b> " + dt2.Rows[0]["AlternativeMobileNo"].ToString() + "<br><br></li><li><b>MailId:</b> " + dt2.Rows[0]["MailId"].ToString() +
                        "<br><br></li><li><b>isFeePaid:</b> " + dt2.Rows[0]["isFeePaid"].ToString() + "<br><br></li><li><b> DeviceType:</b> " + dt2.Rows[0]["DeviceType"].ToString() + " <br><br></li><li><b>Gender:</b> " + dt2.Rows[0]["Gender"].ToString() + " <br><br></li><li><b>BloodGroup:</b> " + dt2.Rows[0]["BloodGroup"].ToString() +
                        " <br><br></li><li><b>Account_No:</b> " + dt2.Rows[0]["Account_No"].ToString() + " <br><br></li><li><b>IFSC_code:</b> " + dt2.Rows[0]["IFSC_code"].ToString() + " <br><br></li>");
                    SendstudentDetails(body, "", "Your Profile Completed successfully");

                    string bod = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your Profile Edit is Completed successfully.</b>", "The details you entered are listed below:",
                        "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>Student_Type:</b> " + dt2.Rows[0]["Student_Type"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>DOB:</b> " + dt2.Rows[0]["DOB"].ToString() + "<br><br></li><li><b>StateName:</b> " + dt2.Rows[0]["StateName"].ToString() + "<br> <br></li><li><b>DistrictName:</b> " + dt2.Rows[0]["DistrictName"].ToString() + "<br> <br></li><li><b>Taluk_Name:</b> " + dt2.Rows[0]["Taluk_Name"].ToString() + "<br> <br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>programmeName:</b> " + dt2.Rows[0]["programmeName"].ToString() + "<br><br></li><li><b>CourseName:</b> " + dt2.Rows[0]["CourseName"].ToString() + "<br><br></li><li><b>SemName:</b> " + dt2.Rows[0]["SemName"].ToString() + "<br><br></li><li><b>AadharNo:</b> " + dt2.Rows[0]["AadharNo"].ToString() + "<br><br></li><li><b>Address:</b> " + dt2.Rows[0]["Address"].ToString() + "<br><br></li><li><b>MailId:</b> " + dt2.Rows[0]["MailId"].ToString() + "<br><br></li><li><b>AlternativeMobileNo:</b> " + dt2.Rows[0]["AlternativeMobileNo"].ToString() + "<br><br></li><li><b> AlternateMailId:</b> " + dt2.Rows[0]["AlternateMailId"].ToString() + " <br><br></li><li><b>Gender:</b> " + dt2.Rows[0]["Gender"].ToString() + " <br><br></li><li><b>BloodGroup:</b> " + dt2.Rows[0]["BloodGroup"].ToString() + " <br><br></li><li><b>Account_No:</b> " + dt2.Rows[0]["Account_No"].ToString() + " <br><br></li><li><b>IFSC_code:</b> " + dt2.Rows[0]["IFSC_code"].ToString() + " <br><br></li></ol><br><br>");
                    SendstudentDetailss(bod, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " Profile Completed successfully");
                }

            }
        }
        catch (Exception ex)
        {
            status += ex.Message.ToString();
        }
        return status;
    }


    [WebMethod]
    public string UpdateStudentProfilewithCompress(long RegistrationId, string Lead_Id, string StudentName, string DOB, int StateCode, int DistrictCode, int TalukaCode, int CollegeCode,
      int StreamCode, int CourseCode, int SemCode, long AadharNo, string Address, string MailId, long AlternativeMobileNo, string AlternateMailId, string Gender, string BloodGroup, string FacebookId,
       string LinkedInId, int AcademicCode, string Bank_Name, string Branch_Name, string Account_No, string IFSC_code, byte[] ProfileImage, string MyTalent)
    {
        string status = "";
        try
        {
            //byte[] ProfileImage = null;
            string fileName = "";
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {

                    try
                    {
                        using (var ms = new MemoryStream(ProfileImage))
                        {
                            fileName = "~/ProfilePics/" + Lead_Id + "_" + "_" + Guid.NewGuid().ToString() + ".jpg";
                            System.Drawing.Image.FromStream(ms);
                            ms.Position = 0;
                            using (System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(ms))
                            {
                                System.Drawing.Imaging.ImageCodecInfo jpgEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);

                                //  Create an Encoder object based on the GUID
                                //   for the Quality parameter category.
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                                System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 50L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                bmp1.Save(Server.MapPath(fileName), jpgEncoder, myEncoderParameters);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        fileName = "";
                        status = ex.Message.ToString();
                    }
                }
                else
                    status = "Image is empty";
            }
            else
                status = "Image is null";

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_STUDENT_PROFILE_NEW";
                cmd.Parameters.AddWithValue("p_RegistrationId", RegistrationId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_StudentName", Regex.Replace(StudentName, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_DOB", DOB);
                cmd.Parameters.AddWithValue("p_StateCode", StateCode);
                cmd.Parameters.AddWithValue("p_DistrictCode", DistrictCode);
                cmd.Parameters.AddWithValue("p_TalukaCode", TalukaCode);
                cmd.Parameters.AddWithValue("p_CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("p_StreamCode", StreamCode);
                cmd.Parameters.AddWithValue("p_CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("p_SemCode", SemCode);
                cmd.Parameters.AddWithValue("p_AadharNo", AadharNo);
                cmd.Parameters.AddWithValue("p_Address", Regex.Replace(Address, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_MailId", Regex.Replace(MailId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AlternativeMobileNo", AlternativeMobileNo);
                cmd.Parameters.AddWithValue("p_AlternateMailId", Regex.Replace(AlternateMailId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Gender", Gender);
                cmd.Parameters.AddWithValue("p_BloodGroup", BloodGroup);
                cmd.Parameters.AddWithValue("p_FacebookId", Regex.Replace(FacebookId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_LinkedInId", Regex.Replace(LinkedInId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AcademicCode", AcademicCode);
                cmd.Parameters.AddWithValue("p_Bank_Name", Regex.Replace(Bank_Name, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Branch_Name", Regex.Replace(Branch_Name, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Account_No", Account_No);
                cmd.Parameters.AddWithValue("p_IFSC_code", IFSC_code);
                cmd.Parameters.AddWithValue("p_Image_path", fileName);
                cmd.Parameters.AddWithValue("p_MyTalent", MyTalent);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0 && fileName != "")
                    status = "Profile Updated.";
                else if (i > 0 && fileName == "")
                    status = "Profile Updated but imaeg not saved.";
                else
                    status = "Unable to update Student Profile";


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_EMAIL_STUDENT_DETAILS";
                cmd.Parameters.AddWithValue("P_id", Lead_Id.ToString());
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    string body = PopulateBody(StudentName.ToString(), "<b>Edit Profile Successfully (MOB)</b>", "The details you entered are listed below:",
                        "<ol><br><p><b>UI Entered data</b></p><br><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + StudentName.ToString() + "<br><br></li><li><b>DOB:</b> " + DOB.ToString() + "<br><br></li><li><b>StateCode:</b> " + StateCode.ToString() + "<br><br></li><li><b>DistrictCode:</b> " + DistrictCode.ToString() + "<br><br></li><li><b>TalukaCode:</b> " + TalukaCode.ToString() +
                     "<br><br></li><li><b>CollegeCode:</b> " + CollegeCode.ToString() + "<br><br></li><li><b>StreamCode:</b> " + StreamCode.ToString() + "<br><br></li><li><b>CourseCode:</b> " + CourseCode.ToString() + "<br><br></li><li><b>p_SemCode:</b> " + SemCode.ToString() + "<br><br></li><li><b>AadharNo:</b> " + AadharNo.ToString() + "<br><br></li><li><b>MailId:</b> " + MailId.ToString() + "<br><br></li><li><b>Whatsapp:</b> " + AlternativeMobileNo.ToString() + "<br><br></li><li><b>Gender:</b> " + Gender.ToString() +
                     "<br><br></li><li><b>BloodGroup:</b> " + BloodGroup.ToString() + "<br><br></li><li><b>Account_No:</b> " + Account_No.ToString() +
                     "<br><br></li></ol><br><hr><p><b>Server data</b><p><br><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>Student_Type:</b> " + dt2.Rows[0]["Student_Type"].ToString() +
                       "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>DOB:</b> " + dt2.Rows[0]["DOB"].ToString() + "<br><br></li><li><b>StateCode:</b> " + dt2.Rows[0]["StateCode"].ToString() +
                         "<br> <br></li><li><b>DistrictCode:</b> " + dt2.Rows[0]["DistrictCode"].ToString() + "<br> <br></li><li><b>TalukaCode:</b> " + dt2.Rows[0]["TalukaCode"].ToString() + "<br> <br></li><li><b>Collegecode:</b> " + dt2.Rows[0]["Collegecode"].ToString() + "<br> <br></li><li><b>StreamCode:</b> " + dt2.Rows[0]["StreamCode"].ToString() +
                        "<br><br></li><li><b>CourseCode:</b> " + dt2.Rows[0]["CourseCode"].ToString() + "<br><br></li><li><b>SemCode:</b> " + dt2.Rows[0]["SemCode"].ToString() + "<br><br></li><li><b>AadharNo:</b> " + dt2.Rows[0]["AadharNo"].ToString() + "<br><br></li><li><b>isProfileEdit:</b> " + dt2.Rows[0]["isProfileEdit"].ToString() + "<br><br></li><li><b>Whatsapp:</b> " + dt2.Rows[0]["AlternativeMobileNo"].ToString() + "<br><br></li><li><b>MailId:</b> " + dt2.Rows[0]["MailId"].ToString() +
                        "<br><br></li><li><b>isFeePaid:</b> " + dt2.Rows[0]["isFeePaid"].ToString() + "<br><br></li><li><b> DeviceType:</b> " + dt2.Rows[0]["DeviceType"].ToString() + " <br><br></li><li><b>Gender:</b> " + dt2.Rows[0]["Gender"].ToString() + " <br><br></li><li><b>BloodGroup:</b> " + dt2.Rows[0]["BloodGroup"].ToString() +
                        " <br><br></li><li><b>Account_No:</b> " + dt2.Rows[0]["Account_No"].ToString() + " <br><br></li><li><b>IFSC_code:</b> " + dt2.Rows[0]["IFSC_code"].ToString() + " <br><br></li>");
                    SendstudentDetails(body, "", "Your Profile Completed successfully");

                    string bod = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your Profile Edit is Completed successfully.</b>", "The details you entered are listed below:",
                        "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>Student_Type:</b> " + dt2.Rows[0]["Student_Type"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>DOB:</b> " + dt2.Rows[0]["DOB"].ToString() + "<br><br></li><li><b>StateName:</b> " + dt2.Rows[0]["StateName"].ToString() + "<br> <br></li><li><b>DistrictName:</b> " + dt2.Rows[0]["DistrictName"].ToString() + "<br> <br></li><li><b>Taluk_Name:</b> " + dt2.Rows[0]["Taluk_Name"].ToString() + "<br> <br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>programmeName:</b> " + dt2.Rows[0]["programmeName"].ToString() + "<br><br></li><li><b>CourseName:</b> " + dt2.Rows[0]["CourseName"].ToString() + "<br><br></li><li><b>SemName:</b> " + dt2.Rows[0]["SemName"].ToString() + "<br><br></li><li><b>AadharNo:</b> " + dt2.Rows[0]["AadharNo"].ToString() + "<br><br></li><li><b>Address:</b> " + dt2.Rows[0]["Address"].ToString() + "<br><br></li><li><b>MailId:</b> " + dt2.Rows[0]["MailId"].ToString() + "<br><br></li><li><b>AlternativeMobileNo:</b> " + dt2.Rows[0]["AlternativeMobileNo"].ToString() + "<br><br></li><li><b> AlternateMailId:</b> " + dt2.Rows[0]["AlternateMailId"].ToString() + " <br><br></li><li><b>Gender:</b> " + dt2.Rows[0]["Gender"].ToString() + " <br><br></li><li><b>BloodGroup:</b> " + dt2.Rows[0]["BloodGroup"].ToString() + " <br><br></li><li><b>Account_No:</b> " + dt2.Rows[0]["Account_No"].ToString() + " <br><br></li><li><b>IFSC_code:</b> " + dt2.Rows[0]["IFSC_code"].ToString() + " <br><br></li></ol><br><br>");
                    SendstudentDetailss(bod, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " Profile Completed successfully");
                }

            }
        }
        catch (Exception ex)
        {
            status += ex.Message.ToString();
        }
        return status;
    }
    public System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
    {
        System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();
        foreach (System.Drawing.Imaging.ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }

    [WebMethod]
    public string UpdateStudentProfilewithstring(long RegistrationId, string Lead_Id, string StudentName, string DOB, int StateCode, int DistrictCode, int TalukaCode, int CollegeCode,
      int StreamCode, int CourseCode, int SemCode, long AadharNo, string Address, string MailId, long AlternativeMobileNo, string AlternateMailId, string Gender, string BloodGroup, string FacebookId,
       string LinkedInId, int AcademicCode, string Bank_Name, string Branch_Name, string Account_No, string IFSC_code, string ProfileImage)
    {
        string status = "";
        try
        {
            //byte[] ProfileImage = null;
            string fileName = "";
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {

                    try
                    {
                        fileName = "~/ProfilePics/" + Lead_Id + "_" + "_" + Guid.NewGuid().ToString() + ".jpg";
                        byte[] bytes = Convert.FromBase64String(ProfileImage);
                        var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                        fs.Write(bytes);
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        fileName = "";
                        status = ex.Message.ToString();
                    }
                }
                else
                    status = "Image is empty";
            }
            else
                status = "Image is null";

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_STUDENT_PROFILE";
                cmd.Parameters.AddWithValue("p_RegistrationId", RegistrationId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_StudentName", Regex.Replace(StudentName, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_DOB", DOB);
                cmd.Parameters.AddWithValue("p_StateCode", StateCode);
                cmd.Parameters.AddWithValue("p_DistrictCode", DistrictCode);
                cmd.Parameters.AddWithValue("p_TalukaCode", TalukaCode);
                cmd.Parameters.AddWithValue("p_CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("p_StreamCode", StreamCode);
                cmd.Parameters.AddWithValue("p_CourseCode", CourseCode);
                cmd.Parameters.AddWithValue("p_SemCode", SemCode);
                cmd.Parameters.AddWithValue("p_AadharNo", AadharNo);
                cmd.Parameters.AddWithValue("p_Address", Regex.Replace(Address, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_MailId", Regex.Replace(MailId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AlternativeMobileNo", AlternativeMobileNo);
                cmd.Parameters.AddWithValue("p_AlternateMailId", Regex.Replace(AlternateMailId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Gender", Gender);
                cmd.Parameters.AddWithValue("p_BloodGroup", BloodGroup);
                cmd.Parameters.AddWithValue("p_FacebookId", Regex.Replace(FacebookId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_LinkedInId", Regex.Replace(LinkedInId, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AcademicCode", AcademicCode);
                cmd.Parameters.AddWithValue("p_Bank_Name", Regex.Replace(Bank_Name, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Branch_Name", Regex.Replace(Branch_Name, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Account_No", Account_No);
                cmd.Parameters.AddWithValue("p_IFSC_code", IFSC_code);
                cmd.Parameters.AddWithValue("p_Image_path", fileName);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0 && fileName != "")
                    status = "Profile Updated.";
                else if (i > 0 && fileName == "")
                    status = "Profile Updated but imaeg not saved.";
                else
                    status = "Unable to update Student Profile";


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_EMAIL_STUDENT_DETAILS";
                cmd.Parameters.AddWithValue("P_id", Lead_Id.ToString());
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    string body = PopulateBody(StudentName.ToString(), "<b>Edit Profile Successfully (MOB)</b>", "The details you entered are listed below:",
                        "<ol><br><p><b>UI Entered data</b></p><br><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + StudentName.ToString() + "<br><br></li><li><b>DOB:</b> " + DOB.ToString() + "<br><br></li><li><b>StateCode:</b> " + StateCode.ToString() + "<br><br></li><li><b>DistrictCode:</b> " + DistrictCode.ToString() + "<br><br></li><li><b>TalukaCode:</b> " + TalukaCode.ToString() +
                     "<br><br></li><li><b>CollegeCode:</b> " + CollegeCode.ToString() + "<br><br></li><li><b>StreamCode:</b> " + StreamCode.ToString() + "<br><br></li><li><b>CourseCode:</b> " + CourseCode.ToString() + "<br><br></li><li><b>p_SemCode:</b> " + SemCode.ToString() + "<br><br></li><li><b>AadharNo:</b> " + AadharNo.ToString() + "<br><br></li><li><b>MailId:</b> " + MailId.ToString() + "<br><br></li><li><b>Whatsapp:</b> " + AlternativeMobileNo.ToString() + "<br><br></li><li><b>Gender:</b> " + Gender.ToString() +
                     "<br><br></li><li><b>BloodGroup:</b> " + BloodGroup.ToString() + "<br><br></li><li><b>Account_No:</b> " + Account_No.ToString() +
                     "<br><br></li></ol><br><hr><p><b>Server data</b><p><br><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>Student_Type:</b> " + dt2.Rows[0]["Student_Type"].ToString() +
                       "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>DOB:</b> " + dt2.Rows[0]["DOB"].ToString() + "<br><br></li><li><b>StateCode:</b> " + dt2.Rows[0]["StateCode"].ToString() +
                         "<br> <br></li><li><b>DistrictCode:</b> " + dt2.Rows[0]["DistrictCode"].ToString() + "<br> <br></li><li><b>TalukaCode:</b> " + dt2.Rows[0]["TalukaCode"].ToString() + "<br> <br></li><li><b>Collegecode:</b> " + dt2.Rows[0]["Collegecode"].ToString() + "<br> <br></li><li><b>StreamCode:</b> " + dt2.Rows[0]["StreamCode"].ToString() +
                        "<br><br></li><li><b>CourseCode:</b> " + dt2.Rows[0]["CourseCode"].ToString() + "<br><br></li><li><b>SemCode:</b> " + dt2.Rows[0]["SemCode"].ToString() + "<br><br></li><li><b>AadharNo:</b> " + dt2.Rows[0]["AadharNo"].ToString() + "<br><br></li><li><b>isProfileEdit:</b> " + dt2.Rows[0]["isProfileEdit"].ToString() + "<br><br></li><li><b>Whatsapp:</b> " + dt2.Rows[0]["AlternativeMobileNo"].ToString() + "<br><br></li><li><b>MailId:</b> " + dt2.Rows[0]["MailId"].ToString() +
                        "<br><br></li><li><b>isFeePaid:</b> " + dt2.Rows[0]["isFeePaid"].ToString() + "<br><br></li><li><b> DeviceType:</b> " + dt2.Rows[0]["DeviceType"].ToString() + " <br><br></li><li><b>Gender:</b> " + dt2.Rows[0]["Gender"].ToString() + " <br><br></li><li><b>BloodGroup:</b> " + dt2.Rows[0]["BloodGroup"].ToString() +
                        " <br><br></li><li><b>Account_No:</b> " + dt2.Rows[0]["Account_No"].ToString() + " <br><br></li><li><b>IFSC_code:</b> " + dt2.Rows[0]["IFSC_code"].ToString() + " <br><br></li>");
                    SendstudentDetails(body, "", "Your Profile Completed successfully");
                }

            }
        }
        catch (Exception ex)
        {
            status += ex.Message.ToString();
        }
        return status;
    }

    [WebMethod]
    public List<vmstate> GetstateList(long stateid)
    {
        List<vmstate> state = new List<vmstate>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_STATES";
                cmd.Parameters.AddWithValue("p_Id", stateid);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    state.Add(new vmstate
                    {
                        code = 0,
                        StateName = "Select State",
                        Status = "Success"
                    });
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        state.Add(new vmstate
                        {
                            code = long.Parse(dt.Rows[i]["code"].ToString()),
                            StateName = dt.Rows[i]["StateName"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    state.Add(new vmstate { Status = "Invalid state code" });
                }
            }
        }
        catch (Exception)
        {
            state.Add(new vmstate { Status = "Error" });
        }
        return state;

    }

    [WebMethod]
    public List<vmprog> GetprogramList()
    {
        List<vmprog> program = new List<vmprog>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_LIST_PROGRAM_DETAILS";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    program.Add(new vmprog
                    {
                        programmeId = 0,
                        programmeName = "Select Course",
                        status = "Success"
                    });
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        program.Add(new vmprog
                        {
                            programmeId = long.Parse(dt.Rows[i]["programmeId"].ToString()),
                            programmeName = dt.Rows[i]["programmeName"].ToString(),
                            status = "Success"
                        });
                    }
                }
                else
                {
                    program.Add(new vmprog { status = "Invalid program code" });
                }
            }
        }
        catch (Exception)
        {
            program.Add(new vmprog { status = "Error" });
        }
        return program;

    }

    [WebMethod]
    public List<vmcourse> GetcourseList(long courseid)
    {
        List<vmcourse> course = new List<vmcourse>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_PROGRAM_COURSES";
                cmd.Parameters.AddWithValue("p_Id", courseid);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        course.Add(new vmcourse
                        {
                            CourseId = long.Parse(dt.Rows[i]["CourseId"].ToString()),
                            ProgrammeCode = long.Parse(dt.Rows[i]["ProgrammeCode"].ToString()),
                            CourseName = dt.Rows[i]["CourseName"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    course.Add(new vmcourse { Status = "Invalid program code" });
                }
            }
        }
        catch (Exception)
        {
            course.Add(new vmcourse { Status = "Error" });
        }
        return course;

    }

    [WebMethod]
    public List<vmtheme> GetThemeList()
    {
        List<vmtheme> Theme = new List<vmtheme>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LIST_THEME";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Theme.Add(new vmtheme
                        {
                            ThemeId = long.Parse(dt.Rows[i]["ThemeId"].ToString()),
                            ThemeName = dt.Rows[i]["ThemeName"].ToString(),
                            status = "Success"
                        });
                    }
                }
                else
                {
                    Theme.Add(new vmtheme { status = "Invalid Semname" });
                }
            }
        }
        catch (Exception)
        {
            Theme.Add(new vmtheme { status = "Error" });
        }
        return Theme;

    }

    [WebMethod]
    public String SaveDeviceDetails(String username, String DeviceId, String OSVersion, String Manufacturer, String ModelNo, String SDKVersion, String DeviceSrlNo, String ServiceProvider, String SIMSrlNo, String DeviceWidth, String DeviceHeight, String AppVersion)
    {
        String strStatus = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INSERT_USER_DEVICE_DETAILS";
                cmd.Parameters.AddWithValue("p_Username", username);
                cmd.Parameters.AddWithValue("p_DeviceId", DeviceId);
                cmd.Parameters.AddWithValue("p_OSVersion", OSVersion);
                cmd.Parameters.AddWithValue("p_Manufacturer", Manufacturer);
                cmd.Parameters.AddWithValue("p_ModelNo", ModelNo);
                cmd.Parameters.AddWithValue("p_SDKVersion", SDKVersion);
                cmd.Parameters.AddWithValue("p_DeviceSrlNo", DeviceSrlNo);
                cmd.Parameters.AddWithValue("p_ServiceProvider", ServiceProvider);
                cmd.Parameters.AddWithValue("p_SIMSrlNo", SIMSrlNo);
                cmd.Parameters.AddWithValue("p_DeviceWidth", DeviceWidth);
                cmd.Parameters.AddWithValue("p_DeviceHeight", DeviceHeight);
                cmd.Parameters.AddWithValue("p_AppVersion", AppVersion);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    strStatus = "success";
                else
                    strStatus = "Unable to insert device Details";
            }
        }
        catch (Exception)
        {
            strStatus = "Error";
        }
        return strStatus;
    }

    [WebMethod]


    public List<vmdist> GetDistricts(long stateId)
    {
        List<vmdist> district = new List<vmdist>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_DISTRICTS_ON_STATES";
                cmd.Parameters.AddWithValue("p_Stateid", stateId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    district.Add(new vmdist
                    {
                        DistrictId = 0,
                        Stateid = stateId,
                        DistrictName = "Select District",
                        status = "Success"
                    });
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        district.Add(new vmdist
                        {
                            DistrictId = long.Parse(dt.Rows[i]["DistrictId"].ToString()),
                            Stateid = long.Parse(dt.Rows[i]["Stateid"].ToString()),
                            DistrictName = dt.Rows[i]["DistrictName"].ToString(),
                            status = "Success"
                        });
                    }
                }
                else
                {
                    district.Add(new vmdist { status = "There are no districts" });
                }
            }
        }
        catch (Exception)
        {
            district.Add(new vmdist { status = "Error" });
        }
        return district;
    }

    [WebMethod]
    public List<vmcity> GetCities(long district)
    {
        List<vmcity> city = new List<vmcity>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_CITIES_ON_DISTRICTS";
                cmd.Parameters.AddWithValue("p_DistrictId", district);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    city.Add(new vmcity
                    {
                        Id = 0,
                        District_Id = district,
                        Taluk_Name = "Select City",
                        status = "Success"
                    });

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        city.Add(new vmcity
                        {
                            Id = long.Parse(dt.Rows[i]["Id"].ToString()),
                            District_Id = long.Parse(dt.Rows[i]["District_Id"].ToString()),
                            Taluk_Name = dt.Rows[i]["Taluk_Name"].ToString(),
                            status = "Success"
                        });
                    }
                }
                else
                {
                    city.Add(new vmcity { status = "There are no cities" });
                }
            }
        }
        catch (Exception)
        {
            city.Add(new vmcity { status = "Error" });
        }
        return city;
    }

    [WebMethod]
    public List<vmclg> GetCollegesOnCity(long city, string type)
    {
        List<vmclg> college = new List<vmclg>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_COLLEGES_ON_CITY";
                cmd.Parameters.AddWithValue("p_City", city);
                cmd.Parameters.AddWithValue("p_CollegeType", type);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    college.Add(new vmclg
                    {
                        CollegeId = 0,
                        TalukId = city,
                        College_Name = "Select Institution",// chnaged Select College 5.3
                        Status = "Success"
                    });

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        college.Add(new vmclg
                        {
                            CollegeId = long.Parse(dt.Rows[i]["CollegeId"].ToString()),
                            TalukId = long.Parse(dt.Rows[i]["TalukId"].ToString()),
                            College_Name = dt.Rows[i]["College_Name"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    college.Add(new vmclg { Status = "There are no colleges" });
                }
            }
        }
        catch (Exception)
        {
            college.Add(new vmclg { Status = "Error" });
        }
        return college;
    }

    [WebMethod]
    public List<vmsem> GetSemesteronCourse(long courseId, int stream)
    {
        List<vmsem> semister = new List<vmsem>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                if (stream != 11)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_SELECT_SEMESTERS_ON_COURSE";
                    cmd.Parameters.AddWithValue("p_Code", courseId);

                    cmd.Connection = con;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        semister.Add(new vmsem
                        {
                            SemId = 0,
                            SemName = "Select Semester",
                            Status = "Success"
                        });
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            semister.Add(new vmsem
                            {
                                SemId = long.Parse(dt.Rows[i]["SemId"].ToString()),
                                SemName = dt.Rows[i]["SemName"].ToString(),
                                Status = "Success"
                            });
                        }
                    }
                    else
                    {
                        semister.Add(new vmsem { Status = "There are no semester" });
                    }
                }

                else
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_SELECT_DET_COURSES";
                    cmd.Parameters.AddWithValue("p_FellowshipId", courseId);

                    cmd.Connection = con;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        semister.Add(new vmsem
                        {
                            SemId = 0,
                            SemName = "Select Semester",
                            Status = "Success"
                        });
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            semister.Add(new vmsem
                            {
                                SemId = long.Parse(dt.Rows[i]["SemId"].ToString()),
                                SemName = dt.Rows[i]["SemName"].ToString(),
                                Status = "Success"
                            });
                        }
                    }
                    else
                    {
                        semister.Add(new vmsem { Status = "There are no semester" });
                    }
                }
            }

        }
        catch (Exception)
        {
            semister.Add(new vmsem { Status = "Error" });
        }
        return semister;
    }


    [WebMethod]
    public List<vmcourse> GetCoursesOnProgram(long program)
    {
        List<vmcourse> course = new List<vmcourse>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_COURSES_ON_PROGRAM";
                cmd.Parameters.AddWithValue("p_Id", program);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    course.Add(new vmcourse
                    {
                        CourseId = 0,
                        ProgrammeCode = program,
                        CourseName = "Select Stream",// changed Select course 5.3
                        Status = "Success"
                    });

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        course.Add(new vmcourse
                        {
                            CourseId = long.Parse(dt.Rows[i]["CourseId"].ToString()),
                            ProgrammeCode = long.Parse(dt.Rows[i]["ProgrammeCode"].ToString()),
                            CourseName = dt.Rows[i]["CourseName"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    course.Add(new vmcourse { Status = "There are no Courses" });
                }
            }
        }
        catch (Exception)
        {
            course.Add(new vmcourse { Status = "Error" });
        }
        return course;
    }

    [WebMethod]
    public string SendTestNotification(string deviceId, string title, string message, string imageurls)
    {
        try
        {
            FCMPushNotification.AndroidPush(deviceId, message, title, imageurls);
            return "success";
        }
        catch (Exception)
        {
            return "Error";
        }
    }

    [WebMethod]
    public vmapp GetAppReleasedDetails()
    {
        vmapp app = new vmapp();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_RELEASE_DETAILS";
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        // app.AppVersion = long.Parse(dr["AppVersion"].ToString());
                        app.AppVersion = dr["AppVersion"].ToString();
                        app.versionCode = dr["versionCode"].ToString();
                        app.MissCallNumber = dr["MissCallNumber"].ToString();
                        app.Status = "Success";
                    }
                }
                else
                {
                    app.Status = "Invalid";
                }
            }
        }
        catch (Exception)
        {
            app.Status = "Error";
        }
        return app;
    }

    protected void SendstudentDetails(string bodyString, string MailId, string Title)
    {

        try
        {
            SendHtmlFormattedEmail(MailId.ToString(), Title, bodyString);
        }
        catch (Exception)
        {
            // BLobj.SendMailException("send_sms", ex.ToString(), "StudentProfile.aspx Proposal Submit Mail", cook.LeadId(), cook.Student_MobileNo());
        }
    }

    [WebMethod]
    public vmLogin ValidateLogin1(string Username, string Password)
    {
        vmLogin vmLg = null;
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_VALIDE_USER";
                cmd.Parameters.AddWithValue("p_Username", Username);
                cmd.Parameters.AddWithValue("p_Password", Password);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        vmLg = new vmLogin();
                        vmLg.LoginId = long.Parse(dr["LoginId"].ToString());
                        vmLg.Username = dr["Username"].ToString();
                        vmLg.Role = dr["Role"].ToString();
                        vmLg.isProfileEdit = int.Parse(dr["isProfileEdit"].ToString());
                        vmLg.MobileNo = dr["MobileNo"].ToString();
                        vmLg.College_Name = dr["College_Name"].ToString();
                        vmLg.AcademicId = dr["AcademicId"].ToString();
                        vmLg.Name = dr["Name"].ToString();
                        vmLg.ManagerName = dr["ManagerName"].ToString();
                        vmLg.Lead_Id = dr["Lead_Id"].ToString();
                        vmLg.MailId = dr["MailId"].ToString();
                        vmLg.Location = dr["Location"].ToString();
                        vmLg.RegistrationId = int.Parse(dr["RegistrationId"].ToString());
                        vmLg.ManagerId = int.Parse(dr["ManagerId"].ToString());
                        vmLg.UserImage = dr["Image_Path"].ToString();
                        vmLg.Manager_Image_Path = dr["Manager_Image_Path"].ToString();
                        vmLg.Facebook = dr["Facebook"].ToString();
                        vmLg.Twitter = dr["Twitter"].ToString();
                        vmLg.InstaGram = dr["InstaGram"].ToString();
                        vmLg.Student_Type = dr["Student_Type"].ToString();
                        vmLg.Gender = dr["Gender"].ToString();
                        vmLg.StartCount = int.Parse(dr["NoOfStarts"].ToString());
                        vmLg.Student_Mobile_No = long.Parse(dr["Student_Mobile_No"].ToString());
                        vmLg.isFeePaid = int.Parse(dr["isFeePaid"].ToString());
                        vmLg.WhatsApp = dr["WhatsApp"].ToString();
                        vmLg.isRequestForTShirt = int.Parse(dr["isRequestForTShirt"].ToString());
                        vmLg.isSanctionForTshirt = int.Parse(dr["isSanctionForTshirt"].ToString());
                        vmLg.TotalProjects = int.Parse(dr["TotalProjects"].ToString());

                        if ((vmLg.Student_Type == "Student" || vmLg.Student_Type != "Student") && (vmLg.isRequestForTShirt == 1 && vmLg.isSanctionForTshirt == 2))
                        {
                            vmLg.isRequestForTShirt = 3;

                        }
                        else if ((vmLg.Student_Type == "Student" || vmLg.Student_Type != "Student") && (vmLg.isRequestForTShirt == 0 && vmLg.TotalProjects > 0))
                        {
                            vmLg.isRequestForTShirt = 1;
                            // return 1;
                        }
                        else if ((vmLg.Student_Type == "Student" || vmLg.Student_Type != "Student") && (vmLg.isRequestForTShirt == 1 && vmLg.isSanctionForTshirt == 0))
                        {
                            vmLg.isRequestForTShirt = 1;
                            // return 1;
                        }
                        //else if (vmLg.Student_Type != "Student" && vmLg.isRequestForTShirt == 0)
                        //{
                        //    vmLg.isRequestForTShirt = 1;
                        //}
                        else if (vmLg.isSanctionForTshirt == 1 && vmLg.isRequestForTShirt == 1)
                        {
                            vmLg.isRequestForTShirt = 3;
                        }
                        else if (vmLg.Student_Type != "Student" && vmLg.isRequestForTShirt == 1)
                        {
                            vmLg.isRequestForTShirt = 2;
                        }
                        else
                        {
                            vmLg.isRequestForTShirt = 0;
                        }
                        vmLg.Status = "Success";
                        vmLg.LLP_Badges = int.Parse(dr["LLP_Badges"].ToString());
                        vmLg.Prayana_Badges = int.Parse(dr["Prayana_Badges"].ToString());
                        vmLg.Yuva_Badges = int.Parse(dr["Yuva_Badges"].ToString());
                        vmLg.Valedicotry_Badges = int.Parse(dr["Valedicotry_Badges"].ToString());
                        vmLg.isStudentLEADer = int.Parse(dr["IsStudentLeader"].ToString());
                    }
                }
                else
                {
                    vmLg = new vmLogin();
                    vmLg.Status = "Invalid Username or Password";
                }
            }
        }
        catch (Exception)
        {
            vmLg = new vmLogin();
            vmLg.Status = "Error";
        }
        return vmLg;
    }

    [WebMethod]
    public List<vmNotification> GetTopTenStudentNotificationList(string Lead_Id)
    {
        List<vmNotification> Notification = new List<vmNotification>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_STUDENT_NOTIFICATION_LIST";
                cmd.Parameters.AddWithValue("p_LeadId", Lead_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Notification.Add(new vmNotification
                        {
                            Notification_Type = dt.Rows[i]["Notification_Type"].ToString(),
                            Notification_Message = dt.Rows[i]["Message"].ToString(),
                            Notification_Date = dt.Rows[i]["createDate"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    Notification.Add(new vmNotification { Status = "No Latest Notification" });
                }
            }
        }
        catch (Exception)
        {
            Notification.Add(new vmNotification { Status = "Error" });
        }
        return Notification;
    }

    [WebMethod]
    public List<vmNotification> GetTopTenManagerNotificationList(string ManagerId)
    {
        List<vmNotification> Notification = new List<vmNotification>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_MANAGER_NOTIFICATION_LIST";
                cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Notification.Add(new vmNotification
                        {
                            Notification_Type = dt.Rows[i]["Notification_Type"].ToString(),
                            Notification_Message = dt.Rows[i]["Message"].ToString(),
                            Notification_Date = dt.Rows[i]["createDate"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    Notification.Add(new vmNotification { Status = "No Latest Notification" });
                }
            }
        }
        catch (Exception)
        {
            Notification.Add(new vmNotification { Status = "Error" });
        }
        return Notification;
    }

    [WebMethod]
    public List<vmContact_Us> GetContactUsDetails()
    {
        List<vmContact_Us> CotactUs = new List<vmContact_Us>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SELECT_CONTACTUS";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CotactUs.Add(new vmContact_Us
                        {
                            slno = int.Parse(dt.Rows[i]["slno"].ToString()),
                            Sandbox_Name = dt.Rows[i]["Sandbox_Name"].ToString(),
                            Sandbox_Address = dt.Rows[i]["Sandbox_Address"].ToString(),
                            Contact_Person = dt.Rows[i]["Contact_Person"].ToString(),
                            Contact_Number1 = dt.Rows[i]["Contact_Number1"].ToString(),
                            Contact_Mailid1 = dt.Rows[i]["Contact_Mailid1"].ToString(),
                            Contact_Mailid2 = dt.Rows[i]["Contact_Mailid2"].ToString(),
                            Type = dt.Rows[i]["Type"].ToString(),
                            Priority = int.Parse(dt.Rows[i]["Priority"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    CotactUs.Add(new vmContact_Us { Status = "No Contact Details" });
                }
            }
        }
        catch (Exception)
        {
            CotactUs.Add(new vmContact_Us { Status = "Error" });
        }
        return CotactUs;
    }

    [WebMethod]
    public vmCollegeSummaryCounts GetCollegeCountSummary(string CollegeId, string AcademicId)
    {
        vmCollegeSummaryCounts Summary = new vmCollegeSummaryCounts();
        List<vmCollege_Count_List> Projects = new List<vmCollege_Count_List>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_COLLEGE_LOGIN_SUMMARY_COUNT";
                cmd.Parameters.AddWithValue("p_CollegeId", CollegeId);
                cmd.Parameters.AddWithValue("p_AcademicId", AcademicId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    Summary.Registration_Count = dt.Rows[0]["p_RegistrationCount"].ToString();
                    Summary.Project_Count = dt.Rows[0]["p_ProjectsCount"].ToString();
                    Summary.Funded_Amount = dt.Rows[0]["P_FundingAmount"].ToString();
                }
                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "USP_COLLEGE_LOGIN_5Start_Projects";
                cmd1.Parameters.AddWithValue("p_CollegeId", CollegeId);
                cmd1.Parameters.AddWithValue("p_AcademicId", AcademicId);
                cmd1.Connection = con;
                da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Summary.Status = "Success";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmCollege_Count_List Obj = new vmCollege_Count_List();
                        Obj.Lead_Id = dt.Rows[i]["LEAD_ID"].ToString();
                        Obj.StudentName = dt.Rows[i]["StudentName"].ToString();
                        Obj.ProjectTitle = dt.Rows[i]["title"].ToString();
                        Projects.Add(Obj);
                    }
                    Summary.ProjectList = Projects;
                }
                else
                {
                    Summary.Status = "No Projects";
                }
            }
        }
        catch (Exception)
        {
            Summary.Status = "Error";
        }
        return Summary;
    }

    [WebMethod]
    public List<vmSuggestionFeedbackHead> SuggestionFeedbackHeadsMaster()
    {
        List<vmSuggestionFeedbackHead> SugFeed = new List<vmSuggestionFeedbackHead>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SELECT_SUGGESTION_FEEDBACK";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SugFeed.Add(new vmSuggestionFeedbackHead
                        {
                            Slno = dt.Rows[i]["slno"].ToString(),
                            Head_Name = dt.Rows[i]["head_name"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    SugFeed.Add(new vmSuggestionFeedbackHead { Status = "Invalid program code" });
                }
            }
        }
        catch (Exception)
        {
            SugFeed.Add(new vmSuggestionFeedbackHead { Status = "Error" });
        }
        return SugFeed;

    }
    [WebMethod]
    public String SaveSuggestionFeedback(String Lead_Id, String ManagerId, String HeadId, String Feedback, String Suggestion, String AcademicCode)
    {
        String strStatus = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_INSERT_SUGGESTION_FEEDBACK";
                cmd.Parameters.AddWithValue("p_LeadId", Lead_Id);
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_HeadId", HeadId);
                cmd.Parameters.AddWithValue("p_Feedback", Feedback);
                cmd.Parameters.AddWithValue("p_Suggestion", Suggestion);
                cmd.Parameters.AddWithValue("p_AcademicCode", AcademicCode);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    strStatus = "success";
                    string Mailsend = "select Lead_Id, StudentName,MailId,MobileNo from Student_Registration where lead_id='" + Lead_Id.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        string body = PopulateBody(Lead_Id.ToString(), "<b> Suggestion / Feedback,  Thank you for your Feedback/Suggestion </b>", "The details you entered are listed below:",
                      "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>Student Name</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>Suggestion:</b> " + Suggestion.ToString() + "<br><br></li><li><b>Feedback:</b> " + Feedback.ToString() + "<br><br></li>");

                        SendHtmlFormattedEmail(dt2.Rows[0]["MailId"].ToString(), "Suggestion / Feedback ", body);

                        body = body = PopulateBody(Lead_Id.ToString(), "<b>Student has Given Suggestion / Feedback, " + " " + dt2.Rows[0]["StudentName"].ToString() + " Has Given Feedback/Suggestion </b>", "The details you entered are listed below:",
                      "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>Student Name</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>Suggestion:</b> " + Suggestion.ToString() + "<br><br></li><li><b>Feedback:</b> " + Feedback.ToString() + "<br><br></li>");
                        SendHtmlFormattedEmail("leadmis@dfmail.org", "Suggestion / Feedback ", body);
                        SendHtmlFormattedEmail("abhinandan.k@dfmail.org", "Suggestion / Feedback ", body);
                      


                    }
                }
                else
                {
                    strStatus = "Unable to insert";
                }

            }
        }
        catch (Exception)
        {
            strStatus = "Error";
        }
        return strStatus;
    }

    [WebMethod]
    public List<vmAcademicYear> GetAcademicYearList()
    {
        List<vmAcademicYear> app = new List<vmAcademicYear>();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_SELECT_ACADEMICYEAR_LIST";
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        // app.AppVersion = long.Parse(dr["AppVersion"].ToString());

                        app.Add(new vmAcademicYear
                        {
                            slno = int.Parse(dr["SLNO"].ToString()),
                            AcademicCode = dr["academicCode"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    app.Add(new vmAcademicYear { Status = "There is No Active Academic List" });
                }
            }
        }
        catch (Exception)
        {
            app.Add(new vmAcademicYear { Status = "error" });
        }
        return app;
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



    private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("leadmis@dfmail.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            //mailMessage.To.Add(new MailAddress(recepientEmail));
            // mailMessage.CC.Add(new MailAddress(lblManagerEmailId.Text.ToString()));
            mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
            //mailMessage.Bcc.Add(new MailAddress("sunil.tech@dfmail.org"));


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
    [WebMethod]
    public List<vmLocationBI> LocationBIChart()
    {

        List<vmLocationBI> vmLg = new List<vmLocationBI>();
        DataTable dt = new DataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_LOCATION_BI_CHART";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmLg.Add(new vmLocationBI
                        {
                            College_Name = dt.Rows[i]["College_Name"].ToString(),
                            StateName = dt.Rows[i]["StateName"].ToString(),
                            DistrictName = dt.Rows[i]["DistrictName"].ToString(),
                            Taluk_Name = dt.Rows[i]["Taluk_Name"].ToString(),
                            latitude = dt.Rows[i]["latitude"].ToString(),
                            longitude = dt.Rows[i]["longitude"].ToString(),
                            Registrations = dt.Rows[i]["Registrations"].ToString(),
                            Status = "Success"
                        });

                    }

                }
                else
                {
                    vmLg.Add(new vmLocationBI { Status = "No Data Found" });

                }
            }
        }
        catch (Exception)
        {
            vmLg.Add(new vmLocationBI { Status = "Error" });

        }
        return vmLg;
    }
    [WebMethod]
    public string Uservalidatelogout(String USERID)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_UPDATE_LOGIN_LOG_OUT";
                cmd.Parameters.AddWithValue("P_USERID", USERID);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "Success";
                else
                    status = "Invalid data";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }
    protected void SendstudentDetailss(string bodyString, string MailId, string Title)
    {
        try
        {
            SendHtmlFormattedEmails(MailId.ToString(), Title, bodyString);
        }
        catch (Exception)
        {
            // BLobj.SendMailException("send_sms", ex.ToString(), "StudentProfile.aspx Proposal Submit Mail", cook.LeadId(), cook.Student_MobileNo());
        }
    }



    private void SendHtmlFormattedEmails(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("leadmis@dfmail.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
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
