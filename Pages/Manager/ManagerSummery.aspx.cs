using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_ManagerSummery : System.Web.UI.Page
{
    Reports rpt = new Reports();
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BLobj.FillAademicYear(ddlAcademicCode);
                ddlAcademicCode.SelectedIndex = 1;
                SummeryReport(ddlAcademicCode);


            }
        }
        catch(Exception)
        {

        }
       
    }

    protected void ddlAcademicCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SummeryReport(ddlAcademicCode);
        }
        catch(Exception)
        {

        }
    }
    protected void SummeryReport(DropDownList ddlAcademicCode)
    {
        string FromDate = "";
        string ToDate = "";
        BLobj.GetDatesFromAcademicYear(ddlAcademicCode.SelectedValue.ToString(), out FromDate, out ToDate);
        string ManagerId = cook.Manager_Id().ToString();
        DataTable dt = rpt.Manager_GetMaleFemaleCount(ManagerId.ToString(), ddlAcademicCode.SelectedValue.ToString());
        lblTotalCount.Text = dt.Rows[0].ItemArray[0].ToString();

        dt = rpt.Manager_GetProjectStatusWiseProjectCount(ManagerId.ToString(), ddlAcademicCode.SelectedValue.ToString());
        lblTotalProposed.Text = dt.Rows[0].ItemArray[0].ToString();
        lblTotalApproved.Text = dt.Rows[0].ItemArray[1].ToString();
        lblTotalCompleted.Text = dt.Rows[0].ItemArray[2].ToString();
        lblTotalRequestForModification.Text = dt.Rows[0].ItemArray[3].ToString();
        lblTotalRequestForCompletion.Text = dt.Rows[0].ItemArray[4].ToString();
        lblTotalRejected.Text = dt.Rows[0].ItemArray[5].ToString();
        lbltotalProjects.Text = dt.Rows[0].ItemArray[6].ToString();

        //lblMale.Text = dt.Rows[0].ItemArray[1].ToString();
        //lblFemale.Text = dt.Rows[0].ItemArray[2].ToString();
        dt = rpt.Manager_GetRequestedAndSanctionAmount(ManagerId.ToString(), ddlAcademicCode.SelectedValue.ToString());
        lblRequestedAmount.Text = dt.Rows[0].ItemArray[0].ToString();
        lblSanctionAmount.Text = dt.Rows[0].ItemArray[1].ToString();

        dt = rpt.Manager_GetReleaseAmount(cook.Manager_Id(), FromDate.ToString(), ToDate.ToString());
        lblReleaseAmount.Text = dt.Rows[0].ItemArray[0].ToString();
        if(lblReleaseAmount.Text=="")
        {
            lblBalanceAmount.Text = "";
        }
        else
        {
            lblBalanceAmount.Text = Convert.ToString(long.Parse(lblSanctionAmount.Text.ToString()) - long.Parse(lblReleaseAmount.Text.ToString()));
        }
        
    }
}