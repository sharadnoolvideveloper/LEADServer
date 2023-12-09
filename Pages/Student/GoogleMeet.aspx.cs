using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Pages_Student_GoogleMeet : System.Web.UI.Page
{
    string link = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            link = Request.QueryString["id"].ToString();
            TheURL = link.ToString();
            Page.DataBind();
        }
    }
    public string TheURL = "";

    public void GetURL(object Src, EventArgs Args)
    {
        TheURL = link.ToString();
        Page.DataBind();
        //if (!(URL.Text == "http://"))
        //{
           
        //}
    }
}