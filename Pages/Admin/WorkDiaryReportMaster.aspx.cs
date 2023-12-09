using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_WorkDiaryReportMaster : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BLobj.Admin_Fillprogramddl(ddlprogram, cook.Admin_Id());
                BLobj.Admin_Fillprogramddl(ddlprogramID, cook.Admin_Id());
                if (Session["Tab"] == null)
                {
                    tabManagerWise.Attributes.Add("class", "active");
                    ManagerWisetab.Attributes.Add("class", "tab-pane active");
                    tabSubCatWise.Attributes.Add("class", "");
                    SubCatWisetab.Attributes.Add("class", "hidden");
                    rptManagerWiseManagerList.DataSource = BLobj.ManagerWiseSpentTimeCount(ddlprogram.SelectedValue.ToString());
                    rptManagerWiseManagerList.DataBind();
                    BLobj.FillWorkDiaryMainCategory(ddlMainCategoryManagerWise);
                }
                else if (Session["Tab"].ToString() == "ManagerWise")
                {
                    tabManagerWise.Attributes.Add("class", "active");
                    ManagerWisetab.Attributes.Add("class", "tab-pane active");
                    tabSubCatWise.Attributes.Add("class", "");
                    SubCatWisetab.Attributes.Add("class", "hidden");
                    rptManagerWiseManagerList.DataSource = BLobj.ManagerWiseSpentTimeCount(ddlprogram.SelectedValue.ToString());
                    rptManagerWiseManagerList.DataBind();
                    BLobj.FillWorkDiaryMainCategory(ddlMainCategoryManagerWise);
                }
                else if (Session["Tab"].ToString() == "SubCategory")
                {
                    tabSubCatWise.Attributes.Add("class", "active");
                    SubCatWisetab.Attributes.Add("class", "tab-pane active");
                    tabManagerWise.Attributes.Add("class", "");
                    ManagerWisetab.Attributes.Add("class", "hidden");
                    BLobj.Admin_Fillprogramddl(ddlprogramID, cook.Admin_Id());
                    BLobj.Admin_FillManagerByprogram(ddlprogramID.SelectedValue.ToString(), ddlManagerName);
                    //  BLobj.Admin_FillManagerddl(ddlManagerName,cook.Admin_Id());

                }
                else
                {
                    tabManagerWise.Attributes.Add("class", "active");
                    ManagerWisetab.Attributes.Add("class", "tab-pane active");
                    tabSubCatWise.Attributes.Add("class", "");
                    SubCatWisetab.Attributes.Add("class", "hidden");
                    rptManagerWiseManagerList.DataSource = BLobj.ManagerWiseSpentTimeCount(ddlprogram.SelectedValue.ToString());
                    rptManagerWiseManagerList.DataBind();
                    BLobj.FillWorkDiaryMainCategory(ddlMainCategoryManagerWise);
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
            rptManagerWiseManagerList.DataSource = BLobj.ManagerWiseSpentTimeCount(ddlprogram.SelectedValue.ToString());
            rptManagerWiseManagerList.DataBind();
            BLobj.FillWorkDiaryMainCategory(ddlMainCategoryManagerWise);
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlprogramID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerByprogram(ddlprogramID.SelectedValue.ToString(), ddlManagerName);
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnManagerWise_Click(object sender, EventArgs e)
    {
        try
        {
            tabManagerWise.Attributes.Add("class", "active");
            ManagerWisetab.Attributes.Add("class", "tab-pane active");
            tabSubCatWise.Attributes.Add("class", "");
            SubCatWisetab.Attributes.Add("class", "hidden");
            Session["Tab"] = "ManagerWise";
            BLobj.FillWorkDiaryMainCategory(ddlMainCategoryManagerWise);
            rptManagerWiseManagerList.DataSource = BLobj.ManagerWiseSpentTimeCount(ddlprogram.SelectedValue.ToString());
            rptManagerWiseManagerList.DataBind();

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
            tabManagerWise.Attributes.Add("class", "");
            ManagerWisetab.Attributes.Add("class", "hidden");
            Session["Tab"] = "SubCategory";

            BLobj.Admin_FillManagerddl(ddlManagerName, cook.Admin_Id());
            rptCategoryWiseList.DataSource = BLobj.CategoryWiseManagerList(ddlManagerName.SelectedValue.ToString());
            rptCategoryWiseList.DataBind();

        }
        catch (Exception)
        {

        }
    }

    protected void rptManagerWiseManagerList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            string Sorting = "";

            Label ManagerId = (e.Item.FindControl("lblManagerId") as Label);
            Label ManagerName = (e.Item.FindControl("lblManagerName") as Label);
            LinkButton btnManagerList = (e.Item.FindControl("btnManagerList") as LinkButton);

            //btnManagerList.Attributes.Add("Class", "list-group-item list-group-item-info");

            if (btnManagerList.CssClass == "list-group-item list-group-item-info brandFont")
            {
                btnManagerList.CssClass = "list-group-item brandFont";
            }
            else
            {
                btnManagerList.CssClass = "list-group-item list-group-item-info brandFont";
            }

            lblManagerId.Text = ManagerId.Text.ToString();
            lblManagerName.Text = ManagerName.Text.ToString();
            Sorting = ddlSortFileds.SelectedValue.ToString() + " " + ddlAscdesc.SelectedValue.ToString();

            rptTaskListManagerWise.DataSource = BLobj.ParticularManagerAndMainCategoryList(ManagerId.Text.ToString(), ddlMainCategoryManagerWise.SelectedValue.ToString(), txtFromDateManagerwise.Text.ToString(), txtToDateManagerWise.Text.ToString(), Sorting.ToString());
            rptTaskListManagerWise.DataBind();


        }
        catch (Exception)
        {

        }
    }
    protected void rptManagerWiseManagerList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton btnManagerList = (e.Item.FindControl("btnManagerList") as LinkButton);
                if (btnManagerList.CssClass == "list-group-item list-group-item-info")
                {
                    btnManagerList.CssClass = "list-group-item";
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnManagerwiseExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string Sorting = "";
            GridView gd = new GridView();
            Sorting = ddlSortFileds.SelectedValue.ToString() + " " + ddlAscdesc.SelectedValue.ToString();
            gd.DataSource = BLobj.ParticularManagerAndMainCategoryList(lblManagerId.Text.ToString(), ddlMainCategoryManagerWise.SelectedValue.ToString(), txtFromDateManagerwise.Text.ToString(), txtToDateManagerWise.Text.ToString(), Sorting.ToString());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {


                string name = lblManagerName.Text + "_DiaryWork_" + System.DateTime.Now.ToString();
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

            rptTaskCategoryWiseDetailedList.DataSource = BLobj.ParticularCategoryWiseManagerWiseListing(ddlManagerName.SelectedValue.ToString(), lblCategoryId.Text.ToString(), txtFromDateCategory.Text.ToString(), txtToDateCategory.Text.ToString(), Sorting.ToString());
            rptTaskCategoryWiseDetailedList.DataBind();
            lblCategoryWiseTotalSpentTime.Text = BLobj.GetSumofTimeSpent(txtFromDateCategory.Text, txtToDateCategory.Text, ddlManagerName.SelectedValue.ToString());

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
            gd.DataSource = BLobj.ParticularCategoryWiseManagerWiseListing(ddlManagerName.SelectedValue.ToString(), lblSelectedCategoryId.Text.ToString(), txtFromDateCategory.Text.ToString(), txtToDateCategory.Text.ToString(), Sorting.ToString());
            gd.DataBind();
            if (gd.Rows.Count > 0)
            {


                string name = ddlManagerName.SelectedItem.Text.ToString() + "_DiaryWork_" + System.DateTime.Now.ToString();
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
            rptTaskCategoryWiseDetailedList.DataSource = BLobj.AllCategoryWiseManagerWiseListing(ddlManagerName.SelectedValue.ToString(), txtFromDateCategory.Text.ToString(), txtToDateCategory.Text.ToString(), Sorting.ToString());
            rptTaskCategoryWiseDetailedList.DataBind();
            lblCategoryWiseTotalSpentTime.Text = BLobj.GetSumofTimeSpent(txtFromDateCategory.Text, txtToDateCategory.Text, ddlManagerName.SelectedValue.ToString());
        }
        catch (Exception)
        {

        }
    }

    protected void ddlManagerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rptTaskCategoryWiseDetailedList.DataSource = null;
            rptTaskCategoryWiseDetailedList.DataBind();
            rptCategoryWiseList.DataSource = BLobj.CategoryWiseManagerList(ddlManagerName.SelectedValue.ToString());
            rptCategoryWiseList.DataBind();
            lblCategoryWiseTotalSpentTime.Text = BLobj.GetSumofTimeSpent(txtFromDateCategory.Text, txtToDateCategory.Text, ddlManagerName.SelectedValue.ToString());
        }
        catch (Exception)
        {

        }
    }




}