<%@ WebHandler Language="C#" Class="StudentProfilePic" %>

using System;
using System.Web;
using System.IO;

public class StudentProfilePic : IHttpHandler {

    public void ProcessRequest(HttpContext context) {
        string theID = "";
        string FinalImgPath = "";
        if (context.Request.QueryString["leadid"] != null)
        {
            theID = context.Request.QueryString["leadid"].ToString();

        }

      

        else
        {
            throw new ArgumentException("No parameter specified");
        }

        String[] filePaths = Directory.GetFiles(context.Server.MapPath("~/ProfilePics/" + theID.ToString() + "/"));
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileName(filePath);
            FinalImgPath= "~/ProfilePics/" + theID.ToString() + "/" + fileName;

        }
        context.Response.WriteFile(FinalImgPath);
        try
        {
         //   string img = DisplayImage(FinalImgPath);

              

        }
        catch (Exception)
        {
            context.Response.ContentType = "CSS/NoImage.png";

        }
    }
    public string DisplayImage(string ImgPath)
    {




        try
        {

            return ImgPath.ToString();
        }
        catch
        {
            return null;
        }
        finally
        {

        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}