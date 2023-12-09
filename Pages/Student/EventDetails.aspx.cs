using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_EventDetails : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if(Request.QueryString["EventId"].ToString()!="")
            {
                string EventId = Request.QueryString["EventId"].ToString();
                BLobj.Student_GetParticularEventDetails(EventId.ToString(), lblFromDate, lblToDate, imgEvenPic, lblEventName, lblEventDescription, btnApplyNow);
            }
        }
    }
}