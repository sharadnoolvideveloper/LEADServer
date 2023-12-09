using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_Manager_CompletionProject : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();

    vmCookies cook = new vmCookies();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if ((Request.QueryString["PDID"].ToString() != null) || (Request.QueryString["PDID"].ToString() != "")
                    && (Request.QueryString["ProjectStatus"].ToString() != null) || (Request.QueryString["ProjectStatus"].ToString() != "")
                    && (Request.QueryString["Lead_Id"].ToString() != null) || (Request.QueryString["Lead_Id"].ToString() != ""))
                {
                    if (cook.Manager_Id() != "")
                    {
                        BLobj.Student_FillSDG_GoalsListBox(LstSDG_Goals);
                        lblPDID.Text = Request.QueryString["PDID"].ToString();
                        lblProjectStatus.Text = Request.QueryString["ProjectStatus"].ToString();
                        lblLead_Id.Text = Request.QueryString["Lead_Id"].ToString();

                        string ManagerID = cook.Manager_Id();
                        BLobj.FillThemeMaster(ddlCompletionTheme);
                        txtRCStartDate.Text = "";
                        txtRCEndDate.Text = "";
                        lblRCTargetDays.Text = "";
                        DataTable dt = BLobj.Manager_GetStudentProjectCompletionDetails_NEW(lblLead_Id.Text.ToString(), lblPDID.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            DateTime EndDate;
                            DateTime StartDate;
                            txtCompletionProjectTitle.Text = dt.Rows[0].ItemArray[0].ToString();
                            txtCompletionProjectObjective.Text = dt.Rows[0].ItemArray[1].ToString();
                            txtCompletionBeneficiary.Text = dt.Rows[0].ItemArray[2].ToString();
                            txtCompletionActualBeneficiaries.Text = dt.Rows[0].ItemArray[3].ToString();
                            txtCompletionPlaceofImplement.Text = dt.Rows[0].ItemArray[4].ToString();
                            txtCompletionRequestedAmount.Text = dt.Rows[0].ItemArray[5].ToString();
                            txtCompletionFundRaised.Text = dt.Rows[0].ItemArray[6].ToString();
                            txtCompletionApprovedAmount.Text = dt.Rows[0].ItemArray[6].ToString();

                            //  lblFinalRatingResult.Text = BLobj.GetRatingStarts(ds.Tables[0].Rows[0].ItemArray[13].ToString());
                            // txtRating.Text = ds.Tables[0].Rows[0].ItemArray[13].ToString();

                            // txtRating.Visible = true;

                            ddlCompletionTheme.SelectedIndex = ddlCompletionTheme.Items.IndexOf(ddlCompletionTheme.Items.FindByValue(dt.Rows[0].ItemArray[7].ToString()));
                            txtCompletionChanllengesFaced.Text = dt.Rows[0].ItemArray[8].ToString();
                            txtCompletionLearningFromProject.Text = dt.Rows[0].ItemArray[9].ToString();
                            txtCompletionProjectStory.Text = dt.Rows[0].ItemArray[10].ToString();
                            txtCompletionResourceUtilize.Text = dt.Rows[0].ItemArray[11].ToString();
                            txtCompletionManagerComments.Text = dt.Rows[0].ItemArray[14].ToString();
                            txtCompletionHoursSpend.Text = dt.Rows[0].ItemArray[22].ToString();
                            txtCompletionStudentName.Text = dt.Rows[0].ItemArray[24].ToString();
                            txtCompletionMobileNo.Text = dt.Rows[0].ItemArray[25].ToString();
                            txtSemName.Text = dt.Rows[0]["SemName"].ToString();
                            txtCompletionResourceAmount.Text = dt.Rows[0].ItemArray[37].ToString();



                            if (dt.Rows[0].ItemArray[27].ToString() != "")
                            {
                                //   ddlSDGGoals.SelectedIndex = ddlSDGGoals.Items.IndexOf(ddlSDGGoals.Items.FindByValue(dt.Rows[0].ItemArray[27].ToString()));
                            }
                            if (dt.Rows[0].ItemArray[26].ToString() == "1")
                            {
                                btnManagerImpactProject.Text = "<span class='fa fa-magic'></span> <br />No-Impact";
                                lblImpact.Text = "No-Impact";
                                if (dt.Rows[0].ItemArray[28].ToString() != "")
                                {
                                    ChkCollabration.Checked = true;
                                    txtCompletionCollabration.Text = dt.Rows[0].ItemArray[28].ToString();
                                }
                                if (dt.Rows[0].ItemArray[29].ToString() != "")
                                {
                                    txtCompletionProcedure.Text = dt.Rows[0].ItemArray[29].ToString();
                                }
                                if (dt.Rows[0].ItemArray[30].ToString() != "")
                                {
                                    txtCompletionInitiativeExperience.Text = dt.Rows[0].ItemArray[30].ToString();
                                }
                                if (dt.Rows[0].ItemArray[31].ToString() != "")
                                {
                                    txtCompletionOvercomeLacking.Text = dt.Rows[0].ItemArray[31].ToString();
                                }
                                if (dt.Rows[0].ItemArray[32].ToString() != "")
                                {
                                    ChkAgainst_Tide.Checked = true;
                                    txtCompletionAgainst_Tide.Text = dt.Rows[0].ItemArray[32].ToString();
                                }
                                if (dt.Rows[0].ItemArray[33].ToString() != "")
                                {
                                    ChkCross_Hurdles.Checked = true;
                                    txtCompletionCross_Hurdles.Text = dt.Rows[0].ItemArray[33].ToString();
                                }
                                if (dt.Rows[0].ItemArray[34].ToString() != "")
                                {
                                    ChkEntrepreneurial_Venture.Checked = true;
                                    txtCompletionEntrepreneurial_Venture.Text = dt.Rows[0].ItemArray[34].ToString();
                                }
                                if (dt.Rows[0].ItemArray[35].ToString() != "")
                                {
                                    ChkGovernment_Awarded.Checked = true;
                                    txtCompletionGovernment_Awarded.Text = dt.Rows[0].ItemArray[35].ToString();
                                }
                                if (dt.Rows[0].ItemArray[36].ToString() != "")
                                {
                                    ChkLeadership_Roles.Checked = true;
                                    txtCompletionLeadership_Roles.Text = dt.Rows[0].ItemArray[36].ToString();
                                }
                                pnl_ImpactQuestions.Visible = true;
                            }
                            else
                            {
                                btnManagerImpactProject.Text = "<span class='fa fa-magic'></span> <br />Impact";
                                lblImpact.Text = "Impact";
                                pnl_ImpactQuestions.Visible = false;
                            }
                            if ((dt.Rows[0].ItemArray[15].ToString() != "0") || (dt.Rows[0].ItemArray[16].ToString() != "0"))
                            {
                                StartDate = DateTime.Parse(dt.Rows[0].ItemArray[15].ToString());
                                EndDate = DateTime.Parse(dt.Rows[0].ItemArray[16].ToString());
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
                                txtRCStartDate.Text = dt.Rows[0].ItemArray[15].ToString();
                                txtRCEndDate.Text = dt.Rows[0].ItemArray[16].ToString();
                            }

                            else
                            {
                                txtRCStartDate.Text = "";
                                txtRCEndDate.Text = "";
                                lblRCTargetDays.Text = "0";
                            }
                            txtRCStartDate.Enabled = false;
                            txtRCEndDate.Enabled = false;

                            if (lblProjectStatus.Text.ToString() == "Completed")
                            {
                                if (dt.Rows[0].ItemArray[23].ToString() != "0")
                                {
                                    ddlCompletionStudentLevel.SelectedIndex = ddlCompletionStudentLevel.Items.IndexOf(ddlCompletionStudentLevel.Items.FindByValue(dt.Rows[0].ItemArray[23].ToString()));
                                }

                                lblFinalRatingStars.Text = BLobj.GetRatingStarts(dt.Rows[0].ItemArray[13].ToString());
                                lblInnovationStars.Text = BLobj.GetRatingStarts(dt.Rows[0].ItemArray[17].ToString());
                                lblLeadershipStarts.Text = BLobj.GetRatingStarts(dt.Rows[0].ItemArray[18].ToString());
                                lblRiskTakenStars.Text = BLobj.GetRatingStarts(dt.Rows[0].ItemArray[19].ToString());
                                lblImpactStars.Text = BLobj.GetRatingStarts(dt.Rows[0].ItemArray[20].ToString());
                                lblFundRaisedStars.Text = BLobj.GetRatingStarts(dt.Rows[0].ItemArray[21].ToString());
                                lblFinalRatingOverAllCount.Visible = false;
                                lblFinalRatingCountLable.Visible = false;
                            }

                        }
                        ddlCompletionStudentLevel.BackColor = System.Drawing.Color.DodgerBlue;
                        ddlCompletionStudentLevel.ForeColor = System.Drawing.Color.WhiteSmoke;
                        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", DstStudentImgDocumentList);
                        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", rptDocument2);
                        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "DOC", DstStudentDOCLst);

                        if (lblProjectStatus.Text.ToString() == "RequestForCompletion")
                        {
                            RatingInnovation.Visible = true;
                            lblInnovationresult.Text = "0";
                            lblLeadershipResult.Text = "0";
                            lblRiskTakenResult.Text = "0";
                            lblImpactResult.Text = "0";
                            lblFundraisedResult.Text = "0";
                            lblFinalRatingResult.Text = "0";
                            lblFinalRatingStars.Text = BLobj.GetRatingStarts("0");
                            RatingInnovation.CurrentRating = 0;
                            RatingLeadership.CurrentRating = 0;
                            RatingRiskTaken.CurrentRating = 0;
                            RatingImpact.CurrentRating = 0;
                            RatingFundraised.CurrentRating = 0;
                            btnDownloadStudentDocument.Visible = false;
                            pnlReject.Visible = true;
                            dt = BLobj.Common_GetProjectSDG_Goals(lblPDID.Text.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    LstSDG_Goals.Items.FindByValue(dr[0].ToString()).Selected = true;
                                }
                            }
                        }
                        else if (lblProjectStatus.Text.ToString() == "Completed")
                        {
                            txtCompletionProjectTitle.Enabled = false;
                            txtCompletionProjectObjective.Enabled = false;
                            txtCompletionBeneficiary.Enabled = false;
                            txtCompletionActualBeneficiaries.Enabled = false;
                            txtCompletionPlaceofImplement.Enabled = false;
                            txtCompletionRequestedAmount.Enabled = false;
                            txtCompletionApprovedAmount.Enabled = false;
                            ddlCompletionTheme.Enabled = false;
                            txtCompletionChanllengesFaced.Enabled = false;
                            txtCompletionLearningFromProject.Enabled = false;
                            txtCompletionProjectStory.Enabled = false;
                            txtCompletionResourceUtilize.Enabled = false;
                            txtRCStartDate.Enabled = false;
                            txtRCEndDate.Enabled = false;
                            txtCompletionHoursSpend.Enabled = false;

                            txtCompletionManagerComments.Enabled = false;
                            btnMangerCompletionComfirmation.Visible = false;

                            RatingInnovation.Visible = false;
                            RatingLeadership.Visible = false;
                            RatingRiskTaken.Visible = false;
                            RatingImpact.Visible = false;
                            RatingFundraised.Visible = false;
                            lblFinalRatingResult.Visible = false;
                            btnDownloadStudentDocument.Visible = true;

                            btnManagerImpactProject.Visible = false;
                            pnlReject.Visible = false;
                            UpdatePanel5.Visible = false;
                            dt = BLobj.Common_GetProjectSDG_Goals(lblPDID.Text.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    LstSDG_Goals.Items.FindByValue(dr[0].ToString()).Selected = true;
                                }
                            }
                            LstSDG_Goals.Enabled = true;
                        }

                        else if (lblProjectStatus.Text.ToString() == "Draft")
                        {
                            txtCompletionProjectTitle.Enabled = false;
                            txtCompletionProjectObjective.Enabled = false;
                            txtCompletionBeneficiary.Enabled = false;
                            txtCompletionActualBeneficiaries.Enabled = false;
                            txtCompletionPlaceofImplement.Enabled = false;
                            txtCompletionRequestedAmount.Enabled = false;
                            txtCompletionApprovedAmount.Enabled = false;
                            ddlCompletionTheme.Enabled = false;
                            txtCompletionChanllengesFaced.Enabled = false;
                            txtCompletionLearningFromProject.Enabled = false;
                            txtCompletionProjectStory.Enabled = false;
                            txtCompletionResourceUtilize.Enabled = false;
                            txtRCStartDate.Enabled = false;
                            txtRCEndDate.Enabled = false;
                            txtCompletionHoursSpend.Enabled = false;

                            txtCompletionManagerComments.Enabled = false;
                            btnMangerCompletionComfirmation.Visible = false;
                            ddlCompletionStudentLevel.Enabled = false;
                            RatingInnovation.Visible = false;
                            RatingLeadership.Visible = false;
                            RatingRiskTaken.Visible = false;
                            RatingImpact.Visible = false;
                            RatingFundraised.Visible = false;
                            lblFinalRatingResult.Visible = false;
                            btnDownloadStudentDocument.Visible = false;

                            btnManagerImpactProject.Visible = false;
                            pnlReject.Visible = false;
                            dt = BLobj.Common_GetProjectSDG_Goals(lblPDID.Text.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    LstSDG_Goals.Items.FindByValue(dr[0].ToString()).Selected = true;
                                }
                            }
                            LstSDG_Goals.Enabled = false;
                        }


                        //   Response.Redirect("DashBoard.aspx?vwType=" + ProjectStatus.ToString());
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx?SessionTimeOut=True");
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnMangerCompletionComfirmation_Click(object sender, EventArgs e)
    {
        try
        {
            if (Validate_Controls() == 1)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirmation();", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    public int Validate_Controls()
    {

        if (RatingInnovation.CurrentRating.ToString() == "0")
        {
            RatingInnovation.Focus();
            lblInnovationresult.Text = "Select Rating";
            lblInnovationresult.ForeColor = System.Drawing.Color.Red;
            return 1;

        }
        if (RatingLeadership.CurrentRating.ToString() == "0")
        {
            RatingLeadership.Focus();
            lblLeadershipResult.Text = "Select Rating";
            lblLeadershipResult.ForeColor = System.Drawing.Color.Red;
            return 1;
        }
        if (RatingRiskTaken.CurrentRating.ToString() == "0")
        {
            RatingRiskTaken.Focus();
            lblRiskTakenResult.Text = "Select Rating";
            lblRiskTakenResult.ForeColor = System.Drawing.Color.Red;
            return 1;
        }
        if (RatingImpact.CurrentRating.ToString() == "0")
        {
            RatingImpact.Focus();
            lblImpactResult.Text = "Select Rating";
            lblImpactResult.ForeColor = System.Drawing.Color.Red;
            return 1;
        }
        if (RatingFundraised.CurrentRating.ToString() == "0")
        {
            RatingFundraised.Focus();
            lblFundraisedResult.Text = "Select Rating";
            lblFundraisedResult.ForeColor = System.Drawing.Color.Red;
            return 1;
        }
        if (ddlCompletionStudentLevel.SelectedItem.Text == "----Project Level----")
        {
            ddlCompletionStudentLevel.Focus();
            ddlCompletionStudentLevel.BackColor = System.Drawing.Color.Crimson;
            ddlCompletionStudentLevel.ForeColor = System.Drawing.Color.WhiteSmoke;
            return 1;
        }
        if (LstSDG_Goals.SelectedIndex == -1)
        {
            LstSDG_Goals.Focus();
            string msg = "Select SDG Goals";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            return 1;
        }
        if (lblImpact.Text.ToString() != "Impact")
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
        }
        catch (Exception)
        {

        }
    }
    protected void btnDownloadStudentDocument_Click(object sender, EventArgs e)
    {
        try
        {
            Reports rpt = new Reports();
            string sourcePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();

            System.Data.DataTable dt = rpt.Student_IndivisualStudentFolderCreating(lblPDID.Text.ToString());

            bool IsManagerExists = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id()));
            if (!IsManagerExists)
            {
                DirectoryInfo directory = new DirectoryInfo(targetPath);
                //string CurrentUserName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                //var Info = new DirectoryInfo(CurrentUserName);

                //var security = Info.GetAccessControl();

                // directory.SetAccessControl(security);
                System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + cook.Manager_Id()));
            }


            bool IsLeadIdExists = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id() + "/LeadID" + lblLead_Id.Text.ToString()));
            if (!IsLeadIdExists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString()));
            }
            else
            {
                System.IO.DirectoryInfo myDirInfo = new DirectoryInfo(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString()));

                foreach (FileInfo file in myDirInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in myDirInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            bool isTitleExists = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()));

            if (!isTitleExists)
            {

                System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()));
                string dest = (Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()));
                foreach (DataRow dr in dt.Rows)
                {
                    string fileName = System.IO.Path.GetFileName(Server.MapPath(dr[0].ToString()));
                    bool isImgExists = File.Exists(Server.MapPath(sourcePath + fileName));
                    if (isImgExists == true)
                    {
                        string destFile = System.IO.Path.Combine(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString() + "/" + txtCompletionProjectTitle.Text.ToString()), fileName);
                        System.IO.File.Copy(Server.MapPath(dr[0].ToString()), destFile, true);
                    }
                }
                //CreatingIndivisualStudentDocument();
                CreateWordDocuments(lblPDID.Text.ToString());
                using (ZipFile zip = new ZipFile())
                {
                    //Response.Clear();
                    //Response.ContentType = "application/zip";
                    //Response.AddHeader("content-disposition", "filename=" + cook.Manager_Id() + "_" + lblProjectCompletionPOPLeadId.Text.ToString() + "_Lead.zip");
                    //// zip.AddDirectory(Server.MapPath("/FilesDownload/26"));
                    //zip.AddDirectory(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblProjectCompletionPOPLeadId.Text.ToString() + "/"));
                    //zip.Save(Response.OutputStream);
                    //Response.End();
                    //Response.End();

                    Response.Clear();
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "filename=" + lblLead_Id.Text.ToString() + "_" + txtCompletionProjectTitle.Text.ToString() + "_Lead.zip");

                    zip.AddDirectory(Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString()));
                    zip.Save(Response.OutputStream);
                    Response.End();
                    Response.End();


                }
            }
            else
            {
                string msg = "Some thing went wrong with directory";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

            //string msg = "Some thing went wrong with directory" + ex.Message.ToString() + " " + ex.InnerException.ToString();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            //lblErrorfield.Text = ex.Message.ToString();
            //lblErrorfield.Visible = true;
        }

    }
    public void CreateWordDocuments(string PDId)
    {
        Reports rpt = new Reports();
        string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
        string MainFolder = Server.MapPath(targetPath);
        string filename = Server.MapPath(targetPath + cook.Manager_Id() + "/" + "LeadID" + lblLead_Id.Text.ToString() + "/" + txtCompletionProjectTitle.Text.ToString() + "/" + txtCompletionProjectTitle.Text + ".docx");
        rpt.CreateWordDocuments(PDId, targetPath, MainFolder, filename);
    }
    protected void RatingInnovation_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        try
        {

            GetFinalRatingResult(RatingInnovation);
        }
        catch (Exception)
        {

        }
    }
    protected void RatingLeadership_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        try
        {

            GetFinalRatingResult(RatingLeadership);
        }
        catch (Exception)
        {

        }
    }

    protected void RatingRiskTaken_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        try
        {

            GetFinalRatingResult(RatingRiskTaken);
        }
        catch (Exception)
        {

        }
    }
    protected void RatingImpact_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        try
        {

            GetFinalRatingResult(RatingImpact);
        }
        catch (Exception)
        {

        }
    }
    protected void RatingFundraised_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        try
        {

            GetFinalRatingResult(RatingFundraised);
        }
        catch (Exception)
        {

        }
    }
    public void GetFinalRatingResult(AjaxControlToolkit.Rating Rating)
    {
        try
        {
            lblInnovationresult.Text = RatingInnovation.CurrentRating.ToString();
            lblLeadershipResult.Text = RatingLeadership.CurrentRating.ToString();
            lblRiskTakenResult.Text = RatingRiskTaken.CurrentRating.ToString();
            lblImpactResult.Text = RatingImpact.CurrentRating.ToString();
            lblFundraisedResult.Text = RatingFundraised.CurrentRating.ToString();
            double Total = Round((double.Parse(lblInnovationresult.Text) + double.Parse(lblLeadershipResult.Text) + double.Parse(lblRiskTakenResult.Text) + double.Parse(lblImpactResult.Text) + double.Parse(lblFundraisedResult.Text)) / 5);
            lblFinalRatingResult.Text = Total.ToString();
            lblFinalRatingStars.Text = BLobj.GetRatingStarts(Total.ToString());
            lblFinalRatingOverAllCount.Text = (double.Parse(lblInnovationresult.Text) + double.Parse(lblLeadershipResult.Text) + double.Parse(lblRiskTakenResult.Text) + double.Parse(lblImpactResult.Text) + double.Parse(lblFundraisedResult.Text)).ToString();
        }
        catch (Exception)
        {
        }
    }
    public double Round(double value)
    {
        return (double)Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;

        //double decimalpoints = Math.Abs(value - Math.Floor(value));
        //if ((decimalpoints == 0) || (decimalpoints > 0.5))
        //    return (double)Math.Round(value);
        //else
        //    return (double)Math.Floor(value) + 0.5;


    }

    protected void btnManagerImpactProject_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblImpact.Text == "Impact")
            {
                BLobj.Manager_UpdateIsImapactProject(lblPDID.Text.ToString(), 1, lblLead_Id.Text.ToString(), txtCompletionProjectTitle.Text.ToString(), cook.Manager_Id());
                btnManagerImpactProject.Text = "<span class='fa fa-magic'></span> <br />No-Impact";
                lblImpact.Text = "No-Impact";
                pnl_ImpactQuestions.Visible = true;

                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), lblLead_Id.Text.ToString(), txtCompletionProjectTitle.Text.ToString() + ",Reason:" + txtCompletionManagerComments.Text.ToString(), "completion(Manager)", "");
                string DeviceID = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                if (DeviceID != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Congratulations, Your project" + " " + txtCompletionProjectTitle.Text.ToString() + " is Selected as Impact Project", "Student", "Empty");
                }

                string msg = "Selected as Impact Project";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            }
            else if (lblImpact.Text == "No-Impact")
            {
                BLobj.Manager_UpdateIsImapactProject(lblPDID.Text.ToString(), 0, lblLead_Id.Text.ToString(), txtCompletionProjectTitle.Text.ToString(), cook.Manager_Id());
                btnManagerImpactProject.Text = "<span class='fa fa-magic'></span> <br />Impact";
                lblImpact.Text = "Impact";
                pnl_ImpactQuestions.Visible = false;
                string msg = "Selected as Non Impact";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    //protected void DstStudentImgDocumentList_ItemCommand(object source, DataListCommandEventArgs e)
    //{

    //    int index = Convert.ToInt32(e.Item.ItemIndex);
    //    Label lbl = (Label)e.Item.FindControl("lblImageSlno");
    //    Image imgPath = (Image)e.Item.FindControl("Image1");
    //    string img = imgPath.ImageUrl.ToString();
    //    int Result = BLobj.Common_Delete_ProjectDocument(lbl.Text.ToString());
    //    if (Result > 0)
    //    {
    //        if ((System.IO.File.Exists(Server.MapPath(img.ToString()))))
    //        {
    //            System.IO.File.Delete(Server.MapPath(img.ToString()));
    //        }
    //        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", DstStudentImgDocumentList);
    //        BLobj.Student_GetDocuments_NEW(lblPDID.Text.ToString(), "IMG", rptDocument2);
    //    }
    //}
    //protected void DstStudentImgDocumentList_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDeleteImage");
    //        if (lblProjectStatus.Text == "Completed")
    //        {
    //            btnDelete.Visible = false;
    //        }
    //    }
    //}

    protected void btnYesCheck_Click(object sender, EventArgs e)
    {
        try
        {
            DataLL DL = new DataLL();
            vmResponseCompletionSave Obj = new vmResponseCompletionSave();
            if (txtCompletionActualBeneficiaries.Text == "")
            {
                Obj.ActualBeneficier = "0";
            }
            else
            {
                Obj.ActualBeneficier = txtCompletionActualBeneficiaries.Text.ToString();
            }
            Obj.LeadId = lblLead_Id.Text.ToString();
            Obj.PDId = lblPDID.Text.ToString();
            Obj.placeofimplement = txtCompletionPlaceofImplement.Text.ToString();
            Obj.Theme = ddlCompletionTheme.SelectedValue.ToString();
            Obj.challenge = txtCompletionChanllengesFaced.Text.ToString();
            Obj.Learning = txtCompletionLearningFromProject.Text.ToString();
            Obj.AsAStory = txtCompletionProjectStory.Text.ToString();
            Obj.Resource = txtCompletionResourceUtilize.Text.ToString();
            Obj.ManagerId = cook.Manager_Id();
            Obj.Title = txtCompletionProjectTitle.Text.ToString();
            Obj.rating = lblFinalRatingResult.Text.ToString(); //ddlCompletionRating.SelectedValue.ToString();
            Obj.ManagerComments = txtCompletionManagerComments.Text.ToString();

            Obj.InnovativeRating = RatingInnovation.CurrentRating.ToString();
            Obj.LeadershipRating = RatingLeadership.CurrentRating.ToString();
            Obj.RiskTaenRating = RatingRiskTaken.CurrentRating.ToString();
            Obj.ImpactRating = RatingImpact.CurrentRating.ToString();
            Obj.FundRaisedRating = RatingFundraised.CurrentRating.ToString();
            if (txtCompletionResourceAmount.Text == "")
            {
                Obj.ResourceUtilizedAmount = "0";
            }
            else
            {
                Obj.ResourceUtilizedAmount = txtCompletionResourceAmount.Text;
            }
            Obj.StudentLevel = ddlCompletionStudentLevel.SelectedItem.Text.ToString();
            if (ChkCollabration.Checked == true)
            {
                Obj.Collaboration_Supported = txtCompletionCollabration.Text.ToString();
            }
            else
            {
                Obj.Collaboration_Supported = "";
            }
            Obj.Permission_And_Activities = txtCompletionProcedure.Text.ToString();
            Obj.Experience_Of_Initiative = txtCompletionInitiativeExperience.Text.ToString();
            Obj.Lacking_initiative = txtCompletionOvercomeLacking.Text.ToString();
            if (ChkAgainst_Tide.Checked == true)
            {
                Obj.Against_Tide = txtCompletionAgainst_Tide.Text.ToString();
            }
            else
            {
                Obj.Against_Tide = "";
            }
            if (ChkCross_Hurdles.Checked == true)
            {
                Obj.Cross_Hurdles = txtCompletionCross_Hurdles.Text.ToString();
            }
            else
            {
                Obj.Cross_Hurdles = "";
            }
            if (ChkEntrepreneurial_Venture.Checked == true)
            {
                Obj.Entrepreneurial_Venture = txtCompletionEntrepreneurial_Venture.Text.ToString();
            }
            else
            {
                Obj.Entrepreneurial_Venture = "";
            }
            if (ChkGovernment_Awarded.Checked == true)
            {
                Obj.Government_Awarded = txtCompletionGovernment_Awarded.Text.ToString();
            }
            else
            {
                Obj.Government_Awarded = "";
            }
            if (ChkLeadership_Roles.Checked == true)
            {
                Obj.Leadership_Roles = txtCompletionLeadership_Roles.Text.ToString();
            }
            else
            {
                Obj.Leadership_Roles = "";
            }
            BLobj.Manager_SaveCompletionWithRating(Obj, LstSDG_Goals);

            string status = DL.UpdateStudentLevelAfterCompletion(Obj.LeadId.ToString(), Obj.StudentLevel.ToString());
            BLobj.Manager_SaveProjectDiscussionForum(lblPDID.Text.ToString(), Obj.ManagerComments, cook.Manager_Id(), "Comments", "Completed");
            BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), Obj.LeadId.ToString(), txtCompletionProjectTitle.Text.ToString() + ",Reason:" + txtCompletionManagerComments.Text.ToString(), "completion(Manager)", "");
            string DeviceID = BLobj.Common_GetDeviceID(Obj.LeadId.ToString());
            if (DeviceID != "")
            {
                FCMPushNotification.AndroidPush(DeviceID.ToString(), "Congratulations, Your project" + " " + txtCompletionProjectTitle.Text.ToString() + " has been requested for completion", "Student", "Empty");
            }
            Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {

            if (cook.Manager_Id() != "")
            {
                string AcademicCode = cook.Manager_AcademicYear();
                string ManagerID = cook.Manager_Id();
                BLobj.Manager_RejectProject(lblLead_Id.Text.ToString(), ManagerID.ToString(), lblPDID.Text.ToString(), "Rejected", txtCompletionStudentName.Text.ToString(), txtCompletionProjectTitle.Text.ToString(), txtCompletionPlaceofImplement.Text.ToString(), txtCompletionBeneficiary.Text.ToString(), txtCompletionActualBeneficiaries.Text.ToString(), txtCompletionRequestedAmount.Text.ToString(), txtCompletionApprovedAmount.Text.ToString(), txtCompletionProjectObjective.Text.ToString(), ddlCompletionTheme.SelectedItem.Text.ToString(), txtCompletionRejectComments.Text.ToString(), AcademicCode.ToString(), txtRCStartDate, txtRCEndDate);
                BLobj.Manager_SaveProjectDiscussionForum(lblPDID.Text.ToString(), txtCompletionRejectComments.Text.ToString(), ManagerID.ToString(), "Comments", "Rejected");
                BLobj.Manager_SaveNotificationLog(cook.Manager_Id(), lblLead_Id.Text.ToString(), txtCompletionProjectTitle.Text.ToString() + ",Reason:" + txtCompletionRejectComments.Text.ToString(), "Rejected(Manager)", "");
                string DeviceID = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                if (DeviceID.ToString() != "")
                {
                    FCMPushNotification.AndroidPush(DeviceID.ToString(), "Your project" + " " + txtCompletionProjectTitle.Text.ToString() + " is Rejected", "Student", "Empty");
                }

                Response.Redirect("DashBoard.aspx?vwType=" + lblProjectStatus.Text.ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=True");
            }

        }
        catch (Exception)
        {

        }
    }
}