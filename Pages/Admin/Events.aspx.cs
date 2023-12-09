using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Events : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BLobj.FillStateMaster(ddlState);
                BLobj.GetEventDetailsRepeater(rptEvent);
                lblEventButtonClick.Text = "New";
                imgEventImg.ImageUrl = "";
            }
        }
        catch(Exception)
        {

        }
       
    }
    
    protected void rptEvent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string btnPressType = "";
            
            string code = e.CommandArgument.ToString();
            string[] itemlist = code.Split('_');
            int i;
            for (i = 0; i <= 0; i++)
            {
               
               if (i == 0)
                {
                    btnPressType = itemlist[0].ToString();

                }


            }
            if(btnPressType=="Notification")
            {
                string EventId="", EventName="", Image_path = "";
                
                Label lblEventId = (e.Item.FindControl("lblEventId") as Label);
                DataTable dt = BLobj.Admin_GetEventForNotification(lblEventId.Text.ToString());
                EventId = dt.Rows[0].ItemArray[0].ToString();
                EventName = dt.Rows[0].ItemArray[1].ToString();
                Image_path = dt.Rows[0].ItemArray[2].ToString();
                dt = BLobj.Admin_GetDeviceIdForSendingEventAndStoriesNotification();
                //string[] DeviceId = new string[dt.Rows.Count];
                //for (int J = 0; J < dt.Rows.Count; J++)
                //{
                //    DeviceId[J] = dt.Rows[J].ItemArray[0].ToString() + ",";
                //}
                foreach (DataRow dr in dt.Rows)
                {
                    string Title = "LEADCampus had just added Event - " + " " + EventName.ToString();
                    FCMPushNotification.AndroidPush(dr[0].ToString(), "LEADCampus had just added Event - " + " " + EventName.ToString(), "Events", "http://mis.leadcampus.org" + Image_path);
                    // GCMNotification.AndroidPush(dr[0].ToString(), "LEADCampus had just added Event - " + " " + EventName.ToString(), "Events", "http://mis.leadcampus.org" + Image_path);
                }
               
            }
            else if(btnPressType=="Edit")
            {
                Label lblEventId = (e.Item.FindControl("lblEventId") as Label);
                lblEditEventId.Text = lblEventId.Text.ToString();
                lblEventButtonClick.Text = "Edit";
                DataTable dt = BLobj.Admin_GetEventsDetailsForEdit(lblEventId.Text.ToString());
                if (dt.Rows.Count > 0)
                {
                    txtEventName.Text = dt.Rows[0].ItemArray[0].ToString();
                    txtEventFromDate.Text = dt.Rows[0].ItemArray[1].ToString();
                    txtEventToDate.Text = dt.Rows[0].ItemArray[2].ToString();
                    txtEventDescription.Text = dt.Rows[0].ItemArray[3].ToString();
                    txtEventURL.Text = dt.Rows[0].ItemArray[4].ToString();
                    txtEventApplyURL.Text = dt.Rows[0].ItemArray[5].ToString();
                    ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt.Rows[0].ItemArray[6].ToString()));
                    if (dt.Rows[0].ItemArray[7].ToString() == "0")
                    {
                        ChkAllState.Checked = false;
                    }
                    else
                    {
                        ChkAllState.Checked = true;
                    }
                    imgEventImg.ImageUrl = dt.Rows[0].ItemArray[8].ToString();

                }
            }
            //else if(btnPressType== "StatusEdit")
            //{
            //    Label lblEventId = (e.Item.FindControl("lblEventId") as Label);
            //    Label lblStatus = (e.Item.FindControl("lblStatus") as Label);
            //    BLobj.Admin_UpdateEventStatus(lblEventId.Text,lblStatus.Text);
            //    BLobj.GetEventDetailsRepeater(rptEvent);
            //    lblEventButtonClick.Text = "New";
            //}
            else if (btnPressType == "Delete")
            {
                Label lblEventId = (e.Item.FindControl("lblEventId") as Label);
               
                BLobj.Admin_DeleteEvent(lblEventId.Text);
                BLobj.GetEventDetailsRepeater(rptEvent);
                lblEventButtonClick.Text = "New";
            }

        }
        catch(Exception)
        {

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtEventName.Text = "";
            txtEventDescription.Text = "";
            txtEventFromDate.Text = "";
            txtEventToDate.Text = "";
            txtEventURL.Text = "";
            txtEventApplyURL.Text = "";
            ChkAllState.Checked = false;
            BLobj.FillStateMaster(ddlState);
            lblEventButtonClick.Text = "New";
            imgEventImg.ImageUrl = "";
        }
        catch(Exception)
        {

        }
    }

    protected void btnSaveEvent_Click(object sender, EventArgs e)
    {
        try
        {
            string CreatedBy = "1";
            int forAll = 0;
            string StateCode = "";
            string EventId = "";
            string EventName = "";
            string Image_path = "";
            if (ChkAllState.Checked==true)
            {
                forAll = 1;
            }
            else
            {
                forAll = 0;
            }
            if(ddlState.SelectedItem.ToString()=="[Select]")
            {
                StateCode = "0";
            }
            else
            {
                StateCode = ddlState.SelectedValue.ToString();
            }
            string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string FileExtenssion = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            FilePath = FilePath + "_"+ Guid.NewGuid().ToString() + FileExtenssion.ToString();
            BLobj.SaveEventDetails(txtEventName, txtEventDescription, txtEventFromDate, txtEventToDate, StateCode.ToString(), txtEventURL, txtEventApplyURL, CreatedBy.ToString(),forAll,FileUpload1, FilePath,FileExtenssion,lblEventButtonClick.Text,lblEditEventId.Text);
            BLobj.FillStateMaster(ddlState);
            BLobj.GetEventDetailsRepeater(rptEvent);
            if (ChkSendNotification.Checked == true)
            {
                DataTable dt = BLobj.Admin_GetLatestTopEventForNotification();
                EventId = dt.Rows[0].ItemArray[0].ToString();
                EventName = dt.Rows[0].ItemArray[1].ToString();
                Image_path = dt.Rows[0].ItemArray[2].ToString();
                dt = BLobj.Admin_GetDeviceIdForSendingEventAndStoriesNotification();
                foreach (DataRow dr in dt.Rows)
                {
                    FCMPushNotification.AndroidPush(dr[0].ToString(), "LEADCampus had just added Event - "+" "+ EventName.ToString(), "Events", "http://mis.leadcampus.org"+Image_path);
                }
            }

            string msg = "Event Saved Successfully!!!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
            txtEventName.Text = "";
            txtEventDescription.Text = "";
            txtEventFromDate.Text = "";
            txtEventToDate.Text = "";
            txtEventURL.Text = "";
            txtEventApplyURL.Text = "";
            ChkAllState.Checked = false;
            imgEventImg.ImageUrl = "";



            lblEventButtonClick.Text = "New";
        }
        catch (Exception)
        {

        }
    }

    protected void rptEvent_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                LinkButton Status = (e.Item.FindControl("btnEditStatus") as LinkButton);
                if (Status.Text == "0")
                {
                    Status.Text = "<span class='fa fa-thumbs-down'></span>";
                    Status.Attributes.Add("class", "btn btn-danger btn-floating");
                }
                else
                {
                    Status.Text = "<span class='fa fa-thumbs-up'></span>";
                    Status.Attributes.Add("class", "btn btn-primary btn-floating");
                }



            }
        }
           
        catch (Exception)
        {

        }
    }



   
}