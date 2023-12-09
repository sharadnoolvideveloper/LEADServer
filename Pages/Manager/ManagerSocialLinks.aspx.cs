using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_ManagerSocialLinks : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            try
            {
                DataTable dt = BLobj.Manager_GetSocialURL(cook.Manager_Id());
                txtFacebook.Text = dt.Rows[0].ItemArray[0].ToString();
                a_Facebook.HRef = dt.Rows[0].ItemArray[0].ToString();
                txtTwitter.Text = dt.Rows[0].ItemArray[1].ToString();
                a_Twitter.HRef = dt.Rows[0].ItemArray[1].ToString();
                txtInstaGram.Text = dt.Rows[0].ItemArray[2].ToString();
                a_Instagram.HRef = dt.Rows[0].ItemArray[2].ToString();
                txtWhatsApp.Text = dt.Rows[0].ItemArray[3].ToString();
                a_WhatsApp.HRef = "https://web.whatsapp.com/send?phone=91" + dt.Rows[0].ItemArray[3].ToString();
            }
            catch(Exception)
            {

            }
           
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BLobj.Manager_UpdateSocialURL(cook.Manager_Id(), txtFacebook.Text.ToString(), txtTwitter.Text.ToString(), txtInstaGram.Text.ToString(),txtWhatsApp.Text.ToString());
            DataTable dt = BLobj.Manager_GetSocialURL(cook.Manager_Id());
            txtFacebook.Text = dt.Rows[0].ItemArray[0].ToString();
            a_Facebook.HRef = dt.Rows[0].ItemArray[0].ToString();
            txtTwitter.Text = dt.Rows[0].ItemArray[1].ToString();
            a_Twitter.HRef = dt.Rows[0].ItemArray[1].ToString();
            txtInstaGram.Text = dt.Rows[0].ItemArray[2].ToString();
            a_Instagram.HRef = dt.Rows[0].ItemArray[2].ToString();
            txtInstaGram.Text = dt.Rows[0].ItemArray[2].ToString();
            a_Instagram.HRef = dt.Rows[0].ItemArray[2].ToString();
            txtWhatsApp.Text= dt.Rows[0].ItemArray[3].ToString();
            a_WhatsApp.HRef= "https://web.whatsapp.com/send?phone=91" + dt.Rows[0].ItemArray[3].ToString();
            string msg = "Updated Successfully !!!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        }
        catch(Exception)
        {

        }
    }
}