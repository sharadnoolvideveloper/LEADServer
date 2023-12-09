using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Developer_CollegeMaster : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                rptCollege.DataSource = BLobj.Developer_GetCollegeDetails();
                rptCollege.DataBind();
                BLobj.FillStateMaster(ddlState);
                BLobj.Student_FillProgramme(ddlCollegeType);
                BLobj.Developer_FillManagerddl(ddlManagerName);
            }
        }
        catch (Exception)
        {

        }

    }



    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_FillDistrict(ddlState.SelectedValue.ToString(), ddlDistrict);
            ddlTaluka.Items.Clear();
        }
        catch (Exception)
        {

        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_FillTaluka(ddlDistrict.SelectedValue.ToString(), ddlTaluka);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlState.SelectedIndex < 1)
            {
                lblError.Text = "Select State";
                ddlState.Focus();
            }
            else if (ddlDistrict.SelectedIndex < 1)
            {
                lblError.Text = "Select District";
                ddlDistrict.Focus();
            }
            else if (ddlTaluka.SelectedIndex < 1)
            {
                lblError.Text = "Select Taluka";
                ddlTaluka.Focus();
            }
            else if (ddlCollegeType.SelectedIndex < 1)
            {
                lblError.Text = "Select College Type";
                ddlCollegeType.Focus();
            }

            // added 
            else if (BLobj.checkCollegeExistence(txtCollegeName.Text) == true)
            {
                lblError.Text = "College name is already exists";
                txtCollegeName.Text = "";
                txtCollegeName.Focus();
            }
            else if (ddlManagerName.SelectedIndex < 1)
            {
                lblError.Text = "Select Manager";
                ddlManagerName.Focus();
            }

            // added checkCollegeExistence
            else if ((ddlState.SelectedIndex >= 1) && (ddlDistrict.SelectedIndex >= 1) && (ddlTaluka.SelectedIndex >= 1) && (ddlCollegeType.SelectedIndex >= 1) && (BLobj.checkCollegeExistence(txtCollegeName.Text) == false) && (ddlManagerName.SelectedIndex >= 1))
            {
                btnSave.Enabled = false;
                btnSave.Text = "Processing";
                BLobj.Developer_SaveCollegeDetails(ddlState.SelectedValue.ToString(), ddlDistrict.SelectedValue.ToString(), ddlTaluka.SelectedValue.ToString(), ddlCollegeType.SelectedItem.Text.ToString(), txtCollegeName.Text.ToString(), ddlManagerName.SelectedValue.ToString());
                ddlDistrict.Items.Clear();
                ddlTaluka.Items.Clear();
                BLobj.FillStateMaster(ddlState);
                BLobj.Student_FillProgramme(ddlCollegeType);
                BLobj.Developer_FillManagerddl(ddlManagerName);
                txtCollegeName.Text = "";

                rptCollege.DataSource = BLobj.Developer_GetCollegeDetails();
                rptCollege.DataBind();
                lblError.Text = "Saved";
                btnSave.Enabled = true;
                btnSave.Text = "Save";
            }


        }
        catch (Exception)
        {

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlTaluka.Items.Clear();
            BLobj.FillStateMaster(ddlState);
            BLobj.Student_FillProgramme(ddlCollegeType);
            BLobj.Developer_FillManagerddl(ddlManagerName);
            txtCollegeName.Text = "";

        }
        catch (Exception)
        {

        }
    }
}