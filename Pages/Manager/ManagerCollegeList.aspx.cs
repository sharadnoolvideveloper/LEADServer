using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_ManagerCollegeList : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FromManagerName.Text = cook.ManagerName();
                BLobj.Admin_fillCollegeFromManagerWise(rptFromManagerCollegeList, cook.Manager_Id());
            }
        }
        catch(Exception)
        {

        }
       
    }



    protected void rptFromManagerCollegeList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string CollegeId = (e.Item.FindControl("lblCollegeId") as Label).Text;
            string CollegeName = (e.Item.FindControl("btnCollege") as LinkButton).Text;
            lblCollegeId.Text = CollegeId.ToString();
            DataTable dt= BLobj.Manager_GetStudentDetailOnClick(cook.Manager_Id(), CollegeId.ToString());
            rptStudentList.DataSource = dt;
            rptStudentList.DataBind();

            lblCollegeName.Text = CollegeName.ToString() +"(" + " " + dt.Rows.Count + " " + ")";
        }
        catch(Exception)
        {

        }
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {

            GridView gd = new GridView();
            gd.DataSource = BLobj.Manager_GetStudentDetailOnClick(cook.Manager_Id(), lblCollegeId.Text.ToString());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {


                string name = cook.ManagerName() + "_" + System.DateTime.Now.ToString() + "_" + lblCollegeName.Text.ToString();
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
}