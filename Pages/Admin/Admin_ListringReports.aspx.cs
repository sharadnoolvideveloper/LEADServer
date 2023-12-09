using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using System.Data;
using Ionic.Zip;
using System.Drawing;
using System.Globalization;
using System.Web.Razor.Parser.SyntaxTree;

public partial class Pages_Admin_Admin_ListringReports : System.Web.UI.Page
{
    Reports rpt = new Reports();
    vmCookies cook = new vmCookies();
    GridView gd = new GridView();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // rpt.Admin_FillManagerddl(ddlManager,cook.Admin_Id());
            rpt.Admin_Fillprogramddl(ddlprogramId, cook.Admin_Id());
            rpt.Admin_FillManagerByprogram(ddlprogramId.SelectedValue.ToString(), ddlManager);

        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rpt.Admin_FillManagerByprogram(ddlprogramId.SelectedValue.ToString(), ddlManager);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string FromDate = "";
        string ToDate = "";
        try
        {

            LeadBL BLobj = new LeadBL();
            
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                FromDate = dtFrom.ToString("yyyy-MM-dd");
                DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                ToDate = dtTo.ToString("yyyy-MM-dd");


                if (ddlProjectType.SelectedItem.Text != "[All]")
                {
                    lblSelectedProjectType.Text = ddlProjectType.SelectedItem.Text.ToString();
                }
                else
                {
                    lblSelectedProjectType.Text = " For All Project Type";
                }

                System.Data.DataTable dt = new System.Data.DataTable();
                if (ddlProjectType.SelectedValue == "Registration")
                {
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(),cook.Admin_program());
                    gd.DataSource = dt;
                    gd.DataBind();
                    grdReport.Visible = false;
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {

                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell Slno = new TableCell();
                                    Slno.Text = "slno";
                                    tr1.Cells.Add(Slno);

                                    TableCell Lead_Id = new TableCell();
                                    Lead_Id.Text = "LEAD_Id";
                                    tr1.Cells.Add(Lead_Id);


                                    TableCell studentname = new TableCell();
                                    studentname.Text = "studentname" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(studentname);


                                    TableCell mobileno = new TableCell();
                                    mobileno.Text = "mobileno.";
                                    tr1.Cells.Add(mobileno);



                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);

                                    TableCell gender = new TableCell();
                                    gender.Text = "gender";
                                    tr1.Cells.Add(gender);

                                    TableCell student_type = new TableCell();
                                    student_type.Text = "student_type";
                                    tr1.Cells.Add(student_type);

                                    TableCell DOB = new TableCell();
                                    DOB.Text = "Date_of_Birth";
                                    tr1.Cells.Add(DOB);

                                    TableCell ProfileUpdate_Date = new TableCell();
                                    ProfileUpdate_Date.Text = "ProfileUpdate_Date";
                                    tr1.Cells.Add(ProfileUpdate_Date);

                                    TableCell LastUpdate_Date = new TableCell();
                                    LastUpdate_Date.Text = "LastUpdate_Date";
                                    tr1.Cells.Add(LastUpdate_Date);

                                    TableCell Fees = new TableCell();
                                    Fees.Text = "IsFeesPaid";
                                    tr1.Cells.Add(Fees);

                                    TableCell statename = new TableCell();
                                    statename.Text = "State";
                                    tr1.Cells.Add(statename);

                                    TableCell DistrictName = new TableCell();
                                    DistrictName.Text = "District";
                                    tr1.Cells.Add(DistrictName);

                                    TableCell Taluk_Name = new TableCell();
                                    Taluk_Name.Text = "Taluka";
                                    tr1.Cells.Add(Taluk_Name);

                                    TableCell College_Name = new TableCell();
                                    College_Name.Text = "College_Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(College_Name);



                                    TableCell managername = new TableCell();
                                    managername.Text = "managername" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(managername);

                                    TableCell Student_Talent = new TableCell();
                                    Student_Talent.Text = "StudentTalent" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Student_Talent);

                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);

                                    TableCell Lead_Id = new TableCell();
                                    Lead_Id.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(Lead_Id);

                                    TableCell studentname = new TableCell();
                                    studentname.Text = dt.Rows[i].ItemArray[1].ToString();
                                    tr.Cells.Add(studentname);

                                    TableCell mobileno = new TableCell();
                                    mobileno.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(mobileno);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[3].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell gender = new TableCell();
                                    gender.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(gender);

                                    TableCell student_type = new TableCell();
                                    student_type.Text = dt.Rows[i].ItemArray[5].ToString();
                                    tr.Cells.Add(student_type);



                                    TableCell DOB = new TableCell();
                                    DOB.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(DOB);

                                    TableCell ProfileUpdate_Date = new TableCell();
                                    ProfileUpdate_Date.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(ProfileUpdate_Date);

                                    TableCell LastUpdate_Date = new TableCell();
                                    LastUpdate_Date.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(LastUpdate_Date);

                                    TableCell Fees = new TableCell();
                                    Fees.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(Fees);

                                    TableCell statename = new TableCell();
                                    statename.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(statename);

                                    TableCell DistrictName = new TableCell();
                                    DistrictName.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(DistrictName);

                                    TableCell Taluk_Name = new TableCell();
                                    Taluk_Name.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(Taluk_Name);

                                    TableCell College_Name = new TableCell();
                                    College_Name.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(College_Name);

                                    TableCell managername = new TableCell();
                                    managername.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(managername);

                                    TableCell Student_Talent = new TableCell();
                                    Student_Talent.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(Student_Talent);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if ((ddlProjectType.SelectedValue == "[All]") && (ddlStudentType.SelectedValue == "[All]"))
                {
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    gd.DataSource = dt;
                    gd.DataBind();

                    grdReport.DataSource = dt;
                    grdReport.DataBind();
                    grdReport.Visible = true;
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }

                }
                else if ((ddlProjectType.SelectedValue == "[All]") && (ddlStudentType.SelectedValue != "[All]"))
                {
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    gd.DataSource = dt;
                    gd.DataBind();
                    grdReport.Visible = false;
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            //table1.Attributes.Add("class", "table table-hover");
                            table1.Attributes.Add("class", "list-group table table-hover");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {

                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");


                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SLNO.";
                                    tr1.Cells.Add(Slno);



                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "LEAD_Id";
                                    tr1.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();

                                    CollegeName.Text = "CollegeName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";


                                    CollegeName.Style.Add("width", "300px");
                                    tr1.Cells.Add(CollegeName);



                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "ProposedDate";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "Manager Name";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    //for (int j = 1; j <= Convert.ToInt32(columns); j++)
                                    //{


                                    //    TableCell fld = new TableCell();
                                    //    fld.Text = BL.ReturnString("select [name] from FeeParticulars where [PRIORITY]='" + j.ToString() + "'");
                                    //    tr1.Cells.Add(fld);


                                    //}


                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {

                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");
                                    //tr.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);




                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(LeadId);



                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[5].ToString();
                                    CollegeName.Style.Add("width", "300px");
                                    tr.Cells.Add(CollegeName);


                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(ProjectStatus);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Proposed")
                {
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    grdReport.Visible = false;
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {

                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");


                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SLNO.";
                                    tr1.Cells.Add(Slno);



                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "LEAD_Id";
                                    tr1.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);


                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = "College Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "ProposedDate";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "Manager Name";
                                    tr1.Cells.Add(ManagerName);


                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);


                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = "Student Level";
                                    tr1.Cells.Add(StudentLevel);

                                    //for (int j = 1; j <= Convert.ToInt32(columns); j++)
                                    //{


                                    //    TableCell fld = new TableCell();
                                    //    fld.Text = BL.ReturnString("select [name] from FeeParticulars where [PRIORITY]='" + j.ToString() + "'");
                                    //    tr1.Cells.Add(fld);


                                    //}


                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");
                                    //tr.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);


                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(LeadId);


                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i].ItemArray[5].ToString();
                                    tr.Cells.Add(StudentName);


                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(MailId);


                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ProjectStatus);


                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(StudentLevel);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }

                }
                else if (ddlProjectType.SelectedValue == "Approved")
                {
                    grdReport.Visible = false;
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SL NO.";
                                    tr1.Cells.Add(Slno);


                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "Lead_Id";
                                    tr1.Cells.Add(LeadId);



                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);


                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = "CollegeName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "ProposedDate";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);


                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = "ApprovedDate";
                                    tr1.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "Requested Amount";
                                    tr1.Cells.Add(RequestedAmount);


                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = "Approved Amount";
                                    tr1.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = "Disperse Amount";
                                    tr1.Cells.Add(DisperseAmount);

                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = "Balace Amount";
                                    tr1.Cells.Add(BalanceAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "ManagerName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell Student_Level = new TableCell();
                                    Student_Level.Text = "Student level";
                                    tr1.Cells.Add(Student_Level);

                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(PDId);

                                    PDId.Style.Add("display", "none");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);


                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i].ItemArray[5].ToString();
                                    tr.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(RequestedAmount);



                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = BLobj.Student_GetProjectTotalFunded(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[2].ToString());
                                    tr.Cells.Add(DisperseAmount);


                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = Convert.ToString(int.Parse(ApprovedAmount.Text) - int.Parse(DisperseAmount.Text));
                                    tr.Cells.Add(BalanceAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell Student_Level = new TableCell();
                                    Student_Level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(Student_Level);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "RequestForModification")
                {
                    grdReport.Visible = false;
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {

                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");


                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SLNO.";
                                    tr1.Cells.Add(Slno);



                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "LEAD_Id";
                                    tr1.Cells.Add(LeadId);


                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = "College Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "ProposedDate";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "Manager Name";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = "Student Level";
                                    tr1.Cells.Add(StudentLevel);


                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = "Manager Comments";
                                    tr1.Cells.Add(ManagerComments);

                                    TableCell ModificationRequestDate = new TableCell();
                                    ModificationRequestDate.Text = "RequestForModification";
                                    tr1.Cells.Add(ModificationRequestDate);

                                    //for (int j = 1; j <= Convert.ToInt32(columns); j++)
                                    //{


                                    //    TableCell fld = new TableCell();
                                    //    fld.Text = BL.ReturnString("select [name] from FeeParticulars where [PRIORITY]='" + j.ToString() + "'");
                                    //    tr1.Cells.Add(fld);


                                    //}


                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");
                                    //tr.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);

                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i].ItemArray[5].ToString();
                                    tr.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(StudentLevel);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(ManagerComments);

                                    TableCell RequestForModificationDate = new TableCell();
                                    RequestForModificationDate.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(RequestForModificationDate);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "RequestForCompletion")
                {
                    grdReport.Visible = false;
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SL NO.";
                                    tr1.Cells.Add(Slno);

                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "LEAD_Id";
                                    tr1.Cells.Add(LeadId);


                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = "College Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "Proposed Date";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);


                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = "Approved Date";
                                    tr1.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "Requested Amount";
                                    tr1.Cells.Add(RequestedAmount);


                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = "Approved Amount";
                                    tr1.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = "Disperse Amount";
                                    tr1.Cells.Add(DisperseAmount);

                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = "Balace Amount";
                                    tr1.Cells.Add(BalanceAmount);


                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "Manager Name";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = "Student Level";
                                    tr1.Cells.Add(StudentLevel);


                                    TableCell RequestForCompletion = new TableCell();
                                    RequestForCompletion.Text = "Requested Date";
                                    tr1.Cells.Add(RequestForCompletion);


                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = "Manager Comments";
                                    tr1.Cells.Add(ManagerComments);

                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(PDId);

                                    PDId.Style.Add("display", "none");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);

                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i].ItemArray[5].ToString();
                                    tr.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i].ItemArray[16].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[17].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(RequestedAmount);



                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = BLobj.Student_GetProjectTotalFunded(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[2].ToString());
                                    tr.Cells.Add(DisperseAmount);


                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = Convert.ToString(int.Parse(ApprovedAmount.Text) - int.Parse(DisperseAmount.Text));
                                    tr.Cells.Add(BalanceAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(ManagerName);


                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(ProjectStatus);


                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(StudentLevel);

                                    TableCell CompletionRequest = new TableCell();
                                    CompletionRequest.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(CompletionRequest);



                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(ManagerComments);

                                }
                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Completed")
                {
                    grdReport.Visible = false;
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SLNO.";
                                    tr1.Cells.Add(Slno);



                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "LEAD_Id";
                                    tr1.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = "CollegeName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "ProposedDate";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);


                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = "ApprovedDate";
                                    tr1.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);


                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = "ApprovedAmount";
                                    tr1.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = "DisperseAmount";
                                    tr1.Cells.Add(DisperseAmount);

                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = "Balace Amount";
                                    tr1.Cells.Add(BalanceAmount);


                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "ManagerName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "ProjectStatus";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = "Student Level";
                                    tr1.Cells.Add(StudentLevel);

                                    TableCell ProjectLevel = new TableCell();
                                    ProjectLevel.Text = "Project Level";
                                    tr1.Cells.Add(ProjectLevel);

                                    TableCell Rating = new TableCell();
                                    Rating.Text = "Rating";
                                    tr1.Cells.Add(Rating);


                                    TableCell RequestForCompletion = new TableCell();
                                    RequestForCompletion.Text = "RequestedDate";
                                    tr1.Cells.Add(RequestForCompletion);


                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = "CompletedDate";
                                    tr1.Cells.Add(CompletedDate);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = "ManagerComments" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerComments);

                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(PDId);

                                    PDId.Style.Add("display", "none");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);



                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i].ItemArray[5].ToString();
                                    tr.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i].ItemArray[18].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[19].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(RequestedAmount);

                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = BLobj.Student_GetProjectTotalFunded(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[2].ToString());
                                    tr.Cells.Add(DisperseAmount);


                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = Convert.ToString(int.Parse(ApprovedAmount.Text) - int.Parse(DisperseAmount.Text));
                                    tr.Cells.Add(BalanceAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(StudentLevel);


                                    TableCell ProjecLevel = new TableCell();
                                    ProjecLevel.Text = dt.Rows[i].ItemArray[22].ToString();
                                    tr.Cells.Add(ProjecLevel);


                                    TableCell Rating = new TableCell();
                                    Rating.Text = dt.Rows[i].ItemArray[16].ToString();
                                    tr.Cells.Add(Rating);




                                    TableCell CompletionRequest = new TableCell();
                                    CompletionRequest.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(CompletionRequest);


                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(CompletedDate);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = dt.Rows[i].ItemArray[17].ToString();
                                    tr.Cells.Add(ManagerComments);

                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Rejected")
                {
                    grdReport.Visible = false;
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {

                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");


                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SL NO.";
                                    tr1.Cells.Add(Slno);


                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "LEAD_Id";
                                    tr1.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = "College Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "Proposed Date";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell RejectedDate = new TableCell();
                                    RejectedDate.Text = "Rejected Date";
                                    tr1.Cells.Add(RejectedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "Manager Name";
                                    tr1.Cells.Add(ManagerName);


                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    //for (int j = 1; j <= Convert.ToInt32(columns); j++)
                                    //{


                                    //    TableCell fld = new TableCell();
                                    //    fld.Text = BL.ReturnString("select [name] from FeeParticulars where [PRIORITY]='" + j.ToString() + "'");
                                    //    tr1.Cells.Add(fld);


                                    //}


                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i].ItemArray[0].ToString();
                                    tr.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");
                                    //tr.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);

                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    tr.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i].ItemArray[5].ToString();
                                    tr.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell RejectedDate = new TableCell();
                                    RejectedDate.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(RejectedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(RequestedAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(ProjectStatus);
                                }
                                table1.Rows.Add(tr);
                            }
                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Impact")
                {
                    grdReport.Visible = false;
                    dt = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                    if (dt.Rows.Count > 0)
                    {
                        lblCount.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        lblCount.Text = "0";
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("class", "list-group table");
                            for (int i = 0; i <= dt.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    TableRow tr1 = null;
                                    tr1 = new TableRow();

                                    TableCell PDId = new TableCell();
                                    PDId.Text = "PDId";
                                    tr1.Cells.Add(PDId);
                                    PDId.Style.Add("Display", "None");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = "SLNO.";
                                    tr1.Cells.Add(Slno);



                                    TableCell LeadId = new TableCell();
                                    LeadId.Text = "LEAD_Id";
                                    tr1.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = "Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = "Mobileno";
                                    tr1.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = "MailId";
                                    tr1.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = "CollegeName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "ProposedDate";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);


                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = "ApprovedDate";
                                    tr1.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);


                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = "ApprovedAmount";
                                    tr1.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = "DisperseAmount";
                                    tr1.Cells.Add(DisperseAmount);

                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = "Balace Amount";
                                    tr1.Cells.Add(BalanceAmount);


                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "ManagerName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "ProjectStatus";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = "StudentLevel";
                                    tr1.Cells.Add(StudentLevel);

                                    TableCell ProjectLevel = new TableCell();
                                    ProjectLevel.Text = "ProjectLevel";
                                    tr1.Cells.Add(ProjectLevel);

                                    TableCell Rating = new TableCell();
                                    Rating.Text = "Rating";
                                    tr1.Cells.Add(Rating);


                                    TableCell RequestForCompletion = new TableCell();
                                    RequestForCompletion.Text = "RequestedDate";
                                    tr1.Cells.Add(RequestForCompletion);


                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = "CompletedDate";
                                    tr1.Cells.Add(CompletedDate);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = "ManagerComments" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerComments);

                                    TableCell ImpactDate = new TableCell();
                                    ImpactDate.Text = "Impact_Date" + "&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ImpactDate);

                                    TableCell Collaboration_Supported = new TableCell();
                                    Collaboration_Supported.Text = "Collaboration_Supported" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Collaboration_Supported);

                                    TableCell Permission_And_Activities = new TableCell();
                                    Permission_And_Activities.Text = "Permission_And_Activities" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Permission_And_Activities);

                                    TableCell Experience_Of_Initiative = new TableCell();
                                    Experience_Of_Initiative.Text = "Experience_Of_Initiative" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Experience_Of_Initiative);

                                    TableCell Lacking_initiative = new TableCell();
                                    Lacking_initiative.Text = "Lacking_initiative" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Lacking_initiative);

                                    TableCell Against_Tide = new TableCell();
                                    Against_Tide.Text = "Against_Tide" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Against_Tide);

                                    TableCell Cross_Hurdles = new TableCell();
                                    Cross_Hurdles.Text = "Cross_Hurdles" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Cross_Hurdles);

                                    TableCell Entrepreneurial_Venture = new TableCell();
                                    Entrepreneurial_Venture.Text = "Entrepreneurial_Venture" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Entrepreneurial_Venture);

                                    TableCell Government_Awarded = new TableCell();
                                    Government_Awarded.Text = "Government_Awarded" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Government_Awarded);

                                    TableCell Leadership_Roles = new TableCell();
                                    Leadership_Roles.Text = "Leadership_Roles" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Leadership_Roles);



                                    table1.Rows.Add(tr1);
                                }
                                TableRow tr = null;
                                tr = new TableRow();
                                if (i < dt.Rows.Count)
                                {
                                    TableCell PDId = new TableCell();
                                    PDId.Text = dt.Rows[i]["PDID"].ToString();
                                    tr.Cells.Add(PDId);

                                    PDId.Style.Add("display", "none");

                                    TableCell Slno = new TableCell();
                                    Slno.Text = (i + 1).ToString();
                                    tr.Cells.Add(Slno);



                                    TableCell LeadId = new TableCell();
                                    //  LeadId.Text = dt.Rows[i].ItemArray[2].ToString();
                                    LeadId.Text = dt.Rows[i]["Lead_Id"].ToString();
                                    tr.Cells.Add(LeadId);

                                    TableCell StudentName = new TableCell();
                                    StudentName.Text = dt.Rows[i]["StudentName"].ToString();
                                    tr.Cells.Add(StudentName);

                                    TableCell MobileNo = new TableCell();
                                    MobileNo.Text = dt.Rows[i]["MobileNo"].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i]["MailId"].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i]["College_Name"].ToString();
                                    tr.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i]["ProposedDate"].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i]["Title"].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = dt.Rows[i]["ApprovedDate"].ToString();
                                    tr.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i]["RequestedAmount"].ToString();
                                    tr.Cells.Add(RequestedAmount);

                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = dt.Rows[i]["ApprovedAmount"].ToString();
                                    tr.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = BLobj.Student_GetProjectTotalFunded(dt.Rows[i]["PDId"].ToString(), dt.Rows[i]["Lead_Id"].ToString());
                                    tr.Cells.Add(DisperseAmount);


                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = Convert.ToString(int.Parse(ApprovedAmount.Text) - int.Parse(DisperseAmount.Text));
                                    tr.Cells.Add(BalanceAmount);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i]["ManagerName"].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i]["ProjectStatus"].ToString();
                                    tr.Cells.Add(ProjectStatus);


                                    TableCell StudentLevel = new TableCell();
                                    StudentLevel.Text = dt.Rows[i]["student_type"].ToString();
                                    tr.Cells.Add(StudentLevel);

                                    TableCell ProjectLevel = new TableCell();
                                    ProjectLevel.Text = dt.Rows[i]["Project_Levels"].ToString();
                                    tr.Cells.Add(ProjectLevel);

                                    TableCell Rating = new TableCell();
                                    Rating.Text = dt.Rows[i]["Rating"].ToString();
                                    tr.Cells.Add(Rating);




                                    TableCell CompletionRequest = new TableCell();
                                    CompletionRequest.Text = dt.Rows[i]["RequestForCompletionDate"].ToString();
                                    tr.Cells.Add(CompletionRequest);


                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = dt.Rows[i]["CompletionDate"].ToString();
                                    tr.Cells.Add(CompletedDate);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = dt.Rows[i]["ManagerComments"].ToString();
                                    tr.Cells.Add(ManagerComments);

                                    TableCell ImpactDate = new TableCell();
                                    ImpactDate.Text = dt.Rows[i]["ImpactDate"].ToString();
                                    tr.Cells.Add(ImpactDate);

                                    TableCell Collaboration_Supported = new TableCell();
                                    Collaboration_Supported.Text = dt.Rows[i]["Collaboration_Supported"].ToString();
                                    tr.Cells.Add(Collaboration_Supported);

                                    TableCell Permission_And_Activities = new TableCell();
                                    Permission_And_Activities.Text = dt.Rows[i]["Permission_And_Activities"].ToString();
                                    tr.Cells.Add(Permission_And_Activities);

                                    TableCell Experience_Of_Initiative = new TableCell();
                                    Experience_Of_Initiative.Text = dt.Rows[i]["Experience_Of_Initiative"].ToString();
                                    tr.Cells.Add(Experience_Of_Initiative);

                                    TableCell Lacking_initiative = new TableCell();
                                    Lacking_initiative.Text = dt.Rows[i]["Lacking_initiative"].ToString();
                                    tr.Cells.Add(Lacking_initiative);

                                    TableCell Against_Tide = new TableCell();
                                    Against_Tide.Text = dt.Rows[i]["Against_Tide"].ToString();
                                    tr.Cells.Add(Against_Tide);

                                    TableCell Cross_Hurdles = new TableCell();
                                    Cross_Hurdles.Text = dt.Rows[i]["Cross_Hurdles"].ToString();
                                    tr.Cells.Add(Cross_Hurdles);

                                    TableCell Entrepreneurial_Venture = new TableCell();
                                    Entrepreneurial_Venture.Text = dt.Rows[i]["Entrepreneurial_Venture"].ToString();
                                    tr.Cells.Add(Entrepreneurial_Venture);

                                    TableCell Government_Awarded = new TableCell();
                                    Government_Awarded.Text = dt.Rows[i]["Government_Awarded"].ToString();
                                    tr.Cells.Add(Government_Awarded);

                                    TableCell Leadership_Roles = new TableCell();
                                    Leadership_Roles.Text = dt.Rows[i]["Leadership_Roles"].ToString();
                                    tr.Cells.Add(Leadership_Roles);

                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
            }
            else
            {

                if (txtFromDate.Text == "")
                {
                    string msg = "Select From Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtFromDate.Focus();
                }
                else if (txtToDate.Text == "")
                {
                    string msg = "Select To Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtToDate.Focus();
                }
            }
        }
        catch (Exception ex)
        {

            string msg = "Error : " + ex.Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
    }

    //public string GetWhere()
    //{
    //    string qry = "";
    //    if (ddlProjectType.SelectedValue.ToString() == "Registration")
    //    {
    //        if((ddlManager.SelectedValue.ToString() == "[All]") && (ddlStudentType.SelectedValue.ToString() == "[All]"))
    //        {

    //        }
    //    }
    //   else  if ((ddlManager.SelectedValue.ToString() == "[All]") && (ddlProjectType.SelectedValue.ToString() == "[All]") && (ddlStudentType.SelectedValue.ToString() == "[All]"))
    //    {
    //        qry = "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '"+txtFromDate.ToString()+"' and '"+txtToDate.ToString()+"' and sr.collegecode = clg.collegeid"+" "+
    //                "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";

    //    }
    //    else if ((ddlManager.SelectedValue.ToString() == "[All]") && (ddlProjectType.SelectedValue.ToString() == "[All]") && (ddlStudentType.SelectedValue.ToString() == "[All]"))
    //    {
    //        qry = "";
    //    }

    //    return "";
    //}
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        string FromDate = "";
        string ToDate = "";
        try
        {
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {


                DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                FromDate = dtFrom.ToString("yyyy-MM-dd");
                DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                ToDate = dtTo.ToString("yyyy-MM-dd");
                GridView gd = new GridView();
                gd.DataSource = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                gd.DataBind();
                string name = "";
                if (gd.Rows.Count > 0)
                {
                    if (ddlManager.SelectedValue == "[All]")
                    {
                        name = "All Managers Details" + "_ From" + FromDate.ToString() + "To" + ToDate.ToString();
                    }
                    else
                    {
                        name = ddlManager.SelectedItem.Text.ToString() + "_ From" + FromDate.ToString() + "To" + ToDate.ToString();
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
            else
            {

                if (txtFromDate.Text == "")
                {
                    string msg = "Select From Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtFromDate.Focus();
                }
                else if (txtToDate.Text == "")
                {
                    string msg = "Select To Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtToDate.Focus();
                }
            }
        }
        catch (Exception)
        {

        }
    }
    //public void Excel()
    //{

    //    GridView gd = new GridView();
    //    string name = "";
    //    gd.DataSource = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString());
    //    gd.DataBind();
    //    if (gd.Rows.Count > 0)
    //    {
    //        if (ddlManager.SelectedValue == "[All]")
    //        {
    //            name = "All Managers Details" + "_ From" + txtFromDate.Text.ToString() + "To" + txtToDate.Text.ToString();
    //        }
    //        else
    //        {
    //            name = ddlManager.SelectedItem.Text.ToString() + "_ From" + txtFromDate.Text.ToString() + "To" + txtToDate.Text.ToString();
    //        }
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.AddHeader("content-disposition", "attachment;filename=" + name + ".xls");
    //        Response.Charset = "";
    //        Response.ContentType = "application/vnd.ms-excel";
    //        using (StringWriter sw = new StringWriter())
    //        {
    //            HtmlTextWriter hw = new HtmlTextWriter(sw);

    //            //To Export all pages
    //            gd.AllowPaging = false;



    //            gd.HeaderRow.BackColor = Color.White;
    //            foreach (TableCell cell in gd.HeaderRow.Cells)
    //            {
    //                cell.BackColor = gd.HeaderStyle.BackColor;
    //            }
    //            foreach (GridViewRow row in gd.Rows)
    //            {
    //                row.BackColor = Color.White;
    //                foreach (TableCell cell in row.Cells)
    //                {
    //                    if (row.RowIndex % 2 == 0)
    //                    {
    //                        cell.BackColor = gd.AlternatingRowStyle.BackColor;
    //                    }
    //                    else
    //                    {
    //                        cell.BackColor = gd.RowStyle.BackColor;
    //                    }
    //                    cell.CssClass = "textmode";
    //                }
    //            }

    //            gd.RenderControl(hw);

    //            //style to format numbers to string
    //            string style = @"<style> .textmode { } </style>";
    //            Response.Write(style);
    //            Response.Output.Write(sw.ToString());
    //            Response.Flush();
    //            Response.End();
    //        }
    //    }
    //}

    protected void btnPDFReport_Click(object sender, EventArgs e)
    {
        string FromDate = "";
        string ToDate = "";
        try
        {
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                FromDate = dtFrom.ToString("yyyy-MM-dd");
                DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                ToDate = dtTo.ToString("yyyy-MM-dd");
                GridView gd = new GridView();
                gd.DataSource = rpt.Manager_GetListingReportData(FromDate.ToString(), ToDate.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), ddlManager.SelectedValue.ToString(), cook.Admin_program());
                gd.DataBind();
                string name = "";
                if (gd.Rows.Count > 0)
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    gd.AllowPaging = false;
                    gd.DataBind();
                    gd.RenderControl(hw);
                    if (ddlManager.SelectedValue == "[All]")
                    {
                        name = "All Managers Details" + "_ From" + FromDate.ToString() + "To" + ToDate.ToString();
                    }
                    else
                    {
                        name = ddlManager.SelectedItem.Text.ToString() + "_ From" + FromDate.ToString() + "To" + ToDate.ToString();
                    }

                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".pdf");
                    Response.ContentType = "application/pdf";
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.LEDGER, 0f, 0f, 0f, 0f);
                    //  pdfDoc.SetPageSize(iTextSharp.text.PageSize.LEGAL.Rotate());



                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.Write(pdfDoc);
                    Response.End();

                }
                else
                {
                    string msg = "No Data Found Check Between Dates";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {

                if (txtFromDate.Text == "")
                {
                    string msg = "Select From Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtFromDate.Focus();
                }
                else if (txtToDate.Text == "")
                {
                    string msg = "Select To Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtToDate.Focus();
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnFolderWiseDownload_Click(object sender, EventArgs e)
    {
        string msg = "";
        string FromDate = "";
        string ToDate = "";
        try
        {

            if ((txtFromDate.Text.ToString() != "") && (txtToDate.Text.ToString() != ""))
            {
                DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                FromDate = dtFrom.ToString("yyyy-MM-dd");
                DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                ToDate = dtTo.ToString("yyyy-MM-dd");

                string sourcePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
                // Use Path class to manipulate file and directory paths.
                //string sourceFile = System.IO.Path.Combine(Server.MapPath(sourcePath), fileName);
                //string destFile = System.IO.Path.Combine(Server.MapPath(targetPath), fileName);
                System.Data.DataTable dt = rpt.Manager_StudentWithProjectsFolderCreating(ddlManager.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString());

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                foreach (DataRow dr in dt.Rows)
                {
                    bool IsExists = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id()));
                    if (!IsExists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + cook.Manager_Id()));
                    }
                    bool isTitleExists = System.IO.Directory.Exists(Server.MapPath(targetPath + cook.Manager_Id() + dr[0].ToString() + dr[2].ToString()));

                    if (!isTitleExists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(targetPath + cook.Manager_Id() + "/" + dr[0].ToString() + "/" + dr[2].ToString()));
                        System.Data.DataTable dtfiles = rpt.Manager_StudentWithDigitalDocuments(dr[1].ToString());
                        string dest = Server.MapPath(targetPath + cook.Manager_Id() + "/" + dr[0].ToString() + "/" + dr[2].ToString());
                        foreach (DataRow dr1 in dtfiles.Rows)
                        {
                            string fileName = System.IO.Path.GetFileName(Server.MapPath(dr1[1].ToString()));
                            bool isImgExists = File.Exists(Server.MapPath(sourcePath + fileName));
                            if (isImgExists == true)
                            {
                                string destFile = System.IO.Path.Combine(Server.MapPath(targetPath + cook.Manager_Id() + "/" + dr[0].ToString() + "/" + dr[2].ToString()), fileName);
                                System.IO.File.Copy(Server.MapPath(dr1[1].ToString()), destFile, true);
                            }

                        }
                    }
                    CreateWordDocuments(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

                    //System.IO.File.Copy(sourceFile, Server.MapPath(targetPath+dt.Rows[0].ItemArray[0].ToString()), true);
                }
                if (dt.Rows.Count > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {


                        Response.Clear();
                        Response.ContentType = "application/zip";

                        Response.AddHeader("content-disposition", "filename=" + "Project_Details_between " + "_" + FromDate.ToString() + "and" + ToDate.ToString() + "_LEAD.zip");



                        // zip.AddDirectory(Server.MapPath("/FilesDownload/26"));
                        zip.AddDirectory(Server.MapPath(targetPath + cook.Manager_Id()));
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }
                }
                else
                {
                    msg = "No Data Fount Select Different Dates";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {

                if (txtFromDate.Text == "")
                {
                    msg = "Select From Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtFromDate.Focus();
                }
                else if (txtToDate.Text == "")
                {
                    msg = "Select To Date";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                    txtToDate.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            msg = "Some thing Went Wrong" + "-" + ex.Message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

        }
    }
    public void CreateWordDocuments(string Lead_Id, string PDId, string Title)
    {
        Reports rpt = new Reports();
        string targetPath = ConfigurationManager.AppSettings["FilesDownload"].ToString();
        string MainFolder = Server.MapPath(targetPath);
        string filename = Server.MapPath(targetPath + cook.Manager_Id() + "/" + Lead_Id.ToString() + "/" + Title.ToString() + "/" + Title.ToString() + ".docx");

        rpt.CreateWordDocuments(PDId, targetPath, MainFolder, filename);


    }
}