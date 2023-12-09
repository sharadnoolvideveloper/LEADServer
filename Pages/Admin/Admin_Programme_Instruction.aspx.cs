using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_Programme_Instruction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [System.Web.Services.WebMethod]
    public static string Admin_Programme_Content()
    {
        LeadBL BL = new LeadBL();
        string tableName = "";
        string ProgrammeType = "Instruction";
        if (ProgrammeType == "Instruction")
        {
            tableName = "programme_instruction_content";
        }
        else if (ProgrammeType == "Benefits")
        {
            tableName = "programme_benefits_content";
        }
        else if (ProgrammeType == "Note")
        {
            tableName = "programme_note";
        }
        string str= BL.Admin_Programme_Content(ProgrammeType.ToString(), tableName.ToString());

        return str;

    }

    [System.Web.Services.WebMethod()]
    public static List<GetInstruction> Admin_GET_Programme_List(string Type)
    {
        LeadBL BL = new LeadBL();
        List<GetInstruction> str = BL.Admin_GET_Programme_Content(Type.ToString());
        return str;
    }

    [System.Web.Services.WebMethod()]
    public static List<GetInstruction> Admin_Save_Programme_Content(vmProgramme vmProgram)
    {
        LeadBL BL = new LeadBL();
        List<GetInstruction> str = BL.Admin_Save_Programme_Content(vmProgram);
        return str;
    }
    [System.Web.Services.WebMethod()]
    public static void Admin_Update_Programme_Content_Status(string slno,string Status)
    {
        LeadBL BL = new LeadBL();
        BL.Admin_Update_Programme_Content_Status(slno,Status);
       
    }

    [System.Web.Services.WebMethod()]
    public static List<object> Admin_GET_Programme_ListBYId(string Slno)
    {
        LeadBL BL = new LeadBL();
        List<object> str = BL.Admin_GET_Programme_ContentBYId(Slno.ToString());
        return str;
    }

}