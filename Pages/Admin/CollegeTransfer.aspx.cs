using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_CollegeTransfer : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BLobj.Admin_FillManagerList(rptManagersList, cook.Admin_Id());
            BLobj.Admin_Fillprogramddl(ddlprogram, cook.Admin_Id());
            BLobj.Admin_FillManagerList(rptManagersList, ddlprogram.SelectedValue.ToString());
        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLobj.Admin_FillManagerList(rptManagersList, ddlprogram.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
        }
    }

    protected void rptManagersList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            lblFromManagerId.Text = (e.Item.FindControl("lblManagerid") as Label).Text;

            FromManagerName.Text = (e.Item.FindControl("lblManagerName") as Label).Text;
            BLobj.Admin_fillCollegeFromManagerWise(rptFromManagerCollegeList, lblFromManagerId.Text.ToString());
            rptToManager.DataSource = "";
            rptToManager.DataBind();

        }
        catch (Exception)
        {

        }
    }





    protected void rptManagersList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataTable dt = new DataTable();
                Label lblProjectCount = (e.Item.FindControl("lblProjectCount") as Label);
                Label lblManagerId = (e.Item.FindControl("lblManagerid") as Label);
                dt = BLobj.Admin_GetManagersCollegeCount(lblManagerId.Text.ToString());
                lblProjectCount.Text = dt.Rows[0].ItemArray[0].ToString();

            }
        }
        catch (Exception)
        {

        }
    }

    List<CollegesList> dataList = new List<CollegesList>();
    protected void btnListCollegeToManager_Click(object sender, EventArgs e)
    {
        try
        {

            rptToManager.DataSource = "";
            rptToManager.DataBind();
            //-- add all existing values to a list
            foreach (RepeaterItem item in rptFromManagerCollegeList.Items)
            {
                CheckBox chk = (item.FindControl("ChkCollege") as CheckBox);
                if (chk.Checked == true)
                {
                    dataList.Add(new CollegesList()
                    {
                        CollegeIdToManager = (item.FindControl("lblCollegeId") as Label).Text,
                        TalukIdToManager = (item.FindControl("lblTalukaId") as Label).Text,
                        CollegeNameToManager = (item.FindControl("ChkCollege") as CheckBox).Text,
                        TalukaNameToManager = (item.FindControl("lblTalukaName") as Label).Text
                    });
                }
            }
            //-- add a blank row to list to show a new row added
            //   dataList.Add(new CollegesList());

            //-- bind repeater
            rptToManager.DataSource = dataList;
            rptToManager.DataBind();
            if (rptToManager.Items.Count > 0)
            {
                BLobj.Admin_FillManagerDropDownForCollegeTransfer(lblFromManagerId.Text.ToString(), ddlManagersList);
            }
            else
            {
                string msg = "Select College To Transfer";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnUpdateCollege_Click(object sender, EventArgs e)
    {
        try
        {

            StringBuilder str = new StringBuilder();

            foreach (RepeaterItem ri in rptToManager.Items)
            {
                Label CollegeCode = (Label)ri.FindControl("lblCollegeId");

                str.Append("" + CollegeCode.Text.ToString().Trim() + "" + ",");
                str.AppendLine().Replace("\r\n", "");

            }
            string clgId = str.ToString().TrimEnd(BLobj.trimChar);

            BLobj.Admin_TransfterCollegesToNewManager(clgId.ToString(), lblFromManagerId.Text.ToString(), ddlManagersList.SelectedValue.ToString());
            BLobj.Admin_FillManagerList(rptManagersList, cook.Admin_Id());
            SendMail(clgId);
            string msg = "Updated Succes";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch (Exception)
        {

        }
    }
    public void SendMail(string CollegeId)
    {

        string Subject = "College is Transfered From - " + lblFromManagerId.Text.ToString() + " Manager  " + "To - " + ddlManagersList.SelectedValue.ToString();
        string Message = "College Id - " + CollegeId.ToString();

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
            string Email = "sharad.noolvi@dfmail.org,raghavendra.chikkalkar@dfmail.org";
            MailMessage message = new MailMessage(senderID, Email, Subject, Message);
            smtp.Send(message);

        }
        catch (Exception)
        {

        }


    }
}

public class CollegesList
{
    public string CollegeIdToManager { get; set; }
    public string CollegeNameToManager { get; set; }

    public string TalukIdToManager { get; set; }

    public string TalukaNameToManager { get; set; }

}