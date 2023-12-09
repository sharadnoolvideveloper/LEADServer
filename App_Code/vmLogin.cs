using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for vmLogin
/// </summary>
public class vmLogin
{
    public vmLogin()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public long LoginId { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
    public int isProfileEdit { get; set; }
    public int RegistrationId { get; set; }
    public string Name { get; set; }
    public string ManagerName { get; set; }
    public int ManagerId { get; set; }
    public string Lead_Id { get; set; }
    public string MailId { get; set; }
    public string Location { get; set; }
    public string MobileNo { get; set; }
    public string UserImage { get; set; }
    public string College_Name { get; set; }
    public string AcademicId { get; set; }
    public string Manager_Image_Path { get; set; }

    public string Student_Type { get; set; }
    public string InstaGram { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string Gender { get; set; }
    public int StartCount { get; set; }
    public long Student_Mobile_No { get; set; }
    public int isFeePaid { get; set; }
    public string WhatsApp { get; set; }
    public int isRequestForTShirt { get; set; }

    public int isSanctionForTshirt { get; set; }

    public int TotalProjects
    {
        get;
        set;
    }
    public int LLP_Badges
    {
        get;set;
    }
    public int Prayana_Badges
    {
        get; set;
    }
    public int Yuva_Badges
    {
        get; set;
    }
    public int Valedicotry_Badges
    {
        get; set;
    }
    public int isStudentLEADer
    {
        get; set;
    }
    public int isBankDetails_Updated
    {
        get; set;
    }
}
public class vmLocationBI
{
    public string College_Name
    {
        get; set;
    }
    public string Taluk_Name
    {
        get; set;
    }
    public string DistrictName
    {
        get; set;
    }
    public string StateName
    {
        get; set;
    }
    public string latitude
    {
        get; set;
    }
    public string longitude
    {
        get; set;
    }
    public string Registrations
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }

}

public class vmstudentreg
{

    public int Registration_Id
    {
        get; set;
    }
    public string StudentName
    {
        get; set;
    }
    public string MobileNo
    {
        get; set;
    }
    public string MaidId
    {
        get; set;
    }
    public string Lead_id { get; set; }
    public int StateCode
    {
        get; set;
    }

    public int DistrictCode
    {
        get; set;
    }
    public int TalukaCode
    {
        get; set;
    }
    public int CollegeCode
    {
        get; set;
    }
    public string CollegeName
    {
        get; set;
    }
    public int StreamId
    {
        get; set;
    }
    public string RegistrationDate { get; set; }
    public int Fees
    {
        get; set;
    }



    public int isFeePaid { get; set; }
    public string Status { get; set; }
    public string TshirtSize { get; set; }
    public int projectcount { get; set; }
    public int RequestedId { get; set; }

}

public class vmProjectCounts
{
    public int Counts { get; set; }
    public string ProjectStatus { get; set; }
    public string Status { get; set; }
    public string ThemeName { get; set; }
    public int FiveStarRating { get; set; }
    public string TshirtStatus
    {
        get;
        set;
    }
    public int TshirtRequestCount
    {
        get;
        set;
    }
}

public class vmStudent
{
    public long RegistrationId { get; set; }
    public string Lead_Id { get; set; }
    public string ManagerCode { get; set; }
    public string StudentName { get; set; }
    public string DOB { get; set; }
    public string RegistrationDate { get; set; }
    public int isProfileEdit { get; set; }
    public int ActiveStatus { get; set; }
    public int AlertStatus { get; set; }
    public string AlertDate { get; set; }
    public int StateCode { get; set; }
    public int DistrictCode { get; set; }
    public int TalukaCode { get; set; }
    public int CollegeCode { get; set; }
    public int StreamCode { get; set; }
    public int CourseCode { get; set; }
    public string SemCode { get; set; }
    public long AadharNo { get; set; }
    public string Address { get; set; }
    public long MobileNo { get; set; }
    public string MailId { get; set; }
    public long AlternativeMobileNo { get; set; }
    public string AlternateMailId { get; set; }
    public string Gender { get; set; }
    public string BloodGroup { get; set; }
    public string FacebookId { get; set; }
    public string LinkedInId { get; set; }
    public long CreatedBy { get; set; }
    public string CreateDate { get; set; }
    public string AcademicCode { get; set; }
    public string Student_Type { get; set; }
    public string Bank_Name { get; set; }
    public string Branch_Name { get; set; }
    public string Account_No { get; set; }
    public string IFSC_code { get; set; }

    public string AccountHolderName { get; set; }
    public string Status { get; set; }
    public string Student_Image { get; set; }
    public string Image_path { get; set; }
    public byte[] ProfileImage { get; set; }

    public string ManagerName { get; set; }
    public string ManagerMailid { get; set; }
    public string ManagerImagePath { get; set; }
    public long ManagerMobileNumber { get; set; }
    public string College_Name { get; set; }

    public string StateName { get; set; }
    public string DistrictName { get; set; }
    public string Taluk_Name { get; set; }

    public string programmeName { get; set; }
    public string CourseName { get; set; }
    public string SemName { get; set; }

    public string Manager_Facebook { get; set; }
    public string Manager_Twitter { get; set; }
    public string Manager_Instagram { get; set; }
    public string Manager_WhatsApp { get; set; }

    public string MyTalent
    {
        get; set;
    }

    public string Bank_Image_path
    {
        get; set;
    }

}

public class vmstory
{

    public int slno { get; set; }
    public string Story_Title { get; set; }
    public string Story_Description { get; set; }
    public string Image_Path { get; set; }
    public string Created_Date { get; set; }
    public string status { get; set; }
    public string Card_Image_Path { get; set; }
    public string URL_Link { get; set; }
    public int Story_Type { get; set; }
    public String Video_Story_URL { get; set; }
}
public class vmMember
{
    public long Member_Id { get; set; }
    public string MemberName { get; set; }
    public string MemberEmail { get; set; }
}

public class vmstate
{
    public long code { get; set; }
    public string StateName { get; set; }
    public string Status { get; set; }
}
public class vmdist
{
    public long DistrictId { get; set; }
    public string DistrictName { get; set; }
    public long Stateid { get; set; }
    public string status { get; set; }

}
public class vmcity
{
    public long Id { get; set; }
    public long District_Id { get; set; }
    public string Taluk_Name { get; set; }
    public string status { get; set; }

}

public class vmclg
{
    public long CollegeId { get; set; }
    public long TalukId { get; set; }
    public string College_Name { get; set; }
    public int Fees
    {
        get; set;
    }
    public int Fees_Id
    {
        get; set;
    }
    public string Status { get; set; }
}

public class vmCollege
{
    public string CollegeId { get; set; }
   
    public string College_Name { get; set; }
    public string Status { get; set; }
}


public class vmprog
{
    public long programmeId { get; set; }
    public string status { get; set; }
    public string programmeName { get; set; }

}
public class vmcourse
{
    public long CourseId { get; set; }
    public string CourseName { get; set; }

    public long ProgrammeCode { get; set; }
    public string Status { get; set; }
}

public class vmsem
{
    public long SemId { get; set; }
    public string SemName { get; set; }
    public string Status { get; set; }

}

public class vmtheme
{
    public long ThemeId { get; set; }
    public string ThemeName { get; set; }
    public string status { get; set; }
}




public class vmGetpd
{

    public string Title { get; set; }
    public string Theme { get; set; }
    public string ThemeName { get; set; }
    public long BeneficiaryNo { get; set; }
    public string Objectives { get; set; }
    public string ActionPlan { get; set; }
    public long AskedAmount { get; set; }
    public long SanctionAmount { get; set; }
    public long giventotal { get; set; }
    public string Placeofimplement { get; set; }
    public string Challenge { get; set; }
    public string Learning { get; set; }
    public string AsAStory { get; set; }
    public string CurrentSituation { get; set; }
    public string Resource { get; set; }
    public string status { get; set; }
    public int HoursSpend
    {
        get;
        set;
    }
    public string ProjectStartDate
    {
        get; set;
    }
    public string ProjectEndDate
    {
        get; set;
    }
    public int IsImpactProject
    {
        get; set;
    }
    public string ProjectStatus
    {
        get; set;
    }

 
    public string Collaboration_Supported
    {
        get;
        set;
    }
    public string Permission_And_Activities
    {
        get;
        set;
    }
    public string Experience_Of_Initiative
    {
        get;
        set;
    }
    public string Lacking_initiative
    {
        get;
        set;
    }
    public string Against_Tide
    {
        get;
        set;
    }
    public string Cross_Hurdles
    {
        get;
        set;
    }
    public string Entrepreneurial_Venture
    {
        get;
        set;
    }
    public string Government_Awarded
    {
        get;
        set;
    }
    public string Leadership_Roles
    {
        get;
        set;
    }
    public List<vmProjectSDG_Goals> SDG_List
    {
        get; set;
    }
    public string SDG_Status
    {
        get; set;
    }

    public long TotalResourses
    {
        get; set;
    }
}

public class vmpl
{
    public string Lead_Id { get; set; }
    public long PDId { get; set; }
    public string Title { get; set; }
    public string status { get; set; }
    public long SanctionAmount { get; set; }
    public long Giventotal { get; set; }
    public int CompletionProgress { get; set; }
    public string ProjectStatus { get; set; }
    public int isImpact_Project
    {
        get; set;
    }
}

public class vmproject
{
    public string StudentName { get; set; }
    public string Lead_Id { get; set; }
    public long CollegeCode { get; set; }
    public string College_name { get; set; }
    public long PDId { get; set; }
    public long ManagerId { get; set; }
    public long Student_Id { get; set; }
    public string Title { get; set; }
    public string Theme { get; set; }
    public string ThemeName { get; set; }
    public long BeneficiaryNo { get; set; }
    public string ActionPlan { get; set; }
    public string Objectives { get; set; }
    public long Amount { get; set; }
    public long SanctionAmount { get; set; }
    public int TotalCost { get; set; }
    public string ProjectStatus { get; set; }

    public string ManagerComments { get; set; }
 
    public long TeamDetails { get; set; }
    public string Meterials { get; set; }
    public string Placeofimplement { get; set; }
    public long Budget { get; set; }
    public long TotalBudget { get; set; }
    public long Duratoin { get; set; }
    public long Beneficiaries { get; set; }

    public string MobileNo { get; set; }

    public string CurrentSituation { get; set; }

    public string AdminComments { get; set; }



    public long FundsReceived { get; set; }

    public long FundsRaised { get; set; }
    public long FundsUtilized { get; set; }
    public string Resource { get; set; }
    public string ProductDetail { get; set; }
    public string Actbeni { get; set; }

    public int Amounts { get; set; }
    public string Output { get; set; }
    public string Challenge { get; set; }



    public long AskedAmount { get; set; }

    public long giventotal { get; set; }

    public string MeterialName { get; set; }

    public float MeterialCost { get; set; }

    public double Rating { get; set; }
    public List<vmmaterial> materials { get; set; }


    public string status { get; set; }
    public string Student_Image_Path { get; set; }

    public string BeneficiariesList { get; set; }

    public List<vmMember> Members { get; set; }

    public string AcademicCode { get; set; }

    public string ProjectStartDate
    {
        get; set;
    }

    public string ProjectEndDate
    {
        get; set;
    }

    public int HoursSpend
    {
        get;
        set;
    }
    public string StreamCode
    {
        get;
        set;
    }
    public int IsImpactProject
    {
        get;
        set;
    }
}
public class vmconts
{
    public int TotalRegistration { get; set; }
    public int FeePaiedStudent { get; set; }
    public string Status { get; set; }
}
public class vmmaterial
{
    public long slno { get; set; }

    public string MeterialName { get; set; }
    public float MeterialCost { get; set; }
}

public class vmmaterialjson
{
    public long slno { get; set; }

    public string MeterialName { get; set; }
    public string MeterialCost { get; set; }
}


public class vmEvent
{
    public long EventId { get; set; }
    public string EventName { get; set; }
    public string EventFromDate { get; set; }
    public string EventToDate { get; set; }
    public string EventDescription { get; set; }
    public string EventApplyURL { get; set; }
    public string EventURL { get; set; }
    public string Image_Path { get; set; }
    public int EventStatus { get; set; }
    public string Status { get; set; }
}
public class vmmanager
{
    public long ManagerId { get; set; }
    public string ManagerName { get; set; }

    public string Address { get; set; }
    public string MobileNo { get; set; }
    public string MailId { get; set; }
    public string Gender { get; set; }
    public string BloodGroup { get; set; }

    public string Status { get; set; }

    public string Image_Path { get; set; }

    public byte[] ProfileImage { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string InstaGram { get; set; }
    public string WhatsApp { get; set; }

}

public class vmProjectCompletion
{
    public long PDId { get; set; }
    public string Title { get; set; }
    public string Theme { get; set; }
    public int BeneficiaryNo { get; set; }
    public string Objectives { get; set; }
    public string Placeofimplement { get; set; }
    public float FundsRaised { get; set; }
    public float SanctionAmount { get; set; }
    public float Fund_Received { get; set; }
    public string Challenge { get; set; }
    public string Learning { get; set; }
    public string AsAStory { get; set; }
    public List<vmProjectDocs> docs { get; set; }
    public string Status { get; set; }

    public string CurrentSituation { get; set; }
    public string Resource { get; set; }

    public int CompletionProgress { get; set; }

    public string ProjectStatus { get; set; }

    public int HoursSpend
    {
        get;
        set;
    }
    public string ProjectStartDate
    {
        get;
        set;
    }
    public string ProjectEndDate
    {
        get;
        set;
    }
    public string Collaboration_Supported
    {
        get;
        set;
    }
    public string Permission_And_Activities
    {
        get;
        set;
    }
    public string Experience_Of_Initiative
    {
        get;
        set;
    }
    public string Lacking_initiative
    {
        get;
        set;
    }
    public string Against_Tide
    {
        get;
        set;
    }
    public string Cross_Hurdles
    {
        get;
        set;
    }
    public string Entrepreneurial_Venture
    {
        get;
        set;
    }
    public string Government_Awarded
    {
        get;
        set;
    }
    public string Leadership_Roles
    {
        get;
        set;
    }
    public List<vmProjectSDG_Goals> SDG_List
    {
        get; set;
    }

    public long TotalResourses
    {
        get; set;
    }
    public string SDG_Status
    {
        get; set;
    }

    public string Project_Level
    {
        get; set;
    }

    public string ManagerComments
    {
        get; set;
    }

    public int Rating
    {
        get; set;
    }
    public int isImpactProject
    {
        get; set;
    }

}
public class vmProjectSDG_Goals
{
    public int Slno
    {
        get;set;
    }
    public string Goals
    {
        get; set;
    }
    public string Goal_Status
    {
        get;set;
    }

   
}

public class vmProjectDocs
{
    public long PDId { get; set; }
    public long SlNo { get; set; }
    public int Document_Id { get; set; }
    public string Document_Path { get; set; }
    public string Document_Type { get; set; }
}


public class FaceBookUser
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string PictureUrl { get; set; }
    public string Email { get; set; }
}
public class vmleadmenber
{
    public string Lead_Id { get; set; }

    public string StudentName { get; set; }
    public long NoOfProject { get; set; }
    public string Status { get; set; }

    public int isApply_MasterLeader { get; set; }

    public int isApply_LeadAmbassador { get; set; }


}

public class vmstdtype
{
    public string Lead_Id { get; set; }
    public string StudentName { get; set; }
    public int isApply_MasterLeader { get; set; }
    public int isApply_LeadAmbassador { get; set; }
    public string Student_Type { get; set; }
    public string Status { get; set; }


}
public class vmfundstatus
{
    public long fundriserdamount { get; set; }

    public long SanctionAmount { get; set; }
    public long fundRelised { get; set; }
    public string Status { get; set; }

    public int AcademicCode { get; set; }
    public int ManagerId { get; set; }
    public string ManagerName { get; set; }

    public string AcademicYear { get; set; }
    public long FiveStarRating { get; set; }
}

public class vmStudentReq
{
    public string Lead_id { get; set; }
    public string StudentName { get; set; }
    public string Email_id { get; set; }
    public string Student_MobileNo { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }

}

public class vmapp
{
    public string AppVersion { get; set; }
    public string versionCode { get; set; }
    public string MissCallNumber { get; set; }
    public string Status { get; set; }
}

public class vmtshirt
{
    public int AllotedS { get; set; }
    public int AllotedM { get; set; }

    public int AllotedL { get; set; }
    public int AllotedXL { get; set; }
    public int AllotedXXL { get; set; }
    public int UsedS { get; set; }
    public int UsedM { get; set; }
    public int UsedL { get; set; }
    public int UsedXL { get; set; }

    public int UsedXXL { get; set; }

    public string Status { get; set; }
}

public class vmNotification
{
    public string Notification_Type { get; set; }
    public string Notification_Message { get; set; }
    public string Notification_Date { get; set; }
    public string Status { get; set; }
}
public class vmTshirtApproval
{
    public string  TshirtList { get; set; }
    public string ManagerId { get; set; }
    public string Size { get; set; }
    public string RequesteType { get; set; }
}
public class vmtshirtlist
{
    public int RequestedId { get; set; }
    public string TshirtSize { get; set; }

    public string RequestedDate { get; set; }
    public string SanctionDate { get; set; }
    public string TshirtStatus { get; set; }

    public string Status { get; set; }

}
public class vmWorkDiary
{
   public string ManagerId { get; set; }
   public string MainCategory { get; set; }
   public string SubCategory { get; set; }
   public string Descritpion { get; set; }
   public string TotalParticipants { get; set; }
   public string CollegeId { get; set; }
   public string SpentTime { get; set; }
   public string Remarks { get; set; }
   public string Progress { get; set; }


}
public class vmContact_Us
{
  public int  slno { get; set; }
  public string Sandbox_Name { get; set; }
  public string Sandbox_Address { get; set; }
  public string Contact_Person { get; set; }
  public string Contact_Number1 { get; set; }
  public string Contact_Mailid1 { get; set; }
  public string Contact_Mailid2 { get; set; }
  public string Type { get; set; }
  public int Priority { get; set; }
  public string Status { get; set; }
}
public class vmAcademicYear
{
    public int slno { get; set; }
    public string AcademicCode { get; set; }
    public string Status { get; set; }
}
public class vmCollegeSummaryCounts
{
    public string Registration_Count
    {
        get; set;
    }
    public string Project_Count
    {
        get; set;
    }
    public string Funded_Amount
    {
        get;set;
    }
    public string Status
    {
        get; set;
    }
    public List<vmCollege_Count_List> ProjectList
    {
        get; set;
    }
   

}
public class vmCollege_Count_List
{
    public string Lead_Id
    {
        get; set;
    }
    public string StudentName
    {
        get; set;
    }
    public string ProjectTitle
    {
        get; set;
    }
   
}
public class GetInstruction
{
    public int slno { get; set; }
    public string content { get; set; }
    public int Status { get; set; }


    public GetInstruction(int _slno, string _content, int _Status)
    {
        slno = _slno;
        content = _content;
        Status = _Status;


    }
}

public class vmSuggestionFeedbackHead
{
    public string Slno
    {
        get; set;
    }
    public string Head_Name
    {
        get; set;
    }
    public string Status
    {
        get;set;
    }

}
public class vmManagerOpenRequest
{
    public string Ticket_No
    {
        get; set;
    }
   
    public string Request_Date
    {
        get; set;
    }
    public string Lead_Id
    {
        get; set;
    }
    public string Student_Name
    {
        get; set;
    }
    public string MobileNo
    {
        get; set;
    }
    public string RequestHead_Id
    {
        get; set;
    }
    public string Project_Id
    {
        get; set;
    }
    public string ProjectName
    {
        get; set;
    }
    public string Request_type
    {
        get; set;
    }
    public string Request_Message
    {
        get; set;
    }
    public string Response_Message
    {
        get; set;
    }
    public string Request_Priority
    {
        get; set;
    }
    public string College_Name
    {
        get; set;
    }
    public string MailId
    {
        get; set;
    }
    public string Manager_MailId
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
    public string Respond_Date
    {
        get; set;
    }

}

public class vmGetMentorMentee
{
    public string Comments
    {
        get; set;
    }
    public string UserType
    {
        get; set;
    }
    public string ManagerName
    {
        get; set;
    }
    public string StudentName
    {
        get; set;
    }
    public string ReplyTime
    {
        get; set;
    }
    public string ProjectStatus
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }

}

public class vmGet_Academic_Detail
{
    public int Academic_Id
    {
        get; set;
    }
    public string Academic_Code
    {
        get; set;
    }
    public string Year_Code
    {
        get; set;
    }
    public string From_Date
    {
        get; set;
    }
    public string To_Date
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
    

}

public class vmGet_Manager_Fees_Summary
{
    public int Fees_Category_Id
    {
        get; set;
    }
    public string Fees_Category_Name
    {
        get; set;
    }
    public int Collected
    {
        get; set;
    }
    public int Submitted
    {
        get; set;
    }
    public int Balance
    {
        get; set;
    }
    public int Total
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
}
public class vmPayment_Details
{
    public int Payment_Id
    {
        get; set;
    }
    public string Lead_Id
    {
        get; set;
    }
    public string StudentName
    {
        get; set;
    }
    public int Registration_Id
    {
        get; set;
    }
    public int Paid_Fees
    {
        get; set;
    }
  
    public string paid_date
    {
        get; set;
    }
    public string Created_Date
    {
        get; set;
    }
    public int Auto_Receipt_No
    {
        get; set;
    }
    public int transanction_Id
    {
        get; set;
    }
    public int reference_id
    {
        get; set;
    }
    public string Fees_Category_description
    {
        get; set;
    }
    public string transactionStatus
    {
        get; set;
    }
    public string YearCode
    {
        get; set;
    }
    public string Payment_Type
    {
        get; set;
    }
    public string Payeer_Id
    {
        get; set;
    }
    public string Created_User_Type
    {
        get; set;
    }
    public string Payment_Mode
    {
        get; set;
    }
    public string Payment_Remark
    {
        get; set;
    }
  
    public string Manager_Submission_Status
    {
        get; set;
    }
    public string Payment_Receipt_Path
    {
        get; set;
    }
    public string Rec_Date
    {
        get; set;
    }
    public string Rec_Remark
    {
        get; set;
    }
    public string Rec_By
    {
        get; set;
    }
    public string Rec_Status
    {
        get; set;
    }

    public string Status
    {
        get; set;
    }
}

public class vmGet_Manager_Fees_Submission
{
    public int Submission_slno
    {
        get; set;
    }
    public int Fees_Category_Id
    {
        get; set;
    }
    public string Fees_Category_description
    {
        get; set;
    }
    public int Submission_Amount
    {
        get; set;
    }
    public string Submitted_Date
    {
        get; set;
    }

    public string Submitted_Mode
    {
        get; set;
    }
    public string Submitted_Remark
    {
        get; set;
    }
    public int Submitted_By
    {
        get; set;
    }
    public string Rec_Status
    {
        get; set;
    }
    public string Submitter_Name
    {
        get; set;
    }
    public string Rec_Date
    {
        get; set;
    }
    public string Rec_By
    {
        get; set;
    }
    public string Rec_Mail_id
    {
        get; set;
    }
    public string Rec_Remark
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
}

public class vmGET_FeeCategory_Master
{
    public int Fees_Category_Slno
    {
        get; set;
    }
    public string fees_category_code
    {
        get; set;
    }
    public string Fees_category_description
    {
        get; set;
    }
    public int Fees
    {
        get; set;
    }
    public int Fees_ID
    {
        get; set;
    }
    public int academic_year
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
  
}
public class vmGet_Payment_Mode
{
    public int payment_mode_slno
    {
        get; set;
    }
    public string short_code
    {
        get; set;
    }
    public string description
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }

}
public class vmGet_Student_Allocated_Fees
{
    public int Registration_ID
    {
        get; set;
    }
    public int Fees_Category_ID
    {
        get; set;
    }
    public int College_ID
    {
        get; set;
    }
    public string College_Name
    {
        get; set;
    }
    public int Allocated_Fees
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
}
public class vmManagerRequestClose
{
    public string Ticket_No
    {
        get; set;
    }
    public string RequestHead_Id
    {
        get; set;
    }
    public string Request_Date
    {
        get; set;
    }
    public string Lead_Id
    {
        get; set;
    }
    public string Student_Name
    {
        get; set;
    }
    public string MobileNo
    {
        get; set;
    }
    public string Request_type
    {
        get; set;
    }
    public string Request_Message
    {
        get; set;
    }
    public string Request_Priority
    {
        get; set;
    }
    public string Response_Message
    {
        get;set;
    }
    public string Status
    {
        get; set;
    }
}
public class vmManagerRequesIdWiseDetails
{
    public string Ticket_No
    {
        get; set;
    }
    public string Request_Date
    {
        get; set;
    }
    public string Lead_Id
    {
        get; set;
    }
    public string Student_Name
    {
        get; set;
    }
    public string MobileNo
    {
        get; set;
    }
    public string MailId
    {
        get; set;
    }
    public string CollegeName
    {
        get; set;
    }
    public string Request_type
    {
        get; set;
    }
    public string Request_Message
    {
        get; set;
    }
    public string Response_Message
    {
        get; set;
    }
 
}

public class vmManagerRequestModule
{
    public string OpenRequest
    {
        get;set;
    }
    public string ClosedRequest
    {
        get; set;
    }
}
public class vmStudentRequest_Head
{
   
   

    public List<Request_Heads> RequestHead
    {
        get; set;
    }
    public List<Get_Student_Request_Project> Projects
    {
        get; set;
    }
    public string request_Id
    {
        get;set;
    }
    public string Request_Date
    {
        get; set;
    }
    public string Request_Message
    {
        get;set;
    }

    public string Request_Type
    {
        get; set;
    }
    public string Request_Priority
    {
        get; set;
    }
    public string Request_Status
    {
        get; set;
    }
    public string HeadId
    {
        get; set;
    }
    public string HeadName
    {
        get;set;
    }

    public string Project_Id
    {
        get; set;
    }
    public string Project_Title
    {
        get; set;
    }
    public string Response_Date
    {
        get; set;
    }
    public string Response_Message
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }



}
public class Request_Heads
{
    public string slno
    {
        get; set;
    }
    public string Head_Name
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
}
public class Get_Student_Request_Project
{
    public string PDID
    {
        get;set;
    }
    public string ProjectTitle
    {
        get;set;
    }
    public string Status
    {
        get;set;
    }
}

public class Sender_Notify
{
    public int Notify_Slno
    {
        get; set;
    }
    public int User_Id
    {
        get; set;
    }
    public string status
    {
        get; set;
    }
}

public class vmGet_Master_Ticket_Status
{
    public int Slno
    {
        get; set;
    }
    public string Ticket_Status
    {
        get; set;
    }
    public int Manager_Alert_Days
    {
        get; set;
    }
    public int Account_Alert_Days
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }

}
public class vmFunding_Details
{
    public List<vmGet_Student_List> Student_Details
    {
        get; set;
    }
    
    public string Status
    {
        get; set;
    }
}
public class vmGet_Student_List
{
    public int Registration_Id
    {
        get; set;
    }
    public string Lead_Id
    {
        get; set;
    }
    public string Student_Name
    {
        get; set;
    }
    public string Mobile_No
    {
        get; set;
    }
    public string Email_Id
    {
        get; set;
    }
    public string College_Name
    {
        get; set;
    }
    public string Sem_Name
    {
        get; set;
    }
   
    public string Status
    {
        get; set;
    }
    public List<vmGet_Released_Fund_Details> Fund_Details
    {
        get; set;
    }
}
public class vmGet_Released_Fund_Details
{
    public int PDID
    {
        get; set;
    }
    public string Project_Title
    {
        get; set;
    }
    public int Requested_Amount
    {
        get; set;
    }
    public int Santioned_Amount
    {
        get; set;
    }
    public int Released_Amount
    {
        get; set;
    }
    public int Total_Released_Amount
    {
        get; set;
    }
    public int Balance_Amount
    {
        get; set;
    }
     public string Status
    {
        get; set;
    }

}

public class vmGet_Funding_Status
{
    public int Ticket_Id
    {
        get; set;
    }
    public string Requeted_By
    {
        get; set;
    }
    public string Requested_Date
    {
        get; set;
    }
    public string Approved_By
    {
        get; set;
    }
    public string Approved_Date
    {
        get; set;
    }
    public string Approved_Remark
    {
        get; set;
    }
    public string Approval_Status
    {
        get; set;
    }

    public int Requested_Project
    {
        get; set;
    }

    public int Manager_Approved_Project
    {
        get; set;
    }
    public int Account_Approved_Project
    {
        get; set;
    }
    public int Requested_Amount
    {
        get; set;
    }
    public int Manager_Approved_Amount
    {
        get; set;
    }
    public int Account_Approved_Amount
    {
        get; set;
    }

    public string status
    {
        get; set;
    }
}

