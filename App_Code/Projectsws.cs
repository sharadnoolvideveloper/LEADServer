using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Configuration;

/// <summary>
/// Summary description for Projectsws
/// </summary>
[WebService(Namespace = "http://mis.leadcampus.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Projectsws : System.Web.Services.WebService
{

    public Projectsws()
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
    public string Applyformasterleader(string Lead_Id, int isApply_MasterLeader)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APPLY_MASTER_LEADER";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_isApply_MasterLeader", isApply_MasterLeader);

                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "Unable to Apply master leader";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public string ApplyforLeadAmbassador(string Lead_Id, int isApply_LeadAmbassador)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APPLY_LEAD_AMBASSADOR";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_isApply_LeadAmbassador", isApply_LeadAmbassador);

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
    public vmleadmenber Getleadmemberdetails(string leadId)
    {
        vmleadmenber stud = new vmleadmenber();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_MASTER_LEADER";
                cmd.Parameters.AddWithValue("P_Lead_Id", leadId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {

                        stud.Lead_Id = dr["Lead_Id"].ToString();
                        stud.StudentName = dr["StudentName"].ToString();
                        stud.NoOfProject = long.Parse(dr["NoOfProject"].ToString());
                        stud.isApply_MasterLeader = int.Parse(dr["isApply_MasterLeader"].ToString());

                        if (stud.isApply_MasterLeader == 1)
                        {
                            stud.Status = "2";
                        }
                        else
                        {
                            stud.Status = "1";
                        }
                    }
                }
                else
                {
                    stud.Status = "0";
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
    public List<vmproject> GetprojectstatusList(string Lead_id)
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
                cmd.CommandText = "USP_APP_STATUS";
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ProjectStatus"].ToString() == "Completed")
                        {
                            Projectstatus.Add(new vmproject
                            {
                                PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                                StudentName = dt.Rows[i]["StudentName"].ToString(),
                                Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                                Title = dt.Rows[i]["Title"].ToString(),
                                //  Theme = dt.Rows[i]["Theme"].ToString(),
                                AskedAmount = long.Parse(dt.Rows[i]["AskedAmount"].ToString()),
                                SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                                giventotal = long.Parse(dt.Rows[i]["giventotal"].ToString()),
                                Rating = double.Parse(dt.Rows[i]["Rating"].ToString()),
                                ProjectStatus = dt.Rows[i]["ProjectStatus"].ToString(),
                                status = "Success",
                                HoursSpend = int.Parse(dt.Rows[i]["HoursSpend"].ToString())


                            });
                        }
                        else
                        {
                            Projectstatus.Add(new vmproject
                            {
                                PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                                StudentName = dt.Rows[i]["StudentName"].ToString(),
                                Lead_Id = dt.Rows[i]["Lead_Id"].ToString(),
                                Title = dt.Rows[i]["Title"].ToString(),
                                //  Theme = dt.Rows[i]["Theme"].ToString(),
                                AskedAmount = long.Parse(dt.Rows[i]["AskedAmount"].ToString()),
                                SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                                giventotal = long.Parse(dt.Rows[i]["giventotal"].ToString()),
                                Rating = double.Parse(dt.Rows[i]["Rating"].ToString()),
                                ProjectStatus = dt.Rows[i]["ProjectStatus"].ToString(),
                                status = "Success",
                                HoursSpend = int.Parse(dt.Rows[i]["HoursSpend"].ToString())
                            });
                        }

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

    public string AddProjectproposaljson(long Student_Id, string Lead_Id, string Title, int BeneficiaryNo,
   string ProjectType, string Objectives, string ActionPlan, string materials, float Budget, string AcademicID,
   string teammembers, string Beneficiary, string placeofimplimentation)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_INSERT_PROJECT_DESCRIPTION";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Student_Id", Student_Id);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
                cmd.Parameters.AddWithValue("p_Budget", Budget);
                cmd.Parameters.AddWithValue("p_Theme", ProjectType);
                cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AcademicCode", AcademicID);
                cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiary, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Placeofimplement", placeofimplimentation);

                cmd.Parameters.Add(new MySqlParameter("p_PDId", MySqlDbType.Int64)).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                long pdId = long.Parse(cmd.Parameters["p_PDId"].Value.ToString());
                if (pdId > 0)
                {
                    cmd = new MySqlCommand();
                    cmd.CommandText = "Delete from project_meterial_details where PDId=" + pdId.ToString() + " and Lead_Id='" + Lead_Id.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    int i = cmd.ExecuteNonQuery();

                    if ((materials != null) || (materials != ""))
                    {
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(materials);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_APP_INSERT_PROJECT_MATERIALS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDId", pdId);
                                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                                cmd.Parameters.AddWithValue("p_MeterialName", Regex.Replace(item.Key, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
                                cmd.Parameters.AddWithValue("p_MeterialCost", item.Value);
                                int iSaved = cmd.ExecuteNonQuery();
                            }
                        }

                    }

                    cmd = new MySqlCommand();
                    cmd.CommandText = "Delete from project_teamdetail where PDId=" + pdId.ToString() + " and Lead_Id='" + Lead_Id.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    if ((teammembers != null) || (teammembers != ""))
                    {
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(teammembers);
                        if (dict != null)
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_APP_INSERT_PROJECT_TEMEDETAILS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDId", pdId);
                                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                                cmd.Parameters.AddWithValue("p_MemberName", Regex.Replace(item.Key, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_MemberMailId", Regex.Replace(item.Value, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
                                int iSaved = cmd.ExecuteNonQuery();
                            }
                        }

                    }

                }
                if (pdId > 0)
                    status = "success";
                string Mailsend = "select S.Lead_Id,S.StudentName,S.MailId,S.MobileNo,C.College_Name from student_registration as S left join colleges as C on S.CollegeCode = C.CollegeId where S.Lead_Id='" + Lead_Id.ToString() + "'";
                MySqlCommand cmd1 = new MySqlCommand(Mailsend, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string body = PopulateBody(dt.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + Title.ToString() + ") proposal form has been submitted successfully</b>", "The details you entered are listed below:",
                        "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString().Trim() + "<br><br></li><li><b>StudentName:</b> " + dt.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + Title.ToString().Trim() + "<br><br></li><li><b>BeneficiaryNo:</b> " + BeneficiaryNo.ToString() + "<br><br></li><li><b>Beneficieries:</b> " + Beneficiary.ToString() + "<br><br></li><li><b>Objectives:</b> " + Objectives.ToString().Trim() + "<br><br></li><li><b>Action Plan:</b> " + ActionPlan.ToString().Trim() + "<br><br></li><li><b>placeofimplimentation:</b> " + placeofimplimentation.ToString().Trim() + "<br><br></li></ol><br><br>");

                    SendEmailProposed(body, dt.Rows[0]["MailId"].ToString(), "" + dt.Rows[0]["Lead_Id"].ToString() + " project proposal submitted successfully");
                }

                // FCMPushNotification.SendNotification("Lead", "Proposal form", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                string gstrQstr = "select distinct DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + pdId.ToString() + "'";
                MySqlCommand cmd2 = new MySqlCommand(gstrQstr, con);
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    FCMPushNotification.AndroidPush(dt2.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project  (" + dt2.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully  ", "Student", "Null");
                }

            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
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
    public vmproject PSGetProjectDetails(string leadId, int PDId)
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
                        vmP.Amount = long.Parse(dt.Rows[i]["Amount"].ToString());
                        vmP.ManagerComments = dt.Rows[i]["ManagerComments"].ToString();
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
                        if (dt.Rows[i]["MeterialName"].ToString() != null && dt.Rows[i]["MeterialCost"].ToString() != null)
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
                        vmP.Members = members;
                    }
                    else
                    {
                        vmP.Members = members;
                    }
                }
                else
                {
                    vmP.status = "Invalid project deails";
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
    public string PSUpdateprojectDetails(int PDId, string Lead_Id, string Title, string Theme, int BeneficiaryNo, string Objectives,
        string ActionPlan, string Beneficiaries, string placeofimplimentation)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandText = "update project_description set Title ='" + Regex.Replace(Title, "'", "`").Trim() + "',Theme=" + Theme + ",BeneficiaryNo=" + BeneficiaryNo + ",Beneficiaries='" + Regex.Replace(Beneficiaries, "'", "`").Trim() + "',Objectives='" + Regex.Replace(Objectives, "'", "`").Trim() + "',ActionPlan='" + Regex.Replace(ActionPlan, "'", "`").Trim() + "',ProjectStatus='Proposed',Placeofimplement='" + placeofimplimentation + "',Edited_Date=now() where PDId=" + PDId;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    status = "Project Updated.";




                string Mailsend = "select P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ActionPlan,P.ManagerComments,P.ProjectStatus,S.StudentName,S.MailId,S.MobileNo,C.College_Name from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
                MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {

                    string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Congratulations, Your project  (" + dt2.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully </b>", "The details you entered are listed below:",
                        "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li></ol><br><br>");

                    SendEmailProposed(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project proposal submitted successfully");
                }

                //   FCMPushNotification.SendNotification("Lead", "Proposal form", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                string gstrQstr = "select Distinct DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da1.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project  (" + dt.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully ", "Student", "Empty");
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

    public List<vmpl> CompletionGetApprovedProjectList(string Lead_Id)
    {
        List<vmpl> project = new List<vmpl>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_LIST_PROJECTS";
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);

                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        project.Add(new vmpl
                        {
                            PDId = long.Parse(dt.Rows[i]["PDId"].ToString()),
                            Title = dt.Rows[i]["Title"].ToString(),
                            SanctionAmount = long.Parse(dt.Rows[i]["SanctionAmount"].ToString()),
                            Giventotal = long.Parse(dt.Rows[i]["Giventotal"].ToString()),
                            ProjectStatus = dt.Rows[i]["ProjectStatus"].ToString(),
                            CompletionProgress = int.Parse(dt.Rows[i]["CompletionProgress"].ToString()),
                            status = "Success",
                            isImpact_Project = int.Parse(dt.Rows[i]["isImpact_Project"].ToString())
                        });
                    }
                }
                else
                {
                    project.Add(new vmpl { status = "Invalid project" });
                }
            }
        }
        catch (Exception)
        {
            project.Add(new vmpl { status = "Error" });
        }
        return project;

    }

    [WebMethod]
    public string UpdateProjectCompletion(string leadId, long ProjectId, float FundsRaised,
        string Challenge, string Learning, string AsAStory)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_UPDATE_PROJECT_COMPLETION";
                cmd.Connection = con;
                //  cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Parameters.AddWithValue("p_FundsRaised", FundsRaised);
                cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(Challenge, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(Learning, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(AsAStory, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                cmd.Parameters.AddWithValue("p_LeadId", leadId);
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                {
                    status += "success";

                    string Mailsend = "select P.Lead_Id, S.StudentName, S.MailId,S.MobileNo, C.College_Name, P.Title, P.BeneficiaryNo,P.Beneficiaries,P.Objectives, P.Placeofimplement, p.SanctionAmount, P.ActionPlan,P.Challenge,P.Learning, P.AsAStory,P.FundsRaised, P.ProjectStatus, P.ManagerComments,(Select Sum(Amount) from project_fund_details where PDId=P.PDId) as FundsReceived from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId = S.CollegeCode where P.PDId ='" + ProjectId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ")  has been requested for completion</b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b> SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + " <br><br></li><li><b>FundsReceived:</b> " + dt2.Rows[0]["FundsReceived"].ToString() + " <br><br></li><li><b>FundsRaised:</b> " + dt2.Rows[0]["FundsRaised"].ToString() + " <br><br></li><li><b>Challenges:</b> " + dt2.Rows[0]["Challenge"].ToString() + " <br><br></li><li><b>Learning:</b> " + dt2.Rows[0]["Learning"].ToString() + " <br><br></li><li><b>AsAStory:</b> " + dt2.Rows[0]["AsAStory"].ToString() + " <br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + " <br><br></li><li><b>ManagerComments:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + " <br><br></li></ol><br><br>");

                        SendEmailProposed(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project completion request submitted successfully");
                    }

                    //  FCMPushNotification.SendNotification("Lead", "project Complition", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + ProjectId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project (" + dt.Rows[0]["Title"].ToString() + ") has been requested for completion", "Student", "Empty");
                    }
                }
                else
                {
                    status += "success";
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
    //public vmGetpd CompletionGetApprovedProjectDetails(string leadId, int PDId)
    //{
    //    vmGetpd project = new vmGetpd();
    //    try
    //    {
    //        using (MySqlConnection con = new MySqlConnection(connection))
    //        {
    //            con.Open();
    //            MySqlCommand cmd = new MySqlCommand();
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_APP_PROJECT_DETAILS_1";
    //            cmd.Parameters.AddWithValue("p_Lead_Id", leadId);
    //            cmd.Parameters.AddWithValue("p_PDId", PDId);
    //            cmd.Connection = con;
    //            MySqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.HasRows)
    //            {
    //                if (dr.Read())
    //                {

    //                    // project.Lead_Id = dr["Lead_Id"].ToString();
    //                    // project.StudentName = dr["StudentName"].ToString();
    //                    project.Title = dr["Title"].ToString();
    //                    project.ThemeName = dr["ThemeName"].ToString();
    //                    project.BeneficiaryNo = long.Parse(dr["BeneficiaryNo"].ToString());
    //                    project.Objectives = dr["Objectives"].ToString();
    //                    project.Placeofimplement = dr["Placeofimplement"].ToString();
    //                    project.AskedAmount = int.Parse(dr["AskedAmount"].ToString());
    //                    project.SanctionAmount = int.Parse(dr["SanctionAmount"].ToString());
    //                    project.giventotal = long.Parse(dr["giventotal"].ToString());
    //                    // project.Amount = int.Parse(dr["Amount"].ToString());


    //                    project.status = "Success";
    //                }
    //            }
    //            else
    //            {
    //                project.status = "Invalid project details";
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        project.status = "Error";
    //    }
    //    return project;
    //}
    public vmGetpd CompletionGetApprovedProjectDetails(string leadId, int PDId)
    {
        vmGetpd project = new vmGetpd();
        List<vmProjectSDG_Goals> SDG = new List<vmProjectSDG_Goals>();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_PROJECT_DETAILS_1";
                cmd.Parameters.AddWithValue("p_Lead_Id", leadId);
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        project.Title = dr["Title"].ToString();
                        project.ThemeName = dr["ThemeName"].ToString();
                        project.BeneficiaryNo = long.Parse(dr["BeneficiaryNo"].ToString());
                        project.Objectives = dr["Objectives"].ToString();
                        project.Placeofimplement = dr["Placeofimplement"].ToString();
                        project.AskedAmount = int.Parse(dr["AskedAmount"].ToString());
                        project.SanctionAmount = int.Parse(dr["SanctionAmount"].ToString());
                        project.giventotal = long.Parse(dr["giventotal"].ToString());
                        project.Challenge = dr["Challenge"].ToString();
                        project.Learning = dr["Learning"].ToString();
                        project.AsAStory = dr["AsAStory"].ToString();
                        project.CurrentSituation = dr["CurrentSituation"].ToString();
                        project.Resource = dr["Resource"].ToString();
                        project.TotalResourses = long.Parse(dr["TotalResourses"].ToString());
                        project.status = "Success";
                        project.HoursSpend = int.Parse(dr["HoursSpend"].ToString());
                        project.ProjectStartDate = dr["ProjectStartDate"].ToString();
                        project.ProjectEndDate = dr["ProjectEndDate"].ToString();
                        project.IsImpactProject = int.Parse(dr["isImpact_Project"].ToString());
                        project.ProjectStatus = dr["ProjectStatus"].ToString();
                        project.Collaboration_Supported = dr["Collaboration_Supported"].ToString();
                        project.Permission_And_Activities = dr["Permission_And_Activities"].ToString();
                        project.Experience_Of_Initiative = dr["Experience_Of_Initiative"].ToString();
                        project.Lacking_initiative = dr["Lacking_initiative"].ToString();
                        project.Against_Tide = dr["Against_Tide"].ToString();
                        project.Cross_Hurdles = dr["Cross_Hurdles"].ToString();
                        project.Entrepreneurial_Venture = dr["Entrepreneurial_Venture"].ToString();
                        project.Government_Awarded = dr["Government_Awarded"].ToString();
                        project.Leadership_Roles = dr["Leadership_Roles"].ToString();
                    }
                    dr.Close();
                    DataTable SDGGoals = new DataTable();
                    cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_GET_PROJECT_SDGGOALS";
                    cmd.Parameters.AddWithValue("p_pdId", PDId);
                    cmd.Connection = con;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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
                        project.SDG_List = SDG;
                    }
                    else
                    {
                        project.SDG_Status = "No Goals";
                    }
                }
                else
                {
                    project.status = "Invalid project details";
                }
            }
        }
        catch (Exception ex)
        {
            project.status = "Error";
        }
        return project;
    }

    [WebMethod]
    public String SaveDeviceDetails(String username, String DeviceId, String OSVersion, String Manufacturer, String ModelNo,
        String SDKVersion, String DeviceSrlNo, String ServiceProvider, String SIMSrlNo, String DeviceWidth, String DeviceHeight, String AppVersion)
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
    public string UpdateProjectCompletionImg(long RegistrationId, string leadId, long ProjectId, string ProfileImage, int doccount)
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";
                            byte[] bytes = Convert.FromBase64String(ProfileImage);
                            var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                            fs.Write(bytes);
                            fs.Close();
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_LeadId", leadId);
                            cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                            cmd.Parameters.AddWithValue("p_Document_Id", doccount);
                            cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                            cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
                            cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                            iSaved = cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
                    }
                }
            }
            if (iSaved > 0)
                status = "success";
            else
                status += "Unable to Update Project Completion Details";
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public string UpdateProjectCompletionImgCompress(long RegistrationId, string leadId, long ProjectId, byte[] ProfileImage, int doccount)
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";

                            using (var ms = new MemoryStream(ProfileImage))
                            {
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

                                    con.Open();
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = con;
                                    //cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
                                    cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS1";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("p_LeadId", leadId);
                                    cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                                    cmd.Parameters.AddWithValue("p_Document_Id", doccount);
                                    cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                                    cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
                                    cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                                    iSaved = cmd.ExecuteNonQuery();
                                    bmp1.Save(Server.MapPath(fileName), jpgEncoder, myEncoderParameters);


                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
                    }
                }
            }

            if (iSaved > 0)
                status = "success";
            else
                status += "Unable to Update Project Completion Details";
        }
        catch (Exception)
        {
            status = "Error";
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
    //public string UpdateProjectCompletionImg(long RegistrationId, string leadId, long ProjectId, string ProfileImage, int doccount)
    //{
    //    string status = "";
    //    int iSaved = 0;
    //    try
    //    {
    //        if (ProfileImage != null)
    //        {
    //            if (ProfileImage.Length > 0)
    //            {
    //                try
    //                {
    //                    using (MySqlConnection con = new MySqlConnection(connection))
    //                    {
    //                        string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";
    //                        byte[] bytes = Convert.FromBase64String(ProfileImage);
    //                        var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
    //                        fs.Write(bytes);
    //                        fs.Close();

    //                        con.Open();
    //                        MySqlCommand cmd = new MySqlCommand();
    //                        cmd.Connection = con;
    //                        cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
    //                        cmd.CommandType = CommandType.StoredProcedure;
    //                        cmd.Parameters.AddWithValue("p_LeadId", leadId);
    //                        cmd.Parameters.AddWithValue("p_PDId", ProjectId);
    //                        cmd.Parameters.AddWithValue("p_Document_Id", doccount);
    //                        cmd.Parameters.AddWithValue("p_Document_Path", fileName);
    //                        cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
    //                        cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
    //                        iSaved = cmd.ExecuteNonQuery();
    //                    }
    //                }
    //                catch (Exception)
    //                {
    //                    status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
    //                }
    //            }
    //        }

    //        if (iSaved > 0)
    //            status = "success";
    //        else
    //            status += "Unable to Update Project Completion Details";
    //    }
    //    catch (Exception)
    //    {
    //        status = "Error";
    //    }
    //    return status;
    //}
    [WebMethod]
    public string UpdateProjectCompletionDocument(long ProjectId, string leadId, long RegistrationId, byte[] docFile, string extension)
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (docFile != null)
            {
                if (docFile.Length > 0)
                {
                    try
                    {
                        string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + "." + extension;
                        //byte[] bytes = Convert.FromBase64String(docFile);
                        var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                        fs.Write(docFile);
                        fs.Close();
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_LeadId", leadId);
                            cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                            cmd.Parameters.AddWithValue("p_Document_Id", 1);
                            cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                            cmd.Parameters.AddWithValue("p_Document_Type", "DOC");
                            cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                            iSaved += cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                if (iSaved > 0)
                    status = "success";
                else
                    status = "Unable to save document";
            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }


    [WebMethod]
    public List<vmEvent> GetEvents()
    {
        List<vmEvent> events = new List<vmEvent>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_EVENT_LIST";
                cmd.Parameters.AddWithValue("p_Id", 0);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        events.Add(new vmEvent
                        {
                            EventId = long.Parse(dt.Rows[i]["EventId"].ToString()),
                            EventApplyURL = dt.Rows[i]["EventApplyURL"].ToString(),
                            EventDescription = dt.Rows[i]["EventDescription"].ToString(),
                            EventFromDate = dt.Rows[i]["EventFromDate"].ToString(),
                            EventName = dt.Rows[i]["EventName"].ToString(),
                            EventToDate = dt.Rows[i]["EventToDate"].ToString(),
                            EventURL = dt.Rows[i]["EventURL"].ToString(),
                            Image_Path = dt.Rows[i]["Image_Path"].ToString(),
                            EventStatus = int.Parse(dt.Rows[i]["Status"].ToString()),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    events.Add(new vmEvent { Status = "Invalid  Project status" });
                }
            }
        }
        catch (Exception)
        {
            events.Add(new vmEvent { Status = "Error" });
        }
        return events;
    }

    [WebMethod]
    public vmleadmenber GetLeadAmbassadordetails(string leadId)
    {
        vmleadmenber stud = new vmleadmenber();
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_SELECT_LEAD_AMBASSADOR";
                cmd.Parameters.AddWithValue("P_Lead_Id", leadId);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {

                        stud.Lead_Id = dr["Lead_Id"].ToString();
                        stud.StudentName = dr["StudentName"].ToString();
                        stud.NoOfProject = long.Parse(dr["NoOfProject"].ToString());
                        stud.isApply_LeadAmbassador = int.Parse(dr["isApply_LeadAmbassador"].ToString());

                        if (stud.isApply_LeadAmbassador == 1)
                        {
                            stud.Status = "2";
                        }
                        else
                        {
                            stud.Status = "1";
                        }

                    }
                }
                else
                {
                    stud.Status = "0";
                }
            }
        }
        catch (Exception ex)
        {
            stud.Status = "Error";
        }
        return stud;
    }

    [WebMethod]
    public List<vmProjectCounts> GetProjectthemecount(int ManagerId)
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
                cmd.CommandText = "USP_MANAGE_THEMEWISE_PROGRAM_COUNT";
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
                            Counts = int.Parse(dt.Rows[i]["Counts"].ToString()),
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
    public string SendStudentRequestforManager(string Lead_Id, string Email_id, string Student_MobileNo, string Message, string StudentName,
        string StateName, string DistrictName, string TalukaName, string CollegeName)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INSERT_STUDENT_REQUEST_MAMAGER_NEW";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Email_id", Email_id);
                cmd.Parameters.AddWithValue("p_Student_MobileNo", Student_MobileNo);
                cmd.Parameters.AddWithValue("p_Message", Regex.Replace(Message.ToString(), "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_StudentName", Regex.Replace(StudentName.ToString(), "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_StateName", Regex.Replace(StateName.ToString(), "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_DistrictName", Regex.Replace(DistrictName.ToString(), "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_TalukaName", Regex.Replace(TalukaName.ToString(), "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_CollegeName", Regex.Replace(CollegeName.ToString(), "'", "`").Trim());
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";
                string managermail = "select M.MailId from student_request as P,manager_details as M,student_registration as S where M.ManagerId = S.ManagerCode and S.Lead_Id = P.Lead_id and P.Lead_Id = '" + Lead_Id.ToString() + "' order by P.slno desc limit 1";

                MySqlCommand cmd3 = new MySqlCommand(managermail, con);
                MySqlDataAdapter da3 = new MySqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                if (dt3.Rows.Count > 0)
                {
                    string MaildBody = PopulateBody(Email_id.ToString(),
                  "<b> Student Raised Some Queries </b>", "",
                 "Request is raised by Lead Id is " + Lead_Id.ToString() + " " + System.DateTime.Now.ToString() + " " +
                  "<table><tr><td>" + " " +
                   "<b style='color:red'>Student Request to Manager</b>" + " " +
                "<ol>" + " " +
                "<li><b>Student Name :</b> " + StudentName.ToString() + "<br><br></li>" + " " +
                "<li><b>Mobile No :</b> " + Student_MobileNo.ToString() + "<br><br></li> " + " " +
                 "<li><b>Email id :</b> " + Email_id.ToString() + "<br><br></li> " + " " +
               "<li><b>State Name :</b> " + StateName.ToString() + "<br><br></li> " + " " +
               "<li><b>District Name :</b> " + DistrictName.ToString() + "<br><br></li> " + " " +
               "<li><b>Taluka Name :</b> " + TalukaName.ToString() + "<br><br></li> " + " " +
               "<li><b>College Name :</b> " + CollegeName.ToString() + "<br><br></li> " + " " +
               "<li><b>Other  :</b> " + Message.ToString() + "<br><br></li></ol></td></tr></table><br><br>");
                    SendEmailstudent(MaildBody, dt3.Rows[0]["MailId"].ToString(), "" + Lead_Id.ToString() + " has raised a query");
                    SendEmailstudent(MaildBody, "leadmis@dfmail.org", "" + Lead_Id.ToString() + " has raised a query");

                }
                string MaildBody1 = PopulateBody(Email_id.ToString(),
                    "<b>Thank you for  getting in touch with us. </b>", "",
                   "Following query raised " + " " + System.DateTime.Now.ToString() + " " +
                   "<table><tr><td>" + " " +
                    "<b style='color:red'>Request to Manager will be addressed within 2 working days " + " " +
                    "If raised query is not resolved in given time you can write a email to abhinandan.k@dfmail.org Thank You</b>" + " " +
                 "<ol>" + " " +
                 "<li><b>Student Name :</b> " + StudentName.ToString() + "<br><br></li>" + " " +
                 "<li><b>Mobile No :</b> " + Student_MobileNo.ToString() + "<br><br></li> " + " " +
                  "<li><b>Email id :</b> " + Email_id.ToString() + "<br><br></li> " + " " +
                "<li><b>State Name :</b> " + StateName.ToString() + "<br><br></li> " + " " +
                "<li><b>District Name :</b> " + DistrictName.ToString() + "<br><br></li> " + " " +
                "<li><b>Taluka Name :</b> " + TalukaName.ToString() + "<br><br></li> " + " " +
                "<li><b>College Name :</b> " + CollegeName.ToString() + "<br><br></li> " + " " +
                "<li><b>Other  :</b> " + Message.ToString() + "<br><br></li></ol></td></tr></table><br><br>");
                SendEmailstudent(MaildBody1, Email_id.ToString(), "" + Lead_Id.ToString() + " has raised a query");
                string gstrQstr = "select DeviceId from user_device_details where Username='" + Lead_Id.ToString() + "' order by Id desc limit 1";
                MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Your request sent to manager", "Request", "Empty");
                }
                string gstrQrystr = "select DeviceId,Username from user_device_details where " + " " +
                "Username = (Select MobileNo from manager_details where ManagerId = (Select ManagerCode from student_registration where Lead_Id = '" + Lead_Id.ToString() + "'))";
                cmd1 = new MySqlCommand(gstrQrystr, con);
                da = new MySqlDataAdapter(cmd1);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string DeviceID = dt.Rows[0].ItemArray[0].ToString();
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), Lead_Id + "-" + "Has raised a query", "Manager", "Empty");
                    }
                }

                SaveStudentLog(Lead_Id.ToString(), "Student Requested " + " " + Message.ToString(), "Student Request");

            }
        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    protected void SendEmailstudent(string bodyString, string MailId, string Title)
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




    protected void SendEmailProposed(string bodyString, string MailId, string Title)
    {

        try
        {
            SendHtmlFormattedEmailBCC(MailId.ToString(), Title, bodyString);
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

            mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
            // mailMessage.Bcc.Add(new MailAddress("leadmis@dfmail.org"));

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


    private void SendHtmlFormattedEmailBCC(string recepientEmail, string subject, string body)
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

    [WebMethod]

    public string AddProjectproposal1json(long Student_Id, string Lead_Id, string Title, int BeneficiaryNo,
string ProjectType, string Objectives, string ActionPlan, string materials, float Budget, string AcademicID,
string teammembers, string Beneficiary, string placeofimplimentation, string CurrentSituation, string ProjectStartDate, string ProjectEndDate)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_INSERT_PROJECT_DESCRIPTIONS_NEW";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Student_Id", Student_Id);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
                cmd.Parameters.AddWithValue("p_Budget", Budget);
                cmd.Parameters.AddWithValue("p_Theme", ProjectType);
                cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AcademicCode", AcademicID);
                cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiary, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Placeofimplement", placeofimplimentation);
                cmd.Parameters.AddWithValue("p_CurrentSituation", Regex.Replace(CurrentSituation, "'", "`"));
                cmd.Parameters.AddWithValue("p_projectStartdate", ProjectStartDate);
                cmd.Parameters.AddWithValue("p_projectEnddate", ProjectEndDate);
                cmd.Parameters.Add(new MySqlParameter("p_PDId", MySqlDbType.Int64)).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                long pdId = long.Parse(cmd.Parameters["p_PDId"].Value.ToString());
                if (pdId > 0)
                {
                    if ((materials != null) || (materials != ""))
                    {
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(materials);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_APP_INSERT_PROJECT_MATERIALS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDId", pdId);
                                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                                cmd.Parameters.AddWithValue("p_MeterialName", Regex.Replace(item.Key, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
                                cmd.Parameters.AddWithValue("p_MeterialCost", item.Value);
                                int iSaved = cmd.ExecuteNonQuery();
                            }
                        }

                    }

                    if ((teammembers != null) || (teammembers != ""))
                    {
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(teammembers);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_APP_INSERT_PROJECT_TEMEDETAILS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDId", pdId);
                                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                                cmd.Parameters.AddWithValue("p_MemberName", Regex.Replace(item.Key, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_MemberMailId", Regex.Replace(item.Value, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
                                int iSaved = cmd.ExecuteNonQuery();
                            }
                        }

                    }

                }
                if (pdId > 0)
                    status = "success";
                string Mailsend = "select S.Lead_Id,S.StudentName,S.MailId,S.MobileNo,C.College_Name from student_registration as S left join colleges as C on S.CollegeCode = C.CollegeId where S.Lead_Id='" + Lead_Id.ToString() + "'";
                MySqlCommand cmd1 = new MySqlCommand(Mailsend, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string body = PopulateBody(dt.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + Title.ToString() + ") proposal form has been submitted successfully</b>", "The details you entered are listed below:",
                        "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString().Trim() + "<br><br></li><li><b>StudentName:</b> " + dt.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + Title.ToString().Trim() + "<br><br></li><li><b>BeneficiaryNo:</b> " + BeneficiaryNo.ToString() + "<br><br></li><li><b>Beneficieries:</b> " + Beneficiary.ToString() + "<br><br></li><li><b>Objectives:</b> " + Objectives.ToString().Trim() + "<br><br></li><li><b>Action Plan:</b> " + ActionPlan.ToString().Trim() + "<br><br></li><li><b>placeofimplimentation:</b> " + placeofimplimentation.ToString().Trim() + "<br><br></li></ol><br><br>");

                    SendEmailProposed(body, dt.Rows[0]["MailId"].ToString(), "" + dt.Rows[0]["Lead_Id"].ToString() + " project proposal submitted successfully");
                }

                // FCMPushNotification.SendNotification("Lead", "Proposal form", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + pdId.ToString() + "'";
                MySqlCommand cmd2 = new MySqlCommand(gstrQstr, con);
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    FCMPushNotification.AndroidPush(dt2.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project  (" + dt2.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully  ", "Student", "Null");
                }

            }
            SaveStudentLog(Lead_Id.ToString(), Title.ToString(), "Proposed Project (Student)");
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
    }
    //   public string AddProjectproposal1json(long Student_Id, string Lead_Id, string Title, int BeneficiaryNo,
    //string ProjectType, string Objectives, string ActionPlan, string materials, float Budget, string AcademicID,
    //string teammembers, string Beneficiary, string placeofimplimentation, string CurrentSituation)
    //   {
    //       string status = "";
    //       try
    //       {
    //           using (MySqlConnection con = new MySqlConnection(connection))
    //           {
    //               con.Open();
    //               MySqlCommand cmd = new MySqlCommand();
    //               cmd.CommandType = CommandType.StoredProcedure;
    //               cmd.CommandText = "USP_APP_INSERT_PROJECT_DESCRIPTIONS";
    //               cmd.Connection = con;
    //               cmd.Parameters.AddWithValue("p_Student_Id", Student_Id);
    //               cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
    //               cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
    //               cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
    //               cmd.Parameters.AddWithValue("p_Budget", Budget);
    //               cmd.Parameters.AddWithValue("p_Theme", ProjectType);
    //               cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
    //               cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
    //               cmd.Parameters.AddWithValue("p_AcademicCode", AcademicID);
    //               cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiary, "'", "`").Trim());
    //               cmd.Parameters.AddWithValue("p_Placeofimplement", placeofimplimentation);
    //               cmd.Parameters.AddWithValue("p_CurrentSituation", Regex.Replace(CurrentSituation, "'", "`"));

    //               cmd.Parameters.Add(new MySqlParameter("p_PDId", MySqlDbType.Int64)).Direction = ParameterDirection.Output;
    //               cmd.ExecuteNonQuery();
    //               long pdId = long.Parse(cmd.Parameters["p_PDId"].Value.ToString());
    //               if (pdId > 0)
    //               {
    //                   if ((materials != null) || (materials != ""))
    //                   {
    //                       var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(materials);
    //                       foreach (var item in dict)
    //                       {
    //                           cmd = new MySqlCommand();
    //                           cmd.Connection = con;
    //                           cmd.CommandText = "USP_APP_INSERT_PROJECT_MATERIALS";
    //                           cmd.CommandType = CommandType.StoredProcedure;
    //                           cmd.Parameters.AddWithValue("p_PDId", pdId);
    //                           cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
    //                           cmd.Parameters.AddWithValue("p_MeterialName", Regex.Replace(item.Key, "'", "`").Trim());
    //                           cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
    //                           cmd.Parameters.AddWithValue("p_MeterialCost", item.Value);
    //                           int iSaved = cmd.ExecuteNonQuery();
    //                       }
    //                   }

    //                   if ((teammembers != null) || (teammembers != ""))
    //                   {
    //                       var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(teammembers);
    //                       foreach (var item in dict)
    //                       {
    //                           cmd = new MySqlCommand();
    //                           cmd.Connection = con;
    //                           cmd.CommandText = "USP_APP_INSERT_PROJECT_TEMEDETAILS";
    //                           cmd.CommandType = CommandType.StoredProcedure;
    //                           cmd.Parameters.AddWithValue("p_PDId", pdId);
    //                           cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
    //                           cmd.Parameters.AddWithValue("p_MemberName", Regex.Replace(item.Key, "'", "`").Trim());
    //                           cmd.Parameters.AddWithValue("p_MemberMailId", Regex.Replace(item.Value, "'", "`").Trim());
    //                           cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
    //                           int iSaved = cmd.ExecuteNonQuery();
    //                       }
    //                   }

    //               }
    //               if (pdId > 0)
    //                   status = "success";
    //               string Mailsend = "select S.Lead_Id,S.StudentName,S.MailId,S.MobileNo,C.College_Name from student_registration as S left join colleges as C on S.CollegeCode = C.CollegeId where S.Lead_Id='" + Lead_Id.ToString() + "'";
    //               MySqlCommand cmd1 = new MySqlCommand(Mailsend, con);
    //               MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
    //               DataTable dt = new DataTable();
    //               da.Fill(dt);
    //               if (dt.Rows.Count > 0)
    //               {
    //                   string body = PopulateBody(dt.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + Title.ToString() + ") proposal form has been submitted successfully</b>", "The details you entered are listed below:",
    //                       "<ol><li><b>LEAD Id:</b> " + Lead_Id.ToString().Trim() + "<br><br></li><li><b>StudentName:</b> " + dt.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + Title.ToString().Trim() + "<br><br></li><li><b>BeneficiaryNo:</b> " + BeneficiaryNo.ToString() + "<br><br></li><li><b>Beneficieries:</b> " + Beneficiary.ToString() + "<br><br></li><li><b>Objectives:</b> " + Objectives.ToString().Trim() + "<br><br></li><li><b>Action Plan:</b> " + ActionPlan.ToString().Trim() + "<br><br></li><li><b>placeofimplimentation:</b> " + placeofimplimentation.ToString().Trim() + "<br><br></li></ol><br><br>");

    //                   SendEmailProposed(body, dt.Rows[0]["MailId"].ToString(), "" + dt.Rows[0]["Lead_Id"].ToString() + " project proposal submitted successfully");
    //               }

    //               // FCMPushNotification.SendNotification("Lead", "Proposal form", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
    //               string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + pdId.ToString() + "'";
    //               MySqlCommand cmd2 = new MySqlCommand(gstrQstr, con);
    //               MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
    //               DataTable dt2 = new DataTable();
    //               da2.Fill(dt2);
    //               if (dt2.Rows.Count > 0)
    //               {
    //                   FCMPushNotification.AndroidPush(dt2.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project  (" + dt2.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully  ", "Student", "Null");
    //               }


    //               else
    //                   status = "Unable to add project";
    //           }
    //       }
    //       catch (Exception)
    //       {
    //           status = ex.Message.ToString();
    //       }
    //       return status;
    //   }



    [WebMethod]
    public string PSUpdateprojectDetails1(int PDId, string Lead_Id, string Title, string Theme, int BeneficiaryNo, string Objectives,
        string ActionPlan, string Beneficiaries, string placeofimplimentation, string CurrentSituation, string StartDate, string EndDate,
        string materials, string teammembers, long Student_Id, int Budget) // in mobile app float is not saving so changing to int
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_UPDATE_PROJECT_STUDENT_MODIFICATION";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Student_Id", Student_Id);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_Title", Regex.Replace(Title, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_BeneficiaryNo", BeneficiaryNo);
                cmd.Parameters.AddWithValue("p_Budget", Budget);
                cmd.Parameters.AddWithValue("p_Theme", Theme);
                cmd.Parameters.AddWithValue("p_Objectives", Regex.Replace(Objectives, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ActionPlan", Regex.Replace(ActionPlan, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Beneficiaries", Regex.Replace(Beneficiaries, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Placeofimplement", placeofimplimentation);
                cmd.Parameters.AddWithValue("p_CurrentSituation", Regex.Replace(CurrentSituation, "'", "`"));
                cmd.Parameters.AddWithValue("p_projectStartdate", StartDate);
                cmd.Parameters.AddWithValue("p_projectEnddate", EndDate);
                cmd.Parameters.AddWithValue("p_PDId", PDId);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Project Updated.";
                    cmd = new MySqlCommand();
                    cmd.CommandText = "Delete from project_meterial_details where PDId=" + PDId.ToString() + " and Lead_Id='" + Lead_Id.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    if ((materials.ToString() != "") || (materials.ToString() != null))
                    {
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(materials);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_APP_INSERT_PROJECT_MATERIALS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDId", PDId);
                                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                                cmd.Parameters.AddWithValue("p_MeterialName", Regex.Replace(item.Key, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
                                cmd.Parameters.AddWithValue("p_MeterialCost", item.Value);
                                int iSaved = cmd.ExecuteNonQuery();
                            }
                        }

                    }

                    cmd = new MySqlCommand();
                    cmd.CommandText = "Delete from project_teamdetail where PDId=" + PDId.ToString() + " and Lead_Id='" + Lead_Id.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    if ((teammembers.ToString() != "") || (teammembers.ToString() != null))
                    {
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(teammembers);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_APP_INSERT_PROJECT_TEMEDETAILS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDId", PDId);
                                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                                cmd.Parameters.AddWithValue("p_MemberName", Regex.Replace(item.Key, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_MemberMailId", Regex.Replace(item.Value, "'", "`").Trim());
                                cmd.Parameters.AddWithValue("p_RegistrationId", Student_Id);
                                int iSaved = cmd.ExecuteNonQuery();
                            }
                        }

                    }

                    string Mailsend = "select P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ActionPlan,P.ManagerComments,P.ProjectStatus,S.StudentName,S.MailId,S.MobileNo,C.College_Name from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Congratulations, Your project  (" + dt2.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully </b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li></ol><br><br>");

                        SendEmailProposed(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project proposal submitted successfully");
                    }

                    //   FCMPushNotification.SendNotification("Lead", "Proposal form", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da1.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project  (" + dt.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully ", "Student", "Empty");
                    }
                    SaveStudentLog(Lead_Id.ToString(), Title.ToString(), "Proposed Project Modification (Student)");
                }
                else
                {
                    status = "Updation Failed";
                }

            }
        }
        catch (Exception ex)
        {
            status = "Error : " + ex.Message.ToString();
        }
        return status;
    }

    //public string PSUpdateprojectDetails1(int PDId, string Lead_Id, string Title, string Theme, int BeneficiaryNo, string Objectives,
    //  string ActionPlan, string Beneficiaries, string placeofimplimentation, string CurrentSituation)
    //{
    //    string status = "";

    //    try
    //    {
    //        using (MySqlConnection con = new MySqlConnection(connection))
    //        {
    //            con.Open();
    //            MySqlCommand cmd = new MySqlCommand();


    //            cmd.CommandText = "update project_description set Title ='" + Regex.Replace(Title, "'", "`").Trim() + "',Theme=" + Theme + ",BeneficiaryNo=" + BeneficiaryNo + ",Beneficiaries='" + Regex.Replace(Beneficiaries, "'", "`").Trim() + "',Objectives='" + Regex.Replace(Objectives, "'", "`").Trim() + "',ActionPlan='" + Regex.Replace(ActionPlan, "'", "`").Trim() + "',ProjectStatus='Proposed',Placeofimplement='" + placeofimplimentation + "',CurrentSituation='" + CurrentSituation + "',Edited_Date=now() where PDId=" + PDId;
    //            cmd.CommandType = CommandType.Text;
    //            cmd.Connection = con;
    //            int i = cmd.ExecuteNonQuery();
    //            if (i > 0)
    //                status = "Project Updated.";
    //            string Mailsend = "select P.Lead_Id,P.Title,P.BeneficiaryNo,P.Beneficiaries,P.Placeofimplement,P.Objectives,P.ActionPlan,P.ManagerComments,P.ProjectStatus,S.StudentName,S.MailId,S.MobileNo,C.College_Name from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId=S.CollegeCode where P.PDId = '" + PDId.ToString() + "'";
    //            MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
    //            MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
    //            DataTable dt2 = new DataTable();
    //            da2.Fill(dt2);
    //            if (dt2.Rows.Count > 0)
    //            {

    //                string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b> Congratulations, Your project  (" + dt2.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully </b>", "The details you entered are listed below:",
    //                    "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br><br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + "<br><br></li></ol><br><br>");

    //                SendEmailProposed(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project proposal submitted successfully");
    //            }

    //            //   FCMPushNotification.SendNotification("Lead", "Proposal form", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
    //            string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + PDId.ToString() + "'";
    //            MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
    //            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
    //            DataTable dt = new DataTable();
    //            da1.Fill(dt);
    //            if (dt.Rows.Count > 0)
    //            {
    //                GCMNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project  (" + dt.Rows[0]["Title"].ToString() + ") proposal form has been submitted successfully ", "Student", "Empty");
    //            }


    //            else
    //                status = "Unable to update project details";
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        status = "Error";
    //    }
    //    return status;
    //}

    [WebMethod]
    public string ApplyTshirtRequest(int RegistrationId, string Lead_Id, int ManagerId,
      string MemberName, string TshirtSize, int RequestedCount)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APPLY_TSHIRT";
                cmd.Connection = con;
                //  cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Parameters.AddWithValue("p_RegistrationId", RegistrationId);
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                cmd.Parameters.AddWithValue("p_MemberName", Regex.Replace(MemberName, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_TshirtSize", Regex.Replace(TshirtSize, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_RequestedCount", RequestedCount);
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";
                string gstrQrystr = "Select MobileNo from manager_details where managerid=" + ManagerId.ToString() + "";
                MySqlCommand cmd1 = new MySqlCommand(gstrQrystr, con);
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                gstrQrystr = "select distinct DeviceId,username from user_device_details where username='" + dt1.Rows[0].ItemArray[0].ToString() + "' order by id desc limit 1";
                cmd1 = new MySqlCommand(gstrQrystr, con);
                da1 = new MySqlDataAdapter(cmd1);
                dt1 = new DataTable();
                da1.Fill(dt1);
                string DeviceID = dt1.Rows[0].ItemArray[0].ToString();
                if (DeviceID != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Student Requested Tshirt" + " " + Lead_Id.ToString() + "- Size" + " " + TshirtSize, "pmtshirt", "Empty");
                }
                else
                    status += "Unable to apply your Tshirt";
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
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
                        vmLg.isRequestForTShirt = 0;  // int.Parse(dr["isRequestForTShirt"].ToString());
                        // vmLg.isRequestForTShirt = int.Parse(dr["isRequestForTShirt"].ToString());
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


    [WebMethod]
    public string UpdateProjectCompletions(string Placeofimplement, string leadId, long ProjectId, float FundsRaised,
     string Challenge, string Learning, string AsAStory, string Resource, int HoursSpend, string SDG_Goal,
     string Collaboration_Supported, string Permission_And_Activities, string Experience_Of_Initiative, string Lacking_initiative,
    string Against_Tide, string Cross_Hurdles, string Entrepreneurial_Venture, string Government_Awarded, string Leadership_Roles, long TotalResources)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_UPDATE_PROJECT_COMPLETIONS_LATEST";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
                cmd.Parameters.AddWithValue("p_FundsRaised", FundsRaised);
                cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(Challenge, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(Learning, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(AsAStory, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                cmd.Parameters.AddWithValue("p_LeadId", leadId);
                cmd.Parameters.AddWithValue("p_Resource", Regex.Replace(Resource, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ResourceAmount", TotalResources);
                cmd.Parameters.AddWithValue("P_HoursSpend", HoursSpend);
                cmd.Parameters.AddWithValue("p_Collaboration_Supported", Regex.Replace(Collaboration_Supported, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Permission_And_Activities", Regex.Replace(Permission_And_Activities, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Experience_Of_Initiative", Regex.Replace(Experience_Of_Initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Lacking_initiative", Regex.Replace(Lacking_initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Against_Tide", Regex.Replace(Against_Tide, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Cross_Hurdles", Regex.Replace(Cross_Hurdles, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Entrepreneurial_Venture", Regex.Replace(Entrepreneurial_Venture, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Government_Awarded", Regex.Replace(Government_Awarded, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Leadership_Roles", Regex.Replace(Leadership_Roles, "'", "`").Trim());
                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                {
                    status += "success";

                    if ((SDG_Goal != null) || (SDG_Goal != ""))
                    {
                        int i = 0;
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(SDG_Goal);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_INSERT_PROJECT_SDG_GOALS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDID", ProjectId);
                                cmd.Parameters.AddWithValue("p_SDG_GOAL", item.Value);
                                cmd.Parameters.AddWithValue("p_LoopCount", i);
                                int iSaved = cmd.ExecuteNonQuery();
                                i++;
                            }
                        }

                    }

                    string Mailsend = "select P.Lead_Id, S.StudentName, S.MailId,S.MobileNo, C.College_Name, P.Title, P.BeneficiaryNo,P.Beneficiaries,P.Objectives, P.Placeofimplement, p.SanctionAmount, P.ActionPlan,P.Challenge,P.Learning, P.AsAStory,P.FundsRaised, P.ProjectStatus, P.ManagerComments,(Select Sum(Amount) from project_fund_details where PDId=P.PDId) as FundsReceived from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId = S.CollegeCode where P.PDId ='" + ProjectId.ToString() + "'";
                    MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {

                        string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ")  has been requested for completion</b>", "The details you entered are listed below:",
                            "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b> SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + " <br><br></li><li><b>FundsReceived:</b> " + dt2.Rows[0]["FundsReceived"].ToString() + " <br><br></li><li><b>FundsRaised:</b> " + dt2.Rows[0]["FundsRaised"].ToString() + " <br><br></li><li><b>Challenges:</b> " + dt2.Rows[0]["Challenge"].ToString() + " <br><br></li><li><b>Learning:</b> " + dt2.Rows[0]["Learning"].ToString() + " <br><br></li><li><b>AsAStory:</b> " + dt2.Rows[0]["AsAStory"].ToString() + " <br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + " <br><br></li><li><b>ManagerComments:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + " <br><br></li></ol><br><br>");
                        SendEmailProposed(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project completion request submitted successfully");

                    }

                    SaveStudentLog("" + dt2.Rows[0]["Lead_Id"].ToString() + "", "" + dt2.Rows[0]["Title"].ToString() + "", "completion request Project (Student)");
                    //FCMPushNotification.SendNotification("Lead", "project Complition", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
                    string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + ProjectId.ToString() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project (" + dt.Rows[0]["Title"].ToString() + ") has been requested for completion", "Student", "Empty");

                    }
                }
                else
                {
                    status = "Failed";
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
    public string UpdateProjectCompletionsDraft(string leadId, string Title, long ProjectId, float FundsRaised,
 string Challenge, string Learning, string AsAStory, string Resource, int CompletionProgress, string Placeofimplement, int HoursSpend,
 string SDG_Goal, string Collaboration_Supported, string Permission_And_Activities, string Experience_Of_Initiative, string Lacking_initiative,
 string Against_Tide, string Cross_Hurdles, string Entrepreneurial_Venture, string Government_Awarded, string Leadership_Roles, long TotalResources)
    {
        string status = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_UPDATE_PROJECT_DRAFT_LATEST";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Placeofimplement", Regex.Replace(Placeofimplement, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_LeadId", leadId);
                cmd.Parameters.AddWithValue("p_Title", Title);
                cmd.Parameters.AddWithValue("p_FundsRaised", FundsRaised);
                cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(Challenge, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(Learning, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(AsAStory, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Resource", Regex.Replace(Resource, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_ResourceAmount", TotalResources);
                cmd.Parameters.AddWithValue("p_CompletionProgress", CompletionProgress);
                cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                cmd.Parameters.AddWithValue("P_HoursSpend", HoursSpend);
                cmd.Parameters.AddWithValue("p_Collaboration_Supported", Regex.Replace(Collaboration_Supported, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Permission_And_Activities", Regex.Replace(Permission_And_Activities, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Experience_Of_Initiative", Regex.Replace(Experience_Of_Initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Lacking_initiative", Regex.Replace(Lacking_initiative, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Against_Tide", Regex.Replace(Against_Tide, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Cross_Hurdles", Regex.Replace(Cross_Hurdles, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Entrepreneurial_Venture", Regex.Replace(Entrepreneurial_Venture, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Government_Awarded", Regex.Replace(Government_Awarded, "'", "`").Trim());
                cmd.Parameters.AddWithValue("p_Leadership_Roles", Regex.Replace(Leadership_Roles, "'", "`").Trim());

                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                {
                    if ((SDG_Goal != null) || (SDG_Goal != ""))
                    {
                        int i = 0;
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(SDG_Goal);
                        if ((dict != null) || (dict.Count != 0))
                        {
                            foreach (var item in dict)
                            {
                                cmd = new MySqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "USP_INSERT_PROJECT_SDG_GOALS";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_PDID", ProjectId);
                                cmd.Parameters.AddWithValue("p_SDG_GOAL", item.Value);
                                cmd.Parameters.AddWithValue("p_LoopCount", i);
                                int iSaved = cmd.ExecuteNonQuery();
                                i++;
                            }
                        }

                    }
                    status += "success";
                }
                else
                {
                    status += "Unable to Update Project Save draft";
                }
                SaveStudentLog(leadId.ToString(), Title.ToString(), "Save Draft Project (Student)");

            }
        }
        catch (Exception ex)
        {
            status = "Error" + ex.Message.ToString();
        }
        return status;
    }

    //    public string UpdateProjectCompletionsDraft(string leadId, long ProjectId, float FundsRaised,
    //string Challenge, string Learning, string AsAStory, string Resource)
    //    {
    //        string status = "";
    //        try
    //        {
    //            using (MySqlConnection con = new MySqlConnection(connection))
    //            {
    //                con.Open();
    //                MySqlCommand cmd = new MySqlCommand();
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.CommandText = "USP_APP_UPDATE_PROJECT_COMPLETION";
    //                cmd.Connection = con;
    //                //  cmd.Parameters.AddWithValue("p_Placeofimplement", Placeofimplement);
    //                cmd.Parameters.AddWithValue("p_FundsRaised", FundsRaised);
    //                cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(Challenge, "'", "`").Trim());
    //                cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(Learning, "'", "`").Trim());
    //                cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(AsAStory, "'", "`").Trim());
    //                cmd.Parameters.AddWithValue("p_PDId", ProjectId);
    //                cmd.Parameters.AddWithValue("p_LeadId", leadId);
    //                cmd.Parameters.AddWithValue("p_Resource", Regex.Replace(Resource, "'", "`").Trim());

    //                int iDBStatus = cmd.ExecuteNonQuery();
    //                if (iDBStatus > 0)
    //                    status += "success";

    //                string Mailsend = "select P.Lead_Id, S.StudentName, S.MailId,S.MobileNo, C.College_Name, P.Title, P.BeneficiaryNo,P.Beneficiaries,P.Objectives, P.Placeofimplement, p.SanctionAmount, P.ActionPlan,P.Challenge,P.Learning, P.AsAStory,P.FundsRaised, P.ProjectStatus, P.ManagerComments,(Select Sum(Amount) from project_fund_details where PDId=P.PDId) as FundsReceived from project_description as P inner join  student_registration as S on P.Lead_Id = S.Lead_Id left join colleges as C on C.CollegeId = S.CollegeCode where P.PDId ='" + ProjectId.ToString() + "'";
    //                MySqlCommand cmd2 = new MySqlCommand(Mailsend, con);
    //                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
    //                DataTable dt2 = new DataTable();
    //                da2.Fill(dt2);
    //                if (dt2.Rows.Count > 0)
    //                {

    //                    string body = PopulateBody(dt2.Rows[0]["StudentName"].ToString(), "<b>Congratulations, Your project (" + dt2.Rows[0]["Title"].ToString() + ")  has been Save draft successfully</b>", "The details you entered are listed below:",
    //                        "<ol><li><b>LEAD Id:</b> " + dt2.Rows[0]["Lead_Id"].ToString() + "<br><br></li><li><b>StudentName:</b> " + dt2.Rows[0]["StudentName"].ToString() + "<br><br></li><li><b>MobileNo:</b> " + dt2.Rows[0]["MobileNo"].ToString() + "<br><br></li><li><b>College_Name:</b> " + dt2.Rows[0]["College_Name"].ToString() + "<br> <br></li><li><b>Title of the Project:</b> " + dt2.Rows[0]["Title"].ToString() + "<br><br></li><li><b>BeneficiaryNo:</b> " + dt2.Rows[0]["BeneficiaryNo"].ToString() + "<br><br></li><li><b>Action Plan:</b> " + dt2.Rows[0]["ActionPlan"].ToString() + "<br><br></li><li><b>Placeofimplement:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b>Beneficiaries:</b> " + dt2.Rows[0]["Beneficiaries"].ToString() + "<br><br></li><li><b>Objectives:</b> " + dt2.Rows[0]["Objectives"].ToString() + "<br><br></li><li><b>placeofimplimentation:</b> " + dt2.Rows[0]["Placeofimplement"].ToString() + "<br><br></li><li><b> SanctionAmount:</b> " + dt2.Rows[0]["SanctionAmount"].ToString() + " <br><br></li><li><b>FundsReceived:</b> " + dt2.Rows[0]["FundsReceived"].ToString() + " <br><br></li><li><b>FundsRaised:</b> " + dt2.Rows[0]["FundsRaised"].ToString() + " <br><br></li><li><b>Challenges:</b> " + dt2.Rows[0]["Challenge"].ToString() + " <br><br></li><li><b>Learning:</b> " + dt2.Rows[0]["Learning"].ToString() + " <br><br></li><li><b>AsAStory:</b> " + dt2.Rows[0]["AsAStory"].ToString() + " <br><br></li><li><b>ProjectStatus:</b> " + dt2.Rows[0]["ProjectStatus"].ToString() + " <br><br></li><li><b>ManagerComments:</b> " + dt2.Rows[0]["ManagerComments"].ToString() + " <br><br></li></ol><br><br>");



    //                    SendEmailProposed(body, dt2.Rows[0]["MailId"].ToString(), "" + dt2.Rows[0]["Lead_Id"].ToString() + " project Save Draft submitted successfully");



    //                }

    //                SaveStudentLog("" + dt2.Rows[0]["Lead_Id"].ToString() + "", "" + dt2.Rows[0]["Title"].ToString() + "", "Save Draft Project (Student)");
    //                //  FCMPushNotification.SendNotification("Lead", "project Complition", "fpBXizyYMus:APA91bEmM6VGZwiqDrk-eMAJiXBkkUfe5L2lRbp9cqljycii4xSbAzDyjkf2fZ6LwBrdJkHak3RgRj0cnF9sinTDD2DqndoORPxNEpBCVQ9AtRL8LPUjn7L-jIViZYCoyH-ycuOCSYb0yxiKHkDtbl9q0GbDDftDCQ");
    //                string gstrQstr = "select DeviceId,Title from user_device_details inner join project_description on Lead_Id=Username  where PDId='" + ProjectId.ToString() + "'";
    //                MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
    //                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
    //                DataTable dt = new DataTable();
    //                da.Fill(dt);
    //                if (dt.Rows.Count > 0)
    //                {
    //                    GCMNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Congratulations, Your project (" + dt.Rows[0]["Title"].ToString() + ") has been added to Save Draft", "Student", "Empty");

    //                }


    //                else
    //                    status += "Unable to Update Project Save draft";
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            status = "Error";
    //        }
    //        return status;
    //    }

    public void SaveStudentLog(string Lead_Id, string Message, string Type)
    {
        string status = "";

        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INSERT_NOTIFICATION_STUDENT";
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
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
    public vmProjectCompletion GetProjectDraftDetails(long projectId, string Lead_Id)
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
                cmd.CommandText = "USP_MANAGER_SELECT_PROJECT_DRAFT_DETAILS";
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
                        vmproj.CompletionProgress = int.Parse(dt.Rows[i]["CompletionProgress"].ToString());
                        vmproj.ProjectStatus = dt.Rows[i]["ProjectStatus"].ToString();
                        vmproj.Status = "Success";
                        vmproj.HoursSpend = int.Parse(dt.Rows[i]["HoursSpend"].ToString());
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
        catch (Exception ex)
        {
            vmproj.Status = "Error" + ex.Message.ToString();
        }
        return vmproj;
    }

    [WebMethod]
    public string UpdateProjectCompletionDocumentDraft(long ProjectId, string leadId, long RegistrationId, byte[] docFile, string extension)
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (docFile != null)
            {
                if (docFile.Length > 0)
                {
                    try
                    {
                        string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + "." + extension;
                        //byte[] bytes = Convert.FromBase64String(docFile);
                        var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                        fs.Write(docFile);
                        fs.Close();
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS1";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_LeadId", leadId);
                            cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                            cmd.Parameters.AddWithValue("p_Document_Id", 1);
                            cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                            cmd.Parameters.AddWithValue("p_Document_Type", "DOC");
                            cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                            iSaved += cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                if (iSaved > 0)
                    status = "success";
                else
                    status = "Unable to save document";
            }
        }
        catch (Exception ex)
        {
            status = "Error" + ex.Message.ToString();
        }
        return status;
    }

    [WebMethod]
    public string UpdateProjectCompletionImgCompressDraft(long RegistrationId, string leadId, long ProjectId, byte[] ProfileImage, int doccount)
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";

                            using (var ms = new MemoryStream(ProfileImage))
                            {
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

                                    con.Open();
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = con;
                                    cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS1";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("p_LeadId", leadId);
                                    cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                                    cmd.Parameters.AddWithValue("p_Document_Id", doccount);
                                    cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                                    cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
                                    cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                                    iSaved = cmd.ExecuteNonQuery();
                                    bmp1.Save(Server.MapPath(fileName), jpgEncoder, myEncoderParameters);

                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
                    }
                }
            }

            if (iSaved > 0)
                status = "success";
            else
                status += "Unable to Update Project Completion Details";
        }
        catch (Exception e)
        {
            status = "Error" + e.Message.ToString();
        }
        return status;
    }

    [WebMethod]

    public string UpdateProjectCompletionImgCompressListINT(long RegistrationId, string leadId, long ProjectId, int[] ProfileImage, int doccount)
    {
        string status = "";

        try
        {
            if (ProfileImage != null)
            {

                status = "Hit Array";
            }
            else
            {
                status = "not hit";
            }

        }
        catch (Exception)
        {
            status = "Error";
        }
        return status;
    }

    [WebMethod]
    public string UpdateProjectCompletionImgCompressList(long RegistrationId, string leadId, long ProjectId, byte[] ProfileImage)
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            int docId = 0;
                            string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";
                            foreach (byte bytess in ProfileImage)
                            {

                                // byte[] array = BitConverter.GetBytes(bytess);

                                docId++;
                                using (var ms = new MemoryStream(bytess))
                                {
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

                                        con.Open();
                                        MySqlCommand cmd = new MySqlCommand();
                                        cmd.Connection = con;
                                        cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("p_LeadId", leadId);
                                        cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                                        cmd.Parameters.AddWithValue("p_Document_Id", docId);
                                        cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                                        cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
                                        cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                                        iSaved = cmd.ExecuteNonQuery();
                                        bmp1.Save(Server.MapPath(fileName), jpgEncoder, myEncoderParameters);
                                        if (iSaved > 0)
                                            status = "success";
                                        else
                                            status += "Unable to Update Project Completion Details";

                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
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
    public string UpdateTshirtRequests(int RequestedId, string TshirtSize)
    {
        string status = "";
        // string Query = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_EDIT_TSHIRT";
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("p_RequestedId", RequestedId);
                cmd.Parameters.AddWithValue("p_TshirtSize", Regex.Replace(TshirtSize, "'", "`").Trim());

                int iDBStatus = cmd.ExecuteNonQuery();
                if (iDBStatus > 0)
                    status += "success";

                else
                    status += "Unable to apply your Tshirt";

            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
    }
    [WebMethod]
    public List<vmtshirtlist> GetTshirtlist(string Lead_id)
    {
        List<vmtshirtlist> StudentReuest = new List<vmtshirtlist>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TSHIRT_LISTS";
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StudentReuest.Add(new vmtshirtlist
                        {
                            RequestedId = int.Parse(dt.Rows[i]["RequestedId"].ToString()),
                            TshirtSize = dt.Rows[i]["TshirtSize"].ToString(),
                            RequestedDate = dt.Rows[i]["RequestedDate"].ToString(),
                            SanctionDate = dt.Rows[i]["SanctionDate"].ToString(),
                            TshirtStatus = dt.Rows[i]["Status"].ToString(),

                            Status = "Success"
                        });
                    }
                }
                else
                {
                    StudentReuest.Add(new vmtshirtlist { Status = "There is no Tshirt request here" });
                }
            }
        }
        catch (Exception)
        {
            StudentReuest.Add(new vmtshirtlist { Status = "Error" });
        }
        return StudentReuest;

    }

    [WebMethod]
    public string UpdateProjectCompletionList(string lead_Id, long Project_Id, string Title, float FundsRaised, string Challenge, string Learning, string AsAStory, string Resource,
        int CompletionProgress, long RegistrationId, string leadId, long ProjectId, List<byte[]> ProjectImage, int doccount, long TotalResources)

    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (ProjectImage != null)
            {
                if (ProjectImage.Count > 0)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "USP_APP_UPDATE_PROJECT_COMPLETIONS";
                            cmd.Connection = con;

                            cmd.Parameters.AddWithValue("p_LeadId", lead_Id);
                            cmd.Parameters.AddWithValue("p_FundsRaised", FundsRaised);
                            cmd.Parameters.AddWithValue("p_Challenge", Regex.Replace(Challenge, "'", "`").Trim());
                            cmd.Parameters.AddWithValue("p_Learning", Regex.Replace(Learning, "'", "`").Trim());
                            cmd.Parameters.AddWithValue("p_AsAStory", Regex.Replace(AsAStory, "'", "`").Trim());
                            cmd.Parameters.AddWithValue("p_Resource", Regex.Replace(Resource, "'", "`").Trim());
                            cmd.Parameters.AddWithValue("p_ResourceAmount ", TotalResources);
                            cmd.Parameters.AddWithValue("p_PDId", Project_Id);
                            int iDBStatus = cmd.ExecuteNonQuery();
                            if (iDBStatus > 0)
                            {
                                status += "success";
                                foreach (byte[] bytess in ProjectImage)
                                {
                                    string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";
                                    using (var ms = new MemoryStream(bytess))
                                    {
                                        // byte[] bytes = Convert.FromBase64String(ProfileImage);
                                        var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                                        fs.Write(bytess);
                                        fs.Close();
                                        //  con.Open();
                                        //   MySqlCommand cmd = new MySqlCommand();
                                        cmd.Connection = con;
                                        cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("p_LeadId", leadId);
                                        cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                                        cmd.Parameters.AddWithValue("p_Document_Id", doccount);
                                        cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                                        cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
                                        cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                                        iSaved = cmd.ExecuteNonQuery();
                                        if (iSaved <= 0)
                                        {
                                            status += "Unable to Update Project Document";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                status += "Failed";
                            }


                        }

                    }
                    catch (Exception ex)
                    {
                        status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
                    }
                }
            }

            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.CommandText = "update project_description set ProjectStatus='RequestForCompletion' where PDId=" + ProjectId + "";
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
        }
        catch (Exception EX)
        {
            status = "Error" + EX.Message.ToString();
        }
        return status;
    }
    [WebMethod]
    public string ApplyTshirtRequested(string RequestedId, string TshirtModSize, int RegistrationId, string Lead_Id, int ManagerId,
  string MemberName, string TshirtSize, int RequestedCount, string ReapplyReson)
    {
        string status = "";
        // string Query = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                if (RequestedId != "")
                {
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "USP_EDIT_TSHIRTs";
                    cmd1.Connection = con;
                    cmd1.Parameters.AddWithValue("p_RequestedId", RequestedId);
                    cmd1.Parameters.AddWithValue("p_TshirtSize", Regex.Replace(TshirtModSize, "'", "`").Trim());
                    cmd1.Parameters.AddWithValue("p_ReapplyReson", ReapplyReson);
                    cmd1.Parameters.AddWithValue("p_RegistrationId", RegistrationId);
                    cmd1.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                    cmd1.Parameters.AddWithValue("p_ManagerId", ManagerId);
                    cmd1.Parameters.AddWithValue("p_MemberName", Regex.Replace(MemberName, "'", "`").Trim());
                    cmd1.Parameters.AddWithValue("p_RequestedCount", RequestedCount);

                    int iDBStatus1 = cmd1.ExecuteNonQuery();
                    if (iDBStatus1 > 0)
                    {
                        status += "success";

                        string gstrQstr = "select DeviceId,T.Status as Tshirtstatus from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + RequestedId.ToString() + "'";
                        MySqlCommand cmd = new MySqlCommand(gstrQstr, con);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Tshirtstatus"].ToString() == "Rejected")
                            {
                                FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), "T-shirt request submitted successfully", "studenttshirtrequested", "Empty");
                            }
                            else
                            {
                                FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), "T-shirt request modified", "studenttshirtrequested", "Empty");
                            }
                        }
                        gstrQstr = "select deviceid from user_device_details as UD " + " " +
                        "where username = (Select MobileNo from manager_details where ManagerID = " + ManagerId.ToString() + ")";
                        cmd = new MySqlCommand(gstrQstr, con);
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0]["DeviceId"].ToString(), Lead_Id + "-" + "Requested for Tshirt", "pmtshirt", "Empty");
                        }
                        SaveStudentLog(Lead_Id.ToString(), TshirtSize.ToString() + " " + "T-shirt request modified", "studenttshirtrequested ");


                    }
                    else
                    {
                        status += "Failed";
                    }
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_APPLY_TSHIRTS";
                    cmd.Connection = con;
                    // cmd.Parameters.AddWithValue("p_RequestedId", RequestedId);
                    cmd.Parameters.AddWithValue("p_RegistrationId", RegistrationId);
                    cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                    cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                    cmd.Parameters.AddWithValue("p_MemberName", Regex.Replace(MemberName, "'", "`").Trim());
                    cmd.Parameters.AddWithValue("p_TshirtSize", Regex.Replace(TshirtSize, "'", "`").Trim());
                    cmd.Parameters.AddWithValue("p_RequestedCount", RequestedCount);
                    cmd.Parameters.AddWithValue("p_ReapplyReson", ReapplyReson);
                    int iDBStatus = cmd.ExecuteNonQuery();
                    if (iDBStatus > 0)
                    {
                        status += "success";

                        string gstrQstr = "select DeviceId from user_device_details where Username='" + Lead_Id.ToString() + "'";
                        MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request submitted successfully", "studenttshirtrequested", "Empty");
                        }
                        gstrQstr = "select deviceid from user_device_details as UD " + " " +
                        "where username = (Select MobileNo from manager_details where ManagerID = " + ManagerId.ToString() + ")";
                        cmd = new MySqlCommand(gstrQstr, con);
                        da = new MySqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), Lead_Id + "-" + "Requested For Tshirt", "pmtshirt", "Empty");
                            }
                        }
                        SaveStudentLog(Lead_Id.ToString(), TshirtSize.ToString() + " " + "T-shirt request modified", "studenttshirtrequested ");
                    }
                    else
                    {
                        status += "Failed";
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
    [WebMethod]
    public string ApplyTshirtRequested1(string RequestedId, string TshirtModSize, int RegistrationId, string Lead_Id, int ManagerId,
     string MemberName, string TshirtSize, int RequestedCount, string ReapplyReson)
    {
        string status = "";
        // string Query = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                if (RequestedId != "")
                {
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "USP_EDIT_TSHIRT";
                    cmd1.Connection = con;
                    cmd1.Parameters.AddWithValue("p_RequestedId", RequestedId);
                    cmd1.Parameters.AddWithValue("p_TshirtSize", Regex.Replace(TshirtModSize, "'", "`").Trim());
                    cmd1.Parameters.AddWithValue("p_ReapplyReson", ReapplyReson);
                    int iDBStatus1 = cmd1.ExecuteNonQuery();
                    if (iDBStatus1 > 0)
                    {
                        status += "success";
                        string gstrQstr = "select DeviceId from user_device_details inner join student_tshirt_allotment as T on Username = T.Lead_Id where RequestedId = '" + RequestedId.ToString() + "'";
                        MySqlCommand cmd = new MySqlCommand(gstrQstr, con);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request modified", "studenttshirtrequested", "Empty");
                        }
                        SaveStudentLog(Lead_Id.ToString(), TshirtSize.ToString() + " " + ":T-shirt request modified", "studenttshirtrequested ");
                    }
                    else
                    {
                        status += "failed";
                    }

                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_APPLY_TSHIRTS";
                    cmd.Connection = con;
                    // cmd.Parameters.AddWithValue("p_RequestedId", RequestedId);
                    cmd.Parameters.AddWithValue("p_RegistrationId", RegistrationId);
                    cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                    cmd.Parameters.AddWithValue("p_ManagerId", ManagerId);
                    cmd.Parameters.AddWithValue("p_MemberName", Regex.Replace(MemberName, "'", "`").Trim());
                    cmd.Parameters.AddWithValue("p_TshirtSize", Regex.Replace(TshirtSize, "'", "`").Trim());
                    cmd.Parameters.AddWithValue("p_RequestedCount", RequestedCount);
                    cmd.Parameters.AddWithValue("p_ReapplyReson", ReapplyReson);
                    int iDBStatus = cmd.ExecuteNonQuery();
                    if (iDBStatus > 0)
                    {
                        status += "success";

                        string gstrQstr = "select DeviceId from user_device_details where Username='" + Lead_Id.ToString() + "'";
                        MySqlCommand cmd1 = new MySqlCommand(gstrQstr, con);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "T-shirt request submitted successfully", "studenttshirtrequested", "Empty");
                        }
                    }
                    else
                    {
                        status = "Failed";
                    }
                    SaveStudentLog(Lead_Id.ToString(), TshirtSize.ToString() + " " + ":T-shirt request submitted successfully", "studenttshirtrequested");
                }
            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
    }

    [WebMethod]
    public string UpdateProjectCompletionImg1(long RegistrationId, string leadId, long ProjectId, string ProfileImage, int doccount) //byte[] ProfileImage
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";
                            byte[] bytes = Convert.FromBase64String(ProfileImage);
                            var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                            fs.Write(bytes);
                            fs.Close();
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_LeadId", leadId);
                            cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                            cmd.Parameters.AddWithValue("p_Document_Id", doccount);
                            cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                            cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
                            cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                            iSaved = cmd.ExecuteNonQuery();
                            if (iSaved > 0)
                                status = "success";
                            else
                                status += "Unable to Update Project Completion Details";
                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
                    }
                }
                else
                {
                    status = "Select Image";
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
    public string UpdateProjectCompletionImgDraft(long RegistrationId, string leadId, long ProjectId, string ProfileImage, int doccount)
    {
        string status = "";
        int iSaved = 0;
        try
        {
            if (ProfileImage != null)
            {
                if (ProfileImage.Length > 0)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            string fileName = "~/Documents/" + leadId + "_" + ProjectId.ToString() + "_" + Guid.NewGuid().ToString() + ".jpg";
                            byte[] bytes = Convert.FromBase64String(ProfileImage);
                            var fs = new BinaryWriter(new FileStream(Server.MapPath(fileName), FileMode.Append, FileAccess.Write));
                            fs.Write(bytes);
                            fs.Close();
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "USP_APP_INSERT_PROJECT_DOCUMENTS";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_LeadId", leadId);
                            cmd.Parameters.AddWithValue("p_PDId", ProjectId);
                            cmd.Parameters.AddWithValue("p_Document_Id", doccount);
                            cmd.Parameters.AddWithValue("p_Document_Path", fileName);
                            cmd.Parameters.AddWithValue("p_Document_Type", "IMG");
                            cmd.Parameters.AddWithValue("p_Student_Id", RegistrationId);
                            iSaved = cmd.ExecuteNonQuery();
                            if (iSaved > 0)
                                status = "success";
                            else
                                status += "Unable to Update Project Completion Details";
                            MySqlCommand cmd1 = new MySqlCommand();
                            cmd1.CommandText = "update project_description set ProjectStatus='RequestForCompletion' where PDId=" + ProjectId;
                            cmd1.CommandType = CommandType.Text;
                            cmd1.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Project Completion Updated. But unable to save image Error: " + ex.Message.ToString();
                    }
                }
            }
        }
        catch (Exception EX)
        {
            status = "Error" + EX.Message.ToString();
        }
        return status;
    }

    [WebMethod]
    public vmStudentRequest_Head Get_Student_Request_Head(string Lead_Id)
    {
        vmStudentRequest_Head vmP = new vmStudentRequest_Head();
        List<Get_Student_Request_Project> Projects = new List<Get_Student_Request_Project>();
        List<Request_Heads> Head = new List<Request_Heads>();

        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPS_GET_STUDENT_REQUEST_HEAD";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Request_Heads obj = new Request_Heads();
                        if (dt.Rows[i]["slno"].ToString() != null && dt.Rows[i]["head_name"].ToString() != null)
                        {
                            obj.slno = dt.Rows[i]["slno"].ToString();
                            obj.Head_Name = dt.Rows[i]["head_name"].ToString();
                            obj.Status = "Sucess";
                            Head.Add(obj);
                        }
                    }
                    vmP.RequestHead = Head;

                    //Fetching members
                    DataTable dtProjects = new DataTable();
                    cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_GET_STUDENT_REQUEST_PROJECT_LIST";
                    cmd.Parameters.AddWithValue("P_LEAD_ID", Lead_Id);
                    cmd.Connection = con;
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(dtProjects);
                    if (dtProjects.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtProjects.Rows.Count; j++)
                        {
                            if (dtProjects.Rows[j]["pdid"].ToString() != null && dtProjects.Rows[j]["title"].ToString() != null)
                            {
                                Get_Student_Request_Project obj = new Get_Student_Request_Project();
                                obj.PDID = dtProjects.Rows[j]["pdid"].ToString();
                                obj.ProjectTitle = dtProjects.Rows[j]["title"].ToString();
                                obj.Status = "Sucess";
                                Projects.Add(obj);
                            }
                        }
                        vmP.Projects = Projects;
                    }
                    else
                    {
                        vmP.Status = "No project List";
                    }
                }
                else
                {
                    vmP.Status = "Heads Disabled";
                }
            }
        }
        catch (Exception)
        {
            vmP.Status = "Error";
        }
        return vmP;
    }

    [WebMethod]
    public string Student_Submit_Request_ToManager(string Lead_Id, string Request_type, string Priority, string PDID, string Request_message, string RequestedId)
    {
        string status = "";
        // string Query = "";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "USP_STUDENT_REQUEST_TOMANAGER";
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd1.Parameters.AddWithValue("p_Request_type", Request_type);
                cmd1.Parameters.AddWithValue("p_Priority", Priority);
                cmd1.Parameters.AddWithValue("p_PDID", PDID);
                cmd1.Parameters.AddWithValue("p_Request_message", Regex.Replace(Request_message, "'", "`").Trim());
                cmd1.Parameters.AddWithValue("p_RequestedId", RequestedId);

                int iDBStatus1 = cmd1.ExecuteNonQuery();
                if (iDBStatus1 > 0)
                {
                    status += "success";
                    string gstrQstr = "select DeviceId from user_device_details where  Username = '" + Lead_Id.ToString() + "' order by Id desc limit 1";
                    MySqlCommand cmd = new MySqlCommand(gstrQstr, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (RequestedId == "")
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Request Submited", "Student", "Empty");
                            SaveStudentLog(Lead_Id.ToString(), Request_message.ToString() + " " + ":Request Submited", "Request ");

                            gstrQstr = "select DeviceId,Username from user_device_details where " + " " +
                            "Username = (Select MobileNo from manager_details where ManagerId = (Select ManagerCode from Student_Registration where Lead_Id = '" + Lead_Id.ToString() + "'))";
                            cmd = new MySqlCommand(gstrQstr, con);
                            da = new MySqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                if (Request_type == "1")
                                {
                                    FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), Lead_Id + "-" + "Requested for Project  Approval Letter", "Manager", "Empty");
                                }
                                else
                                {
                                    FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), Lead_Id + "-" + "Requested , " + Request_message.ToString(), "Manager", "Empty");
                                }
                            }
                        }
                        else
                        {
                            FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), "Request Deleted Successfully", "Student", "Empty");
                            SaveStudentLog(Lead_Id.ToString(), RequestedId + "-" + Request_message.ToString() + " " + ":Request Deleted Successfully ", "Request ");

                            gstrQstr = "select DeviceId,Username from user_device_details where " + " " +
                            "Username = (Select MobileNo from manager_details where ManagerId = (Select ManagerCode from Student_Registration where Lead_Id = '" + Lead_Id.ToString() + "'))";
                            cmd = new MySqlCommand(gstrQstr, con);
                            da = new MySqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                FCMPushNotification.AndroidPush(dt.Rows[0].ItemArray[0].ToString(), Lead_Id + "-" + "Request Deleted From Student", "Manager", "Empty");

                            }
                        }
                        SaveStudentLog(Lead_Id.ToString(), "Student Requested for " + " " + Request_message, "Student Request ");
                    }
                }
                else
                {
                    status = "failed";
                }

            }
        }
        catch (Exception ex)
        {
            status = ex.Message.ToString();
        }
        return status;
    }

    [WebMethod]
    public List<vmStudentRequest_Head> Get_Student_Request_History(string Lead_Id)
    {
        List<vmStudentRequest_Head> RequestList = new List<vmStudentRequest_Head>();
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_STUDENT_REQUEST_HISTORY";
                cmd.Parameters.AddWithValue("p_Lead_Id", Lead_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RequestList.Add(new vmStudentRequest_Head
                        {
                            request_Id = dt.Rows[i]["request_Id"].ToString(),
                            Request_Message = dt.Rows[i]["Request_Message"].ToString(),
                            Request_Priority = dt.Rows[i]["Request_Priority"].ToString(),
                            Request_Date = dt.Rows[i]["Request_Date"].ToString(),
                            Response_Message = dt.Rows[i]["Response_Message"].ToString(),
                            Response_Date = dt.Rows[i]["Response_Date"].ToString(),
                            HeadId = dt.Rows[i]["Request_Head_Id"].ToString(),
                            HeadName = dt.Rows[i]["Head_Name"].ToString(),
                            Project_Id = dt.Rows[i]["PDID"].ToString(),
                            Project_Title = dt.Rows[i]["title"].ToString(),
                            Request_Status = dt.Rows[i]["Status"].ToString(),
                            Status = "Success"
                        });
                    }
                }
                else
                {
                    RequestList.Add(new vmStudentRequest_Head { Status = "No Request List" });
                }
            }
        }
        catch (Exception ex)
        {
            RequestList.Add(new vmStudentRequest_Head { Status = "Error" });
        }
        return RequestList;
    }

    [WebMethod]
    public string Delete_ProjectImg(string ImgDelete)
    {
        string status = "";
        try
        {

            if ((ImgDelete != null) || (ImgDelete != ""))
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(ImgDelete);
                if ((dict != null) || (dict.Count != 0))
                {
                    foreach (var item in dict)
                    {
                        using (MySqlConnection con = new MySqlConnection(connection))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.CommandText = "SET SQL_SAFE_UPDATES=0; delete from project_digital_documents where slno=" + item.Value + "";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                                status = "Image Deleted";

                            if ((System.IO.File.Exists(Server.MapPath(Regex.Replace(item.Key, "'", "`").Trim().ToString()))))
                            {
                                System.IO.File.Delete(Server.MapPath(Regex.Replace(item.Key, "'", "`").Trim().ToString()));
                            }
                            else
                            {
                                status = "Image is not in Server";
                            }
                        }
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
    public vmProjectCompletion GetProjectCompletedByManager(long projectId)
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
                cmd.CommandText = "USP_GETPROJECTCOMPLETED_BYMANAGER";
                cmd.Parameters.AddWithValue("p_Id", projectId);
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
                        vmproj.CompletionProgress = int.Parse(dt.Rows[i]["CompletionProgress"].ToString());
                        vmproj.ProjectStatus = dt.Rows[i]["ProjectStatus"].ToString();
                        vmproj.Status = "Success";
                        vmproj.HoursSpend = int.Parse(dt.Rows[i]["HoursSpend"].ToString());
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
                        vmproj.Rating = int.Parse(dt.Rows[i]["Rating"].ToString());
                        vmproj.Project_Level = dt.Rows[i]["Project_Levels"].ToString();
                        vmproj.ManagerComments = dt.Rows[i]["ManagerComments"].ToString();
                        vmproj.isImpactProject = int.Parse(dt.Rows[i]["IsImpactProject"].ToString());
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
        catch (Exception ex)
        {
            vmproj.Status = "Error" + ex.Message.ToString();
        }
        return vmproj;
    }


    #region Phase 4 Payment Gateway

    [WebMethod]
    public List<vmPayment_Details> Save_Student_PaymentDetails(int Fees_Category_ID, int Programe_ID, int Registration_ID, int Paid_fees, string Paid_date, string Payment_Type, string Payeer_Id, string Transaction_ID, string Reference_ID, string Transaction_Status, string Created_User_Type, int User_Id, int Fees_ID)
    {
        List<vmPayment_Details> paymentdetails = new List<vmPayment_Details>();
        try
        {
            string status = "";
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_APP_STUDENT_PAY_AMOUNT_DETAILS";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_Fees_Category_ID", Fees_Category_ID);
                cmd.Parameters.AddWithValue("p_Fees_Id", Fees_ID);
                cmd.Parameters.AddWithValue("p_Programe_ID", Programe_ID);
                cmd.Parameters.AddWithValue("p_Registration_ID", Registration_ID);
                cmd.Parameters.AddWithValue("p_Paid_fees", Paid_fees);
                cmd.Parameters.AddWithValue("p_Paid_date", Paid_date);
                cmd.Parameters.AddWithValue("p_Payment_Type", Payment_Type);
                cmd.Parameters.AddWithValue("p_Transaction_ID", Transaction_ID);
                cmd.Parameters.AddWithValue("p_Reference_ID", Reference_ID);
                cmd.Parameters.AddWithValue("p_Transaction_Status", Transaction_Status);
                cmd.Parameters.AddWithValue("p_Created_User_Type", Created_User_Type);
                cmd.Parameters.AddWithValue("p_Payeer_Id", Payeer_Id);
                cmd.Parameters.AddWithValue("p_User_Id", User_Id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    status += "success";


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int payment_id = int.Parse(dt.Rows[i]["payment_id"].ToString());
                        string Lead_Id = dt.Rows[i]["Lead_Id"].ToString();
                        int Auto_Receipt_No = int.Parse(dt.Rows[i]["Auto_Receipt_No"].ToString());
                        string College_Name = dt.Rows[i]["College_Name"].ToString();
                        string Created_Date = dt.Rows[i]["Created_Date"].ToString();
                        string StudentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt.Rows[i]["StudentName"].ToString().ToLower());
                        string Fees_Category_description = dt.Rows[i]["Fees_Category_description"].ToString();
                        string Student_MailId = dt.Rows[i]["MailId"].ToString();
                        string Pay_Mode = "Online";
                        Generate_Receipt(Registration_ID, con);
                        //Generate_Receipt(payment_id, Lead_Id, Auto_Receipt_No, Fees_Category_description, Paid_fees, Created_Date, StudentName, College_Name);

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
                    status += "Payment detail failed to add";
                }
            }
        }
        catch (Exception ex)
        {
            paymentdetails.Add(new vmPayment_Details
            {
                Status = "Error"
            });
        }
        return paymentdetails;
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

    #endregion
}
