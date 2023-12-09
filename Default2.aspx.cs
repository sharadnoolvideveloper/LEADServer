using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
		try
		{
            // Define the APK file path and URL
 string apkFilePath = Server.MapPath("~/Reports/leadcampus.apk");
            string apkFileName = "leadcampus.apk";
            string apkFileUrl = Server.MapPath("~/CSS/leadcampus.apk");

            //"http://ec2-18-138-98-10.ap-southeast-1.compute.amazonaws.com:9090/Reports/leadcampus.apk";


            // Download the APK file
            WebClient client = new WebClient();
            client.DownloadFile(apkFileUrl, apkFilePath);

            // Install the downloaded APK
            InstallAPK(apkFilePath, apkFileName);
        }
		catch (Exception ex)
		{

		}
    }

    private void InstallAPK(string apkFilePath,string FileName)
    {
        // Execute an intent to install the APK
        Response.ContentType = "application/vnd.android.package-archive";
        Response.AddHeader("content-disposition", "attachment; filename="+FileName);
        Response.TransmitFile(apkFilePath);
        Response.End();
    }
}