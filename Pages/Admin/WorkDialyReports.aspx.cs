using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_WorkDialyReports : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(cook.Admin_Id()!="")
            {
                if (!Page.IsPostBack)
                {
                    BLobj.Admin_Fillprogramddl(ddlprogram, cook.Admin_Id());
                    rptManagers.DataSource = BLobj.ManagerWiseSpentTimeCount(ddlprogram.SelectedValue.ToString());
                    rptManagers.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx?Session=Out");
            }
           
        }
        catch(Exception)
        {

        }
    }

    protected void rptManagers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string ManagerId = (e.Item.FindControl("hfManagerId") as HiddenField).Value;
                Repeater rptTask = e.Item.FindControl("rptTask") as Repeater;
                rptTask.DataSource = BLobj.ParticularManagerWiseSubCategoryCount(ManagerId.ToString());
                rptTask.DataBind();
            }
        }
        catch(Exception)
        {

        }

    }
}