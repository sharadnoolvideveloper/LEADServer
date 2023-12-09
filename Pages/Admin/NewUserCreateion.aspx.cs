using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Pages_Admin_NewUserCreateion : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BLobj.Admin_FillManagerList(rptManagersList, cook.Admin_Id());
            BLobj.FillStateMaster(ddlState);
            BLobj.Student_FillProgramme(ddlProgramme);
        }
    }

    protected void btnCreateUser_Click(object sender, EventArgs e)
    {
        try
        {
            int ManagerId = BLobj.Manager_CheckMangerIsExists(txtMobileNo.Text.ToString(), txtMailId.Text.ToString());


            string ManagerCode = BLobj.Admin_SaveManagerDetail(txtManagerName, txtMailId, txtMobileNo, txtAddress, ddlGender, ddlBloodGroup, ddlState, ManagerId);

            if (ManagerCode != "")
            {
                string clgName = "";
                string CollegeName = "";
                StringBuilder str = new StringBuilder();
               
                        foreach (RepeaterItem ri in rptCollegeList.Items)
                        {
                            CheckBox Chk = (CheckBox)ri.FindControl("ChkCollege");
                            if (Chk.Checked == true)
                            {
                                Label CollegeCode = (Label)ri.FindControl("lblCollegeId");
                             
                                clgName = BLobj.Admin_CheckCollegeExists(CollegeCode.Text.ToString());

                                if (clgName == "")
                                {
                                    BLobj.Admin_SaveManagerCollege(ManagerCode.ToString(), ddlTaluka.SelectedValue.ToString(), CollegeCode.Text.ToString());
                                }
                                else
                                {
                                    CollegeName = Chk.Text.ToString();
                                    str.Append("" + CollegeName.ToString().Trim() + "" + ",");
                                    str.AppendLine().Replace("\r\n", "");
                                }

                            }

                        }
                    
            

                string UnSavedColleges = str.ToString().TrimEnd(BLobj.trimChar);
               // lblUnsavedColleges.Text = UnSavedColleges.ToString();
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
            string ManagerId = (e.Item.FindControl("lblManagerid") as Label).Text;
        }
        catch (Exception)
        {

        }
    }
   
   

    

    protected void rptManagersList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataTable dt = new DataTable();   
                Label lblProjectCount = (e.Item.FindControl("lblProjectCount") as Label);
                Label lblManagerId = (e.Item.FindControl("lblManagerid") as Label);
               dt= BLobj.Admin_GetManagersCollegeCount(lblManagerId.Text.ToString());
                lblProjectCount.Text = dt.Rows[0].ItemArray[0].ToString();
                

            }
        }
        catch(Exception)
        {

        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_FillDistrict(ddlState.SelectedValue.ToString(), ddlDistrict);
            ddlCollege.Items.Clear();
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
            ddlCollege.Items.Clear();
        }
        catch (Exception)
        {

        }
    }

    protected void ddlTaluka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Student_fillCollegeByTaluka(ddlTaluka.SelectedValue.ToString(), ddlProgramme.SelectedItem.Text.ToString(), ddlCollege);
        }
        catch (Exception)
        {

        }
    }

    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
            BLobj.Student_fillCollegeByTaluka(ddlTaluka.SelectedValue.ToString(), ddlProgramme.SelectedItem.Text.ToString(), ddlCollege);
        }   
        catch (Exception)
        {

        }
    }

  
}

public class Colleges
{
    public string CollegeId { get; set; }
    public string CollegeName { get; set; }
   
}