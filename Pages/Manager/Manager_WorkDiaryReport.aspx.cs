using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_Manager_WorkDiaryReport : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                    tabSubCatWise.Attributes.Add("class", "active");
                    SubCatWisetab.Attributes.Add("class", "tab-pane active");
                rptCategoryWiseList.DataSource = BLobj.CategoryWiseManagerList(cook.Manager_Id());
                rptCategoryWiseList.DataBind();

                lblCategoryWiseTotalSpentTime.Text = BLobj.GetSumofTimeSpent(txtFromDateCategory.Text, txtToDateCategory.Text, cook.Manager_Id());
            }
        }
        catch (Exception)
        {

        }
    }
   

    protected void btnSubCatWise_Click(object sender, EventArgs e)
    {
        try
        {
            tabSubCatWise.Attributes.Add("class", "active");
            SubCatWisetab.Attributes.Add("class", "tab-pane active");
            rptCategoryWiseList.DataSource = BLobj.CategoryWiseManagerList(cook.Manager_Id());
            rptCategoryWiseList.DataBind();
            lblCategoryWiseTotalSpentTime.Text = BLobj.GetSumofTimeSpent(txtFromDateCategory.Text, txtToDateCategory.Text, cook.Manager_Id());

        }
        catch (Exception)
        {

        }
    }

   
  

  


    protected void rptCategoryWiseList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string Sorting = "";
            Label lblCategoryId = (e.Item.FindControl("lblCategoryId") as Label);
            Label lblMain_CategoryName = (e.Item.FindControl("lblMain_CategoryName") as Label);
            lblSelectedCategoryId.Text = lblCategoryId.Text.ToString();
            lblCategoryName.Text = lblMain_CategoryName.Text.ToString();
            Sorting = ddlOrderByCategoryWise.SelectedValue.ToString() + " " + ddlOrderType.SelectedValue.ToString();

            rptTaskCategoryWiseDetailedList.DataSource = BLobj.ParticularCategoryWiseManagerWiseListing(cook.Manager_Id(), lblCategoryId.Text.ToString(), txtFromDateCategory.Text.ToString(), txtToDateCategory.Text.ToString(), Sorting.ToString());
            rptTaskCategoryWiseDetailedList.DataBind();
          //  lblCategoryWiseTotalSpentTime.Text = BLobj.GetSumofTimeSpent(txtFromDateCategory.Text, txtToDateCategory.Text, cook.Manager_Id());

        }
        catch (Exception)
        {

        }
    }


    protected void btnCategoryWiseManagerExcelDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string Sorting = "";
            GridView gd = new GridView();
            Sorting = ddlOrderByCategoryWise.SelectedValue.ToString() + " " + ddlOrderType.SelectedValue.ToString();
            gd.DataSource = BLobj.ParticularCategoryWiseManagerWiseListing(cook.Manager_Id(), lblSelectedCategoryId.Text.ToString(), txtFromDateCategory.Text.ToString(), txtToDateCategory.Text.ToString(), Sorting.ToString());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {


                string name = cook.ManagerName() + "_DiaryWork_" + System.DateTime.Now.ToString();
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

    protected void btnAllCategory_Click(object sender, EventArgs e)
    {
        try
        {
            string Sorting = ddlOrderByCategoryWise.SelectedValue.ToString() + " " + ddlOrderType.SelectedValue.ToString();
            lblSelectedCategoryId.Text = "All";
            rptTaskCategoryWiseDetailedList.DataSource = BLobj.AllCategoryWiseManagerWiseListing(cook.Manager_Id(), txtFromDateCategory.Text.ToString(), txtToDateCategory.Text.ToString(), Sorting.ToString());
            rptTaskCategoryWiseDetailedList.DataBind();
            lblCategoryWiseTotalSpentTime.Text = BLobj.GetSumofTimeSpent(txtFromDateCategory.Text, txtToDateCategory.Text, cook.Manager_Id());
        }
        catch (Exception)
        {

        }
    }

   

}