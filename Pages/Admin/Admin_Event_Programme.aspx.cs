using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_Event_Programme : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                dlState.DataSource = BLobj.GetStateMaster();
                dlState.DataBind();
               
                BLobj.GetEventProgrammeRepeater(rptEvent);
                lblEventButtonClick.Text = "New";
                imgEventImg.ImageUrl = "";
                txtEventName.Focus();
            }
        }
        catch (Exception)
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
            if (btnPressType == "Notification")
            {
                string EventId = "", EventName = "", Image_path = "";
                Label lblEventId = (e.Item.FindControl("lblEventId") as Label);
                DataTable dt = BLobj.Admin_GetEventForNotification(lblEventId.Text.ToString());
                EventId = dt.Rows[0].ItemArray[0].ToString();
                EventName = dt.Rows[0].ItemArray[1].ToString();
                Image_path = dt.Rows[0].ItemArray[2].ToString();
                dt = BLobj.Admin_GetDeviceIdForSendingEventAndStoriesNotification();
                foreach (DataRow dr in dt.Rows)
                {
                    GCMNotification.AndroidPush(dr[0].ToString(), "LEADCampus had just added Event - " + " " + EventName.ToString(), "Events", "http://mis.leadcampus.org" + Image_path);
                }
            }
            else if (btnPressType == "Edit")
            {
                string StateCode = "";
                Label lblEventId = (e.Item.FindControl("lblEventId") as Label);
                lblEditEventId.Text = lblEventId.Text.ToString();
                lblEventButtonClick.Text = "Edit";
                DataTable dt = BLobj.Admin_GetEvents_Programme_DetailsForEdit(lblEventId.Text.ToString());
                if (dt.Rows.Count > 0)
                {
                    txtEventName.Text = dt.Rows[0].ItemArray[0].ToString();
                    txtEventFromDate.Text = dt.Rows[0].ItemArray[1].ToString();
                    txtEventToDate.Text = dt.Rows[0].ItemArray[2].ToString();
                    txtEventDescription.Text = dt.Rows[0].ItemArray[3].ToString();
                    txtEventFees.Text = dt.Rows[0].ItemArray[4].ToString();
                    txtFirstPaymentFees.Text = dt.Rows[0].ItemArray[5].ToString();                   
                    imgEventImg.ImageUrl = dt.Rows[0].ItemArray[6].ToString();
                    StateCode = dt.Rows[0].ItemArray[7].ToString();
                    List<string> statecode = StateCode.Split(',').ToList();

                    for (int m = 0; m < statecode.Count; m++)
                    {
                        foreach (DataListItem dli in dlState.Items)
                        {
                            string id = ((Label)dli.FindControl("lblEventId")).Text;
                            if(id==statecode[m])
                            {
                                CheckBox Chk = ((CheckBox)dli.FindControl("ChkState"));
                                Chk.Checked = true;
                            }                            
                        }
                    }
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

                BLobj.Admin_UpdateEvent_Programme(lblEventId.Text);
                BLobj.GetEventProgrammeRepeater(rptEvent);
                lblEventButtonClick.Text = "New";
            }

        }
        catch (Exception)
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
            txtEventFees.Text = "";
            txtFirstPaymentFees.Text = "";

            dlState.DataSource= BLobj.GetStateMaster();
            dlState.DataBind();
            lblEventButtonClick.Text = "New";
            imgEventImg.ImageUrl = "";
            txtEventName.Focus();
        }
        catch (Exception)
        {

        }
    }

    protected void btnSaveEvent_Click(object sender, EventArgs e)
    {
        try
        {
            string CreatedBy = cook.Admin_Id();
           
            string StateCode = "";
            string EventId = "";
            string EventName = "";
            string Image_path = "";
            StringBuilder str = new StringBuilder();
            DateTime FromDate =DateTime.Parse(txtEventFromDate.Text.ToString());
            DateTime ToDate = DateTime.Parse(txtEventToDate.Text.ToString());
            if (FromDate <= ToDate)
            {


                foreach (DataListItem dli in dlState.Items)
                {
                    EventId = ((Label)dli.FindControl("lblEventId")).Text;
                    CheckBox Chk = ((CheckBox)dli.FindControl("ChkState"));
                    if (Chk.Checked == true)
                    {
                        str.Append(EventId.ToString() + ",");
                    }
                }
                StateCode = str.ToString().TrimEnd(',');

                if (StateCode != "")
                {
                    string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
                    string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string FileExtenssion = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                    FilePath = FilePath + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                    BLobj.SaveEvent_Programme_Details(txtEventName, txtEventDescription, txtEventFromDate, txtEventToDate, StateCode.ToString(), txtEventFees, txtFirstPaymentFees, CreatedBy.ToString(), FileUpload1, FilePath, FileExtenssion, lblEventButtonClick.Text, lblEditEventId.Text);
                    dlState.DataSource = BLobj.GetStateMaster();
                    dlState.DataBind();
                    BLobj.GetEventProgrammeRepeater(rptEvent);
                    if (ChkSendNotification.Checked == true)
                    {
                        DataTable dt = BLobj.Admin_GetLatestTopEventForNotification();
                        EventId = dt.Rows[0].ItemArray[0].ToString();
                        EventName = dt.Rows[0].ItemArray[1].ToString();
                        Image_path = dt.Rows[0].ItemArray[2].ToString();
                        dt = BLobj.Admin_GetDeviceIdForSendingEventAndStoriesNotification();
                        foreach (DataRow dr in dt.Rows)
                        {
                            GCMNotification.AndroidPush(dr[0].ToString(), "LEADCampus had just added Event - " + " " + EventName.ToString(), "Events", "http://mis.leadcampus.org" + Image_path);
                        }
                    }

                    string msg = "Event Saved Successfully!!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                    txtEventName.Text = "";
                    txtEventDescription.Text = "";
                    txtEventFromDate.Text = "";
                    txtEventToDate.Text = "";
                    txtEventFees.Text = "";
                    txtFirstPaymentFees.Text = "";
                    imgEventImg.ImageUrl = "";
                    lblEventButtonClick.Text = "New";
                }
                else
                {
                    string msg = "Select Atleast one state";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg + "')", true);
                }
            }
            else
            {
                string msg = "Kindly Select Correct ToDate";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "warning('" + msg + "')", true);
                txtEventToDate.Focus();
            }
            
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