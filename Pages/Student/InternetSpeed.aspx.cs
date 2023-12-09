using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class Pages_Student_InternetSpeed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CheckSpeed(object sender, EventArgs e)
    {
        double[] speeds = new double[5];
        for (int i = 0; i < 5; i++)
        {
            int jQueryFileSize = 261; //Size of File in KB.
            WebClient client = new WebClient();
            DateTime startTime = DateTime.Now;
            client.DownloadFile("http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.js", Server.MapPath("~/jQuery.js"));
            DateTime endTime = DateTime.Now;
            speeds[i] = Math.Round((jQueryFileSize / (endTime - startTime).TotalSeconds));
        }
        lblDownloadSpeed.Text = string.Format("Download Speed: {0}KB/s", speeds.Average());
    }
}
