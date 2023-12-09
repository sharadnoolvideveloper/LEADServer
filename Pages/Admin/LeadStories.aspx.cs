using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_LeadStories : System.Web.UI.Page
{
    LeadBL BLobj = new LeadBL();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                rptLeadStory.DataSource = BLobj.Admin_LoadStoriesRepeater(cook.Admin_Id());
                rptLeadStory.DataBind();
                btnUploadStoryCoverImage.Visible = false;
                btnUploadStoryCardImage.Visible = false;
                VideoReadMore.Visible = false;
                txtStoryURL.Enabled = false;
                lblEditStatus.Text = "New";
                cover.Visible = true;
                Card.Visible = false;
            }
        }
        catch (Exception)
        {

        }

    }

    protected void btnSaveStory_Click(object sender, EventArgs e)
    {
        try
        {
            int Story_Type = 0;
            string CardImgFileName = "";
            string CardImgFileExtenssion = "";
            string FileName = "";
            string FileExtenssion = "";
            string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string CardImgFilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            
            if(FileUpload2.HasFile)
            {
                CardImgFileName = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
                CardImgFileExtenssion = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
            }
            if(FileUpload1.HasFile)
            {
               FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
               FileExtenssion = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            }
           
            FilePath = FilePath + "_" + cook.Admin_Id() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
            CardImgFilePath = CardImgFilePath + "_" + cook.Admin_Id() + "_" + Guid.NewGuid().ToString() + CardImgFileExtenssion.ToString();

            if (rdoStoryType.SelectedItem.Value == "Content With Image")
            {
                Story_Type = 1;

            }
            else if (rdoStoryType.SelectedItem.Value == "Content With Image With Link")
            {
                Story_Type = 2;
            }
            else if (rdoStoryType.SelectedItem.Value == "Story Card")
            {
                Story_Type = 3;
            }
            else if (rdoStoryType.SelectedItem.Value == "Video Card")
            {
                Story_Type = 4;
            }
            if((Story_Type==1) || (Story_Type == 2) || (Story_Type == 3))
            {
                BLobj.Admin_SaveLeadStory(lblEditSlno.Text.ToString(), cook.Admin_Id(), txtStoryTitle.Text.ToString(), txtLeadStory.Text.ToString(), lblEditStatus.Text.ToString(), FileUpload1, FilePath.ToString(), FileExtenssion.ToString(), Story_Type, FileUpload2, CardImgFileExtenssion, CardImgFilePath.ToString(), txtStoryURL.Text.ToString());
            }
            else if(Story_Type==4)
            {
                BLobj.Admin_SaveLeadStoryVideo(lblEditSlno.Text.ToString(), cook.Admin_Id(), txtStoryTitle.Text.ToString(), txtLeadStory.Text.ToString(), lblEditStatus.Text.ToString(), txtStoryURL.Text.ToString(), Story_Type.ToString(),txtVideoReadMoreURL.Text.ToString());
            }
            


            txtLeadStory.Text = "";
            txtStoryTitle.Text = "";
            txtStoryURL.Text = "";
            rptLeadStory.DataSource = BLobj.Admin_LoadStoriesRepeater(cook.Admin_Id());
            rptLeadStory.DataBind();
            btnUploadStoryCoverImage.Visible = false;
            btnUploadStoryCardImage.Visible = false;
            VideoReadMore.Visible = false;
            lblEditStatus.Text = "New";
            string msg = "Story Created Successfully";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);

        }
        catch (Exception)
        {

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtLeadStory.Text = "";
            txtStoryTitle.Text = "";
            txtStoryURL.Text = "";
            lblEditStatus.Text = "New";
            imgStoryCard.ImageUrl = "";
            imgStoryCover.ImageUrl = "";
            txtVideoReadMoreURL.Text = "";
        }
        catch(Exception)
        {

        }
    }
    protected void rptLeadStory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string btnPressType = "";
            string Status = "";
            string code = e.CommandArgument.ToString();
            string[] itemlist = code.Split('_');
            int i;
            for (i = 0; i <= 1; i++)
            {
                if (i == 0)
                {
                    Status = itemlist[0].ToString();
                }
                else if (i == 1)
                {
                    btnPressType = itemlist[1].ToString();

                }
            }
            if (btnPressType == "StatusEdit")
            {
                Label lblSlno = (e.Item.FindControl("lblSlno") as Label);
                BLobj.Admin_UpdateStoryStatus(lblSlno.Text.ToString(), Status.ToString());
                rptLeadStory.DataSource = BLobj.Admin_LoadStoriesRepeater(cook.Admin_Id());
                rptLeadStory.DataBind();
            }
            else if(btnPressType== "Notification")
            {
                string StoryTitle = "", Image_path = "";
                Label lblSlno = (e.Item.FindControl("lblSlno") as Label);
                DataTable dt = BLobj.Admin_GetLeadStoryForNotification(lblSlno.Text.ToString());
                StoryTitle = dt.Rows[0].ItemArray[0].ToString()+"^^"+dt.Rows[0].ItemArray[4].ToString();
                Image_path = dt.Rows[0].ItemArray[3].ToString();
                dt = BLobj.Admin_GetDeviceIdForSendingEventAndStoriesNotification();
                foreach (DataRow dr in dt.Rows)
                {
                    FCMPushNotification.AndroidPush(dr[0].ToString(),StoryTitle.ToString(), "Stories", "http://mis.leadcampus.org/" + Image_path);
                }
            }
            if (btnPressType == "Delete")
            {
                Label lblSlno = (e.Item.FindControl("lblSlno") as Label);
                BLobj.Admin_DeleteStories(lblSlno.Text.ToString());
                rptLeadStory.DataSource = BLobj.Admin_LoadStoriesRepeater(cook.Admin_Id());
                rptLeadStory.DataBind();
            }
            else
            {
                Label lblSlno = (e.Item.FindControl("lblSlno") as Label);

                lblEditSlno.Text = lblSlno.Text.ToString();
                lblEditStatus.Text = "Edit";
                DataTable dt = BLobj.Admin_GetStoryFromSlno(lblSlno.Text.ToString(), cook.Admin_Id());
                txtStoryTitle.Text = dt.Rows[0].ItemArray[0].ToString();
                txtLeadStory.Text = dt.Rows[0].ItemArray[1].ToString();
                imgStoryCover.ImageUrl = dt.Rows[0].ItemArray[2].ToString();
                imgStoryCard.ImageUrl = dt.Rows[0].ItemArray[3].ToString();
                txtStoryURL.Text = dt.Rows[0].ItemArray[4].ToString();
                int Story_Type = int.Parse(dt.Rows[0].ItemArray[5].ToString());
                txtVideoReadMoreURL.Text= dt.Rows[0].ItemArray[7].ToString();

                if (Story_Type == 1)
                {
                    rdoStoryType.SelectedIndex = 0;
                    txtStoryURL.Enabled = false;
                    txtLeadStory.Enabled = true;

                    VideoReadMore.Visible = false;
                    cover.Visible = true;
                    Card.Visible = false;
                    btnUploadStoryCoverImage.Visible = true;
                    btnUploadStoryCardImage.Visible = false;

                }
                else if (Story_Type == 2)
                {
                   
                    rdoStoryType.SelectedIndex = 1;
                    txtStoryURL.Enabled = true;
                    txtLeadStory.Enabled = true;

                    cover.Visible = true;
                    Card.Visible = false;
                    VideoReadMore.Visible = false;
                    btnUploadStoryCoverImage.Visible = true;
                    btnUploadStoryCardImage.Visible = false;

                }
                else if (Story_Type == 3)
                {                   
                    rdoStoryType.SelectedIndex = 2;
                    txtStoryURL.Enabled = true;
                    txtLeadStory.Enabled = false;

                    cover.Visible = false;
                    Card.Visible = true;
                    VideoReadMore.Visible = false;
                    btnUploadStoryCoverImage.Visible = false;
                    btnUploadStoryCardImage.Visible = true;
                }
                else if(Story_Type==4)
                {
                    rdoStoryType.SelectedIndex = 3;
                    txtStoryURL.Enabled = true;
                    txtLeadStory.Enabled = true;

                    cover.Visible = false;
                    Card.Visible = false;
                    VideoReadMore.Visible = true;


                    btnUploadStoryCoverImage.Visible = false;
                    btnUploadStoryCardImage.Visible = false;
                }
            }
            //btnUploadStoryCoverImage.Visible = true;
            //btnUploadStoryCardImage.Visible = true;

        }
        catch (Exception)
        {

        }
    }

    protected void rptLeadStory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton Status = (e.Item.FindControl("btnEditStatus") as LinkButton);
                LinkButton Notification = (e.Item.FindControl("btnNotification") as LinkButton);
                LinkButton Delete = (e.Item.FindControl("btnDelete") as LinkButton);
                if (Status.Text == "0")
                {
                    Status.Text = "<span class='fa fa-thumbs-down'></span>";
                    Status.Attributes.Add("class", "btn btn-danger btn-rounded");

                    Notification.Text = "<span class='fa fa-bell'></span>";
                    Notification.Attributes.Add("class", "btn btn-danger btn-rounded");
                    Delete.Text = "<span class='fa fa-eraser'></span>";
                    Delete.Attributes.Add("class", "btn btn-danger btn-rounded");
                }
                else
                {
                    Status.Text = "<span class='fa fa-thumbs-up'></span>";
                    Status.Attributes.Add("class", "btn btn-primary btn-rounded");

                    Notification.Text = "<span class='fa fa-bell'></span>";
                    Notification.Attributes.Add("class", "btn btn-success btn-rounded");

                    Delete.Text = "<span class='fa fa-remove'></span>";
                    Delete.Attributes.Add("class", "btn btn-danger btn-rounded");
                }
                //Label ProjectStatus = (e.Item.FindControl("lblProjectStatus") as Label);
                //LinkButton ProjectStatus = (e.Item.FindControl("btnViewProject") as LinkButton);
                //btnEdit.Text = "<span class='fa fa-edit'></span>";
                //btnEdit.Attributes.Add("class", "btn btn-default btn-floating");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnUploadStoryCoverImage_Click(object sender, EventArgs e)
    {
        try
        {
            string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string FileExtenssion = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            FilePath = FilePath + "_" + cook.Admin_Id() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
            if (lblEditStatus.Text == "Edit")
            {
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    BLobj.Admin_UpdateImagePathAndUpload(FileUpload1, FilePath.ToString(), FileExtenssion, lblEditSlno.Text.ToString());
                }
                else
                {
                    string msg = "Select Only .PNG or .JPG!!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {
                string msg = "Not Allowed Without Story!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnUploadStoryCardImage_Click(object sender, EventArgs e)
    {
        try
        {
            string FilePath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string FileName = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
            string FileExtenssion = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
            FilePath = FilePath + "_" + cook.Admin_Id() + "_" + Guid.NewGuid().ToString() + FileExtenssion.ToString();
            if (lblEditStatus.Text == "Edit")
            {
                if ((FileExtenssion == ".png") || (FileExtenssion == ".PNG") || (FileExtenssion == ".jpeg") || (FileExtenssion == ".JPEG") || (FileExtenssion == ".Jpeg") || (FileExtenssion == ".JPG") || (FileExtenssion == ".jpg") || (FileExtenssion == ".Jpg"))
                {
                    BLobj.Admin_UpdateCardImagePathAndUpload(FileUpload2, FilePath.ToString(), FileExtenssion, lblEditSlno.Text.ToString());
                }
                else
                {
                    string msg = "Select Only .PNG or .JPG!!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
                }
            }
            else
            {
                string msg = "Not Allowed Without Story!!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
            }
        }
        catch (Exception)
        {

        }
    }
  
    protected void rdoStoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(rdoStoryType.SelectedItem.Value== "Content With Image")
            {
                txtStoryURL.Enabled = false;
                txtLeadStory.Enabled = true;
                txtStoryURL.Text = "";

                cover.Visible = true;
                Card.Visible = false;
                VideoReadMore.Visible = false;

            }
            else if(rdoStoryType.SelectedItem.Value == "Content With Image With Link")
            {
                txtStoryURL.Enabled = true;
                txtLeadStory.Enabled = true;

                cover.Visible = true;
                Card.Visible = false;
                VideoReadMore.Visible = false;
            }
            else if (rdoStoryType.SelectedItem.Value == "Story Card")
            {
                txtStoryURL.Enabled = true;
                txtLeadStory.Enabled = false;
                
                txtLeadStory.Text = "";

                cover.Visible = false;
                Card.Visible = true;
                VideoReadMore.Visible = false;
            }
            else if (rdoStoryType.SelectedItem.Value == "Video Card")
            {
                txtStoryURL.Enabled = true;
                txtLeadStory.Enabled = true;

                txtLeadStory.Text = "";

                cover.Visible = false;
                Card.Visible = false;
                VideoReadMore.Visible = true;
            }

        }
        catch(Exception)
        {

        }
    }
}