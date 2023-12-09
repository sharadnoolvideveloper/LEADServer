using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

    public class SendEmail
    {
      
        string Default = "ajay@dfmail.org";
        string Sub = "Regarding Profile Completion";
        public void SendMailThroughGMail(string LeadId, string Name, string Email)
        {
            

            string Subject = "Regarding Profile Completion";
            string Message = "Dear " + Name + ",\n";
            Message = Message + "We noticed that your profile is incomplete. Please update your complete profile on mis.leadcampus.org by logging in using your Mobile No. and  LEAD ID :" + LeadId + "\n";
            Message = Message + "Thank you";
            string senderID = "leadmis@dfmail.org";
            const string senderPassword = "leadcampusadmin";
            try
            {
                if (Email != "")
                {
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                        Timeout = 30000,
                    };
                    MailMessage message = new MailMessage(senderID, Email, Subject, Message);
                    smtp.Send(message);
                }
                else
                {
                   
                }

            }
            catch (Exception)
            {
               
            }

        }
        public void SendScheduledMailThroughGMail(string HTML, string Email)
        {


        }
        public void SendMailThroughTecoBytes(string LeadId, string Name, string Email)
        {
           
            string Message = "Dear " + Name + ",\n";
            Message = Message + "We noticed that your profile is incomplete. Please update your complete profile on LEADMIS by logging in using your mobile No. and  LEAD Id :" + LeadId + "\n";
            var msg = new MailMessage
            {
                Subject = Sub,
                Body = Message,
                IsBodyHtml = true,
                From = new MailAddress("lead@leadcampus1.org")
            };

            var mailbox = new SmtpClient();
            mailbox.Host = "server1.tecobytes1.com";
            mailbox.Port = 25;
            mailbox.Send(msg);
            msg.Dispose();

        }

        //internal void ScheduledEmailAlerts()
        //{
        //    DateTime StartDate, EndDate;
        //    StartDate = System.DateTime.Now.ToString("") + " 12:00:00 AM";
        //    EndDate = System.DateTime.Now.ToString("") + " 12:00:00 AM";
        //    GetAllRegisteredListByPM();
        //}
        public void SendMailException(string Method, string NewException, string PageName, string lead_id, string mobile_no)
        {
            string ExceptionEmail = ConfigurationManager.AppSettings["ExceptionEmail"];
            if (!NewException.Contains("Thread was being aborted") && !NewException.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server") && !NewException.Contains("Object reference not set to an instance of an object.") && !NewException.Contains("The parameter 'address' cannot be an empty string."))
            {
                if (ExceptionEmail.ToUpper() == "TRUE")
                {
                    
                    string Subject = "Regarding Exception Caught";
                    string Message = "From Method - " + Method + ",\n";
                    Message = Message + "Lead ID - " + lead_id + ",\n";
                    Message = Message + "Mobile NO. - " + mobile_no + ",\n";
                    Message = Message + " From Page Name :" + PageName + ",\n";
                    Message = Message + " Exception is : " + NewException + ",\n";
                    Message = Message + "Process it soon";
                    string senderID = "leadmis@dfmail.org";
                    const string senderPassword = "leadcampusadmin"; 
                    try
                    {
                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                            Timeout = 30000,
                        };
                        string Email = "sharad.noolvi@dfmail.org";  //,sunil.tech@dfmail.org
                        MailMessage message = new MailMessage(senderID, Email, Subject, Message);
                        smtp.Send(message);

                    }
                    catch (Exception)
                    {

                    }

                }
                else
                {
                   
                    string Subject = "Regarding Exception Caught";
                    string Message = "From Method - " + Method + ",\n";
                    Message = Message + "Lead ID - " + lead_id + ",\n";
                    Message = Message + "Mobile NO. - " + mobile_no + ",\n";
                    Message = Message + " From Page Name :" + PageName + ",\n";
                    Message = Message + " Exception is : " + NewException + ",\n";
                    Message = Message + "Process it soon";
                string senderID = "leadmis @dfmail.org";
                const string senderPassword = "leadcampusadmin";
                    try
                    {
                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                            Timeout = 30000,
                        };
                        string Email = "sharad.noolvi@dfmail.org";
                        MailMessage message = new MailMessage(senderID, Email, Subject, Message);
                        smtp.Send(message);

                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
