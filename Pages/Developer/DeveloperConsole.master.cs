using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Developer_DeveloperConsole : System.Web.UI.MasterPage
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if(Session["Developer"]== null)
            {
                Response.Redirect("~/Default.aspx?SessionTimeOut=true");
            }
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Developer"] = null;
            Response.Redirect("~/Default.aspx?SessionTimeOut=true");
        }
        catch(Exception)
        {

        }
    }
}
