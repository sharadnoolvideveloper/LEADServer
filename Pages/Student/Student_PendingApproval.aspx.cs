using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_Student_PendingApproval : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();

    //private static int SumofAmount = 0;
    //private static string PDID = "";
    //private static string ProjectStatus= "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!Page.IsPostBack)
            {           
                   
                if ((Request.QueryString["PDID"].ToString() != null)||(Request.QueryString["PDID"].ToString() != "")
                    && (Request.QueryString["ProjectStatus"].ToString() != null)|| (Request.QueryString["ProjectStatus"].ToString()) != "")
                {
                    BLobj.FillThemeMaster(ddlProjectType);
                    PDID.Value =Request.QueryString["PDID"].ToString();
                    ProjectStatus.Value = Request.QueryString["ProjectStatus"].ToString();
                    BLobj.Student_GetProposedProjectdetailForEdit(cook.LeadId(), PDID.Value.ToString(), txtProjectTitle, txtTotalBeneficiaries, txtProjectObjectives, txtProjectPlan, txtManagerComments, rptMeterial, rptTeamMembers, ProjectStatus.Value.ToString(), lblTotalAmount, ddlProjectType, txtProposedBeneficiaries, txtProposalPlaceofImplementation, txtCurrentSituation, txtProposedStartDate, txtProposedEndDate, lblProposedProjectTargetDays);

                   
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
       
    }


      List<Meterials> dataList = new List<Meterials>();
    protected void btnAddMeterial_Click(object sender, EventArgs e)
    {
        try
        {

            int sumofmeterial = 0;
    
            //-- add all existing values to a list
            foreach (RepeaterItem item in rptMeterial.Items)
            {
                dataList.Add(new Meterials()
                {
                    MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text,
                    MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text,
                    Slno = (item.FindControl("lblSlno") as Label).Text,
                    SumOfMeterial = int.Parse((item.FindControl("txtMeterialCost") as TextBox).Text)
                });

                sumofmeterial = sumofmeterial+int.Parse((item.FindControl("txtMeterialCost") as TextBox).Text);
            }
            lblTotalAmount.Text = sumofmeterial.ToString();
         
            //-- add a blank row to list to show a new row added
            dataList.Add(new Meterials());

            if (dataList.Select(o => new
            {
                o.MeterialName
            }).Distinct().Count() != dataList.Count())
            {
                string msg = "Remove Duplicate Meterial name";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

            //-- bind repeater
            rptMeterial.DataSource = dataList;
            rptMeterial.DataBind();
        }
        catch (Exception)
        {
           
        }
    }
    protected void btnRemoveRow_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton button = (sender as ImageButton);
            foreach (RepeaterItem item in rptMeterial.Items)
            {
                dataList.Add(new Meterials()
                {
                    MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text,
                    MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text,
                    Slno = (item.FindControl("lblSlno") as Label).Text,
                });
               
            }
           
            int count = rptMeterial.Items.Count;

            //int listcount = dataList.Count();

            RepeaterItem item1 = button.NamingContainer as RepeaterItem;

            //Get the repeater item index
            int index = item1.ItemIndex;
            if((item1.FindControl("txtMeterialCost") as TextBox).Text != "")
            {
                lblTotalAmount.Text = (int.Parse(lblTotalAmount.Text) - int.Parse((item1.FindControl("txtMeterialCost") as TextBox).Text)).ToString();
            }
           

            dataList.RemoveAt(index);
            rptMeterial.DataSource = dataList;
            rptMeterial.DataBind();
            dataList.Clear();
        }
        catch (Exception)
        {
           
        }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rptMeterial.Items)
            {
                dataList.Add(new Meterials()
                {
                    MeterialName = (item.FindControl("txtMeterialName") as TextBox).Text,
                    MeterialCost = (item.FindControl("txtMeterialCost") as TextBox).Text,
                    Slno = (item.FindControl("lblSlno") as Label).Text,

                });
                lblTotalAmount.Text = (int.Parse(lblTotalAmount.Text) - int.Parse((item.FindControl("txtMeterialCost") as TextBox).Text)).ToString();
            }
           
            int count = rptMeterial.Items.Count;

            //int listcount = dataList.Count();
            dataList.RemoveAt(count - 1);
            rptMeterial.DataSource = dataList;
            rptMeterial.DataBind();
            dataList.Clear();
        }
        catch (Exception)
        {
          
        }
    }

    List<TeamMembers> dataTeam = new List<TeamMembers>();
    protected void btnAddTeamMembers_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rptTeamMembers.Items)
            {
                dataTeam.Add(new TeamMembers()
                {
                    MemberName = (item.FindControl("txtName") as TextBox).Text,
                    MemberMailId = (item.FindControl("txtMailId") as TextBox).Text,
                    MemberMobileNo = (item.FindControl("txtMobileNo") as TextBox).Text,
                    Slno = (item.FindControl("lblSlno") as Label).Text,
                });
            }
            //-- add a blank row to list to show a new row added
            dataTeam.Add(new TeamMembers());

            //-- bind repeater
            rptTeamMembers.DataSource = dataTeam;
            rptTeamMembers.DataBind();

        }
        catch (Exception)
        {
           
        }
    }

    protected void btnRemoveTeamMembers_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton button = (sender as ImageButton);
            foreach (RepeaterItem item in rptTeamMembers.Items)
            {
                dataTeam.Add(new TeamMembers()
                {
                    MemberName = (item.FindControl("txtName") as TextBox).Text,
                    MemberMailId = (item.FindControl("txtMailId") as TextBox).Text,
                    MemberMobileNo = (item.FindControl("txtMobileNo") as TextBox).Text,
                    Slno = (item.FindControl("lblSlno") as Label).Text,
                });
            }

            RepeaterItem item1 = button.NamingContainer as RepeaterItem;
            int index = item1.ItemIndex;
            dataTeam.RemoveAt(index);
            rptTeamMembers.DataSource = dataTeam;
            rptTeamMembers.DataBind();
            dataTeam.Clear();
        }
        catch (Exception)
        {
            
        }
    }
    protected void btnSaveProposal_Click(object sender, EventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {

                string RegistrationId = cook.RegistrationId();
                string ManagerId = cook.ManagerId();
                string LeadId = cook.LeadId();
                 string AcademicCode = cook.AcademicYear();
                string ProposedDate = System.DateTime.Now.ToString("dd-MM-yyyy");

                if (BLobj.ValidateMeterial(rptMeterial) == false)
                {
                    BLobj.Student_EditProjectProposal(LeadId.ToString(), PDID.Value.ToString(), ManagerId.ToString(), txtProjectTitle.Text.ToString().Trim(), txtTotalBeneficiaries.Text, txtProjectObjectives.Text.ToString().Trim(), txtProjectPlan.Text.ToString(), ProposedDate.ToString(), AcademicCode.ToString(), ddlProjectType.SelectedValue.ToString(), ProjectStatus.Value.ToString(), txtProposedBeneficiaries.Text.ToString(), txtProposalPlaceofImplementation.Text.ToString(), txtCurrentSituation.Text.ToString(), txtProposedStartDate.Text.ToString(), txtProposedEndDate.Text.ToString());

                    //if (rptMeterial.Items.Count > 0)
                    //{
                    BLobj.Student_SaveProjectMeterials(LeadId.ToString(), PDID.Value.ToString(), rptMeterial, "Edit");
                    // }
                    //   if (rptTeamMembers.Items.Count > 0)
                    //   {
                    BLobj.Student_SaveProjectTeamMembers(LeadId.ToString(), PDID.Value.ToString(), rptTeamMembers, "Edit");
                    //    }

                    //string msgMail = lblLeadId.Text.ToString() + " " + "Your project is proposed successfully " + " Name is : " + lblStudentName.Text.ToString() +
                    //"Title: " + txtProjectTitle.Text.ToString();
                    //DataTable dt = BLobj.Student_GetStudentDetailForMailSend(cook.LeadId());
                    //string Mailid = dt.Rows[0].ItemArray[0].ToString();
                    //if (Mailid.ToString() != "")
                    //{
                    //    string body = PopulateBody(lblStudentName.Text.ToString(),
                    //    " <b>Your project is proposed successfully</b>", "The details you entered are listed below: ", "<ol><li><b>LEAD Id:</b> " + lblLeadId.Text.Trim() + "<br /><br /></li><li><b>Name:</b> " + lblStudentName.Text.Trim() + "<br /><br /></li><li><b>Mobile No.:</b> " + "" +
                    //    "<br /><br /></li><li><b>Institution:</b> " + lblCollegeName.Text.ToString() + "<br /><br /></li><li><b>Title of the Project:</b> " + txtProjectTitle.Text.Trim() + "<br /><br /></li><li><b>Beneficieries:</b> " + txtTotalBeneficiaries.Text.ToString() + "<br /><br /></li><li><b>Theme:</b> " + ddlProjectType.SelectedItem.Text.ToString() + "<br /><br /></li><li><b>Objectives:</b> " + txtProjectObjectives.Text.Trim() + "<br /><br /></li><li><b>Action Plan:</b> " + txtProjectPlan.Text.Trim() + "</li></ol><br /><br />");
                    //    SendHtmlFormattedEmail(Mailid.ToString(), "Project Proposal Details", body);
                    //  }
                    // SendEmailProposed();

                    string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your project" + " " + txtProjectTitle.Text.ToString() + " is Updated successfully", "Student", "");
                    }
                    Response.Redirect("StudentProfile.aspx");
                }
                else
                {
                    string msg = "Remove Duplicate Meterial name";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionOut=True");
            }
        }
        catch (Exception)
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
    protected void SendEmailProposed()
    {

        try
        {
            DataTable dt = BLobj.Student_GetStudentDetailForMailSend(cook.LeadId());
            string Mailid = dt.Rows[0].ItemArray[0].ToString();
            if (Mailid.ToString() != "")
            {
                //string body = PopulateBody(lblStudentName.Text.ToString(),
                //   " <b>Your project is proposed successfully</b>", "The details you entered are listed below: ", "<ol><li><b>LEAD Id:</b> " + lblLeadId.Text.Trim() + "<br /><br /></li><li><b>Name:</b> " + lblStudentName.Text.Trim() + "<br /><br /></li><li><b>Mobile No.:</b> " + "" +
                //   "<br /><br /></li><li><b>Institution:</b> " + lblCollegeName.Text.ToString() + "<br /><br /></li><li><b>Title of the Project:</b> " + txtProjectTitle.Text.Trim() + "<br /><br /></li><li><b>Beneficieries:</b> " + txtTotalBeneficiaries.Text.ToString() + "<br /><br /></li><li><b>Theme:</b> " + ddlProjectType.SelectedItem.Text.ToString() + "<br /><br /></li><li><b>Objectives:</b> " + txtProjectObjectives.Text.Trim() + "<br /><br /></li><li><b>Action Plan:</b> " + txtProjectPlan.Text.Trim() + "</li></ol><br /><br />");
                //SendHtmlFormattedEmail(Mailid.ToString(), "Project Proposal Details", body);
            }
            else
            {
                // LogFile.CreateLog((Session["userInfo"] as User_Info).UserName, "Send Mail after Proposal(Student)", "Student Email Address is not present");
            }
        }
        catch (Exception ex)
        {
            BLobj.SendMailException("send_sms", ex.ToString(), "StudentProfile.aspx Proposal Submit Mail", cook.LeadId(), cook.Student_MobileNo());
        }
    }
    protected void rptTeamMembers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DropDownList ddlGender = (e.Item.FindControl("ddlGender") as DropDownList);
                Label slno = (e.Item.FindControl("lblSlno") as Label);
                string Gender = BLobj.Student_GetProjectMembersGender(slno.Text.ToString());
                ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(Gender.ToString()));
            }
        }
        catch (Exception)
        {

        }
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
            // mailMessage.Bcc.Add(new MailAddress("leadleadmis@dfmail.org"));

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
        public int SumOfMeterial
        {
            get; set;
        }

    }
    public class TeamMembers
    {
        public string MemberName
        {
            get; set;
        }
        public string MemberMailId
        {
            get; set;
        }
        public string MemberMobileNo
        {
            get; set;
        }
        public string Gender
        {
            get; set;
        }
        public string Slno
        {
            get; set;
        }
    }





    protected void txtMeterialCost_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int sum = 0;
            for (int index = 0; index < this.rptMeterial.Items.Count; index++)
            {
                TextBox textBox = this.rptMeterial.Items[index].FindControl("txtMeterialCost") as TextBox;
                sum += textBox.Text.Length > 0 ? int.Parse(textBox.Text) : 0;
            }
       lblTotalAmount.Text = sum.ToString();

        }
        catch (Exception ex)
        {

            throw;
        }
    }
}