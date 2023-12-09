using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_AdminProjectDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                string vwType = Request.QueryString["vwType"].ToString();
                if (vwType == "AllProjects")
                {
                    MainView.SetActiveView(vwAllProjects);
                    //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "ErrorModal();", true);
                    //string msg = "Welcome";
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                }
                else if (vwType == "Proposed")
                {
                    MainView.SetActiveView(vwProposedProjects);
                }
                else if (vwType == "Approved")
                {
                    MainView.SetActiveView(vwApprovedProjects);
                }
                else if (vwType == "UnApproved")
                {
                    MainView.SetActiveView(vwUnApprovedProjects);
                }
                else if (vwType == "Completed")
                {
                    MainView.SetActiveView(vwCompletedProjects);
                }


            }
        }
        catch (Exception)
        {

        }
    }
}