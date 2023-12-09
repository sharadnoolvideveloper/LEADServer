using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Universal.Standard;
using System.Text.RegularExpressions;
using System.Globalization;

/// <summary>
/// Summary description for DataLL
/// </summary>
public class DataLL
{
    public DataLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();

    UniversalDL DLobj = new UniversalDL();


    public string UpdateStudentProfile(vmStudent_Web vms)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_WEB_UPDATE_STUDENT_PROFILE_NEW";
                cmd.Parameters.AddWithValue("p_RegistrationId", vms.RegistrationId);
                cmd.Parameters.AddWithValue("p_Lead_Id", vms.Lead_Id);
                cmd.Parameters.AddWithValue("p_StudentName", vms.StudentName);
                cmd.Parameters.AddWithValue("p_DOB",vms.DOB); // Convert.ToDateTime(vms.DOB)
                cmd.Parameters.AddWithValue("p_StateCode", vms.StateCode);
                cmd.Parameters.AddWithValue("p_DistrictCode", vms.DistrictCode);
                cmd.Parameters.AddWithValue("p_TalukaCode", vms.TalukaCode);
                cmd.Parameters.AddWithValue("p_CollegeCode", vms.CollegeCode);
                cmd.Parameters.AddWithValue("p_StreamCode", vms.StreamCode);
                cmd.Parameters.AddWithValue("p_CourseCode", vms.CourseCode);
                cmd.Parameters.AddWithValue("p_SemCode", vms.SemCode);
                cmd.Parameters.AddWithValue("p_AadharNo", vms.AadharNo);
                cmd.Parameters.AddWithValue("p_Address", vms.Address);
                cmd.Parameters.AddWithValue("p_MailId",DLobj.DVLS(vms.MailId));
                cmd.Parameters.AddWithValue("p_AlternativeMobileNo", vms.AlternativeMobileNo);
                cmd.Parameters.AddWithValue("p_Gender", vms.Gender);
                cmd.Parameters.AddWithValue("p_BloodGroup",DLobj.DVLS(vms.BloodGroup));
                cmd.Parameters.AddWithValue("p_AcademicCode", vms.AcademicCode);
                cmd.Parameters.AddWithValue("p_Bank_Name",DLobj.DVLS(vms.Bank_Name));
                cmd.Parameters.AddWithValue("p_Branch_Name",DLobj.DVLS(vms.Branch_Name));
                cmd.Parameters.AddWithValue("p_Account_No", vms.Account_No);
                cmd.Parameters.AddWithValue("p_Account_Holder",DLobj.DVLS(vms.AccountHolderName));
                cmd.Parameters.AddWithValue("p_IFSC_code",DLobj.DVLS(vms.IFSC_code));
                cmd.Parameters.AddWithValue("p_MyTalent", vms.MyTalent.ToString());

                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "Profile Updated.";
                else
                    status = "Unable to update Student Profile";
            }
        }
        catch (Exception ex)
        {
            status = "Error" + ex.ToString(); ;
        }
        return status;
    }
    public vmStudent_Web GetStudentDetails(string leadId)
    {
        vmStudent_Web stud = new vmStudent_Web();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_SELECT_STUDENT_DETAILS";
                cmd.Parameters.AddWithValue("p_LeadId", leadId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                                              
                        stud.StudentName = dr["StudentName"].ToString();
                        stud.DOB = dr["DOB"].ToString();
                        stud.isProfileEdit = int.Parse(dr["isProfileEdit"].ToString());
                        stud.RegistrationDate = dr["RegistrationDate"].ToString();
                        stud.StateCode = int.Parse(dr["StateCode"].ToString());
                        stud.DistrictCode = int.Parse(dr["DistrictCode"].ToString());
                        stud.TalukaCode = int.Parse(dr["TalukaCode"].ToString());
                        stud.CollegeCode = int.Parse(dr["CollegeCode"].ToString());
                        stud.StreamCode = int.Parse(dr["streamCode"].ToString());
                        stud.CourseCode = int.Parse(dr["CourseCode"].ToString());
                        stud.SemCode = DLobj.DVLS(dr["SemCode"].ToString());
                        stud.AadharNo = DLobj.DVLL(long.Parse(dr["AadharNo"].ToString()));
                        stud.Address = dr["Address"].ToString();
                        stud.MobileNo =DLobj.DVLL(long.Parse(dr["MobileNo"].ToString()));
                        stud.MailId = DLobj.DVLS(dr["MailId"].ToString());
                        stud.AlternativeMobileNo = DLobj.DVLL(long.Parse(dr["AlternativeMobileNo"].ToString()));
                        stud.Gender = dr["Gender"].ToString();
                        stud.BloodGroup =DLobj.DVLS(dr["BloodGroup"].ToString());
                        stud.Student_Type =DLobj.DVLS(dr["Student_Type"].ToString());
                        stud.Bank_Name =DLobj.DVLS(dr["Bank_Name"].ToString());
                        stud.Branch_Name = DLobj.DVLS(dr["Branch_Name"].ToString());
                        stud.Account_No =dr["Account_No"].ToString();
                        stud.AccountHolderName =DLobj.DVLS(dr["Account_HolderName"].ToString());
                        stud.IFSC_code =DLobj.DVLS(dr["IFSC_code"].ToString());

                        stud.GetDay = dr["Days"].ToString();
                        if ((int.Parse(stud.GetDay) >= 1) && (int.Parse(stud.GetDay) <= 9))
                        {
                            stud.GetDay = int.Parse("0") + stud.GetDay;
                        }
                        stud.GetMonth = dr["Months"].ToString();
                        if ((int.Parse(stud.GetMonth) >= 1) && (int.Parse(stud.GetMonth) <= 9))
                        {
                            stud.GetMonth = int.Parse("0") + stud.GetMonth;
                        }
                        stud.GetYear = dr["Years"].ToString();

                        stud.MyTalent = dr["MyTalent"].ToString();

                    }
                }
                else
                {
                    stud.Status = "Invalid Lead Id";
                }
            }
        }
        catch (Exception ex)
        {
            stud.Status = "Error";
        }
        return stud;
    }

    public void DeleteExistingProfilePic(string LeadId)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SELECT_PROFILEPIC_PATH";
                cmd.Parameters.AddWithValue("p_LeadId", LeadId.ToString());
               
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                   if (dr.Read())
                    {
                        FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dr[0].ToString()));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }
               
            }
        }
        catch (Exception)
        {
           
        }
        
    }
    public void DeleteManagerExistingProfilePic(string ManagerId)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SELECT_ManagerPROFILEPIC_PATH";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId.ToString());

                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dr[0].ToString()));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }

            }
        }
        catch (Exception)
        {

        }

    }
    public string DeleteExistsingDocument(string LeadId,string PDId,string DocumentType,int ImgCount)
    {
        string status = "";
        try
        {
           using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SELECT_DOCUMENT_PATH";
                cmd.Parameters.AddWithValue("p_LeadId",LeadId.ToString());
                cmd.Parameters.AddWithValue("p_PDId",PDId.ToString());
                cmd.Parameters.AddWithValue("p_Document_Type", DocumentType.ToString());
                cmd.Parameters.AddWithValue("p_ImgCount", ImgCount.ToString());
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dr[0].ToString()));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }
                else
                {
                    status = "no record fount";
                }
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;

    }
    public string InsertDigitalDocument(vmDigitalDocument vmd)
    {
        //uploadfile.SaveAs(HttpContext.Current.Server.MapPath(vmd.Document_Path.ToString()));
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INSERT_DIGITAL_DOCUMENTS";
                cmd.Parameters.AddWithValue("p_LeadId",vmd.LeadId);
                cmd.Parameters.AddWithValue("p_PDId", vmd.PDId);
                cmd.Parameters.AddWithValue("p_DocId", vmd.Document_Id);
                cmd.Parameters.AddWithValue("p_DocType", vmd.Document_Type);
                cmd.Parameters.AddWithValue("p_DocPath", vmd.Document_Path);
               
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "Documents Uploaded Successfully.";
                else
                    status = "Unable to Upload documents";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    public void SaveManagerLog(string Lead_Id, string ManagerCode, string Message, string Type)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_INSERT_MANAGER_NOTIFICATION";
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerCode);
                cmd.Parameters.AddWithValue("p_Message", Message);
                cmd.Parameters.AddWithValue("p_type", Type);

                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "Notification added Successfully";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        //  return status;
    }

    public void SaveStudentLog(string Lead_Id,string Message, string Type)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_INSERT_STUDENT_NOTIFICATION";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Message", Message);
                cmd.Parameters.AddWithValue("p_type", Type);

                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "Notification added Successfully";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        //  return status;
    }

    public string UpdateStudentLevelAfterCompletion(string Lead_Id,string Level)
    {
       
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_UPDATE_STUDENT_LEVELS";
                cmd.Parameters.AddWithValue("p_LEAD_ID", Lead_Id);
                cmd.Parameters.AddWithValue("p_Level", Level);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "success";
                else
                    status = "Level Is not updated";
            }
        }
        catch (Exception ex)
        {
            status = "Error";
        }
        return status;
    }

    public int WEB_UpdateProjectCompletions(WEB_StudentProjectCompletion vm)
    {
       
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_WEB_UPDATE_STUDENT_PROJECT_COMPLETION";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Placeofimplement", Regex.Replace(vm.PlaceofImplementation, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_FundsRaised", vm.FundRaised);
                cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(vm.Challenges, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(vm.Learning, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(vm.AsAStory, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_PDId", vm.Pdid);
                cmd.Parameters.AddWithValue("p_LeadId", vm.Lead_Id);
                cmd.Parameters.AddWithValue("p_Resource", Regex.Replace(vm.Resources, "'", "`").Trim());
                cmd.Parameters.AddWithValue("P_HoursSpend", vm.HoursSpent);
                cmd.Parameters.AddWithValue("p_ProjectStatus", vm.Projectstatus);
                cmd.Parameters.AddWithValue("p_Collaboration_Supported", Regex.Replace(vm.Collaboration_Supported, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Permission_And_Activities", Regex.Replace(vm.Permission_And_Activities, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Experience_Of_Initiative", Regex.Replace(vm.Experience_Of_Initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Lacking_initiative", Regex.Replace(vm.Lacking_initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Against_Tide", Regex.Replace(vm.Against_Tide, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Cross_Hurdles", Regex.Replace(vm.Cross_Hurdles, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Entrepreneurial_Venture", Regex.Replace(vm.Entrepreneurial_Venture, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Government_Awarded", Regex.Replace(vm.Government_Awarded, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Leadership_Roles", Regex.Replace(vm.Leadership_Roles, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_CompletionProgress", vm.CompletionProgress.ToString());
                cmd.Parameters.AddWithValue("p_ResourceWorthAmount", vm.ResourcesWorthAmount.ToString());
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                {
                    return 1; //Sucess
                }
                else
                {
                    return 0; // Fail
                }
            }
        }
        catch (Exception ex)
        {
            return 3; // Error
        }
       
    }

    public DataTable Get_Appreciation_StudentList(string FromDate,string ToDate,string College_Id,string Manager_Id,string Star_Level)
    {
        DataTable dt = new DataTable();
        try
        {
            DateTime dtFrom = DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            FromDate = dtFrom.ToString("yyyy-MM-dd");
            DateTime dtTo = DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            ToDate = dtTo.ToString("yyyy-MM-dd");


           
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_APPRECIATION_CERTIFICATE_LISTING";
                cmd.Parameters.AddWithValue("p_From_Date", FromDate.ToString());
                cmd.Parameters.AddWithValue("p_To_Date", ToDate.ToString());
                cmd.Parameters.AddWithValue("p_College_Id", College_Id);
                cmd.Parameters.AddWithValue("p_manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_Star_Level", Star_Level);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public void Insert_Appreciation_Certificate(string Student_Id,string Type, string Project_Count,string User_Id)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_INSERT_APPRECIATION_CERTIFICATE";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Student_Id", Student_Id);
                cmd.Parameters.AddWithValue("p_Type", Type);
                cmd.Parameters.AddWithValue("p_Project_Count", Project_Count);
                cmd.Parameters.AddWithValue("p_User_Id", User_Id);
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "error";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        //  return status;
    }

    public DataTable Get_Appreciation_Send_Certificate(string AcademicYear,string Star_Level,string Sent_Status)
    {
        DataTable dt = new DataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_LIST_APPRECIATION_SEND";
                cmd.Parameters.AddWithValue("p_Academic_Year", AcademicYear.ToString());
                cmd.Parameters.AddWithValue("p_Level", Star_Level.ToString());
                cmd.Parameters.AddWithValue("p_Sent_Status", Sent_Status.ToString());
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    public void Update_Appreciation_Certificate(string Slno, string User_Id)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_UPDATE_APPRECIATION_CERTIFICATE";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_slno", Slno);
                cmd.Parameters.AddWithValue("p_userId", User_Id);             
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "error";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        //  return status;
    }

    public vmLEAD_Login LEAD_LoginValidation(string EmailId)
    {

        vmLEAD_Login vmLg = null;
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_GET_SSO_LOGIN";
                cmd.Parameters.AddWithValue("p_EMAILID", EmailId.ToString());

                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        vmLg = new vmLEAD_Login();
                        vmLg.Manager_Id = dr["ManagerId"].ToString();
                        vmLg.Manager_Name = dr["ManagerName"].ToString();
                        vmLg.Manager_UserName = dr["Manager_LoginId"].ToString();
                        vmLg.Manager_RecordCount = dr["RecordCounts"].ToString();
                        vmLg.Manager_MailId = dr["Mailid"].ToString();
                        vmLg.Manager_AcademicYear = dr["GetTop1AademicCode"].ToString();
                        vmLg.Users_Role = dr["User_Role"].ToString();
                        vmLg.User_Type = dr["UserType"].ToString();
                        vmLg.Status = "Success";
                    }
                }
                else
                {
                    vmLg = new vmLEAD_Login();
                    vmLg.Status = "Invalid Email Id";
                }
            }
        }
        catch (Exception ex)
        {
            vmLg = new vmLEAD_Login();
            vmLg.Status = "Error";
        }
        return vmLg;
    }

    public string Upload_Trainer_Certificate(string Trainer_Name,string Lead_Id,string Email_Id,string Level,int User_Id)
    {
        //uploadfile.SaveAs(HttpContext.Current.Server.MapPath(vmd.Document_Path.ToString()));
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_UPLOAD_TRAINERS_DETAILS";
                cmd.Parameters.AddWithValue("p_Trainer_Name", Regex.Replace(Trainer_Name, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Email_Id", Email_Id);
                cmd.Parameters.AddWithValue("p_Level", Level);
                cmd.Parameters.AddWithValue("p_User_Id", User_Id);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "success";
                else
                    status = "failed";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }
    public DataTable Get_Trainer_Certificate_List(string Academic_Year,string Level)
    {
        DataTable dt = new DataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_GET_TRAINER_CERTIFICATE_GRIDVIEW";
                cmd.Parameters.AddWithValue("p_Academic_Year", Academic_Year.ToString());
                cmd.Parameters.AddWithValue("p_Level", Level.ToString());             
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public DataTable Get_Check_Trainer_Certificate_Exists(string Lead_Id, string Level)
    {
        DataTable dt = new DataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_TRAINER_CERTIFICATE_CHECK_EXISTING";
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id.ToString());
                cmd.Parameters.AddWithValue("p_Level", Level.ToString());
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception ex)
        {
            return dt;
        }
    }


    public string Upload_Trainer_Certificate_Progress(long Slno,int User_Id,int Progress)
    {
        //uploadfile.SaveAs(HttpContext.Current.Server.MapPath(vmd.Document_Path.ToString()));
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_UPDATE_TRAINER_CERTIFICATE_GENERATED";
                cmd.Parameters.AddWithValue("p_Slno", Slno);
                cmd.Parameters.AddWithValue("p_User_Id", User_Id);
                cmd.Parameters.AddWithValue("p_Progress", Progress);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "success";
                else
                    status = "failed";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }
}