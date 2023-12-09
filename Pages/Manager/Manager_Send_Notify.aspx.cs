using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_Manager_Send_Notify : System.Web.UI.Page
{
    BL_Notify Notify = new BL_Notify();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Fill_Dashboard_Details();
                  Fill_Left_Summary_Count();
                mvMain.SetActiveView(vmDashboard);
            }
        }
        catch (Exception)
        {


        }
    }

    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMail.Visible = true;
            pnlNotification.Visible = false;
            pnlSMS.Visible = false;
            lblHeading.Text = "Send Mail";
           Fill_Left_Summary_Count();
            mvMain.SetActiveView(vmCompose);

        }
        catch (Exception)
        {

        }
    }

    protected void btnNotification_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMail.Visible = false;
            pnlNotification.Visible = true;
            pnlSMS.Visible = false;
            lblHeading.Text = "Send Notificaiton";
            Fill_Left_Summary_Count();

            mvMain.SetActiveView(vmCompose);


        }
        catch (Exception)
        {

        }
    }

    protected void btnSMS_Click(object sender, EventArgs e)
    {
        try
        {
            pnlMail.Visible = false;
            pnlNotification.Visible = false;
            pnlSMS.Visible = true;
            lblHeading.Text = "Send SMS";
             Fill_Left_Summary_Count();
            mvMain.SetActiveView(vmCompose);
        }
        catch (Exception)
        {

        }
    }
    protected void btnSent_Click(object sender, EventArgs e)
    {
        try
        {
            Fill_Left_Summary_Count();
            DataTable dt = new DataTable();
            dt = Notify.Get_Notify_Processing(int.Parse(cook.Manager_Id()), "Sent");
            grdSent.DataSource = dt;
            grdSent.DataBind();
            mvMain.SetActiveView(vmSent);
        }
        catch (Exception)
        {

        }
    }

    protected void btnFailed_Click(object sender, EventArgs e)
    {
        try
        {
            Fill_Left_Summary_Count();
            DataTable dt = new DataTable();
            dt = Notify.Get_Notify_Processing(int.Parse(cook.Manager_Id()), "Fail");
            grdFail.DataSource = dt;
            grdFail.DataBind();
            mvMain.SetActiveView(vmFailed);
        }
        catch (Exception)
        {

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

        }
    }

    protected void btnDashboard_Click(object sender, EventArgs e)
    {
        try
        {
            Fill_Dashboard_Details();
            mvMain.SetActiveView(vmDashboard);
        }
        catch (Exception)
        {

        }
    }
    public void Fill_Dashboard_Details()
    {
        try
        {
            DataTable dt = Notify.Get_Notify_Dashboard(int.Parse(cook.Manager_Id()));
            if (dt.Rows.Count > 0)
            {
                lblTotal_Sent.Text = dt.Rows[0]["L_Total_Sent"].ToString();
                lblTotal_Success.Text = dt.Rows[0]["L_Total_Success"].ToString();
                lblTotal_Failed.Text = dt.Rows[0]["L_Total_Failed"].ToString();
                lblTotal_Mails.Text = dt.Rows[0]["L_Total_Mails"].ToString();
                lblTotal_Notificaitons.Text = dt.Rows[0]["L_Total_Notificaitons"].ToString();
                lblTotal_SMS.Text = dt.Rows[0]["L_Total_SMS"].ToString();

            }
        }
        catch (Exception)
        {
        }
    }
    public void Fill_Left_Summary_Count()
    {
        try
        {
            DataTable dt = Notify.Get_Notify_Dashboard(int.Parse(cook.Manager_Id()));
            if (dt.Rows.Count > 0)
            {
                lblLeft_Total_Sent.Text = dt.Rows[0]["L_Total_Sent"].ToString();
                lblLeft_Total_Success.Text = dt.Rows[0]["L_Total_Success"].ToString();
                lblLeft_Total_Failed.Text = dt.Rows[0]["L_Total_Failed"].ToString();
                lblLeft_Total_Mail.Text = dt.Rows[0]["L_Total_Mails"].ToString();
                lblLeft_Total_Notificaiton.Text = dt.Rows[0]["L_Total_Notificaitons"].ToString();
                lblLeft_Total_SMS.Text = dt.Rows[0]["L_Total_SMS"].ToString();

            }
        }
        catch (Exception)
        {
        }
    }
    protected void btnSend_Mail_Click(object sender, EventArgs e)
    {
        try
        {
            string status = "";
          Sender_Notify Sending_Console = new Sender_Notify();
            if (txtEmail_Message.Text != "")
            {


                if (rdoEntrepreneurs.Checked == true)
                {
                    Sending_Console = Notify.Save_Notify(int.Parse(cook.Manager_Id()), "Mail", txtSubject.Text.ToString(), txtEmail_Message.Text, "English", 0, "", "", 0, "Entrepreneurs", grdEntrepreneur);
                }
                else
                {
                    Sending_Console = Notify.Save_Notify(int.Parse(cook.Manager_Id()), "Mail", txtSubject.Text.ToString(), txtEmail_Message.Text, "English", 0, "", "", 0, "Internal", grdEntrepreneur);
                }
                Fill_Dashboard_Details();
                if (Sending_Console.status == "true")
                {
                    string str_Path = Server.MapPath("\\Batches\\DFLEADNotify.exe");
                    ProcessStartInfo processInfo = new ProcessStartInfo(str_Path, Sending_Console.Notify_Slno.ToString() + " " + cook.Manager_MailId());
                    processInfo.UseShellExecute = false;
                    Process batchProcess = new Process();
                    batchProcess.StartInfo = processInfo;
                    batchProcess.Start();
                }

            }
            else
            {
                lblErroMessage.Text = "Enter Message";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ErrorMsg();", true);
                txtEmail_Message.Focus();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSend_Notification_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoEntrepreneurs.Checked == true)
            {

                lblErroMessage.Text = "Cant send notifications to Entrepreneurs";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ErrorMsg();", true);
                  Notify.Save_Notify(int.Parse(cook.Manager_Id()), "Notification",txtNotificaiton_Title.Text, txtNotificaiton_Message.Text, "English", 0, "", "", 0, "Entrepreneurs", grdEntrepreneur);
            }
            else
            {
                Sender_Notify Sending_Console = new Sender_Notify();
                Sending_Console = Notify.Save_Notify(int.Parse(cook.Manager_Id()), "Notification", txtNotificaiton_Title.Text, txtNotificaiton_Message.Text, "English", 0, "", "", 0, "Internal", grdEntrepreneur);
                Fill_Dashboard_Details();
                if (Sending_Console.status == "true")
                {
                    string str_Path = Server.MapPath("\\Batches\\DFLEADNotify.exe");
                    ProcessStartInfo processInfo = new ProcessStartInfo(str_Path, Sending_Console.Notify_Slno.ToString() + " " + cook.Manager_MailId());
                    processInfo.UseShellExecute = false;
                    Process batchProcess = new Process();
                    batchProcess.StartInfo = processInfo;
                    batchProcess.Start();
                }

            }

        }
        catch (Exception)
        {
        }
    }

    protected void btnSend_SMS_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_Confirm();", true);


        }
        catch (Exception)
        {

        }
    }
    protected void btnSMS_Yes_Click(object sender, EventArgs e)
    {
        try
        {
            string Language = "";
            string Message = "";
          Sender_Notify Sending_Console = new Sender_Notify();

       
                Language = "English";
                Message = txtSMS_English_Message.Text.ToString();
         

            if (Validation() == 1)
            {
                if (rdoEntrepreneurs.Checked == true)
                {
                        Sending_Console = Notify.Save_Notify(int.Parse(cook.Manager_Id()), "SMS", "", Message.ToString(), Language.ToString(), 0, "", "", 0, "Entrepreneurs", grdEntrepreneur);
                }
                else
                {
                     Sending_Console = Notify.Save_Notify(int.Parse(cook.Manager_Id()), "SMS", "", Message.ToString(), Language.ToString(), 0, "", "", 0, "Internal", grdEntrepreneur);
                }
                Fill_Dashboard_Details();
                 if (Sending_Console.status == "true")
                {
                    string str_Path = Server.MapPath("\\Batches\\DFLEADNotify.exe");
                    ProcessStartInfo processInfo = new ProcessStartInfo(str_Path, Sending_Console.Notify_Slno.ToString() + " " + cook.Manager_MailId());
                    processInfo.UseShellExecute = false;
                    Process batchProcess = new Process();
                    batchProcess.StartInfo = processInfo;
                    batchProcess.Start();
                }
            }


        }
        catch (Exception)
        {


        }
    }
    public int Validation()
    {
        int i = 0;
    
            if (txtSMS_English_Message.Text == "")
            {
                lblErroMessage.Text = "Enter Message";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "POP_ErrorMsg();", true);
                i = 0;
                txtSMS_English_Message.Focus();
            }
            else
            {
                i = 1;
            }
        return i;
    }
    protected void rdoEntrepreneurs_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //   BLobj.Fill_Sector_Listbox(lstSector);
            //  DataTable dt = new DataTable();
            // dt = BL.Get_Entrepreneur_List_For_Notificaiton(int.Parse(cook.UserId()), "");
            //     grdEntrepreneur.DataSource = dt;
            //       grdEntrepreneur.DataBind();
            //         this.grdEntrepreneur.Columns[3].Visible = true;
            //      this.grdEntrepreneur.Columns[7].Visible = true;
            //       ChkSelectAll.Checked = false;
        }
        catch (Exception)
        {
        }
    }

    protected void rdoInternal_Team_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //  DataTable dt = new DataTable();
            //   dt = BL.Get_Inner_Staff_List_For_Notificaiton(int.Parse(cook.UserId()));
            //    grdEntrepreneur.DataSource = dt;
            //    grdEntrepreneur.DataBind();

            //     this.grdEntrepreneur.Columns[3].Visible = false;
            //     this.grdEntrepreneur.Columns[7].Visible = false;
            ChkSelectAll.Checked = false;
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string Sector_Id = "";
            foreach (ListItem item in lstSector.Items)
            {
                if (item.Selected == true)
                {
                    Sector_Id = Sector_Id + "," + item.Value;

                }
            }

            //     DataTable dt = new DataTable();
            //   dt = BL.Get_Entrepreneur_List_For_Notificaiton(int.Parse(cook.UserId()), Sector_Id.ToString().TrimStart(','));
            //      grdEntrepreneur.DataSource = dt;
            //       grdEntrepreneur.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var count = 0;
            //      foreach (GridViewRow row in grdEntrepreneur.Rows)
            //      {

            //            var cb = row.FindControl("ChkCollect") as CheckBox;

            //          if (ChkSelectAll.Checked == true)
            //          {
            //            count++;
            //            cb.Checked = true;
            //         }
            //         else
            //         {
            //               lblSelected_Count.Text = "0";
            //               cb.Checked = false;
            //         }
        }
        //     lblSelected_Count.Text = count.ToString();
        //     }
        catch (Exception)
        {

        }
    }




    protected void btnProcessing_Click(object sender, EventArgs e)
    {
        try
        {
            //    Bind_Processing();
            mvMain.SetActiveView(vmProcessing);
        }
        catch (Exception)
        {

        }
    }
    public void Bind_Processing()
    {
        try
        {
            //DataTable dt = new DataTable();
            //dt = Notify.Get_Notify_Processing(int.Parse(cook.UserId()), "Top");
            //if (dt.Rows.Count > 0)
            //{
            //    lblNotify_Slno.Text = dt.Rows[0]["slno"].ToString();
            //    lblNotify_Type.Text = dt.Rows[0]["Notify_Type"].ToString();
            //    lblUser_Type.Text = dt.Rows[0]["user_type"].ToString();
            //    lblSubject.Text = dt.Rows[0]["subject"].ToString();
            //    lblMessage.Text = dt.Rows[0]["Message"].ToString();
            //    lblLanguage.Text = dt.Rows[0]["language"].ToString();
            //    lblProccess.Text = "Processing...";
            //    btnDelete.Visible = true;
            //}
            //else
            //{
            //    lblNotify_Slno.Text = "";
            //    lblNotify_Type.Text = "";
            //    lblUser_Type.Text = "";
            //    lblSubject.Text = "";
            //    lblMessage.Text = "";
            //    lblLanguage.Text = "";
            //    lblProccess.Text = "No Notify to process";
            //    btnDelete.Visible = false;
            //}


        }
        catch (Exception)
        {


        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //   int status = Notify.Delete_Processing_Notify(int.Parse(lblNotify_Slno.Text));
            //     if (status == 1)
            //      {
            //     Bind_Processing();
            //   }

        }
        catch (Exception)
        {


        }
    }
}