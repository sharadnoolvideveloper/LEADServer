using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Universal.Standard;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Configuration;

/// <summary>
/// Summary description for PassBL
/// </summary>
public class PassBL
{
    UniversalDL DLobj = new UniversalDL();
    string gstrQrystr = "";
    public PassBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void SaveMissCallLog(string CallId,string CallFrom,string CallTime)
    {
        string gstrQrystr = "INSERT INTO misscall_log (callid,callfrom,calldate)";
        gstrQrystr = gstrQrystr + " VALUES('" + CallId + "','" + CallFrom + "','" + CallTime + "')";
        DLobj.ExecuteQuery(gstrQrystr);
    }
    public void GenerateLeadId(string MobileNo)
    {
        SendEmail Email = new SendEmail();
        string Message = "";
        string SMS_Plaform = ConfigurationManager.AppSettings["SMS_Platform"].ToString();
        string Deactivation_Days = ConfigurationManager.AppSettings["Deactivation_Days"].ToString();
        string Helpline_No = ConfigurationManager.AppSettings["Helpline_No"].ToString();
        string rrn = string.Empty;
        string rrn1 = string.Empty;
    
        long ldcount;
      
        try
        {
            string status = "";
            DataTable dt = new DataTable();
            //gstrQrystr= "SELECT lead_id,MobileNo from Student_Registration WHERE MobileNo like '%" + MobileNo + "%'";
            gstrQrystr = "SELECT lead_id,MobileNo from Student_Registration WHERE MobileNo='" + MobileNo + "' ";
            dt = DLobj.GetDataTable(gstrQrystr);
            if (dt.Rows.Count > 0)
            {
                status = "Already Registered";
                rrn = Convert.ToString(dt.Rows[0].ItemArray[0].ToString());//["lead_id"]); // Lead_ID
                                                                           //SendSMS("Mobile No.:" + MobileNo + " already registered with Lead ID: " + rrn + ". Please login and submit project proposals at mis.leadcampus.org Helpline:9686654748 - Deshpande Foundation", MobileNo);  i        
                if (SMS_Plaform == "CampusConnect")
                {
                    Message = "Your " + MobileNo + " No. is already registered with LEAD ID:" + rrn + "\n";
                    Message += "Submit the project proposals at mis.leadcampus.org Helpline:"+Helpline_No.ToString()+" - Deshpande Foundation";
                     SendCampusConnectSMS(Message.ToString(), MobileNo);
                }
                else
                {
                    Message = "Your " + MobileNo + " No. is already registered with LEAD ID:" + rrn + "\n";
                    Message += "Submit the project proposals at mis.leadcampus.org Helpline:"+Helpline_No.ToString()+" - Deshpande Foundation";
                    SendExotelSMS(Message.ToString(), MobileNo);
                }
               
            }
            #region Create New Registration
            else
            {
                // Generate the Lead ID
                gstrQrystr = "Select distinct Lead_Id_Code,slno from AcademicYear where Status=1 order by slno desc limit 1";
                
                dt = DLobj.GetDataTable(gstrQrystr);
                string LID = dt.Rows[0].ItemArray[0].ToString();
                string AcademicCode= dt.Rows[0].ItemArray[1].ToString();
                long tid = 0;
                // Get the newly(latest)  LEAD ID               
                //gstrQrystr = "SELECT MAX(username) as ldcnt from Users where Role='Student' and username like '"+LID.ToString()+"%'";

                gstrQrystr = "select username as ldcnt from users where Role='Student' and username like '" + LID.ToString() + "%' order by Reference_Id desc limit 1";
                dt = DLobj.GetDataTable(gstrQrystr);
                if (dt.Rows.Count > 0)
                {
                    string LeadId = dt.Rows[0].ItemArray[0].ToString();
                    string pass = LeadId.Substring(4, LeadId.LastIndexOf(LID) + 5);
                    ldcount = Convert.ToInt64(pass);//["ldcnt"]);


                    //if(LeadId!="")
                    //{
                    //    string pass = LeadId.Substring(2, LeadId.LastIndexOf(LID) + 5);
                    //    ldcount = Convert.ToInt16(pass);//["ldcnt"]);

                    //}
                    //else
                    //{
                    //    ldcount = 0;
                    //}

                }
                else
                {
                    ldcount = 0;
                }
                tid = ldcount + 1;
                int length = 5;
                string asString = tid.ToString("D" + length); //"00050"
                LID = LID + asString;

                ////---------
                //insert into Registration

                //Mobile Not registered - Register and generate lead id
                gstrQrystr = "select managerid from manager_details where defaultflag=1 order by managerid desc limit 1";
                dt = DLobj.GetDataTable(gstrQrystr);
                //int flag = 0;
                gstrQrystr = "INSERT INTO student_registration(Lead_Id, ManagerCode, MisscallDate, MobileNo, CreateDate, Student_Type,AcademicCode)";
                gstrQrystr = gstrQrystr + "VALUES('"+LID.ToString()+"',"+dt.Rows[0].ItemArray[0].ToString()+",Now(),'"+MobileNo.ToString()+"',Now(), 'Student',"+AcademicCode.ToString()+")";
                DLobj.ExecuteQuery(gstrQrystr);

                gstrQrystr = "select RegistrationId from Student_Registration order by RegistrationId desc limit 1";
                dt = DLobj.GetDataTable(gstrQrystr);
                int registerid = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                // Insert in Login table

                gstrQrystr = "INSERT INTO users (Reference_Id,UserName, Password, Role,Created_Date)";
                gstrQrystr=gstrQrystr+"VALUES("+registerid+", '"+LID.ToString()+"', '"+MobileNo.ToString()+"','Student',Now())";
                DLobj.ExecuteQuery(gstrQrystr);
               
               // Get the newly generated LEAD ID
               gstrQrystr = "SELECT LEAD_ID from Student_Registration WHERE MobileNo='" + MobileNo.ToString() + "'";
                dt = DLobj.GetDataTable(gstrQrystr);           
                if (dt.Rows.Count > 0)
                {
                    rrn = dt.Rows[0].ItemArray[0].ToString();//["LEAD_ID"].ToString();
                }
                if (SMS_Plaform == "CampusConnect")
                {
                  
                     Message = "Your LEAD ID is " + rrn + "\n";
                    Message += "Login and update the profile in mis.leadcampus.org within "+Deactivation_Days.ToString()+" days to avoid de-activation." + "\n";
                    Message += "Helpline: "+Helpline_No.ToString()+" - Deshpande Foundation";                
                    SendCampusConnectSMS(Message.ToString(), MobileNo.ToString());
                 }
                else
                {
                    Message = "Your LEAD ID is " + rrn + "\n";
                    Message += "Login and update the profile in mis.leadcampus.org within " + Deactivation_Days.ToString() + " days to avoid de-activation." + "\n";
                    Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                    SendExotelSMS(Message.ToString(), MobileNo.ToString());
                }
                //SendSMS("Your LEAD ID is " + rrn + ": Login with your LEAD ID and MobileNo and update your profile in mis.leadcampus.org withing 15 days to avoid de-activation of account. Helpline:9686654748 - Deshpande Foundation", MobileNo.ToString());
            
                status = "Registration Completed";
            }
        }
        #endregion Create RRN
        catch (Exception ex)
        {
            Email.SendMailException("Passthrough", ex.ToString(), "Passthrough.aspx", MobileNo, MobileNo);
            GenerateLeadId(MobileNo.ToString());

        }
    }

    private static void SendExotelSMS(string Message, string Mobile_No)
    {

        //string SMS_Id = "";
        SendSMS s = new SendSMS("dfmail", "dfmail", "ddac450064312c03a44ff94d301cf7eabdbd62bd");
        string response = s.execute("08047091456", Mobile_No, Message.ToString());
       //var  matches = Regex.Matches(response, @"(?<=<Sid>)(.+?)(?=</)");
       // foreach (Match m in matches)
       // {
       //     SMS_Id = m.Groups[1].ToString();
       // }
    }

    private static void SendCampusConnectSMS(string message, string number)
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
}