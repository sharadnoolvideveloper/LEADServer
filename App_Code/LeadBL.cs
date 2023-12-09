using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Universal.Standard;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Globalization;
using DocumentFormat.OpenXml.Spreadsheet;

/// <summary>
/// Summary description for LeadBL
/// </summary>
public class LeadBL
{


    public LeadBL()
    {

        //
        // TODO: Add constructor logic here
        //
    }

    UniversalDL DLobj = new UniversalDL();
    string gstrQrystr = "";
    public char[] trimChar = { '*', '!', '%', '&', ',', '\r', '\n' };

    //---------Login
    //public void ClearTextBoxes(ControlCollection ctrls)
    //{
    //    (Control ctrl in ctrls)
    //    {
    //        if (ctrl is TextBox)
    //            ((TextBox)ctrl).Text = string.Empty;
    //        ClearTextBoxes(ctrl.Controls);
    //    }
    //}

    public string Encrypt(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = System.Text.Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;

    }
    public string Decrypt(string encryptpwd)
    {

        string decryptpwd = string.Empty;


        System.Text.UTF8Encoding encodepwd = new System.Text.UTF8Encoding();

        System.Text.Decoder Decode = encodepwd.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
        int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        decryptpwd = new String(decoded_char);

        return decryptpwd;
    }
    public bool IsValidEmail(string email)
    {
        const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);
        return regex.IsMatch(email);
    }


    public vmForgotPassword ForgotPassword(string MailId)
    {
        vmForgotPassword FP = new vmForgotPassword();
        gstrQrystr = "select ManagerId,ManagerName,MailId from manager_details where mailid='" + MailId.ToString() + "' and status=1";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            FP.Name = dt.Rows[0].ItemArray[1].ToString();
            FP.ExistingMailId = dt.Rows[0].ItemArray[2].ToString();
            gstrQrystr = "select UserName,Password from users where Reference_Id=" + dt.Rows[0].ItemArray[0].ToString() + " and Role='Manager'";

            DataTable dt1 = DLobj.GetDataTable(gstrQrystr);
            if (dt1.Rows.Count > 0)
            {
                FP.UserID = dt1.Rows[0].ItemArray[0].ToString();
                FP.Password = dt1.Rows[0].ItemArray[1].ToString();
            }
            else
            {
                FP.Status = "Not Exists";
            }

        }
        else
        {
            gstrQrystr = "select RegistrationId,StudentName,mailid from Student_Registration where mailid='" + MailId.ToString() + "' and ActiveStatus=1";
            dt = DLobj.GetDataTable(gstrQrystr);
            if (dt.Rows.Count > 0)
            {
                FP.Name = dt.Rows[0].ItemArray[1].ToString();
                FP.ExistingMailId = dt.Rows[0].ItemArray[2].ToString();
                gstrQrystr = "select UserName,Password from users where Reference_Id=" + dt.Rows[0].ItemArray[0].ToString() + " and Role='Student'";

                DataTable dt1 = DLobj.GetDataTable(gstrQrystr);
                if (dt1.Rows.Count > 0)
                {
                    FP.UserID = dt1.Rows[0].ItemArray[0].ToString();
                    FP.Password = dt1.Rows[0].ItemArray[1].ToString();
                }
                else
                {
                    FP.Status = "Not Exists";
                }
            }
            else
            {
                FP.Status = "Not Exists";
            }
        }

        return FP;


    }

    public void Student_Year(DropDownList ddl)
    {
        gstrQrystr = "select slno,years from mstr_years where Status=1 order by years Asc";
        DLobj.FillDDLWithYear(ddl, gstrQrystr, "slno", "years");
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

    public void Manager_UpdateUnPaidToPaid(string Managerid, string LeadId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update Student_Registration set isFeePaid=1 where ManagerCode=" + Managerid.ToString() + " and Lead_Id='" + LeadId.ToString() + "' and isFeePaid=0 and RegistrationId<>0 ";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Manager_UpdatePaidToUnPaid(string Managerid, string LeadId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update Student_Registration set isFeePaid=0 where ManagerCode=" + Managerid.ToString() + " and Lead_Id='" + LeadId.ToString() + "' and isFeePaid=1 and RegistrationId<>0 ";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Student_SendRequestToManager(string Lead_Id, string StudentName, string MobileNo, string StudentRequest, string StudentMailId, string StateName, string DistrictName, string TalukaName, string CollegeName, string ManagerMailId, string ManagerMobileNo, string ManagerId)
    {

        gstrQrystr = "INSERT INTO student_request(Lead_Id,StudentName, Student_MobileNo,Email_id,State_Name,District_Name,Taluka_Name,College_Name, Message, Requestdate)";
        gstrQrystr = gstrQrystr + "values('" + Lead_Id.ToString() + "',N'" + Regex.Replace(StudentName.ToString(), "'", "`").Trim() + "','" + MobileNo.ToString() + "','" + StudentMailId.ToString() + "' ,N'" + Regex.Replace(StateName.ToString(), "'", "`").Trim() + "',N'" + Regex.Replace(DistrictName.ToString(), "'", "`").Trim() + "',N'" + Regex.Replace(TalukaName.ToString(), "'", "`").Trim() + "',N'" + Regex.Replace(CollegeName.ToString(), "'", "`").Trim() + "', N'" + Regex.Replace(StudentRequest.ToString(), "'", "`").Trim() + "', Now())";
        DLobj.ExecuteQuery(gstrQrystr);

        string Subject = "Thank you for Request";

        string MaildBody = PopulateBody(StudentMailId.ToString(),
       "<b>thank you for Request (WEB) </b>", "",
       "Request is raised by Lead Id is " + Lead_Id.ToString() + " " + System.DateTime.Now.ToString() + " " +
       "<table><tr><td>" + " " +
       "<b style='color:red'>Student Request to Manager</b>" + " " +
       "<ol>" + " " +
       "<li><b>Student Name :</b> " + StudentName.ToString() + "<br /><br /></li>" + " " +
       "<li><b>Mobile No :</b> " + MobileNo.ToString() + "<br /><br /></li> " + " " +
       "<li><b>State Name :</b> " + StateName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>District Name :</b> " + DistrictName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>Taluka Name :</b> " + TalukaName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>College Name :</b> " + CollegeName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>Other  :</b> " + StudentRequest.ToString() + "<br /><br /></li></ol></td></tr></table><br /><br />");

        SendMailGeneral(MaildBody.ToString(), Subject.ToString(), StudentMailId.ToString(), false);

        MaildBody = "";

        MaildBody = PopulateBody(ManagerMailId.ToString(),
       "<b>Dear Admin Request is Generated By student Please Respond to Student Request (WEB) </b>", "",
       "Request is raised by Lead Id is " + Lead_Id.ToString() + " " + System.DateTime.Now.ToString() + " " +
       "<table><tr><td>" + " " +
       "<b style='color:red'>Student Details as below</b>" + " " +
       "<ol>" + " " +
       "<li><b>Student Name :</b> " + StudentName.ToString() + "<br /><br /></li>" + " " +
       "<li><b>Mobile No :</b> " + MobileNo.ToString() + "<br /><br /></li> " + " " +
       "<li><b>State Name :</b> " + StateName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>District Name :</b> " + DistrictName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>Taluka Name :</b> " + TalukaName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>College Name :</b> " + CollegeName.ToString() + "<br /><br /></li> " + " " +
       "<li><b>Other  :</b> " + StudentRequest.ToString() + "<br /><br /></li></ol></td></tr></table><br /><br />");
        SendMailGeneral(MaildBody.ToString(), Subject.ToString(), ManagerMailId.ToString(), true);

        string DeviceID = Common_GetDeviceID(Lead_Id.ToString());
        if (DeviceID != "")
        {
            FCMPushNotification.AndroidPush(DeviceID.ToString(), Lead_Id + "-" + "Resquest sent to a Manager", "Request", "Empty");
        }

        DeviceID = Manager_GetDeviceID(ManagerId.ToString());
        if (DeviceID != "")
        {
            FCMPushNotification.AndroidPush(DeviceID.ToString(), Lead_Id.ToString() + "-" + "Has raised a query", "Manager", "Empty");
        }
        Manager_SaveNotificationLog(ManagerId.ToString(), Lead_Id.ToString(), "Student Requested " + " " + StudentRequest.ToString(), "Student Request", "");

    }
    public void SendGeneralSMS(string message, string number)
    {

        try
        {

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.campusconnect.co/api/v4/?method=sms&api_key=A6a2f9a0287c81d9dfa2c15cdfb489987&to=" + number + "&sender=LCLEAD&message=" + message);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            //lbl_submission.Text += "Message Sent Successfully";
        }
        catch (Exception)
        {
            // LogException(ex, "SendSMS");

            //throw new FaultException(ex.Message);
        }

    }
    public string PopulateBody(string userName, string title, string url, string description)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Pages/EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);
        body = body.Replace("{Title}", title);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Description}", description);
        return body;
    }
    private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body, string ManagerMailId)
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
    public void SendMailGeneral(string Message, string Subject, string Email, bool isbcc)
    {
        try
        {


            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("leadmis@dfmail.org");
                mailMessage.Subject = Subject;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(Email));
                //mailMessage.CC.Add(new MailAddress(lblManagerEmailId.Text.ToString()));
                mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
                if (isbcc == true)
                {


                    mailMessage.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
                }

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
        catch (Exception)
        {

        }

    }
    public string Common_GetDeviceID(string LeadId)
    {
        gstrQrystr = "select distinct DeviceId,username from user_device_details where username='" + LeadId.ToString() + "' order by id desc limit 1";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            return "";
        }
    }
    public string Manager_GetDeviceID(string ManagerId)
    {
        gstrQrystr = "select distinct DeviceId,username from user_device_details where username=(Select MobileNo from Manager_Details where ManagerId=" + ManagerId.ToString() + ")";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            return "";
        }
    }
    public string UserRole(string strUsername, string strPassword)
    {

        gstrQrystr = "select role,status from users where  username='" + strUsername + "' and password='" + strPassword + "'";
        //  MySqlDataReader objDR = (MySqlDataReader)(DLobj.GetReader(gstrQrystr));

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0].ItemArray[1].ToString() == "0")
            {
                return "Deactive";
            }
            else
            {
                return dt.Rows[0].ItemArray[0].ToString();
            }

        }
        else
        {
            return "";
        }
    }
    public string CollegeLogin(string strUserName, string strPassword)
    {

        gstrQrystr = "select collegeid,isLogInActive from colleges where College_Mailid='" + strUserName + "' and College_Password='" + strPassword + "'";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0].ItemArray[1].ToString() == "0")
            {
                return "Deactive";
            }
            else
            {
                return dt.Rows[0].ItemArray[0].ToString();
            }

        }
        else
        {
            return "";
        }
    }
    public DataTable CollegeLogin_MentorDetails(string CollegeId)
    {
        gstrQrystr = "select CollegeId,College_Name,College_Type,MD.managerid,MD.ManagerName,MD.MobileNo,MD.MailId," + " " +
        "MD.Image_Path,MD.facebook,MD.Twitter,MD.InstaGram,MD.WhatsApp from colleges as CLG," + " " +
        "manager_details as MD,manager_colleges as MC where isLogInActive = 1 and CLG.CollegeId = MC.CollegeCode and MD.ManagerId = MC.ManagerCode" + " " +
        "and CollegeId = " + CollegeId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);

    }
    public string GetRatingStarts(string Rating)
    {
        gstrQrystr = "select RatingStar from mstr_rating where Rating=" + Rating.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }
    public string CheckStudentExist(string strUsername, string strPassword)
    {

        //gstrQrystr = "select username from users where  username='" + strUsername + "' and password='" + strPassword + "' and role='Student'";
        gstrQrystr = "select users.username from users Inner Join Student_registration on Users.UserName = Student_registration.Lead_Id" + " " +
        "where Users.Username = '" + strUsername + "' and users.password = '" + strPassword + "' and Student_registration.ActiveStatus = 1 and Users.role = 'Student'"; ;

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }
    public int CheckProfileisEditedMode(string LeadId)
    {
        int isEdit = 0;

        gstrQrystr = "select isProfileEdit,RegistrationId from Student_registration where Lead_Id='" + LeadId + "' and ActiveStatus=1";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            isEdit = int.Parse(dt.Rows[0].ItemArray[0].ToString());
            return isEdit;
        }
        else
        {
            isEdit = 0;
        }

        return isEdit;

    }
    public vmManager CheckManagerExist(string strUsername, string strPassword, vmManager mng)
    {
        gstrQrystr = "select Reference_Id from users where UserName='" + strUsername.ToString() + "' and password='" + strPassword.ToString() + "' and Role<>'Student'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            gstrQrystr = "select ManagerId,ManagerName,RecordCounts,ifnull(mailid,'') as Mailid,UserType from manager_details where ManagerId=" + dt.Rows[0].ItemArray[0].ToString() + "";
            dt = DLobj.GetDataTable(gstrQrystr);
            if (dt.Rows.Count > 0)
            {
                mng.ManagerID = dt.Rows[0]["ManagerId"].ToString();
                mng.ManagerName = dt.Rows[0]["ManagerName"].ToString();
                mng.RecordCount = int.Parse(dt.Rows[0]["RecordCounts"].ToString());
                mng.MailId = dt.Rows[0]["Mailid"].ToString();
                mng.User_Type = dt.Rows[0]["UserType"].ToString();

            }

        }
        return mng;
    }
    public string GetTop1AademicCode()
    {
        gstrQrystr = "SELECT Slno FROM academicyear WHERE (Status = 1) ORDER BY slno DESC limit 1";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }

    public string getprogramid(string p_userid)
    {
        gstrQrystr = "SELECT program_id FROM user_programs WHERE (user_id = " + p_userid.ToString() + ")";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }

    public string get_managerprogramid(string p_userid)
    {
        gstrQrystr = "SELECT Program_Id FROM manager_details WHERE (ManagerId = " + p_userid.ToString() + ")";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }

    public string Student_GetManagerIdAfterProfileUpdate(string LeadId)
    {
        gstrQrystr = "SELECT ManagerCode FROM student_registration WHERE (Lead_Id = '" + LeadId.ToString() + "')";
        // gstrQrystr = "select username from users inner join manager_details on users.username=manager_details.MailId where  username='" + strUsername + "' and password='" + strPassword + "' and role<>'Student' and Status=1";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }
    public string Student_GetManagerIdBeforeProfileUpdate()
    {
        gstrQrystr = "SELECT ManagerId FROM manager_details WHERE (Status = 1) and (DefaultFlag=1)";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }


        return "";
    }

    public int Student_GetRegistrationId(string LeadId)
    {
        int RegId = 0;

        gstrQrystr = "select RegistrationId from Student_registration where Lead_Id='" + LeadId + "' and ActiveStatus=1";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            RegId = int.Parse(dt.Rows[0].ItemArray[0].ToString());
            return RegId;
        }
        return RegId;




    }
    public void GetDatesFromAcademicYear(string AcademicCode, out string FromDate, out string ToDate)
    {
        gstrQrystr = "Select DATE_FORMAT(FromDate,'%Y-%m-%d'),DATE_FORMAT(ToDate,'%Y-%m-%d') from AcademicYear where slno='" + AcademicCode.ToString() + "' and Status=1";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            FromDate = dt.Rows[0].ItemArray[0].ToString();
            ToDate = dt.Rows[0].ItemArray[1].ToString();
        }
        else
        {
            FromDate = "";
            ToDate = "";
        }



    }
    public string CheckManagerForStudent(string LeadId)
    {
        gstrQrystr = "select ManagerCode from student_registration where Lead_Id='" + LeadId.ToString() + "'";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }

    //----Common
    public void FillAademicYear(DropDownList ddl)
    {
        gstrQrystr = "Select Distinct slno,AcademicCode from AcademicYear where Status=1 order by slno desc";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "slno", "AcademicCode");
    }


    public void Fillprogram(DropDownList ddl, string Adminid)
    {
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();
        /* gstrQrystr = "Select Distinct slno,AcademicCode from AcademicYear where Status=1 order by slno desc";
         DLobj.FillDDLWithSelect(ddl, gstrQrystr, "slno", "AcademicCode");*/
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            conn.Open();
            using (MySqlCommand command = new MySqlCommand("USP_FILLPROGRAM", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("p_user_id", Adminid);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    ddl.DataSource = dt;
                    ddl.DataTextField = "program_name";
                    ddl.DataValueField = "program_id";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("[Select]", ""));
                }

            }
        }
    }

    public void FillAademicYearSelect(DropDownList ddl)
    {
        gstrQrystr = "Select Distinct slno,AcademicCode from AcademicYear where Status=1 order by slno desc";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "slno", "AcademicCode");
    }
    public void FillStateMaster(DropDownList ddl)
    {
        gstrQrystr = "select distinct code,StateName from mstr_state order by StateName asc";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "code", "StateName");
    }

    public DataTable GetStateMaster()
    {
        gstrQrystr = "select distinct code,StateName from mstr_state where status=1 order by StateName asc";
        return DLobj.GetDataTable(gstrQrystr);
    }



    //-------Admin_Manager

    public void Admin_FillManagerList(Repeater rpt, string P_programid)
    {
        // gstrQrystr = "select ManagerId,ManagerName,mstr_state.StateName from manager_details" + " " +
        // "inner join mstr_state on manager_details.StateCode = mstr_state.code where mstr_state.Status = 1 and manager_details.Status = 1 and manager_details.isApplicableforMail=1 order by ManagerName asc";

        gstrQrystr = "select distinct ManagerId,ManagerName,program_id,mstr_state.StateName from manager_details inner join mstr_state on manager_details.StateCode = mstr_state.code where mstr_state.Status = 1 and manager_details.Status = 1 and manager_details.isApplicableforMail=1 and program_id = " + P_programid.ToString() + " order by ManagerName asc";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void Admin_FillManagerForDetailsSending(Repeater rpt, string Sandbox)
    {
        if (Sandbox.ToString() == "All")
        {
            gstrQrystr = "select distinct managerid,managername,MobileNo,MailId from manager_details where Status=1 order by ManagerName";
        }
        else
        {
            gstrQrystr = "select distinct managerid,managername,MobileNo,MailId from manager_details where Status=1 and Sandbox='" + Sandbox.ToString() + "' order by ManagerName";
        }
        rpt.DataSource = DLobj.GetDataSet(gstrQrystr);
        rpt.DataBind();
    }
    public void Admin_FillDistrict(Repeater rpt, string StateCode)
    {
        gstrQrystr = "select Distinct DistrictId,DistrictName from mstr_district where Stateid=" + StateCode.ToString() + " and  Status=1 order by DistrictName";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void Admin_FillTaluka(Repeater rpt, string DistrictCode)
    {
        gstrQrystr = "select Distinct Id,taluk_name from mstr_taluka where District_Id in (" + DistrictCode.ToString().Trim() + ") and  Status=1 order by taluk_name";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }

    public void Admin_fillCollegeByTaluka(Repeater rpt, string Taluka)
    {
        gstrQrystr = "select CollegeId,College_Name,taluk_name,Colleges.TalukId from Colleges inner join mstr_taluka on Colleges.TalukId=mstr_taluka.id where Colleges.status=1 Colleges.TalukId in(" + Taluka.ToString().Trim() + ") order by Colleges.TalukId,College_Name";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void Admin_fillCollegeFromManagerWise(Repeater rpt, string ManagerId)
    {
        gstrQrystr = "SELECT CLG.CollegeId, CLG.College_Name,CLG.talukId, mstr_taluka.Taluk_Name FROM colleges AS CLG INNER JOIN manager_colleges AS MC ON CLG.CollegeId = MC.CollegeCode ";
        gstrQrystr = gstrQrystr + "INNER JOIN mstr_taluka ON CLG.TalukId = mstr_taluka.Id WHERE (MC.ManagerCode = " + ManagerId.ToString() + ") order by CLG.College_Name asc";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void Admin_FillManagerDropDownForCollegeTransfer(string ManagerId, DropDownList ddl)
    {
        gstrQrystr = "select distinct ManagerId,managername from manager_details where managerid<>" + ManagerId.ToString() + "";
        DLobj.FillDDL(ddl, gstrQrystr, "ManagerId", "ManagerName");
    }
    public void Admin_Fillprogramddl(DropDownList ddl, string p_userid)
    {
        gstrQrystr = "select master_programs.program_id,program_name from master_programs inner join user_programs as up on(master_programs.program_id = up.program_id) where  up.user_id = " + p_userid.ToString() + " ";
        DLobj.FillDDL(ddl, gstrQrystr, "program_id", "program_name");
    }

    public void Admin_FillManagerByprogram(string P_programId, DropDownList ddl)
    {
        gstrQrystr = "select distinct ManagerId,managername,md.program_Id from manager_details as md inner join manager_colleges as mc on  md.ManagerId = mc.ManagerCode  inner join college_programs as cp on mc.CollegeCode = cp.college_id where isApplicableforMail=1 and  md.program_id =" + P_programId.ToString() + " order by managername";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "ManagerId", "ManagerName");
    }


    public void Admin_FillManagerddl(DropDownList ddl, string P_userid)
    {
        /*  gstrQrystr = "select distinct ManagerId,managername from manager_details"+" "+
          "where  status=1 and isApplicableforMail=1 order by managername";*/
        gstrQrystr = "select distinct ManagerId,ManagerName,mstr_state.StateName from manager_details inner join mstr_state on manager_details.StateCode = mstr_state.code inner join manager_colleges as mc on  manager_details.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id inner join user_programs as up on cp.program_id = up.program_id where mstr_state.Status = 1 and manager_details.Status = 1 and manager_details.isApplicableforMail=1 and up.user_id = " + P_userid.ToString() + " order by ManagerName asc";

        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "ManagerId", "ManagerName");
    }
    public void Admin_TransfterCollegesToNewManager(string CollegeId, string FromManagerId, string ToManagerId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update manager_colleges SET ManagerCode=" + ToManagerId.ToString() + " where ManagerCode=" + FromManagerId.ToString() + " and slno<>0 and CollegeCode in (" + CollegeId.ToString() + ")";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0; Update Student_registration set ManagerCode=" + ToManagerId.ToString() + " where managercode=" + FromManagerId.ToString() + " and CollegeCode in (" + CollegeId.ToString() + ")  and RegistrationId<>0";
        DLobj.ExecuteQuery(gstrQrystr);

        // gstrQrystr = "Update Project_Description set ManagerId=" + ToManagerId.ToString() + " Where ManagerId=" + FromManagerId.ToString() + "";

        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE Project_Description as PD  INNER JOIN Student_Registration as SR  ON PD.Student_Id = SR.RegistrationId " + " " +
        "SET PD.ManagerId = " + ToManagerId.ToString() + "  where  SR.CollegeCode in (" + CollegeId.ToString() + ") and PD.ManagerId = " + FromManagerId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public string Admin_CheckCollegeExists(string CollegeCode)
    {
        gstrQrystr = "Select Colleges.College_Name from Manager_Colleges inner join Colleges on Manager_Colleges.CollegeCode=Colleges.CollegeId" + " " +
        "where CollegeCode='" + CollegeCode.ToString() + "'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";


    }
    public DataTable Admin_GetLatestTopEventForNotification()
    {
        gstrQrystr = "select EventId,EventName,REPLACE(Image_path, '~', '') as Image_path from mstr_events where Status=1 order by EventId desc Limit 1";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Admin_GetEventForNotification(string EventId)
    {
        gstrQrystr = "select EventId,EventName,REPLACE(Image_path, '~', '') as Image_path from mstr_events where EventId=" + EventId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Admin_GetDeviceIdForSendingEventAndStoriesNotification()
    {

        gstrQrystr = "Select Distinct DeviceId,username from user_device_details  order by id desc";
        // gstrQrystr = "Select Distinct DeviceId,username from user_device_details where username in ('MI00001','MH05102','MI00763','MI00756','MI00755')";
        // gstrQrystr = "select Distinct DeviceId,UserName from user_device_details where username in('MI00001','MH08103','MD1726','ME06225','MH05102','MI00040','MG00367','ME01138','MG00019','9900035214','MI00763','MI00756','MI00755','9900053763','9591294870','MI00019','MI00022','MI00023','MI00024','MI00027')";
        // gstrQrystr = "select Distinct DeviceId,UserName from user_device_details where username in('MI00001','MH05102','MH08103','MI00763')";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Admin_GetLeadStoryForNotification(string StoryId)
    {
        gstrQrystr = "select Story_Title,Story_Type,status,REPLACE(Image_Path, '~', '') as Image_path,slno from mstr_lead_story where slno=" + StoryId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public string Admin_SaveManagerDetail(TextBox txtName, TextBox txtMailId, TextBox MobileNo, TextBox Address, DropDownList ddlGender, DropDownList BloodGroup, DropDownList ddlStateCode, int ManagerId)
    {
        string ManagernewId = "";
        if (ManagerId == 0)
        {
            gstrQrystr = "Insert into Manager_Details(ManagerName,StateCode,Address,MobileNo,MailId,Gender,BloodGroup)" + " " +
            "values ('" + Regex.Replace(txtName.Text.ToString(), "'", "`").Trim() + "','" + ddlStateCode.SelectedValue.ToString() + "','" + Address.Text.ToString() + "','" + MobileNo.Text.ToString().Trim() + "','" + txtMailId.Text.ToString().Trim() + "','" + ddlGender.SelectedValue.ToString() + "','" + BloodGroup.SelectedValue.ToString() + "')";
            DLobj.ExecuteQuery(gstrQrystr);

            gstrQrystr = "select ManagerId from Manager_details where MailId='" + txtMailId.Text.ToString().Trim() + "' and MobileNo='" + MobileNo.Text.ToString().Trim() + "'";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            if (dt.Rows.Count > 0)
            {
                ManagernewId = dt.Rows[0].ItemArray[0].ToString();
            }

            gstrQrystr = "Insert into users(UserName,Password,Role,Reference_Id)" + " " +
            "Values ('" + MobileNo.Text.ToString().Trim() + "','" + MobileNo.Text.ToString().Trim() + "','Manager'," + ManagernewId.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);

            return ManagernewId.ToString();
        }
        else
        {
            ManagernewId = ManagerId.ToString();
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; update Manager_Details set ManagerName='" + Regex.Replace(txtName.Text.ToString(), "'", "`").Trim() + "',Address='" + Regex.Replace(Address.Text.ToString(), "'", "`").Trim() + "',Gender='" + ddlGender.SelectedValue.ToString() + "',BloodGroup='" + BloodGroup.SelectedValue.ToString() + "' where ManagerId=" + ManagernewId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);

        }
        return ManagernewId.ToString();

    }

    public DataTable Admin_GET_Tshirt_Allotment(string AcademicYear)
    {
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();
        using (MySqlConnection con = new MySqlConnection(connection))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WEB_TSHIRT_ALLOTMENT";
            cmd.Parameters.AddWithValue("U_AcademicCode", AcademicYear.ToString());
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }


    public void Admin_SaveManagerCollege(string ManagerCode, string TalukaCode, string CollegeCode)
    {
        gstrQrystr = "insert into Manager_Colleges(ManagerCode,TalukaCode,CollegeCode,CreatedBy,CreateDate)" + " " +
        "Values(" + ManagerCode.ToString() + "," + TalukaCode.ToString() + "," + CollegeCode.ToString() + ",'1',Now())";
        DLobj.ExecuteQuery(gstrQrystr);
    }


    //----Student-----------

    public void Student_FillDistrict(string StateCode, DropDownList ddl)
    {
        gstrQrystr = "select Distinct DistrictId,DistrictName from mstr_district where Stateid=" + StateCode.ToString() + " and  Status=1 order by DistrictName";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "DistrictId", "DistrictName");
    }
    public void Student_FillTaluka(string DistrictCode, DropDownList ddl)
    {
        gstrQrystr = "select Distinct Id,taluk_name from mstr_taluka where District_Id in (" + DistrictCode.ToString().Trim() + ") and  Status=1 order by taluk_name";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "Id", "taluk_name");
    }
    public void Student_fillCollegeByTaluka(string Taluka, string Stream, DropDownList ddl)
    {
        gstrQrystr = "select distinct CollegeId,College_Name,taluk_name from Colleges inner join mstr_taluka on Colleges.TalukId=mstr_taluka.id where College.status=1 and Colleges.TalukId in(" + Taluka.ToString().Trim() + ") and College_Type='" + Stream.ToString() + "' order by Colleges.TalukId,College_Name";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "CollegeId", "College_Name");
    }

    public void Student_FillProgramme(DropDownList ddl)
    {
        gstrQrystr = "select Distinct ProgrammeId,programmeName from mstr_programme where status=1 order by programmename";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "ProgrammeId", "programmeName");
    }
    public void Student_FillProgrammeCourse(string ProgrammeCode, DropDownList ddl)
    {
        gstrQrystr = "select distinct courseid,coursename from mstr_programme_course where status=1 and ProgrammeCode=" + ProgrammeCode.ToString() + " order by coursename";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "courseid", "coursename");
    }
    public void Student_FillSemester(DropDownList ddl, string TotalSem)
    {
        gstrQrystr = "select SemId,SemName from mstr_semester where Status=1 and isDET=0 order by SemName Limit " + TotalSem.ToString();
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "SemId", "SemName");
    }
    public void Student_FillSemesterForDET(DropDownList ddl, string FellowshipId, string CentreId)
    {
        gstrQrystr = "select SemId,SemName from mstr_semester where Status=1 and isDET=1 and status=1 and Fellowship_id=" + FellowshipId.ToString() + " and CentreId=" + CentreId.ToString() + " order by SemName";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "SemId", "SemName");
    }
    public string Student_GetSemesterByCentreId(DropDownList ddl)
    {
        gstrQrystr = "SELECT CentreId FROM mstr_taluka where id=" + ddl.SelectedValue.ToString() + " and Status=1";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt.Rows[0].ItemArray[0].ToString();
    }
    public string Student_GetCourseTotalSem(string CourseCode)
    {
        if (CourseCode != "")
        {
            gstrQrystr = "select ifnull(TotalSemesters,0) as TotalSemesters from mstr_programme_course where courseId=" + CourseCode.ToString() + "";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0].ItemArray[0].ToString();
            }
        }
        else
        {
            return "";
        }

        return "";

    }



    //------------Project Related------------

    public string Student_SaveProjectProposal(string Lead_Id, string ManagerId, string RegistrationId, string ProjectTitle, string Beneficiaries, string ProjectObjectives, string ProjectPlan, string ProposedDate, string AcademicYear, string ProjectType, string PlaceofImplement, string WhoareBeneficiaries, string CurrentSituation, string ProjectStartDate, string ProjectEndDate)
    {
        gstrQrystr = "insert into project_description(Lead_Id,ManagerId,Student_Id,Title,BeneficiaryNo,Objectives,ActionPlan,ProposedDate,ProjectStatus,AcademicCode,theme,Edited_By,Created_Date,Edited_Date,Beneficiaries,Placeofimplement,DeviceType,CurrentSituation,ProjectStartDate,ProjectEndDate)" + " " +
        "value('" + Lead_Id.ToString() + "','" + ManagerId.ToString() + "'," + RegistrationId.ToString() + ",N'" + Regex.Replace(ProjectTitle, "'", "`").Trim() + "'," + Regex.Replace(Beneficiaries, "'", "`").Trim() + ",N'" + Regex.Replace(ProjectObjectives, "'", "`").Trim() + "',N'" + Regex.Replace(ProjectPlan, "'", "`").Trim() + "',NOW(),'Proposed','" + AcademicYear.ToString() + "'," + ProjectType.ToString() + "," + RegistrationId.ToString() + ",Now(),Now(),'" + Regex.Replace(WhoareBeneficiaries, "'", "`").Trim() + "','" + Regex.Replace(PlaceofImplement, "'", "`").Trim() + "','WEB','" + Regex.Replace(CurrentSituation, "'", "`").Trim() + "','" + ProjectStartDate.ToString() + "','" + ProjectEndDate.ToString() + "')";
        DLobj.ExecuteQuery(gstrQrystr);
        Student_InsertNotification(Lead_Id.ToString(), ProjectTitle.ToString(), "Proposed Project (Student)");
        gstrQrystr = "SELECT  PDId FROM project_description WHERE  (Lead_Id = '" + Lead_Id.ToString() + "') AND (Title = N'" + Regex.Replace(ProjectTitle, "'", "`").Trim() + "') order by PdId desc";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        return "";

    }
    public void Student_UpdateManagerIdInProjectAfterProfileUpdate(string StudentId, string ManagerId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update project_description set ManagerId=" + ManagerId.ToString() + ",Edited_Date=Now(),Edited_By=" + ManagerId.ToString() + ",DeviceType='WEB' where Student_Id=" + StudentId.ToString() + " and ProjectStatus='Proposed' and PDID<>0";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public string Student_GetProjectTotalFunded(string PDId, string LeadId)
    {
        gstrQrystr = "select ifnull(sum(amount),0) as DispersedAmount from project_fund_details where PDId=" + PDId.ToString() + " and LeadId='" + LeadId.ToString() + "'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }


        return "";
    }
    public void Student_EditProjectProposal(string Lead_Id, string PDId, string ManagerId, string ProjectTitle, string Beneficiaries, string ProjectObjectives, string ProjectPlan, string ProposedDate, string AcademicYear, string ProjectType, string ProjectStatus, string WhoAreThey, string PlaceofImplementation, string CurrentSituation, string ProjectStartDate, string ProjectEndDate)
    {

        if (ProjectStatus == "Proposed")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET Title = '" + Regex.Replace(ProjectTitle, "'", "`").Trim() + "', BeneficiaryNo =" + Beneficiaries.ToString() + ", Objectives = '" + Regex.Replace(ProjectObjectives, "'", "`").Trim() + "', ActionPlan = '" + Regex.Replace(ProjectPlan, "'", "`").Trim() + "',Theme=" + ProjectType.ToString() + ",Edited_Date=Now(),Beneficiaries='" + Regex.Replace(WhoAreThey, "'", "`").Trim() + "',Placeofimplement='" + Regex.Replace(PlaceofImplementation, "'", "`").Trim() + "',DeviceType='WEB',CurrentSituation='" + Regex.Replace(CurrentSituation, "'", "`").Trim() + "'," + " " +
            "ProjectStartDate='" + ProjectStartDate.ToString() + "',ProjectEndDate='" + ProjectEndDate.ToString() + "'" + " " +
            "WHERE (PDId = " + PDId.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);
            Student_InsertNotification(Lead_Id.ToString(), ProjectTitle.ToString(), "Updated Proposed Project (Student)");
        }
        else if (ProjectStatus == "RequestForModification")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET Title = '" + Regex.Replace(ProjectTitle, "'", "`").Trim() + "', BeneficiaryNo =" + Beneficiaries.ToString() + ", Objectives = '" + Regex.Replace(ProjectObjectives, "'", "`").Trim() + "', ActionPlan = '" + Regex.Replace(ProjectPlan, "'", "`").Trim() + "',Theme=" + ProjectType.ToString() + ",ProjectStatus='Proposed',Edited_Date=Now(),Beneficiaries='" + Regex.Replace(WhoAreThey, "'", "`").Trim() + "',Placeofimplement='" + Regex.Replace(PlaceofImplementation, "'", "`").Trim() + "',DeviceType='WEB',CurrentSituation='" + Regex.Replace(CurrentSituation, "'", "`").Trim() + "' " + " " +
             ",ProjectStartDate='" + ProjectStartDate.ToString() + "',ProjectEndDate='" + ProjectEndDate.ToString() + "'" + " " +
           "WHERE (PDId = " + PDId.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);

            Student_InsertNotification(Lead_Id.ToString(), ProjectTitle.ToString(), "ReApplied Proposed Project (Student)");
        }


    }
    public void Student_SaveProjectMeterials(string Lead_Id, string PDId, Repeater rpt, string ClickType)
    {
        if (ClickType == "New")
        {
            int Sum = 0;
            foreach (RepeaterItem item in rpt.Items)
            {

                string MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text;
                string MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text;
                if ((MeterialName != "") || (MeterialCost != ""))
                {
                    Sum += int.Parse(MeterialCost.ToString());
                    gstrQrystr = "INSERT INTO project_meterial_details(PDId, Lead_Id, MeterialName, MeterialCost)" + " " +
                    "VALUES(" + PDId.ToString() + ", '" + Lead_Id.ToString() + "', '" + Regex.Replace(MeterialName, "'", "`").Trim() + "', " + MeterialCost.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }

            }
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET Amount = " + Sum.ToString() + ",Edited_Date=Now(),DeviceType='WEB' WHERE (PDId = " + PDId.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {
            int Sum = 0;
            gstrQrystr = "Select * from project_meterial_details where PDId=" + PDId.ToString() + "";
            bool PDIdExists = DLobj.ReturnTF(gstrQrystr);
            if (PDIdExists == true)
            {
                gstrQrystr = "SET SQL_SAFE_UPDATES=0; delete from project_meterial_details where PDId=" + PDId.ToString() + "";
                DLobj.ExecuteQuery(gstrQrystr);
                foreach (RepeaterItem item in rpt.Items)
                {
                    string MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text;
                    string MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text;
                    if ((MeterialName != "") || (MeterialCost != ""))
                    {
                        Sum += int.Parse(MeterialCost.ToString());
                        gstrQrystr = "INSERT INTO project_meterial_details(PDId, Lead_Id, MeterialName, MeterialCost)" + " " +
                        "VALUES(" + PDId.ToString() + ", '" + Lead_Id.ToString() + "', '" + Regex.Replace(MeterialName, "'", "`").Trim() + "', " + MeterialCost.ToString() + ")";
                        DLobj.ExecuteQuery(gstrQrystr);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem item in rpt.Items)
                {

                    string MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text;
                    string MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text;
                    if ((MeterialName != "") || (MeterialCost != ""))
                    {
                        Sum += int.Parse(MeterialCost.ToString());
                        gstrQrystr = "INSERT INTO project_meterial_details(PDId, Lead_Id, MeterialName, MeterialCost)" + " " +
                        "VALUES(" + PDId.ToString() + ", '" + Lead_Id.ToString() + "', '" + Regex.Replace(MeterialName, "'", "`").Trim() + "', " + MeterialCost.ToString() + ")";
                        DLobj.ExecuteQuery(gstrQrystr);
                    }

                }
            }
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET Amount = " + Sum.ToString() + ",DeviceType='WEB' WHERE (PDId = " + PDId.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);
        }


    }
    public void Student_SaveProjectTeamMembers(string Lead_Id, string PDId, Repeater rpt, string ClickType)
    {
        if (ClickType == "New")
        {

            foreach (RepeaterItem item in rpt.Items)
            {

                string MemberName = (item.FindControl("txtName") as TextBox).Text;
                string MemberMailId = (item.FindControl("txtMailId") as TextBox).Text;
                string MobileNo = (item.FindControl("txtMobileNo") as TextBox).Text;
                // string Gender = (item.FindControl("ddlGender") as DropDownList).SelectedValue.ToString();
                string Gender = "";
                if (MemberName != "")
                {
                    gstrQrystr = "INSERT INTO project_teamdetail(PDId, Lead_Id, MemberName, MemberMailId, MemberMobileNo, MemberGender)" + " " +
                    "VALUES(" + PDId.ToString() + ", '" + Lead_Id.ToString() + "', '" + Regex.Replace(MemberName, "'", "`").Trim() + "', '" + MemberMailId.ToString() + "', '" + MobileNo.ToString() + "', '" + Gender.ToString() + "')";
                    DLobj.ExecuteQuery(gstrQrystr);
                }

            }

        }
        else
        {

            gstrQrystr = "Select * from project_teamdetail where PDId=" + PDId.ToString() + "";
            bool PDIdExists = DLobj.ReturnTF(gstrQrystr);
            if (PDIdExists == true)
            {
                gstrQrystr = "SET SQL_SAFE_UPDATES=0;  delete from project_teamdetail where PDId=" + PDId.ToString() + "";
                DLobj.ExecuteQuery(gstrQrystr);
                foreach (RepeaterItem item in rpt.Items)
                {
                    string MemberName = (item.FindControl("txtName") as TextBox).Text;
                    string MemberMailId = (item.FindControl("txtMailId") as TextBox).Text;
                    string MobileNo = (item.FindControl("txtMobileNo") as TextBox).Text;
                    string Gender = (item.FindControl("ddlGender") as DropDownList).SelectedValue.ToString();
                    if (MemberName != "")
                    {
                        gstrQrystr = "INSERT INTO project_teamdetail(PDId, Lead_Id, MemberName, MemberMailId, MemberMobileNo, MemberGender)" + " " +
                        "VALUES(" + PDId.ToString() + ", '" + Lead_Id.ToString() + "', '" + Regex.Replace(MemberName, "'", "`").Trim() + "', '" + MemberMailId.ToString() + "', '" + MobileNo.ToString() + "', '" + Gender.ToString() + "')";
                        DLobj.ExecuteQuery(gstrQrystr);
                    }


                }

            }
            else
            {
                foreach (RepeaterItem item in rpt.Items)
                {
                    string MemberName = (item.FindControl("txtName") as TextBox).Text;
                    string MemberMailId = (item.FindControl("txtMailId") as TextBox).Text;
                    string MobileNo = (item.FindControl("txtMobileNo") as TextBox).Text;
                    //string Gender = (item.FindControl("ddlGender") as DropDownList).SelectedValue.ToString();
                    string Gender = (item.FindControl("ddlGender") as DropDownList).SelectedValue.ToString();
                    if (MemberName != "")
                    {
                        gstrQrystr = "INSERT INTO project_teamdetail(PDId, Lead_Id, MemberName, MemberMailId, MemberMobileNo, MemberGender)" + " " +
                        "VALUES(" + PDId.ToString() + ", '" + Lead_Id.ToString() + "', '" + Regex.Replace(MemberName, "'", "`").Trim() + "', '" + MemberMailId.ToString() + "', '" + MobileNo.ToString() + "', '" + Gender.ToString() + "')";
                        DLobj.ExecuteQuery(gstrQrystr);
                    }
                }
            }
        }
    }
    public void Student_GetProposedProjectdetailForEdit(string LeadId, string PDId, TextBox txtTitle, TextBox txtBeneficiaryNo, TextBox txtObjectives, TextBox txtActionPlan, TextBox txtManagerComment, Repeater rptMeterial, Repeater rptTeam, string ProjectStatus, Label lblTotalAmount, DropDownList ddlProjectType, TextBox txtwhoarethey, TextBox txtPlaceofImplementation, TextBox txtCurrenctSituation, TextBox txtProposedStartDate, TextBox txtProposedEndDate, Label lblProposedProjectTargetDays)
    {
        gstrQrystr = "SELECT Title, BeneficiaryNo, Objectives, ActionPlan,ManagerComments,ifnull(amount,0) as amount,theme,Beneficiaries,Placeofimplement,CurrentSituation,ifnull(Date_Format(ProjectStartDate,'%Y-%m-%d'),0) as ProjectStartDate,ifnull(Date_Format(ProjectEndDate,'%Y-%m-%d'),0) as ProjectEndDate FROM project_description WHERE(PDId = " + PDId.ToString() + ") AND(Lead_Id = '" + LeadId.ToString() + "')";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            DateTime EndDate;
            DateTime StartDate;
            txtTitle.Text = Regex.Replace(dt.Rows[0].ItemArray[0].ToString(), "'", "`").Trim();
            txtBeneficiaryNo.Text = dt.Rows[0].ItemArray[1].ToString();
            txtObjectives.Text = Regex.Replace(dt.Rows[0].ItemArray[2].ToString(), "'", "`").Trim();
            txtActionPlan.Text = Regex.Replace(dt.Rows[0].ItemArray[3].ToString(), "'", "`").Trim();
            txtManagerComment.Text = Regex.Replace(dt.Rows[0].ItemArray[4].ToString(), "'", "`").Trim();
            lblTotalAmount.Text = dt.Rows[0].ItemArray[5].ToString();
            ddlProjectType.SelectedIndex = ddlProjectType.Items.IndexOf(ddlProjectType.Items.FindByValue(dt.Rows[0].ItemArray[6].ToString()));
            txtwhoarethey.Text = Regex.Replace(dt.Rows[0].ItemArray[7].ToString(), "'", "`").Trim();
            txtPlaceofImplementation.Text = Regex.Replace(dt.Rows[0].ItemArray[8].ToString(), "'", "`").Trim();
            txtCurrenctSituation.Text = Regex.Replace(dt.Rows[0].ItemArray[9].ToString(), "'", "`").Trim();
            if ((dt.Rows[0].ItemArray[10].ToString() != "0") && (dt.Rows[0].ItemArray[11].ToString() != "0"))
            {
                StartDate = DateTime.Parse(dt.Rows[0].ItemArray[10].ToString());
                EndDate = DateTime.Parse(dt.Rows[0].ItemArray[11].ToString());
                int Days = int.Parse((EndDate.Date - StartDate.Date).TotalDays.ToString());
                if (Days == 0)
                {
                    Days = 1;
                }
                else
                {
                    Days = Days + 1;
                }

                lblProposedProjectTargetDays.Text = Days.ToString();

                txtProposedStartDate.Text = dt.Rows[0].ItemArray[10].ToString();
                txtProposedEndDate.Text = dt.Rows[0].ItemArray[11].ToString();
            }


        }

        gstrQrystr = "SELECT MeterialName, MeterialCost, slno FROM project_meterial_details WHERE(PDId = " + PDId.ToString() + ") AND(Lead_Id = '" + LeadId.ToString() + "') order by slno";
        rptMeterial.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptMeterial.DataBind();

        gstrQrystr = "SELECT MemberName, MemberMailId, MemberMobileNo, MemberGender, slno FROM project_teamdetail" + " " +
        "WHERE (PDId =" + PDId.ToString() + ") AND(Lead_Id = '" + LeadId.ToString() + "') order by slno";
        rptTeam.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptTeam.DataBind();

    }
    public void Manager_GetTeamMemberDetails(string PDId, string LeadId, Repeater rptTeam)
    {
        gstrQrystr = "SELECT MemberName, MemberMailId, MemberMobileNo, MemberGender, slno FROM project_teamdetail" + " " +
       "WHERE (PDId =" + PDId.ToString() + ") AND(Lead_Id = '" + LeadId.ToString() + "') order by slno";
        rptTeam.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptTeam.DataBind();
    }
    public void Manager_GetTeamMemberDetails_NEW(string PDId, Repeater rptTeam)
    {
        gstrQrystr = "SELECT MemberName, MemberMailId, MemberMobileNo, MemberGender, slno FROM project_teamdetail" + " " +
       "WHERE (PDId =" + PDId.ToString() + ")  order by slno";
        rptTeam.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptTeam.DataBind();
    }
    public string Student_GetProjectMembersGender(string Slno)
    {
        gstrQrystr = "select MemberGender from project_teamdetail where slno=" + Slno.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        return "";


    }
    public void Student_GetProjectList(string Lead_Id, Repeater rptProposed)
    {
        gstrQrystr = "select PDId,title,ProjectStatus,rating,amount,SanctionAmount from Project_description where Lead_Id='" + Lead_Id.ToString() + "' order by Edited_Date desc";
        rptProposed.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptProposed.DataBind();
    }
    public string Student_GetCompletionDraftProgress(string PDId)
    {
        gstrQrystr = "select CompletionProgress from project_description where pdid=" + PDId.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            return "";
        }
    }
    public void Student_UpdateStudentProfileImage(string LeadId, string ImgPath)
    {
        DataLL DL = new DataLL();
        DL.DeleteExistingProfilePic(LeadId);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update student_registration set Image_path='" + ImgPath.ToString() + "' where Lead_Id='" + LeadId.ToString() + "'";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Manager_UpdateOnlyManagerProfileImage(string ManagerId, string ImgPath)
    {
        DataLL DL = new DataLL();
        DL.DeleteManagerExistingProfilePic(ManagerId);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update manager_details set Image_path='" + ImgPath.ToString() + "' where ManagerId='" + ManagerId.ToString() + "'";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public string Manager_UpdatePassword(string ManagerId, string OldPassword, string NewPassword)
    {
        gstrQrystr = "select * from users where Password='" + OldPassword.ToString() + "' and Role='Manager' and Reference_Id=" + ManagerId.ToString() + "";
        bool isExists = DLobj.ReturnTF(gstrQrystr);
        if (isExists == true)
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; update users set Password='" + NewPassword.ToString() + "' where Role='Manager' and Reference_Id=" + ManagerId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);


            return "Updated";
        }
        else
        {
            return "Password Not Match";
        }

    }
    public void Manager_UpdateProfileDetails(string ManagerId, string ManagerName, string MobileNo, string Emailid, string Gender, string BloodGroup, string Address, string Facebook, string Twitter, string Instagram, string Whatsapp, string RecordCount)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE manager_details set Managername = '" + Regex.Replace(ManagerName.ToString(), "'", "`").Trim() + "', MobileNo = '" + MobileNo.ToString() + "', mailid = '" + Emailid.ToString() + "', Gender = '" + Gender.ToString() + "', BloodGroup = '" + BloodGroup.ToString() + "',Address=N'" + Regex.Replace(Address.ToString(), "'", "`").Trim() + "', facebook = '" + Facebook.ToString() + "', Twitter = '" + Twitter.ToString() + "', instagram = '" + Instagram.ToString() + "', whatsapp = '" + Whatsapp.ToString() + "',RecordCounts=" + RecordCount.ToString() + " where managerid = " + ManagerId.ToString() + " ";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public DataTable Manager_GetManagerDetails(string ManagerId)
    {
        gstrQrystr = "select managerid,managername,MobileNo,MailId,Gender,BloodGroup,address,Facebook,Twitter,InstaGram,WhatsApp,Image_Path,RecordCounts from manager_details where managerid=" + ManagerId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Student_UpdateProfileDatails(vmStudent_Web Stud)
    {
        DataLL DL = new DataLL();
        DL.UpdateStudentProfile(Stud);
    }
    public vmStudent_Web Student_GetStudentProfileDetails(string LeadId)
    {
        DataLL DL = new DataLL();
        return DL.GetStudentDetails(LeadId.ToString());

    }
    public void Student_SetStudentProfileImage(string LeadId, Image img)
    {
        //  String[] filePaths = null;
        gstrQrystr = "select Image_path from student_registration where lead_id='" + LeadId + "'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0].ItemArray[0].ToString() != "")
            {
                img.ImageUrl = dt.Rows[0].ItemArray[0].ToString();
            }
            else
            {
                img.ImageUrl = HttpContext.Current.Server.MapPath("~/CSS/Images/NoImage.png");   // "http://mis.leadcampus.org/ProfilePics/MG09991__3f71dfef-18a5-45e4-aa8c-27d89fc0fbb6.jpg"; // HttpContext.Current.Server.MapPath("CSS /Images/NoImage.png");// "../CSS/Images/NoImage.png";
            }

        }

    }

    public vmManager Manager_GetManagerDetails(vmManager obj, string ManagerId)
    {
        gstrQrystr = "select ManagerId,ManagerName,MobileNo,MailId,Image_path,Facebook,Twitter,InstaGram from manager_details where ManagerId=" + ManagerId.ToString() + " and status=1";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            obj.ManagerID = dt.Rows[0].ItemArray[0].ToString();
            obj.ManagerName = dt.Rows[0].ItemArray[1].ToString();
            obj.MobileNo = dt.Rows[0].ItemArray[2].ToString();
            obj.MailId = dt.Rows[0].ItemArray[3].ToString();
            if (obj.Image_path != "")
            {
                obj.Image_path = dt.Rows[0].ItemArray[4].ToString();
            }
            else
            {
                obj.Image_path = (HttpContext.Current.Server.MapPath("~/CSS/Images/NoImage.png"));
            }

            obj.Facebook = dt.Rows[0].ItemArray[5].ToString();
            obj.Twitter = dt.Rows[0].ItemArray[6].ToString();
            obj.InstaGram = dt.Rows[0].ItemArray[7].ToString();
        }
        return obj;
    }

    public string Student_CheckIsFeesPaid(string Lead_Id)
    {
        gstrQrystr = "select isFeePaid from student_registration where lead_id = '" + Lead_Id + "'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt.Rows[0].ItemArray[0].ToString();
    }

    public string Manager_GetManagerProfilePicPath(string ManagerId)
    {
        gstrQrystr = "select Image_path from manager_details where ManagerId=" + ManagerId.ToString() + " and status=1";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }

        return "";
    }

    public void Student_SetManagerProfileImage(string ManagerId, Image img)
    {
        //  String[] filePaths = null;
        gstrQrystr = "select Image_path from manager_details where ManagerId='" + ManagerId.ToString() + "'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            img.ImageUrl = dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            img.ImageUrl = "../../CSS/Images/NoImage.png";
        }

    }
    public string Student_GetUploadedImgCount(string PDId)
    {
        gstrQrystr = "select IFNULL(count(PDID),0) as ImgCount from project_digital_documents where Document_Type='IMG' and PDId=" + PDId.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt.Rows[0].ItemArray[0].ToString();
    }
    public void Student_SaveDocumentImages(string LeadId, string PDId, HttpFileCollection fileCollection, string FilePath, int ImgCount)
    {
        vmDigitalDocument vmd = new vmDigitalDocument();
        DataLL DL = new DataLL();

        string Doc_Type = "";
        //DL.DeleteExistsingDocument(LeadId, PDId, "IMG",ImgCount);
        //DL.DeleteExistsingDocument(LeadId, PDId, "DOC", ImgCount);
        //gstrQrystr = "delete from project_digital_documents where lead_id=" + LeadId.ToString() + " and PDId=" + PDId.ToString() + " where Document_Type='IMG'";
        //DLobj.ExecuteQuery(gstrQrystr);
        for (int i = 0; i < fileCollection.Count; i++)
        {
            HttpPostedFile uploadfile = fileCollection[i];
            string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);
            Stream strm = uploadfile.InputStream;
            if (strm.Length > 20000000)
            {
                //msg = "Upload Less Than 20 MB Image";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {

                if ((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF") || (FileExtenssion.ToString() == ".psd") || (FileExtenssion.ToString() == ".PSD"))
                {
                    using (var ms = new MemoryStream())
                    {
                        uploadfile.InputStream.CopyTo(ms);
                        ms.Position = 0;
                        using (System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(ms))
                        {
                            System.Drawing.Imaging.ImageCodecInfo jpgEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);

                            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                            System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                            System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 50L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            Doc_Type = "IMG";

                            if (uploadfile.ContentLength > 0)
                            {
                                vmd.LeadId = LeadId.ToString();
                                vmd.PDId = int.Parse(PDId.ToString());
                                vmd.Document_Id = i;
                                vmd.Document_Path = FilePath + LeadId.ToString() + "_" + PDId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                                vmd.Document_Type = Doc_Type.ToString();
                                bmp1.Save(HttpContext.Current.Server.MapPath(vmd.Document_Path), jpgEncoder, myEncoderParameters);
                                DL.InsertDigitalDocument(vmd);
                            }
                        }
                    }
                }
                else if ((FileExtenssion.ToString() == ".doc") || (FileExtenssion.ToString() == ".DOC") || (FileExtenssion.ToString() == ".pdf") || (FileExtenssion.ToString() == ".PDF") || (FileExtenssion.ToString() == ".docx") || (FileExtenssion.ToString() == ".DOCX") || (FileExtenssion.ToString() == ".xls") || (FileExtenssion.ToString() == ".XLS") || (FileExtenssion.ToString() == ".xlsx") || (FileExtenssion.ToString() == ".XLSX"))
                {
                    Doc_Type = "DOC";
                    if (uploadfile.ContentLength > 0)
                    {
                        vmd.LeadId = LeadId.ToString();
                        vmd.PDId = int.Parse(PDId.ToString());
                        vmd.Document_Id = i;
                        vmd.Document_Path = FilePath + LeadId.ToString() + "_" + PDId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                        vmd.Document_Type = Doc_Type.ToString();
                        uploadfile.SaveAs(HttpContext.Current.Server.MapPath(vmd.Document_Path.ToString()));
                        DL.InsertDigitalDocument(vmd);
                    }
                }

            }
        }
    }

    public void Student_ApplyForMasterLeader(string LeadId)
    {
        // gstrQrystr = "update Student_Registration set isApply_MasterLeader = 1,MasterLeader_RequestedDate=Now(),MasterLeaderDate=Now(),Student_Type='Master Leader' where Lead_Id = '" + LeadId.ToString()+"'";
        // DLobj.ExecuteQuery(gstrQrystr);

        //Student_InsertNotification(LeadId.ToString(), "Master Leader", "Applied For Master LEAder");
    }
    public void Student_ApplyForLeadAmbassador(string LeadId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update Student_Registration set isApply_LeadAmbassador = 1,LeadAmbassador_RequestedDate=Now(),LeadAmbassadorDate=Now(),Student_Type='Lead Ambassador' where Lead_Id = '" + LeadId.ToString() + "' and RegistrationId<>0";
        DLobj.ExecuteQuery(gstrQrystr);
        Student_InsertNotification(LeadId.ToString(), "Master LeadAmbassador", "Applied For LeadAmbassador");
    }
    public void Student_SaveDocuments(string LeadId, string PDId, FileUpload fu, string FilePath)
    {
        vmDigitalDocument vmd = new vmDigitalDocument();
        DataLL DL = new DataLL();
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; delete from project_digital_documents where lead_id=" + LeadId.ToString() + " and PDId=" + PDId.ToString() + " where Document_Type='DOC'";
        DLobj.ExecuteQuery(gstrQrystr);
        // DL.DeleteExistsingDocument(LeadId, PDId, "DOC");

        for (int i = 0; i < fu.PostedFiles.Count; i++)
        {
            HttpPostedFile uploadfile = fu.PostedFiles[i];
            string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);

            if (uploadfile.ContentLength > 0)
            {
                vmd.LeadId = LeadId.ToString();
                vmd.PDId = int.Parse(PDId.ToString());
                vmd.Document_Id = i + 1;
                vmd.Document_Path = FilePath + LeadId.ToString() + "_" + PDId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                vmd.Document_Type = "DOC";
                //DL.InsertDigitalDocument(vmd);
            }
        }
    }
    public void Admin_DisableEventsAutomatically()
    {
        gstrQrystr = "select EventId from mstr_events where status=1 and  date_format(EventToDate,'%d-%m-%Y')<=date_format(Now(),'%d-%m-%Y')";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        foreach (DataRow dr in dt.Rows)
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; update mstr_events set Status=0 where EventId=" + dr[0].ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
    }
    public void Manager_GetProposedProjectList(string ManagerId, Repeater rptProposed, string ProjectStatus)
    {
        // gstrQrystr = "select PDId,Lead_Id,title,ProjectStatus from Project_description where ManagerId='"+ManagerId.ToString()+"'";

        gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.Proposed, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus" + " " +
        "from project_description AS PD inner join student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
        "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
        "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
        "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
        "where PD.ManagerId='" + ManagerId.ToString() + "' and pd.ProjectStatus='" + ProjectStatus.ToString() + "' order by DATE_FORMAT(Edited_Date, '%Y-%m-%d %h:%i:%s')  desc";

        rptProposed.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptProposed.DataBind();
    }
    //added draft project count
    public DataTable GetDashBoardProjectCount(string AcademicCode, string FromDate, string ToDate, string ManagerCode)
    {
        if (AcademicCode.ToString() == "[All]")
        {
            gstrQrystr = "SELECT count(PDId) AS TotalProjects,SUM(CASE WHEN pd.ProjectStatus = 'Proposed' THEN 1  ELSE 0 END) AS TotalProposed," + " " +
        "SUM(CASE WHEN pd.ProjectStatus = 'Approved'  THEN 1  ELSE 0 END) AS TotalApproved," + " " +
        "SUM(CASE WHEN pd.ProjectStatus = 'Rejected' THEN 1  ELSE 0 END) as TotalRejected," + " " +
        "SUM(CASE WHEN pd.ProjectStatus = 'RequestForModification' THEN 1  ELSE 0 END) as TotalRM," + " " +
        "SUM(CASE WHEN pd.ProjectStatus = 'RequestForCompletion' THEN 1  ELSE 0 END) as TotalRC," + " " +
         "SUM(CASE WHEN pd.ProjectStatus = 'Draft' THEN 1  ELSE 0 END) as Totaldraft," + " " +
        "SUM(CASE WHEN pd.ProjectStatus = 'Completed' THEN 1  ELSE 0 END) as TotalCompleted FROM project_description pd inner join student_registration as SR" + " " +
        "on pd.Student_Id=SR.RegistrationId  where pd.ManagerId=" + ManagerCode.ToString() + " and SR.isprofileedit=1";
        }
        else
        {
            gstrQrystr = "select SUM(CASE WHEN pd.ProjectStatus = 'Proposed' and (date(pd.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END)+" + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'Approved' and (date(pd.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END)+" + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'Rejected' and (date(pd.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END)+ " + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'RequestForModification' and (date(pd.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END)+ " + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'RequestForCompletion' and (date(pd.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END)+" + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'Completed' and (date(pd.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) as TotalProjects," + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'Proposed' and (date(pd.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) AS TotalProposed," + " " +
      "SUM(CASE WHEN pd.ProjectStatus = 'Approved' and (date(pd.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) AS TotalApproved," + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'Rejected' and (date(pd.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) as TotalRejected," + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'RequestForModification' and (date(pd.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) as TotalRM," + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'RequestForCompletion' and (date(pd.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) as TotalRC," + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'Draft' and (date(pd.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) as Totaldraft," + " " +
       "SUM(CASE WHEN pd.ProjectStatus = 'Completed' and (date(pd.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') THEN 1  ELSE 0 END) as TotalCompleted " + " " +
       "FROM project_description pd inner join student_registration as SR" + " " +
       "on pd.Student_Id=SR.RegistrationId  where  pd.ManagerId=" + ManagerCode.ToString() + " and SR.isprofileedit=1";
        }

        DataTable dt = DLobj.GetDataTable(gstrQrystr);

        return dt;
    }
    public string Manager_DashboardGetTshirtRequestCount(string ManagerId, string AcademicCode)
    {
        gstrQrystr = "select count(requestedId) as Count from student_tshirt_allotment where managerId=" + ManagerId.ToString() + " and RequestStatus=1 and SanctionStatus=0 and AcademicCode=" + AcademicCode.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        string count = "";
        if (dt.Rows.Count > 0)
        {
            count = dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            count = "0";
        }
        return count;

    }
    public string Manager_DashboardGetFundingAmountCount(string ManagerId, string AcademicCode)
    {
        gstrQrystr = "select count(pdid) from project_description where amount<>0 and SanctionAmount<>0  and projectstatus IN ('Approved' , 'RequestForCompletion', 'Completed') and managerid=" + ManagerId.ToString() + " and AcademicCode=" + AcademicCode.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        string count = "";
        if (dt.Rows.Count > 0)
        {
            count = dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            count = "0";
        }
        return count;
    }
    public DataTable GetDashboardRegistrationCount(string ManagerId, string FromDate, string ToDate)
    {
        gstrQrystr = "select count(Lead_Id) as TotalRegistration from Student_registration where ManagerCode=" + ManagerId.ToString() + " and AcademicCode=(select slno from academicyear order by slno desc limit 1)";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }

    // added draft 
    public DataSet Manager_GetProjectDetail(string ManagerId, string ProjectStatus, DropDownList ddlAcademicYear, string FromDate, string ToDate, string Records)
    {
        if (ddlAcademicYear.SelectedItem.Text.ToString() == "[All]")
        {
            if (ProjectStatus != "DashBoard")
            {
                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
             "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
             "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName  from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
             "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
             "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
             "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
             "where PD.ManagerId='" + ManagerId.ToString() + "' and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "') and SR.isProfileEdit=1 order by ifnull(PD.Edited_Date,PD.Created_Date) desc limit  " + Records.ToString() + "";

            }
            else
            {
                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
      "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
      "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
      "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
      "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
      "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
          "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
      "where PD.ManagerId='" + ManagerId.ToString() + "' and pd.ProjectStatus In ('Proposed','RequestForModification','Approved','Completed','RequestForCompletion','Rejected','Draft') and SR.isProfileEdit=1 order by ifnull(PD.Edited_Date,PD.Created_Date) desc limit  " + Records.ToString() + "";
            }
        }
        else
        {
            if (ProjectStatus == "Draft")
            {

                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
        "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
        "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
        "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
        "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
        "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
         "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
        "where PD.ManagerId='" + ManagerId.ToString() + "' and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "')" + " " +
        "and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')" + " " +
        " and SR.isProfileEdit=1 order by ifnull(PD.Edited_Date,PD.Created_Date) desc limit  " + Records.ToString() + "";


            }
            else if (ProjectStatus != "DashBoard")
            {

                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
        "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
        "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
        "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
        "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
        "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
         "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
        "where PD.ManagerId='" + ManagerId.ToString() + "' and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "')" + " " +
        "and (date(pd." + ProjectStatus.ToString() + "Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')" + " " +
        " and SR.isProfileEdit=1 order by ifnull(PD.Edited_Date,PD.Created_Date) desc limit  " + Records.ToString() + "";


            }
            else
            {
                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
       "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
       "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
       "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
       "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
       "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
        "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
       "where PD.ManagerId='" + ManagerId.ToString() + "' " + " " +
      "and (date(pd.Edited_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')" + " " +

        //"and pd.ProjectStatus = 'Proposed' and (date(pd.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')" + " " +
        //"OR pd.ProjectStatus = 'Approved' and (date(pd.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')" + " " +
        //"OR pd.ProjectStatus = 'Rejected' and (date(pd.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')" + " " +
        //"OR pd.ProjectStatus = 'RequestForModification' and (date(pd.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')" + " " +
        //"OR pd.ProjectStatus = 'RequestForCompletion' and (date(pd.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') " + " " +
        //"OR pd.ProjectStatus = 'Completed' and (date(pd.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') " + " " +
        "and SR.isProfileEdit=1 order by ifnull(PD.Edited_Date,PD.Created_Date) desc limit  " + Records.ToString() + "";

            }
        }


        return DLobj.GetDataSet(gstrQrystr);

        //---- for column check from Database-------

        // select PD.PDId,DATE_FORMAT(PD.ProposalDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus,
        //PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments from project_description AS PD inner join student_registration AS SR on PD.Lead_Id = SR.Lead_Id
        //inner join colleges AS CLG on SR.Collegecode = CLG.CollegeId
        //inner join mstr_taluka AS TL on TL.Id = SR.TalukaCode
        //INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid

    }
    public DataSet Manager_SearchGetProjectDetail(string ManagerId, string ProjectStatus, string SearchValue, string SearchOn, DropDownList ddlAcademicYear, string Records)
    {
        if (ddlAcademicYear.SelectedItem.Text.ToString() == "[All]")
        {
            if (ProjectStatus != "DashBoard")
            {
                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
             "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
             "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName  from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
             "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
             "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
             "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +

             "where PD.ManagerId='" + ManagerId.ToString() + "' and  pd.ProjectStatus In ('" + ProjectStatus.ToString() + "') and SR.isProfileEdit=1" + " " +
             "And " + SearchOn.ToString() + " " + "like '%" + SearchValue.ToString() + "%'" + " " +
             "order by ifnull(PD.Edited_Date,PD.Created_Date) desc";

            }
            else
            {
                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
      "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
      "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
      "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
      "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
      "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
          "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
      "where PD.ManagerId='" + ManagerId.ToString() + "' " + " " +
      "and SR.isProfileEdit=1 and  pd.ProjectStatus In ('Proposed','RequestForModification','Approved','Completed','RequestForCompletion','Rejected')" + " " +
      "And " + SearchOn.ToString() + " " + "like '%" + SearchValue.ToString() + "%'" + " " +
      "order by ifnull(PD.Edited_Date,PD.Created_Date) desc";
            }
        }
        else
        {
            if (ProjectStatus != "DashBoard")
            {

                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
        "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
        "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
        "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
        "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
        "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
        "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
        "where PD.ManagerId='" + ManagerId.ToString() + "' and SR.isProfileEdit=1 and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "') and (pd.AcademicCode=" + ddlAcademicYear.SelectedValue.ToString() + ")" + " " +
        "And " + SearchOn.ToString() + " " + "like '%" + SearchValue.ToString() + "%'" + " " +
        "order by ifnull(PD.Edited_Date,PD.Created_Date) desc";


            }
            else
            {
                gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
       "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
       "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount,Semester.SemName from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
       "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
       "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
       "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
       "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +

       "where PD.ManagerId='" + ManagerId.ToString() + "' and SR.isProfileEdit=1 and  pd.ProjectStatus In ('Proposed','RequestForModification','Approved','Completed','RequestForCompletion','Rejected')" + " " +
       "and (pd.AcademicCode=" + ddlAcademicYear.SelectedValue.ToString() + ") " + " " +
       "And " + SearchOn.ToString() + " " + "like '%" + SearchValue.ToString() + "%'" + " " +
       "order by ifnull(PD.Edited_Date,PD.Created_Date) desc";

            }
        }
        return DLobj.GetDataSet(gstrQrystr);
    }
    public DataTable Manager_GetFeesUnPaidStudentList(string ManagerCode, string CollegeCode)
    {
        if (CollegeCode == "[All]")
        {
            gstrQrystr = "select distinct registrationid,Image_Path,Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,isFeePaid,Semester.SemName from student_registration as SR" + " " +
                "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
                " where ManagerCode=" + ManagerCode.ToString() + " and isFeePaid=0 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1)  order by RegistrationId desc";

        }
        else
        {
            gstrQrystr = "select distinct registrationid,Image_Path,Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,isFeePaid,Semester.SemName from student_registration as SR" + " " +
                   "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
                " where ManagerCode=" + ManagerCode.ToString() + " and CollegeCode=" + CollegeCode.ToString() + " and isFeePaid=0 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1) order by RegistrationId desc";

        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetFeesPaidStudentList(string ManagerCode, string CollegeCode)
    {
        if (CollegeCode == "[All]")
        {
            gstrQrystr = "select distinct registrationid,Image_Path,Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,isFeePaid,Semester.SemName from student_registration as SR " + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
                "where ManagerCode=" + ManagerCode.ToString() + " and isFeePaid=1 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1)  order by RegistrationId desc";
        }
        else
        {
            gstrQrystr = "select distinct registrationid,Image_Path,Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,isFeePaid,Semester.SemName from student_registration as SR" + " " +
                   "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
                "where ManagerCode=" + ManagerCode.ToString() + " and CollegeCode=" + CollegeCode.ToString() + " and isFeePaid=1 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1)  order by RegistrationId desc";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_ExcelGetFeesUnPaidStudentList(string ManagerCode, string CollegeCode)
    {
        if (CollegeCode == "[All]")
        {
            gstrQrystr = "select distinct Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,colleges.College_Name,'UnPaid',Semester.SemName from student_registration as SR inner join colleges on SR.collegeCode=colleges.CollegeId" + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
               " where ManagerCode=" + ManagerCode.ToString() + " and isFeePaid=0 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1)  order by RegistrationId";

        }
        else
        {
            gstrQrystr = "select distinct Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,colleges.College_Name,'UnPaid',Semester.SemName from student_registration as SR inner join colleges on SR.collegeCode=colleges.CollegeId" + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
                " where ManagerCode=" + ManagerCode.ToString() + " and CollegeCode=" + CollegeCode.ToString() + " and isFeePaid=0 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1) order by RegistrationId";

        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_ExcelGetFeesPaidStudentList(string ManagerCode, string CollegeCode)
    {
        if (CollegeCode == "[All]")
        {
            gstrQrystr = "select distinct Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,colleges.College_Name,'Paid',Semester.SemName from student_registration as SR inner join colleges on SR.collegeCode=colleges.CollegeId" + " " +
             "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
             " where ManagerCode=" + ManagerCode.ToString() + " and isFeePaid=1 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1)  order by RegistrationId";
        }
        else
        {
            gstrQrystr = "select distinct Lead_Id,MobileNo,MailId,date_format(RegistrationDate,'%d-%m-%Y')  as RegistrationDate,StudentName,colleges.College_Name,'Paid',Semester.SemName  from student_registration as SR inner join colleges on SR.collegeCode=colleges.CollegeId" + " " +
                "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
                " where ManagerCode=" + ManagerCode.ToString() + " and CollegeCode=" + CollegeCode.ToString() + " and isFeePaid=1 and ActiveStatus=1 and AcademicCode=(select slno from academicyear order by slno desc limit 1)  order by RegistrationId";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Admin_GetManagersCollegeCount(string ManagerId)
    {
        gstrQrystr = "select ifnull(Count(CollegeCode),0) as CollegeCount from manager_colleges where ManagerCode=" + ManagerId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetStudentDetailOnClick(string ManagerId, string CollegeId)
    {
        gstrQrystr = "select date_format(registrationDate,'%d-%m-%Y') as RegistrationDate,Lead_Id,StudentName,MobileNo,MailId,AadharNo,Student_Type from student_registration where ManagerCode = " + ManagerId.ToString() + " and CollegeCode=" + CollegeId.ToString() + " order by studentname";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void FillCollegeNameByManagerId(DropDownList ddl, string ManagerCode)
    {
        if (ManagerCode != "[All]")
        {
            gstrQrystr = "select distinct collegeid,college_name from colleges as C,manager_colleges MC where C.collegeid=MC.collegecode and managercode=" + ManagerCode.ToString() + " and C.status=1";
        }
        else
        {
            gstrQrystr = "select distinct collegeid,college_name from colleges as C where status=1";
        }
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "collegeid", "college_name");
    }
    public void Manager_FillCollegeByManagerCode(string ManagerCode, DropDownList ddl)
    {
        if (ManagerCode != "[All]")
        {
            gstrQrystr = "SELECT Distinct colleges.CollegeId, colleges.College_Name FROM manager_colleges INNER JOIN  ";
            gstrQrystr = gstrQrystr + "colleges ON manager_colleges.CollegeCode = colleges.CollegeId WHERE colleges.status=1 and (manager_colleges.ManagerCode = " + ManagerCode.ToString() + ") Order by College_Name";
        }
        else
        {
            gstrQrystr = "SELECT Distinct colleges.CollegeId, colleges.College_Name FROM manager_colleges INNER JOIN  ";
            gstrQrystr = gstrQrystr + "colleges ON manager_colleges.CollegeCode = colleges.CollegeId WHERE colleges.status=1  Order by College_Name";
        }

        //gstrQrystr = "SELECT Distinct colleges.CollegeId, colleges.College_Name FROM manager_colleges INNER JOIN  ";
        //gstrQrystr = gstrQrystr + "colleges ON manager_colleges.CollegeCode = colleges.CollegeId WHERE colleges.status=1 and (manager_colleges.ManagerCode = " + ManagerCode.ToString()+") Order by College_Name";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "CollegeId", "College_Name");
    }
    public void FillCollegeByManagerCode(string ManagerCode, string Program_ID, DropDownList ddl)
    {
        if (ManagerCode != "[All]")
        {
            gstrQrystr = " SELECT Distinct colleges.CollegeId, colleges.College_Name FROM manager_colleges Inner join manager_details as md on md.ManagerId = manager_colleges.ManagerCode INNER JOIN colleges ON manager_colleges.CollegeCode = colleges.CollegeId WHERE colleges.status = 1 and(manager_colleges.ManagerCode =" + ManagerCode.ToString() + ") and md.Program_Id = " + Program_ID.ToString() + "   Order by College_Name";
        }
        else
        {

            gstrQrystr = " SELECT Distinct colleges.CollegeId, colleges.College_Name FROM manager_colleges Inner join manager_details as md on md.ManagerId = manager_colleges.ManagerCode INNER JOIN colleges ON manager_colleges.CollegeCode = colleges.CollegeId WHERE colleges.status=1 and md.Program_Id =  " + Program_ID.ToString() + "  Order by College_Name";
        }
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "CollegeId", "College_Name");
    }
    public void Manager_FillCollegeByManagerCodeListBox(string ManagerCode, ListBox lst)
    {
        gstrQrystr = "SELECT Distinct colleges.CollegeId, colleges.College_Name FROM manager_colleges INNER JOIN  ";
        gstrQrystr = gstrQrystr + "colleges ON manager_colleges.CollegeCode = colleges.CollegeId WHERE (manager_colleges.ManagerCode = " + ManagerCode.ToString() + ") Order by College_Name";
        DLobj.FillListbox(lst, gstrQrystr, "CollegeId", "College_Name");
    }
    public DataTable Admin_GetProjectsListManagerWise(string AcademicCode, string ManagerId, string ProjectStatus)
    {
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
               "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
               "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount  from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
               "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
               "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
               "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
               "where PD.ManagerId='" + ManagerId.ToString() + "' and SR.isProfileEdit=1 and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "') and (pd.AcademicCode=" + AcademicCode.ToString() + ") order by PD.PDId desc";
        }
        else
        {
            gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.TotalBudget,PD.ProjectStatus," + " " +
               "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.FundsReceived,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate," + " " +
               "SR.Image_Path,IFNULL(PD.Amount,0) AS Amount,IFNULL(PD.SanctionAmount,0) AS SanctionAmount  from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
               "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
               "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
               "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
               "where PD.ManagerId='" + ManagerId.ToString() + "' and SR.isProfileEdit=1 and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "') order by PD.PDId desc";
        }
        return DLobj.GetDataTable(gstrQrystr);

    }
    public void Admin_SaveManager_TshirtAllotment(Repeater rpt, string AcademicCode, string Editor)
    {
        gstrQrystr = "select AcademicCode from manager_tshirt where AcademicCode=" + AcademicCode.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            foreach (RepeaterItem item in rpt.Items)
            {
                Label ManagerId = (item.FindControl("lblManagerId") as Label);
                TextBox AllotedS = (item.FindControl("txtS") as TextBox);
                TextBox AllotedM = (item.FindControl("txtM") as TextBox);
                TextBox AllotedL = (item.FindControl("txtL") as TextBox);
                TextBox AllotedXL = (item.FindControl("txtXL") as TextBox);
                TextBox AllotedXXL = (item.FindControl("txtXXL") as TextBox);

                gstrQrystr = "SET SQL_SAFE_UPDATES=0; update manager_tshirt SET AllotedS=" + AllotedS.Text + ",AllotedM=" + AllotedM.Text + ",AllotedL=" + AllotedL.Text + ",AllotedXL=" + AllotedXL.Text + ",AllotedXXL=" + AllotedXXL.Text + ",Edited_Date=Now(),Edited_By=" + Editor + " Where AcademicCode=" + AcademicCode.ToString() + " and ManagerId=" + ManagerId.Text + "";
                DLobj.ExecuteQuery(gstrQrystr);
            }
        }
        else
        {
            foreach (RepeaterItem item in rpt.Items)
            {
                Label ManagerId = (item.FindControl("lblManagerId") as Label);
                TextBox AllotedS = (item.FindControl("txtS") as TextBox);
                TextBox AllotedM = (item.FindControl("txtM") as TextBox);
                TextBox AllotedL = (item.FindControl("txtL") as TextBox);
                TextBox AllotedXL = (item.FindControl("txtXL") as TextBox);
                TextBox AllotedXXL = (item.FindControl("txtXXL") as TextBox);

                gstrQrystr = "INSERT INTO manager_tshirt(AcademicCode,ManagerId,AllotedS,AllotedM,AllotedL,AllotedXL,AllotedXXL,SanctionDate,SanctionedBy) VALUES" + " " +
                "(" + AcademicCode.ToString() + ", " + ManagerId.Text.ToString() + ", " + AllotedS.Text + "," + AllotedM.Text + ", " + AllotedL.Text + ", " + AllotedXL.Text + ", " + AllotedXXL.Text + ", Now(), " + Editor + ")";

                DLobj.ExecuteQuery(gstrQrystr);
            }
        }
    }
    public DataSet Manager_GetProjectDetailAndModify(string ManagerId, string ProjectStatus, string PDId)
    {
        gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName,CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.Amount,PD.ProjectStatus," + " " +
     "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.SanctionAmount,PD.Theme,PD.CurrentSituation,PD.Objectives,PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ,DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate,DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate,DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate,PD.Placeofimplement," + " " +
     "IFNULL(Date_Format(ProjectStartDate,'%Y-%m-%d'),0) as ProjectStartDate,IFNULL(Date_Format(ProjectEndDate,'%Y-%m-%d'),0) as ProjectEndDate" + " " +
     "from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
     "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
     "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
     "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
     "where PD.ManagerId='" + ManagerId.ToString() + "' and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "') and pd.PDId=" + PDId.ToString() + "";

        return DLobj.GetDataSet(gstrQrystr);
    }
    public DataTable Manager_GetProjectDetailAndModify_NEW(string ManagerId, string ProjectStatus, string PDId)
    {
        gstrQrystr = "select PD.PDId,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,PD.Lead_id,PD.TITLE,SR.StudentName,course.CourseName," + " " +
        "CLG.College_Name,TL.Taluk_Name,SR.MobileNo,PD.Amount,PD.ProjectStatus," + " " + "PD.BeneficiaryNo,PD.Beneficiaries,PD.Duratoin,PD.SanctionAmount,PD.Theme,PD.CurrentSituation,PD.Objectives," + " " +
        "PD.ActionPlan,PD.ManagerComments,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as ApprovedDate ," + " " +
     "DATE_FORMAT(PD.CompletedDate, '%d-%m-%Y') as CompletedDate, DATE_FORMAT(PD.RejectedDate, '%d-%m-%Y') as RejectedDate," + " " +
     "DATE_FORMAT(PD.RequestForModificationDate, '%d-%m-%Y') as RequestForModificationDate," + " " +
     "DATE_FORMAT(PD.RequestForCompletionDate, '%d-%m-%Y') as RequestForCompletionDate,PD.Placeofimplement," + " " +
     "IFNULL(Date_Format(ProjectStartDate,'%Y-%m-%d'),0) as ProjectStartDate,IFNULL(Date_Format(ProjectEndDate,'%Y-%m-%d'),0) as ProjectEndDate,isImpact_Project,ifnull(PD.Amount,0) as Amount,Semester.SemName" + " " +
     "from project_description AS PD INNER JOIN student_registration AS SR on PD.Lead_Id = SR.Lead_Id" + " " +
     "INNER JOIN colleges AS CLG on SR.Collegecode = CLG.CollegeId" + " " +
     "INNER JOIN mstr_taluka AS TL on TL.Id = SR.TalukaCode" + " " +
     "INNER JOIN mstr_programme_course as course on SR.coursecode = course.courseid" + " " +
      "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
     "where PD.ManagerId='" + ManagerId.ToString() + "' and pd.ProjectStatus In ('" + ProjectStatus.ToString() + "') and pd.PDId=" + PDId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Student_GetFiveStarForCrown(string LeadId)
    {
        gstrQrystr = "SELECT IFNULL(COUNT(PD.PDId),0) AS FiveStar, SR.Gender FROM student_registration AS SR INNER JOIN" + " " +
        "project_description AS PD ON SR.Lead_Id = PD.Lead_Id WHERE (PD.Rating = 5) and (PD.Lead_Id='" + LeadId.ToString() + "') GROUP BY SR.Gender";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void FillThemeMaster(DropDownList ddl)
    {
        gstrQrystr = "select distinct ThemeId,ThemeName from Theme where status=1 order by themename";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "ThemeId", "ThemeName");
    }

    public DataTable Student_GetStudentDetailForMailSend(string LeadId)
    {
        gstrQrystr = "select MailId,StudentName,MobileNo from student_registration where Lead_Id='" + LeadId.ToString() + "'";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Student_GetStudentEditedDetailForMail(string LeadId)
    {
        gstrQrystr = "select Distinct Studentname,DOB,aadharno,Address,MobileNo,MailId,AlternativeMobileNo,Gender,BloodGroup,Bank_Name,Branch_Name,Account_No,Account_HolderName,IFSC_code from Student_Registration where Lead_Id='" + LeadId.ToString() + "'";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Manager_UpdateProjectDetails(string Lead_Id, string ManagerId, string PDId, string ProjectStatus, string StudentName, string ProjectTitle, string CurrentSituation, string PlaceOfImplement, string TotalBeneficiaries, string Beneficiaries, string RequestedAmt, string Duration, string SanctionAmt, string ProjectObjective, string ActionPlan, string Theme, string ManagerComment, string AcademicYear, TextBox txtProposedStartDate, TextBox txtProposedEndDate)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE  project_description SET ProjectStatus='" + ProjectStatus.ToString() + "'," + ProjectStatus + "Date=now(), Title = '" + Regex.Replace(ProjectTitle, "'", "`").Trim() + "', BeneficiaryNo = " + TotalBeneficiaries.ToString() + ", Beneficiaries = '" + Beneficiaries.ToString() + "', Placeofimplement = '" + Regex.Replace(PlaceOfImplement, "'", "`").Trim() + "',SanctionAmount=" + SanctionAmt.ToString() + ",SanctionDate=Now(), Duratoin = '" + Duration.ToString() + "', Theme = '" + Theme.ToString() + "', CurrentSituation ='" + Regex.Replace(CurrentSituation, "'", "`").Trim() + "', Objectives = '" + Regex.Replace(ProjectObjective, "'", "`").Trim() + "'," + " " +
        "ActionPlan = '" + Regex.Replace(ActionPlan, "'", "`").Trim() + "', ManagerComments = '" + Regex.Replace(ManagerComment, "'", "`").Trim() + "',AcademicCode='" + AcademicYear.ToString() + "',Edited_Date=Now(),ProjectStartDate='" + txtProposedStartDate.Text.ToString() + "',ProjectEndDate='" + txtProposedEndDate.Text.ToString() + "'," + " " +
        "DeviceType='WEB' WHERE (PDId = " + PDId.ToString() + ") AND (ManagerId = " + ManagerId.ToString() + ") AND (Lead_Id = '" + Lead_Id.ToString() + "')";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE student_registration SET StudentName = '" + Regex.Replace(StudentName, "'", "`").Trim() + "' WHERE (Lead_Id = '" + Lead_Id.ToString() + "') ";
        int Status = DLobj.ExecuteQuery(gstrQrystr);

        Manager_InsertNotification(ManagerId.ToString(), Lead_Id.ToString(), "Title:" + ProjectTitle.ToString() + " " + Regex.Replace(ManagerComment, "'", "`").Trim(), ProjectStatus + " " + "Project");
        string body = "";
        if (Status > 0)
        {
            DataTable dt = Student_GetStudentEditedDetailForMail(Lead_Id.ToString());
            if (dt.Rows.Count > 0)
            {
                if(ProjectStatus== "RequestForModification")
                {
                    body = PopulateBody(StudentName.ToString(), "<b>Your project (" + ProjectTitle.ToString() + ") has been Requested for Modification </b>", "The details you entered are listed below:",
                "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>StudentName:</b> " + StudentName.ToString() + "<br><br></li><li><b>Title of the Project:</b> " + ProjectTitle.ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + TotalBeneficiaries + "<br><br></li><li><b>Beneficies:</b> " + Beneficiaries + "<br><br></li><li><b>Objectives:</b> " + ProjectObjective + "<br><br></li><li><b> Action Plan:</b> " + ActionPlan + " <br><br></li><li><b> Manager Comment:</b> " + ManagerComment + " <br><br></li><li><b>Requested Amount:</b> " + RequestedAmt.ToString() + " <br><br></li><li><b>Sanction Amount:</b> " + SanctionAmt.ToString() + " <br><br></li></ol><br><br>");
                }
                else if (ProjectStatus == "Rejected")
                {
                    body = PopulateBody(StudentName.ToString(), "<b>Your project (" + ProjectTitle.ToString() + ") has been Rejected </b>", "The details you entered are listed below:",
                "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>StudentName:</b> " + StudentName.ToString() + "<br><br></li><li><b>Title of the Project:</b> " + ProjectTitle.ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + TotalBeneficiaries + "<br><br></li><li><b>Beneficies:</b> " + Beneficiaries + "<br><br></li><li><b>Objectives:</b> " + ProjectObjective + "<br><br></li><li><b> Action Plan:</b> " + ActionPlan + " <br><br></li><li><b> Manager Comment:</b> " + ManagerComment + " <br><br></li><li><b>Requested Amount:</b> " + RequestedAmt.ToString() + " <br><br></li><li><b>Sanction Amount:</b> " + SanctionAmt.ToString() + " <br><br></li></ol><br><br>");
                }
                else
                {
                    body = PopulateBody(StudentName.ToString(), "<b>Congratulations, Your project (" + ProjectTitle.ToString() + ") has been Approved successfully </b>", "The details you entered are listed below:",
                "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>StudentName:</b> " + StudentName.ToString() + "<br><br></li><li><b>Title of the Project:</b> " + ProjectTitle.ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + TotalBeneficiaries + "<br><br></li><li><b>Beneficies:</b> " + Beneficiaries + "<br><br></li><li><b>Objectives:</b> " + ProjectObjective + "<br><br></li><li><b> Action Plan:</b> " + ActionPlan + " <br><br></li><li><b> Manager Comment:</b> " + ManagerComment + " <br><br></li><li><b>Requested Amount:</b> " + RequestedAmt.ToString() + " <br><br></li><li><b>Sanction Amount:</b> " + SanctionAmt.ToString() + " <br><br></li></ol><br><br>");
                }

              
                if (ProjectStatus == "Rejected")
                {
                    SendHtmlFormattedEmail(dt.Rows[0]["MailId"].ToString(), "" + Lead_Id.ToString() + " project Rejected successfully", body);
                }
                else if (ProjectStatus == "RequestForModification")
                {
                    SendHtmlFormattedEmail(dt.Rows[0]["MailId"].ToString(), "" + Lead_Id.ToString() + " project is requested for modification", body);
                }
                else
                {
                    SendHtmlFormattedEmail(dt.Rows[0]["MailId"].ToString(), "" + Lead_Id.ToString() + " project Approved successfully", body);
                }

            }

        }
    }
    public void Manager_RejectProject(string Lead_Id, string ManagerId, string PDId, string ProjectStatus, string StudentName, string ProjectTitle, string PlaceOfImplement, string TotalBeneficiaries, string Beneficiaries, string RequestedAmt, string SanctionAmt, string ProjectObjective, string Theme, string ManagerComment, string AcademicYear, TextBox txtProposedStartDate, TextBox txtProposedEndDate)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET ProjectStatus='" + ProjectStatus.ToString() + "', " + ProjectStatus + "Date=now(),DeviceType='WEB', ";
        gstrQrystr += "ManagerComments = '" + Regex.Replace(ManagerComment, "'", "`").Trim() + "' WHERE (PDId = " + PDId.ToString() + ")";
        int Status = DLobj.ExecuteQuery(gstrQrystr);



        Manager_InsertNotification(ManagerId.ToString(), Lead_Id.ToString(), "Title:" + ProjectTitle.ToString() + " " + Regex.Replace(ManagerComment, "'", "`").Trim(), ProjectStatus + " " + "Project");

        if (Status > 0)
        {
            DataTable dt = Student_GetStudentEditedDetailForMail(Lead_Id.ToString());
            if (dt.Rows.Count > 0)
            {
                string body = PopulateBody(StudentName.ToString(), "<b>Your project (" + ProjectTitle.ToString() + ") has been Rejected </b>", "The details you entered are listed below:",
                  "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>StudentName:</b> " + StudentName.ToString() + "<br><br></li><li><b>Title of the Project:</b> " + ProjectTitle.ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + TotalBeneficiaries + "<br><br></li><li><b>Beneficies:</b> " + Beneficiaries + "<br><br></li><li><b>Objectives:</b> " + ProjectObjective + "<br><br></li><li><b> Manager Comment:</b> " + ManagerComment + " <br><br></li><li><b>Requested Amount:</b> " + RequestedAmt.ToString() + " <br><br></li><li><b>Sanction Amount:</b> " + SanctionAmt.ToString() + " <br><br></li></ol><br><br>");
                if (ProjectStatus == "Rejected")
                {
                    SendHtmlFormattedEmail(dt.Rows[0]["MailId"].ToString(), "" + Lead_Id.ToString() + " project is  Rejected please refer the manager comments", body);
                }
                else
                {
                    SendHtmlFormattedEmail(dt.Rows[0]["MailId"].ToString(), "" + Lead_Id.ToString() + " project Approved successfully", body);
                }
            }
        }
    }
    public void Manager_UpdateIsImapactProject(string PDID, int isImpact, string Lead_Id, string ProjectTitle, string ManagerId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; Update project_description set isImpact_Project=" + isImpact.ToString() + ",ImpactDate=now()  where pdid=" + PDID.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
        string DeviceID = Common_GetDeviceID(Lead_Id.ToString());
        if (DeviceID != "")
        {
            if (isImpact == 1)
            {
                FCMPushNotification.AndroidPush(DeviceID.ToString(), "Congratulation!Your Project " + " " + ProjectTitle.ToString() + " " + "is selected as Impactful Project", "Student", "Empty");
            }
        }
        if (isImpact == 1)
        {
            Manager_SaveNotificationLog(ManagerId.ToString(), Lead_Id.ToString(), "Congratulation!Your Project " + " " + ProjectTitle.ToString() + " " + "is selected as Impactful Project", "ImpactProject", "");
        }
    }
    public void Student_UpdateCompletionProject(string LeadId, string PDId, TextBox txtPlaceofImplement, TextBox txtFundRaised, TextBox txtChallenges, TextBox txtLearning, TextBox txtStory, TextBox txtResourceUtilize, TextBox txtRCStartDate, TextBox txtRCEndDate, TextBox txtHoursSpent
        , string SDG_Goals, string Collaboration_Supported, string Permission_And_Activities, string Experience_Of_Initiative, string Lacking_initiative,
        string Against_Tide, string Cross_Hurdles, string Entrepreneurial_Venture, string Government_Awarded, string Leadership_Roles)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET Placeofimplement ='" + Regex.Replace(txtPlaceofImplement.Text, "'", "`").Trim() + "'," + " " +
        "FundsRaised = " + txtFundRaised.Text.ToString() + ", Challenge = N'" + Regex.Replace(txtChallenges.Text, "'", "`").Trim() + "'," + " " +
        "Learning =N'" + Regex.Replace(txtLearning.Text, "'", "`").Trim() + "', AsAStory = N'" + Regex.Replace(txtStory.Text, "'", "`").Trim() + "'," + " " +
        "Edited_Date=Now(),DeviceType='WEB',HoursSpend=" + txtHoursSpent.Text.ToString() + ",Resource=N'" + Regex.Replace(txtResourceUtilize.Text, "'", "`").Trim() + "' " + " " +
        ",ProjectStartDate='" + txtRCStartDate.Text.ToString() + "',ProjectEndDate='" + txtRCEndDate.Text.ToString() + "',SDG_Goals=" + SDG_Goals.ToString() + ", " + " " +
        "Collaboration_Supported='" + Regex.Replace(Collaboration_Supported.ToString(), "'", "`").Trim() + "', " + " " +
        "Permission_And_Activities='" + Regex.Replace(Permission_And_Activities.ToString(), "'", "`").Trim() + "', " + " " +
        "Experience_Of_Initiative='" + Regex.Replace(Experience_Of_Initiative.ToString(), "'", "`").Trim() + "', " + " " +
        "Lacking_initiative='" + Regex.Replace(Lacking_initiative.ToString(), "'", "`").Trim() + "', " + " " +
        "Against_Tide='" + Regex.Replace(Against_Tide.ToString(), "'", "`").Trim() + "', " + " " +
        "Cross_Hurdles='" + Regex.Replace(Cross_Hurdles.ToString(), "'", "`").Trim() + "', " + " " +
        "Entrepreneurial_Venture='" + Regex.Replace(Entrepreneurial_Venture.ToString(), "'", "`").Trim() + "', " + " " +
        "Government_Awarded='" + Regex.Replace(Government_Awarded.ToString(), "'", "`").Trim() + "', " + " " +
        "Leadership_Roles='" + Regex.Replace(Leadership_Roles.ToString(), "'", "`").Trim() + "', " + " " +
        "WHERE (PDId = " + PDId.ToString() + ")";
        DLobj.ExecuteQuery(gstrQrystr);

        Student_InsertNotification(LeadId.ToString(), "Update of ReApply", "Updated ReApply Project (Student)");
    }
    public void Student_FillSDG_GoalsListBox(ListBox lst)
    {
        gstrQrystr = "select slno,goals from sdg_goals where status=1";
        DLobj.FillListbox(lst, gstrQrystr, "slno", "goals");
    }
    public void Student_UpdateCompletionStatus(string LeadId, string PDId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET ProjectStatus='RequestForCompletion',RequestForCompletionDate=Now(),Edited_Date=Now(),DeviceType='WEB' WHERE (PDId = " + PDId.ToString() + ")";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Student_UpdateCompletionProjectSaveAsDraft(string LeadId, string PDId, TextBox txtPlaceofImplement, TextBox txtFundRaised, TextBox txtChallenges, TextBox txtLearning, TextBox txtStory, TextBox txtResourceUtilize, int Progress, TextBox txtRCStartDate, TextBox txtRCEndDate, TextBox txtHoursSpend)
    {
        int fundRaised = 0;
        int HoursSpend = 0;
        if (txtFundRaised.Text == "")
        {
            fundRaised = 0;
        }
        else
        {
            fundRaised = int.Parse(txtFundRaised.Text.ToString());
        }
        if (txtHoursSpend.Text == "")
        {
            HoursSpend = 0;
        }
        else
        {
            HoursSpend = int.Parse(txtHoursSpend.Text.ToString());
        }
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET Placeofimplement ='" + Regex.Replace(txtPlaceofImplement.Text, "'", "`").Trim() + "', " + " " +
        "FundsRaised = " + fundRaised.ToString() + ", Challenge = N'" + Regex.Replace(txtChallenges.Text, "'", "`").Trim() + "'," + " " +
        "Learning =N'" + Regex.Replace(txtLearning.Text, "'", "`").Trim() + "', AsAStory = N'" + Regex.Replace(txtStory.Text, "'", "`").Trim() + "'," + " " +
        "ProjectStatus='Draft',RequestForCompletionDate=Now(),Edited_Date=Now(),DeviceType='WEB',Resource=N'" + Regex.Replace(txtResourceUtilize.Text, "'", "`").Trim() + "'," + " " +
        "CompletionProgress=" + Progress.ToString() + ",HoursSpend=" + HoursSpend.ToString() + " " +
        ",ProjectStartDate='" + txtRCStartDate.Text.ToString() + "',ProjectEndDate='" + txtRCEndDate.Text.ToString() + "'" + " " +
        " WHERE  (PDId = " + PDId.ToString() + ")";
        DLobj.ExecuteQuery(gstrQrystr);

        Student_InsertNotification(LeadId.ToString(), "Drafted Completion Form", "Completion Project Drafted(Student)");
    }
    public DataSet Student_GetCompletionDetailBeforeCompletion(string LeadId, string PDId)
    {
        gstrQrystr = "SELECT Title, BeneficiaryNo, Placeofimplement, FundsRaised, Challenge, Learning, AsAStory, Amount," + " " +
        "SanctionAmount,Objectives,Resource,IFNULL(Date_Format(ProjectStartDate,'%Y-%m-%d'),0) as ProjectStartDate," + " " +
        "IFNULL(Date_Format(ProjectEndDate,'%Y-%m-%d'),0) as ProjectEndDate,ifnull(HoursSpend,0) as HoursSpend," + " " +
        "ifnull(TotalResourses,0) as TotalResourses FROM project_description WHERE(PDId = " + PDId.ToString() + ") AND(Lead_Id = '" + LeadId.ToString() + "')";
        return DLobj.GetDataSet(gstrQrystr);
    }
    public DataTable Student_GetCompletionDetailBeforeCompletion_NEW(string PDId)
    {
        gstrQrystr = "SELECT Title, BeneficiaryNo, Placeofimplement, FundsRaised, Challenge, Learning, AsAStory, Amount," + " " +
        "SanctionAmount,Objectives,Resource,IFNULL(Date_Format(ProjectStartDate,'%Y-%m-%d'),0) as ProjectStartDate," + " " +
        "IFNULL(Date_Format(ProjectEndDate,'%Y-%m-%d'),0) as ProjectEndDate,ifnull(HoursSpend,0) as HoursSpend," + " " +
        "ifnull(SDG_Goals,'') as SDG_Goals,ifnull(Collaboration_Supported,'') as Collaboration_Supported," + " " +
        "ifnull(Permission_And_Activities, '') as Permission_And_Activities,ifnull(Experience_Of_Initiative, '') as Experience_Of_Initiative," + " " +
        "ifnull(Lacking_initiative, '') as Lacking_initiative,ifnull(Against_Tide, '') as Against_Tide,ifnull(Cross_Hurdles, '') as Cross_Hurdles," + " " +
        "ifnull(Entrepreneurial_Venture, '') as Entrepreneurial_Venture,ifnull(Government_Awarded, '') as Government_Awarded," + " " +
        "ifnull(Leadership_Roles, '') as Leadership_Roles,ifnull(TotalResourses,0) as TotalResourses FROM project_description WHERE (PDId = " + PDId.ToString() + ")";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public string Student_ProjectIsImpact(string PDID)
    {
        gstrQrystr = "Select isImpact_Project from project_description where PDId=" + PDID.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            return "0";
        }

    }
    public DataTable Common_GetProjectSDG_Goals(string PDID)
    {
        gstrQrystr = "select sg.slno,sg.goals from project_sdg_details as psd inner join sdg_goals as sg on psd.Sdg_Id=sg.slno " + " " +
        "where pdid=" + PDID.ToString() + " and sg.status=1;";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Commong_SaveProjectSDG_Goals(string PDID, ListBox lst)
    {
        int i = 0;

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Delete from project_sdg_details where pdid=" + PDID.ToString();
        DLobj.ExecuteQuery(gstrQrystr);
        foreach (ListItem item in lst.Items)
        {
            if (item.Selected)
            {
                if (i > 0)
                {
                    if (item.Value != "18")
                    {
                        gstrQrystr = "Insert into project_sdg_details(pdid,Sdg_Id) values(" + PDID.ToString() + "," + item.Value + ")";
                        DLobj.ExecuteQuery(gstrQrystr);
                    }
                }
                else
                {
                    gstrQrystr = "Insert into project_sdg_details(pdid,Sdg_Id) values(" + PDID.ToString() + "," + item.Value + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }

                i++;
            }
        }
    }
    public void Student_GetDocuments(string LeadId, string PDId, string DocumentType, DataList DL)
    {
        if (DocumentType == "IMG")
        {
            gstrQrystr = "select * from project_digital_documents where LeadId='" + LeadId.ToString() + "' and PDId=" + PDId.ToString() + "  and Document_Type='" + DocumentType.ToString() + "' order by  Document_Type desc";
        }
        else if (DocumentType == "DOC")
        {
            gstrQrystr = "select slno,LeadId,PDId,Document_Id,REPLACE(Document_Path, '~', '') as Document_Path,Document_Type,Created_Date,Created_By from project_digital_documents" + " " +
        "where LeadId='" + LeadId.ToString() + "' and PDId=" + PDId.ToString() + "  and Document_Type='" + DocumentType.ToString() + "' order by  slno asc";
        }


        DL.DataSource = DLobj.GetDataTable(gstrQrystr);
        DL.DataBind();
    }
    public void Student_GetDocuments_NEW(string PDId, string DocumentType, DataList DL)
    {
        if (DocumentType == "IMG")
        {
            gstrQrystr = "select * from project_digital_documents where PDId=" + PDId.ToString() + "  and Document_Type='" + DocumentType.ToString() + "' order by  Document_Type desc";
        }
        else if (DocumentType == "DOC")
        {
            gstrQrystr = "select slno,LeadId,PDId,Document_Id,REPLACE(Document_Path, '~', '') as Document_Path,Document_Type,Created_Date,Created_By from project_digital_documents" + " " +
        "where PDId=" + PDId.ToString() + "  and Document_Type='" + DocumentType.ToString() + "' order by  slno asc";
        }


        DL.DataSource = DLobj.GetDataTable(gstrQrystr);
        DL.DataBind();
    }
    public DataSet Manager_GetStudentProjectCompletionDetails(string LeadID, string PDId)
    {
        gstrQrystr = "select Title,objectives,BeneficiaryNo,ActualBeneficiaryNo,placeofimplement,amount,SanctionAmount,theme," + " " +
        "challenge,Learning,AsAStory,Resource,TotalResourses,rating,ManagerComments,IFNULL(Date_Format(ProjectStartDate,'%Y-%m-%d'),0) as ProjectStartDate," + " " +
        "IFNULL(Date_Format(ProjectEndDate,'%Y-%m-%d'),0) as ProjectEndDate,InnovativeRating,LeadershipRating,RiskTakenRating,ImpactRating," + " " +
        "FundRaisedRating,ifnull(HoursSpend,0) as HoursSpend,ifnull(Project_Levels,0) as Project_Levels from project_description where Lead_Id='" + LeadID.ToString() + "' and PDId=" + PDId.ToString() + " ";

        return DLobj.GetDataSet(gstrQrystr);

    }
    public DataTable Manager_GetStudentProjectCompletionDetails_NEW(string LeadID, string PDId)
    {
        gstrQrystr = "select Title,objectives,BeneficiaryNo,ActualBeneficiaryNo,placeofimplement,amount,SanctionAmount,theme," + " " +
        "challenge,Learning,AsAStory,Resource,TotalResourses,rating,ManagerComments,IFNULL(Date_Format(ProjectStartDate,'%Y-%m-%d'),0) as ProjectStartDate," + " " +
        "IFNULL(Date_Format(ProjectEndDate,'%Y-%m-%d'),0) as ProjectEndDate,InnovativeRating,LeadershipRating,RiskTakenRating,ImpactRating," + " " +
        "FundRaisedRating,ifnull(HoursSpend,0) as HoursSpend,ifnull(Project_Levels,0) as Project_Levels,SR.StudentName,SR.MobileNo,PD.isImpact_Project,   " + " " +
         "ifnull(SDG_Goals,' ') as SDG_Goals,ifnull(Collaboration_Supported,' ') as Collaboration_Supported," + " " +
        "ifnull(Permission_And_Activities, ' ') as Permission_And_Activities,ifnull(Experience_Of_Initiative, ' ') as Experience_Of_Initiative," + " " +
        "ifnull(Lacking_initiative, ' ') as Lacking_initiative,ifnull(Against_Tide, ' ') as Against_Tide,ifnull(Cross_Hurdles, ' ') as Cross_Hurdles," + " " +
        "ifnull(Entrepreneurial_Venture, ' ') as Entrepreneurial_Venture,ifnull(Government_Awarded, ' ') as Government_Awarded," + " " +
        "ifnull(Leadership_Roles, ' ') as Leadership_Roles,ifnull(TotalResourses,0) as TotalResourses,Semester.SemName" + " " +
        "from project_description as PD inner join student_registration as SR on  PD.lead_id=SR.lead_id " + " " +
         "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
        "where PDId=" + PDId.ToString() + " ";

        return DLobj.GetDataTable(gstrQrystr);

    }
    public void Manager_SaveCompletionWithRating(vmResponseCompletionSave obj, ListBox LstSDG_Goals)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE project_description SET ActualBeneficiaryNo=" + obj.ActualBeneficier + ",Placeofimplement = '" + Regex.Replace(obj.placeofimplement, "'", "`").Trim() + "', Theme = '" + obj.Theme.ToString() + "', Challenge =N'" + Regex.Replace(obj.challenge, "'", "`").Trim() + "', Learning = N'" + Regex.Replace(obj.Learning, "'", "`").Trim() + "', AsAStory =N'" + Regex.Replace(obj.AsAStory, "'", "`").Trim() + "', Resource = N'" + obj.Resource + "',TotalResourses=" + obj.ResourceUtilizedAmount + "" + " " +
        ",InnovativeRating=" + obj.InnovativeRating + ",LeadershipRating=" + obj.LeadershipRating + ",RiskTakenRating=" + obj.RiskTaenRating + ",ImpactRating=" + obj.ImpactRating + ",FundRaisedRating=" + obj.FundRaisedRating + ",Rating = " + obj.rating + "," + " " +
        "ManagerComments = N'" + Regex.Replace(obj.ManagerComments, "'", "`").Trim() + "',ProjectStatus='Completed',CompletedDate=Now(),Edited_Date=Now(),DeviceType='WEB' " + " " +
        ",Project_Levels='" + obj.StudentLevel.ToString() + "',Collaboration_Supported='" + Regex.Replace(obj.Collaboration_Supported, "'", "`").Trim() + "', " + " " +
        "Permission_And_Activities=' " + Regex.Replace(obj.Permission_And_Activities.ToString(), "'", "`").Trim() + "',Experience_Of_Initiative='" + Regex.Replace(obj.Experience_Of_Initiative.ToString(), "'", "`").Trim() + "', " + " " +
        "Lacking_initiative='" + Regex.Replace(obj.Lacking_initiative.ToString(), "'", "`").Trim() + "',Against_Tide='" + Regex.Replace(obj.Against_Tide.ToString(), "'", "`").Trim() + "', " + " " +
        "Cross_Hurdles='" + Regex.Replace(obj.Cross_Hurdles.ToString(), "'", "`").Trim() + "',Entrepreneurial_Venture='" + Regex.Replace(obj.Entrepreneurial_Venture.ToString(), "'", "`").Trim() + "',  " + " " +
        "Government_Awarded='" + Regex.Replace(obj.Government_Awarded.ToString(), "'", "`").Trim() + "',Leadership_Roles='" + Regex.Replace(obj.Leadership_Roles.ToString(), "'", "`").Trim() + "'  " + " " +
         "WHERE (PDId = " + obj.PDId + ")";
        int Status = DLobj.ExecuteQuery(gstrQrystr);

        Commong_SaveProjectSDG_Goals(obj.PDId, LstSDG_Goals);
        Manager_InsertNotification(obj.ManagerId, obj.LeadId.ToString(), "Title:" + " " + obj.Title + " " + obj.ManagerComments, "Completed Project (Manager) with " + obj.rating + " Rating");
        if (Status > 0)
        {
            DataTable dt = Student_GetStudentEditedDetailForMail(obj.LeadId);
            if (dt.Rows.Count > 0)
            {
                string body = PopulateBody(dt.Rows[0]["Studentname"].ToString(), "<b>Congratulations, Your project (" + obj.Title.ToString() + ") has been completed successfully </b>", "The details you entered are listed below:",
                    "<ol><li><b>LEAD Id:</b> " + obj.LeadId.ToString() + "<br><br></li><li><b>StudentName:</b> " + dt.Rows[0]["Studentname"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + obj.Title + "<br><br></li><li><b>BeneficiaryNo:</b> " + obj.ActualBeneficier + "<br><br></li><li><b>Placeofimplement:</b> " + obj.placeofimplement + "<br><br></li><li><b> challenge:</b> " + obj.challenge + " <br><br></li><li><b> Learning:</b> " + obj.Learning + " <br><br></li><li><b> AsAStory:</b> " + obj.AsAStory + " <br><br></li><li><b>Resource:</b> " + obj.Resource + " <br><br></li><li><b>Resource Utilized Amount:</b> " + obj.ResourceUtilizedAmount + " <br><br></li><li><b>AsAStory:</b> " + obj.AsAStory + " <br><br></li><li><b>Rating:</b> " + obj.rating + " <br><br></li><li><b>ManagerComments:</b> " + obj.ManagerComments + " <br><br></li></ol><br><br>");

                SendHtmlFormattedEmail(dt.Rows[0]["MailId"].ToString(), "" + obj.LeadId.ToString() + " project completed successfully", body);
            }

        }
    }

    public void Manager_ChangeStudentTypeAfterProjectComplete(string Lead_Id, string ManagerId)
    {
        gstrQrystr = "select IFNULL(count(PDId),0) as PDCount from project_description where Lead_Id='" + Lead_Id.ToString() + "' and ProjectStatus='Completed'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        int count = 0;
        count = int.Parse(dt.Rows[0].ItemArray[0].ToString());
        if ((count > 0) && (count == 1))
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE student_registration SET Student_Type = 'Initiator' WHERE (Lead_Id = '" + Lead_Id.ToString() + "') AND (Student_Type = 'Student')";
            DLobj.ExecuteQuery(gstrQrystr);
            Student_InsertNotification(Lead_Id.ToString(), "Congratulation! You have completed your first project successfully", "Initiator");
            //Student_InsertNotification(Lead_Id.ToString(), "Congratulation!You are now eligible to apply for T-shirt", "T-shirt");
            string DeviceID = Common_GetDeviceID(Lead_Id.ToString());
            if (DeviceID != "")
            {
                FCMPushNotification.AndroidPush(DeviceID.ToString(), "Congratulation! You have completed your first project successfully", "Student", "Empty");
            }


        }

    }
    public void Manager_ChangeStudentTypeforOldStudents(string Lead_id, string ManagerId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; UPDATE student_registration SET Student_Type = 'Leader' WHERE (Lead_Id = '" + Lead_id.ToString() + "') AND (Student_Type = 'Student')";
        DLobj.ExecuteQuery(gstrQrystr);
    }

    public int Manager_CheckMangerIsExists(string MobileNo, string MailId)
    {
        gstrQrystr = "select ManagerId from manager_details where MobileNo ='" + MobileNo.ToString() + "' and MailId='" + MailId.ToString() + "'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return int.Parse(dt.Rows[0].ItemArray[0].ToString());
        }
        else
        {
            return 0;
        }

    }
    public vmMailNotification Student_GetProjectDetailsForMailAndNotifications(string PDId)
    {
        vmMailNotification vmMN = new vmMailNotification();
        gstrQrystr = "select Lead_Id,Title,BeneficiaryNo,Beneficiaries,Placeofimplement,Duratoin,CurrentSituation,Objectives," + " " +
        "ActionPlan,ManagerComments,Challenge,Learning,AsAStory,ProposedDate,ApprovedDate,CompletedDate,RejectedDate," + " " +
        "RequestForModificationDate,RequestForCompletionDate, Amount as RequestedAmount,SanctionAmount,ProjectStatus,FundsReceived" + " " +
        "from project_description where PDId=" + PDId.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        vmMN.LeadId = dt.Rows[0].ItemArray[0].ToString();
        vmMN.Title = dt.Rows[0].ItemArray[1].ToString();
        vmMN.BeneficiaryNo = dt.Rows[0].ItemArray[2].ToString();
        vmMN.Beneficiaries = dt.Rows[0].ItemArray[3].ToString();
        vmMN.PlaceofImplement = dt.Rows[0].ItemArray[4].ToString();
        vmMN.Duration = dt.Rows[0].ItemArray[5].ToString();
        vmMN.CurrentSituation = dt.Rows[0].ItemArray[6].ToString();
        vmMN.Objectives = dt.Rows[0].ItemArray[7].ToString();
        vmMN.ActionPlan = dt.Rows[0].ItemArray[8].ToString();
        vmMN.ManagerComments = dt.Rows[0].ItemArray[9].ToString();
        vmMN.Challenges = dt.Rows[0].ItemArray[10].ToString();
        vmMN.Learning = dt.Rows[0].ItemArray[11].ToString();
        vmMN.AsAStory = dt.Rows[0].ItemArray[12].ToString();
        vmMN.ProposedDate = dt.Rows[0].ItemArray[13].ToString();
        vmMN.CompletedDate = dt.Rows[0].ItemArray[14].ToString();
        vmMN.RejectedDate = dt.Rows[0].ItemArray[15].ToString();
        vmMN.RequestForModificationDate = dt.Rows[0].ItemArray[16].ToString();
        vmMN.RequestForCompletionDate = dt.Rows[0].ItemArray[17].ToString();
        vmMN.RequestedAmount = dt.Rows[0].ItemArray[18].ToString();
        vmMN.SanctionAmount = dt.Rows[0].ItemArray[19].ToString();
        vmMN.ProjectStatus = dt.Rows[0].ItemArray[20].ToString();
        vmMN.FundsReceived = dt.Rows[0].ItemArray[21].ToString();
        return vmMN;
    }
    public void Student_GetLeftProfileDetails(string LeadId, Label lblStudentName, Label lblCollegeName, Label lblLeadId, Label lblStatus, Label lblRegistrationDate)
    {

        gstrQrystr = "SELECT StudentName,DATE_FORMAT(STR_TO_DATE(RegistrationDate,'%Y-%m-%d' ),'%d-%m-%Y' ) as RegistrationDate,Student_Type,College_Name" + " " +
        "FROM student_registration LEFT OUTER JOIN colleges ON student_registration.CollegeCode = colleges.CollegeId WHERE (Lead_Id = '" + LeadId.ToString() + "')";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        lblStudentName.Text = dt.Rows[0].ItemArray[0].ToString();
        lblRegistrationDate.Text = dt.Rows[0].ItemArray[1].ToString();
        //  lblStatus.Text = dt.Rows[0].ItemArray[2].ToString();
        lblCollegeName.Text = dt.Rows[0].ItemArray[3].ToString();
        lblLeadId.Text = LeadId.ToString();


    }
    public DataTable Student_GetBadges(string Lead_Id)
    {
        gstrQrystr = "Select ifnull(LLP_Badges,0) as LLP_Badges,ifnull(Prayana_Badges,0) as Prayana_Badges," + " " +
        "ifnull(Yuva_Badges,0) as LLP_Badges,ifnull(Valedicotry_Badges,0) as Valedicotry_Badges from student_registration where lead_id='" + Lead_Id.ToString() + "'";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Student_GetManagerDetails(string ManagerId, Label lblManagerName, Label lblManagerMailId, Label lblManagerMobileNo, Image img, System.Web.UI.HtmlControls.HtmlAnchor btnFacebook, System.Web.UI.HtmlControls.HtmlAnchor btnTwitter, System.Web.UI.HtmlControls.HtmlAnchor btnInstaGram, System.Web.UI.HtmlControls.HtmlAnchor btnWhatsApp)
    {
        gstrQrystr = "SELECT ManagerName, MailId, MobileNo,Image_Path,Facebook,Twitter,InstaGram,WhatsApp FROM manager_details WHERE (ManagerId = " + ManagerId.ToString() + ") AND(Status = 1)";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            lblManagerName.Text = dt.Rows[0].ItemArray[0].ToString();
            lblManagerMailId.Text = dt.Rows[0].ItemArray[1].ToString();
            lblManagerMobileNo.Text = dt.Rows[0].ItemArray[2].ToString();
            img.ImageUrl = dt.Rows[0].ItemArray[3].ToString();

            if (dt.Rows[0].ItemArray[4].ToString() == "")
            {
                btnFacebook.Visible = false;
            }
            else
            {
                btnFacebook.Visible = true;
                btnFacebook.InnerHtml = "<span class='fa fa-facebook'></span>";
                btnFacebook.Attributes.Add("class", "btn btn-facebook animated btn-floating");
                btnFacebook.HRef = dt.Rows[0].ItemArray[4].ToString();
            }




            if (dt.Rows[0].ItemArray[5].ToString() == "")
            {
                btnTwitter.Visible = false;
            }
            else
            {
                btnTwitter.Visible = true;
                btnTwitter.InnerHtml = "<span class='fa fa-twitter'></span>";
                btnTwitter.Attributes.Add("class", "btn btn-primary animated btn-floating");
                btnTwitter.HRef = dt.Rows[0].ItemArray[5].ToString();
            }



            if (dt.Rows[0].ItemArray[6].ToString() == "")
            {
                btnInstaGram.Visible = false;
            }
            else
            {
                btnInstaGram.Visible = true;
                btnInstaGram.InnerHtml = "<span class='fa fa-instagram'></span>";
                btnInstaGram.Attributes.Add("class", "btn btn-danger animated btn-floating");
                btnInstaGram.HRef = dt.Rows[0].ItemArray[6].ToString();
            }



            if (dt.Rows[0].ItemArray[7].ToString() == "")
            {
                btnWhatsApp.Visible = false;
            }
            else
            {
                btnWhatsApp.Visible = true;
                btnWhatsApp.InnerHtml = "<span class='fa fa-whatsapp'></span>";
                btnWhatsApp.Attributes.Add("class", "btn btn-success animated btn-floating");
                btnWhatsApp.HRef = "https://web.whatsapp.com/send?phone=91" + dt.Rows[0].ItemArray[7].ToString();
            }




        }

        //MySqlDataReader dr = DLobj.GetReader(gstrQrystr);
        //if (dr.Read())
        //{
        //    lblManagerName.Text = dr[0].ToString();
        //    lblManagerMailId.Text = dr[1].ToString();
        //    lblManagerMobileNo.Text = dr[2].ToString();
        //    img.ImageUrl = dr[3].ToString();

        //    if(dr[4].ToString()=="")
        //    {
        //        btnFacebook.Visible = false;
        //    }
        //    else
        //    {
        //        btnFacebook.Visible = true;
        //    }

        //    btnFacebook.Text = "<span class='fa fa-facebook'></span>";
        //    btnFacebook.Attributes.Add("class", "btn btn-facebook animated btn-floating");
        //    btnFacebook.PostBackUrl = dr[4].ToString();

        //    if (dr[5].ToString() == "")
        //    {
        //        btnTwitter.Visible = false;
        //    }
        //    else
        //    {
        //        btnTwitter.Visible = true;
        //    }

        //    btnTwitter.Text = "<span class='fa fa-twitter'></span>";
        //    btnTwitter.Attributes.Add("class", "btn btn-primary animated btn-floating");
        //    btnTwitter.PostBackUrl = dr[5].ToString();

        //    if (dr[6].ToString() == "")
        //    {
        //        btnInstaGram.Visible = false;
        //    }
        //    else
        //    {
        //        btnInstaGram.Visible = true;
        //    }

        //    btnInstaGram.Text = "<span class='fa fa-instagram'></span>";
        //    btnInstaGram.Attributes.Add("class", "btn btn-danger animated btn-floating");
        //    btnInstaGram.PostBackUrl = dr[6].ToString();



        //}


    }

    public void Manager_UpdateMeterialDetails(string PDId, string LeadId, Repeater rpt)
    {
        int Sum = 0;
        gstrQrystr = "Select * from project_meterial_details where PDId=" + PDId.ToString() + "";
        bool PDIdExists = DLobj.ReturnTF(gstrQrystr);
        if (PDIdExists == true)
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  delete from project_meterial_details where PDId=" + PDId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
            foreach (RepeaterItem item in rpt.Items)
            {
                string MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text;
                string MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text;
                if ((MeterialName != "") || (MeterialCost != ""))
                {
                    Sum += int.Parse(MeterialCost.ToString());
                    gstrQrystr = "INSERT INTO project_meterial_details(PDId, Lead_Id, MeterialName, MeterialCost)" + " " +
                    "VALUES(" + PDId.ToString() + ", '" + LeadId.ToString() + "', '" + Regex.Replace(MeterialName.ToString(), "'", "`").Trim() + "', " + MeterialCost.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }
        }
        else
        {
            foreach (RepeaterItem item in rpt.Items)
            {

                string MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text;
                string MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text;
                if ((MeterialName != "") || (MeterialCost != ""))
                {
                    Sum += int.Parse(MeterialCost.ToString());
                    gstrQrystr = "INSERT INTO project_meterial_details(PDId, Lead_Id, MeterialName, MeterialCost)" + " " +
                    "VALUES(" + PDId.ToString() + ", '" + LeadId.ToString() + "', '" + Regex.Replace(MeterialName.ToString(), "'", "`").Trim() + "', " + MeterialCost.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }

            }
        }
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE project_description SET Amount = " + Sum.ToString() + ",DeviceType='WEB' WHERE (PDId = " + PDId.ToString() + ")";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Manager_GetMeterialDetails(string PDId, string LeadId, Repeater rptMeterial)
    {
        gstrQrystr = "SELECT MeterialName, MeterialCost, slno FROM project_meterial_details WHERE(PDId = " + PDId.ToString() + ") AND(Lead_Id = '" + LeadId.ToString() + "') order by slno";
        rptMeterial.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptMeterial.DataBind();
    }
    public void Manager_GetMeterialDetails_NEW(string PDId, Repeater rptMeterial)
    {
        gstrQrystr = "SELECT MeterialName, MeterialCost, slno FROM project_meterial_details WHERE(PDId = " + PDId.ToString() + ")  order by slno";
        rptMeterial.DataSource = DLobj.GetDataTable(gstrQrystr);
        rptMeterial.DataBind();
    }

    public void GetEventDetailsRepeater(Repeater rpt)
    {
        gstrQrystr = "SELECT  EventId, EventName,Date_Format(EventFromDate,'%d-%m-%Y') as EventFromDate,Date_Format(EventToDate,'%d-%m-%Y') as EventToDate,mstr_state.StateName,ForAllStates, mstr_events.Status,Image_Path " + " " +
        "FROM mstr_Events  LEFT OUTER JOIN mstr_state ON mstr_Events.statecode = mstr_state.code order by createDate desc";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void GetEventProgrammeRepeater(Repeater rpt)
    {
        gstrQrystr = "select distinct slno, event_name, event_description, Date_Format(Event_FromDate,'%d-%m-%Y') as EventFromDate,Date_Format(Event_ToDate, '%d-%m-%Y') as EventToDate,Event_Fees,Event_FirstPayment,Event_ImagePath from mstr_events_programme where status = 1 order by Event_FromDate desc";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void Manager_GetEventDetails(Repeater rpt)
    {
        gstrQrystr = "SELECT  EventId, EventName,EventFromDate,EventToDate,mstr_state.StateName,ForAllStates, mstr_state.Status,Image_Path " + " " +
        "FROM mstr_Events  LEFT OUTER JOIN mstr_state ON mstr_Events.statecode = mstr_state.code";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void SaveEventDetails(TextBox txtEventName, TextBox txtEventDescription, TextBox txtEventFromDate, TextBox txtEventToDate, string State, TextBox txtEventURL, TextBox txtEventApplyURL, string CreateBy, int ForAll, FileUpload fu, string FilePath, string FileExtenssion, string ClickEvent, string EventId)
    {

        if (fu.HasFile)
        {

            if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
            {

                if (ClickEvent == "New")
                {
                    fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
                }
                else
                {
                    if (fu.HasFile)
                    {
                        gstrQrystr = "select Image_Path from mstr_events where EventId=" + EventId.ToString() + "";
                        DataTable dt = DLobj.GetDataTable(gstrQrystr);
                        if (dt.Rows.Count > 0)
                        {
                            FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dt.Rows[0].ItemArray[0].ToString()));
                            if (file.Exists)
                            {
                                file.Delete();
                            }

                        }


                        fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
                    }


                    // }




                }
            }
        }

        if (ClickEvent == "New")
        {
            gstrQrystr = "INSERT INTO mstr_Events (Eventname, EventFromDate, EventToDate, EventDescription, EventURL, EventApplyURL, StateCode, CreateDate, CreatedBy, ForAllStates)" + " " +
            "VALUES('" + txtEventName.Text.ToString() + "', '" + txtEventFromDate.Text.ToString() + "', '" + txtEventToDate.Text.ToString() + "', N'" + Regex.Replace(txtEventDescription.Text.ToString(), "'", "`").Trim() + "', N'" + txtEventURL.Text.ToString() + "', '" + txtEventApplyURL.Text.ToString() + "', " + State.ToString() + ", now(), " + CreateBy + ", " + ForAll.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);



            gstrQrystr = "select EventId from mstr_Events order by eventid desc";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            string GetEventId = dt.Rows[0].ItemArray[0].ToString();
            if (fu.HasFile)
            {

                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_Events SET Image_Path = '" + FilePath.ToString() + "' WHERE (EventId = " + GetEventId.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }

        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_Events SET Eventname = '" + Regex.Replace(txtEventName.Text.ToString(), "'", "`").Trim() + "', EventFromDate = '" + txtEventFromDate.Text.ToString() + "', EventToDate = '" + txtEventToDate.Text.ToString() + "', EventDescription = N'" + txtEventDescription.Text.ToString() + "', EventURL = '" + txtEventURL.Text.ToString() + "'," + " " +
                   "EventApplyURL = '" + txtEventApplyURL.Text.ToString() + "', StateCode =" + State.ToString() + ", ForAllStates =" + ForAll.ToString() + " WHERE (EventId = " + EventId.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);
            if (fu.HasFile)
            {

                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_Events SET Image_Path = '" + FilePath.ToString() + "' WHERE (EventId = " + EventId.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }
        }

    }
    public void SaveEvent_Programme_Details(TextBox txtEventName, TextBox txtEventDescription, TextBox txtEventFromDate, TextBox txtEventToDate, string State, TextBox txtEventFees, TextBox txtEventFirstPay, string CreateBy, FileUpload fu, string FilePath, string FileExtenssion, string ClickEvent, string EventId)
    {

        if (fu.HasFile)
        {

            if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
            {

                if (ClickEvent == "New")
                {
                    fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
                }
                else
                {
                    if (fu.HasFile)
                    {
                        gstrQrystr = "select Image_Path from mstr_events_programme where slno=" + EventId.ToString() + "";
                        DataTable dt = DLobj.GetDataTable(gstrQrystr);
                        if (dt.Rows.Count > 0)
                        {
                            FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dt.Rows[0].ItemArray[0].ToString()));
                            if (file.Exists)
                            {
                                file.Delete();
                            }

                        }


                        fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
                    }


                    // }




                }
            }
        }

        if (ClickEvent == "New")
        {
            string EventFees = txtEventFees.Text.ToString();
            gstrQrystr = "INSERT INTO mstr_events_programme (Event_name,Event_description, Event_FromDate, Event_ToDate, Event_Fees, Event_FirstPayment, StateCode, CreateDate, CreatedBy)" + " " +
            "VALUES( '" + Regex.Replace(txtEventName.Text.ToString(), "'", "`").Trim() + "', '" + Regex.Replace(txtEventDescription.Text.ToString(), "'", "`").Trim() + "', '" + txtEventFromDate.Text.ToString() + "', '" + txtEventToDate.Text.ToString() + "', N'" + Regex.Replace(txtEventDescription.Text.ToString(), "'", "`").Trim() + "', N'" + txtEventFees.Text.ToString() + "', '" + txtEventFirstPay.Text.ToString() + "', '" + State.ToString() + "', now(), " + CreateBy + ")";
            DLobj.ExecuteQuery(gstrQrystr);
            gstrQrystr = "select slno from mstr_events_programme where status=1 order by slno desc";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            string GetEventId = dt.Rows[0].ItemArray[0].ToString();
            if (fu.HasFile)
            {

                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_events_programme SET Event_ImagePath = '" + FilePath.ToString() + "' WHERE (slno = " + GetEventId.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }

        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_events_programme SET Event_name = '" + Regex.Replace(txtEventName.Text.ToString(), "'", "`").Trim() + "', Event_FromDate = '" + txtEventFromDate.Text.ToString() + "', Event_ToDate = '" + txtEventToDate.Text.ToString() + "', Event_Description = N'" + txtEventDescription.Text.ToString() + "', Event_Fees = '" + txtEventFees.Text.ToString() + "'," + " " +
                   "Event_FirstPayment = '" + txtEventFirstPay.Text.ToString() + "', StateCode ='" + State.ToString() + "' WHERE (slno = " + EventId.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);
            if (fu.HasFile)
            {

                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_events_programme SET Event_ImagePath = '" + FilePath.ToString() + "' WHERE (slno = " + EventId.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }
        }

    }
    public DataTable Admin_GetEventProgrammeStateCode(string EventId)
    {
        gstrQrystr = "select StateCode from mstr_events_programme where slno=1 ";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Student_GetEventDetail(Repeater rpt)
    {
        gstrQrystr = "select distinct EventId,EventName,EventDescription,Image_path,date_format(EventFromDate,'%d-%M-%Y') as FromDate,date_format(EventToDate,'%d-%M-%Y') as ToDate,EventApplyURL,EventURL  from mstr_events where Status=1 order by DATE_FORMAT(EventFromDate, '%d-%m-%Y %h:%i:%s') desc";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }
    public void Student_GetParticularEventDetails(string EventId, Label lblFromDate, Label lblToDate, Image img, Label lblEventName, Label lblEventDescription, LinkButton btnApplyNow)
    {
        gstrQrystr = "Select distinct EventId,EventName,EventDescription,Image_path,date_format(EventFromDate,'%d-%M-%Y') as FromDate,date_format(EventToDate,'%d-%M-%Y') as ToDate,EventApplyURL  from mstr_events where EventId=" + EventId.ToString() + " and Status=1 order by DATE_FORMAT(EventFromDate, '%d-%m-%Y %h:%i:%s')  desc";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {


            lblEventName.Text = dt.Rows[0].ItemArray[1].ToString();
            lblEventDescription.Text = dt.Rows[0].ItemArray[2].ToString();
            img.ImageUrl = dt.Rows[0].ItemArray[3].ToString();
            lblFromDate.Text = dt.Rows[0].ItemArray[4].ToString();
            lblToDate.Text = dt.Rows[0].ItemArray[5].ToString();
            btnApplyNow.PostBackUrl = dt.Rows[0].ItemArray[6].ToString();
        }

    }
    public DataTable Admin_GetEventsDetailsForEdit(string EventId)
    {
        gstrQrystr = "SELECT  Eventname, Date_format(EventFromDate,'%Y-%m-%d') as EventFromDate,date_format(EventToDate,'%Y-%m-%d') as EventToDate, EventDescription, EventURL, EventApplyURL, StateCode, ForAllStates, Image_Path" + " " +
        "FROM mstr_Events WHERE(EventId = " + EventId.ToString() + ")";
        return DLobj.GetDataTable(gstrQrystr);

    }
    public DataTable Admin_GetEvents_Programme_DetailsForEdit(string EventId)
    {
        gstrQrystr = "SELECT  Event_name, Date_format(Event_FromDate,'%Y-%m-%d') as EventFromDate,date_format(Event_ToDate,'%Y-%m-%d') as EventToDate, Event_Description, Event_Fees, Event_FirstPayment, Event_ImagePath,StateCode" + " " +
        "FROM mstr_events_programme WHERE(slno = " + EventId.ToString() + ")";
        return DLobj.GetDataTable(gstrQrystr);

    }
    public void Manager_GetFundingDetails(string PDId, string ManagerId, Repeater rpt, Label lblStudentName, Label lblTitle, Label lblRequestedAmount, Label lblSanctionAmount, Label lblTotalGiveAmount, Label lblBalanceAmount)
    {
        gstrQrystr = "select fund_id,Amount,GivenDate,ManagerRemark,ReceivedStatus from project_fund_details where PDId = " + PDId.ToString() + " and ManagerId = " + ManagerId.ToString() + "";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();

        gstrQrystr = "SELECT SD.StudentName,PD.Title,IFNULL(PD.Amount, 0) AS RequestedAmount,IFNULL(PD.SanctionAmount, 0) AS SanctionAmount,IFNULL(SUM(PFD.Amount),0) AS giventotal," + " " +
        "IFNULL((SanctionAmount -  IFNULL(SUM(PFD.Amount),0)),0) as Balance FROM project_description PD Left Outer join student_registration SD ON PD.Lead_Id = SD.Lead_Id" + " " +
        "LEFT JOIN project_fund_details PFD ON PD.PDId = PFD.PDId where PD.PDId = " + PDId.ToString() + " and PD.ManagerId=" + ManagerId.ToString() + "";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            lblStudentName.Text = dt.Rows[0].ItemArray[0].ToString();
            lblTitle.Text = dt.Rows[0].ItemArray[1].ToString();
            lblRequestedAmount.Text = dt.Rows[0].ItemArray[2].ToString();
            lblSanctionAmount.Text = dt.Rows[0].ItemArray[3].ToString();
            lblTotalGiveAmount.Text = dt.Rows[0].ItemArray[4].ToString();
            lblBalanceAmount.Text = dt.Rows[0].ItemArray[5].ToString();


        }

    }

    public void Manager_GetFundingDetailWithDataBound(string PDId, string LeadId, LinkButton btnPayee, string ManagerId, Label lblDisperse, Label lblBalace)
    {
        gstrQrystr = "SELECT IFNULL(PD.Amount, 0) AS RequestedAmount,IFNULL(PD.SanctionAmount, 0) AS SanctionAmount,IFNULL(SUM(PFD.Amount),0) AS giventotal," + " " +
       "IFNULL((SanctionAmount -  IFNULL(SUM(PFD.Amount),0)),0) as Balance,PD.ProjectStatus FROM project_description PD Left Outer join student_registration SD ON PD.Lead_Id = SD.Lead_Id" + " " +
       "LEFT JOIN project_fund_details PFD ON PD.PDId = PFD.PDId where PD.PDId = " + PDId.ToString() + " and PD.ManagerId=" + ManagerId.ToString() + "";

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            int requestedAmount = int.Parse(dt.Rows[0].ItemArray[0].ToString());
            int SanctionAmount = int.Parse(dt.Rows[0].ItemArray[1].ToString());
            int TotalGivenAmount = int.Parse(dt.Rows[0].ItemArray[3].ToString());
            int BalanceAmount = int.Parse(dt.Rows[0].ItemArray[2].ToString());
            string ProjectStatus = dt.Rows[0].ItemArray[4].ToString();

            lblDisperse.Text = TotalGivenAmount.ToString();
            lblBalace.Text = BalanceAmount.ToString();
            if ((requestedAmount == 0) || (requestedAmount > 0) && (SanctionAmount == 0))
            {
                btnPayee.Attributes.Add("class", "hidden");
            }
            else if ((requestedAmount > 0) && (BalanceAmount > 0))
            {
                btnPayee.Text = "<span class='fa fa-edit'></span>";
                btnPayee.Attributes.Add("class", "btn btn-warning btn-floating");
            }
            else if ((requestedAmount > 0) && (SanctionAmount > 0))
            {
                btnPayee.Text = "<span class='fa fa fa-rupee'></span>";
                btnPayee.Attributes.Add("class", "btn btn-info btn-floating");

            }
        }
    }
    public void Manager_SaveFundDetails(string PDId, string ManagerId, string LeadId, TextBox txtAmount, TextBox txtManagerComments)
    {
        gstrQrystr = "insert into project_fund_details(PDId,ManagerId,LeadId,Amount,GivenDate,ManagerRemark,academiccode,DeviceType)" + " " +
        "Values(" + PDId.ToString() + "," + ManagerId.ToString() + ",'" + LeadId.ToString() + "'," + txtAmount.Text + ",Now(),'" + Regex.Replace(txtManagerComments.Text, "'", "`").Trim() + "',(select slno from academicyear where status=1 order by slno desc limit 1),'WEB')";
        DLobj.ExecuteQuery(gstrQrystr);

        Manager_InsertNotification(ManagerId.ToString(), LeadId.ToString(), txtManagerComments.ToString(), "Funded Successfully");
    }
    public DataTable Student_GetStudentDetailAfterEditProfile(string Lead_Id)
    {
        string gstrQrystr = "select studentname,mobileno,mailid,statecode,districtcode,talukacode,streamcode,coursecode,semcode,collegecode,address,alternativemobileno,DOB,Gender,BloodGroup,AadharNo," + " " +
       "Bank_Name,account_no,IFSC_Code,Account_HolderName,Branch_Name,RegistrationDate as Edited_Date,isprofileedit,DeviceType from student_registration where lead_id = '" + Lead_Id.ToString() + "' ";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public int Student_GetStudentType(string LeadId)
    {
        gstrQrystr = "select count(pdid) as TotalProject from project_description where Lead_Id='" + LeadId.ToString() + "'";
        int StudentType = 0;
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {

            StudentType = int.Parse(dt.Rows[0].ItemArray[0].ToString());
        }
        return StudentType;


    }
    public DataTable Manager_FillCollegeForSMS(string ManagerId)
    {
        gstrQrystr = "SELECT mstr_taluka.Taluk_Name,mstr_taluka.Id as TalukId, colleges.CollegeId, colleges.College_Name FROM manager_colleges INNER JOIN" + " " +
        "colleges ON manager_colleges.CollegeCode = colleges.CollegeId INNER JOIN mstr_taluka ON colleges.TalukId = mstr_taluka.Id" + " " +
         "WHERE(manager_colleges.ManagerCode = " + ManagerId.ToString() + ") AND(colleges.Status = 1) order by College_Name";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }
    public DataTable Manager_FillStudentsCollegeWiseForSMSMail(string ManagerId, string CollegeCode, string StudentType, string ProjectStatus)
    {


        gstrQrystr = "SELECT RegistrationId, Lead_Id, StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
        "WHERE (ManagerCode =" + ManagerId.ToString() + ") and  (CollegeCode IN(" + CollegeCode.ToString() + ")) AND (Student_Type ='" + StudentType.ToString() + "') and (ActiveStatus = 1) and (ProjectStatus='" + ProjectStatus.ToString() + "') ORDER BY StudentName";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }
    public DataTable Admin_FillStudentManagerWise(string ManagerId, string StudentType, string ProjectStatus, string AcademicCode)
    {
        if ((AcademicCode.ToString() == "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
            "WHERE (ManagerCode =" + ManagerId.ToString() + ")  ORDER BY StudentName asc";
        }
        else if ((AcademicCode.ToString() != "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
       "WHERE (ManagerCode =" + ManagerId.ToString() + ") and (project_description.academicCode=" + AcademicCode.ToString() + ")    ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() == "[All]") && (StudentType.ToString() != "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
     "WHERE (ManagerCode =" + ManagerId.ToString() + ") AND (Student_Type ='" + StudentType.ToString() + "')   ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() == "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() != "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
     "WHERE (ManagerCode =" + ManagerId.ToString() + ") AND (ProjectStatus='" + ProjectStatus.ToString() + "')   ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() != "[All]") && (StudentType.ToString() != "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
     "WHERE (ManagerCode =" + ManagerId.ToString() + ")  AND (Student_Type ='" + StudentType.ToString() + "') and (project_description.academicCode=" + AcademicCode.ToString() + ")   ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() != "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() != "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
     "WHERE (ManagerCode =" + ManagerId.ToString() + ")  AND (ProjectStatus='" + ProjectStatus.ToString() + "') and (project_description.academicCode=" + AcademicCode.ToString() + ")   ORDER BY StudentName asc";
        }

        else
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
      "WHERE (ManagerCode =" + ManagerId.ToString() + ") AND (Student_Type ='" + StudentType.ToString() + "') and (ActiveStatus = 1) and (ProjectStatus='" + ProjectStatus.ToString() + "') and (project_description.academicCode=" + AcademicCode.ToString() + ")  ORDER BY StudentName asc";
        }

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }
    public DataTable Manager_FillStudentsCollegeWiseForSMSMail(string ManagerId, string CollegeCode, string StudentType, string ProjectStatus, string AcademicCode)
    {
        if ((AcademicCode.ToString() == "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            //     gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
            //"WHERE (ManagerCode =" + ManagerId.ToString() + ") and  (CollegeCode IN(" + CollegeCode.ToString() + "))   ORDER BY StudentName";

            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration " + " " +
            "WHERE student_registration.isProfileEdit=1 and (ManagerCode =" + ManagerId.ToString() + ")  ORDER BY StudentName asc";
        }
        if ((AcademicCode.ToString() == "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() == "[All]") && (ManagerId == "[All]"))
        {
            //     gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
            //"WHERE (ManagerCode =" + ManagerId.ToString() + ") and  (CollegeCode IN(" + CollegeCode.ToString() + "))   ORDER BY StudentName";

            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id where student_registration.isProfileEdit=1" + " " +
            "ORDER BY StudentName asc";
        }
        else if ((AcademicCode.ToString() != "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() == "[All]") && (ManagerId == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration" + " " +
         "where student_registration.isProfileEdit=1 and (AcademicCode=" + AcademicCode.ToString() + ")    ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() != "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration" + " " +
       "WHERE student_registration.isProfileEdit=1 and (ManagerCode =" + ManagerId.ToString() + ") and (AcademicCode=" + AcademicCode.ToString() + ")    ORDER BY StudentName";
        }

        else if ((AcademicCode.ToString() == "[All]") && (StudentType.ToString() != "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration" + " " +
     "WHERE student_registration.isProfileEdit=1 and (Student_Type ='" + StudentType.ToString() + "')   ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() == "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() != "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
     "WHERE student_registration.isProfileEdit=1 and (ManagerCode =" + ManagerId.ToString() + ") AND (ProjectStatus='" + ProjectStatus.ToString() + "')   ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() != "[All]") && (StudentType.ToString() != "[All]") && (ProjectStatus.ToString() == "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration" + " " +
       "WHERE (ManagerCode =" + ManagerId.ToString() + ")  AND (Student_Type ='" + StudentType.ToString() + "') and (academicCode=" + AcademicCode.ToString() + ") and student_registration.isProfileEdit=1   ORDER BY StudentName";
        }
        else if ((AcademicCode.ToString() != "[All]") && (StudentType.ToString() == "[All]") && (ProjectStatus.ToString() != "[All]"))
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
     "WHERE (ManagerCode =" + ManagerId.ToString() + ")  AND (ProjectStatus='" + ProjectStatus.ToString() + "') and (project_description.academicCode=" + AcademicCode.ToString() + ") and student_registration.isProfileEdit=1   ORDER BY StudentName asc";
        }

        else
        {
            gstrQrystr = "SELECT Distinct student_registration.RegistrationId, student_registration.Lead_Id,StudentName as StudentName, MobileNo, MailId FROM student_registration INNER JOIN project_description ON student_registration.Lead_Id = project_description.Lead_Id" + " " +
      "WHERE (ManagerCode =" + ManagerId.ToString() + ") AND (Student_Type ='" + StudentType.ToString() + "') and (ActiveStatus = 1) and (ProjectStatus='" + ProjectStatus.ToString() + "') and (project_description.academicCode=" + AcademicCode.ToString() + ") and student_registration.isProfileEdit=1  ORDER BY StudentName asc";
        }

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }
    public void Manager_UpdateSocialURL(string ManagerId, string Facebook, string Twitter, string InstaGram, string WhatsApp)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update manager_details set Facebook=N'" + Facebook.ToString() + "',Twitter='" + Twitter.ToString() + "',InstaGram='" + InstaGram.ToString() + "',WhatsApp='" + WhatsApp.ToString() + "' where ManagerId=" + ManagerId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public DataTable Manager_GetSocialURL(string ManagerId)
    {
        gstrQrystr = "Select Facebook,Twitter,InstaGram,WhatsApp from manager_details where ManagerId=" + ManagerId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }

    public void SendMailException(string Method, string NewException, string PageName, string lead_id, string mobile_no)
    {
        string ExceptionEmail = ConfigurationManager.AppSettings["ExceptionEmail"];
        if (!NewException.Contains("Thread was being aborted") && !NewException.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server") && !NewException.Contains("Object reference not set to an instance of an object.") && !NewException.Contains("The parameter 'address' cannot be an empty string."))
        {
            if (ExceptionEmail.ToUpper() == "TRUE")
            {
                //Email = "Kalplanath.tech@dfmail.org";
                string Subject = "Regarding Exception Caught";
                string Message = "From Method - " + Method + ",\n";
                Message = Message + "Lead ID - " + lead_id + ",\n";
                Message = Message + "Mobile NO. - " + mobile_no + ",\n";
                Message = Message + " From Page Name :" + PageName + ",\n";
                Message = Message + " Exception is : " + NewException + ",\n";
                Message = Message + "Process it soon";
                string senderID = "leadmis@dfmail.org";
                const string senderPassword = "leadcampusadmin";
                try
                {
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                        Timeout = 30000,
                    };
                    string Email = "sharad.noolvi@dfmail.org";  //,sunil.tech@dfmail.org
                    MailMessage message = new MailMessage(senderID, Email, Subject, Message);
                    smtp.Send(message);

                }
                catch (Exception)
                {

                }

            }
            else
            {
                //Email = "Kalplanath.tech@dfmail.org";
                string Subject = "Regarding Exception Caught";
                string Message = "From Method - " + Method + ",\n";
                Message = Message + "Lead ID - " + lead_id + ",\n";
                Message = Message + "Mobile NO. - " + mobile_no + ",\n";
                Message = Message + " From Page Name :" + PageName + ",\n";
                Message = Message + " Exception is : " + NewException + ",\n";
                Message = Message + "Process it soon";
                string senderID = "leadmis@dfmail.org";
                const string senderPassword = "leadcampusadmin";
                try
                {
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                        Timeout = 30000,
                    };
                    string Email = "sharad.noolvi@dfmail.org";
                    MailMessage message = new MailMessage(senderID, Email, Subject, Message);
                    smtp.Send(message);

                }
                catch (Exception)
                {

                }
            }
        }
    }
    public string Send_Multiple_SMS(string MobileNo, string Message, string Lead_Id, string StudentType)
    {
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.campusconnect.co/api/web2sms.php?workingkey=A6a2f9a0287c81d9dfa2c15cdfb489987&to=" + MobileNo + "&sender=LCLEAD&message=" + Message);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            return responseString;
        }
        catch (Exception ex)
        {
            //lbl_sres.Text = "Looks like we are having some problem. Please retry. ";
            SendMailException("send_sms", ex.ToString(), "Send_Mail2.aspx", Lead_Id.ToString(), MobileNo.ToString());
            return "";
        }
    }
    public void Manager_SaveNotificationLog(string ManagerId, string LeadId, string Message, string Type, string Response)
    {
        gstrQrystr = "insert into notification_log(ManagerCode,Lead_Id,Message,Type,Response,CreateDate,CreatedBy,Edited_By,Edited_Date)" + " " +
        "values(" + ManagerId.ToString() + ", '" + LeadId.ToString() + "', N'" + Regex.Replace(Message.ToString(), "'", "`").Trim() + "','" + Type.ToString() + "','" + Response.ToString() + "', Now(), " + ManagerId.ToString() + ", " + ManagerId.ToString() + ", Now())";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Manager_SaveScheduleMessaging(string ScheduleId, string ScheduleDescription, string ScheduleType, string ScheduleMessage, string ProjectType, string StudentType, string ScheduleDate, string Created_By, string ActionType, string CollegeId)
    {
        if (ActionType == "NEW")
        {
            gstrQrystr = "insert into schedule_messaging (Description,Schedule_Message,Parameter_Type,Schedule_Type,Student_Type,Schedule_Date,User_Type,Created_By,Created_Date,AcademicYear,College_Id)" + " " +
           "values('" + ScheduleDescription.ToString() + "', '" + ScheduleMessage.ToString() + "', '" + ProjectType.ToString() + "', '" + ScheduleType.ToString() + "', '" + StudentType.ToString() + "', '" + ScheduleDate.ToString() + "', 'Manager', " + Created_By.ToString() + ", now()," + " " +
           "(select slno from academicyear where status=1 order by slno desc limit 1)," + CollegeId.ToString() + ")";
        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update schedule_messaging set Description='" + ScheduleDescription.ToString() + "'," + " " +
            "Schedule_Message='" + ScheduleMessage.ToString() + "',Parameter_Type='" + ProjectType.ToString() + "'," + " " +
            "Schedule_Type='" + ScheduleType.ToString() + "',Student_Type='" + StudentType.ToString() + "'," + " " +
            "Schedule_Date='" + ScheduleDate.ToString() + "',College_Id=" + CollegeId.ToString() + ",User_Type='Manager',Edited_By=" + Created_By.ToString() + ",Edited_Date= now()" + " " +
            "where slno=" + ScheduleId.ToString() + "";
        }
        DLobj.ExecuteQuery(gstrQrystr);

    }
    public DataTable Manager_GetScheduleMessage(string UserId, string repeater, string AcademicYear)
    {
        if (repeater == "Schedule")
        {
            gstrQrystr = "SELECT slno as ScheduleId,DATE_FORMAT(Schedule_Date, '%Y-%m-%d %r') as Schedule_Date," + " " +
       "(case when Status=1 then TIMEDIFF(Schedule_Date,now()) else hour(schedule_date) end) as Spent_Time,Description," + " " +
       "Schedule_Message,Schedule_Type,Parameter_Type,Status from schedule_messaging where Created_By=" + UserId.ToString() + " and Status=1 and AcademicYear=" + AcademicYear.ToString() + "" + " " +
       "order by DATE_FORMAT(Schedule_Date, '%Y-%m-%d %r') desc";
        }
        else if (repeater == "CompletedSchedule")
        {
            gstrQrystr = "SELECT slno as ScheduleId,DATE_FORMAT(Schedule_Date, '%Y-%m-%d %r') as Schedule_Date," + " " +
      "(case when Status=1 then TIMEDIFF(Schedule_Date,now()) else hour(schedule_date) end) as Spent_Time,Description," + " " +
      "Schedule_Message,Schedule_Type,Parameter_Type,Status from schedule_messaging where Created_By=" + UserId.ToString() + " and Status=2 and AcademicYear=" + AcademicYear.ToString() + "" + " " +
      "order by DATE_FORMAT(Schedule_Date, '%Y-%m-%d %r') desc";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }

    public void Manager_DisableScheduleMessage(string ManagerId, string ScheduleId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update schedule_messaging set Status=3 where created_By=" + ManagerId.ToString() + " and slno=" + ScheduleId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public DataTable Manager_GetScheduleforEdit(string ManagerId, string ScheduleId)
    {
        gstrQrystr = "SELECT Student_Type,Parameter_Type,date_format(Schedule_Date,'%Y-%m-%d %T') as ScheduleDate,Schedule_Type,College_Id,description," + " " +
        "Schedule_Message,TIME_FORMAT(Schedule_Date,'%p') as ScheduleDate1 FROM schedule_messaging where created_by = " + ManagerId.ToString() + " and slno = " + ScheduleId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Admin_SaveLeadStoryVideo(string Slno, string EditorCode, string txtStoryTitle, string txtStoryBody, string EditStatus, string URL, string Story_Type, string VideReadMoreURL)
    {
        if (EditStatus != "Edit")
        {
            gstrQrystr = "INSERT INTO mstr_lead_story (Story_Title, Story_Description, Created_Date, Create_By,Story_Type,URL_Link,Video_Story_URL)" + " " +
          "VALUES('" + Regex.Replace(txtStoryTitle.ToString(), "'", "`").Trim() + "', N'" + Regex.Replace(txtStoryBody.ToString(), "'", "`").Trim() + "',Now(), " + EditorCode.ToString() + "," + Story_Type.ToString() + ",'" + URL.ToString() + "','" + VideReadMoreURL.ToString() + "')";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_lead_story SET Story_Title = '" + Regex.Replace(txtStoryTitle.ToString(), "'", "`").Trim() + "', Story_Description = N'" + Regex.Replace(txtStoryBody.ToString(), "'", "`").Trim() + "', Created_Date = Now(), Create_By = " + EditorCode.ToString() + ",Story_Type=" + Story_Type.ToString() + ",URL_Link='" + URL.ToString() + "',Video_Story_URL='" + VideReadMoreURL.ToString() + "' WHERE (slno = " + Slno.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);
        }
    }
    public void Admin_SaveLeadStory(string slno, string EditorCode, string txtStoryTitle, string txtStoryBody, string EditStatus, FileUpload fu, string FilePath, string FileExtenssion, int Story_Type, FileUpload CardImgfu, string CardImgFileExtenssion, string CardImgFilePath, string URL)
    {
        if (EditStatus != "Edit")
        {
            int dbSlno = 0;
            gstrQrystr = "INSERT INTO mstr_lead_story (Story_Title, Story_Description, Created_Date, Create_By,Story_Type,URL_Link)" + " " +
            "VALUES('" + Regex.Replace(txtStoryTitle.ToString(), "'", "`").Trim() + "', N'" + Regex.Replace(txtStoryBody.ToString(), "'", "`").Trim() + "',Now(), " + EditorCode.ToString() + "," + Story_Type.ToString() + ",'" + URL.ToString() + "')";
            DLobj.ExecuteQuery(gstrQrystr);
            gstrQrystr = "Select slno from mstr_lead_story order by slno desc limit 1";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            dbSlno = int.Parse(dt.Rows[0].ItemArray[0].ToString());
            if (fu.HasFile)
            {
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_lead_story set Image_Path='" + FilePath.ToString() + "' where slno=" + dbSlno.ToString() + "";
                    DLobj.ExecuteQuery(gstrQrystr);

                }
            }
            if (CardImgfu.HasFile)
            {
                if ((CardImgFileExtenssion == ".png") || (CardImgFileExtenssion == ".PNG") || (CardImgFileExtenssion == ".jpeg") || (CardImgFileExtenssion == ".JPEG") || (CardImgFileExtenssion == ".Jpeg") || (CardImgFileExtenssion == ".JPG") || (CardImgFileExtenssion == ".jpg") || (CardImgFileExtenssion == ".Jpg"))
                {
                    CardImgfu.SaveAs(HttpContext.Current.Server.MapPath(CardImgFilePath.ToString()));
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_lead_story set Card_Image_Path='" + CardImgFilePath.ToString() + "' where slno=" + dbSlno.ToString() + "";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }

        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_lead_story SET Story_Title = '" + Regex.Replace(txtStoryTitle.ToString(), "'", "`").Trim() + "', Story_Description = N'" + Regex.Replace(txtStoryBody.ToString(), "'", "`").Trim() + "', Created_Date = Now(), Create_By = " + EditorCode.ToString() + ",Story_Type=" + Story_Type.ToString() + ",URL_Link='" + URL.ToString() + "' WHERE (slno = " + slno.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);
            if (fu.HasFile)
            {
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    gstrQrystr = "select Image_Path from mstr_lead_story where slno=" + slno.ToString() + "";

                    DataTable dt = DLobj.GetDataTable(gstrQrystr);
                    FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dt.Rows[0].ItemArray[0].ToString()));
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_lead_story SET Image_Path='" + FilePath.ToString() + "', Created_Date = Now(), Create_By = " + EditorCode.ToString() + "  WHERE (slno = " + slno.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }
            if (CardImgfu.HasFile)
            {
                if ((CardImgFileExtenssion == ".png") || (CardImgFileExtenssion == ".PNG") || (CardImgFileExtenssion == ".jpeg") || (CardImgFileExtenssion == ".JPEG") || (CardImgFileExtenssion == ".Jpeg") || (CardImgFileExtenssion == ".JPG") || (CardImgFileExtenssion == ".jpg") || (CardImgFileExtenssion == ".Jpg"))
                {
                    gstrQrystr = "select Card_Image_Path from mstr_lead_story where slno=" + slno.ToString() + "";

                    DataTable dt = DLobj.GetDataTable(gstrQrystr);
                    FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dt.Rows[0].ItemArray[0].ToString()));
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    CardImgfu.SaveAs(HttpContext.Current.Server.MapPath(CardImgFilePath.ToString()));
                    gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_lead_story SET Card_Image_Path='" + CardImgFilePath.ToString() + "', Created_Date = Now(), Create_By = " + EditorCode.ToString() + "  WHERE (slno = " + slno.ToString() + ")";
                    DLobj.ExecuteQuery(gstrQrystr);
                }
            }
        }
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public DataTable Admin_LoadStoriesRepeater(string EditorCode)
    {
        gstrQrystr = "SELECT slno, Story_Title,status,Created_Date FROM mstr_lead_story WHERE (Create_By = " + EditorCode.ToString() + ") ORDER BY Created_Date DESC";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }
    public DataTable Admin_GetStoryFromSlno(string Slno, string EditorCode)
    {
        gstrQrystr = "SELECT Story_Title,Story_Description,Image_Path,Card_Image_Path,URL_Link,Story_Type,Status,Video_Story_URL FROM  mstr_lead_story WHERE (slno = " + Slno.ToString() + ") AND (Create_By = " + EditorCode.ToString() + ") ORDER BY DATE_FORMAT(Created_Date, '%d-%m-%Y %h:%i:%s')  DESC";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }

    public void Admin_UpdateImagePathAndUpload(FileUpload fu, string FilePath, string FileExtenssion, string Slno)
    {
        if (fu.HasFile)
        {
            gstrQrystr = "select Image_Path from mstr_lead_story where slno=" + Slno.ToString() + "";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            if (dt.Rows.Count > 0)
            {
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dt.Rows[0].ItemArray[0].ToString()));
                if (file.Exists)
                {
                    file.Delete();
                }

            }

            if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
            {

                fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
                gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_lead_story SET Image_Path ='" + FilePath.ToString() + "' WHERE (slno = " + Slno.ToString() + ")";
                DLobj.ExecuteQuery(gstrQrystr);
            }
        }

    }
    public void Admin_UpdateCardImagePathAndUpload(FileUpload fu, string FilePath, string FileExtenssion, string Slno)
    {
        if (fu.HasFile)
        {
            gstrQrystr = "select Card_Image_Path from mstr_lead_story where slno=" + Slno.ToString() + "";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            if (dt.Rows.Count > 0)
            {
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(dt.Rows[0].ItemArray[0].ToString()));
                if (file.Exists)
                {
                    file.Delete();
                }

            }
            fu.SaveAs(HttpContext.Current.Server.MapPath(FilePath.ToString()));
            if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
            {
                gstrQrystr = "SET SQL_SAFE_UPDATES=0;  UPDATE mstr_lead_story SET Card_Image_Path ='" + FilePath.ToString() + "' WHERE (slno = " + Slno.ToString() + ")";
                DLobj.ExecuteQuery(gstrQrystr);
            }
        }

    }
    public void Admin_UpdateStoryStatus(string Slno, string Status)
    {
        if (Status == "0")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_lead_story SET Status=1 where status=0 and slno=" + Slno.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_lead_story SET Status=0 where status=1 and slno=" + Slno.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
    }
    public void Admin_DeleteStories(string Slno)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Delete from mstr_lead_story where slno=" + Slno.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Admin_UpdateEventStatus(string EventId, string Status)
    {
        if (Status == "0")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_events SET Status=1 where status=0 and EventId=" + EventId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_events SET Status=0 where status=1 and EventId=" + EventId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
    }
    public void Admin_DeleteEvent(string EventId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Delete from mstr_events where EventId=" + EventId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Admin_UpdateEvent_Programme(string EventId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update mstr_events_programme set status=0 where slno=" + EventId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public DataTable Student_GetMasterLeaderLevelUpdate(string LeadId)
    {
        gstrQrystr = "SELECT IFNULL(COUNT(PD.PDID),0) AS TotalProjects,IFNULL(TIMESTAMPDIFF(MONTH, SR.RegistrationDate, Now()),0) as MemberFrom," + " " +
        "SR.Student_Type,IFNULL(TIMESTAMPDIFF(MONTH, SR.MasterLeaderDate, Now()),0) as MasterLeaderDate," + " " +
        "SR.isApply_MasterLeader,SR.isApply_LeadAmbassador,ifnull(sum(PD.isImpact_Project),0) as ImpactProjectcount from student_registration AS SR INNER JOIN" + " " +
        "project_description AS PD ON SR.Lead_Id = PD.Lead_Id  where SR.Lead_Id = '" + LeadId.ToString() + "' and PD.ProjectStatus='Completed'";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Student_GetStudentLevels(string LeadId)
    {
        gstrQrystr = "SELECT Levels FROM student_levels where Lead_Id='" + LeadId.ToString() + "'";
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Student_GetTshirtLevel(string LeadId)
    {
        gstrQrystr = "select ifnull(count(pdid),0) as PDID,isRequestForTShirt,isSanctionForTshirt from Student_Registration as SR " + " " +
        "left outer join project_description as PD on SR.lead_id = PD.lead_Id where  SR.Lead_Id='" + LeadId.ToString() + "'";

        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Student_ApplyForTshirt(string RegistrationID, string LeadId, string ManagerId, string MemberName, string Size, string MemberType, string AcademicCode, string RequestedId)
    {
        if (RequestedId != "")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update student_tshirt_allotment SET TshirtSize='" + Size.ToString() + "',EditedDate=NOW(),EditedBy=" + RegistrationID.ToString() + ",DeviceType='WEB' where RequestedId=" + RequestedId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {
            gstrQrystr = "Insert into student_tshirt_allotment(RegistrationId,Lead_Id,ManagerId,RequestedDate,MemberName,TshirtSize,MemberType,AcademicCode,Status,DeviceType)" + " " +
           "Values(" + RegistrationID.ToString() + ",'" + LeadId.ToString() + "', " + ManagerId.ToString() + ", now(),'" + MemberName.ToString() + "', '" + Size.ToString() + "','" + MemberType.ToString() + "', " + AcademicCode.ToString() + ",'Requested','WEB')";
            DLobj.ExecuteQuery(gstrQrystr);

            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update Student_Registration SET isRequestForTShirt=1,DeviceType='WEB' where Lead_Id='" + LeadId.ToString() + "'";
            DLobj.ExecuteQuery(gstrQrystr);

            Manager_InsertNotification(ManagerId.ToString(), LeadId.ToString(), Size + "- size Tshirt Requested", LeadId.ToString() + " Tshirt Request");
        }

    }
    public void Student_MultipleTshirtRequestApply(string RegistrationID, string LeadId, string ManagerId, string MemberName, string Size, string MemberType, string AcademicCode, string RequestedId, string ReapplyReson)
    {
        if (RequestedId != "")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update student_tshirt_allotment SET TshirtSize='" + Size.ToString() + "',ReapplyReson='" + ReapplyReson.ToString() + "',EditedDate=NOW(),EditedBy=" + RegistrationID.ToString() + ",ReapplyReson='" + ReapplyReson.ToString() + "',DeviceType='WEB' where RequestedId=" + RequestedId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {
            gstrQrystr = "Insert into student_tshirt_allotment(RegistrationId,Lead_Id,ManagerId,RequestedDate,MemberName,TshirtSize,MemberType,AcademicCode,Status,ReapplyReson,DeviceType)" + " " +
           "Values(" + RegistrationID.ToString() + ",'" + LeadId.ToString() + "', " + ManagerId.ToString() + ", now(),'" + MemberName.ToString() + "', '" + Size.ToString() + "','" + MemberType.ToString() + "', " + AcademicCode.ToString() + ",'Requested','" + ReapplyReson.ToString() + "','WEB')";
            DLobj.ExecuteQuery(gstrQrystr);


        }

    }
    public DataTable Student_GetMultipleTshirtRequestRepeater(string Lead_id)
    {
        gstrQrystr = "select requestedId,Lead_id,managerid,Date_format(RequestedDate,'%d-%m-%Y') as RequestedDate,TshirtSize,Date_format(SanctionDate,'%d-%m-%Y') as SanctionDate,Date_format(RejectedDate,'%d-%m-%Y') as RejectedDate,Date_format(ExchangeDate,'%d-%m-%Y') as ExchangeDate,Remark,SanctionStatus,reapplyreson,Status from student_tshirt_allotment where Lead_id ='" + Lead_id.ToString() + "'";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Student_GetTshirtAppliedDetails(string LeadId)
    {
        gstrQrystr = "select RequestStatus,SanctionStatus,Tshirtsize,RequestedId from student_tshirt_allotment where Lead_Id='" + LeadId.ToString() + "' order by RequestedId desc";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Student_GetMultipleTshirtAppliedDetails(string RequestedId)
    {
        gstrQrystr = "select RequestStatus,SanctionStatus,Tshirtsize,RequestedId,ReapplyReson from student_tshirt_allotment where RequestedId=" + RequestedId.ToString() + " order by RequestedId desc";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetTshirtAllList(string ManagerId, string CollegeCode)
    {
        if (CollegeCode.ToString() != "[All]")
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,SR.studentName,Colleges.college_Name,ST.TshirtSize,date_format(ST.RequestedDate,'%d-%m-%Y') as RequestedDate,date_format(ST.SanctionDate,'%d-%m-%Y') as SanctionDate,date_format(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,ST.Status,ST.Remark,ST.ReapplyReson,Semester.SemName from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID Inner join Colleges on SR.CollegeCode=Colleges.CollegeId " + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
            " where RequestStatus = 1 And ST.managerid = " + ManagerId.ToString() + " and SR.CollegeCode = " + CollegeCode.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        else
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,SR.studentName,Colleges.college_Name,ST.TshirtSize,date_format(ST.RequestedDate,'%d-%m-%Y') as RequestedDate,date_format(ST.SanctionDate,'%d-%m-%Y') as SanctionDate,date_format(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,ST.Status,ST.Remark,ST.ReapplyReson,Semester.SemName from student_tshirt_allotment as ST" + " " +
           "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID Inner join Colleges on SR.CollegeCode=Colleges.CollegeId" + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
           " where RequestStatus = 1 And ST.managerid = " + ManagerId.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";

        }
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Manager_GetTshirtAllotedUsedCount(string ManagerId, string AcademicCode)
    {
        gstrQrystr = "select sum(Useds+UsedM+UsedL+UsedXL+UsedXXL) as TotalUsed,sum(Alloteds+AllotedM+AllotedL+AllotedXL+AllotedXXL) as TotalAlloted, UsedS,AllotedS,UsedM,AllotedM,UsedL,AllotedL,UsedXL,AllotedXL,UsedXXL,AllotedXXL from manager_tshirt where AcademicCode=" + AcademicCode.ToString() + " and ManagerId=" + ManagerId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetTshirtCollegeWiseRequestCount(string CollegeCode, string ManagerId)
    {

        if (CollegeCode != "[All]")
        {
            gstrQrystr = "select ST.TshirtSize,count(ST.lead_id) as Count from student_tshirt_allotment AS ST Inner Join student_registration as SR on ST.RegistrationId=SR.RegistrationId" + " " +
            "where SR.collegeCode = " + CollegeCode.ToString() + " and ST.ManagerId=" + ManagerId.ToString() + " and ST.AcademicCode=" + GetTop1AademicCode() + " and RequestStatus = 1 and SanctionStatus = 0 group by ST.Tshirtsize";
        }
        else
        {
            gstrQrystr = "select ST.TshirtSize,count(ST.lead_id) as Count from student_tshirt_allotment AS ST Inner Join student_registration as SR on ST.RegistrationId=SR.RegistrationId" + " " +
            "where ST.ManagerId=" + ManagerId.ToString() + " and RequestStatus = 1 and ST.AcademicCode=" + GetTop1AademicCode() + " and SanctionStatus = 0 group by ST.Tshirtsize";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetStudentTshirtRequestedList(string ManagerId, string Size, string CollegeCode)
    {
        if (CollegeCode != "[All]")
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.RequestedDate,'%d-%m-%Y') as RequestedDate,SR.studentName,Semester.SemName from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID" + " " +
             "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
            " where RequestStatus = 1 and SanctionStatus = 0 And ST.managerid = " + ManagerId.ToString() + " and ST.TshirtSize = '" + Size.ToString() + "' and SR.CollegeCode=" + CollegeCode.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        else
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.RequestedDate,'%d-%m-%Y') as RequestedDate,SR.studentName,Semester.SemName from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID " + " " +
               "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
            " where RequestStatus = 1 and SanctionStatus = 0 And ST.managerid = " + ManagerId.ToString() + " and ST.TshirtSize = '" + Size.ToString() + "' and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetStudentTshirtSanctionList(string ManagerId, string Size, string CollegeCode)
    {
        if (CollegeCode != "[All]")
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.SanctionDate,'%d-%m-%Y') as SanctionDate,SR.studentName,Semester.SemName from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID " + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
            " where RequestStatus = 1 and SanctionStatus = 1 And ST.managerid = " + ManagerId.ToString() + " and ST.TshirtSize = '" + Size.ToString() + "' and SR.CollegeCode=" + CollegeCode.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        else
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.SanctionDate,'%d-%m-%Y') as SanctionDate,SR.studentName,Semester.SemName from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID" + " " +
              "INNER JOIN mstr_semester as Semester on SR.SemCode = Semester.SemId" + " " +
            " where RequestStatus = 1 and SanctionStatus = 1 And ST.managerid = " + ManagerId.ToString() + " and ST.TshirtSize = '" + Size.ToString() + "' and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Manager_GetStudentTshirtRejectedCount(string ManagerId, string CollegeCode)
    {
        if (CollegeCode != "[All]")
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,SR.studentName from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID where RequestStatus = 1 and SanctionStatus = 2 And ST.managerid = " + ManagerId.ToString() + " and SR.CollegeCode=" + CollegeCode.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        else
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,SR.studentName from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID where RequestStatus = 1 and SanctionStatus = 2 And ST.managerid = " + ManagerId.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetStudentTshirtRejectedList(string ManagerId, string CollegeCode)
    {
        if (CollegeCode != "[All]")
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,SR.studentName,ST.Remark from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID where RequestStatus = 1 and SanctionStatus = 2 And ST.managerid = " + ManagerId.ToString() + " and SR.CollegeCode=" + CollegeCode.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        else
        {
            gstrQrystr = "select ST.RequestedId,ST.Lead_Id,ST.TshirtSize,date_format(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,SR.studentName,ST.Remark from student_tshirt_allotment as ST" + " " +
            "Inner join student_registration as SR on ST.LEAD_ID = SR.LEAD_ID where RequestStatus = 1 and SanctionStatus = 2 And ST.managerid = " + ManagerId.ToString() + " and ST.AcademicCode = " + GetTop1AademicCode() + "";
        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Manager_ApprovalOfStudentTshirt(string RequestedId, string Lead_Id, string ManagerId, string Size, string AcademicCode)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update Student_Registration SET isSanctionForTshirt=1,DeviceType='WEB' where Lead_id='" + Lead_Id.ToString() + "'";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = " SET SQL_SAFE_UPDATES=0; Update student_tshirt_allotment SET SanctionStatus=1,SanctionDate=Now(),Status='Approved',DeviceType='WEB' where RequestedId=" + RequestedId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);

        Manager_InsertNotification(ManagerId.ToString(), Lead_Id.ToString(), Size + " Approved Tshirt", "Tshirt Approved");

        DataTable dt = Manager_GetTshirtSizeWiseStockDetails(ManagerId.ToString(), Size.ToString(), AcademicCode);
        if (dt.Rows.Count > 0)
        {
            int ExistsingStock = int.Parse(dt.Rows[0].ItemArray[1].ToString());
            int NewStockCount = ExistsingStock + 1;
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update manager_tshirt SET Used" + Size + "=" + NewStockCount + " where AcademicCode=" + AcademicCode.ToString() + " and ManagerId=" + ManagerId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
    }
    public DataTable Manager_GetTshirtSizeWiseStockDetails(string ManagerId, string Size, string AcademicCode)
    {
        gstrQrystr = "select Alloted" + Size + ",Used" + Size + ",Alloted" + Size + "-Used" + Size + " as Balance from manager_tshirt where managerid=" + ManagerId.ToString() + " and AcademicCode=" + AcademicCode.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Manager_TshirtApprovalRollBack(string RequestedId, string Lead_Id, string ManagerId, string Size, string AcademicCode)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update student_tshirt_allotment SET SanctionStatus=0,status='ApproveRollBacked',DeviceType='WEB' where RequestedId=" + RequestedId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
        Manager_InsertNotification(ManagerId.ToString(), Lead_Id.ToString(), Size + " Approved Rejected (Manager)", "Approved Rejected");
        DataTable dt = Manager_GetTshirtSizeWiseStockDetails(ManagerId.ToString(), Size.ToString(), AcademicCode);
        if (dt.Rows.Count > 0)
        {
            int ExistsingStock = int.Parse(dt.Rows[0].ItemArray[1].ToString());
            int NewStockCount = ExistsingStock - 1;
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update manager_tshirt SET Used" + Size + "=" + NewStockCount + " where AcademicCode=" + AcademicCode.ToString() + " and ManagerId=" + ManagerId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
    }
    public void Manager_UpdateRejectTshirtStudentRequest(string RequestedId, string Remark)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update student_tshirt_allotment SET SanctionStatus=2,RejectedDate=Now(),Remark='" + Regex.Replace(Remark.ToString(), "'", "`").Trim() + "',Status='Rejected' where RequestedId=" + RequestedId.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);



    }
    public string Manager_UpdateExchangedTshirtSize(string RequestedId, string Size, string AcademicCode, string ManagerId, string NewSize, string Remark)
    {
        string Status = "";
        DataTable dt = Manager_GetTshirtSizeWiseStockDetails(ManagerId, NewSize, AcademicCode.ToString());
        if (dt.Rows[0].ItemArray[2].ToString() != "0")
        {
            int ExistsingStock = int.Parse(dt.Rows[0].ItemArray[1].ToString());
            int NewStockCount = ExistsingStock + 1;
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update manager_tshirt SET Used" + NewSize + "=" + NewStockCount + " where AcademicCode=" + AcademicCode.ToString() + " and ManagerId=" + ManagerId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);

            dt = Manager_GetTshirtSizeWiseStockDetails(ManagerId, Size, AcademicCode.ToString());
            int StockTaken = int.Parse(dt.Rows[0].ItemArray[1].ToString());
            int LatestStockCount = StockTaken - 1;
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update manager_tshirt SET Used" + Size + "=" + LatestStockCount + "  where AcademicCode=" + AcademicCode.ToString() + " and ManagerId=" + ManagerId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
            gstrQrystr = "SET SQL_SAFE_UPDATES=0; Update student_tshirt_allotment SET TshirtSize='" + NewSize.ToString() + "',Remark='" + Remark.ToString() + "',EditedDate=Now(),EditedBy=" + ManagerId.ToString() + ",Status='Exchange',DeviceType='WEB' where RequestedId=" + RequestedId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
            Status = "Exchange is Done";
        }
        else
        {
            Status = "No Stock Available";
        }
        return Status;
    }

    public void Student_InsertNotification(string Lead_id, string Title, string Type)
    {
        gstrQrystr = "Insert into notification_log(Lead_Id,Message,Type,DeviceType,createDate,Edited_Date)" + " " +
        "values('" + Lead_id.ToString() + "','" + Title.ToString() + "','" + Type.ToString() + "','WEB',NOW(),NOW())";
        DLobj.ExecuteQuery(gstrQrystr);
    }

    public void Manager_InsertNotification(string ManagerId, string Lead_id, string Title, string Type)
    {
        gstrQrystr = "Insert into notification_log(ManagerCode,Lead_Id,Message,Type,DeviceType,createDate,Edited_Date)" + " " +
        "values(" + ManagerId.ToString() + ",'" + Lead_id.ToString() + "','" + Title.ToString() + "','" + Type.ToString() + "','WEB',NOW(),NOW())";
        DLobj.ExecuteQuery(gstrQrystr);
    }

    public void Student_Login_Log(string Lead_Id)
    {
        gstrQrystr = "Insert into login_log(Lead_Id,login_date,DeviceType,Type)values('" + Lead_Id.ToString() + "',Now(),'WEB','Student')";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Student_Update_Login_Log(string Lead_Id)
    {
        gstrQrystr = "select distinct * from login_log  where lead_id='" + Lead_Id.ToString() + "' and DeviceType='WEB' order by slno desc limit 1 ";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update login_log SET logout_date=Now(),DeviceType='WEB' where slno=" + dt.Rows[0].ItemArray[0].ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Student_Update_Notification_Log(string Lead_Id)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update notification_log set status=1 where lead_id='" + Lead_Id.ToString() + "' and status=0";
        DLobj.ExecuteQuery(gstrQrystr);
    }


    public void Manager_Login_Log(string ManagerId, string User)
    {
        if (User == "Manager")
        {
            gstrQrystr = "Insert into login_log(ManagerId,login_date,DeviceType,Type)values(" + ManagerId.ToString() + ",Now(),'WEB','Manager')";
        }
        else if (User == "Admin")
        {
            gstrQrystr = "Insert into login_log(ManagerId,login_date,DeviceType,Type)values(" + ManagerId.ToString() + ",Now(),'WEB','Admin')";
        }
        else if (User == "Account")
        {
            gstrQrystr = "Insert into login_log(ManagerId,login_date,DeviceType,Type)values(" + ManagerId.ToString() + ",Now(),'WEB','Account')";
        }

        DLobj.ExecuteQuery(gstrQrystr);
    }
    [System.Web.Services.WebMethod]
    public void Manager_Update_Login_Log(string ManagerId, string User)
    {
        gstrQrystr = "select distinct * from login_log  where ManagerId=" + ManagerId.ToString() + " and DeviceType='WEB' order by slno desc limit 1 ";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update login_log SET logout_date=Now(),DeviceType='WEB' where slno=" + dt.Rows[0].ItemArray[0].ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }

    [System.Web.Services.WebMethod]
    public string Admin_Programme_Content(string ProgrammeType, string tableName)
    {
        List<object> Programme = new List<object>();


        string gstrQrystr = "select Slno,Contents from " + tableName + " " + "Where status=1";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        foreach (DataRow dr in dt.Rows)
        {
            Programme.Add(new
            {
                slno = dr[0].ToString(),
                contents = dr[1].ToString(),

            });
        }

        return (new JavaScriptSerializer().Serialize(Programme));
    }

    [System.Web.Services.WebMethod()]
    public List<GetInstruction> Admin_Save_Programme_Content(vmProgramme vmP)
    {

        string gstrQrystr = "";
        if (vmP.Event_Type == "NEW")
        {
            gstrQrystr = "Insert into mstr_programme_landingpage(Content,type) values('" + vmP.Content + "','" + vmP.Type.ToString() + "')";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {

        }


        List<GetInstruction> Instruction = new List<GetInstruction>();
        try
        {
            gstrQrystr = "select slno,content,status from mstr_programme_landingpage  Where status=1 and type='" + vmP.Type.ToString() + "' order by slno";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            foreach (DataRow dr in dt.Rows)
            {
                Instruction.Add(new GetInstruction(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString())));

            }

        }
        catch (Exception)
        {

        }

        return Instruction;
    }

    [System.Web.Services.WebMethod]
    public string NotificationAll()
    {
        System.Collections.Generic.List<object> Notification = new List<object>();
        UniversalDL DLobj = new UniversalDL();
        vmCookies cook = new vmCookies();
        try
        {

            string gstrQrystr = "select Type as Notification_Type,Message,Date_format(createDate,'%d-%m-%Y %h:%i:%s') as createDate from notification_log Where Lead_Id='" + cook.LeadId() + "' and status=1 order by Date_format(createDate,'%Y-%m-%d %h:%i:%s') desc";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);

            foreach (DataRow dr in dt.Rows)
            {
                Notification.Add(new
                {
                    Notification_Type = dr[0].ToString(),
                    Message = dr[1].ToString(),
                    createDate = dr[2].ToString(),
                });
            }

        }

        catch (Exception ex)
        {
            Notification.Add(new
            {
                error = "Error : " + ex.Message,

            });
        }

        return (new JavaScriptSerializer().Serialize(Notification));
    }

    [System.Web.Services.WebMethod()]
    public List<GetInstruction> Admin_GET_Programme_Content(string Type)
    {
        List<GetInstruction> Instruction = new List<GetInstruction>();
        try
        {
            string gstrQrystr = "select slno,content,status from mstr_programme_landingpage  Where type='" + Type.ToString() + "' order by slno";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            foreach (DataRow dr in dt.Rows)
            {
                Instruction.Add(new GetInstruction(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString())));

            }

        }
        catch (Exception)
        {

        }

        return Instruction;// (new JavaScriptSerializer().Serialize(Instruction));
    }
    [System.Web.Services.WebMethod()]
    public void Admin_Update_Programme_Content_Status(string slno, string status)
    {

        string gstrQrystr = "";
        if (status == "1")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_programme_landingpage SET Status=0 where status=1 and slno=" + slno.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update mstr_programme_landingpage SET Status=1 where status=0 and slno=" + slno.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }

    }

    [System.Web.Services.WebMethod()]
    public List<object> Admin_GET_Programme_ContentBYId(string slno)
    {
        System.Collections.Generic.List<object> Instruction = new List<object>();
        try
        {
            string gstrQrystr = "select slno,content,status,type from mstr_programme_landingpage  Where slno=" + slno.ToString() + " order by slno";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            foreach (DataRow dr in dt.Rows)
            {
                Instruction.Add(new
                {
                    slno = dr[0].ToString(),
                    contents = dr[1].ToString(),
                    Status = dr[2].ToString(),
                    Type = dr[3].ToString(),

                });


            }

        }
        catch (Exception)
        {

        }

        return Instruction;// (new JavaScriptSerializer().Serialize(Instruction));
    }

    public DataTable getlivesheet()
    {
        gstrQrystr = "select lead_id,Type,devicetype,login_date from login_log order by login_date desc limit 5";
        return DLobj.GetDataTable(gstrQrystr);

    }
    public DataTable GetYuvaSummitCertificate(string AcademicYear, string CollegeName, string Parameter, string SearchType)
    {
        if (Parameter == "[All]")
        {
            gstrQrystr = "select * from yuvasummit_feedback where AcademicYear=" + AcademicYear.ToString() + " and type='" + SearchType.ToString() + "'";
        }
        else
        {
            gstrQrystr = "select * from yuvasummit_feedback where AcademicYear=" + AcademicYear.ToString() + " and Institute_Name='" + CollegeName.ToString() + "' and type='" + SearchType.ToString() + "'";
        }

        return DLobj.GetDataTable(gstrQrystr);

    }
    public void UpdateCertificateStatus(string slno, string AcademicCode, int Status)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update yuvasummit_feedback set Status=" + Status.ToString() + " where slno=" + slno.ToString() + " and AcademicYear=" + AcademicCode.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }

    public void FillYuvaCertificateCollege(DropDownList ddl, DropDownList ddlType, string AcademicYear)
    {
        gstrQrystr = "select distinct Institute_Name as name from yuvasummit_feedback where AcademicYear=" + AcademicYear.ToString() + " order by Institute_Name";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "", "name");

        gstrQrystr = "select distinct Type from yuvasummit_feedback where AcademicYear=" + AcademicYear.ToString() + "";

        DLobj.FillDDLWithSelect(ddlType, gstrQrystr, "", "Type");
    }
    public void FillAademicYearWithTop(DropDownList ddl)
    {
        gstrQrystr = "Select Distinct slno,AcademicCode from AcademicYear where Status=1 order by slno desc";
        DLobj.FillDDL(ddl, gstrQrystr, "slno", "AcademicCode");
    }
    public DataTable GetPrayanaStudentList(string AcademicYear, string Sandbox)
    {
        gstrQrystr = "select * from leadprayana_certificate_formuttu where AcademicYear=" + AcademicYear.ToString() + " and sandboxId=" + Sandbox.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void FillPrayanaSandbox(DropDownList ddl)
    {
        gstrQrystr = "select distinct sandboxId as id,SandboxName as name from leadprayana_certificate_formuttu order by sandboxId";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "id", "name");
    }
    public DataTable GetPrayanaCertificate(string AcademicYear, string SandboxId, string SearchType)
    {
        if (SearchType == "[All]")
        {
            gstrQrystr = "select * from leadprayana_certificate_formuttu where AcademicYear=" + AcademicYear.ToString() + "";
        }
        else
        {
            gstrQrystr = "select * from leadprayana_certificate_formuttu where AcademicYear=" + AcademicYear.ToString() + " and sandboxId='" + SandboxId.ToString() + "'";
        }
        return DLobj.GetDataTable(gstrQrystr);

    }
    public void UpdatePrayanaCertificateStatus(string slno, string AcademicCode, int Status)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update leadprayana_certificate_formuttu set Status=" + Status.ToString() + " where slno=" + slno.ToString() + " and AcademicYear=" + AcademicCode.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void FillWorkDiaryMainCategory(DropDownList ddl)
    {
        gstrQrystr = "SELECT DISTINCT SLNO,Main_CategoryName as name FROM workdiary_main_category WHERE STATUS=1 order by Main_CategoryName";
        DLobj.FillDDLSub(ddl, gstrQrystr, "SLNO", "name");
    }
    public void FillWorkDiarySubCategory(DropDownList ddl, string MainCategory_id)
    {
        gstrQrystr = "select distinct slno,Sub_CategoryName as name from workdiary_sub_category where Main_CategoryCode=" + MainCategory_id.ToString() + " and status=1 order by Sub_CategoryName";
        DLobj.FillDDLSub(ddl, gstrQrystr, "SLNO", "name");
    }

    public void FillCollegeNameForWorkDiary(DropDownList ddl, string ManagerCode)
    {
        if (ManagerCode != "[All]")
        {
            gstrQrystr = "select distinct collegeid,college_name from colleges as C,manager_colleges MC where C.collegeid=MC.collegecode and managercode=" + ManagerCode.ToString() + " and C.status=1";
        }
        else
        {
            gstrQrystr = "select distinct collegeid,college_name from colleges as C where status=1";
        }
        DLobj.FillDDLSub(ddl, gstrQrystr, "collegeid", "college_name");
    }
    public void InsertWorkDiary(vmWorkDiary vm)
    {
        gstrQrystr = "insert into workdiary(Entry_Date,Manager_id,MainCategory_Id,SubCategory_Id,Description,No_of_Participants,College_Id,Spent_Time,Remarks,Progress,AcademicYear)" + " " +
        "Values(now(), " + vm.ManagerId.ToString() + ", " + vm.MainCategory.ToString() + ", " + vm.SubCategory.ToString() + ", '" + vm.Descritpion.ToString() + "'," + vm.TotalParticipants.ToString() + ", " + vm.CollegeId.ToString() + ", '" + vm.SpentTime.ToString() + "', '" + vm.Remarks.ToString() + "', '" + vm.Progress.ToString() + "',(select distinct slno from academicyear where status=1 order by slno desc limit 1))";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public DataTable GetWorkDiary(string ManagerCode, bool Limit)
    {
        if (Limit == true)
        {
            gstrQrystr = "select distinct wd.slno,wd.entry_date,md.ManagerName,mc.Main_CategoryName,sc.Sub_CategoryName,wd.description,wd.Spent_Time,wd.Progress  from workdiary as wd,manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno and mc.slno = sc.Main_CategoryCode" + " " +
      "and wd.Manager_Id=" + ManagerCode.ToString() + " and wd.Status=1 group by mc.slno,sc.slno,wd.slno order by wd.entry_date desc limit 30";
        }
        else
        {
            gstrQrystr = "select distinct wd.slno,wd.entry_date,md.ManagerName,mc.Main_CategoryName,sc.Sub_CategoryName,wd.description,wd.Spent_Time,wd.Progress  from workdiary as wd,manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno and mc.slno = sc.Main_CategoryCode" + " " +
      "and wd.Manager_Id=" + ManagerCode.ToString() + " and wd.Status=1 group by mc.slno,sc.slno,wd.slno order by wd.entry_date desc";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public void DeleteTask(string managerId, string slno)
    {
        // gstrQrystr= "Update workdiary Set Status=0 Where Manager_Id=" + managerId.ToString()+" and slno="+slno.ToString()+"";
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Delete from workdiary where Manager_Id=" + managerId.ToString() + " and slno=" + slno.ToString() + "";
        DLobj.ExecuteQuery(gstrQrystr);
    }

    //------Work Diary Reports

    /* public DataTable ManagerWiseSpentTimeCountold()
{
   gstrQrystr = "select distinct md.managerid as Manager_Id,md.ManagerName,SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) AS SpentTime from manager_details as md " + " " +
   "left outer join workdiary as wd on md.managerid = wd.Manager_Id where md.status = 1" + " " +
   "group by md.managername order by SEC_TO_TIME(SUM(TIME_TO_SEC(wd.Spent_Time))) desc";
   return DLobj.GetDataTable(gstrQrystr);
}*/
    public DataTable ManagerWiseSpentTimeCount(string p_programid)
    {
        /*   gstrQrystr = "select distinct md.managerid as Manager_Id,md.ManagerName,SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) AS SpentTime from manager_details as md " + " " +
           "left outer join workdiary as wd on md.managerid = wd.Manager_Id where md.status = 1" + " " +
           "group by md.managername order by SEC_TO_TIME(SUM(TIME_TO_SEC(wd.Spent_Time))) desc";*/

        gstrQrystr = "select distinct md.managerid as Manager_Id,md.ManagerName,SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) AS SpentTime from manager_details as md  left outer join workdiary as wd on md.managerid = wd.Manager_Id  inner join  manager_colleges as mc on  md.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id where md.status = 1 and cp.program_id = " + p_programid + " group by md.managername order by SEC_TO_TIME(SUM(TIME_TO_SEC(wd.Spent_Time))) desc";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable ParticularManagerWiseSubCategoryCount(string ManagerId)
    {

        gstrQrystr = "select distinct sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time  from workdiary as wd,manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
   "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno and mc.slno = sc.Main_CategoryCode  and wd.manager_id = " + ManagerId.ToString() + "" + " " +
   "group by wd.manager_id,sc.slno order by SEC_TO_TIME(SUM(TIME_TO_SEC(wd.Spent_Time))) desc";
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable ParticularManagerAndMainCategoryList(string ManagerId, string MainCategoryId, string FromDate, string ToDate, string Sort)
    {
        if (MainCategoryId == "[Select]~[Select]")
        {
            gstrQrystr = "select wd.slno,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
       "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
       "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
       "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
       "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
       "and mc.slno = sc.Main_CategoryCode  and wd.manager_id = " + ManagerId.ToString() + " and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') group by wd.manager_id,wd.slno" + " " +
       "order by " + Sort.ToString();
        }

        else
        {
            gstrQrystr = "select wd.slno,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
       "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
       "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
       "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
       "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
       "and mc.slno = sc.Main_CategoryCode  and wd.manager_id = " + ManagerId.ToString() + " and wd.MainCategory_Id=" + MainCategoryId.ToString() + " and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
       "group by wd.manager_id,wd.slno order by " + Sort.ToString();
        }
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable CategoryWiseManagerList(string ManagerId)
    {
        if (ManagerId.ToString() == "[All]")
        {
            gstrQrystr = "select distinct mc.slno,mc.Main_CategoryName,SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) AS Spent_Time  from workdiary_main_category as mc" + " " +
            "left outer join workdiary as wd on wd.MainCategory_Id = mc.slno group by mc.Main_CategoryName order by SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) desc";

        }
        else
        {
            gstrQrystr = "select distinct mc.slno,mc.Main_CategoryName,SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) AS Spent_Time  from workdiary_main_category as mc" + " " +
            "inner join workdiary as wd on wd.MainCategory_Id = mc.slno where wd.Manager_Id=" + ManagerId.ToString() + " group by mc.Main_CategoryName Order by SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) desc";
        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable ParticularCategoryWiseManagerWiseListing(string ManagerId, string MainCategoryId, string FromDate, string ToDate, string Sort)
    {
        if ((ManagerId != "[All]") && (MainCategoryId == "All"))
        {
            gstrQrystr = "select wd.slno,md.ManagerName,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
      "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
      "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
      "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
      "and mc.slno = sc.Main_CategoryCode  and wd.manager_id = " + ManagerId.ToString() + "  and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') group by wd.manager_id,wd.slno" + " " +
      "order by " + Sort.ToString();
        }
        else if (ManagerId != "[All]")
        {
            gstrQrystr = "select wd.slno,md.ManagerName,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
      "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
      "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
      "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
      "and mc.slno = sc.Main_CategoryCode  and wd.manager_id = " + ManagerId.ToString() + " and wd.MainCategory_Id=" + MainCategoryId.ToString() + " and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') group by wd.manager_id,wd.slno" + " " +
      "order by " + Sort.ToString();
        }
        else if ((ManagerId == "[All]") && (MainCategoryId == "All"))
        {
            gstrQrystr = "select wd.slno,md.ManagerName,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
      "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
      "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
      "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
      "and mc.slno = sc.Main_CategoryCode and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') group by wd.manager_id,wd.slno" + " " +
      "order by " + Sort.ToString();
        }
        else if (ManagerId == "[All]")
        {
            gstrQrystr = "select wd.slno,md.ManagerName,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
      "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
      "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
      "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
      "and mc.slno = sc.Main_CategoryCode and wd.MainCategory_Id=" + MainCategoryId.ToString() + " and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') group by wd.manager_id,wd.slno" + " " +
      "order by " + Sort.ToString();
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable AllCategoryWiseManagerWiseListing(string ManagerId, string FromDate, string ToDate, string Sort)
    {

        if (ManagerId != "[All]")
        {
            gstrQrystr = "select wd.slno,md.ManagerName,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
      "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
      "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
      "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
      "and mc.slno = sc.Main_CategoryCode  and wd.manager_id = " + ManagerId.ToString() + " and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') group by wd.manager_id,wd.slno" + " " +
      "order by " + Sort.ToString();
        }
        else if (ManagerId == "[All]")
        {
            gstrQrystr = "select wd.slno,md.ManagerName,date_format(wd.Entry_Date,'%d-%m-%Y') as Entry_Date,sc.Sub_CategoryName,SEC_TO_TIME( SUM(TIME_TO_SEC(wd.Spent_Time))) AS Spent_Time," + " " +
      "clg.College_Name,wd.description,wd.No_Of_Participants,wd.Remarks,wd.Progress" + " " +
      "from workdiary as wd left outer join colleges as clg on wd.College_Id = clg.CollegeId," + " " +
      "manager_details as md,workdiary_main_category as mc,workdiary_sub_category as sc" + " " +
      "where wd.manager_id = md.ManagerId and wd.MainCategory_id = mc.slno and wd.SubCategory_id = sc.slno" + " " +
      "and mc.slno = sc.Main_CategoryCode  and (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') group by wd.manager_id,wd.slno" + " " +
      "order by " + Sort.ToString();
        }
        return DLobj.GetDataTable(gstrQrystr);
    }

    public string GetSumofTimeSpent(string FromDate, string ToDate, string ManagerId)
    {

        if (ManagerId == "[All]")
        {
            gstrQrystr = "select SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0))  AS Spent_Time from workdiary as wd" + " " +
            "where (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')";
        }
        else
        {
            gstrQrystr = "select SEC_TO_TIME(ifnull(SUM(TIME_TO_SEC(wd.Spent_Time)),0)) AS Spent_Time from workdiary as wd" + " " +
            "where (date(wd.Entry_Date) BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') and wd.Manager_Id = " + ManagerId.ToString() + "";
        }

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {


            return dt.Rows[0].ItemArray[0].ToString();

        }
        else
        {
            return "";
        }
    }

    public void Developer_FillManagerddl(DropDownList ddl)
    {
        gstrQrystr = "select distinct ManagerId,managername from manager_details order by managername";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "ManagerId", "ManagerName");
    }

    public DataTable Developer_GetCollegeDetails()
    {
        gstrQrystr = "SELECT distinct clg.collegeid,ms.statename,md.DistrictName,mt.Taluk_Name as TalukaName,College_Name as CollegeName,clg.College_Type as CollegeType,mad.managername FROM" + " " +
        "colleges as clg,mstr_state as ms,mstr_district as md,mstr_taluka as mt,manager_details as mad,manager_colleges as mc " + " " +
        "where clg.stateid = ms.code and clg.districtId = md.DistrictId and clg.talukId = mt.Id and mt.district_id = md.districtId  and mad.ManagerId = mc.ManagerCode and mc.CollegeCode = clg.CollegeId" + " " +
        "order by clg.college_name asc";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Developer_SaveCollegeDetails(string StateCode, string DistrictCode, string TalukaCode, string CollegeType, string CollegeName, string ManagerId)
    {
        gstrQrystr = "insert into colleges (stateid,districtid,talukid,College_Type,college_name,status)" + " " +
        "values(" + StateCode.ToString() + ", " + DistrictCode.ToString() + ", " + TalukaCode.ToString() + ", '" + CollegeType.ToString() + "', '" + Regex.Replace(CollegeName.ToString(), "'", "`").Trim() + "', 1)";
        DLobj.ExecuteQuery(gstrQrystr);

        if (ManagerId.ToString() != "[Select]")
        {
            gstrQrystr = "select CollegeId from colleges order by collegeid desc limit 1";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            string collegeid = dt.Rows[0].ItemArray[0].ToString();

            gstrQrystr = "insert into manager_colleges(managercode, TalukaCode, collegecode, status)" + " " +
            "values(" + ManagerId.ToString() + ", " + TalukaCode.ToString() + ", " + collegeid.ToString() + ", 1)";
            DLobj.ExecuteQuery(gstrQrystr);
        }

    }
    public DataTable FillFundingAmount(string ManagerId, string AcademicCode)
    {
        gstrQrystr = "SELECT PD.PDId,SD.LEAD_Id,studentname,mobileno,MailId,title,IFNULL(PD.Amount, 0) AS RequestedAmount,IFNULL(PD.SanctionAmount, 0) AS SanctionAmount,IFNULL(SUM(PFD.Amount),0) AS ReleaseAmount," + " " +
        "IFNULL((SanctionAmount - IFNULL(SUM(PFD.Amount), 0)), 0) as Balance,PD.ProjectStatus FROM project_description PD Left Outer join student_registration SD ON PD.Lead_Id = SD.Lead_Id" + " " +
        "LEFT JOIN project_fund_details PFD ON PD.PDId = PFD.PDId where PD.managerid = " + ManagerId.ToString() + " and Pd.academicCode = " + AcademicCode.ToString() + "" + " " +
        "and projectstatus in ('Approved','RequestForCompletion','Completed') and PD.amount <> 0 and PD.SanctionAmount <> 0" + " " +
        "group by PD.pdid order by PD.Edited_Date desc ";
        return DLobj.GetDataTable(gstrQrystr);
    }

    public void FillStudentRequestHeads(DropDownList ddl)
    {
        gstrQrystr = "select slno,head_name from general_request_heads where status=1 order by head_name";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "slno", "head_name");
    }
    public void FillStudentProjectTitle(DropDownList ddl, string Lead_Id)
    {
        gstrQrystr = "select distinct pdid,title from project_description where Lead_Id='" + Lead_Id.ToString() + "' and projectstatus in ('Approved','RequestForCompletion','Draft')";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "pdid", "title");
    }
    public void Student_RequestInsertDelete(string RequestHeadId, string Lead_Id, string RegistrationId, string ManagerId, string RequestMessage, string RequestPriority, string OperationType, string RequestId, string PDID)
    {
        if (OperationType == "NEW")
        {
            gstrQrystr = "INSERT INTO general_request_details (Request_Head_Id,Lead_Id,Request_Date,Request_By,Manager_Id,Request_Message,Request_Priority,Edited_Date,Edited_By,AcademicCode,PDID)" + " " +
            "values (" + RequestHeadId.ToString() + ",'" + Lead_Id.ToString() + "',now()," + RegistrationId.ToString() + "," + ManagerId.ToString() + ",'" + RequestMessage.ToString() + "','" + RequestPriority.ToString() + "',Now()," + RegistrationId.ToString() + ",(Select slno from AcademicYear where status=1 order by slno desc limit 1)," + PDID.ToString() + ")";
            DLobj.ExecuteQuery(gstrQrystr);

            string DeviceID = Common_GetDeviceID(Lead_Id.ToString());
            if (DeviceID != "")
            {
                FCMPushNotification.AndroidPush(DeviceID.ToString(), Lead_Id + "-" + "Resquest sent to a Manager", "Request", "Empty");
            }

            DeviceID = Manager_GetDeviceID(ManagerId.ToString());
            if (DeviceID != "")
            {
                if (RequestHeadId == "1")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), Lead_Id.ToString() + "-" + "Requested for Project  Approval Letter", "Manager", "Empty");

                }
                else
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), Lead_Id.ToString() + "-" + "Requested " + " " + RequestMessage.ToString(), "Manager", "Empty");

                }
            }
            Manager_SaveNotificationLog(ManagerId.ToString(), Lead_Id.ToString(), "Student Requested " + " " + RequestMessage.ToString(), "Student Request", "");

        }
        else if (OperationType == "DELETE")
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  delete from general_request_details where Request_Id=" + RequestId.ToString() + "";
            DLobj.ExecuteQuery(gstrQrystr);
        }
    }
    public DataTable Student_FillRequestRepeater(string Lead_Id, string RequestHead)
    {
        if (RequestHead.ToString() == "")
        {
            gstrQrystr = "select RD.request_Id,SR.Image_Path,RD.Request_Message,RD.Request_Priority,TIME_FORMAT(RD.Request_Date,'%r') as Request_Time," + " " +
       "date_format(RD.Request_Date, '%d %M %y') as Request_Date,MD.Image_Path,RD.Response_Message," + " " +
       "TIME_FORMAT(RD.Response_Date, '%r') as Response_Time,date_format(RD.Response_Date, '%d %M %y') as Response_Date," + " " +
       "RH.Head_Name,RD.Status,ifnull(PD.Title,'-') as ProjectTitle" + " " +
       "from general_request_details as RD left outer join project_description as PD on RD.pdid=PD.PDId," + " " +
       "general_request_heads as RH,manager_details as MD,student_registration as SR" + " " +
       "where RD.Request_Head_Id = RH.slno and RD.Manager_Id = MD.ManagerId and RD.Request_By = SR.RegistrationId and" + " " +
       "RD.Lead_Id = '" + Lead_Id + "'  order by ifnull(RD.Edited_Date,RD.Request_Date)  desc";
        }
        else
        {
            gstrQrystr = "select RD.request_Id,SR.Image_Path,RD.Request_Message,RD.Request_Priority,TIME_FORMAT(RD.Request_Date,'%r') as Request_Time," + " " +
         "date_format(RD.Request_Date, '%d %M %y') as Request_Date,MD.Image_Path,RD.Response_Message," + " " +
         "TIME_FORMAT(RD.Response_Date, '%r') as Response_Time,date_format(RD.Response_Date, '%d %M %y') as Response_Date," + " " +
         "RH.Head_Name,RD.Status,ifnull(PD.Title,'-') as ProjectTitle" + " " +
         "from general_request_details as RD  left outer join project_description as PD on RD.pdid=PD.PDId," + " " +
         "general_request_heads as RH,manager_details as MD,student_registration as SR" + " " +
          "where RD.Request_Head_Id = RH.slno and RD.Manager_Id = MD.ManagerId and RD.Request_By = SR.RegistrationId and" + " " +
         "RD.Lead_Id = '" + Lead_Id + "' and RD.Request_Head_Id=" + RequestHead.ToString() + "  order by ifnull(RD.Edited_Date,RD.Request_Date)   desc";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_FillStudentRequestRepeater(string Where)
    {
        gstrQrystr = "select RD.request_Id as Ticket_No,Date_format(RD.Request_Date,'%d-%m-%Y %r') as Request_Date,Date_format(RD.Response_Date,'%d-%m-%Y %r') as Response_Date," + " " +
        "SR.Lead_Id,SR.StudentName,SR.MobileNo,SR.mailid,CLG.College_Name,RD.Request_Message,RD.Request_Priority," + " " +
        "RD.Response_Message,RH.slno as Request_Head_Id,RH.Head_Name,(Case when RD.status = 1 then 'Open' else 'Close' end) as Status," + " " +
        "ifnull(RD.PDID,0) as PDID,ifnull(PD.Title,'-') as ProjectTitle" + " " +
        "from general_request_details as RD left outer join project_description as PD on RD.pdid=PD.PDId," + " " +
        "general_request_heads as RH,manager_details as MD,student_registration as SR,colleges as CLG" + " " +
        "where RD.Request_Head_Id = RH.slno and RD.Manager_Id = MD.ManagerId and RD.Request_By = SR.RegistrationId and SR.CollegeCode = CLG.CollegeId and " + " " +
        "" + Where.ToString() + " " +
        "order by Date_format(RD.Edited_Date,'%d-%m-%Y %r') desc";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public string Manager_GetProjectName(string PDID)
    {
        gstrQrystr = "select title from project_description where pdid=" + PDID.ToString() + "";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            return "";
        }
    }
    List<Meterials> dataList = new List<Meterials>();
    public bool ValidateMeterial(Repeater rptMeterial)
    {
        foreach (RepeaterItem item in rptMeterial.Items)
        {
            dataList.Add(new Meterials()
            {
                MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text,
                MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text,
                Slno = (item.FindControl("lblSlno") as Label).Text
            });
        }
        //-- add a blank row to list to show a new row added
        dataList.Add(new Meterials());

        if (dataList.Select(o => new
        {
            o.MeterialName
        }).Distinct().Count() != dataList.Count())
        {

            return true;
        }
        else
        {
            return false;
        }

    }
    public class Meterials
    {
        public string MeterialName
        {
            get; set;
        }
        public string MeterialCost
        {
            get; set;
        }
        public string Slno
        {
            get; set;
        }
    }
    public DataTable Common_College_FillDetails(string Where)
    {
        gstrQrystr = "select distinct CollegeId,College_Name,Principal_Name,Principal_MobileNo,Principal_MailId," + " " +
        "Principal_WhatsAppNo,Principal_FacebookId from colleges as clg ,college_programs as cp,manager_colleges as mc " + " " +
        "" + Where.ToString();
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Common_College_PrincipalSave(string CollegeId, string PrincipalName, string PrincipalMobile, string PrincipalMailId, string PrincipalWhatsApp, string PrincipalFacebookId)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  Update Colleges set Principal_Name='" + Regex.Replace(PrincipalName.ToString(), "'", "`").Trim() + "',Principal_MobileNo='" + Regex.Replace(PrincipalMobile.ToString(), "'", "`").Trim() + "', " + " " +
        "Principal_MailId='" + Regex.Replace(PrincipalMailId.ToString(), "'", "`").Trim() + "',Principal_WhatsAppNo='" + Regex.Replace(PrincipalWhatsApp.ToString(), "'", "`").Trim() + "', " + " " +
        "Principal_FacebookId='" + Regex.Replace(PrincipalFacebookId.ToString(), "'", "`").Trim() + "',College_Mailid='" + Regex.Replace(PrincipalMailId.ToString(), "'", "`").Trim() + "',College_Password='" + Regex.Replace(PrincipalMobile.ToString(), "'", "`").Trim() + "' where CollegeId=" + CollegeId.ToString() + " ";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void Manager_UpdateRespondStudentRequest(string RequestId, string RespondMessage, string ManagerId, int isDocCreated)
    {
        if (isDocCreated == 1)
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update general_request_details set Response_Message='" + RespondMessage.ToString() + "',Response_Date=now(),Response_By=" + ManagerId.ToString() + ",Doc_Created=" + isDocCreated + "  " +
      ",Edited_Date=now(),Edited_By=" + ManagerId.ToString() + ",Status=2 where Request_Id=" + RequestId.ToString().TrimStart('#') + " ";
        }
        else
        {
            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update general_request_details set Response_Message='" + RespondMessage.ToString() + "',Response_Date=now(),Response_By=" + ManagerId.ToString() + " " + " " +
      ",Edited_Date=now(),Edited_By=" + ManagerId.ToString() + ",Status=2  where Request_Id=" + RequestId.ToString().TrimStart('#') + " ";
        }

        DLobj.ExecuteQuery(gstrQrystr);
    }

    public void Student_FillSuggestionFeedbackHeads(DropDownList ddl)
    {
        gstrQrystr = "select slno,head_name from general_suggestion_feedback_heads where status=1";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "slno", "head_name");
    }
    public int Student_SubmitSuggestionFeedback(string Lead_Id, string ManagerId, string HeadId, string Feedback, string Suggestion)
    {
        int Status = 0;
        gstrQrystr = "insert into general_suggestion_feedback(Lead_Id, Manager_Id, feedback_head_id, feedback, suggestion, Created_Date,DeviceType,AcademicCode)" + " " +
        "value('" + Lead_Id.ToString() + "', " + ManagerId.ToString() + "," + HeadId.ToString() + ", '" + Feedback.ToString() + "', '" + Suggestion.ToString() + "', now(),'WEB'," + " " +
        "(select slno from academicyear where status=1 order by slno desc limit 1))";
        Status = DLobj.ExecuteQuery(gstrQrystr);
        if (Status == 1)
        {
            string msgMail = Lead_Id.ToString() + " " + "Thank you for your Feedback/Suggestion " + "\n";
            msgMail += "Suggestion :" + Suggestion.ToString() + "\n";
            msgMail += "Feeback :" + Feedback.ToString();
            DataTable dt = Student_GetStudentDetailForMailSend(Lead_Id.ToString());
            string Mailid = dt.Rows[0].ItemArray[0].ToString();
            string StudentName = dt.Rows[0].ItemArray[1].ToString();
            if (Mailid.ToString() != "")
            {
                string body = PopulateBody(Lead_Id.ToString(),
                " <b>Thank you for your Feedback/Suggestion</b>", " Given Details are Followed: ", "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br /><br /></li><li><b>Name :</b> " + StudentName.ToString() + "<br /><br /></li><li><b>Suggestion:</b> " + Suggestion.ToString() + "<br /><br /></li> " + "" +
                "<li><b>Feed back :</b> " + Feedback.ToString() + "<br /><br />");
                SendHtmlFormattedEmail(Mailid.ToString(), "Suggestion / Feedback", body);

                body = PopulateBody(Lead_Id.ToString(),
                     " <b>Student has Given Suggestion/Feedback</b>", " Given Details are Followed: ", "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br /><br /></li><li><b>Name :</b> " + StudentName.ToString() + "<br /><br /></li><li><b>Suggestion:</b> " + Suggestion.ToString() + "<br /><br /></li> " + "" +
                     "<li><b>Feed back :</b> " + Feedback.ToString() + "<br /><br />");
                SendHtmlFormattedEmail("abhinandan.k@dfmail.org", "Suggestion / Feedback", body);

            }
            string DeviceID = Common_GetDeviceID(Lead_Id.ToString());
            if (DeviceID != "")
            {
                FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your Suggestion & Feedback Submitted Successfully", "Proposed", "");
            }
            return Status;
        }
        else
        {
            return Status = 0;
        }
    }
    public int Common_Delete_ProjectDocument(string ImgSlno)
    {
        int Result = 0;
        if (ImgSlno != "")
        {
            gstrQrystr = "Select Document_Path from project_digital_documents where slno in(" + ImgSlno.ToString() + ")";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);
            foreach (DataRow dr in dt.Rows)
            {
                if ((System.IO.File.Exists(HttpContext.Current.Server.MapPath(dr[0].ToString()))))
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(dr[0].ToString()));
                }
            }

            gstrQrystr = "SET SQL_SAFE_UPDATES=0;  delete from project_digital_documents where slno in(" + ImgSlno.ToString() + ")";
            Result = DLobj.ExecuteQuery(gstrQrystr);



        }

        return Result;
    }
    public DataTable Admin_FillSuggestionFeedback(string Where)
    {
        gstrQrystr = "SELECT gsf.slno,sr.lead_id,sr.studentName,sr.mobileno,sr.mailid,md.managerid,md.managername," + " " +
        "date_format(gsf.Created_Date,'%d-%m-%Y') as Created_date,gsf.Suggestion,gsf.Feedback,gsfh.Head_Name" + " " +
        "FROM general_suggestion_feedback AS gsf,general_suggestion_feedback_heads AS gsfh,manager_details AS md,student_registration AS sr" + " " +
        "where gsf.feedback_head_id = gsfh.slno and gsf.Lead_Id = sr.Lead_Id and gsf.Manager_Id = md.ManagerId" + " " +
        " " + Where.ToString() + " " +
        "order by date_format(gsf.Created_Date,'%d-%m-%Y %r') desc";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }
    public DataTable Admin_FillManagerWithSuggFeedbackCount(string ManagerId)
    {
        gstrQrystr = "SELECT count(gsf.slno) as Count FROM  general_suggestion_feedback AS gsf where gsf.Manager_Id = " + ManagerId.ToString() + "" + " " +
       "order by date_format(gsf.Created_Date,'%d-%m-%Y %r') desc";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }

    public DataTable Student_GetRatingsDetails(string Lead_Id)
    {
        gstrQrystr = "select Rating,count(rating) from Project_Description where lead_id='" + Lead_Id.ToString() + "' and projectstatus='Completed' " + " " +
         " group by Rating order by Rating desc  ";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Student_GetProjectDiscussionForum(string PDID)
    {
        gstrQrystr = "select Comment_Type,comments,User_Type,ManagerName,StudentName,date_format(PDF.Created_Date,'%d-%m-%y %r') as Reply_Time,PDF.ProjectStatus from project_discussion_forum as PDF" + " " +
        "inner join project_description as PD on PD.PDId = PDF.PDId " + " " +
        "Inner join Student_registration as SR on SR.RegistrationId = PD.Student_Id" + " " +
        "Inner join manager_details as MD on MD.ManagerId = PD.ManagerId" + " " +
        "where PDF.PDID = " + PDID.ToString() + " and PDF.Status = 1 order by PDF.Slno Asc";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Student_SaveProjectDiscussionForum(string PDID, string Comments, string StudentId, string ProjectStatus)
    {
        gstrQrystr = "insert into project_discussion_forum(PDID,Comment_Type,Comments,User_Type,Created_By,Created_Date,Projectstatus)" + " " +
        "values(" + PDID.ToString() + ", 'Discussion', '" + Regex.Replace(Comments.ToString(), "'", "`").Trim() + "', 'Student', " + StudentId.ToString() + ", now(),'" + ProjectStatus.ToString() + "')";
        int i = DLobj.ExecuteQuery(gstrQrystr);
        if (i > 0)
        {
            //string DeviceID = Common_GetDeviceID(Lead_Id.ToString());
            //if (DeviceID != "")
            //{
            //    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your Suggestion & Feedback Submitted Successfully", "Proposed", "");
            //}
        }
    }
    public void Manager_SaveProjectDiscussionForum(string PDID, string Comments, string ManagerId, string Comment_Type, string ProjectStatus)
    {
        gstrQrystr = "insert into project_discussion_forum(PDID,Comment_Type,Comments,User_Type,Created_By,Created_Date,ProjectStatus)" + " " +
        "values(" + PDID.ToString() + ", '" + Comment_Type.ToString() + "', '" + Regex.Replace(Comments.ToString(), "'", "`").Trim() + "', 'Manager', " + ManagerId.ToString() + ", now(),'" + ProjectStatus.ToString() + "')";
        int i = DLobj.ExecuteQuery(gstrQrystr);
        if (i > 0)
        {

        }
    }
    public void FillTalukaByManagerId(string Where, DropDownList ddl)
    {

        gstrQrystr = "select distinct Id,Taluk_Name from mstr_taluka as mt,colleges as clg,manager_colleges as mc" + " " +
   "where mt.id = clg.TalukId and mt.id = mc.TalukaCode and clg.collegeid = mc.collegecode and" + " " +
   Where.ToString() + " " + "order by mt.Taluk_Name";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "Id", "Taluk_Name");


    }

    public int Common_GetChatUnreadMessageCount(string PDID)
    {
        gstrQrystr = "select ifnull(count(pdid),0) from project_discussion_forum where pdid=" + PDID.ToString() + " and isRead=0";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return int.Parse(dt.Rows[0].ItemArray[0].ToString());
    }
    public void Student_UpdateChatRead(string PDID, string UserType)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update project_discussion_forum set isRead=1 where PDId=" + PDID.ToString() + " and User_Type='" + UserType.ToString() + "'";
        DLobj.ExecuteQuery(gstrQrystr);
    }

    public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("leadmis@dfmail.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            // mailMessage.CC.Add(new MailAddress(lblManagerEmailId.Text.ToString()));
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

    // check collge exist or not
    public bool checkCollegeExistence(string collegeName)
    {
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LEADMIS"].ToString();
        bool exists = false;

        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            conn.Open();

            using (MySqlCommand cmd = new MySqlCommand("USP_CHECKCOLLEGEEXISTANCE", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("P_CollegeName", collegeName);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        if (count > 0)
                        {
                            exists = true;
                        }
                    }
                }
            }
        }

        return exists;
    }


}