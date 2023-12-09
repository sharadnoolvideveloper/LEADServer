using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using System.Globalization;

public partial class Pages_Admin_Admin_Appreciate : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    DataLL DLobj = new DataLL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnTabSelectAward.Attributes.Add("class", "nav-link active");
            Selection.Attributes.Add("class", "tab-pane fade in active");
            btnTabSelectAward.BackColor = System.Drawing.Color.CornflowerBlue;
            btnTabSelectAward.ForeColor = System.Drawing.Color.White;

            btnTabSendCertificate.Attributes.Add("class", "nav-link");
            Send.Attributes.Add("class", "tab-pane fade");
            btnTabSendCertificate.BackColor = System.Drawing.Color.White;
            btnTabSendCertificate.ForeColor = System.Drawing.Color.CornflowerBlue;

            /* BLobj.Admin_FillManagerddl(ddlManager,cook.Admin_Id());
             BLobj.Manager_FillCollegeByManagerCode(ddlManager.SelectedValue.ToString(),ddlCollegeName);*/
            BLobj.Admin_Fillprogramddl(ddlprogramId, cook.Admin_Id());
            BLobj.Admin_Fillprogramddl(ddlprogram, cook.Admin_Id());
            BLobj.Admin_FillManagerByprogram(ddlprogramId.SelectedValue.ToString(), ddlManager);
            BLobj.FillCollegeByManagerCode(ddlManager.SelectedValue.ToString(), ddlprogramId.SelectedValue.ToString(), ddlCollegeName);

        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            BLobj.Admin_FillManagerByprogram(ddlprogramId.SelectedValue.ToString(), ddlManager);
            BLobj.FillCollegeByManagerCode(ddlManager.SelectedValue.ToString(), ddlprogramId.SelectedValue.ToString(), ddlCollegeName);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnTabSelectAward_Click(object sender, EventArgs e)
    {
        try
        {
            btnTabSelectAward.Attributes.Add("class", "nav-link active");
            Selection.Attributes.Add("class", "tab-pane fade in active");
            btnTabSelectAward.BackColor = System.Drawing.Color.CornflowerBlue;
            btnTabSelectAward.ForeColor = System.Drawing.Color.White;

            btnTabSendCertificate.Attributes.Add("class", "nav-link");
            Send.Attributes.Add("class", "tab-pane fade");
            btnTabSendCertificate.BackColor = System.Drawing.Color.White;
            btnTabSendCertificate.ForeColor = System.Drawing.Color.CornflowerBlue;

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnTabSendCertificate_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            btnTabSendCertificate.Attributes.Add("class", "nav-link active");
            Send.Attributes.Add("class", "tab-pane fade in active");
            btnTabSendCertificate.BackColor = System.Drawing.Color.CornflowerBlue;
            btnTabSendCertificate.ForeColor = System.Drawing.Color.White;

            btnTabSelectAward.Attributes.Add("class", "nav-link");
            Selection.Attributes.Add("class", "tab-pane fade");
            btnTabSelectAward.BackColor = System.Drawing.Color.White;
            btnTabSelectAward.ForeColor = System.Drawing.Color.CornflowerBlue;
            lblTotal_Sending_Summary.Text = "0";
            lblSent.Text = "0";
            lblPending.Text = "0";
            lblError.Text = "0";

            BLobj.FillAademicYearWithTop(ddlAcademicYear);
            dt = DLobj.Get_Appreciation_Send_Certificate(ddlAcademicYear.SelectedValue.ToString(), ddlAppreciate_Type.SelectedItem.Text.ToString(), ddlStatus.SelectedValue.ToString());
            rptSendCertificate.DataSource = dt;
            rptSendCertificate.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblTotal_Students.Text = "0";
            rptStudents.DataSource = null;
            rptStudents.DataBind();
            BLobj.FillCollegeByManagerCode(ddlManager.SelectedValue.ToString(), ddlprogramId.SelectedValue.ToString(), ddlCollegeName);

        }
        catch (Exception ex)
        {
        }
    }
    protected void bntGenerateCertificate_Click(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            string SlapDest = "";
            string Source = "";

            if (ddlprogramId.SelectedValue.ToString() == "")
            {
                string msg = "Select Student generation Type";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                if (ddlprogramId.SelectedValue.ToString() == "2")
                {
                    Source = "~/Reports/" + "Star" + ddlType.SelectedValue.ToString() + ".pdf";
                    if (ddlType.SelectedValue.ToString() == "")
                    {
                        string msg = "Select Appreciation Type";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    }
                    else
                    {
                        foreach (RepeaterItem ri in rptStudents.Items)
                        {

                            CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                            if (ChkCertificate.Checked == true)
                            {
                                count = 1;

                                Label lblLead_Id = (Label)ri.FindControl("lblLead_Id");
                                Label lblRegistration_Id = (Label)ri.FindControl("lblRegistrationId");
                                Label lblMailId = (Label)ri.FindControl("lblEmailId");
                                //   string StudentName = "Leader " + ChkCertificate.Text;
                                string StudentName = "LEAD"+"er " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ChkCertificate.Text.ToLower());
                                Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");
                                Label lblInstituteName = (Label)ri.FindControl("lblInstituteName");
                                Label lblProject_Count = (Label)ri.FindControl("lblProjectCount");
                                //  StudentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(StudentName.ToLower());
                                SlapDest = ddlType.SelectedItem.Text.ToString() + "_" + lblLead_Id.Text.ToString() + "_" + lblMailId.Text.ToString() + ".pdf";
                                var reader = new PdfReader(Server.MapPath(Source));

                                FileStream fs = null;
                                fs = new System.IO.FileStream(Server.MapPath("~/Certificate/Appreciate/" + SlapDest), FileMode.Create, FileAccess.Write);
                                var document = new Document(reader.GetPageSizeWithRotation(1));
                                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                                document.Open();
                                document.NewPage();
                                var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                                //   var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);
                                // var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRADHITC.TTF"), BaseFont.IDENTITY_H, true);
                                var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/fonts/Lora-Italic.ttf"), BaseFont.IDENTITY_H, true);


                                var importedPage = writer.GetImportedPage(reader, 1);
                                var contentByte = writer.DirectContent;
                                contentByte.BeginText();
                                contentByte.SetFontAndSize(baseFont1, 26);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 420, 200, 0);

                                int length = lblInstituteName.Text.ToString().ToString().Length;
                                contentByte.SetFontAndSize(baseFont1, 18);
                                if (length >= 50)
                                {
                                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblInstituteName.Text.ToString(), 465, 165, 0);
                                }
                                else
                                {
                                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblInstituteName.Text.ToString(), 420, 165, 0);
                                }
                                contentByte.SetFontAndSize(baseFont1, 26);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblProject_Count.Text + " " + " leadership initiatives", 420, 90, 0);


                                contentByte.SetFontAndSize(baseFont, 10);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, System.DateTime.Now.ToString("dd-MM-yyyy"), 85, 62, 0);

                                contentByte.SetFontAndSize(baseFont, 10);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblLead_Id.Text.ToString(), 85, 37, 0);


                                PdfGState graphicsState = new PdfGState();
                                graphicsState.BlendMode = PdfGState.BM_DARKEN;
                                contentByte.SetGState(graphicsState);
                                contentByte.EndText();
                                contentByte.AddTemplate(importedPage, 0, 0);
                                document.Close();
                                writer.Close();
                                DLobj.Insert_Appreciation_Certificate(lblRegistration_Id.Text.ToString(), ddlType.SelectedItem.Text.ToString(), lblProject_Count.Text.ToString(), cook.Admin_Id());

                            }
                        }

                        if (count > 0)
                        {
                            DataTable dt;
                            lblTotal_Students.Text = "0";
                            dt = DLobj.Get_Appreciation_StudentList(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollegeName.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), ddlType.SelectedValue.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                rptStudents.DataSource = dt;
                                rptStudents.DataBind();
                                lblTotal_Students.Text = dt.Rows.Count.ToString();
                            }
                            else
                            {
                                rptStudents.DataSource = null;
                                rptStudents.DataBind();
                            }
                            string msg = "Certificates Generated Successfully!!!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                        }
                        else
                        {
                            string msg = "Select Student To generate Certificate!!!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                        }
                    }
                }
                else
                {
                    Source = "~/Reports/" + "Star" + ddlType.SelectedValue.ToString() + "_Skillplus" + ".pdf";
                    if (ddlType.SelectedValue.ToString() == "")
                    {
                        string msg = "Select Appreciation Type";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    }
                    else
                    {
                        foreach (RepeaterItem ri in rptStudents.Items)
                        {

                            CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                            if (ChkCertificate.Checked == true)
                            {
                                count = 1;

                                Label lblLead_Id = (Label)ri.FindControl("lblLead_Id");
                                Label lblRegistration_Id = (Label)ri.FindControl("lblRegistrationId");
                                Label lblMailId = (Label)ri.FindControl("lblEmailId");
                                string StudentName = "LEADER " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ChkCertificate.Text.ToLower());
                                Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");
                                Label lblInstituteName = (Label)ri.FindControl("lblInstituteName");
                                Label lblProject_Count = (Label)ri.FindControl("lblProjectCount");
                                //StudentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(StudentName.ToLower());
                                SlapDest = ddlType.SelectedItem.Text.ToString() + "_" + lblLead_Id.Text.ToString() + "_" + lblMailId.Text.ToString() + ".pdf";
                                var reader = new PdfReader(Server.MapPath(Source));

                                FileStream fs = null;
                                fs = new System.IO.FileStream(Server.MapPath("~/Certificate/Appreciate/" + SlapDest), FileMode.Create, FileAccess.Write);
                                var document = new Document(reader.GetPageSizeWithRotation(1));
                                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                                document.Open();
                                document.NewPage();
                                var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                                //   var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);
                                // var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRADHITC.TTF"), BaseFont.IDENTITY_H, true);
                                //var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/calibri.ttf"), BaseFont.IDENTITY_H, true);
                                var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/fonts/Lora-Italic.ttf"), BaseFont.IDENTITY_H, true);


                                var importedPage = writer.GetImportedPage(reader, 1);
                                var contentByte = writer.DirectContent;
                                contentByte.BeginText();
                                contentByte.SetFontAndSize(baseFont1, 26);
                                contentByte.SetRGBColorFill(0, 0, 0); // Dark color (black)
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 420, 200, 0);

                                int length = lblInstituteName.Text.ToString().ToString().Length;
                                contentByte.SetFontAndSize(baseFont1, 18);
                                if (length >= 50)
                                {
                                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblInstituteName.Text.ToString(), 465, 165, 0);
                                }
                                else
                                {
                                    contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblInstituteName.Text.ToString(), 420, 165, 0);
                                }
                                contentByte.SetFontAndSize(baseFont1, 26);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblProject_Count.Text + " " + " leadership initiatives", 420, 90, 0);


                                contentByte.SetFontAndSize(baseFont, 10);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, System.DateTime.Now.ToString("dd-MM-yyyy"), 85, 62, 0);

                                contentByte.SetFontAndSize(baseFont, 10);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblLead_Id.Text.ToString(), 85, 37, 0);


                                PdfGState graphicsState = new PdfGState();
                                graphicsState.BlendMode = PdfGState.BM_DARKEN;
                                contentByte.SetGState(graphicsState);
                                contentByte.EndText();
                                contentByte.AddTemplate(importedPage, 0, 0);
                                document.Close();
                                writer.Close();
                                DLobj.Insert_Appreciation_Certificate(lblRegistration_Id.Text.ToString(), ddlType.SelectedItem.Text.ToString(), lblProject_Count.Text.ToString(), cook.Admin_Id());

                            }
                        }

                        if (count > 0)
                        {
                            DataTable dt;
                            lblTotal_Students.Text = "0";
                            dt = DLobj.Get_Appreciation_StudentList(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollegeName.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), ddlType.SelectedValue.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                rptStudents.DataSource = dt;
                                rptStudents.DataBind();
                                lblTotal_Students.Text = dt.Rows.Count.ToString();
                            }
                            else
                            {
                                rptStudents.DataSource = null;
                                rptStudents.DataBind();
                            }
                            string msg = "Certificates Generated Successfully!!!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                        }
                        else
                        {
                            string msg = "Select Student To generate Certificate!!!";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                        }
                    }

                }

            }

            /*  Source = "~/Reports/" + "Star" + ddlType.SelectedValue.ToString() + ".pdf";
              if (ddlType.SelectedValue.ToString() == "")
              {
                  string msg = "Select Appreciation Type";
                  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
              }
              else
              {
                  foreach (RepeaterItem ri in rptStudents.Items)
                  {

                      CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                      if (ChkCertificate.Checked == true)
                      {
                          count = 1;

                          Label lblLead_Id = (Label)ri.FindControl("lblLead_Id");
                          Label lblRegistration_Id = (Label)ri.FindControl("lblRegistrationId");
                          Label lblMailId = (Label)ri.FindControl("lblEmailId");
                          string StudentName = "Leader " + ChkCertificate.Text;
                          Label lblMobileNo = (Label)ri.FindControl("lblMobileNo");
                          Label lblInstituteName = (Label)ri.FindControl("lblInstituteName");
                          Label lblProject_Count = (Label)ri.FindControl("lblProjectCount");
                          StudentName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(StudentName.ToLower());
                          SlapDest = ddlType.SelectedItem.Text.ToString() + "_" + lblLead_Id.Text.ToString() + "_" + lblMailId.Text.ToString() + ".pdf";
                          var reader = new PdfReader(Server.MapPath(Source));

                          FileStream fs = null;
                          fs = new System.IO.FileStream(Server.MapPath("~/Certificate/Appreciate/" + SlapDest), FileMode.Create, FileAccess.Write);
                          var document = new Document(reader.GetPageSizeWithRotation(1));
                          var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
                          document.Open();
                          document.NewPage();
                          var baseFont = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/Montserrat.ttf"), BaseFont.IDENTITY_H, true);
                          var baseFont1 = BaseFont.CreateFont(Server.MapPath("~/CSS/fonts/BRUSHSCI.ttf"), BaseFont.IDENTITY_H, true);


                          var importedPage = writer.GetImportedPage(reader, 1);
                          var contentByte = writer.DirectContent;
                          contentByte.BeginText();
                          contentByte.SetFontAndSize(baseFont1, 26);
                          contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, StudentName.ToString(), 420, 200, 0);

                          int length = lblInstituteName.Text.ToString().ToString().Length;
                          contentByte.SetFontAndSize(baseFont1, 18);
                          if (length >= 50)
                          {
                              contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblInstituteName.Text.ToString(), 465, 165, 0);
                          }
                          else
                          {
                              contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblInstituteName.Text.ToString(), 420, 165, 0);
                          }
                          contentByte.SetFontAndSize(baseFont1, 26);
                          contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, lblProject_Count.Text + " " + " leadership initiatives", 420, 90, 0);


                          contentByte.SetFontAndSize(baseFont, 10);
                          contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, System.DateTime.Now.ToString("dd-MM-yyyy"), 85, 62, 0);

                          contentByte.SetFontAndSize(baseFont, 10);
                          contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblLead_Id.Text.ToString(), 85, 37, 0);


                          PdfGState graphicsState = new PdfGState();
                          graphicsState.BlendMode = PdfGState.BM_DARKEN;
                          contentByte.SetGState(graphicsState);
                          contentByte.EndText();
                          contentByte.AddTemplate(importedPage, 0, 0);
                          document.Close();
                          writer.Close();
                          DLobj.Insert_Appreciation_Certificate(lblRegistration_Id.Text.ToString(), ddlType.SelectedItem.Text.ToString(), lblProject_Count.Text.ToString(), cook.Admin_Id());

                      }
                  }

                  if (count > 0)
                  {
                      DataTable dt;
                      lblTotal_Students.Text = "0";
                      dt = DLobj.Get_Appreciation_StudentList(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollegeName.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), ddlType.SelectedValue.ToString());
                      if (dt.Rows.Count > 0)
                      {
                          rptStudents.DataSource = dt;
                          rptStudents.DataBind();
                          lblTotal_Students.Text = dt.Rows.Count.ToString();
                      }
                      else
                      {
                          rptStudents.DataSource = null;
                          rptStudents.DataBind();
                      }
                      string msg = "Certificates Generated Successfully!!!";
                      Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                  }
                  else
                  {
                      string msg = "Select Student To generate Certificate!!!";
                      Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                  }
              }
            */

        }
        catch (Exception ex)
        {

        }
    }
    protected void SendEmail(object sender, EventArgs e)
    {
        int count1 = 0;
        try
        {
            foreach (RepeaterItem ri in rptSendCertificate.Items)
            {
                CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                if (ChkCertificate.Checked == true)
                {
                    count1 = 1;
                }
            }
            if (count1 > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirm();", true);
            }
            else
            {
                string msg = "Select Students!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            if (ddlType.SelectedValue.ToString() == "")
            {
                string msg = "Select Appreciation Type";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                lblTotal_Students.Text = "0";
                dt = DLobj.Get_Appreciation_StudentList(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollegeName.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), ddlType.SelectedValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    rptStudents.DataSource = dt;
                    rptStudents.DataBind();
                    lblTotal_Students.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    rptStudents.DataSource = null;
                    rptStudents.DataBind();
                    string msg = "No Data Found!!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }



        }
        catch (Exception)
        {

        }
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = DLobj.Get_Appreciation_Send_Certificate(ddlAcademicYear.SelectedValue.ToString(), ddlAppreciate_Type.SelectedItem.Text.ToString(), ddlStatus.SelectedValue.ToString());

            if (dt.Rows.Count > 0)
            {
                rptSendCertificate.DataSource = dt;
                rptSendCertificate.DataBind();
            }
            else
            {
                rptSendCertificate.DataSource = dt;
                rptSendCertificate.DataBind();
                string msg = "No Data Found!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlAppreciate_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblTotal_Sending_Summary.Text = "0";
            lblSent.Text = "0";
            lblPending.Text = "0";
            lblError.Text = "0";

            DataTable dt = new DataTable();
            dt = DLobj.Get_Appreciation_Send_Certificate(ddlAcademicYear.SelectedValue.ToString(), ddlAppreciate_Type.SelectedItem.Text.ToString(), ddlStatus.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                rptSendCertificate.DataSource = dt;
                rptSendCertificate.DataBind();
            }
            else
            {
                rptSendCertificate.DataSource = dt;
                rptSendCertificate.DataBind();
                string msg = "No Data Found!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rptSendCertificate_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int Pending = 0;
        int Sent = 0;
        int Error = 0;
        int Total = 0;
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //Label ProjectStatus = (e.Item.FindControl("lblProjectStatus") as Label);
                // LinkButton btnPayee = (e.Item.FindControl("btnView") as LinkButton);
                Label lblStatus = (e.Item.FindControl("lblStatus") as Label);
                Label lblNewStatus = (e.Item.FindControl("lblNewStatus") as Label);
                if (lblStatus.Text.ToString() == "0")
                {
                    lblNewStatus.Text = "No Certificate";
                    lblNewStatus.Attributes.Add("class", "label label-warning");
                }
                else if (lblStatus.Text.ToString() == "1")
                {
                    Pending = int.Parse(lblPending.Text.ToString()) + 1;
                    lblPending.Text = Pending.ToString();

                    lblNewStatus.Text = "Generated";
                    lblNewStatus.Attributes.Add("class", "label label-primary");
                }
                else if (lblStatus.Text.ToString() == "2")
                {
                    Sent = int.Parse(lblSent.Text.ToString()) + 1;
                    lblSent.Text = Sent.ToString();
                    lblNewStatus.Text = "Mail Sent";
                    lblNewStatus.Attributes.Add("class", "label label-success");
                }
                else if (lblStatus.Text.ToString() == "3")
                {
                    Error = int.Parse(lblError.Text.ToString()) + 1;
                    lblError.Text = Error.ToString();
                    lblNewStatus.Text = "Error on Mailsent";
                    lblNewStatus.Attributes.Add("class", "label label-danger");
                }
                Total = int.Parse(lblTotal_Sending_Summary.Text.ToString()) + 1;
                lblTotal_Sending_Summary.Text = Total.ToString();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void rptSendCertificate_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            string Appreciation_Type = "";
            string lead_id = "";
            string mailid = "";
            string slno = "";
            string StudentName = "";
            string Certificate_Status = "";
            string code = e.CommandArgument.ToString();
            string[] itemlist = code.Split('_');
            int i;
            for (i = 0; i <= 5; i++)
            {
                if (i == 0)
                {
                    Appreciation_Type = itemlist[0].ToString();
                }
                else if (i == 1)
                {
                    lead_id = itemlist[1].ToString();
                }
                else if (i == 2)
                {
                    mailid = itemlist[2].ToString();
                }
                else if (i == 3)
                {
                    slno = itemlist[3].ToString();
                }
                else if (i == 4)
                {
                    StudentName = itemlist[4].ToString();
                }
                else if (i == 5)
                {
                    Certificate_Status = itemlist[5].ToString();
                }
            }
            if ((Certificate_Status == "1") || (Certificate_Status == "2"))
            {
                string FilePath = "~/Certificate/Appreciate/" + Appreciation_Type.ToString() + "_" + lead_id + "_" + mailid.ToString() + ".pdf";
                lblCertificateUserName.Text = StudentName.ToString();
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"600px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl(FilePath));
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_PDFView();", true);
            }
            else
            {
                string msg = "Certificate Not Generated!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {
            foreach (RepeaterItem ri in rptSendCertificate.Items)
            {
                CheckBox ChkCertificate = (CheckBox)ri.FindControl("ChkCertificate");
                if (ChkCertificate.Checked == true)
                {

                    string StudentName = ChkCertificate.Text;
                    Label lblEmailId = (Label)ri.FindControl("lblEmailId");
                    Label lblSlno = (Label)ri.FindControl("lblSlno");
                    Label lblRegistration_Id = (Label)ri.FindControl("lblRegistrationId");
                    Label lblAppreciation_Type = (Label)ri.FindControl("lblAppreciation_Type");
                    Label lblStatus = (Label)ri.FindControl("lblStatus");
                    Label lblLead_Id = (Label)ri.FindControl("lblLead_Id");
                    if ((lblStatus.Text == "1") || (lblStatus.Text == "2"))
                    {
                        string FilePath = "~/Certificate/Appreciate/" + lblAppreciation_Type.Text.ToString() + "_" + lblLead_Id.Text.ToString() + "_" + lblEmailId.Text.ToString() + ".pdf";

                        using (MailMessage mm = new MailMessage("leadmis@dfmail.org", lblEmailId.Text.ToString()))
                        {
                            string body = PopulateBody(StudentName.ToString(), "", "", "");
                            mm.Subject = "Appreciation Certificate";
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
                                
                                if (lblStatus.Text != "2")
                                {
                                    DLobj.Update_Appreciation_Certificate(lblSlno.Text.ToString(), cook.Admin_Id());
                                    //string Device_Id = "";
                                    //Device_Id = BLobj.Common_GetDeviceID(lblLead_Id.Text.ToString());
                                    //if (Device_Id != "")
                                    //{
                                    //    string ServerResponse = FCMPushNotification.AndroidPush(Device_Id.ToString(), txtNotificationText.Text, "Notification", "Empty");
                                    //    BLobj.Manager_SaveNotificationLog(cook.Admin_Id(), lblLead_Id.Text.ToString(), txtNotificationText.Text.ToString(), "Notification", ServerResponse.ToString());
                                    //}

                                   
                                }
                            }
                        }
                    }
                }
            }
            lblTotal_Sending_Summary.Text = "0";
            lblSent.Text = "0";
            lblPending.Text = "0";
            lblError.Text = "0";
            Response.Redirect("Admin_Appreciate.aspx");

            //DataTable dt = new DataTable();
            //dt = DLobj.Get_Appreciation_Send_Certificate(ddlAcademicYear.SelectedValue.ToString(), ddlAppreciate_Type.SelectedItem.Text.ToString(), ddlStatus.SelectedValue.ToString());
            //rptSendCertificate.DataSource = dt;
            //rptSendCertificate.DataBind();
            //string msg = "Certificate Sent SuccessFully!!!";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);

        }
        catch (Exception ex)
        {
            Response.Redirect("Admin_Appreciate.aspx");
        }
    }
    private string PopulateBody(string userName, string title, string url, string description)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("/Pages/Appreciation_EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);
        body = body.Replace("{Title}", title);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Description}", description);
        return body;
    }

    protected void btnListingReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedValue.ToString() == "")
            {
                string msg = "Select Appreciation Type";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                GridView gd = new GridView();
                DataTable dt = new DataTable();
                dt = DLobj.Get_Appreciation_StudentList(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlCollegeName.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), ddlType.SelectedValue.ToString());

                gd.DataSource = dt;
                gd.DataBind();

                string name = "";
                if (gd.Rows.Count > 0)
                {
                    if (ddlManager.SelectedValue == "[All]")
                    {
                        name = "All Managers Details" + "_ From" + txtFromDate.Text.ToString() + "To" + txtToDate.Text.ToString();
                    }
                    else
                    {
                        name = ddlManager.SelectedItem.Text.ToString() + "_ From" + txtFromDate.Text.ToString() + "To" + txtToDate.Text.ToString();
                    }

                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                    Response.ContentType = "application/excel";
                    System.IO.StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    gd.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();

                }
                else
                {
                    string msg = "No Data Found Check Between Dates";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }

        }
        catch (Exception)
        {

        }
    }

    protected void btnSendCertificateReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gd = new GridView();
            gd.DataSource = DLobj.Get_Appreciation_Send_Certificate(ddlAcademicYear.SelectedValue.ToString(), ddlAppreciate_Type.SelectedItem.Text.ToString(), ddlStatus.SelectedValue.ToString());
            gd.DataBind();
            string name = "";
            if (gd.Rows.Count > 0)
            {
                if (ddlAppreciate_Type.SelectedValue == "[All]")
                {
                    name = "All Appreciation Type" + "_ For" + ddlAcademicYear.SelectedItem.Text.ToString();
                }
                else
                {
                    name = ddlAppreciate_Type.SelectedItem.Text.ToString() + " Appreciation Type" + "_ For" + ddlAcademicYear.SelectedItem.Text.ToString();
                }

                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gd.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                string msg = "Data not found";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }

        }
        catch (Exception)
        {

        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblTotal_Sending_Summary.Text = "0";
            lblSent.Text = "0";
            lblPending.Text = "0";
            lblError.Text = "0";

            DataTable dt = new DataTable();
            dt = DLobj.Get_Appreciation_Send_Certificate(ddlAcademicYear.SelectedValue.ToString(), ddlAppreciate_Type.SelectedItem.Text.ToString(), ddlStatus.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                rptSendCertificate.DataSource = dt;
                rptSendCertificate.DataBind();
            }
            else
            {
                rptSendCertificate.DataSource = dt;
                rptSendCertificate.DataBind();
                string msg = "No Data Found!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
}