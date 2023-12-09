using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.Helpers;
public partial class Pages_Student_StudentProfile : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    int clickcount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                if (cook.LeadId() != "")
                {

                    DataTable dtCrown = BLobj.Student_GetFiveStarForCrown(cook.LeadId());
                    if (dtCrown.Rows.Count > 0)
                    {

                        if (int.Parse(dtCrown.Rows[0].ItemArray[0].ToString()) > 0)
                        {
                            ImgBatch.Visible = true;
                            lblFiveStartsCount.Visible = true;
                            lblFiveStartsCount.Text = dtCrown.Rows[0].ItemArray[0].ToString();
                        }
                    }
                    BLobj.Student_GetLeftProfileDetails(cook.LeadId(), lblStudentName, lblCollegeName, lblLeadId, lblStudentType, lblRegistrationDate);
                    string ManagerId = BLobj.Student_GetManagerIdAfterProfileUpdate(cook.LeadId());
                    BLobj.Student_GetManagerDetails(ManagerId.ToString(), lblManagerName, lblManagerEmailId, lblManagerMobileNo, imgManagerDetailInStudentProfile, btnFacebook, btnTwitter, btnInstaGram, btnWhataApp);
                    if (btnFacebook.InnerText == "")
                    {
                        Facebook.Visible = false;
                    }
                    else
                    {
                        Facebook.Visible = true;
                    }
                    if (btnTwitter.InnerText == "")
                    {
                        Twitter.Visible = false;
                    }
                    else
                    {
                        Twitter.Visible = true;
                    }
                    if (btnInstaGram.InnerText == "")
                    {
                        InstaGram.Visible = false;
                    }
                    else
                    {
                        InstaGram.Visible = true;
                    }
                    if (btnWhataApp.InnerText == "")
                    {
                        WhataApp.Visible = false;
                    }
                    else
                    {
                        WhataApp.Visible = true;
                    }
                    BLobj.FillStateMaster(ddlState);
                    BLobj.Student_FillProgramme(ddlProgramme);
                    BLobj.Student_SetStudentProfileImage(cook.LeadId(), PreviewImage);
                    int isProfileEdit = BLobj.CheckProfileisEditedMode(cook.LeadId());

                    Student_Apply_NextLevel();
                    StudentLevels();

                    if ((isProfileEdit == 0) || (isProfileEdit == 2))
                    {
                        tabprofile.Attributes.Add("class", "active");
                        profiletab.Attributes.Add("class", "tab-pane active");

                        tabProject.Attributes.Add("class", "hidden");
                        ProjectTab.Attributes.Add("class", "hidden");

                        tabEvent.Attributes.Add("class", "");
                        EventTab.Attributes.Add("class", "hidden");

                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");




                        lblStudentType.Text = "Student";
                        txtLeadId.Text = cook.LeadId();
                        txtMobileNo.Text = cook.Student_MobileNo();
                        BLobj.Student_Year(ddlYear);
                        getStudentProfileDetails(cook.LeadId());
                        // pnlRequest.Visible = true;
                    }
                    else
                    {
                        tabProject.Attributes.Add("class", "active");
                        ProjectTab.Attributes.Add("class", "tab-pane active");

                        tabprofile.Attributes.Add("class", "");
                        profiletab.Attributes.Add("class", "hidden");

                        tabEvent.Attributes.Add("class", "");
                        EventTab.Attributes.Add("class", "hidden");




                        BLobj.Student_Year(ddlYear);
                        BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptStudentProposedList);
                        // pnlRequest.Visible = false;
                    }

                    //   StudentProgressBar();
                    DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                    if (dt2.Rows.Count > 0)
                    {
                        if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                            btnTshirt.Visible = true;
                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");
                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                            btnTshirt.Visible = true;
                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");
                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                        {
                            tabTshirt.Attributes.Add("class", "");
                            TshirtTab.Attributes.Add("class", "hidden");
                            btnTshirt.Visible = false;
                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "2"))
                        {

                            tabTshirt.Attributes.Add("class", "");
                            TshirtTab.Attributes.Add("class", "hidden");
                            btnTshirt.Visible = false;

                        }
                        else
                        {
                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");
                            btnTshirt.Visible = false;
                        }
                    }
                    else
                    {
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx?SessionOut=True");
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Default.aspx?SessionOut=True");
        }

    }
    public void Student_Apply_NextLevel()
    {
        int CompletedProjectCount = 0, TotalMonths = 0, TotalMasterLeaderMonths = 0, IsApplyForMasterLeader = 0, IsApplyForLeadAmbassador = 0,
            ImpactProjectCount = 0;
        string StudenType = "";

        DataTable dt = new DataTable();
        dt = BLobj.Student_GetMasterLeaderLevelUpdate(cook.LeadId());
        CompletedProjectCount = int.Parse(dt.Rows[0].ItemArray[0].ToString());
        TotalMonths = int.Parse(dt.Rows[0].ItemArray[1].ToString());
        StudenType = dt.Rows[0].ItemArray[2].ToString();
        TotalMasterLeaderMonths = int.Parse(dt.Rows[0].ItemArray[3].ToString());
        IsApplyForMasterLeader = int.Parse(dt.Rows[0].ItemArray[4].ToString());
        IsApplyForLeadAmbassador = int.Parse(dt.Rows[0].ItemArray[5].ToString());
        ImpactProjectCount = int.Parse(dt.Rows[0].ItemArray[6].ToString());

        if ((CompletedProjectCount == 0) && (StudenType == "Student"))
        {
            pnlVisibility(true, false, false, false);
        }
        else if ((CompletedProjectCount >= 3) && (TotalMonths >= 6) && ((StudenType.ToString() != "Lead Ambassador") || (ImpactProjectCount >= 1)))
        {
            if (IsApplyForMasterLeader == 0)
            {
                pnlVisibility(false, true, false, false);
            }
            else
            {
                pnlVisibility(false, false, false, false);
            }
        }
        else if ((TotalMasterLeaderMonths >= 6) && ((StudenType == "Master Leader") || (StudenType == "LEADer")))
        {
            if (IsApplyForLeadAmbassador == 0)
            {
                pnlVisibility(false, false, true, false);
            }
            else
            {
                pnlVisibility(false, false, false, false);
            }
        }
        else
        {
            pnlVisibility(false, false, false, false);
        }
    }

    public void StudentLevels()
    {
        bool Student = true;
        bool Initiator = false;
        bool ChangeMaker = false;
        bool LEADer = false;
        bool MasterLeader = false;
        bool LeadAmbassador = false;
        string TempType = "";

        DataTable dt = BLobj.Student_GetStudentLevels(cook.LeadId());
        if (dt.Rows.Count >= 1)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TempType = dr[0].ToString();

                if (TempType == "Initiator")
                {
                    Initiator = true;
                }
                else if (TempType == "Change Maker")
                {
                    ChangeMaker = true;
                }
                else if (TempType == "LEADer")
                {
                    LEADer = true;
                }
                else if (TempType == "Master Leader")
                {
                    MasterLeader = true;
                }
                else if (TempType == "Lead Ambassador")
                {
                    LeadAmbassador = true;
                }
                else
                {
                    Student = true;
                }

            }
        }
        else
        {
            Student = true;
        }
        if (LeadAmbassador == true)
        {

            lblStudentType.Text = "Lead Ambassador";
        }
        else if ((MasterLeader == true) && (LEADer == true))
        {

            lblStudentType.Text = "Master Leader";
        }
        else if ((MasterLeader == true) && (LEADer == false))
        {

            lblStudentType.Text = "Master Leader";
        }
        else if ((LEADer == true) && (MasterLeader == false))
        {

            lblStudentType.Text = "LEADer";
        }
        else if (ChangeMaker == true)
        {

            lblStudentType.Text = "Change Maker";
        }
        else if (Initiator == true)
        {

            lblStudentType.Text = "Initiator";
        }
        else if (Student == true)
        {

            lblStudentType.Text = "Student";
        }
    }
    protected void btnProfile_Click(object sender, EventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {


                int isProfileEdit = BLobj.CheckProfileisEditedMode(cook.LeadId());

                if ((isProfileEdit == 0) || (isProfileEdit == 2))
                {
                    tabprofile.Attributes.Add("class", "active");
                    profiletab.Attributes.Add("class", "tab-pane active");

                    tabProject.Attributes.Add("class", "hidden");
                    ProjectTab.Attributes.Add("class", "hidden");

                    tabEvent.Attributes.Add("class", "");
                    EventTab.Attributes.Add("class", "hidden");
                    tabTshirt.Attributes.Add("class", "hidden");
                    TshirtTab.Attributes.Add("class", "hidden");
                    // pnlRequest.Visible = true;
                }
                else
                {
                    tabProject.Attributes.Add("class", "");
                    ProjectTab.Attributes.Add("class", "hidden");
                    tabprofile.Attributes.Add("class", "active");
                    profiletab.Attributes.Add("class", "tab-pane active");
                    tabEvent.Attributes.Add("class", "");
                    EventTab.Attributes.Add("class", "hidden");
                    DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                    if (dt2.Rows.Count > 0)
                    {
                        if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                            btnTshirt.Visible = true;

                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");

                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                            btnTshirt.Visible = true;
                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");


                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                        {

                            tabTshirt.Attributes.Add("class", "");
                            TshirtTab.Attributes.Add("class", "hidden");
                            btnTshirt.Visible = false;

                        }
                        else
                        {
                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");
                            btnTshirt.Visible = false;
                        }
                    }
                    else
                    {
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;
                    }
                    // pnlRequest.Visible = false;

                    // BLobj.Student_GetProjectList(cook.LeadId(), rptStudentProposedList);
                }

                tabEvent.Attributes.Add("class", "");
                EventTab.Attributes.Add("class", "hidden");

                getStudentProfileDetails(cook.LeadId());
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

    protected void btnProjects_Click(object sender, EventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {


                tabProject.Attributes.Add("class", "active");
                ProjectTab.Attributes.Add("class", "tab-pane active");

                tabprofile.Attributes.Add("class", "");
                profiletab.Attributes.Add("class", "hidden");



                DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                if (dt2.Rows.Count > 0)
                {
                    if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                    {
                        btnTshirt.Attributes["data-toggle"] = "tooltip";
                        btnTshirt.Attributes["data-placement"] = "top";
                        btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                        btnTshirt.Visible = true;

                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");

                    }
                    else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                    {
                        btnTshirt.Attributes["data-toggle"] = "tooltip";
                        btnTshirt.Attributes["data-placement"] = "top";
                        btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                        btnTshirt.Visible = true;
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");


                    }
                    else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                    {

                        tabTshirt.Attributes.Add("class", "");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;

                    }
                    else
                    {
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;
                    }
                }
                else
                {
                    tabTshirt.Attributes.Add("class", "hidden");
                    TshirtTab.Attributes.Add("class", "hidden");
                    btnTshirt.Visible = false;
                }

                tabEvent.Attributes.Add("class", "");
                EventTab.Attributes.Add("class", "hidden");

                BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptStudentProposedList);
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

    protected void btnTabTshirt_Click(object sender, EventArgs e)
    {
        try
        {

            tabTshirt.Attributes.Add("class", "active");
            TshirtTab.Attributes.Add("class", "tab-pane active");

            tabProject.Attributes.Add("class", "");
            ProjectTab.Attributes.Add("class", "hidden");

            tabprofile.Attributes.Add("class", "");
            profiletab.Attributes.Add("class", "hidden");


            tabEvent.Attributes.Add("class", "");
            EventTab.Attributes.Add("class", "hidden");
            rptMultipleTshirtRequest.DataSource = BLobj.Student_GetMultipleTshirtRequestRepeater(cook.LeadId());
            rptMultipleTshirtRequest.DataBind();

        }
        catch (Exception)
        {

        }
    }
    protected void btnEvent_Click(object sender, EventArgs e)
    {
        try
        {

            int isProfileEdit = BLobj.CheckProfileisEditedMode(cook.LeadId());

            if ((isProfileEdit == 0) || (isProfileEdit == 2))
            {
                tabprofile.Attributes.Add("class", "");
                profiletab.Attributes.Add("class", "hidden");

                tabProject.Attributes.Add("class", "hidden");
                ProjectTab.Attributes.Add("class", "hidden");

                tabEvent.Attributes.Add("class", "active");
                EventTab.Attributes.Add("class", "tab-pane active");

                tabTshirt.Attributes.Add("class", "hidden");
                TshirtTab.Attributes.Add("class", "hidden");
            }
            else
            {
                tabProject.Attributes.Add("class", "");
                ProjectTab.Attributes.Add("class", "hidden");

                tabprofile.Attributes.Add("class", "");
                profiletab.Attributes.Add("class", "hidden");

                tabEvent.Attributes.Add("class", "active");
                EventTab.Attributes.Add("class", "tab-pane active");
                DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                if (dt2.Rows.Count > 0)
                {
                    if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                    {
                        btnTshirt.Attributes["data-toggle"] = "tooltip";
                        btnTshirt.Attributes["data-placement"] = "top";
                        btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                        btnTshirt.Visible = true;

                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");

                    }
                    else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                    {
                        btnTshirt.Attributes["data-toggle"] = "tooltip";
                        btnTshirt.Attributes["data-placement"] = "top";
                        btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                        btnTshirt.Visible = true;
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");


                    }
                    else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                    {

                        tabTshirt.Attributes.Add("class", "");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;

                    }
                    else
                    {
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;
                    }
                }
                else
                {
                    tabTshirt.Attributes.Add("class", "hidden");
                    TshirtTab.Attributes.Add("class", "hidden");
                    btnTshirt.Visible = false;
                }
            }
            BLobj.Student_GetEventDetail(rptEvent);

        }
        catch (Exception)
        {

        }
    }
    protected void rptEvent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {
                string LeadId = cook.LeadId();

                string EventId = "";

                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 0; i++)
                {
                    if (i == 0)
                    {
                        EventId = itemlist[0].ToString();
                    }

                }
                Response.Redirect("EventDetails.aspx?EventId=" + EventId.ToString());

            }
        }
        catch (Exception)
        {

        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_FillDistrict(ddlState.SelectedValue.ToString(), ddlDistrict);
            ddlCollege.Items.Clear();
            ddlTaluka.Items.Clear();
        }
        catch (Exception)
        {

        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_FillTaluka(ddlDistrict.SelectedValue.ToString(), ddlTaluka);
            ddlCollege.Items.Clear();
        }
        catch (Exception)
        {

        }
    }

    protected void ddlTaluka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_fillCollegeByTaluka(ddlTaluka.SelectedValue.ToString(), ddlProgramme.SelectedItem.Text.ToString(), ddlCollege);
        }
        catch (Exception)
        {

        }
    }

    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_FillProgrammeCourse(ddlProgramme.SelectedValue.ToString(), ddlCourse);
            ddlSemester.Items.Clear();
            BLobj.Student_fillCollegeByTaluka(ddlTaluka.SelectedValue.ToString(), ddlProgramme.SelectedItem.Text.ToString(), ddlCollege);
        }
        catch (Exception)
        {

        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramme.SelectedValue == "11")
            {

                BLobj.Student_FillSemesterForDET(ddlSemester, ddlCourse.SelectedValue.ToString(), BLobj.Student_GetSemesterByCentreId(ddlTaluka));
            }
            else
            {
                string TotalSemester = BLobj.Student_GetCourseTotalSem(ddlCourse.SelectedValue.ToString());
                if (TotalSemester.ToString() == "")
                {
                    TotalSemester = "0";
                }
                BLobj.Student_FillSemester(ddlSemester, TotalSemester);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnSaveProfileImage_Click(object sender, EventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {
                if (ProfilePic.PostedFile != null)
                {
                    string LeadId = cook.LeadId();
                    string msg = "";
                    string FilePath = ConfigurationManager.AppSettings["ProfilePicPath"].ToString();
                    string TempFilePath = FilePath + "temp";
                    string FileExtenssion = System.IO.Path.GetExtension(ProfilePic.PostedFile.FileName);
                    string FileName = LeadId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                    if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                    {
                        Stream strm = ProfilePic.PostedFile.InputStream;
                        if (strm.Length > 20000000)
                        {
                            msg = "Upload Less Than 20 MB Image";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                        }
                        else
                        {
                            using (var ms = new MemoryStream())
                            {
                                ProfilePic.PostedFile.InputStream.CopyTo(ms);
                                ms.Position = 0;
                                //  image = new System.Drawing.Bitmap(ms);


                                using (System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(ms))
                                {
                                    System.Drawing.Imaging.ImageCodecInfo jpgEncoder = BLobj.GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);

                                    //  Create an Encoder object based on the GUID
                                    //   for the Quality parameter category.
                                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                                    // Create an EncoderParameters object.
                                    //An EncoderParameters object has an array of EncoderParameter
                                    //   objects.In this case, there is only one
                                    //  EncoderParameter object in the array.  
                                    System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                                    System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 50L);
                                    myEncoderParameters.Param[0] = myEncoderParameter;
                                    // bmp1.Save("D:\\TestPhotoQualityFifty.jpg", jpgEncoder, myEncoderParameters);
                                    BLobj.Student_UpdateStudentProfileImage(LeadId.ToString(), FilePath + FileName);
                                    bmp1.Save(Server.MapPath(FilePath + FileName), jpgEncoder, myEncoderParameters);

                                    //myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 100L);
                                    //myEncoderParameters.Param[0] = myEncoderParameter;
                                    //bmp1.Save(@"D:\TestPhotoQualityHundred.jpg", jpgEncoder, myEncoderParameters);

                                    ////  Save the bitmap as a JPG file with zero quality level compression.
                                    //myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 0L);
                                    //myEncoderParameters.Param[0] = myEncoderParameter;
                                    //bmp1.Save(@"D:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);
                                }
                            }
                            msg = "Profile Pic Updated!!!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);


                            BLobj.Student_GetLeftProfileDetails(cook.LeadId(), lblStudentName, lblCollegeName, lblLeadId, lblStudentType, lblRegistrationDate);
                            string ManagerId = BLobj.Student_GetManagerIdAfterProfileUpdate(cook.LeadId());
                            BLobj.Student_GetManagerDetails(ManagerId.ToString(), lblManagerName, lblManagerEmailId, lblManagerMobileNo, imgManagerDetailInStudentProfile, btnFacebook, btnTwitter, btnInstaGram, btnWhataApp);
                            BLobj.Student_SetStudentProfileImage(cook.LeadId(), PreviewImage);
                            BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptStudentProposedList);
                            if (btnFacebook.InnerText == "")
                            {
                                Facebook.Visible = false;
                            }
                            else
                            {
                                Facebook.Visible = true;
                            }
                            if (btnTwitter.InnerText == "")
                            {
                                Twitter.Visible = false;
                            }
                            else
                            {
                                Twitter.Visible = true;
                            }
                            if (btnInstaGram.InnerText == "")
                            {
                                InstaGram.Visible = false;
                            }
                            else
                            {
                                InstaGram.Visible = true;
                            }
                            if (btnWhataApp.InnerText == "")
                            {
                                WhataApp.Visible = false;
                            }
                            else
                            {
                                WhataApp.Visible = true;
                            }
                            Response.Redirect("StudentProfile.aspx?ProfileUpdated=Success", false);
                        }
                    }


                }
                else
                {
                    lblError.Text = "Please select .png || jpeg  Format Images";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionOut=True");
            }
        }

        catch (Exception ex)
        {
            lblError.Text = ex.Message.ToString() + " " + "Please upload Less Then 20 MB";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
        }
    }

    protected void btnSaveStudentProfile_Click(object sender, EventArgs e)
    {

        string msg = "";
        try
        {
            if (cook.LeadId() != "")
            {
                string RegistrationId = cook.RegistrationId();
                string LeadId = cook.LeadId();
                string AcademicYear = cook.AcademicYear();
                vmStudent_Web stud = new vmStudent_Web();
                if (ddlCollege.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select Proper College";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlCollege.Focus();

                }
                else if (ddlSemester.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select Semester";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlSemester.Focus();

                }
                else if (ddlCourse.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select Course";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlCourse.Focus();

                }
                else if (ddlProgramme.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select Stream";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlProgramme.Focus();

                }
                else if (ddlSemester.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select Semester";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlSemester.Focus();

                }
                else if (ddlState.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select State";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlSemester.Focus();

                }
                else if (ddlDistrict.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select District";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlSemester.Focus();

                }
                else if (ddlTaluka.SelectedItem.Text == "[Select]")
                {
                    lblError.Text = "Please Select Taluka";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    ddlSemester.Focus();

                }
                else if ((ddlCollege.SelectedItem.Text != "[Select]") || (ddlSemester.SelectedItem.Text != "[Select]"))
                {
                    stud.Lead_Id = LeadId.ToString();
                    stud.RegistrationId = int.Parse(RegistrationId.ToString());
                    stud.StudentName = Regex.Replace(txtStudentName.Text, "'", "`").Trim();
                    stud.StateCode = int.Parse(ddlState.SelectedValue.ToString());
                    stud.DistrictCode = int.Parse(ddlDistrict.SelectedValue.ToString());
                    stud.TalukaCode = int.Parse(ddlTaluka.SelectedValue.ToString());
                    stud.CollegeCode = int.Parse(ddlCollege.SelectedValue.ToString());
                    stud.StreamCode = int.Parse(ddlProgramme.SelectedValue.ToString());
                    stud.CourseCode = int.Parse(ddlCourse.SelectedValue.ToString());
                    stud.SemCode = ddlSemester.SelectedValue.ToString();
                    stud.Address = Regex.Replace(txtAddress.Text, "'", "`").Trim();
                    stud.AcademicCode = AcademicYear.ToString();
                    if (rdoMale.Checked == true)
                    {
                        stud.Gender = "M";
                    }
                    else
                    {
                        stud.Gender = "F";
                    }
                    stud.DOB = ddlDay.SelectedValue.ToString() + "-" + ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedItem.Text.ToString();
                    stud.BloodGroup = ddlBloodGroup.SelectedValue.ToString();
                    if (txtAlternativeMobileNo.Text == "")
                    {
                        stud.AlternativeMobileNo = 0;
                    }
                    else
                    {
                        stud.AlternativeMobileNo = long.Parse(txtAlternativeMobileNo.Text.ToString());
                    }
                    stud.MailId = Regex.Replace(txtStudentMailId.Text, "'", "").Trim();
                    stud.Bank_Name = Regex.Replace(txtBankName.Text, "'", "`").Trim();
                    if (txtAccountNo.Text == "")
                    {
                        stud.Account_No = "0";
                    }
                    else
                    {
                        stud.Account_No = txtAccountNo.Text.ToString();
                    }
                    stud.IFSC_code = txtIFSCCode.Text.ToString();
                    stud.AccountHolderName = Regex.Replace(txtAccountHolderName.Text, "'", "`").Trim();
                    stud.Branch_Name = Regex.Replace(txtBankBranch.Text, "'", "`").Trim();
                    if (txtAadharNo.Text == "")
                    {
                        stud.AadharNo = 0;
                    }
                    else
                    {
                        stud.AadharNo = long.Parse(txtAadharNo.Text.ToString());
                    }
                    stud.MyTalent = txtMyTalent.Text.ToString().TrimStart(',').TrimEnd(',');
                    BLobj.Student_UpdateProfileDatails(stud);
                    string FilePath = ConfigurationManager.AppSettings["ProfilePicPath"].ToString();
                    string FileExtenssion = System.IO.Path.GetExtension(ProfilePic.PostedFile.FileName);
                    string FileName = LeadId + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                    string TempFilePath = FilePath + "temp";
                    if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                    {
                        //BLobj.Student_UpdateStudentProfileImage(LeadId.ToString(), FilePath + FileName);
                        //ProfilePic.SaveAs(Server.MapPath(FilePath + FileName));
                        Stream strm = ProfilePic.PostedFile.InputStream;

                        if (strm.Length > 20000000)
                        {
                            msg = "Upload Less Than 20 MB Image";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                        }
                        else
                        {
                            using (var ms = new MemoryStream())
                            {
                                ProfilePic.PostedFile.InputStream.CopyTo(ms);
                                ms.Position = 0;
                                //ProfilePic.SaveAs(Server.MapPath(TempFilePath + "/" + FileName));
                                //string filePathhttp = Server.MapPath(TempFilePath + "/" + FileName);
                                using (System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(ms))
                                {
                                    System.Drawing.Imaging.ImageCodecInfo jpgEncoder = BLobj.GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
                                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                    System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                                    System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 50L);
                                    myEncoderParameters.Param[0] = myEncoderParameter;
                                    BLobj.Student_UpdateStudentProfileImage(LeadId.ToString(), FilePath + FileName);
                                    bmp1.Save(Server.MapPath(FilePath + FileName), jpgEncoder, myEncoderParameters);
                                }
                            }
                        }
                    }
                    string StudentMangerId = BLobj.Student_GetManagerIdAfterProfileUpdate(LeadId.ToString());
                    BLobj.Student_GetLeftProfileDetails(LeadId.ToString(), lblStudentName, lblCollegeName, lblLeadId, lblStudentType, lblRegistrationDate);
                    BLobj.Student_GetManagerDetails(StudentMangerId, lblManagerName, lblManagerEmailId, lblManagerMobileNo, imgManagerDetailInStudentProfile, btnFacebook, btnTwitter, btnInstaGram, btnWhataApp);
                    if (btnFacebook.InnerText == "")
                    {
                        Facebook.Visible = false;
                    }
                    else
                    {
                        Facebook.Visible = true;
                    }
                    if (btnTwitter.InnerText == "")
                    {
                        Twitter.Visible = false;
                    }
                    else
                    {
                        Twitter.Visible = true;
                    }
                    if (btnInstaGram.InnerText == "")
                    {
                        InstaGram.Visible = false;
                    }
                    else
                    {
                        InstaGram.Visible = true;
                    }
                    if (btnWhataApp.InnerText == "")
                    {
                        WhataApp.Visible = false;
                    }
                    else
                    {
                        WhataApp.Visible = true;
                    }
                    BLobj.Student_UpdateManagerIdInProjectAfterProfileUpdate(cook.RegistrationId(), StudentMangerId.ToString());
                    HttpCookie Student_ManagerId = new HttpCookie("Student_ManagerId");
                    Student_ManagerId.Value = StudentMangerId.ToString();
                    Student_ManagerId.Expires = DateTime.Now.AddDays(24);
                    Response.SetCookie(Student_ManagerId);

                    string msgMail = "Dear " + " " + txtStudentName.Text.ToString() + " " + "Your Profile has been updated successfully Lead Id is : " + lblLeadId.Text.ToString() + " Name is : " + lblStudentName.Text.ToString();


                    BLobj.Student_SetStudentProfileImage(cook.LeadId(), PreviewImage);
                    BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptStudentProposedList);

                    tabProject.Attributes.Add("class", "active");
                    ProjectTab.Attributes.Add("class", "tab-pane active");

                    tabprofile.Attributes.Add("class", "");
                    profiletab.Attributes.Add("class", "hidden");

                    tabEvent.Attributes.Add("class", "");
                    EventTab.Attributes.Add("class", "hidden");




                    SendEditProfileDetails(LeadId.ToString(), "");
                    DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                    if (dt2.Rows.Count > 0)
                    {
                        if ((dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                            btnTshirt.Visible = true;

                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");

                        }
                        else if ((dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                            btnTshirt.Visible = true;
                            tabTshirt.Attributes.Add("class", "hidden");
                            TshirtTab.Attributes.Add("class", "hidden");


                        }
                        else if ((dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                        {

                            tabTshirt.Attributes.Add("class", "");
                            TshirtTab.Attributes.Add("class", "hidden");
                            btnTshirt.Visible = false;

                        }
                    }
                    else
                    {
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;
                    }

                    // BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptStudentProposedList);
                    Response.Redirect("StudentProfile.aspx?ProfileUpdated=Success", true);
                }
            }
            else
            {
                SendEditProfileDetails(cook.LeadId(), "SessionOUT");
                Response.Redirect("~/Default.aspx?SessionOut=True", true);
            }

        }
        catch (Exception ex)
        {
            SendEditProfileDetails(cook.LeadId(), ex.Message.ToString());
            ll.Text = ex.Message.ToString();
        }
    }
    public void getStudentProfileDetails(string LeadId)
    {
        try
        {
            if (cook.LeadId() != "")
            {

                vmStudent_Web vms = new vmStudent_Web();
                vms = BLobj.Student_GetStudentProfileDetails(LeadId.ToString());
                txtStudentName.Text = vms.StudentName.ToString();
                lblStudentName.Text = vms.StudentName.ToString();
                txtMobileNo.Text = vms.MobileNo.ToString();
                txtLeadId.Text = LeadId.ToString();
                if (vms.AadharNo == 0)
                {
                    txtAadharNo.Text = "";
                }
                else
                {
                    txtAadharNo.Text = vms.AadharNo.ToString();
                }

                txtBankName.Text = vms.Bank_Name.ToString();

                if (vms.Account_No == "0")
                {
                    txtAccountNo.Text = "";
                }
                else
                {
                    txtAccountNo.Text = vms.Account_No.ToString();
                }
                txtIFSCCode.Text = vms.IFSC_code.ToString();
                txtAccountHolderName.Text = vms.AccountHolderName.ToString();
                txtBankBranch.Text = vms.Branch_Name.ToString();
                lblRegistrationDate.Text = vms.RegistrationDate.ToString();
                lblStudentType.Text = vms.Student_Type.ToString();
                lblRegistrationDate.Text = vms.RegistrationDate.ToString();
                txtAddress.Text = vms.Address.ToString();
                txtAlternativeMobileNo.Text = vms.AlternativeMobileNo.ToString();
                txtStudentMailId.Text = vms.MailId.ToString();
                ddlProgramme.SelectedIndex = ddlProgramme.Items.IndexOf(ddlProgramme.Items.FindByValue(vms.StreamCode.ToString()));
                BLobj.Student_FillProgrammeCourse(vms.StreamCode.ToString(), ddlCourse);
                ddlCourse.SelectedIndex = ddlCourse.Items.IndexOf(ddlCourse.Items.FindByValue(vms.CourseCode.ToString()));
                string TotalSemester = BLobj.Student_GetCourseTotalSem(ddlCourse.SelectedValue.ToString());


                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(vms.StateCode.ToString()));
                BLobj.Student_FillDistrict(vms.StateCode.ToString(), ddlDistrict);
                ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByValue(vms.DistrictCode.ToString()));
                BLobj.Student_FillTaluka(vms.DistrictCode.ToString(), ddlTaluka);
                ddlTaluka.SelectedIndex = ddlTaluka.Items.IndexOf(ddlTaluka.Items.FindByValue(vms.TalukaCode.ToString()));
                BLobj.Student_fillCollegeByTaluka(vms.TalukaCode.ToString(), ddlProgramme.SelectedItem.Text.ToString(), ddlCollege);
                ddlCollege.SelectedIndex = ddlCollege.Items.IndexOf(ddlCollege.Items.FindByValue(vms.CollegeCode.ToString()));
                if (vms.StreamCode.ToString() == "11")
                {
                    BLobj.Student_FillSemesterForDET(ddlSemester, vms.CourseCode.ToString(), BLobj.Student_GetSemesterByCentreId(ddlTaluka));
                }
                else
                {
                    if (TotalSemester.ToString() == "")
                    {
                        TotalSemester = "0";
                    }
                    BLobj.Student_FillSemester(ddlSemester, TotalSemester);
                }


                ddlSemester.SelectedIndex = ddlSemester.Items.IndexOf(ddlSemester.Items.FindByValue(vms.SemCode.ToString()));


                if (vms.Gender == "M")
                {
                    rdoMale.Checked = true;
                }
                else if (vms.Gender == "F")
                {
                    rdoFemale.Checked = true;
                }
                else
                {
                    rdoMale.Checked = true;
                }


                ddlBloodGroup.SelectedIndex = ddlBloodGroup.Items.IndexOf(ddlBloodGroup.Items.FindByValue(vms.BloodGroup.ToString()));
                lblCollegeName.Text = ddlCollege.SelectedItem.Text.ToString();
                ddlDay.SelectedIndex = ddlDay.Items.IndexOf(ddlDay.Items.FindByValue(vms.GetDay.ToString()));
                ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(vms.GetMonth.ToString()));
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(vms.GetYear.ToString()));
                txtMyTalent.Text = vms.MyTalent.ToString();
                // BLobj.Student_GetLeftProfileDetails(cook.LeadId(), lblStudentName, lblCollegeName, lblLeadId, lblStudentType, lblRegistrationDate);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionOut=True");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnProposedProject_Click(object sender, EventArgs e)
    {
        try
        {
            txtProjectTitle.Text = "";
            txtProjectPlan.Text = "";
            txtProjectObjectives.Text = "";
            txtTotalBeneficiaries.Text = "";
            txtProposalPlaceofImplementation.Text = "";
            txtProposedBeneficiaries.Text = "";
            txtProposedStartDate.Text = "";
            txtProposedEndDate.Text = "";
            txtCurrentSituation.Text = "";
            rptMeterial.DataSource = null;
            rptMeterial.DataBind();
            rptTeamMembers.DataSource = null;
            rptTeamMembers.DataBind();
            ManagerCommentText.Visible = false;
            lblProposedTitle.Text = "Project Proposal Form";
            lblProposedEventType.Text = "New";
            lblTotalAmt.Visible = false;
            BLobj.FillThemeMaster(ddlProjectType);

            string IsFeesPaid = BLobj.Student_CheckIsFeesPaid(cook.LeadId());
            if (IsFeesPaid == "1")
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
            }
            else
            {

                lblError.Text = "You have not paid registration fees. kindly contact your mentor!!!";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
            }
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
                if (clickcount == 0)
                {

                    clickcount = clickcount + 1;

                    string RegistrationId = cook.RegistrationId();
                    string ManagerId = cook.ManagerId();
                    string LeadId = cook.LeadId();
                    string PDId = "";
                    string AcademicCode = cook.AcademicYear();
                    string ProposedDate = System.DateTime.Now.ToString("dd-MM-yyyy");
                    if (ddlProjectType.SelectedItem.Text == "[Select]")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
                        string msg = "Select Project Type";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

                    }
                    else
                    {
                        if (BLobj.ValidateMeterial(rptMeterial) == false)
                        {
                            if (lblProposedEventType.Text == "New")
                            {
                                PDId = BLobj.Student_SaveProjectProposal(LeadId.ToString(), ManagerId.ToString(), RegistrationId.ToString(), txtProjectTitle.Text.ToString().Trim(), txtTotalBeneficiaries.Text, txtProjectObjectives.Text.ToString().Trim(), txtProjectPlan.Text.ToString(), ProposedDate.ToString(), AcademicCode.ToString(), ddlProjectType.SelectedValue.ToString(), txtProposalPlaceofImplementation.Text.ToString(), txtProposedBeneficiaries.Text.ToString(), txtCurrentSituation.Text.ToString(), txtProposedStartDate.Text.ToString(), txtProposedEndDate.Text.ToString());
                            }
                            else
                            {
                                PDId = lblPDIdEditProject.Text.ToString();
                                BLobj.Student_EditProjectProposal(LeadId.ToString(), PDId.ToString(), ManagerId.ToString(), txtProjectTitle.Text.ToString().Trim(), txtTotalBeneficiaries.Text, txtProjectObjectives.Text.ToString().Trim(), txtProjectPlan.Text.ToString(), ProposedDate.ToString(), AcademicCode.ToString(), ddlProjectType.SelectedValue.ToString(), lblProjectStatusClickingEvent.Text.ToString(), txtProposedBeneficiaries.Text.ToString(), txtProposalPlaceofImplementation.Text.ToString(), txtCurrentSituation.Text.ToString(), txtProposedStartDate.Text.ToString(), txtProposedEndDate.Text.ToString());
                            }
                            if (rptMeterial.Items.Count >= 0)
                            {
                                BLobj.Student_SaveProjectMeterials(LeadId.ToString(), PDId.ToString(), rptMeterial, lblProposedEventType.Text.ToString());
                            }
                            if (rptTeamMembers.Items.Count >= 0)
                            {
                                BLobj.Student_SaveProjectTeamMembers(LeadId.ToString(), PDId.ToString(), rptTeamMembers, lblProposedEventType.Text.ToString());
                            }

                            if (lblProposedEventType.Text == "New")
                            {
                                string msgMail = lblLeadId.Text.ToString() + " " + "Your project is proposed successfully " + " Name is : " + lblStudentName.Text.ToString() +
                                "Title: " + txtProjectTitle.Text.ToString();
                                DataTable dt = BLobj.Student_GetStudentDetailForMailSend(cook.LeadId());
                                string Mailid = dt.Rows[0].ItemArray[0].ToString();
                                string MobileNo = dt.Rows[0].ItemArray[2].ToString();
                                if (Mailid.ToString() != "")
                                {
                                    string body = PopulateBody(lblStudentName.Text.ToString(),
                                    " <b>Your project is proposed successfully</b>", "The details you entered are listed below: ", "<ol><li><b>LEAD Id:</b> " + lblLeadId.Text.Trim() + "<br /><br /></li><li><b>Name:</b> " + lblStudentName.Text.Trim() + "<br /><br /></li><li><b>Mobile No.:</b> " + MobileNo.ToString() + "<br /><br />" + "" +
                                    "<br /><br /></li><li><b>Institution:</b> " + lblCollegeName.Text.ToString() + "<br /><br /></li><li><b>Title of the Project:</b> " + txtProjectTitle.Text.Trim() + "<br /><br /></li><li><b>Beneficieries:</b> " + txtTotalBeneficiaries.Text.ToString() + "<br /><br /></li><li><b>Theme:</b> " + ddlProjectType.SelectedItem.Text.ToString() + "<br /><br /></li><li><b>Objectives:</b> " + txtProjectObjectives.Text.Trim() + "<br /><br /></li><li><b>Action Plan:</b> " + txtProjectPlan.Text.Trim() + "</li></ol><br /><br />");
                                    SendHtmlFormattedEmail(Mailid.ToString(), "Project Proposal Details", body);
                                }
                                string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                                if (DeviceID != "")
                                {
                                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your project" + " " + txtProjectTitle.Text.ToString() + " is proposed successfully", "Student", "");
                                }

                            }
                            else
                            {
                                string msgMail = "Update of Your Project is Successfully Completed Lead Id is : " + lblLeadId.Text.ToString() + " Name is : " + lblStudentName.Text.ToString() +
                               "Title: " + txtProjectTitle.Text.ToString();
                                SendEmailProposed();
                                string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                                if (DeviceID != "")
                                {
                                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your project" + " " + txtProjectTitle.Text.ToString() + " is Updated successfully", "Student", "");
                                }

                            }
                            Response.Redirect("StudentProfile.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
                            string msg = "Remove Duplicate Meterial name";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                        }

                    }
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
    protected void rptStudentProposedList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {
                string LeadId = cook.LeadId();
                string PDId = "";
                string ProjectStatus = "";
                string btnPressType = "";
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 2; i++)
                {
                    if (i == 0)
                    {
                        PDId = itemlist[0].ToString();
                    }
                    else if (i == 1)
                    {
                        ProjectStatus = itemlist[1].ToString();
                    }
                    else if (i == 2)
                    {
                        btnPressType = itemlist[2].ToString();
                    }
                }
                if (btnPressType == "View")
                {

                }
                else
                {
                    if (ProjectStatus == "Proposed")
                    {
                        ManagerCommentText.Visible = true;
                        Server.Transfer("Student_PendingApproval.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString(), false);
                    }
                    else if (ProjectStatus == "RequestForModification")
                    {
                        ManagerCommentText.Visible = true;
                        Server.Transfer("Student_PendingApproval.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString(), false);
                        lblTotalAmt.Visible = true;
                    }
                    else if (ProjectStatus == "Approved")
                    {
                        Server.Transfer("Student_ProjectCompletion.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString(), false);
                    }
                    else if (ProjectStatus == "RequestForCompletion")
                    {
                        Server.Transfer("Student_ProjectCompletion.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString(), false);
                    }
                    else if (ProjectStatus == "Draft")
                    {
                        Server.Transfer("Student_ProjectCompletion.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString(), false);
                    }
                    else if (ProjectStatus == "Completed")
                    {
                        Server.Transfer("Student_ProjectCompletion.aspx?PDID=" + PDId.ToString() + "&ProjectStatus=" + ProjectStatus.ToString(), false);
                    }
                    else if (ProjectStatus == "Rejected")
                    {
                    }
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
    protected void rptStudentProposedList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Label ProjectStatus = (e.Item.FindControl("lblProjectStatus") as Label);
                //LinkButton ProjectStatus = (e.Item.FindControl("btnViewProject") as LinkButton);
                Label Rating = (e.Item.FindControl("lblRating") as Label);
                Label PDId = (e.Item.FindControl("lblPDId") as Label);
                Label lblDispersedAmount = (e.Item.FindControl("lblDispersedAmount") as Label);
                Label lblBalanceAmount = (e.Item.FindControl("lblBalanceAmount") as Label);
                Label lblFundedAmount = (e.Item.FindControl("lblFundedAmount") as Label);
                Label lblCompletionProgress = (e.Item.FindControl("lblCompletionProgress") as Label);
                Label lblCompletionProgressCount = (e.Item.FindControl("lblCompletionProgressCount") as Label);
                //Panel pnlCountVisibility = (e.Item.FindControl("pnlChatCountVisibility") as Panel);
                //Label lblChatUnReadCount = (e.Item.FindControl("lblChatUnReadCount") as Label);
                LinkButton btnChat = (e.Item.FindControl("btnChat") as LinkButton);


                int UnReadCount = BLobj.Common_GetChatUnreadMessageCount(PDId.Text.ToString());

                if (UnReadCount == 0)
                {

                    btnChat.Attributes.Add("class", "btn btn-success  btn-rounded btn-sm");
                }
                else
                {
                    btnChat.Attributes.Add("class", "btn btn-warning  btn-rounded btn-sm");

                }


                //  System.Web.UI.HtmlControls.HtmlGenericControl hh = (e.Item.FindControl("tdProgress") as System.Web.UI.HtmlControls.HtmlGenericControl);


                //Label tdProgress = new Label();
                //tdProgress.ID = hh.ID;
                //(HtmlTableCell)rpSearchItems.FindControl("tdValue2");


                lblDispersedAmount.Text = BLobj.Student_GetProjectTotalFunded(PDId.Text, cook.LeadId());
                lblBalanceAmount.Text = Convert.ToString(int.Parse(lblFundedAmount.Text) - int.Parse(lblDispersedAmount.Text));
                LinkButton btnEdit = (e.Item.FindControl("btnEditProject") as LinkButton);
                if (ProjectStatus.Text == "Proposed")
                {
                    btnEdit.Text = "<span class='fa fa-edit'></span>";
                    btnEdit.Attributes.Add("class", "btn btn-default btn-rounded btn-sm");
                    ProjectStatus.Attributes.Add("class", "label label-default");
                    lblProjectStatusClickingEvent.Text = "Proposed";
                    btnEdit.Attributes["data-toggle"] = "tooltip";
                    btnEdit.Attributes["data-placement"] = "top";
                    btnEdit.Attributes["title"] = "Edit Project";
                    //tdProgress.Visible = false;
                }
                else if (ProjectStatus.Text == "Approved")
                {
                    btnEdit.Text = "<span class='fa fa-share'></span>";
                    btnEdit.Attributes.Add("class", "btn btn-info btn-rounded btn-sm");
                    ProjectStatus.Attributes.Add("class", "label label-primary");
                    btnEdit.Attributes["data-toggle"] = "tooltip";
                    btnEdit.Attributes["data-placement"] = "top";
                    btnEdit.Attributes["title"] = "Submit Completion Form";
                    // tdProgress.Visible = false;
                }
                else if (ProjectStatus.Text == "RequestForModification")
                {
                    btnEdit.Text = "<span class='fa fa-reply'></span>";
                    btnEdit.Attributes.Add("class", "btn btn-primary btn-rounded btn-sm");
                    ProjectStatus.Attributes.Add("class", "label label-warning text-danger");
                    lblProjectStatusClickingEvent.Text = "RequestForModification";
                    btnEdit.Attributes["data-toggle"] = "tooltip";
                    btnEdit.Attributes["data-placement"] = "top";
                    btnEdit.Attributes["title"] = "Edit Project Details";
                    // tdProgress.Visible = false;
                }
                else if (ProjectStatus.Text == "RequestForCompletion")
                {
                    btnEdit.Text = "<span class='fa fa-edit'></span>";
                    btnEdit.Attributes.Add("class", "btn btn-warning btn-rounded btn-sm");
                    ProjectStatus.Attributes.Add("class", "label label-warning text-danger");
                    btnEdit.Attributes["data-toggle"] = "tooltip";
                    btnEdit.Attributes["data-placement"] = "top";
                    btnEdit.Attributes["title"] = "Edit Submitted Completion Form";
                    // tdProgress.Visible = false;
                }
                else if (ProjectStatus.Text == "Draft")
                {
                    btnEdit.Text = "<span class='fa fa-edit'></span>";
                    btnEdit.Attributes.Add("class", "btn btn-warning btn-rounded btn-sm");
                    ProjectStatus.Attributes.Add("class", "label label-warning text-danger");
                    btnEdit.Attributes["data-toggle"] = "tooltip";
                    btnEdit.Attributes["data-placement"] = "top";
                    btnEdit.Attributes["title"] = "Edit drafted Completion Form";

                    string Progress = BLobj.Student_GetCompletionDraftProgress(PDId.Text.ToString());

                    lblCompletionProgress.Attributes.Add("style", "max-width:" + Progress + "%");
                    lblCompletionProgress.Attributes["data-toggle"] = "tooltip";
                    lblCompletionProgress.Attributes["data-placement"] = "top";
                    lblCompletionProgress.Attributes["title"] = "Draft Progress";
                    lblCompletionProgress.Text = Progress.ToString();
                    int p_count = int.Parse(Progress.ToString());
                    if (p_count <= 30)
                    {
                        lblCompletionProgress.Attributes.Add("class", "progress-bar progress-bar-danger progress-bar-striped");
                    }
                    else if ((p_count >= 31) && (p_count <= 60))
                    {
                        lblCompletionProgress.Attributes.Add("class", "progress-bar progress-bar-warning progress-bar-striped");
                    }
                    else if ((p_count >= 61) && (p_count <= 90))
                    {
                        lblCompletionProgress.Attributes.Add("class", "progress-bar progress-bar-primary progress-bar-striped");
                    }
                    else if (p_count == 100)
                    {
                        lblCompletionProgress.Attributes.Add("class", "progress-bar progress-bar-success progress-bar-striped");
                        lblCompletionProgress.Attributes["data-toggle"] = "tooltip";
                        lblCompletionProgress.Attributes["data-placement"] = "top";
                        lblCompletionProgress.Attributes["title"] = "Final Submission";
                        btnEdit.Attributes["title"] = "Ready For Final Submission";
                    }
                    ProjectStatus.Visible = false;

                    lblCompletionProgressCount.Text = Progress;
                    //tdProgress.Visible = true;
                    //tdProgress.Attributes.Add("class", "");
                }
                else if (ProjectStatus.Text == "Completed")
                {
                    ProjectStatus.Attributes.Add("class", "label label-success");
                    btnEdit.Text = "<span class='fa fa-thumbs-up'></span>";
                    btnEdit.Attributes.Add("class", "btn btn-success btn-rounded btn-sm");
                    btnEdit.Attributes["data-toggle"] = "tooltip";
                    btnEdit.Attributes["data-placement"] = "top";
                    btnEdit.Attributes["title"] = "Completed Project";
                    Rating.Text = BLobj.GetRatingStarts(Rating.Text);
                    Rating.Visible = true;
                    ProjectStatus.Visible = false;

                    //  tdProgress.Visible = false;

                }
                else if (ProjectStatus.Text == "Rejected")
                {
                    btnEdit.Text = "<span class='fa fa-remove text-danger'></span>";
                    btnEdit.Attributes.Add("class", "btn btn-danger btn-rounded btn-sm text-danger");
                    btnEdit.Enabled = false;
                    ProjectStatus.Attributes.Add("class", "label label-danger");
                    btnEdit.Attributes["data-toggle"] = "tooltip";
                    btnEdit.Attributes["data-placement"] = "top";
                    btnEdit.Attributes["title"] = "Rejected Project";
                    // tdProgress.Visible = false;

                }

            }
        }
        catch (Exception)
        {

        }
    }



    List<Meterials> dataList = new List<Meterials>();
    protected void btnAddMeterial_Click(object sender, EventArgs e)
    {
        try
        {
            //-- add all existing values to a list
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
                string msg = "Remove Duplicate Meterial name";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }




            //-- bind repeater
            rptMeterial.DataSource = dataList;
            rptMeterial.DataBind();
        }
        catch (Exception)
        {
            lblProposedTitle.Text = "Project Proposal Form";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
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


            dataList.RemoveAt(index);
            rptMeterial.DataSource = dataList;
            rptMeterial.DataBind();
            dataList.Clear();
        }
        catch (Exception)
        {
            lblProposedTitle.Text = "Project Proposal Form";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
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
            lblProposedTitle.Text = "Project Proposal Form";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
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
            lblProposedTitle.Text = "Project Proposal Form";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
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
            lblProposedTitle.Text = "Project Proposal Form";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectProposal();", true);
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


    protected void SendEmailProposed()
    {

        try
        {
            DataTable dt = BLobj.Student_GetStudentDetailForMailSend(cook.LeadId());
            string Mailid = dt.Rows[0].ItemArray[0].ToString();
            if (Mailid.ToString() != "")
            {
                string body = PopulateBody(lblStudentName.Text.ToString(),
                   " <b>Your project is proposed successfully</b>", "The details you entered are listed below: ", "<ol><li><b>LEAD Id:</b> " + lblLeadId.Text.Trim() + "<br /><br /></li><li><b>Name:</b> " + lblStudentName.Text.Trim() + "<br /><br /></li><li><b>Mobile No.:</b> " + "" +
                   "<br /><br /></li><li><b>Institution:</b> " + lblCollegeName.Text.ToString() + "<br /><br /></li><li><b>Title of the Project:</b> " + txtProjectTitle.Text.Trim() + "<br /><br /></li><li><b>Beneficieries:</b> " + txtTotalBeneficiaries.Text.ToString() + "<br /><br /></li><li><b>Theme:</b> " + ddlProjectType.SelectedItem.Text.ToString() + "<br /><br /></li><li><b>Objectives:</b> " + txtProjectObjectives.Text.Trim() + "<br /><br /></li><li><b>Action Plan:</b> " + txtProjectPlan.Text.Trim() + "</li></ol><br /><br />");
                SendHtmlFormattedEmail(Mailid.ToString(), "Project Proposal Details", body);
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
    private void SendHtmlFormattedEmailException(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress("leadmis@dfmail.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            //  mailMessage.To.Add(new MailAddress(recepientEmail));
            //  mailMessage.CC.Add(new MailAddress(lblManagerEmailId.Text.ToString()));
            mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
            // mailMessage.Bcc.Add(new MailAddress("sunil.tech@dfmail.org"));
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
    public void send_sms(string no, string message)
    {
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://alerts.campusconnect.co/api/web2sms.php?workingkey=A6a2f9a0287c81d9dfa2c15cdfb489987&to=" + no + "&sender=LCLEAD&message=" + message);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
            String ManagerName = string.Empty;
        }
        catch (Exception ex)
        {
            BLobj.SendMailException("send_sms", ex.ToString(), "StudentProfile.aspx Proposal Submit", cook.LeadId(), no.ToString());
        }
    }
    protected void pnlVisibility(bool Student, bool Master, bool Ambassador, bool Tshirt)
    {
        pnlStudent.Visible = Student;
        pnlMasterLeader.Visible = Master;
        pnlLeadAmbassador.Visible = Ambassador;


    }

    protected void btnClickForMasterLeaderApply_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_ApplyForMasterLeader(cook.LeadId());
            Response.Redirect("StudentProfile.aspx");

        }
        catch (Exception)
        {

        }
    }

    protected void btnClickForLeadAmbassadorApply_Click(object sender, EventArgs e)
    {
        try
        {
            // BLobj.Student_ApplyForLeadAmbassador(cook.LeadId());
            Response.Redirect("StudentProfile.aspx");

        }
        catch (Exception)
        {

        }
    }

    protected void btnRequestToManager_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_RequestToManager();", true);

        }
        catch (Exception)
        {
            string msg = "Not Valid Id / Internal Connection Lost";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }
    public void SendEditProfileDetails(string Lead_Id, string Exception)
    {
        string Gender = "";

        if (rdoMale.Checked == true)
        {
            Gender = "M";
        }
        else
        {
            Gender = "F";
        }
        DataTable dt = BLobj.Student_GetStudentDetailAfterEditProfile(Lead_Id.ToString());

        //if(Exception.ToString()=="")
        //{
        //    Subject = "Student Profile Edit (Exception) Lead Id is " + Lead_Id.ToString() + " " + System.DateTime.Now.ToString("dd-MM-yyyy") + ",\n";
        //}
        //else
        //{
        //    Subject = "Student Profile Edit Lead Id is " + Lead_Id.ToString() + " " + System.DateTime.Now.ToString("dd-MM-yyyy") + ",\n";
        //}
        string MaildBody = "";

        MaildBody = PopulateBody(txtStudentMailId.Text.ToString(),
        "<b>Edit Profile Successfully (WEB) </b>", "",
        "Student Profile Edit Lead Id is " + Lead_Id.ToString() + " " + System.DateTime.Now.ToString() + " " +
        "<table><tr><td>" + " " +
        "<b style='color:red'>UI Entered data</b>" + " " +
        "<ol>" + " " +
        "<li><b>Student Name :</b> " + txtStudentName.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Mobile No :</b> " + txtMobileNo.Text.ToString() + "<br /><br /></li> " + " " +
        "<li><b>Mail Id :</b> " + txtStudentMailId.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>State :</b> " + ddlState.SelectedValue.ToString() + " " + ddlState.SelectedItem.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>District :</b> " + ddlDistrict.SelectedValue.ToString() + " " + ddlDistrict.SelectedItem.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Taluka :</b> " + ddlTaluka.SelectedValue.ToString() + " " + ddlTaluka.SelectedItem.Text.ToString() + " " + "<br /><br /></li>" + " " +
        "<li><b>Stream :</b> " + ddlProgramme.SelectedValue.ToString() + " " + ddlProgramme.SelectedItem.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Course :</b> " + ddlCourse.SelectedValue.ToString() + " " + ddlCourse.SelectedItem.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Semester :</b> " + ddlSemester.SelectedValue.ToString() + " " + ddlSemester.SelectedItem.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>College :</b> " + ddlCollege.SelectedValue.ToString() + " " + ddlCollege.SelectedItem.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Address :</b> " + txtAddress.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Alternate Mobileno :</b> " + txtAlternativeMobileNo.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Day :</b> " + " " + ddlDay.SelectedValue.ToString() + " " + "<b>Month :</b> " + " " + ddlMonth.SelectedValue.ToString() + " " + "<b>Year :</b>" + ddlYear.SelectedValue.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Gender :</b> " + Gender.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Blood Group :</b> " + ddlBloodGroup.SelectedValue.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Aadhar No :</b> " + txtAadharNo.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Bank Name :</b> " + txtBankName.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Account No :</b> " + txtAccountNo.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>IFSC Code :</b> " + txtIFSCCode.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Account Holder Name</b> " + txtAccountHolderName.Text.ToString() + "<br /><br /></li>" + " " +
        "<li><b>Branch</b> " + txtBankBranch.Text.ToString() + "<br /><br /></li></ol></td>" + " " +
        //---------------------------------Data from Database------------------

        "<td>" + " " +
        "<b style='color:red'>Data From Database</b>" + " " +
        "<ol>" + " " +
        "<li><b>Student Name :</b> " + dt.Rows[0].ItemArray[0].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Mobile No :</b> " + dt.Rows[0].ItemArray[1].ToString() + "<br /><br /></li> " + " " +
        "<li><b>MailId :</b> " + dt.Rows[0].ItemArray[2].ToString() + "<br /><br /></li>" + " " +
        "<li><b>State :</b> " + dt.Rows[0].ItemArray[3].ToString() + "<br /><br /></li>" + " " +
        "<li><b>District :</b> " + dt.Rows[0].ItemArray[4].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Taluka :</b> " + dt.Rows[0].ItemArray[5].ToString() + " " + "<br /><br /></li>" + " " +
        "<li><b>Stream :</b> " + dt.Rows[0].ItemArray[6].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Course :</b> " + dt.Rows[0].ItemArray[7].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Semester :</b> " + dt.Rows[0].ItemArray[8].ToString() + "<br /><br /></li>" + " " +
        "<li><b>College :</b> " + dt.Rows[0].ItemArray[9].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Address :</b> " + dt.Rows[0].ItemArray[10].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Alternate Mobileno :</b> " + dt.Rows[0].ItemArray[11].ToString() + "<br /><br /></li>" + " " +
        "<li><b>DOB :</b> " + " " + dt.Rows[0].ItemArray[12].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Gender :</b> " + dt.Rows[0].ItemArray[13].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Blood Group :</b> " + dt.Rows[0].ItemArray[14].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Aadhar No :</b> " + dt.Rows[0].ItemArray[15].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Bank Name :</b> " + dt.Rows[0].ItemArray[16].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Account No :</b> " + dt.Rows[0].ItemArray[17].ToString() + "<br /><br /></li>" + " " +
        "<li><b>IFSC Code :</b> " + dt.Rows[0].ItemArray[18].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Account Holder Name</b> " + dt.Rows[0].ItemArray[19].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Branch</b> " + dt.Rows[0].ItemArray[20].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Edited Date</b> " + dt.Rows[0].ItemArray[21].ToString() + "<br /><br /></li>" + " " +
        "<li><b>isProfileEdit</b> " + dt.Rows[0].ItemArray[22].ToString() + "<br /><br /></li>" + " " +
        "<li><b>Device Type</b> " + dt.Rows[0].ItemArray[23].ToString() + "<br /><br /></li></ol></td></tr></table><br /><br />");

        SendHtmlFormattedEmailException("", cook.LeadId() + "-" + "Profile Edit Details (WEB)", MaildBody);

    }
    protected void btnSendRequestToManager_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_SendRequestToManager(cook.LeadId(), txtStudentRequestName.Text.ToString(), txtMobileNo.Text.ToString(), txtRequestToManager.Text.ToString(), txtRequestMailId.Text.ToString(), txtStateName.Text.ToString(), txtDistrictName.Text.ToString(), txtTalukaName.Text.ToString(), txtCollegeName.Text.ToString(), lblManagerEmailId.Text.ToString(), lblManagerMobileNo.Text.ToString(), cook.ManagerId());
            txtStudentMailId.Text = "";
            txtRequestToManager.Text = "";

            txtRequestToManager.Text = "";
            txtRequestMailId.Text = "";
            txtStateName.Text = "";
            txtDistrictName.Text = "";
            txtTalukaName.Text = "";
            txtCollegeName.Text = "";

            string msg = "Thank you Student Request Sent To Admin";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch (Exception ex)
        {
            string msg = ex.Message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }

    protected void btnClickForTshirt_Click(object sender, EventArgs e)
    {
        try
        {
            // BLobj.Student_ApplyForTshirt(cook.LeadId());

        }
        catch (Exception)
        {

        }
    }

    protected void btnTshirt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = BLobj.Student_GetTshirtAppliedDetails(cook.LeadId());
            if (dt.Rows.Count > 0)
            {
                string TshirtSize = dt.Rows[0].ItemArray[2].ToString();
                lblTshirtRequestedId.Text = dt.Rows[0].ItemArray[3].ToString();
                if (TshirtSize == "S")
                {
                    rdoS.Checked = true;
                }
                else if (TshirtSize == "M")
                {
                    rdoM.Checked = true;
                }
                else if (TshirtSize == "L")
                {
                    rdoL.Checked = true;
                }
                else if (TshirtSize == "XL")
                {
                    rdoXL.Checked = true;
                }
                else if (TshirtSize == "XXL")
                {
                    rdoXXL.Checked = true;
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Tshirt();", true);

        }
        catch (Exception)
        {

        }
    }

    protected void btnSendTshirtRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if ((rdoS.Checked == false) && (rdoM.Checked == false) && (rdoL.Checked == false) && (rdoXL.Checked == false) && (rdoXXL.Checked == false))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Tshirt();", true);

                string msg = "Select T-shirt Size";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg + "')", true);
            }
            else
            {
                string Manager_Id = BLobj.Student_GetManagerIdAfterProfileUpdate(cook.LeadId());
                string Size = "";
                if (rdoS.Checked == true)
                {
                    Size = "S";
                }
                else if (rdoM.Checked == true)
                {
                    Size = "M";
                }
                else if (rdoL.Checked == true)
                {
                    Size = "L";
                }
                else if (rdoXL.Checked == true)
                {
                    Size = "XL";
                }
                else if (rdoXXL.Checked == true)
                {
                    Size = "XXL";
                }
                BLobj.Student_ApplyForTshirt(cook.RegistrationId(), cook.LeadId(), Manager_Id.ToString(), lblStudentName.Text.ToString(), Size.ToString(), "Student", cook.AcademicYear(), lblTshirtRequestedId.Text.ToString());
                string msg = "Sent request for T-shirt";
                rptMultipleTshirtRequest.DataSource = BLobj.Student_GetMultipleTshirtRequestRepeater(cook.LeadId());
                rptMultipleTshirtRequest.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                if (dt2.Rows.Count > 0)
                {
                    if ((dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                    {
                        btnTshirt.Attributes["data-toggle"] = "tooltip";
                        btnTshirt.Attributes["data-placement"] = "top";
                        btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                        btnTshirt.Visible = true;

                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");

                    }
                    else if ((dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                    {
                        btnTshirt.Attributes["data-toggle"] = "tooltip";
                        btnTshirt.Attributes["data-placement"] = "top";
                        btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                        btnTshirt.Visible = true;
                        tabTshirt.Attributes.Add("class", "hidden");
                        TshirtTab.Attributes.Add("class", "hidden");


                    }
                    else if ((dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                    {

                        tabTshirt.Attributes.Add("class", "");
                        TshirtTab.Attributes.Add("class", "hidden");
                        btnTshirt.Visible = false;

                    }
                }
                else
                {
                    tabTshirt.Attributes.Add("class", "hidden");
                    TshirtTab.Attributes.Add("class", "hidden");
                    btnTshirt.Visible = false;
                }

                if (lblTshirtRequestedId.Text.ToString() != "")
                {
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request modified", "studenttshirtrequested", "");
                    string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request modified", "studenttshirtrequested", "Empty");
                    }

                    DeviceID = BLobj.Manager_GetDeviceID(cook.ManagerId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), cook.LeadId() + "-" + "T-shirt request modified", "pmtshirt", "Empty");
                    }

                }
                else
                {
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request submitted successfully", "studenttshirtrequested", "");
                    string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request submitted successfully", "studenttshirtrequested", "Empty");
                    }

                    DeviceID = BLobj.Manager_GetDeviceID(cook.ManagerId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), cook.LeadId() + "-" + "T-shirt request submitted successfully", "pmtshirt", "Empty");
                    }

                }
            }

        }
        catch (Exception)
        {

        }
    }
    protected void btnRequestNewTshirt_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_StudentTshirt();", true);
            string msg = "Select T-shirt Size";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "info('" + msg + "')", true);
        }
        catch (Exception)
        {

        }
    }
    protected void btnSubmitStudentTshirtRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if ((rdoS1.Checked == false) && (rdoM1.Checked == false) && (rdoL1.Checked == false) && (rdoXL1.Checked == false) && (rdoXXL1.Checked == false))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_StudentTshirt();", true);

                string msg = "Select T-shirt Size";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg + "')", true);
            }
            else
            {
                string Manager_Id = BLobj.Student_GetManagerIdAfterProfileUpdate(cook.LeadId());
                string Size = "";
                if (rdoS1.Checked == true)
                {
                    Size = "S";
                }
                else if (rdoM1.Checked == true)
                {
                    Size = "M";
                }
                else if (rdoL1.Checked == true)
                {
                    Size = "L";
                }
                else if (rdoXL1.Checked == true)
                {
                    Size = "XL";
                }
                else if (rdoXXL1.Checked == true)
                {
                    Size = "XXL";
                }
                BLobj.Student_MultipleTshirtRequestApply(cook.RegistrationId(), cook.LeadId(), Manager_Id.ToString(), lblStudentName.Text.ToString(), Size.ToString(), "Student", cook.AcademicYear(), lblMultipleTshirtRequest.Text.ToString(), ddlStudentResonForReApply.SelectedValue.ToString());

                tabTshirt.Attributes.Add("class", "active");
                TshirtTab.Attributes.Add("class", "tab-pane active");

                tabProject.Attributes.Add("class", "");
                ProjectTab.Attributes.Add("class", "hidden");

                tabprofile.Attributes.Add("class", "");
                profiletab.Attributes.Add("class", "hidden");

                tabEvent.Attributes.Add("class", "");
                EventTab.Attributes.Add("class", "hidden");

                rptMultipleTshirtRequest.DataSource = BLobj.Student_GetMultipleTshirtRequestRepeater(cook.LeadId());
                rptMultipleTshirtRequest.DataBind();

                if (lblMultipleTshirtRequest.Text.ToString() != "")
                {
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request modified", "studenttshirtrequested", "");
                    string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request modified", "studenttshirtrequested", "Empty");
                    }
                    DeviceID = BLobj.Manager_GetDeviceID(cook.ManagerId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), cook.LeadId() + "-" + "T-shirt request modified", "pmtshirt", "Empty");
                    }
                }
                else
                {
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request submitted successfully", "studenttshirtrequested", "");
                    string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), cook.LeadId() + "-" + "T-shirt request submitted successfully", "pmtshirt", "Empty");
                    }
                    DeviceID = BLobj.Manager_GetDeviceID(cook.ManagerId());
                    if (DeviceID != "")
                    {
                        FCMPushNotification.AndroidPush(DeviceID.ToString(), cook.LeadId() + "-" + "T-shirt request modified", "pmtshirt", "Empty");
                    }

                }
                string msg = "Sent request for T-shirt";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void rptMultipleTshirtRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblSanctionStatus = (e.Item.FindControl("lblSanctionStatus") as Label);
                Label lblRequestedId = (e.Item.FindControl("lblRequestedId") as Label);
                LinkButton btnEditTshirt = (e.Item.FindControl("btnEditTshirt") as LinkButton);
                if (lblSanctionStatus.Text == "0")
                {
                    btnEditTshirt.Attributes["data-toggle"] = "tooltip";
                    btnEditTshirt.Attributes["data-placement"] = "top";
                    btnEditTshirt.Attributes["title"] = "Waiting for Manager Approval";
                    btnEditTshirt.Text = "<span class='fa fa-pencil text-info'></span>";
                    btnEditTshirt.Enabled = true;
                    btnRequestNewTshirt.Visible = false;
                }
                else if (lblSanctionStatus.Text == "1")
                {
                    btnEditTshirt.Attributes["data-toggle"] = "tooltip";
                    btnEditTshirt.Attributes["data-placement"] = "top";
                    btnEditTshirt.Attributes["title"] = "Approved";
                    btnEditTshirt.Text = "<span class='fa fa-thumbs-up text-primary'></span>";
                    btnEditTshirt.Enabled = false;
                    btnRequestNewTshirt.Visible = true;
                }
                else if (lblSanctionStatus.Text == "2")
                {
                    btnEditTshirt.Attributes["data-toggle"] = "tooltip";
                    btnEditTshirt.Attributes["data-placement"] = "top";
                    btnEditTshirt.Attributes["title"] = "Rejected Refer Remark";
                    btnEditTshirt.Text = "<span class='fa fa-thumbs-down text-danger'></span>";
                    btnEditTshirt.Enabled = false;
                    btnRequestNewTshirt.Visible = true;
                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void rptMultipleTshirtRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            Label lblSanctionStatus = (e.Item.FindControl("lblSanctionStatus") as Label);
            Label lblRequestedId = (e.Item.FindControl("lblRequestedId") as Label);
            LinkButton btnEditTshirt = (e.Item.FindControl("btnEditTshirt") as LinkButton);
            lblMultipleTshirtRequest.Text = lblRequestedId.Text.ToString();
            DataTable dt = BLobj.Student_GetMultipleTshirtAppliedDetails(lblRequestedId.Text);
            if (dt.Rows.Count > 0)
            {
                string TshirtSize = dt.Rows[0].ItemArray[2].ToString();
                lblTshirtRequestedId.Text = dt.Rows[0].ItemArray[3].ToString();
                if (TshirtSize == "S")
                {
                    rdoS1.Checked = true;
                }
                else if (TshirtSize == "M")
                {
                    rdoM1.Checked = true;
                }
                else if (TshirtSize == "L")
                {
                    rdoL1.Checked = true;
                }
                else if (TshirtSize == "XL")
                {
                    rdoXL1.Checked = true;
                }
                else if (TshirtSize == "XXL")
                {
                    rdoXXL1.Checked = true;
                }
            }

            ddlStudentResonForReApply.SelectedIndex = ddlStudentResonForReApply.Items.IndexOf(ddlStudentResonForReApply.Items.FindByValue(dt.Rows[0].ItemArray[4].ToString()));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_StudentTshirt();", true);
            string msg = "you can modify T-shirt Size";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "info('" + msg + "')", true);
        }
        catch (Exception)
        {

        }
    }


    [System.Web.Services.WebMethod]
    public static void Logout()
    {
        try
        {
            LeadBL BLobj = new LeadBL();
            vmCookies cook = new vmCookies();

            BLobj.Student_Update_Login_Log(cook.LeadId());
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("Registration_Id", ""));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("Student_LeadId", ""));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("Student_ManagerId", ""));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("Student_MobileNo", ""));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("Student_AcademicYear", ""));
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("Student_isProfileCompleted", ""));

            // HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        catch (Exception)
        {

        }
    }

    [System.Web.Services.WebMethod]
    public static void ResetNotification()
    {
        try
        {
            LeadBL BL = new LeadBL();
            vmCookies cook = new vmCookies();
            BL.Student_Update_Notification_Log(cook.LeadId());
        }
        catch (Exception)
        {

        }
    }

    protected void btnProjectRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptStudentProposedList);
            // StudentProgressBar();
            Student_Apply_NextLevel();
            StudentLevels();


        }
        catch (Exception)
        {

        }
    }


    [System.Web.Services.WebMethod]
    public static string NotificationAll()
    {
        try
        {
            LeadBL BL = new LeadBL();
            return BL.NotificationAll();
        }
        catch (Exception)
        {
            return "";
        }
    }



    protected void btnGeneralRequest_Click(object sender, EventArgs e)
    {
        try
        {
            tabProject.Attributes.Add("class", "");
            ProjectTab.Attributes.Add("class", "hidden");

            tabprofile.Attributes.Add("class", "");
            profiletab.Attributes.Add("class", "hidden");

            tabEvent.Attributes.Add("class", "");
            EventTab.Attributes.Add("class", "hidden");


            DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
            if (dt2.Rows.Count > 0)
            {
                if ((dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                {
                    btnTshirt.Attributes["data-toggle"] = "tooltip";
                    btnTshirt.Attributes["data-placement"] = "top";
                    btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                    btnTshirt.Visible = true;

                    tabTshirt.Attributes.Add("class", "hidden");
                    TshirtTab.Attributes.Add("class", "hidden");

                }
                else if ((dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                {
                    btnTshirt.Attributes["data-toggle"] = "tooltip";
                    btnTshirt.Attributes["data-placement"] = "top";
                    btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                    btnTshirt.Visible = true;
                    tabTshirt.Attributes.Add("class", "hidden");
                    TshirtTab.Attributes.Add("class", "hidden");


                }
                else if ((dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                {

                    tabTshirt.Attributes.Add("class", "");
                    TshirtTab.Attributes.Add("class", "hidden");
                    btnTshirt.Visible = false;

                }
            }
            else
            {
                tabTshirt.Attributes.Add("class", "hidden");
                TshirtTab.Attributes.Add("class", "hidden");
                btnTshirt.Visible = false;
            }
        }
        catch (Exception)
        {


        }
    }
    protected void btnRefreshTshirt_Click(object sender, EventArgs e)
    {
        try
        {
            rptMultipleTshirtRequest.DataSource = BLobj.Student_GetMultipleTshirtRequestRepeater(cook.LeadId());
            rptMultipleTshirtRequest.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnChat_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string PDID = "";

            string code = e.CommandArgument.ToString();
            string[] itemlist = code.Split('^');
            int i;
            for (i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    PDID = itemlist[0].ToString();
                }
                else if (i == 1)
                {
                    lblChatProjectTitle.Text = itemlist[1].ToString();
                }
                else if (i == 2)
                {
                    lblChatProjectStatus.Text = itemlist[2].ToString();
                }
            }
            BLobj.Student_UpdateChatRead(PDID.ToString(), "Manager");
            lblChatPDID.Text = PDID.ToString();
            DataTable dt = BLobj.Student_GetProjectDiscussionForum(PDID.ToString());
            rptDiscussionForum.DataSource = dt;
            rptDiscussionForum.DataBind();
            txtChatMessage.Text = "";
            BLobj.Student_GetProjectList(cook.LeadId().ToString(), rptStudentProposedList);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Chat();", true);
        }
        catch (Exception)
        {


        }
    }

    protected void btnSaveCommentChat_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_SaveProjectDiscussionForum(lblChatPDID.Text.ToString(), txtChatMessage.Text.ToString(), cook.RegistrationId(), lblChatProjectStatus.Text.ToString());
            string msg = "Message Sent!";
            txtChatMessage.Text = " ";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            //DataTable dt = BLobj.Student_GetProjectDiscussionForum(lblChatPDID.Text.ToString());
            //rptDiscussionForum.DataSource = dt;
            //rptDiscussionForum.DataBind();


        }
        catch (Exception)
        {
            throw;
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

            throw ex;
        }
    }
    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = BLobj.Student_GetProjectDiscussionForum(lblChatPDID.Text.ToString());
    //        rptDiscussionForum.DataSource = dt;
    //        rptDiscussionForum.DataBind();
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

    //}


    //public int ChatCount(string PDID)
    //{
    //    int UnReadCount = BLobj.Common_GetChatUnreadMessageCount(PDID.ToString());

    //    return UnReadCount;
    //}
}

public class Meterials
{
    public string MeterialName { get; set; }
    public string MeterialCost { get; set; }
    public string Slno { get; set; }
}
public class TeamMembers
{
    public string MemberName { get; set; }
    public string MemberMailId { get; set; }
    public string MemberMobileNo { get; set; }
    public string Gender { get; set; }
    public string Slno { get; set; }
}