using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_Student_ProjectCompletion : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    public  int imgdelcount = 0;
    public  string imgslno = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {

                if ((Request.QueryString["PDID"].ToString() != null) || (Request.QueryString["PDID"].ToString() != "")
                    && (Request.QueryString["ProjectStatus"].ToString() != null) || (Request.QueryString["ProjectStatus"].ToString() != ""))
                {
                    BLobj.Student_FillSDG_GoalsListBox(LstSDG_Goals);
                    lblDeleteImageCount.Text = "0";
                    lblDeleteImgSlno.Text = "";
                
                    lblPDID.Text = Request.QueryString["PDID"].ToString();
                    lblProjectStatus.Text= Request.QueryString["ProjectStatus"].ToString();
                    if ((lblProjectStatus.Text == "Approved") || (lblProjectStatus.Text == "Draft") || (lblProjectStatus.Text == "RequestForCompletion") || (lblProjectStatus.Text == "Completed"))
                    {
                        DataTable dt = BLobj.Student_GetCompletionDetailBeforeCompletion_NEW(lblPDID.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            DateTime StartDate;
                            DateTime EndDate;
                            txtCompletionProjectTitle.Text = dt.Rows[0].ItemArray[0].ToString();
                            txtCompletionBeneficiary.Text = dt.Rows[0].ItemArray[1].ToString();
                            txtCompletionPlaceofImplement.Text = dt.Rows[0].ItemArray[2].ToString();
                            txtCompletionFundRaised.Text = dt.Rows[0].ItemArray[3].ToString();
                            txtCompletionChanllengesFaced.Text = dt.Rows[0].ItemArray[4].ToString();
                            txtCompletionLearningFromProject.Text = dt.Rows[0].ItemArray[5].ToString();
                            txtCompletionProjectStory.Text = dt.Rows[0].ItemArray[6].ToString();
                            txtCompletionRequestedAmount.Text = dt.Rows[0].ItemArray[7].ToString();
                            txtCompletionApprovedAmount.Text = dt.Rows[0].ItemArray[8].ToString();
                            txtCompletionProjectObjective.Text = dt.Rows[0].ItemArray[9].ToString();
                            txtResourceUtilize.Text = dt.Rows[0].ItemArray[10].ToString();                          
                            if ((dt.Rows[0].ItemArray[11].ToString() != "0") || (dt.Rows[0].ItemArray[12].ToString() != "0"))
                            {
                                StartDate = DateTime.Parse(dt.Rows[0].ItemArray[11].ToString());
                                EndDate = DateTime.Parse(dt.Rows[0].ItemArray[12].ToString());
                                int Days = int.Parse((EndDate.Date - StartDate.Date).TotalDays.ToString());
                                if (Days == 0)
                                {
                                    Days = 1;
                                }
                                else
                                {
                                    Days = Days + 1;
                                }
                                lblRCTargetDays.Text = Days.ToString();
                                txtRCStartDate.Enabled = false;
                                txtRCEndDate.Enabled = false;
                                txtRCStartDate.Text = dt.Rows[0].ItemArray[11].ToString();
                                txtRCEndDate.Text = dt.Rows[0].ItemArray[12].ToString();
                            }
                            else
                            {
                                txtRCStartDate.Enabled = true;
                                txtRCEndDate.Enabled = true;
                            }
                            if (dt.Rows[0].ItemArray[13].ToString() == "0")
                            {
                                txtCompletionHoursSpend.Text = "";
                            }
                            else
                            {
                                txtCompletionHoursSpend.Text = dt.Rows[0].ItemArray[13].ToString();
                            }
                          txtResourceWorthAmount.Text= dt.Rows[0].ItemArray[24].ToString();
                            string isImpact = BLobj.Student_ProjectIsImpact(lblPDID.Text.ToString());
                            if (isImpact != "0")
                            {
                                if (dt.Rows[0].ItemArray[15].ToString() != "")
                                {
                                    ChkCollabration.Checked = true;
                                    txtCompletionCollabration.Text = dt.Rows[0].ItemArray[15].ToString();
                                }
                                if (dt.Rows[0].ItemArray[16].ToString() != "")
                                {
                                    txtCompletionProcedure.Text = dt.Rows[0].ItemArray[16].ToString();
                                }
                                if (dt.Rows[0].ItemArray[17].ToString() != "")
                                {
                                    txtCompletionInitiativeExperience.Text = dt.Rows[0].ItemArray[17].ToString();
                                }
                                if (dt.Rows[0].ItemArray[18].ToString() != "")
                                {
                                    txtCompletionOvercomeLacking.Text = dt.Rows[0].ItemArray[18].ToString();
                                }
                                if (dt.Rows[0].ItemArray[19].ToString() != "")
                                {
                                    ChkAgainst_Tide.Checked = true;
                                    txtCompletionAgainst_Tide.Text = dt.Rows[0].ItemArray[19].ToString();
                                }
                                if (dt.Rows[0].ItemArray[20].ToString() != "")
                                {
                                    ChkCross_Hurdles.Checked = true;
                                    txtCompletionCross_Hurdles.Text = dt.Rows[0].ItemArray[20].ToString();
                                }
                                if (dt.Rows[0].ItemArray[21].ToString() != "")
                                {
                                    ChkEntrepreneurial_Venture.Checked = true;
                                    txtCompletionEntrepreneurial_Venture.Text = dt.Rows[0].ItemArray[21].ToString();
                                }
                                if (dt.Rows[0].ItemArray[22].ToString() != "")
                                {
                                    ChkGovernment_Awarded.Checked = true;
                                    txtCompletionGovernment_Awarded.Text = dt.Rows[0].ItemArray[22].ToString();
                                }
                                if (dt.Rows[0].ItemArray[23].ToString() != "")
                                {
                                    ChkLeadership_Roles.Checked = true;
                                    txtCompletionLeadership_Roles.Text = dt.Rows[0].ItemArray[23].ToString();
                                }
                                pnl_ImpactQuestions.Visible = true;

                            }
                            else
                            {
                                pnl_ImpactQuestions.Visible = false;
                            }

                        }
                       
                        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", DstStudentImgDocumentList);
                        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", rptDocument2);
                        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "DOC", DstStudentDOCLst);
                    }

                   

                    if (lblProjectStatus.Text == "Approved")
                    {

                        txtCompletionProjectTitle.Enabled = false;
                        txtCompletionBeneficiary.Enabled = false;
                        txtCompletionPlaceofImplement.Enabled = true;
                        txtCompletionFundRaised.Enabled = true;
                        txtCompletionChanllengesFaced.Enabled = true;
                        txtCompletionLearningFromProject.Enabled = true;
                        txtCompletionProjectStory.Enabled = true;
                        txtCompletionRequestedAmount.Enabled = false;
                        txtCompletionApprovedAmount.Enabled = false;
                        txtCompletionProjectObjective.Enabled = false;
                        txtResourceUtilize.Enabled = true;
                        UpdatePanel1.Visible = true;
                        UpdatePanel2.Visible = true;
                        btnDownloadStudentDocument.Visible = false;
                        btnCompletionSubmit.Visible = true;
                        btnCompletionSaveAsDraft.Visible = true;
                    }
                    else if (lblProjectStatus.Text == "RequestForCompletion")
                    {

                        txtCompletionProjectTitle.Enabled = false;
                        txtCompletionBeneficiary.Enabled = false;
                        txtCompletionPlaceofImplement.Enabled = true;
                        txtCompletionFundRaised.Enabled = true;
                        txtCompletionChanllengesFaced.Enabled = true;
                        txtCompletionLearningFromProject.Enabled = true;
                        txtCompletionProjectStory.Enabled = true;
                        txtCompletionRequestedAmount.Enabled = false;
                        txtCompletionApprovedAmount.Enabled = false;
                        txtCompletionProjectObjective.Enabled = false;
                        txtResourceUtilize.Enabled = true;
                        UpdatePanel1.Visible = true;
                        UpdatePanel2.Visible = true;
                        btnDownloadStudentDocument.Visible = false;
                        btnCompletionSubmit.Visible = true;
                        btnCompletionSaveAsDraft.Visible = false;
                        DataTable  dt = BLobj.Common_GetProjectSDG_Goals(lblPDID.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                LstSDG_Goals.Items.FindByValue(dr[0].ToString()).Selected = true;
                            }
                        }
                    }
                    else if (lblProjectStatus.Text == "Draft")
                    {

                        txtCompletionProjectTitle.Enabled = false;
                        txtCompletionBeneficiary.Enabled = false;
                        txtCompletionPlaceofImplement.Enabled = true;
                        txtCompletionFundRaised.Enabled = true;
                        txtCompletionChanllengesFaced.Enabled = true;
                        txtCompletionLearningFromProject.Enabled = true;
                        txtCompletionProjectStory.Enabled = true;
                        txtCompletionRequestedAmount.Enabled = false;
                        txtCompletionApprovedAmount.Enabled = false;
                        txtCompletionProjectObjective.Enabled = false;
                        txtResourceUtilize.Enabled = true;
                        txtCompletionHoursSpend.Enabled = true;
                        UpdatePanel1.Visible = true;
                        UpdatePanel2.Visible = true;
                        btnDownloadStudentDocument.Visible = false;
                        btnCompletionSubmit.Visible = true;
                        btnCompletionSaveAsDraft.Visible = true;
                        DataTable dt = BLobj.Common_GetProjectSDG_Goals(lblPDID.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                LstSDG_Goals.Items.FindByValue(dr[0].ToString()).Selected = true;
                            }
                        }
                    }
                    else if (lblProjectStatus.Text == "Completed")
                    {

                        txtCompletionHoursSpend.Enabled = false;
                        txtCompletionProjectTitle.Enabled = false;
                        txtCompletionBeneficiary.Enabled = false;
                        txtCompletionPlaceofImplement.Enabled = false;
                        txtCompletionFundRaised.Enabled = false;
                        txtCompletionChanllengesFaced.Enabled = false;
                        txtCompletionLearningFromProject.Enabled = false;
                        txtCompletionProjectStory.Enabled = false;
                        txtCompletionRequestedAmount.Enabled = false;
                        txtCompletionApprovedAmount.Enabled = false;
                        txtCompletionProjectObjective.Enabled = false;
                        txtResourceUtilize.Enabled = false;
                        pnl_ImpactQuestions.Enabled = false;
                        UpdatePanel1.Visible = false;
                        UpdatePanel2.Visible = false;
                        btnCompletionSubmit.Visible = false;
                        btnCompletionSaveAsDraft.Visible = false;
                        btnDownloadStudentDocument.Visible = true;
                        txtCompletionHoursSpend.Enabled = false;
                        txtCompletionCollabration.Enabled = false;
                        txtCompletionProcedure.Enabled = false;
                        txtCompletionInitiativeExperience.Enabled = false;
                        txtCompletionOvercomeLacking.Enabled = false;
                        DataTable dt = BLobj.Common_GetProjectSDG_Goals(lblPDID.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                LstSDG_Goals.Items.FindByValue(dr[0].ToString()).Selected = true;
                            }
                        }
                        LstSDG_Goals.Enabled = false;

                    }
                }
            }
        }
        catch (Exception)
        {            
        }
    }
    protected void btnCompletionSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (cook.LeadId() != "")
            {
                int imgCount = 0, DBImgCount = 0;
                string LeadId = cook.LeadId();
                HttpFileCollection fileCollection = Request.Files;
                var files = Enumerable.Range(0, Request.Files.Count).Select(i => Request.Files[i]);
                
                int SelectedCount = 0;
                foreach (var file in files)
                {   
                    if (file.ContentLength>0)
                    {
                        string FileExtenssion = System.IO.Path.GetExtension(file.FileName);
                        if((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF"))
                        {
                            SelectedCount = SelectedCount + 1;
                        } 
                    }
                }
                if (Validate_Del_Img(SelectedCount) == 1)
                {

                }
                else if (Validate_Controls(SelectedCount) == 1)
                {

                }
                else
                {
                    DataLL DL = new DataLL();
                    int Status = 0;
                    WEB_StudentProjectCompletion vmCompletion = (WEB_StudentProjectCompletion)getValues("RequestForCompletion");
                    Status = DL.WEB_UpdateProjectCompletions(vmCompletion);
                    if (Status > 0)
                    {
                        BLobj.Commong_SaveProjectSDG_Goals(lblPDID.Text.ToString(), LstSDG_Goals);
                        BLobj.Student_InsertNotification(LeadId.ToString(), "Update of ReApply", "Updated ReApply Project (Student)");
                        if (int.Parse(lblDeleteImageCount.Text.ToString()) > 0)
                        {
                            int Result = BLobj.Common_Delete_ProjectDocument(lblDeleteImgSlno.Text.ToString().TrimEnd(','));
                        }     
                    }

                    DBImgCount = int.Parse(BLobj.Student_GetUploadedImgCount(lblPDID.Text.ToString()));


                    if ((DBImgCount >= 4) && (DBImgCount < 11))
                    {
                        if (Request.Files != null)
                        {
                            string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                            BLobj.Student_SaveDocumentImages(LeadId.ToString(), lblPDID.Text.ToString(), fileCollection, FilePath.ToString(), imgCount);

                            BLobj.Student_UpdateCompletionStatus(LeadId.ToString(), lblPDID.Text.ToString());
                            DataTable dt = BLobj.Student_GetStudentDetailForMailSend(cook.LeadId());
                            string Mailid = dt.Rows[0].ItemArray[0].ToString();
                            if (Mailid.ToString() != "")
                            {
                                vmMailNotification vm = new vmMailNotification();
                                vm = BLobj.Student_GetProjectDetailsForMailAndNotifications(lblPDID.Text.ToString());
                                string body = PopulateBody(vm.LeadId.ToString(),
                                "<b>Project Completion Request Sent to Your Mentor Successfully</b>", "The details you entered are listed below: ",
                                "<ol><li><b>LEAD Id:</b> " + vm.LeadId.ToString() + "<br /><br /></li><li><b>Mobile No.:</b> " + cook.Student_MobileNo() +
                                "<br /><br /></li><li><b>Title of the Project:</b> " + vm.Title.ToString() + "<br /><br /></li><li><b>Beneficieries:</b> " + vm.BeneficiaryNo.ToString() + "<br /><br /></li><li><b>Objectives:</b> " + vm.Objectives.ToString() + "<br /><br /></li><li><b>Action Plan:</b> " + vm.ActionPlan.ToString() + "</li></ol><br /><br />");
                                SendHtmlFormattedEmail(Mailid.ToString(), "Project Completion Request", body);
                            }
                            //   SendEmailProposed();
                            string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                            if (DeviceID != "")
                            {
                                FCMPushNotification.AndroidPush(DeviceID.ToString(), "Project Completion Request Sent To Your Mentor Successfully", "Student", "");
                            }

                            Response.Redirect("StudentProfile.aspx");
                        }
                    }
                    else if ((DBImgCount >= 0) && (DBImgCount <= 3))
                    {
                        for (int i = 0; i < fileCollection.Count; i++)
                        {
                            HttpPostedFile uploadfile = fileCollection[i];
                            string fileName = Path.GetFileName(uploadfile.FileName);
                            string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);
                            if ((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF") || (FileExtenssion.ToString() == ".psd") || (FileExtenssion.ToString() == ".PSD"))
                            {
                                imgCount = imgCount + 1;
                                DBImgCount = DBImgCount + 1;
                            }
                        }
                        if ((imgCount >= 4) && (imgCount < 11) || (DBImgCount < 11))
                        {
                            if (Request.Files != null)
                            {
                                string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                                BLobj.Student_SaveDocumentImages(LeadId.ToString(), lblPDID.Text.ToString(), fileCollection, FilePath.ToString(), imgCount);
                                BLobj.Student_UpdateCompletionStatus(LeadId.ToString(), lblPDID.Text.ToString());
                            }
                            //  string msg = "Submission of Digital Documents Successfully ";
                            //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                            //    BLobj.Student_GetProjectList(cook.LeadId(), rptStudentProposedList);
                            DataTable dt = BLobj.Student_GetStudentDetailForMailSend(cook.LeadId());
                            string Mailid = dt.Rows[0].ItemArray[0].ToString();
                            if (Mailid.ToString() != "")
                            {
                                vmMailNotification vm = new vmMailNotification();
                                vm = BLobj.Student_GetProjectDetailsForMailAndNotifications(lblPDID.Text.ToString());
                                string body = PopulateBody(vm.LeadId.ToString(),
                                "<b>Project Completion Request Sent to Your Mentor Successfully</b>", "The details you entered are listed below: ",
                                "<ol><li><b>LEAD Id:</b> " + vm.LeadId.ToString() + "<br /><br /></li><li><b>Mobile No.:</b> " + cook.Student_MobileNo() +
                                "<br /><br /></li><li><b>Title of the Project:</b> " + vm.Title.ToString() + "<br /><br /></li><li><b>Beneficieries:</b> " + vm.BeneficiaryNo.ToString() + "<br /><br /></li><li><b>Objectives:</b> " + vm.Objectives.ToString() + "<br /><br /></li><li><b>Action Plan:</b> " + vm.ActionPlan.ToString() + "</li></ol><br /><br />");
                                SendHtmlFormattedEmail(Mailid.ToString(), "Project Completion Request", body);
                            }
                            //   SendEmailProposed();
                            string DeviceID = BLobj.Common_GetDeviceID(cook.LeadId());
                            if (DeviceID != "")
                            {
                                FCMPushNotification.AndroidPush(DeviceID.ToString(), "Project Completion Request Sent To Your Mentor Successfully", "Student", "");
                            }

                            Response.Redirect("StudentProfile.aspx");
                        }
                        else
                        {
                            //lblProjectCompletionTitle.Text = "Project Completion Form";
                            //   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_AddProjectCompletion();", true);
                            string msg = "Select Alteast 4 Images At a Time and less than 11 image";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                        }
                    }
                    else
                    {
                        string msg = "Select Images";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    }

                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionOut=True");
            }
        }
        catch (Exception ex)
        {
            string msg = "Upload Image Once Again. ";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }
    public int Validate_Controls(int FileCollection)
    {
        if (LstSDG_Goals.SelectedIndex == -1)
        {
            LstSDG_Goals.Focus();

            string msg = "Select SDG Goals";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            return 1;
        }
        int DBImgCount = 0;
        DBImgCount = int.Parse(BLobj.Student_GetUploadedImgCount(lblPDID.Text.ToString()));

         if (DBImgCount <= 0) 
        {
            string msg = "";
            if (FileCollection < 4)
            {
                return 1;
            }
        }
         else if(DBImgCount == 1)
        {
            string msg = "";
            if (FileCollection >=3)
            {
                return 0;
            }
            else
            {
                msg = "Select Min 3 Images";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                return 1;
            }
        }
        else if (DBImgCount == 2)
        {
            string msg = "";
            if (FileCollection >= 2)
            {
                return 0;
            }
            else
            {

                msg = "Select Min 2 Images";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                return 1;
            }
        }
        else if (DBImgCount == 3)
        {
            string msg = "";
            if ((FileCollection >= 1))
            {
                return 0;
            }
            else
            {
                msg = "Select Min 1 Images";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                return 1;
            }
        }
     
        string isImpact = BLobj.Student_ProjectIsImpact(lblPDID.Text.ToString());
        if (isImpact != "0")
        {
            if (ChkCollabration.Checked == true)
            {
                if (txtCompletionCollabration.Text == "")
                {
                    txtCompletionCollabration.Focus();

                    string msg = "Enter Collabration";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }
            }
            if (txtCompletionProcedure.Text == "")
            {
                txtCompletionProcedure.Focus();
                string msg = "Enter Procedure";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                return 1;
            }
            if (txtCompletionInitiativeExperience.Text == "")
            {
                txtCompletionInitiativeExperience.Focus();
                string msg = "Enter Initiative Experience";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                return 1;
            }
            if (txtCompletionOvercomeLacking.Text == "")
            {
                txtCompletionOvercomeLacking.Focus();
                string msg = "Enter Overcome Lacking";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                return 1;
            }
            if (ChkAgainst_Tide.Checked == true)
            {
                if (txtCompletionAgainst_Tide.Text == "")
                {
                    txtCompletionAgainst_Tide.Focus();
                    string msg = "Enter Against Tide";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }
            }
            if (ChkCross_Hurdles.Checked == true)
            {
                if (txtCompletionCross_Hurdles.Text == "")
                {
                    txtCompletionCross_Hurdles.Focus();
                    string msg = "Enter Completion Cross_Hurdles";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }
            }
            if (ChkEntrepreneurial_Venture.Checked == true)
            {
                if (txtCompletionEntrepreneurial_Venture.Text == "")
                {
                    txtCompletionEntrepreneurial_Venture.Focus();
                    string msg = "Enter Entrepreneurial Venture";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }
            }
            if (ChkGovernment_Awarded.Checked == true)
            {
                if (txtCompletionGovernment_Awarded.Text == "")
                {
                    txtCompletionGovernment_Awarded.Focus();
                    string msg = "Enter Government Awards";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }
            }
            if (ChkLeadership_Roles.Checked == true)
            {
                if (txtCompletionLeadership_Roles.Text == "")
                {
                    txtCompletionLeadership_Roles.Focus();
                    string msg = "Enter Leadership Roles";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }
            }

           
        }
        else
        {
            return 0;
        }

      
        return 0;

    }

    public int Validate_Del_Img(int FileCollection)
    {
        int DBImgCount = 0;
        int sumofimage = 0;
      
        string msg = "";
        DBImgCount = int.Parse(BLobj.Student_GetUploadedImgCount(lblPDID.Text.ToString()));
        if (lblDeleteImageCount.Text == "")
        {
            lblDeleteImageCount.Text = "0";
        }
        if (int.Parse(lblDeleteImageCount.Text.ToString())>0)
        {
            sumofimage = DBImgCount - int.Parse(lblDeleteImageCount.Text.ToString());

            if (sumofimage <= 0)
            {
                if (FileCollection >= 4)
                {
                    return 0;
                }
                else
                {
                  
                    msg = "Required Min 4 Images upload remaining images at a time.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }

            }
           else if (sumofimage == 1)
            {
                if (FileCollection >=3)
                {
                    return 0;
                }
                else
                {
                    msg = "add 3 Images At a time";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }

            }
            else if (sumofimage == 2)
            {
                if (FileCollection >= 2)
                {
                    return 0;
                }
                else
                {
                    msg = "add 2 Images  At a time";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }

            }
            else if (sumofimage == 3)
            {
                if (FileCollection >= 1)
                {
                    return 0;
                }
                else
                {
                    msg = "add 1 Image";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    return 1;
                }

            }
            else
            {
                return 0;
            }
        }      
        else
        {
            return 0;
        }           
      
    }


    protected void btnCompletionSaveAsDraft_Click(object sender, EventArgs e)
    {
        try
        {
            if (cook.LeadId() != "")
            {
                int imgCount = 0, DBImgCount = 0, Progress = 0, temp = 0;
                string LeadId = cook.LeadId();
                DataLL DL = new DataLL();
                int Status = 0;
                HttpFileCollection fileCollection = Request.Files;
                string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                if (txtCompletionPlaceofImplement.Text != "")
                {
                    Progress = Progress + 10;
                }
                if (txtCompletionFundRaised.Text != "")
                {
                    Progress = Progress + 10;
                }
                if (txtCompletionChanllengesFaced.Text != "")
                {
                    Progress = Progress + 10;
                }
                if (txtCompletionLearningFromProject.Text != "")
                {
                    Progress = Progress + 10;
                }
                if (txtCompletionProjectStory.Text != "")
                {
                    Progress = Progress + 10;
                }
                if (txtResourceUtilize.Text != "")
                {
                    Progress = Progress + 10;
                }
                DBImgCount = int.Parse(BLobj.Student_GetUploadedImgCount(lblPDID.Text.ToString()));
                if (DBImgCount >= 4)
                {
                    Progress = Progress + 40;
                }
                else if (DBImgCount == 3)
                {
                    Progress = Progress + 30;
                }
                else if (DBImgCount == 2)
                {
                    Progress = Progress + 20;
                }
                else if (DBImgCount == 1)
                {
                    Progress = Progress + 10;
                }
                else if (DBImgCount == 0)
                {
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        HttpPostedFile uploadfile = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfile.FileName);
                        string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);
                        if ((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF") || (FileExtenssion.ToString() == ".psd") || (FileExtenssion.ToString() == ".PSD"))
                        {
                            temp = temp + 1;
                            if (temp <= 4)
                            {
                                Progress = Progress + 10;
                            }
                        }
                    }
                }
                if (DBImgCount == 3)
                {
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        HttpPostedFile uploadfile = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfile.FileName);
                        string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);
                        if ((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF") || (FileExtenssion.ToString() == ".psd") || (FileExtenssion.ToString() == ".PSD"))
                        {
                            temp = temp + 1;
                            if (temp <= 1)
                            {
                                Progress = Progress + 10;
                            }
                        }
                    }
                }
                if (DBImgCount == 2)
                {
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        HttpPostedFile uploadfile = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfile.FileName);
                        string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);
                        if ((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF") || (FileExtenssion.ToString() == ".psd") || (FileExtenssion.ToString() == ".PSD"))
                        {
                            temp = temp + 1;
                            if (temp <= 2)
                            {
                                Progress = Progress + 10;
                            }
                        }
                    }
                }
                if (DBImgCount == 1)
                {
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        HttpPostedFile uploadfile = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfile.FileName);
                        string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);
                        if ((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF") || (FileExtenssion.ToString() == ".psd") || (FileExtenssion.ToString() == ".PSD"))
                        {
                            temp = temp + 1;
                            if (temp <= 3)
                            {
                                Progress = Progress + 10;
                            }
                        }
                    }
                }
                WEB_StudentProjectCompletion vmCompletion = (WEB_StudentProjectCompletion)getValues("Draft");
                vmCompletion.CompletionProgress = Progress.ToString();
                Status = DL.WEB_UpdateProjectCompletions(vmCompletion);
                if (Status > 0)
                {
                    BLobj.Commong_SaveProjectSDG_Goals(lblPDID.Text.ToString(), LstSDG_Goals);
                    BLobj.Student_InsertNotification(LeadId.ToString(), "Drafted Completion Form", "Completion Project Drafted(Student)");
                }
                //  BLobj.Student_UpdateCompletionProjectSaveAsDraft(LeadId.ToString(), PDID.ToString(), txtCompletionPlaceofImplement, txtCompletionFundRaised, txtCompletionChanllengesFaced, txtCompletionLearningFromProject, txtCompletionProjectStory, txtResourceUtilize, Progress, txtRCStartDate, txtRCEndDate, txtCompletionHoursSpend);
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile uploadfile = fileCollection[i];
                    string fileName = Path.GetFileName(uploadfile.FileName);
                    string FileExtenssion = System.IO.Path.GetExtension(uploadfile.FileName);
                    if ((FileExtenssion.ToString() == ".jpg") || (FileExtenssion.ToString() == ".JPG") || (FileExtenssion.ToString() == ".jpeg") || (FileExtenssion.ToString() == ".JPEG") || (FileExtenssion.ToString() == ".png") || (FileExtenssion.ToString() == ".PNG") || (FileExtenssion.ToString() == ".gif") || (FileExtenssion.ToString() == ".GIF") || (FileExtenssion.ToString() == ".psd") || (FileExtenssion.ToString() == ".PSD"))
                    {
                        imgCount = imgCount + 1;
                    }
                }
                int TotalImg = DBImgCount + imgCount;
                string msg = "";
                if (TotalImg < 10)
                {
                    BLobj.Student_SaveDocumentImages(LeadId.ToString(), lblPDID.Text.ToString(), fileCollection, FilePath.ToString(), imgCount);
                    Response.Redirect("StudentProfile.aspx");
                }
                else
                {
                    msg = "Select less than 11 images";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg + "')", true);
                }

             //   BLobj.Student_GetProjectList(cook.LeadId(), rptStudentProposedList);
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionOut=True");
            }
        }
        catch (Exception ex)
        {
            string msg = "Alert : "+ex.Message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }

    public object getValues(string Project_Status)
    {
     
        WEB_StudentProjectCompletion vm = new WEB_StudentProjectCompletion();
        vm.Pdid = lblPDID.Text.ToString();
        vm.Lead_Id = cook.LeadId();
        vm.PlaceofImplementation = txtCompletionPlaceofImplement.Text.ToString();
        if (txtCompletionFundRaised.Text == "")
        {
            vm.FundRaised = 0;
        }
        else
        {
            vm.FundRaised =double.Parse(txtCompletionFundRaised.Text.ToString());
        }
        if (txtResourceWorthAmount.Text == "")
        {
            vm.ResourcesWorthAmount = 0;
        }
        else
        {
            vm.ResourcesWorthAmount = long.Parse(txtResourceWorthAmount.Text.ToString());
        }
        vm.Challenges = txtCompletionChanllengesFaced.Text.ToString();
        vm.Learning = txtCompletionLearningFromProject.Text.ToString();
        vm.AsAStory = txtCompletionProjectStory.Text.ToString();
        vm.Resources = txtResourceUtilize.Text.ToString();
        if (txtCompletionHoursSpend.Text == "")
        {
            vm.HoursSpent = 0;
        }
        else
        {
            vm.HoursSpent = int.Parse(txtCompletionHoursSpend.Text.ToString());
        }
        vm.Projectstatus = Project_Status.ToString();
     //   vm.SDG_Goals = ddlSDGGoals.SelectedValue.ToString();
        if (ChkCollabration.Checked == true)
        {
            vm.Collaboration_Supported = txtCompletionCollabration.Text.ToString();
        }
        else
        {
            vm.Collaboration_Supported = "";
        }
        vm.Permission_And_Activities = txtCompletionProcedure.Text.ToString();
        vm.Experience_Of_Initiative = txtCompletionInitiativeExperience.Text.ToString();
        vm.Lacking_initiative = txtCompletionOvercomeLacking.Text.ToString();
        if (ChkAgainst_Tide.Checked == true)
        {
            vm.Against_Tide = txtCompletionAgainst_Tide.Text.ToString();
        }
        else
        {
            vm.Against_Tide = "";
        }
        if (ChkCross_Hurdles.Checked == true)
        {
            vm.Cross_Hurdles = txtCompletionCross_Hurdles.Text.ToString();
        }
        else
        {
            vm.Cross_Hurdles = "";
        }
        if (ChkEntrepreneurial_Venture.Checked == true)
        {
            vm.Entrepreneurial_Venture = txtCompletionEntrepreneurial_Venture.Text.ToString();
        }
        else
        {
            vm.Entrepreneurial_Venture = "";
        }
        if (ChkGovernment_Awarded.Checked == true)
        {
            vm.Government_Awarded = txtCompletionGovernment_Awarded.Text.ToString();
        }
        else
        {
            vm.Government_Awarded = "";
        }
        if (ChkLeadership_Roles.Checked == true)
        {
            vm.Leadership_Roles = txtCompletionLeadership_Roles.Text.ToString();
        }
        else
        {
            vm.Leadership_Roles = "";
        }
        vm.CompletionProgress = "";
        return vm;
    }
    protected void btnDownloadStudentDocument_Click(object sender, EventArgs e)
    {
        try
        {

            Reports rpt = new Reports();
            string sourcePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
            string Manager_Id = BLobj.Student_GetManagerIdAfterProfileUpdate(cook.LeadId());
            System.Data.DataTable dt = rpt.Student_IndivisualStudentFolderCreating(lblPDID.Text.ToString());
            string Lead_Id = cook.LeadId();
            bool IsManagerExists = System.IO.Directory.Exists(Server.MapPath(targetPath + Manager_Id.ToString()));
            if (!IsManagerExists)
            {
                DirectoryInfo directory = new DirectoryInfo(targetPath);
                System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + Manager_Id.ToString()));
            }
            bool IsLeadIdExists = System.IO.Directory.Exists(Server.MapPath(targetPath + Manager_Id.ToString() + "/LeadID" +Lead_Id.ToString()));
            if (!IsLeadIdExists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + Lead_Id.ToString()));
            }
            else
            {
                System.IO.DirectoryInfo myDirInfo = new DirectoryInfo(Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + Lead_Id.ToString()));

                foreach (FileInfo file in myDirInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in myDirInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            //Array.ForEach(Directory.GetFiles(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblProjectCompletionPOPLeadId.Text.ToString())), Directory.Delete);
            bool isTitleExists = System.IO.Directory.Exists(Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + Lead_Id.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()));

            if (!isTitleExists)
            {

                System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + Lead_Id.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()));
                string dest = (Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + Lead_Id.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()));
                foreach (DataRow dr in dt.Rows)
                {
                    string fileName = System.IO.Path.GetFileName(Server.MapPath(dr[0].ToString()));
                    bool isImgExists = File.Exists(Server.MapPath(sourcePath + fileName));
                    if (isImgExists == true)
                    {
                        string destFile = System.IO.Path.Combine(Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + Lead_Id.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()), fileName);
                        System.IO.File.Copy(Server.MapPath(dr[0].ToString()), destFile, true);
                    }
                }
                //CreatingIndivisualStudentDocument();
                CreateWordDocuments(lblPDID.Text.ToString(), Manager_Id.ToString());
                using (ZipFile zip = new ZipFile())
                {
                    Response.Clear();
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "filename=" + txtCompletionProjectTitle.Text.ToString() + "_" + cook.LeadId() + "_Lead.zip");
                    zip.AddDirectory(Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + Lead_Id.ToString() + "/"));
                    zip.Save(Response.OutputStream);
                    Response.End();
                }
            }
            else
            {
                string msg = "Some thing went wrong with directory";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception ex)
        {

            string msg = "Some thing went wrong with directory" + ex.Message.ToString() + " " + ex.InnerException.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

        }


    }
    public void CreateWordDocuments(string PDId, string Manager_Id)
    {
        Reports rpt = new Reports();
        string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
        string MainFolder = Server.MapPath(targetPath);
        string filename = Server.MapPath(targetPath + Manager_Id.ToString() + "/" + "LeadID" + cook.LeadId() + "/" + txtCompletionProjectTitle.Text.ToString() + "/" + txtCompletionProjectTitle.Text + ".docx");
        rpt.CreateWordDocuments(PDId, targetPath, MainFolder, filename);
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
    protected void DstStudentImgDocumentList_ItemCommand(object source, DataListCommandEventArgs e)
    {
       
        int index = Convert.ToInt32(e.Item.ItemIndex);
        Label lbl = (Label)e.Item.FindControl("lblImageSlno");
        Image imgPath = (Image)e.Item.FindControl("Image1");
        Panel pnl = (Panel)e.Item.FindControl("pnl");
        pnl.Visible = false;
        lblDeleteImgSlno.Text =  lbl.Text.ToString() + "," + lblDeleteImgSlno.Text;
        lblDeleteImageCount.Text = (int.Parse(lblDeleteImageCount.Text) + 1).ToString();
        //lblDeleteImageCount.Text = imgdelcount.ToString();
        //lblDeleteImgSlno.Text = imgslno.ToString();
        

        //int Result = BLobj.Common_Delete_ProjectDocument(lbl.Text.ToString());
        //if (Result > 0)
        //{
        //    if ((System.IO.File.Exists(Server.MapPath(img.ToString()))))
        //    {
        //        System.IO.File.Delete(Server.MapPath(img.ToString()));
        //    }
        //    BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", DstStudentImgDocumentList);
        //    BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", rptDocument2);
        //}
    }

    protected void DstStudentImgDocumentList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDeleteImage");
            if (lblProjectStatus.Text == "Completed")
            {
                btnDelete.Visible = false;
            }
        }
    }
}

public class imgs
{
    public  int slno
    {
        get;set;
     }
    public  string imgpath
    {
        get; set;
    }
   
}