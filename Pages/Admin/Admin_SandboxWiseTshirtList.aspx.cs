using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_SandboxWiseTshirtList : System.Web.UI.Page
{
    Reports rpt = new Reports();
    LeadBL BLobj = new LeadBL();

    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BLobj.Admin_Fillprogramddl(ddlprogramId, cook.Admin_Id());
            BLobj.FillAademicYear(ddlAcademicYear);
            ddlAcademicYear.SelectedIndex = 1;
            rptSandboxList.DataSource = rpt.GetSandboxDetailsTshirt();
            rptSandboxList.DataBind();

        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rptSandboxList.DataSource = rpt.GetSandboxDetailsTshirt();
            rptSandboxList.DataBind();
        }
        catch (Exception ex)
        {
        }
    }


    protected void rptSandboxList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSandbox = (e.Item.FindControl("SandboxName") as Label);
                Label lblTopAllTshirt = (e.Item.FindControl("lblTopAllTshirt") as Label);
                Label lblAllotedTopS = (e.Item.FindControl("lblAllotedTopS") as Label);
                Label lblAllotedTopM = (e.Item.FindControl("lblAllotedTopM") as Label);
                Label lblAllotedTopL = (e.Item.FindControl("lblAllotedTopL") as Label);
                Label lblAllotedTopXL = (e.Item.FindControl("lblAllotedTopXL") as Label);
                Label lblAllotedTopXXL = (e.Item.FindControl("lblAllotedTopXXL") as Label);
                Label lblUsedTopS = (e.Item.FindControl("lblUsedTopS") as Label);
                Label lblUsedTopM = (e.Item.FindControl("lblUsedTopM") as Label);
                Label lblUsedTopL = (e.Item.FindControl("lblUsedTopL") as Label);
                Label lblUsedTopXL = (e.Item.FindControl("lblUsedTopXL") as Label);
                Label lblUsedTopXXL = (e.Item.FindControl("lblUsedTopXXL") as Label);
                Label lblTopTotalUsed = (e.Item.FindControl("lblTopTotalUsed") as Label);
                Label lblTopTotalBalance = (e.Item.FindControl("lblTopTotalBalance") as Label);
                Label lblTopRequests = (e.Item.FindControl("lblTopRequests") as Label);

                Repeater rptManagerList = e.Item.FindControl("rptManagerList") as Repeater;

                rptManagerList.DataSource = rpt.FillManagerSandboxWiseTshirt(lblSandbox.Text.ToString(), cook.Admin_Id(), ddlprogramId.SelectedValue.ToString());
                rptManagerList.DataBind();

                DataTable dt = rpt.Admin_GetManagerWiseTshirtStockCount(ddlAcademicYear.SelectedValue.ToString(), lblSandbox.Text.ToString());


                lblAllotedTopS.Text = dt.Rows[0].ItemArray[3].ToString();
                lblUsedTopS.Text = dt.Rows[0].ItemArray[4].ToString();

                lblAllotedTopM.Text = dt.Rows[0].ItemArray[5].ToString();
                lblUsedTopM.Text = dt.Rows[0].ItemArray[6].ToString();

                lblAllotedTopL.Text = dt.Rows[0].ItemArray[7].ToString();
                lblUsedTopL.Text = dt.Rows[0].ItemArray[8].ToString();

                lblAllotedTopXL.Text = dt.Rows[0].ItemArray[9].ToString();
                lblUsedTopXL.Text = dt.Rows[0].ItemArray[10].ToString();

                lblAllotedTopXXL.Text = dt.Rows[0].ItemArray[11].ToString();
                lblUsedTopXXL.Text = dt.Rows[0].ItemArray[12].ToString();
                lblTopRequests.Text = dt.Rows[0].ItemArray[13].ToString();

                lblTopAllTshirt.Text = Convert.ToString(int.Parse(lblAllotedTopS.Text) + int.Parse(lblAllotedTopM.Text) + int.Parse(lblAllotedTopL.Text) + int.Parse(lblAllotedTopXL.Text) + int.Parse(lblAllotedTopXXL.Text));
                lblTopTotalUsed.Text = Convert.ToString(int.Parse(lblUsedTopS.Text) + int.Parse(lblUsedTopM.Text) + int.Parse(lblUsedTopL.Text) + int.Parse(lblUsedTopXL.Text) + int.Parse(lblUsedTopXXL.Text));
                lblTopTotalBalance.Text = Convert.ToString(int.Parse(lblTopAllTshirt.Text) - int.Parse(lblTopTotalUsed.Text));






            }
        }
        catch (Exception)
        {

        }
    }

    protected void rptManagerList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblManagerId = (e.Item.FindControl("lblManagerId") as Label);

                Label lblAllTshirt = (e.Item.FindControl("lblAllTshirt") as Label);
                Label lblAllotedS = (e.Item.FindControl("lblAllotedS") as Label);
                Label lblAllotedM = (e.Item.FindControl("lblAllotedM") as Label);
                Label lblAllotedL = (e.Item.FindControl("lblAllotedL") as Label);
                Label lblAllotedXL = (e.Item.FindControl("lblAllotedXL") as Label);
                Label lblAllotedXXL = (e.Item.FindControl("lblAllotedXXL") as Label);
                Label lblUsedS = (e.Item.FindControl("lblUsedS") as Label);
                Label lblUsedM = (e.Item.FindControl("lblUsedM") as Label);
                Label lblUsedL = (e.Item.FindControl("lblUsedL") as Label);
                Label lblUsedXL = (e.Item.FindControl("lblUsedXL") as Label);
                Label lblUsedXXL = (e.Item.FindControl("lblUsedXXL") as Label);
                Label lblUsedTotal = (e.Item.FindControl("lblUsedTotal") as Label);
                Label lblBalance = (e.Item.FindControl("lblBalance") as Label);
                Label lblRequestCount = (e.Item.FindControl("lblRequestCount") as Label);

                DataTable dt = rpt.Admin_GetManagerWiseTshirtStockDetails(ddlAcademicYear.SelectedValue.ToString(), lblManagerId.Text);


                lblAllotedS.Text = dt.Rows[0].ItemArray[3].ToString();
                lblUsedS.Text = dt.Rows[0].ItemArray[4].ToString();

                lblAllotedM.Text = dt.Rows[0].ItemArray[5].ToString();
                lblUsedM.Text = dt.Rows[0].ItemArray[6].ToString();

                lblAllotedL.Text = dt.Rows[0].ItemArray[7].ToString();
                lblUsedL.Text = dt.Rows[0].ItemArray[8].ToString();

                lblAllotedXL.Text = dt.Rows[0].ItemArray[9].ToString();
                lblUsedXL.Text = dt.Rows[0].ItemArray[10].ToString();

                lblAllotedXXL.Text = dt.Rows[0].ItemArray[11].ToString();
                lblUsedXXL.Text = dt.Rows[0].ItemArray[12].ToString();
                lblAllTshirt.Text = Convert.ToString(int.Parse(lblAllotedS.Text) + int.Parse(lblAllotedM.Text) + int.Parse(lblAllotedL.Text) + int.Parse(lblAllotedXL.Text) + int.Parse(lblAllotedXXL.Text));
                lblUsedTotal.Text = Convert.ToString(int.Parse(lblUsedS.Text) + int.Parse(lblUsedM.Text) + int.Parse(lblUsedL.Text) + int.Parse(lblUsedXL.Text) + int.Parse(lblUsedXXL.Text));
                lblBalance.Text = Convert.ToString(int.Parse(lblAllTshirt.Text) - int.Parse(lblUsedTotal.Text));
                lblRequestCount.Text = dt.Rows[0].ItemArray[13].ToString();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rptSandboxList.DataSource = rpt.GetSandboxDetailsTshirt();
            rptSandboxList.DataBind();
        }
        catch (Exception)
        {

        }
    }

    protected void btnExcelCount_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gd = new GridView();
            gd.DataSource = rpt.Admin_GetSandboxWiseTshirtCount(ddlAcademicYear.SelectedValue.ToString());
            gd.DataBind();
            string name = "";
            if (gd.Rows.Count > 0)
            {
                if (ddlAcademicYear.SelectedValue == "[All]")
                {
                    name = "All Year Sandbox Wise T-Shirt Count";
                }
                else
                {
                    name = ddlAcademicYear.SelectedItem.Text.ToString() + "  Year Sandbox Wise T-Shirt Count";
                }
                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gd.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnExcelList_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gd = new GridView();
            gd.DataSource = rpt.Admin_GetSandboxWiseTshirtList(ddlAcademicYear.SelectedValue.ToString());
            gd.DataBind();
            string name = "";
            if (gd.Rows.Count > 0)
            {
                if (ddlAcademicYear.SelectedValue == "[All]")
                {
                    name = "All Year Sandbox Wise T-Shirt Count";
                }
                else
                {
                    name = ddlAcademicYear.SelectedItem.Text.ToString() + "  Year Sandbox Wise T-Shirt Count";
                }
                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment;filename=" + name + ".xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gd.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception)
        {

        }
    }
}