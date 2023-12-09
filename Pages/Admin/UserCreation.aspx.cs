using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_UserCreation : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BLobj.Admin_FillManagerList(rptManagersList);
            BLobj.FillStateMaster(ddlState);
        }
    }

    protected void btnCreateUser_Click(object sender, EventArgs e)
    {
        try
        {
            int ManagerId = BLobj.Manager_CheckMangerIsExists(txtMobileNo.Text.ToString(),txtMailId.Text.ToString());


            string ManagerCode=  BLobj.Admin_SaveManagerDetail(txtManagerName, txtMailId, txtMobileNo, txtAddress, ddlGender, ddlBloodGroup, ddlState, ManagerId);

           if(ManagerCode!="")
            {
                string clgName = "";
                string CollegeName = "";
                StringBuilder str = new StringBuilder();
                foreach (RepeaterItem ri1 in rptTaluka.Items)
                {
                    CheckBox ChkTaluk = (CheckBox)ri1.FindControl("ChkTaluk");
                    if (ChkTaluk.Checked == true)
                    {
                        
                        foreach (RepeaterItem ri in rptColleges.Items)
                        {
                 
                            CheckBox Chk = (CheckBox)ri.FindControl("ChkCollege");
                            if (Chk.Checked == true)
                            {
                                Label CollegeCode = (Label)ri.FindControl("lblCollegeCode");
                                Label TalukaCode = (Label)ri.FindControl("lblTalukaId");
                                clgName = BLobj.Admin_CheckCollegeExists(CollegeCode.Text.ToString());

                                if (clgName == "")
                                {
                                    BLobj.Admin_SaveManagerCollege(ManagerCode.ToString(), TalukaCode.Text.ToString(), CollegeCode.Text.ToString());
                                }
                                else
                                {
                                    CollegeName = Chk.Text.ToString();
                                    str.Append("" + CollegeName.ToString().Trim() + "" + ",");
                                    str.AppendLine().Replace("\r\n", "");
                                }

                           }

                        }
                    }
               }
                
                string UnSavedColleges= str.ToString().TrimEnd(BLobj.trimChar);
                lblUnsavedColleges.Text = UnSavedColleges.ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "UnSavedColleges();", true);
                //string msg = "Record Saved!!!";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);

                //Response.Redirect("UserCreation.aspx");


            }


            //------------Save ManagerTalukaCollegeCode-------------



        }
        catch (Exception)
        {

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

        }
    }
    protected void rptManagersList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

        }
    }
    protected void btnListDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillDistrict(rptDistrict, ddlState.SelectedValue.ToString());
            rptColleges.DataSource = "";
            rptColleges.DataBind();
            rptTaluka.DataSource = "";
            rptTaluka.DataBind();
        }
        catch (Exception)
        {

        }
    }
    protected void btnListThalukas_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder str = new StringBuilder();
            foreach (RepeaterItem ri in rptDistrict.Items)
            {


                CheckBox Chk = (CheckBox)ri.FindControl("ChkDistrict");
                if (Chk.Checked == true)
                {
                    Label lblDistrictCode = (Label)ri.FindControl("lblDistinctCode");
                    str.Append("" + lblDistrictCode.Text.Trim() + "" + ",");
                    str.AppendLine().Replace("\r\n", "");

                }

            }
            string DistrictCode = str.ToString().TrimEnd(BLobj.trimChar);

            BLobj.Admin_FillTaluka(rptTaluka, DistrictCode.ToString());
        }
        catch (Exception)
        {

        }
    }

    protected void btnListColleges_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder str = new StringBuilder();
            foreach (RepeaterItem ri in rptTaluka.Items)
            {


                CheckBox Chk = (CheckBox)ri.FindControl("ChkTaluk");
                if (Chk.Checked == true)
                {
                    Label TalukaCode = (Label)ri.FindControl("lblTalukaId");
                    str.Append("" + TalukaCode.Text.Trim() + "" + ",");
                    str.AppendLine().Replace("\r\n", "");

                }

            }
            string TalukaId = str.ToString().TrimEnd(BLobj.trimChar);

            BLobj.Admin_fillCollegeByTaluka(rptColleges,TalukaId);
        }
        catch (Exception)
        {

        }
    }
}