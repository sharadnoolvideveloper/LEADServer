<%@ WebHandler Language="C#" Class="Get_ManagerNotificationAll" %>

using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Universal.Standard;

public class Get_ManagerNotificationAll : IHttpHandler
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

            string gstrQrystr = "select Type as Notification_Type,Message,Date_format(createDate,'%d-%m-%Y %h:%i:%s') as createDate from notification_log Where ManagerCode="+cook.Manager_Id()+" and type not in('Notification','Mail','SMS')  order by Date_format(createDate,'%Y-%m-%d %h:%i:%s') desc Limit 100";
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
               return (new JavaScriptSerializer().Serialize(Notification));
        }
        catch (Exception ex)
        {
            Notification.Add(new
            {
                error = "Error : " + ex.Message,

            });
                  return (new JavaScriptSerializer().Serialize(Notification));
        }

      //  return (new JavaScriptSerializer().Serialize(Notification));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}