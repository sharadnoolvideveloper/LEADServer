using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_Rating : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Label1.Text = TextBox1.Text;

        //Label1.Text= Regex.Replace(TextBox2.Text, "'", "`").Trim();
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (Rating1.CurrentRating.ToString()=="0")
        {
            lbresult.Text = "its Zero";
        }
        else
        {
            lbresult.Text = Rating1.CurrentRating.ToString() +" "+"Rating is given";
        }
    }

    protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        lbresult.Text= Rating1.CurrentRating.ToString() + " " + "Rating is given";
    }
}