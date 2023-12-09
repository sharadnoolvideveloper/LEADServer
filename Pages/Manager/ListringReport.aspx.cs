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

public partial class Pages_Manager_ListringReport : System.Web.UI.Page
{
    Reports rpt = new Reports();
    vmCookies cook = new vmCookies();
    GridView gd = new GridView();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {

            LeadBL BLobj = new LeadBL();
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                if ((ddlProjectType.SelectedValue == "[All]") && (ddlStudentType.SelectedValue == "[All]"))
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(),cook.Manager_program());
                    gd.DataSource = dt;
                    gd.DataBind();
                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");

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
                                    CollegeName.Text = "CollegeName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "ProposedDate";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "Requested_Amount";
                                    tr1.Cells.Add(RequestedAmount);

                                    TableCell SanctionAmount = new TableCell();
                                    SanctionAmount.Text = "Sanction_Amount";
                                    tr1.Cells.Add(SanctionAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = "Disperse_Amount";
                                    tr1.Cells.Add(DisperseAmount);

                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = "Balance";
                                    tr1.Cells.Add(BalanceAmount);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = "Student_level" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(student_level);


                                    TableCell Project_levels = new TableCell();
                                    Project_levels.Text = "Project_level" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Project_levels);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);

                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = "Approved_Date";
                                    tr1.Cells.Add(ApprovedDate);

                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = "Completed_Date";
                                    tr1.Cells.Add(CompletedDate);

                                    TableCell Rejected_Date = new TableCell();
                                    Rejected_Date.Text = "Rejected_Date";
                                    tr1.Cells.Add(Rejected_Date);

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
                                    MobileNo.Text = dt.Rows[i].ItemArray[6].ToString();
                                    tr.Cells.Add(MobileNo);

                                    TableCell MailId = new TableCell();
                                    MailId.Text = dt.Rows[i].ItemArray[7].ToString();
                                    tr.Cells.Add(MailId);

                                    TableCell CollegeName = new TableCell();
                                    CollegeName.Text = dt.Rows[i].ItemArray[8].ToString();
                                    tr.Cells.Add(CollegeName);



                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = dt.Rows[i].ItemArray[9].ToString();
                                    tr.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(RequestedAmount);

                                    TableCell SansantionAmount = new TableCell();
                                    SansantionAmount.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(SansantionAmount);

                                    TableCell DisperAmount = new TableCell();
                                    DisperAmount.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(DisperAmount);

                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(BalanceAmount);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);

                                    TableCell Project_levels = new TableCell();
                                    Project_levels.Text = dt.Rows[i].ItemArray[21].ToString();
                                    tr.Cells.Add(Project_levels);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[17].ToString();
                                    tr.Cells.Add(Semester);

                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = dt.Rows[i].ItemArray[18].ToString();
                                    tr.Cells.Add(ApprovedDate);

                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = dt.Rows[i].ItemArray[19].ToString();
                                    tr.Cells.Add(CompletedDate);

                                    TableCell RejectedDate = new TableCell();
                                    RejectedDate.Text = dt.Rows[i].ItemArray[20].ToString();
                                    tr.Cells.Add(RejectedDate);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if ((ddlProjectType.SelectedValue == "[All]") && (ddlStudentType.SelectedValue != "[All]"))
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
                    gd.DataSource = dt;
                    gd.DataBind();
                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");
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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = "student level";
                                    tr1.Cells.Add(student_level);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);


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
                                    CollegeName.Style.Add("width", "300px");
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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[3].ToString();
                                    tr.Cells.Add(student_level);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(Semester);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Proposed")
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());

                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");
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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = "student level";
                                    tr1.Cells.Add(student_level);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);

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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);


                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(Semester);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }

                }
                else if (ddlProjectType.SelectedValue == "Approved")
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");
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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = "student level";
                                    tr1.Cells.Add(student_level);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "ManagerName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerName);
                                    table1.Rows.Add(tr1);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);
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


                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(ManagerName);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[16].ToString();
                                    tr.Cells.Add(Semester);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "RequestForModification")
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");
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

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = "Studentlevel";
                                    tr1.Cells.Add(student_level);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = "ManagerComments" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerComments);

                                    TableCell ModificationRequestDate = new TableCell();
                                    ModificationRequestDate.Text = "RequestForModificationDate";
                                    tr1.Cells.Add(ModificationRequestDate);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);

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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[10].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(ManagerComments);

                                    TableCell RequestForModificationDate = new TableCell();
                                    RequestForModificationDate.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(RequestForModificationDate);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[16].ToString();
                                    tr.Cells.Add(Semester);
                                }

                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "RequestForCompletion")
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");
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
                                    CollegeName.Text = "College Name" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(CollegeName);

                                    TableCell ProposedDate = new TableCell();
                                    ProposedDate.Text = "Proposed_Date";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);


                                    TableCell ApprovedDate = new TableCell();
                                    ApprovedDate.Text = "Approved_Date";
                                    tr1.Cells.Add(ApprovedDate);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "Requested_Amount";
                                    tr1.Cells.Add(RequestedAmount);


                                    TableCell ApprovedAmount = new TableCell();
                                    ApprovedAmount.Text = "Approved_Amount";
                                    tr1.Cells.Add(ApprovedAmount);

                                    TableCell DisperseAmount = new TableCell();
                                    DisperseAmount.Text = "Disperse_Amount";
                                    tr1.Cells.Add(DisperseAmount);

                                    TableCell BalanceAmount = new TableCell();
                                    BalanceAmount.Text = "Balance_Amount";
                                    tr1.Cells.Add(BalanceAmount);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project_Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell Student_level = new TableCell();
                                    Student_level.Text = "Student_level";
                                    tr1.Cells.Add(Student_level);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "Manager_Name";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell RequestForCompletion = new TableCell();
                                    RequestForCompletion.Text = "Requested_Date";
                                    tr1.Cells.Add(RequestForCompletion);


                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = "Manager Comments" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerComments);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);

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


                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(ManagerName);


                                    TableCell CompletionRequest = new TableCell();
                                    CompletionRequest.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(CompletionRequest);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(ManagerComments);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[18].ToString();
                                    tr.Cells.Add(Semester);

                                }
                                table1.Rows.Add(tr);

                            }

                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Completed")
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");
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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "ProjectStatus";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = "student_level";
                                    tr1.Cells.Add(student_level);

                                    TableCell Project_levels = new TableCell();
                                    Project_levels.Text = "Project_level";
                                    tr1.Cells.Add(Project_levels);

                                    TableCell Rating = new TableCell();
                                    Rating.Text = "Rating";
                                    tr1.Cells.Add(Rating);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = "ManagerName" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerName);

                                    TableCell RequestForCompletion = new TableCell();
                                    RequestForCompletion.Text = "RequestedDate";
                                    tr1.Cells.Add(RequestForCompletion);


                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = "CompletedDate";
                                    tr1.Cells.Add(CompletedDate);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = "ManagerComments" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(ManagerComments);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);

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


                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[12].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);

                                    TableCell Project_levels = new TableCell();
                                    Project_levels.Text = dt.Rows[i].ItemArray[21].ToString();
                                    tr.Cells.Add(Project_levels);

                                    TableCell Rating = new TableCell();
                                    Rating.Text = dt.Rows[i].ItemArray[16].ToString();
                                    tr.Cells.Add(Rating);

                                    TableCell ManagerName = new TableCell();
                                    ManagerName.Text = dt.Rows[i].ItemArray[13].ToString();
                                    tr.Cells.Add(ManagerName);


                                    TableCell CompletionRequest = new TableCell();
                                    CompletionRequest.Text = dt.Rows[i].ItemArray[14].ToString();
                                    tr.Cells.Add(CompletionRequest);


                                    TableCell CompletedDate = new TableCell();
                                    CompletedDate.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(CompletedDate);

                                    TableCell ManagerComments = new TableCell();
                                    ManagerComments.Text = dt.Rows[i].ItemArray[17].ToString();
                                    tr.Cells.Add(ManagerComments);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[20].ToString();
                                    tr.Cells.Add(Semester);
                                }
                                table1.Rows.Add(tr);
                            }
                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Rejected")
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
                    lblCount.Text = dt.Rows.Count.ToString();
                    {
                        if (dt.Rows.Count > 0)
                        {

                            Table table1 = new Table();
                            table1.Attributes.Add("id", "Listing");
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
                                    ProposedDate.Text = "Proposed_Date";
                                    tr1.Cells.Add(ProposedDate);

                                    TableCell RejectedDate = new TableCell();
                                    RejectedDate.Text = "Rejected_Date";
                                    tr1.Cells.Add(RejectedDate);

                                    TableCell Title = new TableCell();
                                    Title.Text = "Title" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    tr1.Cells.Add(Title);

                                    TableCell RequestedAmount = new TableCell();
                                    RequestedAmount.Text = "RequestedAmount";
                                    tr1.Cells.Add(RequestedAmount);

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = "Project Status";
                                    tr1.Cells.Add(ProjectStatus);

                                    TableCell Student_level = new TableCell();
                                    Student_level.Text = "Student level";
                                    tr1.Cells.Add(Student_level);

                                    TableCell Semester = new TableCell();
                                    Semester.Text = "Semester";
                                    tr1.Cells.Add(Semester);

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

                                    TableCell ProjectStatus = new TableCell();
                                    ProjectStatus.Text = dt.Rows[i].ItemArray[11].ToString();
                                    tr.Cells.Add(ProjectStatus);

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);


                                    TableCell Semester = new TableCell();
                                    Semester.Text = dt.Rows[i].ItemArray[15].ToString();
                                    tr.Cells.Add(Semester);
                                }
                                table1.Rows.Add(tr);
                            }
                            Listing_table.Controls.Add(table1);
                        }
                    }
                }
                else if (ddlProjectType.SelectedValue == "Impact")
                {
                    dt = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
                    lblCount.Text = dt.Rows.Count.ToString();
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

                                    TableCell Student_level = new TableCell();
                                    Student_level.Text = "Student_level";
                                    tr1.Cells.Add(Student_level);

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

                                    TableCell student_level = new TableCell();
                                    student_level.Text = dt.Rows[i].ItemArray[4].ToString();
                                    tr.Cells.Add(student_level);


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

                                    TableCell ImpactDate = new TableCell();
                                    ImpactDate.Text = dt.Rows[i].ItemArray[21].ToString();
                                    tr.Cells.Add(ImpactDate);

                                    TableCell Collaboration_Supported = new TableCell();
                                    Collaboration_Supported.Text = dt.Rows[i].ItemArray[22].ToString();
                                    tr.Cells.Add(Collaboration_Supported);

                                    TableCell Permission_And_Activities = new TableCell();
                                    Permission_And_Activities.Text = dt.Rows[i].ItemArray[23].ToString();
                                    tr.Cells.Add(Permission_And_Activities);

                                    TableCell Experience_Of_Initiative = new TableCell();
                                    Experience_Of_Initiative.Text = dt.Rows[i].ItemArray[24].ToString();
                                    tr.Cells.Add(Experience_Of_Initiative);

                                    TableCell Lacking_initiative = new TableCell();
                                    Lacking_initiative.Text = dt.Rows[i].ItemArray[25].ToString();
                                    tr.Cells.Add(Lacking_initiative);

                                    TableCell Against_Tide = new TableCell();
                                    Against_Tide.Text = dt.Rows[i].ItemArray[26].ToString();
                                    tr.Cells.Add(Against_Tide);

                                    TableCell Cross_Hurdles = new TableCell();
                                    Cross_Hurdles.Text = dt.Rows[i].ItemArray[27].ToString();
                                    tr.Cells.Add(Cross_Hurdles);

                                    TableCell Entrepreneurial_Venture = new TableCell();
                                    Entrepreneurial_Venture.Text = dt.Rows[i].ItemArray[28].ToString();
                                    tr.Cells.Add(Entrepreneurial_Venture);

                                    TableCell Government_Awarded = new TableCell();
                                    Government_Awarded.Text = dt.Rows[i].ItemArray[29].ToString();
                                    tr.Cells.Add(Government_Awarded);

                                    TableCell Leadership_Roles = new TableCell();
                                    Leadership_Roles.Text = dt.Rows[i].ItemArray[30].ToString();
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

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {

            GridView gd = new GridView();
            gd.DataSource = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {


                string name = cook.ManagerName() + "_" + System.DateTime.Now.ToString() + "_" + ddlProjectType.SelectedItem.Text.ToString();
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
        catch (Exception)
        {

        }
    }

    protected void btnPDFReport_Click(object sender, EventArgs e)
    {
        try
        {

            GridView gd = new GridView();
            gd.DataSource = rpt.Manager_GetListingReportData(txtFromDate.Text.ToString(), txtToDate.Text.ToString(), ddlStudentType.SelectedValue.ToString(), ddlProjectType.SelectedValue.ToString(), cook.Manager_Id(), cook.Manager_program());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gd.AllowPaging = false;
                gd.DataBind();
                gd.RenderControl(hw);
                string name = cook.ManagerName() + "_" + System.DateTime.Now.ToString() + "_" + ddlProjectType.SelectedItem.Text.ToString();

                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".pdf");
                Response.ContentType = "application/pdf";
                StringReader sr = new StringReader(sw.ToString());
                iTextSharp.text.Document pdfDoc = new Document(PageSize.LEGAL, 1f, 1f, 1f, 0f);
                pdfDoc.SetPageSize(iTextSharp.text.PageSize.LEGAL.Rotate());



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
        catch (Exception)
        {

        }
    }
}

