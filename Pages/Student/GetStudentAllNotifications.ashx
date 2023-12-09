<%@ WebHandler Language="C#" Class="Get_NotificationAll" %>

using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Universal.Standard;

public class Get_NotificationAll : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string callback = context.Request.QueryString["callback"];
        string json = "";
        json = this.list();
        if (!string.IsNullOrEmpty(callback))
        {
            json = string.Format("{0}({1});", callback, json);
        }
        context.Response.ContentType = "application/json";
        context.Response.Write(json);
    }

    private string list()
    {
        System.Collections.Generic.List<object> Notification = new List<object>();
        UniversalDL DLobj = new UniversalDL();
        vmCookies cook = new vmCookies();
        try
        {

            string gstrQrystr = "select Type as Notification_Type,Message,Date_format(createDate,'%d-%m-%Y %h:%i:%s') as createDate from notification_log Where Lead_Id='"+cook.LeadId()+"' and status=1 order by Date_format(createDate,'%Y-%m-%d %h:%i:%s') desc";
            DataTable dt = DLobj.GetDataTable(gstrQrystr);

            foreach(DataRow dr in dt.Rows)
            {
                Notification.Add(new
                {
                    Notification_Type = dr[0].ToString(),
                    Message = dr[1].ToString(),
                    createDate = dr[2].ToString(),
                });
            }

        }

        catch (Exception ex)
        {
            Notification.Add(new
            {
                error = "Error : " + ex.Message,

            });
        }

        return (new JavaScriptSerializer().Serialize(Notification));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    //public string Lead_id()
    //{
    //    try
    //    {
    //        HttpCookie Student_Lead_id = HttpContext.Current.Request.Cookies["Student_Lead_id"];
    //        return HttpContext.Current.Server.HtmlEncode(Student_Lead_id.Value);
    //    }
    //    catch (Exception)
    //    {
    //        return "";
    //    }
    //}
}