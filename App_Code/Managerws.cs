using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for Managerws
/// </summary>
[WebService(Namespace = "http://mis.leadcampus.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Managerws : System.Web.Services.WebService
{

    public Managerws()
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
    public vmmanager GetManagerDetails(int ManagerId)
    {
        vmmanager manager = new vmmanager();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SELECT_MANAGER_DETAILS";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        // manager.ManagerId = long.Parse(dr["ManagerId "].ToString());
                        manager.ManagerName = dr["ManagerName"].ToString();
                        manager.Address = dr["Address"].ToString();
                        manager.MobileNo = dr["MobileNo"].ToString();
                        manager.MailId = dr["MailId"].ToString();
                        manager.Gender = dr["Gender"].ToString();
                        manager.BloodGroup = dr["BloodGroup"].ToString();
                        manager.Image_Path = dr["Image_Path"].ToString();
                        manager.Facebook = dr["Facebook"].ToString();
                        manager.Twitter = dr["Twitter"].ToString();
                        manager.InstaGram = dr["InstaGram"].ToString();
                        manager.WhatsApp = dr["WhatsApp"].ToString();
                        manager.Status = "Success";
                    }
                }
                else
                {
                    manager.Status = "Invalid Lead Id";
                }
            }
        }
        catch (Exception)
        {
            manager.Status = "Error";
        }
        return manager;
    }

    [WebMethod]
    public string UpdateManagerDetailswithstring(int ManagerId, string ManagerName, string Address, string MobileNo, string MailId, string Gender, string BloodGroup,
        string Image_Path, string ProfileImage, string Facebook, string Twitter, string InstaGram, string WhatsApp)
    {
        string status = "";
        try
        {
            string fileName = "";

            try
            {
                if (ProfileImage != null)
                {
                    if (ProfileImage.Length > 0)
                    {
                        fileName = "~/ProfilePics/" + ManagerId + "_" + "_" + Guid.NewGuid().ToString() + ".jpg";
                        byte[] bytes = Convert.FromBase64String(ProfileImage);
                        var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                        fs.Write(bytes);
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                fileName = "";
                status = ex.Message.ToString();
            }

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_MANAGER_PROFILE";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_ManagerName", ManagerName);
                cmd.Parameters.AddWithValue("p_Address", Address);
                cmd.Parameters.AddWithValue("p_MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("p_MailId", MailId);
                cmd.Parameters.AddWithValue("p_Gender", Gender);
                cmd.Parameters.AddWithValue("p_BloodGroup", BloodGroup);
                cmd.Parameters.AddWithValue("p_Image_Path", fileName);
                cmd.Parameters.AddWithValue("p_Facebook", Facebook);
                cmd.Parameters.AddWithValue("p_Twitter", Twitter);
                cmd.Parameters.AddWithValue("p_InstaGram", InstaGram);
                cmd.Parameters.AddWithValue("p_WhatsApp", WhatsApp);

                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "Success";
                else
                    status = "Unable to update Manager Profile";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
    }



    [WebMethod]
    public List<vmproject> GetUnaprrovedProjectList(int ManagerId)
    {
        List<vmproject> Unapproved = new List<vmproject>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LIST_UNAPPROVED";
                cmd.Parameters.AddWithValue("p_Managerid", ManagerId);
                cmd.Parameters.AddWithValue("p_status", "Proposed");


                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Unapproved.Add(new vmproject
                        {
                            PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            College_name = dt.Rows[i]["College_name"].ToString(),
                            Student_Image_Path = dt.Rows[i]["Student_Image_Path"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            Title = dt.Rows[i]["Title"].ToString(),
                            Amount = long.Parse(dt.Rows[i]["Amount"].ToString()),
                            AcademicCode = dt.Rows[i]["AcademicCode"].ToString(),
                            status = "Success",
                            StreamCode = dt.Rows[i]["StreamName"].ToString()
                        });
                    }
                }
                else
                {
                    Unapproved.Add(new vmproject { status = "Invalid Projects" });
                }
            }
        }
        catch (Exception)
        {
            Unapproved.Add(new vmproject { status = "Error" });
        }
        return Unapproved;

    }

    [WebMethod]
    public List<vmproject> GetcomplitedProjectList(int ManagerId)
    {
        List<vmproject> complited = new List<vmproject>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LIST_COMPLITED";
                cmd.Parameters.AddWithValue("p_Managerid", ManagerId);
                cmd.Parameters.AddWithValue("p_status", "RequestForCompletion");
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        complited.Add(new vmproject
                        {
                            PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            College_name = dt.Rows[i]["College_name"].ToString(),
                            Student_Image_Path = dt.Rows[i]["Student_Image_Path"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            Title = dt.Rows[i]["Title"].ToString(),
                            SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                            AcademicCode = dt.Rows[i]["AcademicCode"].ToString(),
                            status = "Success",
                            StreamCode = dt.Rows[i]["StreamName"].ToString(),
                            IsImpactProject = int.Parse(dt.Rows[i]["isImpact_Project"].ToString())
                        });
                    }
                }
                else
                {
                    complited.Add(new vmproject { status = "Invalid Completed Projects" });
                }
            }
        }
        catch (Exception)
        {
            complited.Add(new vmproject { status = "Error" });
        }
        return complited;
    }

    [WebMethod]
    public List<vmproject> GetApprovedProjectList(int ManagerId)
    {
        List<vmproject> Approved = new List<vmproject>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LIST_APPROVED";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_status", "Approved");
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Approved.Add(new vmproject
                        {
                            PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            Student_Image_Path = dt.Rows[i]["Student_Image_Path"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            Title = dt.Rows[i]["Title"].ToString(),
                            AskedAmount = long.Parse(dt.Rows[i]["AskedAmount"].ToString()),
                            SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                            giventotal = long.Parse(dt.Rows[i]["giventotal"].ToString()),
                            AcademicCode = dt.Rows[i]["AcademicCode"].ToString(),
                            status = "Success",
                            StreamCode = dt.Rows[i]["StreamName"].ToString(),
                            College_name = dt.Rows[i]["College_Name"].ToString()
                        });
                    }
                }
                else
                {
                    Approved.Add(new vmproject { status = "There are no Projects" });
                }
            }
        }
        catch (Exception ex)
        {
            Approved.Add(new vmproject { status = "Error" });
        }
        return Approved;

    }


    [WebMethod]
    public List<vmproject> GetDraftedProjectList(int ManagerId)
    {
        List<vmproject> complited = new List<vmproject>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LIST_COMPLITED";
                cmd.Parameters.AddWithValue("p_Managerid", ManagerId);
                cmd.Parameters.AddWithValue("p_status", "Draft");
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        complited.Add(new vmproject
                        {
                            PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            College_name = dt.Rows[i]["College_name"].ToString(),
                            Student_Image_Path = dt.Rows[i]["Student_Image_Path"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            Title = dt.Rows[i]["Title"].ToString(),
                            SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                            AcademicCode = dt.Rows[i]["AcademicCode"].ToString(),
                            status = "Success",
                            StreamCode = dt.Rows[i]["StreamName"].ToString(),
                            IsImpactProject = int.Parse(dt.Rows[i]["isImpact_Project"].ToString())
                        });
                    }
                }
                else
                {
                    complited.Add(new vmproject { status = "Invalid Draft Projects" });
                }
            }
        }
        catch (Exception)
        {
            complited.Add(new vmproject { status = "Error" });
        }
        return complited;
    }

    [WebMethod]
    public vmproject GetUnapprovedProjectDetails(string leadId, int PDId)
    {
        vmproject vmP = new vmproject();
        List<vmmaterial> materials = new List<vmmaterial>();
        List<vmMember> members = new List<vmMember>();

        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_PROJECT_DETAILS";
                cmd.Parameters.AddWithValue("p_Lead_Id", leadId);
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmP.Title = dt.Rows[i]["Title"].ToString();
                        vmP.ThemeName = dt.Rows[i]["ThemeName"].ToString();
                        vmP.BeneficiaryNo = long.Parse(dt.Rows[i]["BeneficiaryNo"].ToString());
                        vmP.Objectives = dt.Rows[i]["Objectives"].ToString();
                        vmP.ActionPlan = dt.Rows[i]["ActionPlan"].ToString();
                        vmP.ActionPlan = dt.Rows[i]["ActionPlan"].ToString();
                        vmP.Amount = long.Parse(dt.Rows[i]["Amount"].ToString());
                        vmP.Student_Image_Path = dt.Rows[i]["Student_Image_Path"].ToString();
                        vmP.BeneficiariesList = dt.Rows[i]["Beneficiaries"].ToString();
                        vmP.Placeofimplement = dt.Rows[i]["Placeofimplement"].ToString();
                        vmP.CurrentSituation = dt.Rows[i]["CurrentSituation"].ToString();
                        vmP.status = "Success";
                        vmP.ProjectStartDate = dt.Rows[i]["ProjectStartDate"].ToString();
                        vmP.ProjectEndDate = dt.Rows[i]["ProjectEndDate"].ToString();
                        break;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmmaterial obj = new vmmaterial();
                        if (dt.Rows[i]["MeterialName"].ToString() != "" && dt.Rows[i]["MeterialCost"].ToString() != null)
                        {
                            obj.MeterialName = dt.Rows[i]["MeterialName"].ToString();
                            obj.MeterialCost = long.Parse(dt.Rows[i]["MeterialCost"].ToString());
                            materials.Add(obj);
                        }
                    }
                    vmP.materials = materials;

                    //Fetching members
                    DataTable dtmembers = new DataTable();
                    cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_SELECT_PROJECT_TEAM_MEMBERS";
                    cmd.Parameters.AddWithValue("p_pdId", PDId);
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(dtmembers);
                    if (dtmembers.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtmembers.Rows.Count; j++)
                        {
                            if (dtmembers.Rows[j]["MemberName"].ToString() != null && dtmembers.Rows[j]["MemberMailId"].ToString() != null)
                            {
                                vmMember obj = new vmMember();
                                obj.Member_Id = long.Parse(dtmembers.Rows[j]["slno"].ToString());
                                obj.MemberName = dtmembers.Rows[j]["MemberName"].ToString();
                                obj.MemberEmail = dtmembers.Rows[j]["MemberMailId"].ToString();
                                members.Add(obj);
                            }
                        }
                    }
                    vmP.Members = members;
                }
                else
                {
                    vmP.status = "Invalid project details";
                }
            }
        }
        catch (Exception)
        {
            vmP.status = "Error";
        }
        return vmP;
    }

    [WebMethod]

    public string SaveUnapprovedprojectDetails(int PDId, string Lead_Id, string Title,
        string Theme, int BeneficiaryNo, string Objectives, string ActionPlan, string Beneficiaries, string Placeofimplement)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_STUDENT_PENDING_PROJECT";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Theme", Theme);
                cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
                cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiaries, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";

                    //  FCMPushNotification.SendNotification("LEAD", "Edit", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), " Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Edited by Manager", "Student", "Empty");
                    }
                }
                else
                {
                    status = "Failed";
                }


            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]

    public string UpdateUnApprovedprojectDetails(int PDId, string Lead_Id, string Title, string Theme, int BeneficiaryNo,
        string Objectives, string ActionPlan, int SanctionAmount, string ManagerComments, string ProjectStatus
        , string Beneficiaries, string Placeofimplement)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_APPROVE_PROJECT";
                cmd.Parameters.AddWithValue("p_PDId", PDId); //Regex.Replace(TextBox2.Text, "'", "`").Trim();
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Theme", Theme);
                cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
                cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_SanctionAmount", SanctionAmount);
                cmd.Parameters.AddWithValue("p_ManagerComments", Regex.Replace(ManagerComments, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ProjectStatus", ProjectStatus);
                cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiaries, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";

                    string Mailsend = "select P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ActionPlan,P.ManagerComments, P.SanctionAmount,P.ProjectStatus,S.StudentName,S.MailId,S.MobileNo,C.College_Name from project_description as P inner join student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Your project (" + dt2.Rows[0]["Title"].ToString() + ") has been Rejected. </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li><li><b>Manager Comment:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + "<br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " Project is Rejected");
                    }


                    //  FCMPushNotification.SendNotification("LEAD", "Approved", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Rejected", "Student", "Empty");
                    }
                }
                else
                {
                    status = "Unable to update project details";
                }
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }



    [WebMethod]
    public vmproject GetApprovedProjectDetails(string leadId, int PDId)
    {
        vmproject vmP = new vmproject();
        List<vmmaterial> materials = new List<vmmaterial>();
        List<vmMember> members = new List<vmMember>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_PROJECT_DETAILS_APPROVED_NEW";
                cmd.Parameters.AddWithValue("p_Lead_Id", leadId);
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmP.Title = dt.Rows[i]["Title"].ToString();
                        vmP.ThemeName = dt.Rows[i]["ThemeName"].ToString();
                        vmP.BeneficiaryNo = long.Parse(dt.Rows[i]["BeneficiaryNo"].ToString());
                        vmP.Objectives = dt.Rows[i]["Objectives"].ToString();
                        vmP.ActionPlan = dt.Rows[i]["ActionPlan"].ToString();
                        vmP.Amount = long.Parse(dt.Rows[i]["Amount"].ToString());
                        vmP.SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString());
                        vmP.ManagerComments = dt.Rows[i]["ManagerComments"].ToString();
                        vmP.BeneficiariesList = dt.Rows[i]["Beneficiaries"].ToString();
                        vmP.Placeofimplement = dt.Rows[i]["Placeofimplement"].ToString();
                        vmP.status = "Success";
                        vmP.ProjectStartDate = dt.Rows[i]["ProjectStartDate"].ToString();
                        vmP.ProjectEndDate = dt.Rows[i]["ProjectEndDate"].ToString();
                        vmP.IsImpactProject = int.Parse(dt.Rows[i]["isImpact_Project"].ToString());
                        break;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmmaterial obj = new vmmaterial();
                        if (dt.Rows[i]["MeterialName"].ToString() != "" && dt.Rows[i]["MeterialCost"].ToString() != null)
                        {
                            obj.MeterialName = dt.Rows[i]["MeterialName"].ToString();
                            obj.MeterialCost = long.Parse(dt.Rows[i]["MeterialCost"].ToString());
                            materials.Add(obj);
                        }
                    }
                    vmP.materials = materials;


                    //Fetching members
                    DataTable dtmembers = new DataTable();
                    cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_SELECT_PROJECT_TEAM_MEMBERS";
                    cmd.Parameters.AddWithValue("p_pdId", PDId);
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(dtmembers);
                    if (dtmembers.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtmembers.Rows.Count; j++)
                        {
                            if (dtmembers.Rows[j]["MemberName"].ToString() != null && dtmembers.Rows[j]["MemberMailId"].ToString() != null)
                            {
                                vmMember obj = new vmMember();
                                obj.Member_Id = long.Parse(dtmembers.Rows[j]["slno"].ToString());
                                obj.MemberName = dtmembers.Rows[j]["MemberName"].ToString();
                                obj.MemberEmail = dtmembers.Rows[j]["MemberMailId"].ToString();
                                members.Add(obj);
                            }
                        }
                    }
                    vmP.Members = members;


                }
                else
                {
                    vmP.status = "Invalid project details";
                }
            }
        }
        catch (Exception ex)
        {
            vmP.status = "Error";
        }
        return vmP;

    }

    [WebMethod]
    public List<vmProjectCounts> GetProjectCount(int ManagerId)
    {
        List<vmProjectCounts> projects = new List<vmProjectCounts>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_MANAGER_SELECT_PROJECT_COUNTS";
                cmd.Parameters.AddWithValue("p_Id", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        projects.Add(new vmProjectCounts
                        {
                            Counts = int.Parse(dt.Rows[i]["Counts"].ToString()),
                            ProjectStatus = dt.Rows[i]["ProjectStatus"].ToString(),
                            Status = "Success",
                            TshirtStatus = "Tshirt",
                            TshirtRequestCount = int.Parse(dt.Rows[i]["tshirtcount"].ToString())
                        });
                    }
                }
                else
                {
                    projects.Add(new vmProjectCounts { ProjectStatus = "There are no projects" });
                }
            }
        }
        catch (Exception)
        {
            projects.Add(new vmProjectCounts { Status = "Error" });
        }
        return projects;
    }

    [WebMethod]
    public List<vmproject> GetFundstatusList(long ManagerId)
    {
        List<vmproject> Projectstatus = new List<vmproject>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_FUND_STATUS";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_ProjectStatus", "Completed");

                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Projectstatus.Add(new vmproject
                        {
                            PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            Title = dt.Rows[i]["Title"].ToString(),
                            AskedAmount = long.Parse(dt.Rows[i]["AskedAmount"].ToString()),
                            SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                            giventotal = long.Parse(dt.Rows[i]["giventotal"].ToString()),
                            AcademicCode = dt.Rows[i]["AcademicCode"].ToString(),

                            status = "Success"
                        });
                    }
                }
                else
                {
                    Projectstatus.Add(new vmproject { status = "Invalid  Project status" });
                }
            }
        }
        catch (Exception ex)
        {
            Projectstatus.Add(new vmproject { status = "Error" });
        }
        return Projectstatus;

    }

    [WebMethod]
    public vmProjectCompletion GetProjectCompletionDetails(long projectId, string Lead_Id)
    {
        vmProjectCompletion vmproj = new vmProjectCompletion();
        List<vmProjectDocs> docs = new List<vmProjectDocs>();
        List<vmProjectSDG_Goals> SDG = new List<vmProjectSDG_Goals>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_MANAGER_SELECT_PROJECT_COMPLETION_DETAILS";
                cmd.Parameters.AddWithValue("p_Id", projectId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmproj.PDId = long.Parse(dt.Rows[i]["PdId"].ToString());
                        vmproj.Title = dt.Rows[i]["Title"].ToString();
                        vmproj.Theme = dt.Rows[i]["Theme"].ToString();
                        vmproj.BeneficiaryNo = int.Parse(dt.Rows[i]["BeneficiaryNo"].ToString());
                        vmproj.Objectives = dt.Rows[i]["Objectives"].ToString();
                        vmproj.Placeofimplement = dt.Rows[i]["Placeofimplement"].ToString();
                        vmproj.FundsRaised = float.Parse(dt.Rows[i]["FundsRaised"].ToString());
                        vmproj.SanctionAmount = float.Parse(dt.Rows[i]["SanctionAmount"].ToString());
                        vmproj.Fund_Received = float.Parse(dt.Rows[i]["Fund_Received"].ToString());
                        vmproj.Challenge = dt.Rows[i]["Challenge"].ToString();
                        vmproj.Learning = dt.Rows[i]["Learning"].ToString();
                        vmproj.AsAStory = dt.Rows[i]["AsAStory"].ToString();
                        vmproj.CurrentSituation = dt.Rows[i]["CurrentSituation"].ToString();
                        vmproj.Resource = dt.Rows[i]["Resource"].ToString();
                        vmproj.TotalResourses = long.Parse(dt.Rows[i]["TotalResourses"].ToString());
                        vmproj.HoursSpend = int.Parse(dt.Rows[i]["HoursSpend"].ToString());
                        vmproj.Status = "Success";
                        vmproj.ProjectStartDate = dt.Rows[i]["ProjectStartDate"].ToString();
                        vmproj.ProjectEndDate = dt.Rows[i]["ProjectEndDate"].ToString();
                        vmproj.Collaboration_Supported = dt.Rows[i]["Collaboration_Supported"].ToString();
                        vmproj.Permission_And_Activities = dt.Rows[i]["Permission_And_Activities"].ToString();
                        vmproj.Experience_Of_Initiative = dt.Rows[i]["Experience_Of_Initiative"].ToString();
                        vmproj.Lacking_initiative = dt.Rows[i]["Lacking_initiative"].ToString();
                        vmproj.Against_Tide = dt.Rows[i]["Against_Tide"].ToString();
                        vmproj.Cross_Hurdles = dt.Rows[i]["Cross_Hurdles"].ToString();
                        vmproj.Entrepreneurial_Venture = dt.Rows[i]["Entrepreneurial_Venture"].ToString();
                        vmproj.Government_Awarded = dt.Rows[i]["Government_Awarded"].ToString();
                        vmproj.Leadership_Roles = dt.Rows[i]["Leadership_Roles"].ToString();
                        break;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmProjectDocs obj = new vmProjectDocs();
                        if (dt.Rows[i]["Document_Path"].ToString() != null && dt.Rows[i]["Document_Id"].ToString() != null
                            && long.Parse(dt.Rows[i]["SlNo"].ToString()) > 0)
                        {
                            obj.Document_Path = dt.Rows[i]["Document_Path"].ToString();
                            obj.Document_Id = int.Parse(dt.Rows[i]["Document_Id"].ToString());
                            obj.SlNo = int.Parse(dt.Rows[i]["SlNo"].ToString());
                            docs.Add(obj);
                        }
                    }
                    vmproj.docs = docs;

                    DataTable SDGGoals = new DataTable();
                    cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_GET_PROJECT_SDGGOALS";
                    cmd.Parameters.AddWithValue("p_pdId", projectId);
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(SDGGoals);
                    if (SDGGoals.Rows.Count > 0)
                    {
                        for (int j = 0; j < SDGGoals.Rows.Count; j++)
                        {

                            vmProjectSDG_Goals obj = new vmProjectSDG_Goals();
                            obj.Slno = int.Parse(SDGGoals.Rows[j]["slno"].ToString());
                            obj.Goals = SDGGoals.Rows[j]["goals"].ToString();
                            SDG.Add(obj);

                        }
                        vmproj.SDG_List = SDG;
                    }
                    else
                    {
                        vmproj.SDG_Status = "No Goals";
                    }
                }
                else
                {
                    vmproj.Status = "Invalid project details";
                }
            }
        }
        catch (Exception)
        {
            vmproj.Status = "Error";
        }
        return vmproj;
    }

    [WebMethod]

    public string UpdateComplitedprojectDetails(int PDId, string Lead_Id, string ManagerComments, string Placeofimplement, int FundsRaised, string Challenge,
        string Learning, string AsAStory, int Rating, string ProjectStatus)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_APP_UPDATE_COMPLITED_PROJECT_DETAILS";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Parameters.AddWithValue("p_FundsRaised", FundsRaised);
                cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(Challenge, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(Learning, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(AsAStory, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ManagerComments", Regex.Replace(ManagerComments, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Rating", Rating);
                cmd.Parameters.AddWithValue("p_ProjectStatus", ProjectStatus);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success.";
                    LeadBL BL = new LeadBL();
                    BL.Manager_ChangeStudentTypeAfterProjectComplete(Lead_Id.ToString(), "");

                    //  FCMPushNotification.SendNotification("LEAD", "complited", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been completed successfully", "Student", "Empty");
                    }
                    string Mailsend = "select P.Lead_Id, S.StudentName, S.MailId,S.MobileNo, C.College_Name, P.Title, P.BeneficiaryNo,P.Beneficiaries,P.Objectives, P.Placeofimplement, p.SanctionAmount, P.ActionPlan,P.Challenge,P.Learning, P.AsAStory,P.FundsRaised, P.Rating,P.ManagerComments,(Select Sum(Amount) from project_fund_details where PDId=P.PDId) as FundsReceived from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId = S.CollegeCode where P.PDId ='" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ") has been completed successfully </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b> SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + " <br><br></li><li><b> FundsReceived:</b> " + dt2.Rows[0]["FundsReceived"].ToString() + " <br><br></li><li><b> FundsRaised:</b> " + dt2.Rows[0]["FundsRaised"].ToString() + " <br><br></li><li><b>Challenges:</b> " + dt2.Rows[0]["Challenge"].ToString() + " <br><br></li><li><b>Learning:</b> " + dt2.Rows[0]["Learning"].ToString() + " <br><br></li><li><b>AsAStory:</b> " + dt2.Rows[0]["AsAStory"].ToString() + " <br><br></li><li><b>Rating:</b> " + dt2.Rows[0]["Rating"].ToString() + " <br><br></li><li><b>ManagerComments:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + " <br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project completed successfully");
                    }
                }
                else
                {
                    status = "Unable to update project details";
                }

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }




    [WebMethod]

    public string AddFundAmount(int PDId, int ManagerId,
        string LeadId, int Amount)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_INSERT_FUND_AMOUNT";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_LeadId", LeadId);
                cmd.Parameters.AddWithValue("p_Amount", Amount);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";
                    //   FCMPushNotification.SendNotification("LEAD", "test", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your Project(" + dt.Rows[0]["Title"].ToString() + ") Received Fund of " + " (" + Amount.ToString() + " Rs)", "Student", "Empty");
                    }
                    string Mailsend = "select P.Lead_Id, S.StudentName, S.MailId,S.MobileNo, C.College_Name, P.Title, P.BeneficiaryNo,P.Beneficiaries,P.Objectives, P.Placeofimplement, p.SanctionAmount, P.ActionPlan,P.Challenge,P.Learning, P.AsAStory,P.FundsRaised, P.Rating,P.ManagerComments,(Select Sum(Amount) from project_fund_details where PDId=P.PDId) as FundsReceived from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId = S.CollegeCode where P.PDId ='" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ") Fund Amount is Granted(" + Amount + "Rs). </b>", "<b style='color:red;'>if you are not received the fund please fill feed back and suggession form.<b>" + "  " +
                       " <br>The details you entered are listed below:<br>",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b> SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + " <br><br></li><li><b> FundsReceived:</b> " + dt2.Rows[0]["FundsReceived"].ToString() + " <br><br></li><li><b> FundsRaised:</b> " + dt2.Rows[0]["FundsRaised"].ToString() + " <br><br></li><li><b>Challenges:</b> " + dt2.Rows[0]["Challenge"].ToString() + " <br><br></li><li><b>Learning:</b> " + dt2.Rows[0]["Learning"].ToString() + " <br><br></li><li><b>AsAStory:</b> " + dt2.Rows[0]["AsAStory"].ToString() + " <br><br></li><li><b>Rating:</b> " + dt2.Rows[0]["Rating"].ToString() + " <br><br></li><li><b>ManagerComments:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + " <br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " Fund released details");

                        SaveManagerLog(LeadId.ToString(), ManagerId.ToString(), "Title:" + dt2.Rows[0]["Title"].ToString() + ",Amount:" + dt2.Rows[0]["FundsReceived"].ToString() + ",Reason:" + dt2.Rows[0]["ManagerComments"].ToString() + "", "Fund released details (Manager)");
                    }
                }
                else
                {
                    status = "Unable to add fund amount";
                }

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }



    [WebMethod]

    public string ReApplayProject(int PDId, string ProjectStatus, string ManagerComments)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_REAPPLY_PROJECT";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_ProjectStatus", ProjectStatus);
                cmd.Parameters.AddWithValue("p_ManagerComments", Regex.Replace(ManagerComments, "'", "`").Trim());
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";


                    //    FCMPushNotification.SendNotification("LEAD", "Your project  has been rejected", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Your project(" + dt.Rows[0]["Title"].ToString() + ")Proposal form  has been ReApply ", "Student", "Empty");
                    }

                    string Mailsend = "select P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ActionPlan,P.ManagerComments,P.ProjectStatus,S.StudentName,S.MailId,S.MobileNo,C.College_Name,P.ManagerId from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd1);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Your project (" + dt2.Rows[0]["Title"].ToString() + ") has been asked to resubmit </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li><li><b>Manager Comment:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + "<br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project sent for modification");

                        SaveManagerLog("" + dt2.Rows[0]["Lead_Id"].ToString() + "", "" + dt2.Rows[0]["ManagerId"].ToString() + "", "Title: " + dt2.Rows[0]["Title"].ToString() + ",Reason: " + dt2.Rows[0]["ManagerComments"].ToString() + "", "ReApply Project (Manager)");
                    }
                }
                else
                {
                    status = "Unable to Reapply projects";
                }

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }
    [WebMethod]

    public string RejectProject(int PDId, string ProjectStatus, string ManagerComments)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_REAPPLY_PROJECT";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_ProjectStatus", ProjectStatus);
                cmd.Parameters.AddWithValue("p_ManagerComments", Regex.Replace(ManagerComments, "'", "`").Trim());
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";


                    //  FCMPushNotification.SendNotification("LEAD", "Your project  has been rejected", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Your project(" + dt.Rows[0]["Title"].ToString() + ") has been Rejected ", "Student", "Empty");
                    }

                    string Mailsend = "select P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ActionPlan,P.ManagerComments,P.ProjectStatus,P.ManagerId,S.StudentName,S.MailId,S.MobileNo,C.College_Name from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd1);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Your project (" + dt2.Rows[0]["Title"].ToString() + ") has been rejected </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li><li><b>Manager Comment:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + "<br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project is rejected please refer the manager comments");

                        SaveManagerLog("" + dt2.Rows[0]["Lead_Id"].ToString() + "", "" + dt2.Rows[0]["ManagerId"].ToString() + "", "Title:" + dt2.Rows[0]["Title"].ToString() + ",Reason:" + dt2.Rows[0]["ManagerComments"].ToString() + "", "Reject Project (Manager)");
                    }
                }
                else
                {
                    status = "Unable to Reapply projects";
                }

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]

    public string AddFundAmountwithPMcomments(int PDId, int ManagerId,
      string LeadId, int Amount, string ManagerRemark, string ProjectStartDate, string ProjectEndDate, int IsImpactProject)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_INSERT_FUND_AMOUNT_PMCOMMENT_LATEST";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_LeadId", LeadId);
                cmd.Parameters.AddWithValue("p_Amount", Amount);
                cmd.Parameters.AddWithValue("p_ManagerRemark", Regex.Replace(ManagerRemark, "'", "`").Trim());
                cmd.Parameters.AddWithValue("P_ProjectStarDate", ProjectStartDate);
                cmd.Parameters.AddWithValue("P_ProjectEndDate", ProjectEndDate);
                cmd.Parameters.AddWithValue("p_IsImpactProject", IsImpactProject);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";
                    // FCMPushNotification.SendNotification("Lead", "Fund Amount", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (IsImpactProject == 1)
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project(" + dt.Rows[0]["Title"].ToString() + ") has been select as Impactful Project ", "Student", "Empty");
                        }
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project(" + dt.Rows[0]["Title"].ToString() + ") Fund amount" + " (" + Amount.ToString() + ") Rs" + "  has been Credited if your not received the fund please send feed back and suggestion ", "Student", "Empty");
                    }

                    string Mailsend = "select P.Lead_Id, S.StudentName, S.MailId,S.MobileNo, C.College_Name,P.ManagerId, P.Title, P.BeneficiaryNo,P.Beneficiaries,P.Objectives, P.Placeofimplement, p.SanctionAmount, P.ActionPlan,P.Challenge,P.Learning, P.AsAStory,P.FundsRaised, P.Rating,P.ManagerComments,(Select Sum(Amount) from project_fund_details where PDId=P.PDId) as FundsReceived from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId = S.CollegeCode where P.PDId ='" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ") Fund Amount is Granted(" + dt2.Rows[0]["FundsReceived"].ToString() + "Rs). </b>", "<b style='color:red;'>if you are not received the fund please fill feed back and suggession form.<b>" + "  " +
                       " <br>The details you entered are listed below:<br>",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b> SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + " <br><br></li><li><b> FundsReceived:</b> " + dt2.Rows[0]["FundsReceived"].ToString() + " <br><br></li><li><b> FundsRaised:</b> " + dt2.Rows[0]["FundsRaised"].ToString() + " <br><br></li><li><b>Challenges:</b> " + dt2.Rows[0]["Challenge"].ToString() + " <br><br></li><li><b>Learning:</b> " + dt2.Rows[0]["Learning"].ToString() + " <br><br></li><li><b>AsAStory:</b> " + dt2.Rows[0]["AsAStory"].ToString() + " <br><br></li><li><b>Rating:</b> " + dt2.Rows[0]["Rating"].ToString() + " <br><br></li><li><b>ManagerComments:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + " <br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " Fund released details");

                        SaveManagerLog("" + dt2.Rows[0]["Lead_Id"].ToString() + "", "" + dt2.Rows[0]["ManagerId"].ToString() + "", "Title:" + dt2.Rows[0]["Title"].ToString() + "Amount:" + dt2.Rows[0]["FundsReceived"].ToString() + ",Reason:" + dt2.Rows[0]["ManagerComments"].ToString() + "", "Fund released details (Manager)");
                    }
                }
                else
                {
                    status = "Unable to add fund amount and PMcomments";
                }

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }


    [WebMethod]
    public List<vmstory> GetMasterLeaderStoryList()
    {
        List<vmstory> story = new List<vmstory>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LEAD_STOR";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        story.Add(new vmstory
                        {
                            slno = int.Parse(dt.Rows[i]["slno"].ToString()),
                            Story_Title = dt.Rows[i]["Story_Title"].ToString(),
                            Story_Description = dt.Rows[i]["Story_Description"].ToString(),
                            Image_Path = dt.Rows[i]["Image_Path"].ToString(),
                            Card_Image_Path = dt.Rows[i]["Card_Image_Path"].ToString(),
                            URL_Link = dt.Rows[i]["URL_Link"].ToString(),
                            Story_Type = int.Parse(dt.Rows[i]["Story_Type"].ToString()),
                            Created_Date = dt.Rows[i]["Created_Date"].ToString(),
                            status = "Success"
                        });
                    }
                }
                else
                {
                    story.Add(new vmstory { status = "Invalid Lead Story" });
                }
            }
        }
        catch (Exception)
        {
            story.Add(new vmstory { status = "Error" });
        }
        return story;

    }

    [WebMethod]
    public List<vmstory> GetMasterLeaderStoryList1()
    {
        List<vmstory> story = new List<vmstory>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LEAD_STOR";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        story.Add(new vmstory
                        {
                            slno = int.Parse(dt.Rows[i]["slno"].ToString()),
                            Story_Title = dt.Rows[i]["Story_Title"].ToString(),
                            Story_Description = dt.Rows[i]["Story_Description"].ToString(),
                            Image_Path = dt.Rows[i]["Image_Path"].ToString(),
                            Card_Image_Path = dt.Rows[i]["Card_Image_Path"].ToString(),
                            URL_Link = dt.Rows[i]["URL_Link"].ToString(),
                            Story_Type = int.Parse(dt.Rows[i]["Story_Type"].ToString()),
                            Created_Date = dt.Rows[i]["Created_Date"].ToString(),
                            Video_Story_URL = dt.Rows[i]["Video_Story_URL"].ToString(),
                            status = "Success"
                        });
                    }
                }
                else
                {
                    story.Add(new vmstory { status = "Invalid Lead Story" });
                }
            }
        }
        catch (Exception)
        {
            story.Add(new vmstory { status = "Error" });
        }
        return story;

    }

    [WebMethod]
    public List<vmstdtype> ApplayedLeadMembers(int ManagerId)
    {
        List<vmstdtype> story = new List<vmstdtype>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APPLAYED_MASTER_LEADERS";
                cmd.Parameters.AddWithValue("P_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        story.Add(new vmstdtype
                        {

                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            isApply_MasterLeader = int.Parse(dt.Rows[i]["isApply_MasterLeader"].ToString()),
                            Student_Type = dt.Rows[i]["Student_Type"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    story.Add(new vmstdtype { Status = "There is No Applayed students here" });
                }
            }
        }
        catch (Exception)
        {
            story.Add(new vmstdtype { Status = "Error" });
        }
        return story;

    }

    [WebMethod]
    public List<vmstdtype> ApplayedLeadAmbassador(int ManagerId)
    {
        List<vmstdtype> story = new List<vmstdtype>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APPLAYED_LEAD_AMBASSADOR";
                cmd.Parameters.AddWithValue("P_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        story.Add(new vmstdtype
                        {

                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            isApply_LeadAmbassador = int.Parse(dt.Rows[i]["isApply_LeadAmbassador"].ToString()),
                            Student_Type = dt.Rows[i]["Student_Type"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    story.Add(new vmstdtype { Status = "There is No Applayed students here" });
                }
            }
        }
        catch (Exception)
        {
            story.Add(new vmstdtype { Status = "Error" });
        }
        return story;

    }

    [WebMethod]
    public string Applyleadmasterandambassador(string Lead_Id, string Student_Type)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_LEAD_TO_MASTERLEADER";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("P_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("P_Student_Type", Student_Type);

                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "Unable to Apply LeadAmbassador";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public List<vmProjectCounts> GetManagerCollegeCount(int ManagerId)
    {
        List<vmProjectCounts> projects = new List<vmProjectCounts>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_MANAGER_COLLEGE_COUNT";
                cmd.Parameters.AddWithValue("P_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        projects.Add(new vmProjectCounts
                        {
                            Counts = int.Parse(dt.Rows[i]["Counts"].ToString()),

                            Status = "Success"
                        });
                    }
                }
                else
                {
                    projects.Add(new vmProjectCounts { ProjectStatus = "There is college list " });
                }
            }
        }
        catch (Exception)
        {
            projects.Add(new vmProjectCounts { Status = "Error" });
        }
        return projects;
    }

    [WebMethod]
    public List<vmProjectCounts> GetStudentcount(int ManagerId)
    {
        List<vmProjectCounts> projects = new List<vmProjectCounts>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_STUDENT_COUNT";
                cmd.Parameters.AddWithValue("P_ManagerCode", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        projects.Add(new vmProjectCounts
                        {
                            Counts = int.Parse(dt.Rows[i]["Counts"].ToString()),

                            Status = "Success"
                        });
                    }
                }
                else
                {
                    projects.Add(new vmProjectCounts { ProjectStatus = "There is no students in this manager" });
                }
            }
        }
        catch (Exception)
        {
            projects.Add(new vmProjectCounts { Status = "Error" });
        }
        return projects;
    }
    [WebMethod]
    public vmfundstatus Fundamountdetaillist(int ManagerId, int AcademicCode)
    {
        vmfundstatus stud = new vmfundstatus();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_MANAGER_FUND_AMOUNT_DETAILS";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_AcademicCode", AcademicCode);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {

                        stud.fundriserdamount = long.Parse(dr["fundriserdamount"].ToString());
                        stud.SanctionAmount = long.Parse(dr["SanctionAmount"].ToString());
                        stud.fundRelised = long.Parse(dr["fundRelised"].ToString());
                        stud.Status = "Success";
                    }
                }
                else
                {
                    stud.Status = "There is No Applayed students here";
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
    public List<vmfundstatus> GetFundamountYearwise(int ManagerId)
    {
        List<vmfundstatus> projects = new List<vmfundstatus>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_FUND_DETAILS_YEARWISE";
                cmd.Parameters.AddWithValue("P_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        projects.Add(new vmfundstatus
                        {
                            ManagerId = int.Parse(dt.Rows[i]["ManagerId"].ToString()),
                            ManagerName = dt.Rows[i]["ManagerName"].ToString(),
                            AcademicCode = int.Parse(dt.Rows[i]["AcademicCode"].ToString()),
                            fundriserdamount = long.Parse(dt.Rows[i]["fundriserdamount"].ToString()),
                            SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                            fundRelised = long.Parse(dt.Rows[i]["fundRelised"].ToString()),

                            Status = "Success"
                        });
                    }
                }
                else
                {
                    projects.Add(new vmfundstatus { Status = "There is no data" });
                }
            }
        }
        catch (Exception)
        {
            projects.Add(new vmfundstatus { Status = "Error" });
        }
        return projects;
    }

    [WebMethod]
    public List<vmfundstatus> GetFiveStarProjectCoutYearWise(int ManagerId)
    {
        List<vmfundstatus> projects = new List<vmfundstatus>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_FIVE_STAR_PROJECTCOUNT";
                cmd.Parameters.AddWithValue("P_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        projects.Add(new vmfundstatus
                        {

                            ManagerName = dt.Rows[i]["ManagerName"].ToString(),
                            AcademicYear = dt.Rows[i]["AcademicYear"].ToString(),
                            FiveStarRating = long.Parse(dt.Rows[i]["FiveStarRating"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    projects.Add(new vmfundstatus { Status = "There is no data" });
                }
            }
        }
        catch (Exception)
        {
            projects.Add(new vmfundstatus { Status = "Error" });
        }
        return projects;
    }

    [WebMethod]
    public List<vmStudentReq> GetStudentRequestList(long ManagerId)
    {
        List<vmStudentReq> StudentReuest = new List<vmStudentReq>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_STUDENT_REQUEST";
                cmd.Parameters.AddWithValue("P_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReuest.Add(new vmStudentReq
                        {
                            Lead_id = dt.Rows[i]["Lead_id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Email_id = dt.Rows[i]["Email_id"].ToString(),
                            Student_MobileNo = dt.Rows[i]["Student_MobileNo"].ToString(),
                            Message = dt.Rows[i]["Message"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    StudentReuest.Add(new vmStudentReq { Status = "There is no request here" });
                }
            }
        }
        catch (Exception)
        {
            StudentReuest.Add(new vmStudentReq { Status = "Error" });
        }
        return StudentReuest;

    }



    [WebMethod]
    public List<vmProjectCounts> GetThemeWiseRatingCount(int ManagerId)
    {
        List<vmProjectCounts> projects = new List<vmProjectCounts>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_THEME_WISE_FIVESTAR_COUNT";
                cmd.Parameters.AddWithValue("P_ManagerCode", ManagerId);

                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        projects.Add(new vmProjectCounts
                        {
                            ThemeName = dt.Rows[i]["ThemeName"].ToString(),
                            FiveStarRating = int.Parse(dt.Rows[i]["FiveStarRating"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    projects.Add(new vmProjectCounts { Status = "There is no projects" });
                }
            }
        }
        catch (Exception)
        {
            projects.Add(new vmProjectCounts { Status = "Error" });
        }
        return projects;
    }




    [WebMethod]
    public string ApplyStudentFeepaid(string Lead_Id, int isFeePaid)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_STUDENT_FEE_APPLAYED";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("P_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("P_isFeePaid", isFeePaid);

                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "Unable to Apply";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }



    [WebMethod]
    public string ApplyStudentFeepaidd(string Lead_Id)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandText = "UPDATE student_registration SET isFeePaid=1 where Lead_Id IN(" + Lead_Id + ")";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "Success";


                string gstrQstr = "select DeviceId,ManagerCode,Username from user_device_details inner join student_registration on Username=Lead_Id where Username IN(" + Lead_Id + ")";
                MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        SaveManagerLog(dt.Rows[j]["Username"].ToString(), "" + dt.Rows[j]["ManagerCode"].ToString() + "", "Registration Fees received Kindly Add your projects now(Manager)", "Fees approved");
                        FCMPushNotification.AndroidPush(dt.Rows[j]["DeviceId"].ToString(), "Registration Fees received Kindly Add your projects now", "Student", "Empty");
                    }
                }

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }



    [WebMethod]
    public List<vmconts> GetStudntRegAndPaiedStudentconts(long ManagerId)
    {
        List<vmconts> StudentRegCount = new List<vmconts>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_REG_AND_PAID_COUNT";
                cmd.Parameters.AddWithValue("P_ManagerCode", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentRegCount.Add(new vmconts
                        {
                            TotalRegistration = int.Parse(dt.Rows[i]["TotalRegistration"].ToString()),
                            FeePaiedStudent = int.Parse(dt.Rows[i]["FeePaiedStudent"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    StudentRegCount.Add(new vmconts { Status = "There is no students here" });
                }
            }
        }
        catch (Exception)
        {
            StudentRegCount.Add(new vmconts { Status = "Error" });
        }
        return StudentRegCount;

    }

    protected void SendEmail(string bodyString, string MailId, string Title)
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
            mailMessage.To.Add(new MailAddress(recepientEmail));
            // mailMessage.CC.Add(new MailAddress(lblManagerEmailId.Text.ToString()));
            mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
            mailMessage.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
            // mailMessage.Bcc.Add(new MailAddress("anisha.c@dfmail.org"));
            //mailMessage.Bcc.Add(new MailAddress("abhinandan.k@dfmail.org"));
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
    public string SaveUnapprovedprojectDetails1(int PDId, string Lead_Id, string Title,
       string Theme, int BeneficiaryNo, string Objectives, string ActionPlan, string Beneficiaries, string Placeofimplement,
       string CurrentSituation, string ProjectStartDate, string ProjectEndDate, int IsImpactProject)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_STUDENT_PENDING_PROJECTS_LATEST";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Theme", Theme);
                cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
                cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiaries, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Parameters.AddWithValue("p_CurrentSituation", Regex.Replace(CurrentSituation, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ProjectStartDate", ProjectStartDate);
                cmd.Parameters.AddWithValue("p_ProjectEndDate", ProjectEndDate);
                cmd.Parameters.AddWithValue("p_IsImpactProject", IsImpactProject);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";

                    //  FCMPushNotification.SendNotification("LEAD", "Edit", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title,ManagerId from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (IsImpactProject == 1)
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), " Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Selected as Impactful Project", "Student", "Empty");
                        }
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), " Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Edited by Manager", "Student", "Empty");

                        SaveManagerLog(Lead_Id.ToString(), " " + dt.Rows[0]["ManagerId"].ToString() + "", "Title: " + dt.Rows[0]["Title"].ToString() + ",Edited by Manager", "Save Project (Manager)");
                    }
                }
                else
                {
                    status = "unable to update project";
                }
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]

    public string UpdateUnApprovedprojectDetails1(int PDId, string Lead_Id, string Title, string Theme, int BeneficiaryNo,
           string Objectives, string ActionPlan, int SanctionAmount, string ManagerComments, string ProjectStatus
           , string Beneficiaries, string Placeofimplement, string CurrentSituation, string ProjectStartDate, string ProjectEndDate,
           int IsImpactProject)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_APPROVE_PROJECTS_LATEST";
                cmd.Parameters.AddWithValue("p_PDId", PDId); //Regex.Replace(TextBox2.Text, "'", "`").Trim();
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Theme", Theme);
                cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
                cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_SanctionAmount", SanctionAmount);
                cmd.Parameters.AddWithValue("p_ManagerComments", Regex.Replace(ManagerComments, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ProjectStatus", ProjectStatus);
                cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiaries, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Parameters.AddWithValue("p_CurrentSituation", Regex.Replace(CurrentSituation, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ProjectStartDate", Regex.Replace(ProjectStartDate.ToString(), "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ProjectEndDate", Regex.Replace(ProjectEndDate.ToString(), "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ImpactProject", IsImpactProject);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";

                    //  FCMPushNotification.SendNotification("LEAD", "Approved", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (IsImpactProject == 1)
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Selected as Impactful Project", "Student", "Empty");
                        }
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Approved", "Student", "Empty");
                    }
                    string Mailsend = "select P.ManagerId,P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ManagerId,P.ActionPlan,P.ManagerComments, P.SanctionAmount,P.ProjectStatus,S.StudentName,S.MailId,S.MobileNo,C.College_Name from project_description as P inner join student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ") has been Approved. </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li><li><b>Manager Comment:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + "<br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project approved successfully");

                        SaveManagerLog(Lead_Id.ToString(), "" + dt2.Rows[0]["ManagerId"].ToString() + "", "Title:" + dt2.Rows[0]["Title"].ToString() + ",Reason:" + dt2.Rows[0]["ManagerComments"].ToString() + "", "Approved Project (Manager)");
                    }
                }
                else
                {

                    status = "Unable to update project details";
                }
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }


    [WebMethod]

    public string UpdateComplitedprojectDetails1(int PDId, string Lead_Id, string ManagerComments, string Placeofimplement, int FundsRaised, string Challenge,
           string Learning, string AsAStory, double Rating, string ProjectStatus, string CurrentSituation, string Resource,
           int InnovationRating, int LeadershipRating, int RiskTakenRating, int ImpactRating, int FundRaisedRating, int HoursSpend,
           string ProjectStartDate, string ProjectEndDate, string Level, string SDG_Goal, string Collaboration_Supported,
     string Permission_And_Activities, string Experience_Of_Initiative, string Lacking_initiative,
    string Against_Tide, string Cross_Hurdles, string Entrepreneurial_Venture, string Government_Awarded, string Leadership_Roles, long ResourceAmount)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_APP_UPDATE_COMPLITED_PROJECT_DETAILS_LATEST";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Parameters.AddWithValue("p_FundsRaised", FundsRaised);
                cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(Challenge, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(Learning, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(AsAStory, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ManagerComments", Regex.Replace(ManagerComments, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Rating", Rating);
                cmd.Parameters.AddWithValue("p_ProjectStatus", ProjectStatus);
                cmd.Parameters.AddWithValue("p_CurrentSituation", Regex.Replace(CurrentSituation, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Resource", Regex.Replace(Resource, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ResourceAmount", ResourceAmount);
                cmd.Parameters.AddWithValue("p_InnovationRating", InnovationRating);
                cmd.Parameters.AddWithValue("p_LeadershipRating", LeadershipRating);
                cmd.Parameters.AddWithValue("p_RiskTakenRating", RiskTakenRating);
                cmd.Parameters.AddWithValue("p_ImpactRating", ImpactRating);
                cmd.Parameters.AddWithValue("p_FundRaisedRating", FundRaisedRating);
                cmd.Parameters.AddWithValue("P_HoursSpend", HoursSpend);
                cmd.Parameters.AddWithValue("P_ProjectStartDate", ProjectStartDate);
                cmd.Parameters.AddWithValue("P_ProjectEndDate", ProjectEndDate);
                // string L1 = Level.Replace(" ", string.Empty);
                cmd.Parameters.AddWithValue("P_Level", Level.ToString());
                cmd.Parameters.AddWithValue("p_Collaboration_Supported", Regex.Replace(Collaboration_Supported, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Permission_And_Activities", Regex.Replace(Permission_And_Activities, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Experience_Of_Initiative", Regex.Replace(Experience_Of_Initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Lacking_initiative", Regex.Replace(Lacking_initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Against_Tide", Regex.Replace(Against_Tide, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Cross_Hurdles", Regex.Replace(Cross_Hurdles, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Entrepreneurial_Venture", Regex.Replace(Entrepreneurial_Venture, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Government_Awarded", Regex.Replace(Government_Awarded, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Leadership_Roles", Regex.Replace(Leadership_Roles, "'", "`").Trim());

                //   cmd.Parameters.AddWithValue("p_TotalResourses", TotalResourses);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    if ((SDG_Goal != null) || (SDG_Goal != ""))
                    {
                        int j = 0;
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(SDG_Goal);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_INSERT_PROJECT_SDG_GOALS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDID", PDId);
                                cmd.Parameters.AddWithValue("p_SDG_GOAL", item.Value);
                                cmd.Parameters.AddWithValue("p_LoopCount", j);
                                int iSaved = cmd.ExecuteNonQuery();
                                j++;
                            }
                        }
                    }
                    status = "Success";
                    string gstrQstr = "select DeviceId,Title,count(PDId) as Projectcount,ManagerId from user_device_details inner join project_description on Lead_Id=Username  where Username='" + Lead_Id.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been completed successfully", "Student", "Empty");
                        if (dt.Rows[0]["Projectcount"].ToString() == "1")
                        {
                            SaveManagerLog(Lead_Id.ToString(), "" + dt.Rows[0]["ManagerId"].ToString() + "", "Congratulation ! You have Completed your first Project", "Completed");
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulation ! You have Completed your first Project", "Student", "Empty");
                        }
                    }
                    string Mailsend = "select P.ManagerId, P.Lead_Id, S.StudentName, S.MailId,S.MobileNo, C.College_Name, P.Title, P.BeneficiaryNo,P.Beneficiaries,P.Objectives, P.Placeofimplement, p.SanctionAmount, P.ActionPlan,P.Challenge,P.Learning, P.AsAStory,P.FundsRaised, P.Rating,P.ManagerComments,(Select Sum(Amount) from project_fund_details where PDId=P.PDId) as FundsReceived from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId = S.CollegeCode where P.PDId ='" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ") has been completed successfully </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b> SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + " <br><br></li><li><b> FundsReceived:</b> " + dt2.Rows[0]["FundsReceived"].ToString() + " <br><br></li><li><b> FundsRaised:</b> " + dt2.Rows[0]["FundsRaised"].ToString() + " <br><br></li><li><b>Challenges:</b> " + dt2.Rows[0]["Challenge"].ToString() + " <br><br></li><li><b>Learning:</b> " + dt2.Rows[0]["Learning"].ToString() + " <br><br></li><li><b>AsAStory:</b> " + dt2.Rows[0]["AsAStory"].ToString() + " <br><br></li><li><b>Rating:</b> " + dt2.Rows[0]["Rating"].ToString() + " <br><br></li><li><b>ManagerComments:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + " <br><br></li></ol><br><br>");
                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project completed successfully");
                        SaveManagerLog(Lead_Id.ToString(), "" + dt2.Rows[0]["ManagerId"].ToString() + "", "Title:" + dt2.Rows[0]["Title"].ToString() + ",Reason:" + dt2.Rows[0]["ManagerComments"].ToString() + "", "Completed Project (Manager)");
                    }
                }
                else
                {
                    status = "Unable to update project details";
                }

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public List<vmstudentreg> GetApplyTshirtRequestlist(long ManagerId)
    {
        List<vmstudentreg> StudentReuest = new List<vmstudentreg>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TSHIRT_APPLYED_STUDENT";
                cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReuest.Add(new vmstudentreg
                        {
                            Lead_id = dt.Rows[i]["Lead_id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            CollegeName = dt.Rows[i]["College_Name"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            TshirtSize = dt.Rows[i]["TshirtSize"].ToString(),
                            projectcount = int.Parse(dt.Rows[i]["projectcount"].ToString()),
                            RequestedId = int.Parse(dt.Rows[i]["RequestedId"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    StudentReuest.Add(new vmstudentreg { Status = "There is no Tshirt request here" });
                }
            }
        }
        catch (Exception)
        {
            StudentReuest.Add(new vmstudentreg { Status = "Error" });
        }
        return StudentReuest;

    }


    [WebMethod]
    public List<vmstudentreg> GetSanctionTshirtList(long ManagerId)
    {
        List<vmstudentreg> StudentReuest = new List<vmstudentreg>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TSHIRT_SANCTION_STUDENT";
                cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReuest.Add(new vmstudentreg
                        {
                            Lead_id = dt.Rows[i]["Lead_id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            CollegeName = dt.Rows[i]["College_Name"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            TshirtSize = dt.Rows[i]["TshirtSize"].ToString(),
                            projectcount = int.Parse(dt.Rows[i]["projectcount"].ToString()),
                            RequestedId = int.Parse(dt.Rows[i]["RequestedId"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    StudentReuest.Add(new vmstudentreg { Status = "There is No Sanction Tshirt request here" });
                }
            }
        }
        catch (Exception)
        {
            StudentReuest.Add(new vmstudentreg { Status = "Error" });
        }
        return StudentReuest;

    }

    //[WebMethod]
    //public string ApproveForTShirt(string TshirtList, string ManagerId, string Size, string RequestType)
    //{
    //    string status = "";
    //    //  vmtshirt tshirt = new vmtshirt();
    //    try
    //    {
    //        using (MySqlConnection con = new MySqlConnection(connection))
    //        {
    //            con.Open();
    //            MySqlCommand cmd = new MySqlCommand();
    //            if ((TshirtList != null) || (TshirtList != ""))
    //            {
    //                var Tshirt = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(TshirtList);
    //                foreach (var item in Tshirt)
    //                {
    //                    cmd = new MySqlCommand();
    //                    cmd.Connection = con;
    //                    cmd.CommandText = "UPS_APP_MANAGER_TSHIRTLISTAPPROVAL";
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
    //                    cmd.Parameters.AddWithValue("p_Size", Size);
    //                    cmd.Parameters.AddWithValue("p_RequestType", RequestType);
    //                    cmd.Parameters.AddWithValue("p_Lead_id", item.Key);
    //                    cmd.Parameters.AddWithValue("p_RequestId", item.Value);
    //                    int i = cmd.ExecuteNonQuery();
    //                    if (i > 0)
    //                    {
    //                        status = "Success";
    //                        string gstrQstr = "select DeviceId,Lead_Id,TshirtSize from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + item.Value.ToString() + "'";
    //                        MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
    //                        MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
    //                        DataTable dt = new DataTable();
    //                        da.Fill(dt);
    //                        if (dt.Rows.Count > 0)
    //                        {
    //                            if (RequestType == "Approved")
    //                            {
    //                                SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request approved kindly collect it from your Mentor(Manager)", "Approved");
    //                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request approved kindly collect it from your Mentor", "Student", "Empty");
    //                            }
    //                            else if (RequestType == "ApproveRollBacked")
    //                            {
    //                                SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request UnApproved (Manager)", "UnApproved");
    //                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request UnApproved ", "Student", "Empty");
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        status = "unable to approve t-shirt";
    //                    }



    //                }
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        status = ex.Message.ToString();
    //    }
    //    return status;
    //}

    [WebMethod]
    public string ApproveForTShirt(string TshirtList, string ManagerId, string RequestType)
    {
        string status = "";
        vmtshirt tshirt = new vmtshirt();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                if ((TshirtList != null) || (TshirtList != ""))
                {
                    var Tshirt = JsonConvert.DeserializeObject<Dictionary<string, string>>(TshirtList);
                    foreach (var item in Tshirt)
                    {
                        cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "UPS_APP_MANAGER_TSHIRTLISTAPPROVAL_NEW";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
                        cmd.Parameters.AddWithValue("p_RequestType", RequestType);
                        cmd.Parameters.AddWithValue("p_RequestId", item.Key);
                        cmd.Parameters.AddWithValue("p_Size", item.Value);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            status = "Success";
                            //  string gstrQstr = "select DeviceId,Lead_Id,TshirtSize from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + item.Value.ToString() + "'";

                            string gstrQstr = "Select DeviceId,username from user_device_details" + " " +
                            "where username=(Select Lead_Id from leadtestbed.student_tshirt_allotment where RequestedId=" + item.Key + ")";
                            MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                if (RequestType == "Approved")
                                {
                                    SaveManagerLog("" + dt.Rows[0]["username"].ToString() + "", ManagerId.ToString(), "" + item.Value + ":T-shirt request approved kindly collect it from your Mentor(Manager)", "Approved");
                                    FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), "T-shirt request approved kindly collect it from your Mentor", "Student", "Empty");
                                }
                                else if (RequestType == "ApproveRollBacked")
                                {
                                    SaveManagerLog("" + dt.Rows[0]["username"].ToString() + "", ManagerId.ToString(), "" + item.Value + ":T-shirt request UnApproved (Manager)", "UnApproved");
                                    FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), "T-shirt request UnApproved ", "Student", "Empty");
                                }
                            }
                        }
                        else
                        {
                            status = "unable to approve t-shirt";
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
    }

    //[WebMethod]
    //public string ApproveForTShirt(string TshirtList, string ManagerId, string Size, string RequestType)
    //{
    //    string status = "";
    //    //  vmtshirt tshirt = new vmtshirt();
    //    try
    //    {
    //        using (MySqlConnection con = new MySqlConnection(connection))
    //        {
    //            con.Open();
    //            MySqlCommand cmd = new MySqlCommand();
    //            if ((TshirtList != null) || (TshirtList != ""))
    //            {
    //                var Tshirt = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(TshirtList);
    //                foreach (var item in Tshirt)
    //                {
    //                    cmd = new MySqlCommand();
    //                    cmd.Connection = con;
    //                    cmd.CommandText = "UPS_APP_MANAGER_TSHIRTLISTAPPROVAL";
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
    //                    cmd.Parameters.AddWithValue("p_Size", Size);
    //                    cmd.Parameters.AddWithValue("p_RequestType", RequestType);
    //                    cmd.Parameters.AddWithValue("p_Lead_id", item.Key);
    //                    cmd.Parameters.AddWithValue("p_RequestId", item.Value);
    //                    int i = cmd.ExecuteNonQuery();
    //                    if (i > 0)
    //                    {
    //                        status = "Success";
    //                        string gstrQstr = "select DeviceId,Lead_Id,TshirtSize from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + item.Value.ToString() + "'";
    //                        MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
    //                        MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
    //                        DataTable dt = new DataTable();
    //                        da.Fill(dt);
    //                        if (dt.Rows.Count > 0)
    //                        {
    //                            if (RequestType == "Approved")
    //                            {
    //                                SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request approved kindly collect it from your Mentor(Manager)", "Approved");
    //                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request approved kindly collect it from your Mentor", "Student", "Empty");
    //                            }
    //                            else if (RequestType == "ApproveRollBacked")
    //                            {
    //                                SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request UnApproved (Manager)", "UnApproved");
    //                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request UnApproved ", "Student", "Empty");
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        status = "unable to approve t-shirt";
    //                    }



    //                }
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        status = ex.Message.ToString();
    //    }
    //    return status;
    //}

    //[WebMethod]
    //public string ApproveForTShirt(string TshirtList, string ManagerId, string Size, string RequestType)
    //{
    //    string status = "";
    //    //  vmtshirt tshirt = new vmtshirt();
    //    try
    //    {
    //        using (MySqlConnection con = new MySqlConnection(connection))
    //        {
    //            con.Open();
    //            MySqlCommand cmd = new MySqlCommand();
    //            if ((TshirtList != null) || (TshirtList != ""))
    //            {
    //                var Tshirt = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(TshirtList);
    //                foreach (var item in Tshirt)
    //                {
    //                    cmd = new MySqlCommand();
    //                    cmd.Connection = con;
    //                    cmd.CommandText = "UPS_APP_MANAGER_TSHIRTLISTAPPROVAL";
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
    //                    cmd.Parameters.AddWithValue("p_Size", Size);
    //                    cmd.Parameters.AddWithValue("p_RequestType", RequestType);
    //                    cmd.Parameters.AddWithValue("p_Lead_id", item.Key);
    //                    cmd.Parameters.AddWithValue("p_RequestId", item.Value);
    //                    int i = cmd.ExecuteNonQuery();
    //                    if (i > 0)
    //                    {
    //                        status = "Success";
    //                        string gstrQstr = "select DeviceId,Lead_Id,TshirtSize from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + item.Value.ToString() + "'";
    //                        MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
    //                        MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
    //                        DataTable dt = new DataTable();
    //                        da.Fill(dt);
    //                        if (dt.Rows.Count > 0)
    //                        {
    //                            if (RequestType == "Approved")
    //                            {
    //                                SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request approved kindly collect it from your Mentor(Manager)", "Approved");
    //                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request approved kindly collect it from your Mentor", "Student", "Empty");
    //                            }
    //                            else if (RequestType == "ApproveRollBacked")
    //                            {
    //                                SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request UnApproved (Manager)", "UnApproved");
    //                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request UnApproved ", "Student", "Empty");
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        status = "unable to approve t-shirt";
    //                    }



    //                }
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        status = ex.Message.ToString();
    //    }
    //    return status;
    //}

    [WebMethod]
    public vmtshirt GetAssignedTshirtlist(int ManagerId)
    {
        vmtshirt tshirt = new vmtshirt();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_ALLOTED_STHIRT_COUNT";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        tshirt.AllotedS = int.Parse(dr["AllotedS"].ToString());
                        tshirt.AllotedM = int.Parse(dr["AllotedM"].ToString());
                        tshirt.AllotedL = int.Parse(dr["AllotedL"].ToString());
                        tshirt.AllotedXL = int.Parse(dr["AllotedXL"].ToString());
                        tshirt.AllotedXXL = int.Parse(dr["AllotedXXL"].ToString());
                        tshirt.UsedS = int.Parse(dr["UsedS"].ToString());
                        tshirt.UsedM = int.Parse(dr["UsedM"].ToString());
                        tshirt.UsedL = int.Parse(dr["UsedL"].ToString());
                        tshirt.UsedXL = int.Parse(dr["UsedXL"].ToString());
                        tshirt.UsedXXL = int.Parse(dr["UsedXXL"].ToString());
                        tshirt.Status = "Success";
                    }
                }
                else
                {
                    tshirt.Status = "There is no tshirt ";
                }
            }
        }
        catch (Exception)
        {
            tshirt.Status = "Error";
        }
        return tshirt;
    }

    [WebMethod]
    public string ExchangeForTShirt(string TshirtList, string ManagerId, string Size, String NewTshirtSize, String Reason)
    {
        string status = "";
        // vmtshirt tshirt = new vmtshirt();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                if ((TshirtList != null) || (TshirtList != ""))
                {
                    var Tshirt = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(TshirtList);
                    if (Tshirt != null)
                    {
                        foreach (var item in Tshirt)
                        {
                            cmd = new MySqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "UPS_APP_MANAGER_TSHIRTLISTEXCHANGE";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_ManagerCode", ManagerId);
                            cmd.Parameters.AddWithValue("p_Size", Size);
                            cmd.Parameters.AddWithValue("p_Lead_id", item.Key);
                            cmd.Parameters.AddWithValue("p_RequestId", item.Value);
                            cmd.Parameters.AddWithValue("p_TshirtSize", NewTshirtSize);
                            cmd.Parameters.AddWithValue("p_Remark", Reason);

                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                status = "Success";

                                string gstrQstr = "select DeviceId,Lead_Id,TshirtSize from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + item.Value.ToString() + "'";
                                MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request modified(Manager)", "Exchange");
                                    FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "	T-shirt request modified", "Student", "Empty");

                                }
                            }
                            else
                            {
                                status = "unable to exchange the t-shirt";
                            }


                        }
                    }
                    else
                    {
                        status = "List is Emty";
                    }

                }


            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public string RejectForTshirt(int ManagerId, int RequestId, String Remark)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_REJECT_TSHIRT";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_RequestId", RequestId);
                cmd.Parameters.AddWithValue("p_Reson", Remark);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";

                    string gstrQstr = "select DeviceId,Lead_Id,TshirtSize from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + RequestId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        SaveManagerLog("" + dt.Rows[0]["Lead_Id"].ToString() + "", ManagerId.ToString(), "" + dt.Rows[0]["TshirtSize"].ToString() + ":T-shirt request rejected(Manager)" + " " + Remark.ToString(), "Rejected");

                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request rejected", "Student", "Empty");
                    }

                }
                else
                {
                    status = "unable to reject the Tshirt";
                }

            }
        }
        catch (Exception ex)
        {
            status = "Error :" + ex.ToString();
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
                cmd.CommandText = "USP_INSERT_NOTIFICATION_MANAGER";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_ManagerCode", ManagerCode);
                cmd.Parameters.AddWithValue("p_Message", Message);
                cmd.Parameters.AddWithValue("p_type", Type);
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";
                else
                    status += "your notification is added";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        //  return status;
    }

    [WebMethod]
    public string GetCollegeMaster(string ManagerId)
    {
        System.Collections.Generic.List<object> Clg = new List<object>();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_COLLEGE_MASTER";
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Clg.Add(new
                        {

                            Collegeid = dr["Collegeid"].ToString(),
                            CollegeName = dr["College_name"].ToString(),
                            Status = "Success"
                        });


                    }
                }
                else
                {
                    Clg.Add(new
                    {
                        Status = "College Is Not Assigned"
                    });
                }
            }
        }
        catch (Exception)
        {
            Clg.Add(new
            {
                Status = "Error"
            });
        }
        return (new JavaScriptSerializer().Serialize(Clg));
    }

    [WebMethod]
    public List<vmManagerOpenRequest> GetOpenRequest(string ManagerId)
    {
        List<vmManagerOpenRequest> vm = new List<vmManagerOpenRequest>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_MANAGER_REQUEST_OPEN_CLOSE";
                cmd.Parameters.AddWithValue("Manager_Id", ManagerId);
                cmd.Parameters.AddWithValue("Request_Status", 1);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vm.Add(new vmManagerOpenRequest
                        {
                            Ticket_No = dt.Rows[i]["request_Id"].ToString(),
                            Request_Date = dt.Rows[i]["Request_Date"].ToString(),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            Student_Name = dt.Rows[i]["StudentName"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            RequestHead_Id = dt.Rows[i]["Request_Head_Id"].ToString(),
                            Request_type = dt.Rows[i]["Head_Name"].ToString(),
                            Project_Id = dt.Rows[i]["PDID"].ToString(),
                            ProjectName = dt.Rows[i]["ProjectName"].ToString(),
                            Request_Message = dt.Rows[i]["Request_Message"].ToString(),
                            Request_Priority = dt.Rows[i]["Request_Priority"].ToString(),
                            College_Name = dt.Rows[i]["College_Name"].ToString(),
                            MailId = dt.Rows[i]["mailid"].ToString(),
                            Manager_MailId = dt.Rows[i]["ManagerMailid"].ToString(),
                            Status = "Success"
                        });


                    }

                }
                else
                {
                    vm.Add(new vmManagerOpenRequest
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {

            vm.Add(new vmManagerOpenRequest
            {
                Status = "Error"
            });
        }
        return vm;
    }
    [WebMethod]
    public List<vmManagerOpenRequest> GetClosedRequest(string ManagerId)
    {
        List<vmManagerOpenRequest> vm = new List<vmManagerOpenRequest>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_MANAGER_REQUEST_OPEN_CLOSE";
                cmd.Parameters.AddWithValue("Manager_Id", ManagerId);
                cmd.Parameters.AddWithValue("Request_Status", 2);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vm.Add(new vmManagerOpenRequest
                        {
                            Ticket_No = dt.Rows[i]["request_Id"].ToString(),
                            Request_Date = dt.Rows[i]["Request_Date"].ToString(),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            Student_Name = dt.Rows[i]["StudentName"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            RequestHead_Id = dt.Rows[i]["Request_Head_Id"].ToString(),
                            Request_type = dt.Rows[i]["Head_Name"].ToString(),
                            Project_Id = dt.Rows[i]["PDID"].ToString(),
                            ProjectName = dt.Rows[i]["ProjectName"].ToString(),
                            Request_Message = dt.Rows[i]["Request_Message"].ToString(),
                            Response_Message = dt.Rows[i]["Response_Message"].ToString(),
                            Request_Priority = dt.Rows[i]["Request_Priority"].ToString(),
                            College_Name = dt.Rows[i]["College_Name"].ToString(),
                            MailId = dt.Rows[i]["mailid"].ToString(),
                            Manager_MailId = dt.Rows[i]["ManagerMailid"].ToString(),
                            Status = "Success",
                            Respond_Date = dt.Rows[i]["Response_Date"].ToString()

                        });


                    }

                }
                else
                {
                    vm.Add(new vmManagerOpenRequest
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {

            vm.Add(new vmManagerOpenRequest
            {
                Status = "Error"
            });
        }
        return vm;
    }

    [WebMethod]
    public string UpdateProjectAsImpact(long PDId, int isImpactProject)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_UPDATE_ISIMPACTPROJECT";
                cmd.Parameters.AddWithValue("p_PDID", PDId);
                cmd.Parameters.AddWithValue("p_isImpactProject", isImpactProject);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";
                    if (isImpactProject == 1)
                    {
                        string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                        MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (isImpactProject == 1)
                            {
                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), " Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Selected as Impactful Project", "Student", "Empty");
                            }

                        }
                    }


                }
                else
                {
                    status = "Failed";
                }


            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public List<vmGetMentorMentee> GetMentorMentee(long PDId)
    {
        List<vmGetMentorMentee> vm = new List<vmGetMentorMentee>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_MENTOR_MENTEE_DETAILS";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vm.Add(new vmGetMentorMentee
                        {
                            Comments = dt.Rows[i]["comments"].ToString(),
                            UserType = dt.Rows[i]["User_Type"].ToString(),
                            ManagerName = dt.Rows[i]["ManagerName"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            ReplyTime = dt.Rows[i]["Reply_Time"].ToString(),
                            ProjectStatus = dt.Rows[i]["ProjectStatus"].ToString(),
                            Status = "Success"
                        });


                    }

                }
                else
                {
                    vm.Add(new vmGetMentorMentee
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {

            vm.Add(new vmGetMentorMentee
            {
                Status = "Error"
            });
        }
        return vm;
    }

    [WebMethod]
    public string Update_Manager_CloseTicket(string Ticket_Id, string Lead_Id, string Request_Message, string Response_Message, string ManagerId, string Head_Id, bool IsDocCreate,
        string ValidFromDate, string ValidToDate, string StudentName, string CollegeName,
        string MailId, string ProjectTitle, string Manager_Mailid)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_MANAGER_STUDENT_TICKET";
                cmd.Parameters.AddWithValue("p_Ticket", Ticket_Id);
                cmd.Parameters.AddWithValue("p_Manager_Id", ManagerId);
                cmd.Parameters.AddWithValue("p_Response_Message", Regex.Replace(Response_Message, "'", "`").Trim());
                if ((Head_Id.ToString() == "1") && (IsDocCreate == true))
                {
                    cmd.Parameters.AddWithValue("p_isDocCreated", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("p_isDocCreated", 0);
                }
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";
                    if ((Head_Id.ToString() == "1") && (IsDocCreate == true))
                    {
                        string SlapDest = "";
                        string Source = "";
                        Source = "~/Reports/Letter_Size.pdf";
                        string P1 = "";

                        DateTime FromDate = DateTime.ParseExact(ValidFromDate.ToString().Substring(0, 10), "yyyy-M-d", CultureInfo.InvariantCulture);
                        DateTime ToDate = DateTime.ParseExact(ValidToDate.ToString().Substring(0, 10), "yyyy-M-d", CultureInfo.InvariantCulture);

                        SlapDest = Lead_Id.ToString() + "_" + "_" + Ticket_Id.ToString() + "_" + MailId.ToString() + ".pdf";
                        DeleteExistingFile(SlapDest);
                        var reader = new PdfReader(Server.MapPath(Source));
                        var document = new Document(reader.GetPageSizeWithRotation(1));
                        var fileStream = new System.IO.FileStream(Server.MapPath("~/Certificate/ApprovalLetter/" + SlapDest), FileMode.Create, FileAccess.Write);
                        var writer = PdfWriter.GetInstance(document, fileStream);
                        document.Open();
                        document.NewPage();

                        var contentByte = writer.DirectContent;
                        PdfGState graphicsState = new PdfGState();

                        var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                        var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);
                        var baseFont2 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/ARIAL.TTF"), BaseFont.IDENTITY_H, true);
                        contentByte.SetFontAndSize(baseFont, 10.5f);


                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont, 11);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, System.DateTime.Today.ToString("dd-MM-yyyy"), 510, 670, 0);

                        contentByte.SetFontAndSize(baseFont2, 10.5f);
                        P1 = "The below mentioned student is selected as a LEADer in the LEAD program to work along with his team for the self-";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 610, 0);

                        contentByte.SetFontAndSize(baseFont2, 10.5f);
                        P1 = "initiated community development leadership project to bring the positive change in the society. We expect and request ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 590, 0);

                        P1 = "for your best co-operation in execution of the Leadership initiative. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 570, 0);

                        P1 = "Leader’s Name    :" + " " + StudentName.ToString();
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 540, 0);

                        P1 = "Project Title         :" + " " + ProjectTitle.ToString();
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 520, 0);

                        P1 = "College                :" + " " + CollegeName.ToString();
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 500, 0);

                        P1 = "Project Duration  :" + " " + FromDate.ToString("dd-MM-yyyy") + " " + "To" + " " + ToDate.ToString("dd-MM-yyyy");
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 480, 0);

                        P1 = "LEAD is an initiative of the Deshpande Foundation, Hubballi a program offering college students an opportunity to make ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 450, 0);

                        P1 = "change in their world. The aim of the program is to provide a platform for the students to explore their LEADership skills ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 430, 0);

                        P1 = "with practical exposure and take initiation in solving the problem in the society. The Program is striving to build the";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 410, 0);

                        P1 = "nation of motivated young LEADers. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 390, 0);

                        P1 = "The Deshpande Foundation strives to create social ecosystems of entrepreneurship and innovation throughout India. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 360, 0);

                        P1 = "Founded in 2007 by Mrs. Jaishree Deshpande and Dr. Gururaj 'Desh' Deshpande, the premise behind the Deshpande ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 340, 0);

                        P1 = "Foundation is to spread the belief that local leadership, innovation, and entrepreneurship can be catalysts of ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 320, 0);


                        P1 = "development. The aim of the Deshpande Foundation is to improve the quality of life for all. This means making quality ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 300, 0);

                        P1 = "education, healthy livelihoods, and access to healthcare available to every citizen. In pursuit of that goal, we have ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 280, 0);

                        P1 = "partnered with more than one hundred organizations engaged in the aforementioned fields. ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 260, 0);

                        P1 = "For further information about the program kindly visit: www.leadcampus.org ";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 230, 0);

                        P1 = "Warm Regards,";
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, P1.ToString(), 20, 210, 0);


                        graphicsState.BlendMode = PdfGState.BM_NORMAL;
                        contentByte.SetGState(graphicsState);
                        contentByte.EndText();
                        PdfImportedPage page = writer.GetImportedPage(reader, 1);
                        contentByte.AddTemplate(page, 0, 0);
                        graphicsState.BlendMode = PdfGState.BM_DARKEN;
                        contentByte.SetGState(graphicsState);
                        string imagepath = "";
                        if (Head_Id.ToString() == "1") // Project Approval Letter
                        {
                            imagepath = (Server.MapPath("~/Reports/ProjectApprovalLetter.jpg"));
                        }
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagepath);
                        image.ScaleToFit(820f, 810f);
                        image.SetAbsolutePosition(-2, -4);
                        image.Alignment = Element.ALIGN_LEFT;
                        // image.ScalePercent(22f);
                        document.Add(image);
                        document.Close();
                        fileStream.Close();
                        writer.Close();
                        reader.Close();
                        LeadBL BLobj = new LeadBL();
                        string Subject = "Project Approval Letter For " + ProjectTitle.ToString();
                        string Body = BLobj.PopulateBody(Lead_Id.ToString(),
                       " <b style='font-size:larger'>Dear  " + StudentName.ToString() + ", " + "Project Approval Letter is sent Please Find below Attached .pdf</b>", " Details below : ", "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>Name :</b> " + StudentName.ToString() + "<br><br></li><li><b>Project Title:</b> " + ProjectTitle.ToString() + "<br><br></li> " + "" +
                      "<li><b>Request Id</b> " + Ticket_Id.ToString() + "<br><br><li><b>Request Message :</b> " + Request_Message.ToString() + "<br><br></li><li><b>Response Message :</b> " + Response_Message.ToString() + "<br><br></li>");
                        SendHtmlFormattedEmailForRequest(MailId.ToString(), Manager_Mailid.ToString(), Subject.ToString(), Body.ToString(), Server.MapPath("~/Certificate/ApprovalLetter/") + SlapDest.ToString());
                    }
                    else
                    {
                        LeadBL BLobj = new LeadBL();
                        string Subject = "your request - " + " " + "[" + Ticket_Id.ToString() + "]" + " " + "Has been Closed";
                        string Body = BLobj.PopulateBody(Lead_Id.ToString(),
                       " <b style='font-size:larger'>Dear  " + StudentName.ToString() + ", " + "Thanks for reaching out! the request has been closed</b>", " Details below : ", "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString() + "<br><br></li><li><b>Name :</b> " + StudentName.ToString() + "<br><br></li> " + "" +
                      "<li><b>Request Id</b> " + Ticket_Id.ToString() + "<br><br><li><b>Request Message :</b> " + Request_Message.ToString() + "<br><br></li><li><b>Response Message :</b> " + Response_Message.ToString() + "<br><br></li>");
                        SendHtmlFormattedEmailForRequest(MailId.ToString(), Manager_Mailid.ToString(), Subject.ToString(), Body.ToString(), "");
                    }
                    string gstrQstr = "select DeviceId,username from user_device_details   where Username='" + Lead_Id.ToString() + "' order by Id desc limit 1";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), " Your Request(" + Request_Message.ToString() + ") has been Resolved by Manager", "Student", "Empty");
                        SaveManagerLog(Lead_Id.ToString(), " " + ManagerId.ToString() + "", "Title: " + Request_Message.ToString() + ",Resolved by Manager", "Response for (Request)");
                    }
                }
                else
                {
                    status = "unable to close the ticket";
                }
            }
        }
        catch (Exception ex)
        {
            status = "Error" + ex.Message.ToString();
        }
        return status;
    }

    #region Phase 4 Payment Gateway


    [WebMethod]
    public string Check_Student_Exists(string MobileNo)
    {

        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_CHECK_STUDENT_EXISTS";
                cmd.Parameters.AddWithValue("p_MobileNo", MobileNo);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                return dt.Rows[0]["Checkstudent"].ToString();


            }
        }
        catch (Exception ex)
        {


            return "Error";

        }

    }


    //[WebMethod]
    public string Save_Manager_PaymentDetails_OLD(int Fees_Category_ID, string Fees_Category_Name, int Fees_ID, int Registration_ID,
        string Student_Name, string MobileNo, int State_Id, int District_Id, int City_Id, string stream, int College_ID, string Email_Id, string Paid_date, int Paid_fees, int Payment_Mode, string Payment_Remark, int Manager_Id)
    {

        string status = "";
        try
        {

            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_MANAGER_PAY_AMOUNT_DETAILS";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Fees_Category_ID", Fees_Category_ID);
                cmd.Parameters.AddWithValue("p_Fees_Id", Fees_ID);
                cmd.Parameters.AddWithValue("p_Registration_ID", Registration_ID);
                cmd.Parameters.AddWithValue("p_Student_Name", Student_Name);
                cmd.Parameters.AddWithValue("p_Student_MobileNo", MobileNo);
                cmd.Parameters.AddWithValue("p_State_Id", State_Id);
                cmd.Parameters.AddWithValue("p_District_Id", District_Id);
                cmd.Parameters.AddWithValue("p_Taluka_Id", City_Id);
                cmd.Parameters.AddWithValue("p_Stream", stream);
                cmd.Parameters.AddWithValue("p_College_Id", College_ID);
                cmd.Parameters.AddWithValue("p_Student_Email_Id", Email_Id);
                cmd.Parameters.AddWithValue("p_Paid_date", Paid_date);
                cmd.Parameters.AddWithValue("p_Paid_fees", Paid_fees);
                cmd.Parameters.AddWithValue("p_Payment_mode", Payment_Mode);
                cmd.Parameters.AddWithValue("p_Remark", Payment_Remark);
                cmd.Parameters.AddWithValue("p_User_Id", Manager_Id);
                cmd.Parameters.Add(new MySqlParameter("p_New_Registration_Id", MySqlDbType.Int64)).Direction = ParameterDirection.Output;
                int iDBStatus = cmd.ExecuteNonQuery();
                long Reg = long.Parse(cmd.Parameters["p_New_Registration_Id"].Value.ToString());
                if (iDBStatus > 0)
                {

                    status += "success";

                    string Message = "";
                    string SMS_Plaform = ConfigurationManager.AppSettings["SMS_Platform"].ToString();
                    string Deactivation_Days = ConfigurationManager.AppSettings["Deactivation_Days"].ToString();
                    string Helpline_No = ConfigurationManager.AppSettings["Helpline_No"].ToString();
                    if (Reg != 0)
                    {

                        MySqlCommand cmd1 = new MySqlCommand();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "USP_APP_GET_STUDENT_DETAILS_FOR_NOTIFICATION";
                        cmd1.Parameters.AddWithValue("p_Registration_Id", Reg);
                        cmd1.Connection = con;
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            if (SMS_Plaform == "CampusConnect")
                            {

                                Message = "Your LEAD ID is " + dt.Rows[0]["lead_id"].ToString() + "\n";
                                Message += "Login and update the profile in mis.leadcampus.org within " + Deactivation_Days.ToString() + " days to avoid de-activation." + "\n";
                                Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                                SendCampusConnectSMS(Message.ToString(), MobileNo.ToString());
                                Message = "";
                                Message = "Dear " + dt.Rows[0]["lead_id"].ToString() + "\n";
                                Message += "We have received Rs " + Paid_fees + " fees towards LEAD " + Fees_Category_Name + " and the receipt # is " + Paid_date + "\n";
                                Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                                SendCampusConnectSMS(Message.ToString(), MobileNo.ToString());
                            }
                            else
                            {
                                Message = "Your LEAD ID is " + dt.Rows[0]["lead_id"].ToString() + "\n";
                                Message += "Login and update the profile in mis.leadcampus.org within " + Deactivation_Days.ToString() + " days to avoid de-activation." + "\n";
                                Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                                SendExotelSMS(Message.ToString(), MobileNo.ToString());

                                Message = "";
                                Message = "Dear " + dt.Rows[0]["lead_id"].ToString() + "\n";
                                Message += "We have received Rs " + Paid_fees + " fees towards LEAD " + Fees_Category_Name + " and the receipt # is " + Paid_date + "\n";
                                Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                                SendExotelSMS(Message.ToString(), MobileNo.ToString());

                            }
                        }
                    }
                    else
                    {
                        MySqlCommand cmd1 = new MySqlCommand();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "USP_APP_GET_DEVICE_DETAILS_FOR_NOTIFICATION";
                        cmd1.Parameters.AddWithValue("p_Registration_Id", Reg);
                        cmd1.Parameters.AddWithValue("P_Manager_Id", 0);
                        cmd1.Connection = con;
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (SMS_Plaform == "CampusConnect")
                            {

                                Message = "Dear " + dt.Rows[0]["lead_id"].ToString() + "\n";
                                Message += "We have received Rs " + Paid_fees + " fees towards LEAD " + Fees_Category_Name + " and the receipt # is " + Paid_date + "\n";
                                Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                                SendCampusConnectSMS(Message.ToString(), MobileNo.ToString());
                            }
                            else
                            {
                                Message = "Dear " + dt.Rows[0]["lead_id"].ToString() + "\n";
                                Message += "We have received Rs " + Paid_fees + " fees towards LEAD " + Fees_Category_Name + " and the receipt # is " + Paid_date + "\n";
                                Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                                SendExotelSMS(Message.ToString(), MobileNo.ToString());
                            }
                            Message += "We have received Rs " + Paid_fees + " fees towards LEAD " + Fees_Category_Name + " and the receipt # is " + Paid_date + "\n";
                            Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                            if (dt.Rows[0]["DeviceId"].ToString() != "")
                            {
                                FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), Message.ToString(), "Student", "Empty");
                            }
                            SaveManagerLog(dt.Rows[0]["lead_id"].ToString(), " " + Manager_Id.ToString() + "", "Title: " + Message.ToString() + ",Received by Manager", "Fees Received");
                        }

                    }
                }
                else
                {
                    status += "Failed to process";
                }
            }
        }
        catch (Exception ex)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public string Save_Manager_PaymentDetails(int Fees_Category_ID, string Fees_Category_Name, int Fees_ID, int isNewRegistration, string Payment_Remark, int Manager_Id, string PaymentDetails)
    {

        string status = "";
        try
        {
            string isExists = "";
            // if fees collecting for existing lead id.
            if (isNewRegistration == 0)
            {

                status = Save_Fees_Details(Fees_Category_ID, Fees_Category_Name, Fees_ID, Manager_Id, Payment_Remark, PaymentDetails);

            }
            else
            {
                // if fees collecting for new generate lead id.
                isExists = Validate(PaymentDetails);
                if (isExists == "NEW")
                {
                    status = Save_Fees_Details(Fees_Category_ID, Fees_Category_Name, Fees_ID, Manager_Id, Payment_Remark, PaymentDetails);
                }
                else
                {
                    status = isExists.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            status = "Error" + ex.Message.ToString();
        }
        return status;
    }
    public string Validate(string PaymentDetails)
    {
        try
        {
            JArray array = JArray.Parse(PaymentDetails.Replace("\\", ""));
            string MobileNo = "";
            foreach (JObject obj in array.Children<JObject>())
            {
                foreach (JProperty singleProp in obj.Properties())
                {
                    if (singleProp.Name.ToString() == "MobileNo")
                        MobileNo = singleProp.Value.ToString();
                }
            }
            return Check_Student_Exists(MobileNo.ToString());
        }
        catch (Exception ex)
        {
            return "error" + ex.Message.ToString();

        }
    }
    public string Save_Fees_Details(int Fees_Category_ID, string Fees_Category_Name, int Fees_ID, int Manager_Id, string Payment_Remark, string PaymentDetails)
    {
        string status = "";
        DataTable dt = new DataTable();
        try
        {
            int Registration_ID = 0; string Student_Name = ""; string MobileNo = ""; int State_Id = 0; int District_Id = 0; int City_Id = 0; int stream_Id = 0; int College_ID = 0; string Email_Id = ""; string Paid_date = ""; int Paid_fees = 0; int Payment_Mode = 0; int iDBStatus = 0; int count = 0;
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                if ((PaymentDetails != null) || (PaymentDetails != ""))
                {

                    JArray array = JArray.Parse(PaymentDetails.Replace("\\", ""));

                    foreach (JObject obj in array.Children<JObject>())
                    {
                        count = count + 1;
                        foreach (JProperty singleProp in obj.Properties())
                        {
                            if (singleProp.Name.ToString() == "Registration_Id")
                                Registration_ID = int.Parse(singleProp.Value.ToString());
                            else if (singleProp.Name.ToString() == "Student_Name")
                                Student_Name = singleProp.Value.ToString();
                            else if (singleProp.Name.ToString() == "MobileNo")
                                MobileNo = singleProp.Value.ToString();
                            else if (singleProp.Name.ToString() == "State_Id")
                                State_Id = int.Parse(singleProp.Value.ToString());
                            else if (singleProp.Name.ToString() == "District_Id")
                                District_Id = int.Parse(singleProp.Value.ToString());
                            else if (singleProp.Name.ToString() == "City_Id")
                                City_Id = int.Parse(singleProp.Value.ToString());
                            else if (singleProp.Name.ToString() == "stream_Id")
                                stream_Id = int.Parse(singleProp.Value.ToString());
                            else if (singleProp.Name.ToString() == "College_ID")
                                College_ID = int.Parse(singleProp.Value.ToString());
                            else if (singleProp.Name.ToString() == "Email_Id")
                                Email_Id = singleProp.Value.ToString();
                            else if (singleProp.Name.ToString() == "Paid_fees")
                                Paid_fees = int.Parse(singleProp.Value.ToString());
                            else if (singleProp.Name.ToString() == "Payment_Mode")
                                Payment_Mode = int.Parse(singleProp.Value.ToString());
                        }

                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "USP_APP_MANAGER_PAY_AMOUNT_DETAILS";
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("p_Fees_Category_ID", Fees_Category_ID);
                        cmd.Parameters.AddWithValue("p_Fees_Id", Fees_ID);
                        cmd.Parameters.AddWithValue("p_Registration_ID", Registration_ID);
                        cmd.Parameters.AddWithValue("p_Student_Name", Student_Name);
                        cmd.Parameters.AddWithValue("p_Student_MobileNo", MobileNo);
                        cmd.Parameters.AddWithValue("p_State_Id", State_Id);
                        cmd.Parameters.AddWithValue("p_District_Id", District_Id);
                        cmd.Parameters.AddWithValue("p_Taluka_Id", City_Id);
                        cmd.Parameters.AddWithValue("p_Stream", stream_Id);
                        cmd.Parameters.AddWithValue("p_College_Id", College_ID);
                        cmd.Parameters.AddWithValue("p_Student_Email_Id", Email_Id);
                        cmd.Parameters.AddWithValue("p_Paid_fees", Paid_fees);
                        cmd.Parameters.AddWithValue("p_Payment_mode", Payment_Mode);
                        cmd.Parameters.AddWithValue("p_Remark", Payment_Remark);
                        cmd.Parameters.AddWithValue("p_User_Id", Manager_Id);
                        cmd.Parameters.Add(new MySqlParameter("p_New_Registration_Id", MySqlDbType.Int64)).Direction = ParameterDirection.Output;
                        iDBStatus = cmd.ExecuteNonQuery();
                        int Reg = int.Parse(cmd.Parameters["p_New_Registration_Id"].Value.ToString());
                        if (iDBStatus > 0)
                        {
                            //status = Sending_SMS_PDF(con, Reg, MobileNo, Paid_fees, Fees_Category_Name, Paid_date, Manager_Id);

                            if (Reg != 0)
                            {
                                status = Generate_Receipt(Reg, con);
                            }
                            else
                            {
                                status = Generate_Receipt(Registration_ID, con);
                            }
                        }
                        else
                        {
                            status += "Failed to process";
                        }
                    }
                    if (count <= 0)
                    {
                        status += "Array Format is not Correct";
                    }

                }
            }



        }
        catch (Exception ex)
        {
            status = "Fail to Insert Fees Error" + ex.Message.ToString();
        }
        return status;
    }

    public string Generate_Receipt(int Registration_Id, MySqlConnection con)
    {
        string status = "";

        try
        {

            DataTable dt = new DataTable();

            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "USP_GET_SAVED_PAYMENTDETAILS_FOR_RECEIPT";
            cmd1.Parameters.AddWithValue("p_Registration_Id", Registration_Id);
            cmd1.Connection = con;
            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
            da1.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                int Auto_Receipt_No = 0;
                string Lead_Id = "";
                string College_Name = "";
                string StudentName = "";// CultureInfo.CurrentCulture.TextInfo.ToTitleCase("sharad noolvi".ToString().ToLower());
                string Fees_Category_description = "";// "Received towards LEAD Registration";
                string Student_MailId = "";// "sharad.noolvi@dfmail.org";
                string Payment_Mode = "";
                string Mobile_No = "";
                int Payment_Id = 0;
                string Paid_Fees = "";
                string Paid_Date = "";


                Lead_Id = dt.Rows[0]["Lead_Id"].ToString();
                StudentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt.Rows[0]["StudentName"].ToString().ToLower());
                College_Name = dt.Rows[0]["College_Name"].ToString();
                Mobile_No = dt.Rows[0]["MobileNo"].ToString();
                Student_MailId = dt.Rows[0]["MailId"].ToString();
                Paid_Fees = dt.Rows[0]["paid_fees"].ToString();
                Paid_Date = dt.Rows[0]["Created_Date"].ToString();
                Auto_Receipt_No = int.Parse(dt.Rows[0]["Auto_Receipt_No"].ToString());
                Fees_Category_description = dt.Rows[0]["Fees_Category_description"].ToString();
                Payment_Mode = dt.Rows[0]["PaymentMode"].ToString();
                Payment_Id = int.Parse(dt.Rows[0]["payment_id"].ToString());
                string SlapDest = "";
                string Source = "";
                //Source = "~/Reports/" + Fees_Category_Name.ToString() + ".pdf";
                Source = "~/Reports/Fees_Receipt.pdf";
                SlapDest = Lead_Id.ToString() + "_" + Guid.NewGuid().ToString() + ".pdf";
                var reader = new PdfReader(Server.MapPath(Source));

                FileStream fs = null;
                fs = new System.IO.FileStream(Server.MapPath("~/Receipts/" + SlapDest), FileMode.Create, FileAccess.Write);
                var document = new Document(reader.GetPageSizeWithRotation(1));
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                document.Open();
                document.NewPage();
                var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);


                var importedPage = writer.GetImportedPage(reader, 1);
                var contentByte = writer.DirectContent;
                contentByte.BeginText();


                int length = College_Name.ToString().ToString().Length;
                contentByte.SetFontAndSize(baseFont, 12);
                if (length >= 50)
                {
                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, College_Name.ToString(), 85, 37, 0);
                }
                else
                {
                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, College_Name.ToString(), 280, 70, 0);
                }

                contentByte.SetFontAndSize(baseFont, 14);
                contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 130, 120, 0);

                contentByte.SetFontAndSize(baseFont, 12);
                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Auto_Receipt_No.ToString(), 480, 234, 0);

                contentByte.SetFontAndSize(baseFont, 12);
                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Lead_Id.ToString(), 80, 233, 0);


                contentByte.SetFontAndSize(baseFont, 12);
                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Fees_Category_description.ToString(), 35, 190, 0);

                contentByte.SetFontAndSize(baseFont, 14);
                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Paid_Fees.ToString() + " " + "/" + " " + Payment_Mode.ToString(), 50, 160, 0);
                contentByte.SetFontAndSize(baseFont, 12);
                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Paid_Date, 480, 170, 0);

                PdfGState graphicsState = new PdfGState();
                graphicsState.BlendMode = PdfGState.BM_DARKEN;
                contentByte.SetGState(graphicsState);
                contentByte.EndText();
                contentByte.AddTemplate(importedPage, 0, 0);
                document.Close();
                writer.Close();
                // Update Receipt Path to DB
                status = Update_Receipt_Path(Payment_Id, "~/Receipts/" + SlapDest, con);
                if (status == "success")
                {
                    // If Receipt path updated to DB then send SMS and Mail

                    // Sending_SMS_PDF(con, Registration_ID, MobileNo, Paid_fees, Fees_Category_Name, Paid_date, Manager_Id);
                    status = Send_Fees_Confirmation(Registration_Id, Mobile_No.ToString(), Lead_Id.ToString(), Paid_Fees, Fees_Category_description, Paid_Fees, Auto_Receipt_No.ToString(), con);
                    status = SendMail("~/Receipts/" + SlapDest, Student_MailId.ToString(), StudentName.ToString(), Fees_Category_description.ToString());
                }
            }
            else
            {
                status = "No Record Found";
            }
        }
        catch (Exception ex)
        {
            status = "Receipt Generation Error : " + ex.Message.ToString();
        }
        return status;
    }

    public string Update_Receipt_Path(int Payment_Id, string Receipt_Path, MySqlConnection con)
    {
        string status = "";
        try
        {

            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "USP_UPDATE_FEES_RECEIPT_PATH";
            cmd2.Connection = con;

            cmd2.Parameters.AddWithValue("p_Payment_Id", Payment_Id);
            cmd2.Parameters.AddWithValue("p_Receipt_Path", Receipt_Path.ToString());

            int iDBStatus = cmd2.ExecuteNonQuery();
            if (iDBStatus > 0)
            {
                status += "success";
            }
            else
            {
                status += "failed";
            }

        }
        catch (Exception ex)
        {
            status += "error" + ex.Message.ToString();
        }
        return status;
    }
    public string Send_Fees_Confirmation(int Registration_Id, string Mobile_No, string Lead_Id, string Paid_Fees, string Fees_Category, string Paid_Date, string Receipt_No, MySqlConnection con)
    {
        string status = "";
        try
        {
            string Message = "";
            string SMS_Plaform = ConfigurationManager.AppSettings["SMS_Platform"].ToString();
            string Helpline_No = ConfigurationManager.AppSettings["Helpline_No"].ToString();
            string Deactivation_Days = ConfigurationManager.AppSettings["Deactivation_Days"].ToString();
            if (Registration_Id != 0)
            {
                if (SMS_Plaform == "CampusConnect")
                {
                    Message = "Dear " + Lead_Id + "\n";
                    Message += "We have received Rs " + Paid_Fees + " fees towards LEAD " + Fees_Category + "\n";
                    Message += " on " + Paid_Date.ToString() + "  and the receipt # is " + Receipt_No + "\n";
                    Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                    SendCampusConnectSMS(Message.ToString(), Mobile_No.ToString());
                }
                else
                {
                    Message = "Dear " + Lead_Id + "\n";
                    Message += "We have received Rs " + Paid_Fees + " fees towards LEAD " + Fees_Category + "\n";
                    Message += " on " + Paid_Date.ToString() + "  and the receipt # is " + Receipt_No + "\n";
                    Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                    SendExotelSMS(Message.ToString(), Mobile_No.ToString());
                }
            }
            else
            {
                if (SMS_Plaform == "CampusConnect")
                {
                    Message = "Your LEAD ID is " + Lead_Id.ToString() + "\n";
                    Message += "Login and update the profile in mis.leadcampus.org within " + Deactivation_Days.ToString() + " days to avoid de-activation." + "\n";
                    Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                    SendCampusConnectSMS(Message.ToString(), Mobile_No.ToString());


                    Message = "Dear " + Lead_Id + "\n";
                    Message += "We have received Rs " + Paid_Fees + " fees towards LEAD " + Fees_Category + "\n";
                    Message += " on " + Paid_Date.ToString() + "  and the receipt # is " + Receipt_No + "\n";
                    Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                    SendCampusConnectSMS(Message.ToString(), Mobile_No.ToString());
                }
                else
                {
                    Message = "Your LEAD ID is " + Lead_Id.ToString() + "\n";
                    Message += "Login and update the profile in mis.leadcampus.org within " + Deactivation_Days.ToString() + " days to avoid de-activation." + "\n";
                    Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                    SendExotelSMS(Message.ToString(), Mobile_No.ToString());


                    Message = "Dear " + Lead_Id + "\n";
                    Message += "We have received Rs " + Paid_Fees + " fees towards LEAD " + Fees_Category + "\n";
                    Message += " on " + Paid_Date.ToString() + "  and the receipt # is " + Receipt_No + "\n";
                    Message += "Helpline: " + Helpline_No.ToString() + " - Deshpande Foundation";
                    SendExotelSMS(Message.ToString(), Mobile_No.ToString());
                }
            }
            DataTable dt = new DataTable();
            MySqlCommand cmd3 = new MySqlCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "USP_APP_GET_DEVICE_DETAILS_FOR_NOTIFICATION";
            cmd3.Parameters.AddWithValue("p_Registration_Id", Registration_Id);
            cmd3.Parameters.AddWithValue("P_Manager_Id", 0);
            cmd3.Connection = con;
            MySqlDataAdapter da3 = new MySqlDataAdapter(cmd3);
            da3.Fill(dt);
            if (dt.Rows[0]["DeviceId"].ToString() != "")
            {
                FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), Message.ToString(), "Student", "Empty");

            }
            status = "success";
            //SaveManagerLog(dt.Rows[0]["lead_id"].ToString(), " " + 0 + "", "Title: " + Message.ToString() + ",Received by Manager", "Fees Received");
        }
        catch (Exception ex)
        {
            status = "SMS Error" + ex.Message.ToString();

        }
        return status;
    }
    public string SendMail(string FilePath, string Student_MailId, string Student_Name, string Fees_Category_description)
    {
        string status = "";
        try
        {
            using (MailMessage mm = new MailMessage("leadmis@dfmail.org", Student_MailId.ToString()))
            {
                string body = PopulateBody(Student_Name.ToString(), "", "", "");
                mm.Subject = "Fees Received towards" + " " + Fees_Category_description.ToString();
                mm.Body = body.ToString();
                if (File.Exists(Server.MapPath(FilePath)))
                {
                    Attachment Certificate = new Attachment(Server.MapPath(FilePath));
                    mm.Attachments.Add(Certificate);
                }
                mm.Body = body.ToString();

                mm.IsBodyHtml = true;
                mm.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
                mm.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
                //  mm.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
                // mm.Bcc.Add(new MailAddress("abhinandan.k@dfmail.org"));
                // mm.Bcc.Add(new MailAddress("anisha.c@dfmail.org"));
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential
                    {
                        UserName = "leadmis@dfmail.org",
                        Password = "leadcampusadmin"
                    };
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(mm);

                    string Device_Id = "";
                    //      Device_Id = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                    //   if (Device_Id != "")
                    // {
                    //  string ServerResponse = FCMPushNotification.AndroidPush(Device_Id.ToString(), txtNotificationText.Text, "Notification", "Empty");
                    // BLobj.Manager_SaveNotificationLog(cook.Admin_Id(), lblLead_Id.Text.ToString(), txtNotificationText.Text.ToString(), "Notification", ServerResponse.ToString());
                }
                status = "success";
            }
        }
        catch (Exception ex)
        {
            status = "Mail Error : " + ex.Message.ToString();


        }
        return status;
    }

    [WebMethod]

    public string Manager_Submit_Fees(int Fees_Category_Id, string Fees_Category_Name, int Submission_Amount, int Payment_Mode, string Remark, int Submitted_By)
    {

        string status = "";
        try
        {

            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_MANAGER_FEES_SUBMISSION";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Fees_Category_ID", Fees_Category_Id);
                cmd.Parameters.AddWithValue("p_Submission_Amount", Submission_Amount);
                cmd.Parameters.AddWithValue("p_Payment_Mode", Payment_Mode);
                cmd.Parameters.AddWithValue("p_Remark", Remark);
                cmd.Parameters.AddWithValue("p_Submitted_By", Submitted_By);
                int iDBStatus = cmd.ExecuteNonQuery();

                if (iDBStatus > 0)
                {

                    status += "success";

                    string Message = "";
                    string SMS_Plaform = ConfigurationManager.AppSettings["SMS_Platform"].ToString();
                    string Deactivation_Days = ConfigurationManager.AppSettings["Deactivation_Days"].ToString();
                    string Helpline_No = ConfigurationManager.AppSettings["Helpline_No"].ToString();

                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "USP_APP_GET_DEVICE_DETAILS_FOR_NOTIFICATION";
                    cmd1.Parameters.AddWithValue("p_Registration_Id", 0);
                    cmd1.Parameters.AddWithValue("P_Manager_Id", Submitted_By);
                    cmd1.Connection = con;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        Message += "Thank you for submitting Rs " + Submission_Amount + " fees towards LEAD " + Fees_Category_Name;
                        if (dt.Rows[0]["DeviceId"].ToString() != "")
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), Message.ToString(), "Manager", "Empty");
                        }
                        SaveManagerLog("", " " + Submitted_By.ToString() + "", "Title: " + Message.ToString() + ",Submitted by Manager", "Fees Submission");
                    }

                }
                else
                {
                    status += "Failed to process";
                }
            }
        }
        catch (Exception ex)
        {
            status = "Error" + ex.Message.ToString();
        }
        return status;
    }




    [WebMethod]
    public List<vmGet_Academic_Detail> Get_Academic_Detail()
    {

        List<vmGet_Academic_Detail> Academic = new List<vmGet_Academic_Detail>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_ACADEMIC_DETAIL";

                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Academic.Add(new vmGet_Academic_Detail
                        {
                            Academic_Id = int.Parse(dt.Rows[i]["Academic_Id"].ToString()),
                            Academic_Code = dt.Rows[i]["AcademicCode"].ToString(),
                            Year_Code = dt.Rows[i]["YearCode"].ToString(),
                            From_Date = dt.Rows[i]["From_Date"].ToString(),
                            To_Date = dt.Rows[i]["To_Date"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    Academic.Add(new vmGet_Academic_Detail
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {
            Academic.Add(new vmGet_Academic_Detail
            {
                Status = "Error"
            });
        }
        return Academic;
    }

    [WebMethod]
    public string Get_Academic_Detail_Json()
    {

        List<vmGet_Academic_Detail> Academic = new List<vmGet_Academic_Detail>();
        string jsonData = "";
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_ACADEMIC_DETAIL_JSON";

                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Academic.Add(new vmGet_Academic_Detail
                        {
                            Academic_Id = int.Parse(dt.Rows[i]["Academic_Id"].ToString()),
                            Academic_Code = dt.Rows[i]["AcademicCode"].ToString(),
                            Year_Code = dt.Rows[i]["YearCode"].ToString(),
                            From_Date = dt.Rows[i]["From_Date"].ToString(),
                            To_Date = dt.Rows[i]["To_Date"].ToString(),
                            Status = "Success"
                        });
                    }

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    jsonData = js.Serialize(Academic);
                }
                else
                {

                    Academic.Add(new vmGet_Academic_Detail
                    {
                        Status = "No Record"
                    });
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    jsonData = js.Serialize(Academic);
                }
            }
        }
        catch (Exception)
        {
            Academic.Add(new vmGet_Academic_Detail
            {
                Status = "Error"
            });
            JavaScriptSerializer js = new JavaScriptSerializer();
            jsonData = js.Serialize(Academic);
        }
        return jsonData;
    }


    [WebMethod]
    public List<vmGet_Manager_Fees_Summary> Get_Manager_Summary(int Manager_Id, string From_Date, string To_Date)
    {

        List<vmGet_Manager_Fees_Summary> Summary = new List<vmGet_Manager_Fees_Summary>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_MANAGER_SUMMARY";
                cmd.Parameters.AddWithValue("p_Manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_From_Date", From_Date);
                cmd.Parameters.AddWithValue("p_To_Date", To_Date);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Summary.Add(new vmGet_Manager_Fees_Summary
                        {
                            Fees_Category_Id = int.Parse(dt.Rows[i]["Fees_Category_Slno"].ToString()),
                            Fees_Category_Name = dt.Rows[i]["Fees_Category_description"].ToString(),
                            Collected = int.Parse(dt.Rows[i]["Collected"].ToString()),
                            Submitted = int.Parse(dt.Rows[i]["Submitted"].ToString()),
                            Balance = int.Parse(dt.Rows[i]["Balance"].ToString()),
                            Total = int.Parse(dt.Rows[i]["Total"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    Summary.Add(new vmGet_Manager_Fees_Summary
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {
            Summary.Add(new vmGet_Manager_Fees_Summary
            {
                Status = "Error"
            });
        }
        return Summary;
    }


    [WebMethod]
    public List<vmGet_Manager_Fees_Submission> Get_Manager_Submission_Details(int Manager_Id, int p_Fees_Category_Id, string From_Date, string To_Date)
    {

        List<vmGet_Manager_Fees_Submission> submission_details = new List<vmGet_Manager_Fees_Submission>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_MANAGER_SUBMISSION";
                cmd.Parameters.AddWithValue("p_Manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_Fees_Category_Id", p_Fees_Category_Id);
                cmd.Parameters.AddWithValue("p_From_Date", From_Date);
                cmd.Parameters.AddWithValue("p_To_Date", To_Date);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        submission_details.Add(new vmGet_Manager_Fees_Submission
                        {
                            Submission_slno = int.Parse(dt.Rows[i]["Submission_slno"].ToString()),
                            Fees_Category_Id = int.Parse(dt.Rows[i]["Fees_Category_Id"].ToString()),
                            Fees_Category_description = dt.Rows[i]["Fees_Category_description"].ToString(),
                            Submission_Amount = int.Parse(dt.Rows[i]["Submission_Amount"].ToString()),
                            Submitted_Date = dt.Rows[i]["Submitted_Date"].ToString(),
                            Submitted_Mode = dt.Rows[i]["Submitted_Mode"].ToString(),
                            Submitted_Remark = dt.Rows[i]["Submitted_Remark"].ToString(),
                            Submitted_By = int.Parse(dt.Rows[i]["Submitted_By"].ToString()),
                            Submitter_Name = dt.Rows[i]["Submitter_Name"].ToString(),
                            Rec_Status = dt.Rows[i]["Rec_Status"].ToString(),
                            Rec_Date = dt.Rows[i]["Rec_Date"].ToString(),
                            Rec_By = dt.Rows[i]["Rec_By"].ToString(),
                            Rec_Mail_id = dt.Rows[i]["Rec_Mail_id"].ToString(),
                            Rec_Remark = dt.Rows[i]["Rec_Remark"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    submission_details.Add(new vmGet_Manager_Fees_Submission
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            submission_details.Add(new vmGet_Manager_Fees_Submission
            {
                Status = "Error"
            });
        }
        return submission_details;
    }



    [WebMethod]
    public List<vmPayment_Details> Get_Payment_Details(int Registration_ID, int Manager_Id, int Academic_Id)
    {

        List<vmPayment_Details> paymentdetails = new List<vmPayment_Details>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_PAYMENT_DETAILS";
                cmd.Parameters.AddWithValue("P_Registration_Id", Registration_ID);
                cmd.Parameters.AddWithValue("P_Manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_academic_year", Academic_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        paymentdetails.Add(new vmPayment_Details
                        {
                            Payment_Id = int.Parse(dt.Rows[i]["payment_id"].ToString()),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Registration_Id = int.Parse(dt.Rows[i]["Registration_Id"].ToString()),
                            Paid_Fees = int.Parse(dt.Rows[i]["paid_fees"].ToString()),
                            paid_date = dt.Rows[i]["paid_date"].ToString(),
                            Created_Date = dt.Rows[i]["Created_Date"].ToString(),
                            Auto_Receipt_No = int.Parse(dt.Rows[i]["Auto_Receipt_No"].ToString()),
                            transanction_Id = int.Parse(dt.Rows[i]["transanction_Id"].ToString()),
                            reference_id = int.Parse(dt.Rows[i]["reference_id"].ToString()),
                            Fees_Category_description = dt.Rows[i]["Fees_Category_description"].ToString(),
                            transactionStatus = dt.Rows[i]["transactionStatus"].ToString(),
                            YearCode = dt.Rows[i]["YearCode"].ToString(),
                            Payment_Type = dt.Rows[i]["payment_type"].ToString(),
                            Payeer_Id = dt.Rows[i]["Payeer_Id"].ToString(),
                            Payment_Receipt_Path = dt.Rows[i]["Payment_Receipt_Path"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    paymentdetails.Add(new vmPayment_Details
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {
            paymentdetails.Add(new vmPayment_Details
            {
                Status = "Error"
            });
        }
        return paymentdetails;
    }


    [WebMethod]
    public List<vmPayment_Details> Get_Manager_Payment_History(int Manager_Id, string From_Date, string To_Date, int College_Id, int Fees_Category_Id)
    {
        List<vmPayment_Details> paymentdetails = new List<vmPayment_Details>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_MANAGER_GET_PAYMENT_DETAILS";
                cmd.Parameters.AddWithValue("p_Manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_From_Date", From_Date);
                cmd.Parameters.AddWithValue("p_To_Date", To_Date);
                cmd.Parameters.AddWithValue("p_College_Id", College_Id);
                cmd.Parameters.AddWithValue("p_Fees_Category_Id", Fees_Category_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        paymentdetails.Add(new vmPayment_Details
                        {
                            Payment_Id = int.Parse(dt.Rows[i]["payment_id"].ToString()),
                            Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Registration_Id = int.Parse(dt.Rows[i]["Registration_Id"].ToString()),
                            Paid_Fees = int.Parse(dt.Rows[i]["paid_fees"].ToString()),
                            paid_date = dt.Rows[i]["paid_date"].ToString(),
                            Created_Date = dt.Rows[i]["Created_Date"].ToString(),
                            Auto_Receipt_No = int.Parse(dt.Rows[i]["Auto_Receipt_No"].ToString()),
                            transanction_Id = int.Parse(dt.Rows[i]["transanction_Id"].ToString()),
                            reference_id = int.Parse(dt.Rows[i]["reference_id"].ToString()),
                            Fees_Category_description = dt.Rows[i]["Fees_Category_description"].ToString(),
                            transactionStatus = dt.Rows[i]["transactionStatus"].ToString(),
                            YearCode = dt.Rows[i]["YearCode"].ToString(),
                            Payment_Type = dt.Rows[i]["payment_type"].ToString(),
                            Payeer_Id = dt.Rows[i]["Payeer_Id"].ToString(),
                            Payment_Mode = dt.Rows[i]["PaymentMode"].ToString(),
                            Payment_Remark = dt.Rows[i]["Remark"].ToString(),
                            Created_User_Type = dt.Rows[i]["Created_User_Type"].ToString(),
                            Manager_Submission_Status = dt.Rows[i]["Manager_Submission_Status"].ToString(),
                            Payment_Receipt_Path = dt.Rows[i]["Payment_Receipt_Path"].ToString(),
                            Rec_Date = dt.Rows[i]["Rec_date"].ToString(),
                            Rec_By = dt.Rows[i]["Rec_By"].ToString(),
                            Rec_Remark = dt.Rows[i]["Rec_Remark"].ToString(),
                            Rec_Status = dt.Rows[i]["Rec_Status"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    paymentdetails.Add(new vmPayment_Details
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            paymentdetails.Add(new vmPayment_Details
            {
                Status = "Error" + ex.Message.ToString()
            });
        }
        return paymentdetails;
    }



    [WebMethod]
    public List<vmGET_FeeCategory_Master> Get_Fees_Category_Master(int Registration_Id, int College_ID)
    {

        List<vmGET_FeeCategory_Master> Fees_Category = new List<vmGET_FeeCategory_Master>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_FEES_CATEGORY";
                cmd.Parameters.AddWithValue("p_Registration_ID", Registration_Id);
                cmd.Parameters.AddWithValue("p_College_Id", College_ID);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Fees_Category.Add(new vmGET_FeeCategory_Master
                        {
                            Fees_Category_Slno = int.Parse(dt.Rows[i]["Fees_Category_Slno"].ToString()),
                            fees_category_code = dt.Rows[i]["fees_category_code"].ToString(),
                            Fees_category_description = dt.Rows[i]["Fees_category_description"].ToString(),
                            Fees = int.Parse(dt.Rows[i]["Fees"].ToString()),
                            Fees_ID = int.Parse(dt.Rows[i]["Fees_Id"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    Fees_Category.Add(new vmGET_FeeCategory_Master
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {
            Fees_Category.Add(new vmGET_FeeCategory_Master
            {
                Status = "Error"
            });
        }
        return Fees_Category;
    }

    [WebMethod]
    public List<vmGet_Payment_Mode> Get_Payment_Mode()
    {

        List<vmGet_Payment_Mode> Payment_Mode = new List<vmGet_Payment_Mode>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_PAYMENT_MODE";
                //cmd.Parameters.AddWithValue("p_Academic_Year", Academic_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Payment_Mode.Add(new vmGet_Payment_Mode
                        {
                            payment_mode_slno = int.Parse(dt.Rows[i]["payment_mode_slno"].ToString()),
                            short_code = dt.Rows[i]["short_code"].ToString(),
                            description = dt.Rows[i]["description"].ToString(),

                            Status = "Success"
                        });
                    }
                }
                else
                {
                    Payment_Mode.Add(new vmGet_Payment_Mode
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception)
        {
            Payment_Mode.Add(new vmGet_Payment_Mode
            {
                Status = "Error"
            });
        }
        return Payment_Mode;
    }

    [WebMethod]
    //modified old to new
    public List<vmstudentreg> GetStudentRegistration(long ManagerId, int College_Id, int Fees_Category_Id, int Academic_Id)
    {
        List<vmstudentreg> StudentReuest = new List<vmstudentreg>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_STUDENT_REG_FEE_REQ";
                cmd.Parameters.AddWithValue("P_ManagerCode", ManagerId);
                cmd.Parameters.AddWithValue("p_College_Id", College_Id);
                cmd.Parameters.AddWithValue("p_Fees_Category_Id", Fees_Category_Id);
                cmd.Parameters.AddWithValue("p_Academic_Id", Academic_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReuest.Add(new vmstudentreg
                        {
                            Registration_Id = int.Parse(dt.Rows[i]["RegistrationId"].ToString()),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            MaidId = dt.Rows[i]["MailId"].ToString(),
                            Lead_id = dt.Rows[i]["Lead_id"].ToString(),
                            StateCode = int.Parse(dt.Rows[i]["StateCode"].ToString()),
                            DistrictCode = int.Parse(dt.Rows[i]["DistrictCode"].ToString()),
                            TalukaCode = int.Parse(dt.Rows[i]["TalukaCode"].ToString()),
                            StreamId = int.Parse(dt.Rows[i]["StateCode"].ToString()),
                            CollegeCode = int.Parse(dt.Rows[i]["collegeid"].ToString()),
                            CollegeName = dt.Rows[i]["College_Name"].ToString(),
                            isFeePaid = int.Parse(dt.Rows[i]["isFeePaid"].ToString()),
                            RegistrationDate = dt.Rows[i]["RegistrationDate"].ToString(),
                            Fees = int.Parse(dt.Rows[i]["Fees"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    StudentReuest.Add(new vmstudentreg { Status = "There is no request here" });
                }
            }
        }
        catch (Exception ex)
        {
            StudentReuest.Add(new vmstudentreg { Status = "Error" });
        }
        return StudentReuest;

    }

    [WebMethod]

    //modified old to new
    public List<vmstudentreg> GetStudentFeepaid(long ManagerId)
    {
        List<vmstudentreg> StudentReuest = new List<vmstudentreg>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_STUDENT_REG_FEE_PAID";
                cmd.Parameters.AddWithValue("P_ManagerCode", ManagerId);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReuest.Add(new vmstudentreg
                        {
                            Lead_id = dt.Rows[i]["Lead_id"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            RegistrationDate = dt.Rows[i]["RegistrationDate"].ToString(),
                            CollegeName = dt.Rows[i]["College_Name"].ToString(),
                            MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                            isFeePaid = int.Parse(dt.Rows[i]["isFeePaid"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    StudentReuest.Add(new vmstudentreg { Status = "There is no request here" });
                }
            }
        }
        catch (Exception)
        {
            StudentReuest.Add(new vmstudentreg { Status = "Error" });
        }
        return StudentReuest;

    }



    #endregion
    public void DeleteExistingFile(string FileName)
    {
        if (File.Exists(Path.Combine(Server.MapPath("~/Certificate/ApprovalLetter/"), FileName)))
        {
            // If file found, delete it    
            File.Delete(Path.Combine(Server.MapPath("~/Certificate/ApprovalLetter/"), FileName));
        }
    }
    [WebMethod]
    public string SaveMentorMentee(long PDId, string Comments, string UserType, int UserId, string ProjectStatus)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INSERT_MENTOR_MENTEE";
                cmd.Parameters.AddWithValue("p_PDID", PDId);
                cmd.Parameters.AddWithValue("p_Comments", Regex.Replace(Comments, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_UserType", UserType);
                cmd.Parameters.AddWithValue("p_UserId", UserId);
                cmd.Parameters.AddWithValue("p_ProjectStatus", ProjectStatus);
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";
                    string gstrQstr = "select DeviceId,Title,ManagerId,project_description.Lead_Id from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), " You have new discussion topic", "Student", "Empty");
                        SaveManagerLog(dt.Rows[0].ItemArray[2].ToString(), " " + dt.Rows[0]["ManagerId"].ToString() + "", "Topic: " + Comments.ToString() + ",Discussion", "Discussion");
                    }
                }
                else
                {
                    status = "unable to update Discussion";
                }
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }
    public void SendHtmlFormattedEmailForRequest(string recepientEmail, string ManagerMailId, string subject, string body, string FilePath)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("leadmis@dfmail.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            if (ManagerMailId.ToString() != "")
            {
                mailMessage.CC.Add(new MailAddress(ManagerMailId.ToString()));
            }
            mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
            mailMessage.Bcc.Add(new MailAddress("leadmis@dfmail.org"));
            if (FilePath.ToString() != "")
            {
                Attachment attCertificate = new Attachment(FilePath);
                mailMessage.Attachments.Add(attCertificate);
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

    //[WebMethod]
    //public List<vmManagerOpenRequest> Get_Manager_Ticket_Details(string TicketId)
    //{
    //    List<vmManagerOpenRequest> vm = new List<vmManagerOpenRequest>();
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        using (MySqlConnection con = new MySqlConnection(connection))
    //        {
    //            con.Open();
    //            MySqlCommand cmd = new MySqlCommand();
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_APP_SELECT_MANAGER_TICKET_DETAILS";
    //            cmd.Parameters.AddWithValue("RequestId", TicketId);              
    //            cmd.Connection = con;
    //            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
    //            da.Fill(dt);
    //            if (dt.Rows.Count > 0)
    //            {
    //                for (int i = 0; i < dt.Rows.Count; i++)
    //                {
    //                    vm.Add(new vmManagerOpenRequest
    //                    {
    //                        Ticket_No = dt.Rows[i]["request_Id"].ToString(),
    //                        Request_Date = dt.Rows[i]["Request_Date"].ToString(),
    //                        Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
    //                        Student_Name = dt.Rows[i]["StudentName"].ToString(),
    //                        MobileNo = dt.Rows[i]["MobileNo"].ToString(),
    //                        RequestHead_Id = dt.Rows[i]["Request_Head_Id"].ToString(),
    //                        Request_type = dt.Rows[i]["Head_Name"].ToString(),
    //                        Project_Id = dt.Rows[i]["PDID"].ToString(),
    //                        Request_Message = dt.Rows[i]["Request_Message"].ToString(),
    //                        College_Name = dt.Rows[i]["College_Name"].ToString(),
    //                        Request_Priority = dt.Rows[i]["Request_Priority"].ToString(),
    //                        Status = "Success"
    //                    });
    //                }
    //            }
    //            else
    //            {
    //                vm.Add(new vmManagerOpenRequest
    //                {
    //                    Status = "No Record"
    //                });
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        vm.Add(new vmManagerOpenRequest
    //        {
    //            Status = "Error"
    //        });
    //    }
    //    return vm;
    //}




    [WebMethod]

    public string ManagerRejectProject(int PDId, string ManagerComments)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_UPDATE_PROJECT_REJECTION";
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Parameters.AddWithValue("p_ManagerComments", Regex.Replace(ManagerComments, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ProjectStatus", "Rejected");
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Success";

                    string Mailsend = "select P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ActionPlan,P.ManagerComments, P.SanctionAmount,P.ProjectStatus,S.StudentName,S.MailId,S.MobileNo,C.College_Name from project_description as P inner join student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Your project (" + dt2.Rows[0]["Title"].ToString() + ") has been Rejected. </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li><li><b>Manager Comment:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + "<br><br></li></ol><br><br>");

                        SendEmail(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " Project is Rejected");
                    }
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Your Project(" + dt.Rows[0]["Title"].ToString() + ") has been Rejected", "Student", "Empty");
                    }
                }
                else
                {
                    status = "Unable to update project details";
                }
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }


    #region Funding_Ticketing

    [WebMethod]
    public List<vmGet_Master_Ticket_Status> Get_Master_Ticket_Status()
    {

        List<vmGet_Master_Ticket_Status> Ticket_Status = new List<vmGet_Master_Ticket_Status>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_TICEKT_STATUS_MASTER";

                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Ticket_Status.Add(new vmGet_Master_Ticket_Status
                        {
                            Slno = int.Parse(dt.Rows[i]["Slno"].ToString()),
                            Ticket_Status = dt.Rows[i]["Ticket_Status"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    Ticket_Status.Add(new vmGet_Master_Ticket_Status
                    {
                        Status = "No Record"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Ticket_Status.Add(new vmGet_Master_Ticket_Status
            {
                Status = "Error" + ex.Message.ToString()
            });
        }
        return Ticket_Status;
    }

    [WebMethod]
    public vmFunding_Details Get_Release_Fund_List(int Manager_Id, string From_Date, string To_Date, string Ticket_Status)
    {
        vmFunding_Details vmFunding = new vmFunding_Details();
        List<vmGet_Student_List> Student_Details = new List<vmGet_Student_List>();


        try
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_FUNDING_STUDENT_DETAILS";
                cmd.Parameters.AddWithValue("p_Manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_Ticket_Status", Ticket_Status);
                cmd.Parameters.AddWithValue("p_FromDate", From_Date);
                cmd.Parameters.AddWithValue("p_ToDate", To_Date);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        vmGet_Student_List vmstudent = new vmGet_Student_List();
                        vmstudent.Registration_Id = int.Parse(dt.Rows[i]["RegistrationId"].ToString());
                        vmstudent.Lead_Id = dt.Rows[i]["Lead_Id"].ToString();
                        vmstudent.Student_Name = dt.Rows[i]["StudentName"].ToString();
                        vmstudent.Mobile_No = dt.Rows[i]["MobileNo"].ToString();
                        vmstudent.Email_Id = dt.Rows[i]["MailId"].ToString();
                        vmstudent.College_Name = dt.Rows[i]["College_Name"].ToString();
                        vmstudent.Sem_Name = dt.Rows[i]["SemName"].ToString();
                        vmstudent.Status = "Success";

                        Student_Details.Add(vmstudent);
                        List<vmGet_Released_Fund_Details> fund = new List<vmGet_Released_Fund_Details>();
                        using (MySqlConnection con1 = new MySqlConnection(connection))
                        {
                            DataTable dt1 = new DataTable();    
                            MySqlCommand cmd1 = new MySqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "USP_GET_MANAGER_FUNDING_DETAILS";
                            cmd1.Parameters.AddWithValue("p_Student_Id", int.Parse(dt.Rows[i]["RegistrationId"].ToString()));
                            cmd1.Parameters.AddWithValue("p_Funding_FromDate", From_Date);
                            cmd1.Parameters.AddWithValue("p_Funding_ToDate", To_Date);
                            cmd1.Connection = con1;
                            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                            da1.Fill(dt1);
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                vmGet_Released_Fund_Details vmfund = new vmGet_Released_Fund_Details();
                                vmfund.PDID = int.Parse(dt1.Rows[j]["pdid"].ToString());
                                vmfund.Project_Title = dt1.Rows[j]["Project_Title"].ToString();
                                vmfund.Requested_Amount = int.Parse(dt1.Rows[j]["Req_Amt"].ToString());
                                vmfund.Santioned_Amount = int.Parse(dt1.Rows[j]["San_Amt"].ToString());
                                vmfund.Released_Amount = int.Parse(dt1.Rows[j]["Release_Amount"].ToString());
                                vmfund.Total_Released_Amount = int.Parse(dt1.Rows[j]["Total_Released"].ToString());
                                vmfund.Balance_Amount = int.Parse(dt1.Rows[j]["Bal"].ToString());
                                vmfund.Status = "success";
                                fund.Add(vmfund);
                            }
                        }
                        vmstudent.Fund_Details = fund;
                        vmFunding.Student_Details = Student_Details;
                    }
                    //   vmFunding.Fund_Details = fund;
                }
                else
                {
                    vmFunding.Status = "No Record";

                }
            }

        }
        catch (Exception ex)
        {
            vmFunding.Status = "Error" + ex.Message.ToString();

        }
        return vmFunding;
    }


   [WebMethod]
    public string Create_Ticketing_System(int Manager_Id,string Manager_MailId, string Ticket_Priority, string Ticket_Details)
    {
        string status = "";
        DataTable dt = new DataTable();
        try
        {
          int count = 0; int pdid = 0; int Student_Id = 0;  int Ticket_Id = 0;
          
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SAVE_TICKET_ID";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_Ticket_Priority", Ticket_Priority);
                cmd.Parameters.Add(new MySqlParameter("p_New_Ticket_Id", MySqlDbType.Int64)).Direction = ParameterDirection.Output;
                int iDBStatus = cmd.ExecuteNonQuery();
                Ticket_Id = int.Parse(cmd.Parameters["p_New_Ticket_Id"].Value.ToString());
                if (iDBStatus > 0)
                {
                    if (Ticket_Id != 0)
                    {
                        if ((Ticket_Details != null) || (Ticket_Details != ""))
                        {
                            JArray array = JArray.Parse(Ticket_Details.Replace("\\", ""));
                            foreach (JObject obj in array.Children<JObject>())
                            {
                                count = count + 1;
                                foreach (JProperty singleProp in obj.Properties())
                                {
                                    if (singleProp.Name.ToString() == "PDID")
                                        pdid = int.Parse(singleProp.Value.ToString());
                                    else if (singleProp.Name.ToString() == "Student_Id")
                                        Student_Id = int.Parse(singleProp.Value.ToString());
                                }
                             
                                    MySqlCommand cmd1 = new MySqlCommand();
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.CommandText = "USP_APP_UPDATE_TICKET_ID_DETAILS";
                                    cmd1.Connection = con;
                                    cmd1.Parameters.AddWithValue("p_Ticket_Id", Ticket_Id);
                                    cmd1.Parameters.AddWithValue("p_PDID", pdid);
                                    int iDBStatus1 = cmd1.ExecuteNonQuery();
                                    if (iDBStatus1 > 0)
                                    {
                                     
                                          status = "success";
                                    }
                                    else
                                    {
                                        status = "Failed to Update Ticket Details";
                                    }
                            }                           
                            if (count <= 0)
                            {
                                status = "Array Format is not Correct";
                            }
                            if (status == "success")
                            {
                                string Subject = "Funding Ticket OPEN - " + Ticket_Priority + "  [# " + Ticket_Id + "  ] "; 
                                string body = PopulateBody(Manager_MailId.ToString(), "<b> Ticketing For Funding </b>", "",
                                "<br><br> <p>A request for support has been created and assigned # " +Ticket_Id +" - " +Ticket_Priority + ". A representative will follow-up with you as soon as possible. </p>");
                                Send_Ticket_Mail(Manager_MailId,Subject.ToString(),body);
                            }
                        }
                        else
                        {
                            status = "Array Format is null";
                        }
                    }
                    else
                    {
                        status = "Failed  to Create Ticket Id";
                    }
                }
                else
                {
                    status = "Failed  to Create Ticket";
                }
            }
        }
        catch (Exception ex)
        {
            status = "Fail to Insert Fees Error" + ex.Message.ToString();
        }
        return status;
    }
    public void Send_Ticket_Mail(string recepientEmail,string subject, string body)
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

    [WebMethod]
    public List<vmGet_Funding_Status> Get_Fund_Status(int Manager_Id, string From_Date, string To_Date)
    {
       
        List<vmGet_Funding_Status> Fund_Status = new List<vmGet_Funding_Status>();
        try
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_GET_TICKET_STATUS";
                cmd.Parameters.AddWithValue("p_Manager_Id", Manager_Id);
                cmd.Parameters.AddWithValue("p_FromDate", From_Date);
                cmd.Parameters.AddWithValue("p_ToDate", To_Date);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Fund_Status.Add(new vmGet_Funding_Status
                        {
                            Ticket_Id =int.Parse(dt.Rows[i]["Ticket_Id"].ToString()),
                            Requeted_By = dt.Rows[i]["Requeted_By"].ToString(),
                            Requested_Date = dt.Rows[i]["Requested_Date"].ToString(),
                            Approved_By = dt.Rows[i]["Approved_By"].ToString(),
                            Approved_Date = dt.Rows[i]["Approved_Date"].ToString(),
                            Approved_Remark = dt.Rows[i]["Approved_Remark"].ToString(),
                            Approval_Status =dt.Rows[i]["Approval_Status"].ToString(),
                            Requested_Project = int.Parse(dt.Rows[i]["Requested_Project"].ToString()),
                            Manager_Approved_Project = int.Parse(dt.Rows[i]["Manager_Approved_Project"].ToString()),
                            Account_Approved_Project = int.Parse(dt.Rows[i]["Account_Approved_Project"].ToString()),
                            Requested_Amount = int.Parse(dt.Rows[i]["Requested_Amount"].ToString()),
                            Manager_Approved_Amount = int.Parse(dt.Rows[i]["Manager_Approved_Amount"].ToString()),
                            Account_Approved_Amount = int.Parse(dt.Rows[i]["Account_Approved_Amount"].ToString()),
                            status = "success"
                        });
                    }
                }
                else
                {
                    Fund_Status.Add(new vmGet_Funding_Status { status = "There is no request here" });
                }
            }
        }
        catch (Exception ex)
        {
            Fund_Status.Add(new vmGet_Funding_Status 
            { 
                status = "Error" + ex.Message.ToString()
            });

        }
        return Fund_Status;
    }


    [WebMethod]
    public int Check_BankDetails_Updated(string Lead_Id)
    {
        try
        {
            int status = 0;
          
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_CHECK_BANK_DETAILS_UPDATED";
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    status = int.Parse(dt.Rows[0]["Checkstudent"].ToString());
                  
                }
                else
                {
                    status = 2; // Student Not Exists
                }

                return status;
            }
        }
        catch (Exception ex)
        {
            return 3; // if any errors
        }
    }

    #endregion region

}

