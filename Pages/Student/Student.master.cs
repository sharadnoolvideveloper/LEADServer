using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_Student : System.Web.UI.MasterPage
{


    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                vmCookies cook = new vmCookies();

                if (cook.LeadId() != "")
                {
                   
                    LeadBL BLobj = new LeadBL();
                    string LeadId = cook.LeadId();

                    BLobj.Student_FillSuggestionFeedbackHeads(ddlSuggestionFeedbackHeads);

                    //BLobj.Student_SetStudentProfileImage(LeadId, ProfilePic);
                    //string ManagerId = BLobj.Student_GetManagerIdAfterProfileUpdate(LeadId.ToString());
                    //BLobj.Student_GetManagerDetails(ManagerId.ToString(), lblLeftUserName, lblLeftMailid, lblMobileNo, imgLeftAvatar, btnFacebook, btnTwitter, btnInstaGram, btnWhataApp);

                    //if (btnFacebook.InnerText == "")
                    //{
                    //    Facebook.Visible = false;
                    //}
                    //else
                    //{
                    //    Facebook.Visible = true;
                    //}
                    //if (btnTwitter.InnerText == "")
                    //{
                    //    Twitter.Visible = false;
                    //}
                    //else
                    //{
                    //    Twitter.Visible = true;
                    //}
                    //if (btnInstaGram.InnerText == "")
                    //{
                    //    InstaGram.Visible = false;
                    //}
                    //else
                    //{
                    //    InstaGram.Visible = true;
                    //}
                    //if (btnWhataApp.InnerText == "")
                    //{
                    //    WhatsApp.Visible = false;
                    //}
                    //else
                    //{
                    //    WhatsApp.Visible = true;
                    //}
                }
                else
                {
                    Response.Redirect("~/Default.aspx?SessionOut=true");
                }

            }

            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }
        }
        catch (Exception)
        {

        }

    }
   

    protected void btnLeftLogout_Click(object sender, EventArgs e)
    {
        try
        {
            vmCookies cook = new vmCookies();
            BLobj.Student_Update_Login_Log(cook.LeadId());
            Request.Cookies.Remove("Registration_Id");
            Request.Cookies.Remove("Student_LeadId");
            Request.Cookies.Remove("Student_ManagerId");
            Request.Cookies.Remove("Student_MobileNo");
            Request.Cookies.Remove("Student_AcademicYear");
            Request.Cookies.Remove("Student_isProfileCompleted");
           
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception)
        {

        }
    }

    protected void btnTopLogout_Click(object sender, EventArgs e)
    {
        try
        {
            vmCookies cook = new vmCookies();
            BLobj.Student_Update_Login_Log(cook.LeadId());
            
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("Registration_Id", ""));
            Response.Cookies.Add(new HttpCookie("Student_LeadId", ""));
            Response.Cookies.Add(new HttpCookie("Student_ManagerId", ""));
            Response.Cookies.Add(new HttpCookie("Student_MobileNo", ""));
            Response.Cookies.Add(new HttpCookie("Student_AcademicYear", ""));
            Response.Cookies.Add(new HttpCookie("Student_isProfileCompleted", ""));
           
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception)
        {

        }
    }




    protected void btnSuggestionFeedback_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_SuggestionFeedback();", true);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSendSuggestionFeedback_Click(object sender, EventArgs e)
    {
        try
        {
            int Status = 0;
            vmCookies cook = new vmCookies();
           Status= BLobj.Student_SubmitSuggestionFeedback(cook.LeadId(), cook.ManagerId(), ddlSuggestionFeedbackHeads.SelectedValue.ToString(), Regex.Replace(txtFeedBack.Text.ToString(), "'", "`").Trim() , Regex.Replace(txtSuggestion.Text.ToString(), "'", "`").Trim());
            if (Status == 1)
            {
                string msg = "Suggestion/Feedback Successfully Submited. ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                txtFeedBack.Text = "";
                txtSuggestion.Text = "";
                ddlSuggestionFeedbackHeads.SelectedIndex = 0;
            }
            else
            {
                string msg = "Not Submited try Again ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
           
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + ex.Message.ToString() + "')", true);

        }
    }
}


