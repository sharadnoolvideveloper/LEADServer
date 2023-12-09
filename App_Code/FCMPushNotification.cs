using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for FCMPushNotification
/// </summary>
public class FCMPushNotification
{
   
    public FCMPushNotification()
    {
        // TODO: Add constructor logic here
    }

    public bool Successful
    {
        get;
        set;
    }

    public string Response
    {
        get;
        set;
    }
    public Exception Error
    {
        get;
        set;
    }

    public static FCMPushNotification SendNotification(string _title, string _message, string _topic)
    {
        var applicationID = "AAAAwUOt-C4:APA91bEB7JuYHc08frjm6tL91qiMlenyPfa65lZ7N9B_4KZOMilkXoBateyVhlIaeuXdHnhLbpovuoVrTyeWst2aYN8y1PZ2dz6MRC3OfJ5Wd9EyujLv4GTla4EphZ84I3jN-0bZR1fj52XmOkE2J5qLoMCAWnvNoA";
        var SENDER_ID = "830064162862";
        FCMPushNotification result = new FCMPushNotification();
        try
        {
            result.Successful = true;
            result.Error = null;
            var requestUri = "https://fcm.googleapis.com/fcm/send";

            WebRequest webRequest = WebRequest.Create(requestUri);
            webRequest.Method = "POST";
            webRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
            webRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            webRequest.ContentType = "application/json";

            var msg = new
            {
                to = _topic,
                data = new
                {
                    data = new
                    {
                        title = _title,
                        is_background = false,
                        message = _message,
                        image = "",
                        payload = "null",
                        timestamp = DateTime.Now.ToLongDateString()
                    }
                }
            };


            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(msg);

            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            webRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = webResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                            result.Response = sResponseFromServer;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            result.Successful = false;
            result.Response = null;
            result.Error = ex;
        }
        return result;
    }
    public static string AndroidPush(string deviceId, string message, string title, string imageurls)
    {
        FCMPushNotification result = new FCMPushNotification();
        var serverApiKey = "AAAAwUOt-C4:APA91bEB7JuYHc08frjm6tL91qiMlenyPfa65lZ7N9B_4KZOMilkXoBateyVhlIaeuXdHnhLbpovuoVrTyeWst2aYN8y1PZ2dz6MRC3OfJ5Wd9EyujLv4GTla4EphZ84I3jN-0bZR1fj52XmOkE2J5qLoMCAWnvNoA";
        var senderId = "830064162862";
        try
        {
            //for (int i = 0; i < deviceId.Length; i++)
            //{               
                result.Successful = false;
                result.Error = null;

                var value = message;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverApiKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));

                // string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.title=" + title + "&data.message="
              + value + "&data.time=" + System.DateTime.Now.ToString() + "&data.image=" + imageurls + "&registration_id=" + deviceId.ToString() + "";
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
        }
        catch (Exception ex)
        {
            result.Successful = false;
            result.Response = null;
            result.Error = ex;
        }

        return result.ToString();
    }
    public static FCMPushNotification SendFCMNotification(string _topic, string _message,string _title,string _ImagePath)
    {
       var applicationID = "AAAAwUOt-C4:APA91bEB7JuYHc08frjm6tL91qiMlenyPfa65lZ7N9B_4KZOMilkXoBateyVhlIaeuXdHnhLbpovuoVrTyeWst2aYN8y1PZ2dz6MRC3OfJ5Wd9EyujLv4GTla4EphZ84I3jN-0bZR1fj52XmOkE2J5qLoMCAWnvNoA";
       var senderId = "830064162862";
        FCMPushNotification result = new FCMPushNotification();
        try
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";

            var data = new
            {
                to = _topic,
                notification = new
                {
                    body = _message,
                    title = _title
                  //  image= _ImagePath,
                  //  timestamp = DateTime.Now.ToLongDateString()
                },
                data = new
                {
                    title = _title,
                    body = _message
                }
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                           result.Response = sResponseFromServer;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            result.Response = ex.Message;
        }

        return result;
    }

}

public class GCMNotification
{
    LeadBL BLobj = new LeadBL();
    public static string AndroidPush(string deviceId, string message, string title, string imageurls)
    {
        string regId = deviceId.Trim();
        // var applicationID = "AAAAQtv5FiE:APA91bHN4gRq2ZU5wx91T-VrAixvAnFmxeDVOTtMoS1pQFGqRDV5j-7-NowRSNeC_lJxUbeYGqAL1WVvzbArL9Tbgj97b-C2jzc3HOHIoldw8mT8UqNGKzBwNZmjcwgDuJwwK4cOR9MNtwzIQe9j_5XOatGd596ICQ";
        var applicationID = "AAAAwUOt-C4:APA91bEB7JuYHc08frjm6tL91qiMlenyPfa65lZ7N9B_4KZOMilkXoBateyVhlIaeuXdHnhLbpovuoVrTyeWst2aYN8y1PZ2dz6MRC3OfJ5Wd9EyujLv4GTla4EphZ84I3jN-0bZR1fj52XmOkE2J5qLoMCAWnvNoA";
        //var SENDER_ID = "287158375969";
        var SENDER_ID = "830064162862";
        var value = message; //message text box
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";
        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
        //Data post to server
        string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.title=" + title + "&data.message="
       + value + "&data.time=" + System.DateTime.Now.ToString() + "&data.image=" + imageurls + "&registration_id=" + regId + "";
        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;
        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
       
        WebResponse tResponse = tRequest.GetResponse();
        dataStream = tResponse.GetResponseStream();
        StreamReader tReader = new StreamReader(dataStream);
        String sResponseFromServer = tReader.ReadToEnd();
        tReader.Close();
        dataStream.Close();
        tResponse.Close();
        
        return sResponseFromServer;
    }

    public static void SendMailGeneral(string Message, string Subject, string Email)
    {
        try
        {


            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("leadmis@dfmail.org");
                mailMessage.Subject = Subject;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(Email));
                //mailMessage.CC.Add(new MailAddress(lblManagerEmailId.Text.ToString()));
                mailMessage.Bcc.Add(new MailAddress("sharad.noolvi@dfmail.org"));
                mailMessage.Bcc.Add(new MailAddress("leadmis@dfmail.org"));


                string senderID = "leadmis@dfmail.org";
                const string senderPassword = "leadcampusadmin";

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };

                smtp.Send(mailMessage);

            }

        }
        catch (Exception)
        {

        }

    }
}