using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BL_Notify
/// </summary>
public class BL_Notify
{
    public BL_Notify()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["Notify"].ToString();
    public Sender_Notify Save_Notify(int User_Id, string Notify_Type, string Subject, string Message, string Language, int Notify_Slno, string Email_cc, string Email_bcc, int Status, string User_Type, GridView GV)
    {

        int New_Id = 0;
        int count = 0;
        Sender_Notify Notify = new Sender_Notify();
        Notify.status = "false";
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_SAVE_NOTIFY_MAIN";
                cmd.Parameters.AddWithValue("p_User_Id", User_Id);
                cmd.Parameters.AddWithValue("p_Notify_Type", Notify_Type);
                cmd.Parameters.AddWithValue("p_Subject", Subject);
                cmd.Parameters.AddWithValue("p_Message", Message);
                cmd.Parameters.AddWithValue("p_Language", Language);
                cmd.Parameters.AddWithValue("p_Email_cc", Email_cc);
                cmd.Parameters.AddWithValue("p_Email_bcc", Email_bcc);
                cmd.Parameters.AddWithValue("p_Notify_Slno", Notify_Slno);
                cmd.Parameters.AddWithValue("p_User_Type", User_Type);
                cmd.Parameters.AddWithValue("p_Status", Status);
                cmd.Parameters.Add(new MySqlParameter("p_New_Id", MySqlDbType.Int32)).Direction = ParameterDirection.Output;
                cmd.Connection = con;
                int i = cmd.ExecuteNonQuery();
                New_Id = int.Parse(cmd.Parameters["p_New_Id"].Value.ToString());
                Notify.Notify_Slno = New_Id;
                if (i > 0)
                {
                    Notify.status = "true";
                    for (int z = 0; z < GV.Rows.Count; z++)
                    {
                        CheckBox CheckList = ((CheckBox)GV.Rows[z].FindControl("ChkCollect"));
                        if (CheckList.Checked == true)
                        {
                            if (count == 0)
                            {

                            }
                            string lblSlno = ((Label)GV.Rows[z].FindControl("lblSlno")).Text.Trim();
                            string lblEntreprenuer_Id = ((Label)GV.Rows[z].FindControl("lblEntreprenuer_Id")).Text.Trim();
                            string lblMobile_No = ((Label)GV.Rows[z].FindControl("lblMobile_No")).Text.Trim();
                            string lblEmail_Id = ((Label)GV.Rows[z].FindControl("lblEmail_Id")).Text.Trim();
                            string lblDevice_Id = ((Label)GV.Rows[z].FindControl("lblDevice_Id")).Text.Trim();
                            string lblName = ((Label)GV.Rows[z].FindControl("lblName")).Text.Trim();

                            if (Notify_Type.ToString() == "Mail")
                            {
                                if ((lblEmail_Id.ToString() != "") || (lblEmail_Id.ToString() != null))
                                {

                                    i = Save_Notify_Details(New_Id, int.Parse(lblSlno.ToString()), lblEntreprenuer_Id.ToString(), lblName.ToString(), lblDevice_Id.ToString(), lblMobile_No.ToString(), lblEmail_Id.ToString());
                                    if (i == 1)
                                    {
                                        Notify.status = "true";
                                    }
                                    else
                                    {
                                        Notify.status = "false";
                                    }

                                }
                            }
                            else if (Notify_Type.ToString() == "Notification")
                            {
                                if (lblDevice_Id.ToString() != "")
                                {
                                    i = Save_Notify_Details(New_Id, int.Parse(lblSlno.ToString()), lblEntreprenuer_Id.ToString(), lblName.ToString(), lblDevice_Id.ToString(), lblMobile_No.ToString(), lblEmail_Id.ToString());
                                    if (i == 1)
                                    {
                                        Notify.status = "true";
                                    }
                                    else
                                    {
                                        Notify.status = "false";
                                    }
                                }

                            }
                            else if (Notify_Type.ToString() == "SMS")
                            {
                                int count_mobileNo = lblMobile_No.ToString().Count();
                                if ((count_mobileNo == 10) && (lblMobile_No.ToString() != ""))
                                {
                                    i = Save_Notify_Details(New_Id, int.Parse(lblSlno.ToString()), lblEntreprenuer_Id.ToString(), lblName.ToString(), lblDevice_Id.ToString(), lblMobile_No.ToString(), lblEmail_Id.ToString());
                                    if (i == 1)
                                    {
                                        Notify.status = "true";
                                    }
                                    else
                                    {
                                        Notify.status = "false";
                                    }
                                }

                            }

                        }
                    }
                }
                else
                {
                    Notify.status = "false";
                }

            }
        }
        catch (Exception ex)
        {
            Notify.status = "false";
        }
        return Notify;
    }
    public int Save_Notify_Details(int Notify_Slno, int Applicant_Slno, string Applicant_Id, string Name, string Device_Id, string Mobile_No, string Email_Id)
    {
        int i = 0;
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_SAVE_NOTIFY_DETAILS";
                cmd.Parameters.AddWithValue("p_Notify_Slno", Notify_Slno);
                cmd.Parameters.AddWithValue("p_Applicant_Slno", Applicant_Slno);
                cmd.Parameters.AddWithValue("p_Applicant_Id", Applicant_Id);
                cmd.Parameters.AddWithValue("p_Name", Name);
                cmd.Parameters.AddWithValue("p_Device_Id", Device_Id);
                cmd.Parameters.AddWithValue("p_Mobile_No", Mobile_No);
                cmd.Parameters.AddWithValue("p_Email_Id", Email_Id);
                cmd.Connection = con;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        catch (Exception ex)
        {
            return i;
        }
    }

    public DataTable Get_Notify_Dashboard(int User_Id)
    {
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_NOTIFY_DASHBOARD_COUNT";
                cmd.Parameters.AddWithValue("p_User_Id", User_Id);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception)
        {
            return null;
        }

    }

    public DataTable Get_Notify_Processing(int User_Id, string p_Type)
    {
        try
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_GET_PROCESSING_SENT_FAILED_TASK";
                cmd.Parameters.AddWithValue("p_User_Id", User_Id);
                cmd.Parameters.AddWithValue("p_Type", p_Type);
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }
        catch (Exception)
        {
            return null;
        }

    }
    public int Delete_Processing_Notify(int Notify_Slno)
    {
        int i = 0;
        try
        {
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_WEB_DELETE_NOTIFY";
                cmd.Parameters.AddWithValue("p_Notify_Slno", Notify_Slno);

                cmd.Connection = con;
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        catch (Exception ex)
        {
            return i;
        }
    }
}