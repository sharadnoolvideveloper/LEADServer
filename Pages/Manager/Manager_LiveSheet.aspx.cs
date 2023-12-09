using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Pages_Manager_Manager_LiveSheet : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt = BLobj.getlivesheet();
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
           
        }
        catch(Exception)
        {

        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        dt = BLobj.getlivesheet();
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "myModal();", true);
        }
        catch(Exception)
        {

        }
    }
}