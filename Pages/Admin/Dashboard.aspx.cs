using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Dashboard : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BLobj.FillAademicYear(ddlAcademicYear);
                ddlAcademicYear.SelectedIndex = 1;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                //string msg = "Welcome";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                /* BLobj.Fillprogram(ddlprogramtype, cook.Admin_Id());
                 ddlAcademicYear.SelectedIndex = 1;
                 BLobj.Admin_FillManagerList(rptManagerList, cook.Admin_Id());
 */
                BLobj.Admin_Fillprogramddl(ddlprogramtype, cook.Admin_Id());
                BLobj.Admin_FillManagerList(rptManagerList, ddlprogramtype.SelectedValue.ToString());

                if (Request.QueryString["vwType"].ToString() != "")
                {
                    if (Session["ManagerId"].ToString() != "")
                    {
                        string vwType = Request.QueryString["vwType"].ToString();
                        DataTable dt = BLobj.Admin_GetProjectsListManagerWise(ddlAcademicYear.SelectedValue.ToString(), Session["ManagerId"].ToString(), vwType.ToString());

                        rptAllProjects.DataSource = dt;
                        rptAllProjects.DataBind();
                    }




                    //if (vwType == "Proposed")
                    //{

                    //}
                    //else if (vwType == "Approved")
                    //{

                    //}
                    //else if (vwType == "RequestForCompletion")
                    //{

                    //}
                    //else if (vwType == "RequestForModification")
                    //{

                    //}
                    //else if (vwType == "Completed")
                    //{

                    //}




                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerList(rptManagerList, ddlprogramtype.SelectedValue.ToString());
            if (Request.QueryString["vwType"].ToString() != "")
            {
                if (Session["ManagerId"].ToString() != "")
                {
                    string vwType = Request.QueryString["vwType"].ToString();
                    DataTable dt = BLobj.Admin_GetProjectsListManagerWise(ddlAcademicYear.SelectedValue.ToString(), Session["ManagerId"].ToString(), vwType.ToString());

                    rptAllProjects.DataSource = dt;
                    rptAllProjects.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void rptManagerList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string FromDate = "", ToDate = "";
            BLobj.GetDatesFromAcademicYear(ddlAllProjectAcademicYear.SelectedValue.ToString(), out FromDate, out ToDate);
            Label ManagerId = (e.Item.FindControl("lblManagerId") as Label);
            Session["ManagerId"] = ManagerId.Text.ToString();
            DataTable dt = BLobj.GetDashBoardProjectCount(ddlAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerId.Text.ToString());
            foreach (DataRow dr in dt.Rows)
            {

                lblProposedProjects.Text = dr[1].ToString();
                lblApproved.Text = dr[2].ToString();
                //lblRequestForModificationCount.Text = dr[4].ToString();
                //lblRequestForCompletionCount.Text = dr[5].ToString();
                //lblCompletedProjectsCount.Text = dr[6].ToString();

            }
        }
        catch (Exception)
        {

        }
    }

    /* protected void rptprgrmList_ItemCommand(object source, RepeaterCommandEventArgs e)
     {
         try
         {
             string FromDate = "", ToDate = "";
             BLobj.Getprogramlist(ddlprogramtype.SelectedValue.ToString(), out FromDate, out ToDate);
             Label ManagerId = (e.Item.FindControl("lblManagerId") as Label);
             Session["ManagerId"] = ManagerId.Text.ToString();
             DataTable dt = BLobj.GetDashBoardProjectCount(ddlAcademicYear.SelectedValue.ToString(), FromDate.ToString(), ToDate.ToString(), ManagerId.Text.ToString());
             foreach (DataRow dr in dt.Rows)
             {

                 lblProposedProjects.Text = dr[1].ToString();
                 lblApproved.Text = dr[2].ToString();
                 //lblRequestForModificationCount.Text = dr[4].ToString();
                 //lblRequestForCompletionCount.Text = dr[5].ToString();
                 //lblCompletedProjectsCount.Text = dr[6].ToString();

             }
         }
         catch (Exception)
         {

         }
     }*/
    protected void rptAllProjects_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //Label ProjectStatus = (e.Item.FindControl("lblProjectStatus") as Label);
                LinkButton btnPayee = (e.Item.FindControl("btnPayee") as LinkButton);
                Label lblPDId = (e.Item.FindControl("lblPDId") as Label);
                Label lblLeadId = (e.Item.FindControl("lblLeadId") as Label);
                Label lblDispersedAmount = (e.Item.FindControl("lblDisperseAmt") as Label);
                Label lblBalanceAmount = (e.Item.FindControl("lblBalance") as Label);
                Label lblSacntionAmount = (e.Item.FindControl("lblSacntionAmount") as Label);

                BLobj.Manager_GetFundingDetailWithDataBound(lblPDId.Text.ToString(), lblLeadId.Text.ToString(), btnPayee, Session["ManagerId"].ToString(), lblBalanceAmount, lblDispersedAmount);

                //lblDispersedAmount.Text = BLobj.Student_GetProjectTotalFunded(lblPDId.Text, lblLeadId.Text.ToString());
                //lblBalanceAmount.Text = Convert.ToString(int.Parse(lblSacntionAmount.Text) - int.Parse(lblDispersedAmount.Text));


            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnProposedList_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

        }
    }
}