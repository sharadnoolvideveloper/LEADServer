using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Manager_ManagerProfileUpdate : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            try
            {
                DataTable dt = BLobj.Manager_GetManagerDetails(cook.Manager_Id());
                txtName.Text = dt.Rows[0].ItemArray[1].ToString();
                txtMobileNo.Text = dt.Rows[0].ItemArray[2].ToString();
                txtEmailId.Text = dt.Rows[0].ItemArray[3].ToString();
                if (dt.Rows[0].ItemArray[4].ToString() == "M")
                {
                    rdoMale.Checked = true;
                }
                else
                {
                    rdoFemale.Checked = true;
                }
                ddlBloodGroup.SelectedIndex = ddlBloodGroup.Items.IndexOf(ddlBloodGroup.Items.FindByValue(dt.Rows[0].ItemArray[5].ToString()));
                txtAddress.Text = dt.Rows[0].ItemArray[6].ToString();
                txtFacebook.Text = dt.Rows[0].ItemArray[7].ToString();
                txtTwitter.Text = dt.Rows[0].ItemArray[8].ToString();
                txtInstaGram.Text = dt.Rows[0].ItemArray[9].ToString();
                txtWhatsApp.Text = dt.Rows[0].ItemArray[10].ToString();
                ImgManagerProfilePic.ImageUrl = dt.Rows[0].ItemArray[11].ToString();
                ddlManagerRecordSet.SelectedIndex = ddlManagerRecordSet.Items.IndexOf(ddlManagerRecordSet.Items.FindByValue(dt.Rows[0].ItemArray[12].ToString()));
            }
            catch(Exception)
            {

            }
           
        }
    }
    protected void btnSaveProfileImage_Click(object sender, EventArgs e)
    {
        try
        {
            if (ProfilePic.PostedFile != null)
            {
                string ManagerId = cook.Manager_Id();
               
                string FilePath = ConfigurationManager.AppSettings["ProfilePicPath"].ToString();

                string FileExtenssion = System.IO.Path.GetExtension(ProfilePic.PostedFile.FileName);
                string FileName = ManagerId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    BLobj.Manager_UpdateOnlyManagerProfileImage(ManagerId.ToString(), FilePath + FileName);
                    ProfilePic.SaveAs(Server.MapPath(FilePath + FileName));
                    Response.Redirect("DashBoard.aspx?vwType=DashBoard");
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnChangePasswordEvent_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            
            if (ProfilePic.PostedFile != null)
            {
                string LeadId = cook.LeadId();

                string FilePath = ConfigurationManager.AppSettings["ProfilePicPath"].ToString();

                string FileExtenssion = System.IO.Path.GetExtension(ProfilePic.PostedFile.FileName);
                string FileName = LeadId.ToString() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    BLobj.Manager_UpdateOnlyManagerProfileImage(cook.Manager_Id(), FilePath + FileName);
                    ProfilePic.SaveAs(Server.MapPath(FilePath + FileName));
                    msg = "Profile Pic Updated!!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
                }
            }
            //string Result = BLobj.Manager_UpdateProfileDetails(cook.Manager_Id());
            
            
                msg = "Updated Successfully..";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
         



        }
        catch (Exception)
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Gender = "";
            if(rdoMale.Checked==true)
            {
                Gender = "M";
            }
            else
            {
                Gender = "F";
            }
            BLobj.Manager_UpdateProfileDetails(cook.Manager_Id(), txtName.Text.ToString(), txtMobileNo.Text.ToString(), txtEmailId.Text.ToString(), Gender.ToString(), ddlBloodGroup.SelectedValue.ToString(), txtAddress.Text.ToString(), txtFacebook.Text.ToString(), txtTwitter.Text.ToString(), txtInstaGram.Text.ToString(), txtWhatsApp.Text.ToString(),ddlManagerRecordSet.SelectedValue.ToString());
            Response.Cookies.Add(new HttpCookie("Manager_RecordCount", ddlManagerRecordSet.SelectedValue.ToString()));
          
        }
        catch (Exception)
        {

        }
    }
}