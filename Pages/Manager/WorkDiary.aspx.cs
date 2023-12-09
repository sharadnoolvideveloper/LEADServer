using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_WorkDiary : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies Cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if(Cook.Manager_Id() != null)
                {
                    BLobj.FillWorkDiaryMainCategory(ddlMainCategory);
                    BLobj.FillCollegeNameForWorkDiary(ddlCollege, Cook.Manager_Id());
                    DataTable dt = BLobj.GetWorkDiary(Cook.Manager_Id(), true);
                    dlWorkList.DataSource = dt;
                    dlWorkList.DataBind();
                }
                else
                {
                    Response.Redirect("~/Default.aspx?SessionTimeOut=True");
                }
                
               
            }
           
        }
        catch (Exception)
        {

        }

    }

    protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.FillWorkDiarySubCategory(ddlSubCategory, ddlMainCategory.SelectedValue.ToString());
        }
        catch (Exception)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            vmWorkDiary vmDiary = new vmWorkDiary();
            string msg = "";
            if (ddlMainCategory.SelectedIndex==0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DiaryWork();", true);
                msg = "Select Main Category";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                

            }
            else if(ddlSubCategory.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DiaryWork();", true);
                msg = "Select Sub Category";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        
            else if (ddlProgress.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DiaryWork();", true);
                msg = "Select Task Progress";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (ddlHH.SelectedIndex == 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DiaryWork();", true);
                msg = "Select HH";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else if (ddlMM.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DiaryWork();", true);
                msg = "Select MM";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
            else
            {
                vmDiary.MainCategory = ddlMainCategory.SelectedValue.ToString();
                vmDiary.SubCategory = ddlSubCategory.SelectedValue.ToString();
                if(ddlCollege.SelectedIndex==0)
                {
                    vmDiary.CollegeId = "461";
                }
                else
                {
                    vmDiary.CollegeId = ddlCollege.SelectedValue.ToString();
                }
                vmDiary.Descritpion = Regex.Replace(txtDescription.Text.ToString(), "'", "`").Trim();
                vmDiary.TotalParticipants = txtParticipations.Text.ToString();
                vmDiary.Progress = ddlProgress.SelectedValue.ToString();
                vmDiary.SpentTime = ddlHH.SelectedValue.ToString() + ":" + ddlMM.SelectedValue.ToString();
                vmDiary.Remarks = Regex.Replace(txtRemark.Text.ToString(), "'", "`").Trim();
                vmDiary.ManagerId = Cook.Manager_Id();
                BLobj.InsertWorkDiary(vmDiary);
                ddlMainCategory.SelectedIndex = 0;
                ddlSubCategory.SelectedIndex = 0;
                ddlCollege.SelectedIndex = 0;
                txtDescription.Text = "";
                txtParticipations.Text = "0";
                ddlProgress.SelectedIndex = 0;
                ddlHH.SelectedIndex = 0;
                ddlMM.SelectedIndex = 0;
                txtRemark.Text = "";
                DataTable dt = BLobj.GetWorkDiary(Cook.Manager_Id(),true);
                dlWorkList.DataSource = dt;
                dlWorkList.DataBind();
                msg = " Task Saved!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);  
            }
        }
        catch(Exception)
        {

        }
    }

    protected void dlWorkList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (Cook.Manager_Id() != "")
            {

               
                string Slno = "";
               
                string code = e.CommandArgument.ToString();
                string[] itemlist = code.Split('_');
                int i;
                for (i = 0; i <= 2; i++)
                {
                    if (i == 0)
                    {
                        Slno = itemlist[0].ToString();
                    }
                   
                }
                lblSlno.Text = Slno.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "POP_Confirm();", true);

            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
           
            BLobj.DeleteTask(Cook.Manager_Id(), lblSlno.Text.ToString());
            DataTable dt = BLobj.GetWorkDiary(Cook.Manager_Id(),true);
            dlWorkList.DataSource = dt;
            dlWorkList.DataBind();
            string msg = " Task Deleted!!!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }
        catch (Exception)
        {

        }
    }

    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = BLobj.GetWorkDiary(Cook.Manager_Id(), false);
            dlWorkList.DataSource = dt;
            dlWorkList.DataBind();
        }
        catch(Exception)
        {

        }
    }
}