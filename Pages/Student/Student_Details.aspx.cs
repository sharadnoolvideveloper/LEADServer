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

public partial class Pages_Student_Student_Details : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_EligibleForMasterLeader();", true);
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
                
                    BLobj.Student_SetStudentProfileImage(cook.LeadId(), PreviewImage);
                    int isProfileEdit = BLobj.CheckProfileisEditedMode(cook.LeadId());
                    Student_Apply_NextLevel();
                    StudentLevels();
                    DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                    if (dt2.Rows.Count > 0)
                    {
                        if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                            btnTshirt.Visible = true;
                      
                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                        {
                            btnTshirt.Attributes["data-toggle"] = "tooltip";
                            btnTshirt.Attributes["data-placement"] = "top";
                            btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
                            btnTshirt.Visible = true;
                       
                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                        {
                        
                            btnTshirt.Visible = false;
                        }
                        else if ((int.Parse(dt2.Rows[0].ItemArray[0].ToString()) > 0) && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "2"))
                        {

                        
                            btnTshirt.Visible = false;

                        }
                        else
                        {
                          
                            btnTshirt.Visible = false;
                        }
                    }
                    else
                    {
                      
                        btnTshirt.Visible = false;
                    }                
                StudentBadges();
                    GetRatingDetails();
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
    public void StudentBadges()
    {
        DataTable dt = BLobj.Student_GetBadges(cook.LeadId());
        if (dt.Rows[0].ItemArray[0].ToString() != "0")
        {
            pnlLLP.Visible = true;
            lblLLPBadge.Text = dt.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            pnlLLP.Visible = false;
        }
        if (dt.Rows[0].ItemArray[1].ToString() != "0")
        {
            pnlPrayana.Visible = true;
            lblPrayanaBadges.Text = dt.Rows[0].ItemArray[1].ToString();
        }
        else
        {
            pnlPrayana.Visible = false;
        }
        if (dt.Rows[0].ItemArray[2].ToString() != "0")
        {
            pnlYuvaSummit.Visible = true;
            lblYuvaBadges.Text = dt.Rows[0].ItemArray[2].ToString();
        }
        else
        {
            pnlYuvaSummit.Visible = false;
        }
        if (dt.Rows[0].ItemArray[3].ToString() != "0")
        {
            pnlValidatory.Visible = true;
            lblValidatoryBadge.Text = dt.Rows[0].ItemArray[3].ToString();
        }
        else
        {
            pnlValidatory.Visible = false;
        }
    }
    public void GetRatingDetails()
    {
      
        DataTable dt=  BLobj.Student_GetRatingsDetails(cook.LeadId());
      
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == "5")
                {
                    lbl5StarCount.Text = dt.Rows[i].ItemArray[1].ToString();
                }
              else if (dt.Rows[i].ItemArray[0].ToString() == "4")
                {
                    lbl4StarCount.Text = dt.Rows[i].ItemArray[1].ToString();
                }
              else  if (dt.Rows[i].ItemArray[0].ToString() == "3")
                {
                    lbl3StarCount.Text = dt.Rows[i].ItemArray[1].ToString();
                }
                else if (dt.Rows[i].ItemArray[0].ToString() == "2")
                {
                    lbl2starCount.Text = dt.Rows[i].ItemArray[1].ToString();
                }
                else if (dt.Rows[i].ItemArray[0].ToString() == "1")
                {
                    lbl1StarCount.Text = dt.Rows[i].ItemArray[1].ToString();
                }
               
            }
         

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
            imgStudentLevel.ImageUrl = "../../../CSS/Images/Level/Lead Ambassador.gif";
            imgStudentLevel.Attributes["title"] = "Successfully Reached " + LeadAmbassador.ToString();
            imgStudentLevel.Attributes["data-toggle"] = "tooltip";
            imgStudentLevel.Attributes["data-placement"] = "top";
            lblStudentType.Text = "Lead Ambassador";
        }
        else if((MasterLeader==true) && (LEADer == true))
        {
            imgStudentLevel.ImageUrl = "../../../CSS/Images/Level/Master Leader.gif";
            imgStudentLevel.Attributes["title"] = "Successfully Reached Master Leader";
            imgStudentLevel.Attributes["data-toggle"] = "tooltip";
            imgStudentLevel.Attributes["data-placement"] = "top";
            lblStudentType.Text = "Master Leader";
        }
        else if ((MasterLeader == true) && (LEADer == false))
        {
            imgStudentLevel.ImageUrl = "../../../CSS/Images/Level/MasterWithoutLeader.gif";
            imgStudentLevel.Attributes["title"] = "Successfully Reached Master Leader";
            imgStudentLevel.Attributes["data-toggle"] = "tooltip";
            imgStudentLevel.Attributes["data-placement"] = "top";
            lblStudentType.Text = "Master Leader";
        }
        else if ((LEADer == true) && (MasterLeader == false))
        {
            imgStudentLevel.ImageUrl = "../../../CSS/Images/Level/LEADer.gif";
            imgStudentLevel.Attributes["title"] = "Successfully Reached LEADer";
            imgStudentLevel.Attributes["data-toggle"] = "tooltip";
            imgStudentLevel.Attributes["data-placement"] = "top";
            lblStudentType.Text = "LEADer";
        }
        else if (ChangeMaker == true)
        {
            imgStudentLevel.ImageUrl = "../../../CSS/Images/Level/Change Maker.gif";
            imgStudentLevel.Attributes["title"] = "Successfully Reached Change Maker";
            imgStudentLevel.Attributes["data-toggle"] = "tooltip";
            imgStudentLevel.Attributes["data-placement"] = "top";
            lblStudentType.Text = "Change Maker";
        }
        else if (Initiator == true)
        {
            imgStudentLevel.ImageUrl = "../../../CSS/Images/Level/Initiator.gif";
            imgStudentLevel.Attributes["title"] = "Successfully Reached Initiator";
            imgStudentLevel.Attributes["data-toggle"] = "tooltip";
            imgStudentLevel.Attributes["data-placement"] = "top";
            lblStudentType.Text = "Initiator";
        }
        else if (Student == true)
        {
            imgStudentLevel.ImageUrl = "../../../CSS/Images/Level/Student.gif";
            imgStudentLevel.Attributes["title"] = "Complte 1 project to Start Journey of LEAD";
            imgStudentLevel.Attributes["data-toggle"] = "tooltip";
            imgStudentLevel.Attributes["data-placement"] = "top";
            lblStudentType.Text = "Student";
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
        else if ((CompletedProjectCount >= 3) && (TotalMonths >= 6) && (StudenType.ToString() != "Lead Ambassador") || (ImpactProjectCount >= 1) )
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
        else if ((TotalMasterLeaderMonths >= 6) && (StudenType == "Master Leader") || (StudenType == "LEADer"))
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
    protected void pnlVisibility(bool Student, bool Master, bool Ambassador, bool Tshirt)
    {
        pnlStudent.Visible = Student;
        pnlMasterLeader.Visible = Master;
        pnlLeadAmbassador.Visible = Ambassador;
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
             
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                DataTable dt2 = BLobj.Student_GetTshirtLevel(cook.LeadId());
                if ((dt2.Rows[0].ItemArray[0].ToString() != "Student") && (dt2.Rows[0].ItemArray[1].ToString() == "0") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                {
                    btnTshirt.Attributes["data-toggle"] = "tooltip";
                    btnTshirt.Attributes["data-placement"] = "top";
                    btnTshirt.Attributes["title"] = "you are eligible for Tshirt Click to Apply";
                    btnTshirt.Visible = true;


                }
                else if ((dt2.Rows[0].ItemArray[0].ToString() != "Student") && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "0"))
                {
                    btnTshirt.Attributes["data-toggle"] = "tooltip";
                    btnTshirt.Attributes["data-placement"] = "top";
                    btnTshirt.Attributes["title"] = "Waiting for Manager Approval";
               
                }
                else if ((dt2.Rows[0].ItemArray[0].ToString() != "Student") && (dt2.Rows[0].ItemArray[1].ToString() == "1") && (dt2.Rows[0].ItemArray[2].ToString() == "1"))
                {

                    btnTshirt.Visible = false;

                }
                else
                {
                    btnTshirt.Visible = false;
                
                }

                if (lblTshirtRequestedId.Text.ToString() != "")
                {
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request modified", "ReApply", "");
                    //     string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    //  FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request modified", "Request", "Empty");

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
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request submitted successfully", "Proposed", "");
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
            BLobj.Student_ApplyForLeadAmbassador(cook.LeadId());
            Response.Redirect("StudentProfile.aspx");

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


                if (lblMultipleTshirtRequest.Text.ToString() != "")
                {
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request modified", "ReApply", "");
                    string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request modified", "Request", "Empty");
                }
                else
                {
                    BLobj.Manager_SaveNotificationLog(cook.ManagerId(), cook.LeadId(), Size.ToString() + " " + ":T-shirt request submitted successfully", "Proposed", "");
                    string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "T-shirt request submitted successfully", "Request", "Empty");
                }
                string msg = "Sent request for T-shirt";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
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


    protected void btnMLAgree_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {
         
        }
    }

}