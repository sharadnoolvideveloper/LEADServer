using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_TshirtAllotment : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                    BLobj.FillAademicYearSelect(ddlAcademicYear);
                    ddlAcademicYear.SelectedIndex = 1;
               
            }
        }
        catch(Exception)
        {

        }
       
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAcademicYear.SelectedValue != "[Select]")
            {
                rptTshirtAllotment.DataSource = BLobj.Admin_GET_Tshirt_Allotment(ddlAcademicYear.SelectedValue.ToString());
                rptTshirtAllotment.DataBind();
            }
            else
            {
                rptTshirtAllotment.DataSource = null;
                rptTshirtAllotment.DataBind();
                string msg = "Select Academic Year";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg.ToString() + "')", true);
            }
        }
        catch(Exception ex)
        {
           
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_SaveManager_TshirtAllotment(rptTshirtAllotment, ddlAcademicYear.SelectedValue.ToString(), cook.Admin_Id());
            string msg = "T-Shirt Allotment is done Successfully";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg.ToString() + "')", true);
        }
        catch(Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + ex.Message.ToString() + "')", true);
        }
    }
}