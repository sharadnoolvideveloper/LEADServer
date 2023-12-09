using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_College_College_Dashboard : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    Reports rpt = new Reports();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!Page.IsPostBack)
            {
                if(cook.CollegeLogin_Id()!=null)
                {
                    DataTable dt = new DataTable();
                    dt = BLobj.CollegeLogin_MentorDetails(cook.CollegeLogin_Id());
                    if (dt.Rows.Count > 0)
                    {
                        lblMailId.Text = dt.Rows[0].ItemArray[1].ToString();
                        lblManagerName.Text = dt.Rows[0].ItemArray[4].ToString();
                        lblManagerMobileNo.Text = dt.Rows[0].ItemArray[5].ToString();
                        lblManagerEmailId.Text = dt.Rows[0].ItemArray[6].ToString();
                        imgCollegeMentor.ImageUrl = "http://mis.leadcampus.org/" + dt.Rows[0].ItemArray[7].ToString();

                        if (dt.Rows[0].ItemArray[8].ToString() == "")
                        {
                            Facebook.Visible = false;
                        }
                        else
                        {
                            Facebook.Visible = true;

                            btnFacebook.HRef = dt.Rows[0].ItemArray[8].ToString();

                            btnFacebook.InnerText = dt.Rows[0].ItemArray[8].ToString();
                        }
                        if (dt.Rows[0].ItemArray[9].ToString() == "")
                        {

                            Twitter.Visible = false;
                        }
                        else
                        {
                            Twitter.Visible = true;

                            btnTwitter.HRef = dt.Rows[0].ItemArray[9].ToString();
                            btnTwitter.InnerText = dt.Rows[0].ItemArray[9].ToString();

                        }



                        if (dt.Rows[0].ItemArray[10].ToString() == "")
                        {
                            InstaGram.Visible = false;
                        }
                        else
                        {
                            InstaGram.Visible = true;

                            btnInstaGram.HRef = dt.Rows[0].ItemArray[10].ToString();
                            btnInstaGram.InnerText = dt.Rows[0].ItemArray[10].ToString();
                        }



                        if (dt.Rows[0].ItemArray[11].ToString() == "")
                        {
                            WhataApp.Visible = false;
                        }
                        else
                        {
                            WhataApp.Visible = true;

                            btnWhatsApp.HRef = "https://web.whatsapp.com/send?phone=91" + dt.Rows[0].ItemArray[11].ToString();
                            btnWhatsApp.InnerText = dt.Rows[0].ItemArray[11].ToString();
                        }

                    }

                    BLobj.FillAademicYear(ddlAcademicCode);
                    ddlAcademicCode.SelectedIndex = 1;
                    SummeryReport(ddlAcademicCode);

                    tabSummary.Attributes.Add("class", "active");
                    SummaryTab.Attributes.Add("class", "tab-pane active");
                }
                else
                {
                    Response.Redirect("~/Default.aspx?Logout=SessionOut");
                }
              
            }
        }
        catch (Exception)
        {}
    }

    protected void btnSummaryCount_Click(object sender, EventArgs e)
    {
        try
        {
            tabSummary.Attributes.Add("class", "active");
            SummaryTab.Attributes.Add("class", "tab-pane active");

            tabFiveStarProject.Attributes.Add("class", "");
            FiveStarProjectTab.Attributes.Add("class", "hidden");

            tabEvent.Attributes.Add("class", "");
            EventTab.Attributes.Add("class", "hidden");



        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnFiveStarProject_Click(object sender, EventArgs e)
    {
        try
        {
            tabFiveStarProject.Attributes.Add("class", "active");
            FiveStarProjectTab.Attributes.Add("class", "tab-pane active");

            tabSummary.Attributes.Add("class", "");
            SummaryTab.Attributes.Add("class", "hidden");

            tabEvent.Attributes.Add("class", "");
            EventTab.Attributes.Add("class", "hidden");



        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void SummeryReport(DropDownList ddlAcademicCode)
    {
        string FromDate = "";
        string ToDate = "";
        BLobj.GetDatesFromAcademicYear(ddlAcademicCode.SelectedValue.ToString(), out FromDate, out ToDate);
        string ManagerId = cook.Manager_Id().ToString();
        DataTable dt = rpt.College_GetMaleFemaleCount(cook.CollegeLogin_Id(), ddlAcademicCode.SelectedValue.ToString());
        lblTotalCount.Text = dt.Rows[0].ItemArray[0].ToString();

        dt = rpt.College_GetProjectStatusWiseProjectCount(cook.CollegeLogin_Id(), ddlAcademicCode.SelectedValue.ToString());
        lblTotalProposed.Text = dt.Rows[0].ItemArray[0].ToString();
        lblTotalApproved.Text = dt.Rows[0].ItemArray[1].ToString();
        lblTotalCompleted.Text = dt.Rows[0].ItemArray[2].ToString();
        lblTotalRequestForModification.Text = dt.Rows[0].ItemArray[3].ToString();
        lblTotalRequestForCompletion.Text = dt.Rows[0].ItemArray[4].ToString();
        lblTotalRejected.Text = dt.Rows[0].ItemArray[5].ToString();
        lbltotalProjects.Text = dt.Rows[0].ItemArray[6].ToString();

        //lblMale.Text = dt.Rows[0].ItemArray[1].ToString();
        //lblFemale.Text = dt.Rows[0].ItemArray[2].ToString();
        dt = rpt.College_GetRequestedAndSanctionAmount(cook.CollegeLogin_Id(), ddlAcademicCode.SelectedValue.ToString());
        lblRequestedAmount.Text = dt.Rows[0].ItemArray[0].ToString();
        lblSanctionAmount.Text = dt.Rows[0].ItemArray[1].ToString();
        

        dt = rpt.College_GetReleaseAmount(cook.CollegeLogin_Id(), FromDate.ToString(), ToDate.ToString());
        lblReleaseAmount.Text = dt.Rows[0].ItemArray[0].ToString();
       
        if (long.Parse(lblSanctionAmount.Text.ToString()) >= long.Parse(lblReleaseAmount.Text.ToString()))
         {
            lblBalanceAmount.Text = Convert.ToString(long.Parse(lblSanctionAmount.Text.ToString()) - long.Parse(lblReleaseAmount.Text.ToString()));
        }
        else
        {
            lblBalanceAmount.Text = "0";
        }
            
        

    }

    protected void ddlAcademicCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SummeryReport(ddlAcademicCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnTopLogOut_Click(object sender, EventArgs e)
    {
        try
        {
            vmCookies cook = new vmCookies();
            //BLobj.Student_Update_Login_Log(cook.CollegeLogin_Id());

            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("CollegeLogin_Id", ""));
            Response.Redirect("~/Default.aspx?Logout=Successfully");
        }
        catch (Exception)
        {

            throw;
        }
    }
}