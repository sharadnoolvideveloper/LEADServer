<%@ WebHandler Language="C#" Class="PDFView" %>

using System;
using System.Web;

public class PDFView : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string FileName = context.Request.QueryString["FileName"].ToString();
      //  byte[] bytes =(byte[])FileName.ToString();

        context.Response.Buffer = true;
        context.Response.Charset = "";
        if (context.Request.QueryString["download"] == "1")
        {
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        }
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "application/pdf";
      //  context.Response.BinaryWrite(bytes);
        context.Response.Flush();
        context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}