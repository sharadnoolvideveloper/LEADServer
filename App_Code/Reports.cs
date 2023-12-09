using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Universal.Standard;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using DocumentFormat.OpenXml.ExtendedProperties;
using MySql.Data.MySqlClient;
using System.Web.WebPages;

/// <summary>
/// Summary description for Reports
/// </summary>
public class Reports
{
    UniversalDL DLobj = new UniversalDL();

    string gstrQrystr = "";
    public Reports()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable Manager_GetStudentListWithFunded(string ManagerId, string AcademicYear)
    {

        DataTable dt = new DataTable();
        if (AcademicYear.ToString() != "[All]")
        {
            gstrQrystr = "SELECT DATE_FORMAT(SR.RegistrationDate, '%d-%m-%Y') as RegistrationDate,SR.Lead_Id,SR.managerCode, SR.StudentName, PD.Title,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as SanctionDate, PD.Amount AS RequestedAmount," + " " +
            "PD.SanctionAmount AS SanctionAmount, (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId and" + " " +
            "PF.ManagerId = " + ManagerId.ToString() + " and PD.AcademicCode=" + AcademicYear.ToString() + ") as Disperse, PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount" + " " +
            "from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId and PF.ManagerId = " + ManagerId.ToString() + " and PD.AcademicCode=" + AcademicYear.ToString() + ") as Balance," + " " +
            "CLG.College_Name, DATE_FORMAT(SR.DOB, '%d-%m-%Y') as DOB FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id AND SR.ManagerCode = PD.ManagerId " + " " +
            "INNER JOIN colleges AS CLG ON SR.CollegeCode = CLG.CollegeId INNER JOIN mstr_taluka AS TK ON CLG.TalukId = TK.Id where pd.academiccode=" + AcademicYear.ToString() + " and PD.ManagerId=" + ManagerId.ToString() + "";


        }
        else
        {
            gstrQrystr = "SELECT DATE_FORMAT(SR.RegistrationDate, '%d-%m-%Y') as RegistrationDate,SR.Lead_Id,SR.managerCode, SR.StudentName, PD.Title, PD.Amount AS RequestedAmount," + " " +
            "PD.SanctionAmount AS SanctionAmount, (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId and" + " " +
            "PF.ManagerId = " + ManagerId.ToString() + ") as Disperse, PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount" + " " +
            "from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId and PF.ManagerId = " + ManagerId.ToString() + ") as Balance," + " " +
            "CLG.College_Name, DATE_FORMAT(SR.DOB, '%d-%m-%Y') as DOB FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id AND SR.ManagerCode = PD.ManagerId " + " " +
            "INNER JOIN colleges AS CLG ON SR.CollegeCode = CLG.CollegeId INNER JOIN mstr_taluka AS TK ON CLG.TalukId = TK.Id where PD.ManagerId=" + ManagerId.ToString() + "";
        }
        dt = DLobj.GetDataTable(gstrQrystr);
        return dt;

    }
    public DataTable Manager_GetFundingDetailsBetweenDates(string ManagerId, string FromDate, string ToDate)
    {
        gstrQrystr = "SELECT DATE_FORMAT(SR.RegistrationDate, '%d-%m-%Y') as RegistrationDate,SR.Lead_Id,SR.managerCode, SR.StudentName,SR.mobileno, PD.Title,DATE_FORMAT(PD.ProposedDate, '%d-%m-%Y') as ProposedDate,DATE_FORMAT(PD.ApprovedDate, '%d-%m-%Y') as SanctionDate, PD.Amount AS RequestedAmount," + " " +
            "PD.SanctionAmount AS SanctionAmount, (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId and" + " " +
            "PF.ManagerId = " + ManagerId.ToString() + " and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')) as Disperse, PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount" + " " +
            "from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId and PF.ManagerId = " + ManagerId.ToString() + " and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'))  as Balance," + " " +
            "CLG.College_Name,sem.semname,DATE_FORMAT(SR.DOB, '%d-%m-%Y') as DOB,PD.ProjectStatus,'' as Signature FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id AND SR.ManagerCode = PD.ManagerId " + " " +
            "INNER JOIN colleges AS CLG ON SR.CollegeCode = CLG.CollegeId INNER JOIN mstr_taluka AS TK ON CLG.TalukId = TK.Id INNER JOIN mstr_semester AS sem ON SR.SemCode = sem.SemId where(Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  and PD.ManagerId=" + ManagerId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_AcademicYearAndProjectStatusWiseReport(string ManagerId, string AcademicCode, string ProjectStatus)
    {
        DataTable dt = new DataTable();
        if (AcademicCode.ToString() != "[All]")
        {
            if (ProjectStatus != "[All")
            {
                gstrQrystr = "SELECT SR.RegistrationDate, SR.Lead_Id, SR.StudentName, PD.Title,PD.ProjectStatus, PD.ProposedDate, PD.ApprovedDate, PD.CompletedDate, PD.RejectedDate, PD.RequestForModificationDate, PD.RequestForCompletionDate, PD.Rating" + " " +
           "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id  AND SR.ManagerCode = PD.ManagerId" + " " +
           "WHERE(SR.ManagerCode = " + ManagerId.ToString() + ") and(PD.AcademicCode = " + AcademicCode.ToString() + ") and (PD.ProjectStatus = '" + ProjectStatus.ToString() + "')";
            }
            else
            {
                gstrQrystr = "SELECT SR.RegistrationDate, SR.Lead_Id, SR.StudentName, PD.Title,PD.ProjectStatus, PD.ProposedDate, PD.ApprovedDate, PD.CompletedDate, PD.RejectedDate, PD.RequestForModificationDate, PD.RequestForCompletionDate, PD.Rating" + " " +
           "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id  AND SR.ManagerCode = PD.ManagerId" + " " +
           "WHERE(SR.ManagerCode = " + ManagerId.ToString() + ") and(PD.AcademicCode = " + AcademicCode.ToString() + ")";
            }

        }
        else
        {
            if (ProjectStatus != "[All")
            {
                gstrQrystr = "SELECT SR.RegistrationDate, SR.Lead_Id, SR.StudentName, PD.Title,PD.ProjectStatus, PD.ProposedDate, PD.ApprovedDate, PD.CompletedDate, PD.RejectedDate, PD.RequestForModificationDate, PD.RequestForCompletionDate, PD.Rating" + " " +
           "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id  AND SR.ManagerCode = PD.ManagerId" + " " +
           "WHERE(SR.ManagerCode = " + ManagerId.ToString() + ") and (PD.ProjectStatus = '" + ProjectStatus.ToString() + "')";
            }
            else
            {
                gstrQrystr = "SELECT SR.RegistrationDate, SR.Lead_Id, SR.StudentName, PD.Title,PD.ProjectStatus, PD.ProposedDate, PD.ApprovedDate, PD.CompletedDate, PD.RejectedDate, PD.RequestForModificationDate, PD.RequestForCompletionDate, PD.Rating" + " " +
           "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id  AND SR.ManagerCode = PD.ManagerId" + " " +
           "WHERE(SR.ManagerCode = " + ManagerId.ToString() + ")";
            }


        }
        dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }

    public DataTable Manager_StudentWithProjectsFolderCreating(string ManagerId, string FromDate, string ToDate)
    {
        if (ManagerId == "[All]")
        {
            gstrQrystr = "SELECT SR.Lead_Id, PD.PDId, PD.Title FROM student_registration AS SR INNER JOIN" + " " +
     "project_description AS PD ON SR.Lead_Id = PD.Lead_Id AND SR.ManagerCode = PD.ManagerId" + " " +
     "WHERE (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')";
        }
        else
        {
            gstrQrystr = "SELECT SR.Lead_Id, PD.PDId, PD.Title FROM student_registration AS SR INNER JOIN" + " " +
     "project_description AS PD ON SR.Lead_Id = PD.Lead_Id AND SR.ManagerCode = PD.ManagerId" + " " +
     "WHERE(SR.ManagerCode = " + ManagerId.ToString() + ") AND (Date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')";
        }



        return DLobj.GetDataTable(gstrQrystr);

    }
    public DataTable Common_LeadIdWithProjectsListFolderCreating(string ManagerId, string ProjectStatus, string LeadId)
    {
        gstrQrystr = "Update student_Registration set IsFolderDownload=1 where lead_Id='" + LeadId.ToString() + "'";
        DLobj.ExecuteQuery(gstrQrystr);
        gstrQrystr = "SELECT PD.PDId, PD.Title FROM project_description AS PD WHERE(PD.ManagerId = " + ManagerId.ToString() + ") AND (PD.lead_id='" + LeadId.ToString() + "') AND (PD.ProjectStatus='" + ProjectStatus.ToString() + "')";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Student_IndivisualStudentFolderCreating(string PDId)
    {
        gstrQrystr = "SELECT Document_Path from project_digital_documents  WHERE project_digital_documents.PDId=" + PDId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);

    }
    public DataTable Manager_StudentWithDigitalDocuments(string PDId)
    {
        gstrQrystr = "SELECT PD.Title, PDD.Document_Path, PD.PDId FROM project_description AS PD INNER JOIN" + " " +
        "project_digital_documents AS PDD ON PD.PDId = PDD.PDId AND PD.Lead_Id = PDD.LeadId WHERE(PDD.PDId = " + PDId.ToString() + ")";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetMaleFemaleCount(string ManagerID, string AcademicCode)
    {
        //gstrQrystr = "select count(lead_ID) as TotalCount, SUM(CASE WHEN Gender = 'M' THEN 1 ELSE 0 END) as Male,SUM(CASE WHEN Gender = 'F' THEN 1 ELSE 0 END) as Female from student_registration where ManagerCode ="+ManagerID.ToString()+"";
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select count(lead_ID) as TotalCount from student_registration where ManagerCode =" + ManagerID.ToString() + " and AcademicCode=" + AcademicCode.ToString() + "";
        }
        else
        {
            gstrQrystr = "select count(lead_ID) as TotalCount from student_registration where ManagerCode =" + ManagerID.ToString() + "";

        }
        return DLobj.GetDataTable(gstrQrystr);

    }
    public DataTable Manager_GetProjectStatusWiseProjectCount(string ManagerId, string AcademicCode)
    {
        gstrQrystr = "select sum(case when ProjectStatus='Proposed' then 1 else 0 end) as Proposed, ";
        gstrQrystr = gstrQrystr + "sum(case when projectstatus='Approved' then 1 else 0 end) as Approved, ";
        gstrQrystr = gstrQrystr + "sum(case when projectStatus='Completed' then 1 else 0 end) as Completed, ";
        gstrQrystr = gstrQrystr + "sum(case when projectstatus='RequestForModification' then 1 else 0 end) as RequestForModification, ";
        gstrQrystr = gstrQrystr + "sum(case when projectstatus='RequestForCompletion' then 1 else 0 end) as RequestForCompletion, ";
        gstrQrystr = gstrQrystr + "sum(case when ProjectStatus='Rejected' then 1 else 0 end) as Rejected, ";
        gstrQrystr = gstrQrystr + "count(PDId) as TotalProjects ";
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = gstrQrystr + "from project_description as PD inner join student_registration as sr on" + " " +
            "PD.Student_Id=sr.registrationId where ManagerId = " + ManagerId.ToString() + " and PD.AcademicCode=" + AcademicCode.ToString() + " and sr.isProfileEdit=1";
        }
        else
        {
            gstrQrystr = gstrQrystr + "from project_description as PD inner join student_registration as sr on" + " " +
            "PD.student_id=sr.registrationId where ManagerId = " + ManagerId.ToString() + " and sr.isProfileEdit=1";

        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetRequestedAndSanctionAmount(string ManagerId, string AcademicCode)
    {
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select sum(amount) as RequestedAmount,sum(SanctionAmount) as SanctionAmount from project_description as PD inner join student_registration as sr on" + " " +
            "PD.student_id=sr.registrationId where ManagerId=" + ManagerId.ToString() + " and PD.AcademicCode=" + AcademicCode.ToString() + " and sr.isprofileedit=1";
        }
        else
        {
            gstrQrystr = "select sum(amount) as RequestedAmount,sum(SanctionAmount) as SanctionAmount from project_description as PD inner join student_registration as sr on" + " " +
            "PD.student_id=sr.registrationId where ManagerId=" + ManagerId.ToString() + " and sr.isprofileedit=1";

        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Manager_GetReleaseAmount(string ManagerId, string FromDate, string ToDate)
    {
        if ((FromDate.ToString() != "") && (ToDate.ToString() != ""))
        {
            gstrQrystr = "select sum(amount) as ReleaseAmount from project_fund_details where managerid=" + ManagerId.ToString() + " and Date_Format(GivenDate,'%Y-%m-%d') between Date_Format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d') ";
        }
        else
        {
            gstrQrystr = "select sum(amount) as ReleaseAmount from project_fund_details where managerid=" + ManagerId.ToString() + "";

        }
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Manager_GetListingReportData(string FromDate, string ToDate, string StudentType, string ProjectType, string ManagerId, string ProgramId)
    {
        string gstrQrystr = "";
        if (ManagerId == "[All]")
        {
            if (ProjectType == "Registration")
            {
                if ((StudentType == "[All]") && (ManagerId == "[All]"))
                {

                    /*  gstrQrystr= "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " "+
                      "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " "+
                      "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt"+" "+
                      "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '"+FromDate.ToString()+"' and '"+ToDate.ToString()+"' and sr.collegecode = clg.collegeid"+" "+
                      "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";
                */
                    gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as " +
                        "ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate,ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername," + " " +
                        "(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent ,clgprgrm.program_id" + " " +
                        "from student_registration as sr,mstr_state as ms,colleges as clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,college_programs as clgprgrm " + " " +
                        "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.collegecode = clgprgrm.college_id" + " " +
                        "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and mt.Id = sr.TalukaCode ORDER BY lead_id";
                }


                else if ((StudentType == "[All]") && (ManagerId != "[All]"))
                {
                    gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                   "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                   "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,college_programs as clgprgrm" + " " +
                   "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.collegecode = clgprgrm.college_id and sr.managercode=" + ManagerId.ToString() + "" + " " +
                   "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and  clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and mt.Id = sr.TalukaCode ORDER BY lead_id";
                }
                else if ((StudentType != "[All]") && (ManagerId == "[All]"))
                {
                    if (StudentType != "Student")
                    {
                        /*gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
              "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
              "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL" + " " +
              "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and SL.Levels='" + StudentType.ToString() + "' and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
              " and mt.Id = sr.TalukaCode  and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";*/
                        gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
              "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
              "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL , college_programs as clgprgrm" + " " +
              "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid  and clg.collegeid = clgprgrm.college_id  and SL.Levels='" + StudentType.ToString() + "' and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
              " and mt.Id = sr.TalukaCode and  clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "'  and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";
                    }
                    else
                    {
                        /*gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                   "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                   "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt" + " " +
                   "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.managercode=" + ManagerId.ToString() + "" + " " +
                   "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";*/
                        gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                         "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                         "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,college_programs as clgprgrm" + " " +
                         "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and clg.collegeid = clgprgrm.college_id" + " " +
                         "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode  and mt.Id = sr.TalukaCode and  clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "'  ORDER BY lead_id";
                    }

                }
                else if ((StudentType != "[All]") && (ManagerId != "[All]"))
                {
                    if (StudentType != "Student")
                    {
                        /*gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
              "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
              "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL" + " " +
              "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and SL.Levels='" + StudentType.ToString() + "' and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
              " and mt.Id = sr.TalukaCode  and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";*/
                        gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
              "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
              "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL,college_programs as clgprgrm" + " " +
              "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and  sr.managercode=" + ManagerId.ToString() + " and clg.collegeid = clgprgrm.college_id and  SL.Levels='" + StudentType.ToString() + "' and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
              " and mt.Id = sr.TalukaCode  and clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "'  and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";
                    }
                    else
                    {
                        /*gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                   "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                   "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt" + " " +
                   "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.managercode=" + ManagerId.ToString() + "" + " " +
                   "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";*/
                        gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                       "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                       "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,college_programs as clgprgrm" + " " +
                       "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and clg.collegeid = clgprgrm.college_id and sr.managercode=" + ManagerId.ToString() + "" + " " +
                       "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "'and mt.Id = sr.TalukaCode ORDER BY lead_id";
                    }
                }
            }
            else if ((StudentType == "[All]") && (ProjectType == "[All]"))
            {
                // gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate," + " " +
                //"IFNULL(PD.SanctionAmount,0) as ApprovedAmount FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                //"manager_details AS MD ON PD.ManagerId = MD.ManagerId LEFT OUTER JOIN colleges ON SR.CollegeCode = colleges.CollegeId LEFT OUTER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                //"WHERE (date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"Order by PD.Title";

                /*   gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.StudentName,SR.student_type,SR.MobileNo,SR.MailId,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount," + " " +
                      "PD.SanctionAmount AS SanctionAmount," + " " +
                     "(SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                     "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')) as Disperse," + " " +
                     "PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                     "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'))  as Balance," + " " +
                     "PD.ProjectStatus,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate,PD.Project_Levels" + " " +
                     "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                     "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                     "Where  (PD.ProjectStatus = 'Proposed' AND DATE_FORMAT(PD.ProposedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                   "(PD.ProjectStatus = 'Approved' AND DATE_FORMAT(PD.ApprovedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                   "(PD.ProjectStatus = 'Completed'AND DATE_FORMAT(PD.CompletedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                   "(PD.ProjectStatus = 'Rejected' AND DATE_FORMAT(PD.RejectedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                   "(PD.ProjectStatus = 'RequestForCompletion' AND DATE_FORMAT(PD.RequestForCompletionDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                   "(PD.ProjectStatus = 'RequestForModification' AND DATE_FORMAT(PD.RequestForModificationDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                   "(PD.ProjectStatus = 'Draft' AND DATE_FORMAT(PD.Edited_Date, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') " + " " +
                   " and SR.isProfileEdit=1 Order by PD.Title";*/
                gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.StudentName,SR.student_type,SR.MobileNo,SR.MailId,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount," + " " +
                     "PD.SanctionAmount AS SanctionAmount," + " " +
                    "(SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                    "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')) as Disperse," + " " +
                    "PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                    "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'))  as Balance," + " " +
                    "PD.ProjectStatus,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate,PD.Project_Levels" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId  inner join college_programs as clgprgrm on  colleges.CollegeId = clgprgrm.college_id" + " " +
                    "Where   colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and ((PD.ProjectStatus = 'Proposed' AND DATE_FORMAT(PD.ProposedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                  "(PD.ProjectStatus = 'Approved' AND DATE_FORMAT(PD.ApprovedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                  "(PD.ProjectStatus = 'Completed'AND DATE_FORMAT(PD.CompletedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                  "(PD.ProjectStatus = 'Rejected' AND DATE_FORMAT(PD.RejectedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                  "(PD.ProjectStatus = 'RequestForCompletion' AND DATE_FORMAT(PD.RequestForCompletionDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                  "(PD.ProjectStatus = 'RequestForModification' AND DATE_FORMAT(PD.RequestForModificationDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                  "(PD.ProjectStatus = 'Draft' AND DATE_FORMAT(PD.Edited_Date, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')) " + " " +
                  " and  SR.isProfileEdit=1 Order by PD.Title";



                //"WHERE (date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and (Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //"OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))) and SR.isProfileEdit=1" + " " +
                //"Order by PD.Title";


                //  "order by Date_format(PD.Created_Date,'%d-%m-%Y %h:%i:%s') desc";
            }
            else if ((ProjectType == "[All]") && (StudentType != "[All]"))
            {
                string temp = "";
                if (StudentType == "Student")
                {
                    temp = " and(SR.Student_Type = '" + StudentType.ToString() + "')";
                }
                else
                {
                    temp = " and(PD.Project_Levels = '" + StudentType.ToString() + "')";
                }
                /*  gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender," + " " +
                  "SR.StudentName,SR.student_type,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate," + " " +
                  "PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName," + " " +
                  "sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate," + " " +
                  "date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate," + " " +
                  "IFNULL(PD.SanctionAmount,0) as ApprovedAmount FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                  "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON" + " " +
                  "SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and (Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                   "OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                   "OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                   "OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                    "OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                    "OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))) " + " " +
                     "and (SR.isProfileEdit=1) " + " " +
                    temp.ToString() + " " +
                    // " and (SR.Student_Type='" + StudentType.ToString() + "')" + " " +
                    " Order by PD.Title";*/
                gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender," + " " +
                  "SR.StudentName,SR.student_type,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate," + " " +
                  "PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName," + " " +
                  "sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate," + " " +
                  "date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate," + " " +
                  "IFNULL(PD.SanctionAmount,0) as ApprovedAmount FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                  "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON" + " " +
                  "SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on  SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE (date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and (Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                   "OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                   "OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                   "OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                    "OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                    "OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))) " + " " +
                     "and (SR.isProfileEdit=1) and  clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' " + " " +
                    temp.ToString() + " " +
                    // " and (SR.Student_Type='" + StudentType.ToString() + "')" + " " +
                    " Order by PD.Title";
            }

            else if (ProjectType == "Proposed")
            {
                if (StudentType == "[All]")
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') order by Date(PD.proposedDate) desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                        "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                        "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                        "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') order by Date(PD.proposedDate) desc";
                }
                else
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date(PD.proposedDate) desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                       "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                       "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                       "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date(PD.proposedDate) desc";
                }

            }
            else if (ProjectType == "Approved")
            {
                if (StudentType == "[All]")
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount ,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount ,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                        "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                        "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId  inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                        "WHERE clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and  (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and  (SR.student_type='" + StudentType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE  clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (PD.ProjectStatus = '" + ProjectType.ToString() + "') and  (SR.student_type='" + StudentType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";

                }
            }
            else if (ProjectType == "Completed")
            {
                if (StudentType == "[All]")
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y') ,date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y') ,date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE  clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1'  and (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y') ,date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y') ,date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                       "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                       "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                       "WHERE  clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1'  and (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
            else if (ProjectType == "RequestForModification")
            {
                if (StudentType == "[All]")
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                         "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                         "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                         "WHERE  clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1'  and (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                       "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                       "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                       "WHERE  clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1'  and (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
            else if (ProjectType == "RequestForCompletion")
            {
                if (StudentType == "[All]")
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date(PD.RequestForCompletionDate) desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                        "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                        "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                        "WHERE clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and  (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date(PD.RequestForCompletionDate) desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date(PD.RequestForCompletionDate) desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                      "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                      "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                      "WHERE clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date(PD.RequestForCompletionDate) desc";
                }
            }

            else if (ProjectType == "Rejected")
            {
                if (StudentType == "[All]")
                {
                    /*  gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                     "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                     "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                     "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                     "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                     "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                     "WHERE clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "')  and (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                      "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                      "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                      "WHERE clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "')  and (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
            else if (ProjectType == "Impact")
            {
                if (StudentType == "[All]")
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,Date_Format(PD.RequestForCompletionDate,'%d%m-%Y') as RequestForCompletionDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname," + " " +
                    "date_format(PD.ImpactDate,'%d-%m-%Y')  as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities,PD.Experience_Of_Initiative,PD.Lacking_initiative,PD.Against_Tide," + " "+
                    "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " "+
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.isImpact_Project = 1) and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,Date_Format(PD.RequestForCompletionDate,'%d%m-%Y') as RequestForCompletionDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname," + " " +
                    "date_format(PD.ImpactDate,'%d-%m-%Y')  as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities,PD.Experience_Of_Initiative,PD.Lacking_initiative,PD.Against_Tide," + " " +
                    "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (PD.isImpact_Project = 1) and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y') as RequestForCompletionDate ,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname," + " " +
                      "date_format(PD.ImpactDate,'%d-%m-%Y') as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities,PD.Experience_Of_Initiative,PD.Lacking_initiative,PD.Against_Tide," + " " +
                     "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.isImpact_Project =1) AND (SR.student_type ='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y') as RequestForCompletionDate ,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname," + " " +
                         "date_format(PD.ImpactDate,'%d-%m-%Y') as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities,PD.Experience_Of_Initiative,PD.Lacking_initiative,PD.Against_Tide," + " " +
                        "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " " +
                       "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                       "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                       "WHERE clgprgrm.program_id= '" + ProgramId.ToString() + "' and colleges.status='1' and (PD.isImpact_Project =1) AND (SR.student_type ='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
        }
        else
        {
            if (ProjectType == "Registration")
            {



                if ((StudentType == "[All]") && (ManagerId == "[All]"))
                {

                    /* gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                     "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                     "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt" + " " +
                     "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid" + " " +
                     "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";*/
                    gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as " +
                       "ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate,ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername," + " " +
                       "(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent ,clgprgrm.program_id" + " " +
                       "from student_registration as sr,mstr_state as ms,colleges as clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,college_programs as clgprgrm " + " " +
                       "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.collegecode = clgprgrm.college_id" + " " +
                       "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and mt.Id = sr.TalukaCode ORDER BY lead_id";
                }
                else if ((StudentType == "[All]") && (ManagerId != "[All]"))
                {
                    /*   gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                      "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                      "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt" + " " +
                      "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.managercode=" + ManagerId.ToString() + "" + " " +
                      "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";*/
                    gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                    "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                    "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,college_programs as clgprgrm" + " " +
                    "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.collegecode = clgprgrm.college_id and sr.managercode=" + ManagerId.ToString() + "" + " " +
                    "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and clgprgrm.program_id= '" + ProgramId.ToString() + "' and clg.status='1' and mt.Id = sr.TalukaCode ORDER BY lead_id";
                }
                else if ((StudentType != "[All]") && (ManagerId == "[All]"))
                {
                    if (StudentType != "Student")
                    {
                        /*gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
              "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
              "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL" + " " +
              "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and SL.Levels='" + StudentType.ToString() + "' and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
              " and mt.Id = sr.TalukaCode  and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";*/
                        gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL , college_programs as clgprgrm" + " " +
                "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid  and clg.collegeid = clgprgrm.college_id  and SL.Levels='" + StudentType.ToString() + "' and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
                " and mt.Id = sr.TalukaCode clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "'  and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";
                    }
                    else
                    {
                        /* gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                    "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                    "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt" + " " +
                    "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.managercode=" + ManagerId.ToString() + "" + " " +
                    "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";*/
                        gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                   "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                   "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt ,college_programs as clgprgrm" + " " +
                   "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and clg.collegeid = clgprgrm.college_id and sr.managercode=" + ManagerId.ToString() + "" + " " +
                   "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and mt.Id = sr.TalukaCode ORDER BY lead_id";
                    }

                }
                else if ((StudentType != "[All]") && (ManagerId != "[All]"))
                {
                    if (StudentType != "Student")
                    {
                        /*gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
              "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
              "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL" + " " +
              "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and SL.Levels='" + StudentType.ToString() + "' and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
              " and mt.Id = sr.TalukaCode  and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";*/
                        gstrQrystr = "select SR.lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
              "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
              "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,student_levels as SL,college_programs as clgprgrm" + " " +
              "where sr.statecode = ms.code and DATE(SL.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and clg.collegeid = clgprgrm.college_id " +
              "and  SL.Levels='" + StudentType.ToString() + "' and  sr.managercode=" + ManagerId.ToString() + " " +
              " and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode " + " " +
              " and mt.Id = sr.TalukaCode and clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' " +
              " and SR.lead_Id=SL.Lead_Id ORDER BY SR.lead_id";
                    }
                    else
                    {
                        /*gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                   "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                   "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt" + " " +
                   "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and sr.managercode=" + ManagerId.ToString() + "" + " " +
                   "and sr.managercode = md.ManagerId and mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";*/
                        gstrQrystr = "select lead_id,studentname,sr.mobileno,sr.mailid,sr.gender,student_type,Date_Format(sr.DOB,'%d-%m-%Y') as DOB,Date_Format(sr.RegistrationDate,'%d-%m-%Y') as ProfileUpdateDate,Date_format(sr.edited_date,'%d-%m-%Y') as LastEditedDate," + " " +
                   "ms.statename,mdtl.DistrictName,mt.Taluk_Name,College_Name,md.managername,(case when sr.isFeePaid=1 then 'Paid' else 'UnPaid' end) as IsFeesPaid,sr.MyTalent" + " " +
                   "from student_registration as sr,mstr_state as ms,colleges clg, manager_details as md,mstr_district as mdtl,mstr_taluka as mt,college_programs as clgprgrm" + " " +
                   "where sr.statecode = ms.code and DATE(sr.RegistrationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' and sr.collegecode = clg.collegeid and clg.collegeid = clgprgrm.college_id and  sr.managercode=" + ManagerId.ToString() + "" + " " +
                   "and sr.managercode = md.ManagerId and clg.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and  mdtl.DistrictId = sr.DistrictCode and mt.Id = sr.TalukaCode ORDER BY lead_id";
                    }
                }
            }
            else if ((StudentType == "[All]") && (ProjectType == "[All]"))
            {
                /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,SR.MobileNo,SR.MailId,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount," + " " +
                 "PD.SanctionAmount AS SanctionAmount," + " " +
                "(SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')) as Disperse," + " " +
                "PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'))  as Balance," + " " +
                "PD.ProjectStatus,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate" + " " +
                "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   //  "WHERE and (date_Format(PD.Created_Date,'%Y-%m-%d') between Date_Format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d')) order by Date_Format(PD.Created_Date,'%d-%m-%Y %h:%i:%s') desc";

                   "Where (PD.ManagerId = " + ManagerId.ToString() + ") and ((PD.ProjectStatus = 'Proposed' AND DATE_FORMAT(PD.ProposedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                "(PD.ProjectStatus = 'Approved' AND DATE_FORMAT(PD.ApprovedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'Completed'AND DATE_FORMAT(PD.CompletedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                "(PD.ProjectStatus = 'Rejected' AND DATE_FORMAT(PD.RejectedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'RequestForCompletion' AND DATE_FORMAT(PD.RequestForCompletionDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'RequestForModification' AND DATE_FORMAT(PD.RequestForModificationDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'Draft' AND DATE_FORMAT(PD.Edited_Date, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') " + " " +
                " and SR.isProfileEdit=1) Order by PD.Title";*/

                gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,SR.MobileNo,SR.MailId,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount," + " " +
                 "PD.SanctionAmount AS SanctionAmount," + " " +
                "(SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')) as Disperse," + " " +
                "PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
                "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'))  as Balance," + " " +
                "PD.ProjectStatus,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate" + " " +
                "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId  INNER JOIN college_programs AS clgprgrm ON colleges.CollegeId = clgprgrm.college_id" + " " +
                   "Where clgprgrm.program_id= '" + ProgramId.ToString() + "' and  colleges.status='1' and (PD.ManagerId = " + ManagerId.ToString() + ") and ((PD.ProjectStatus = 'Proposed' AND DATE_FORMAT(PD.ProposedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                "(PD.ProjectStatus = 'Approved' AND DATE_FORMAT(PD.ApprovedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'Completed'AND DATE_FORMAT(PD.CompletedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') OR " + " " +
                "(PD.ProjectStatus = 'Rejected' AND DATE_FORMAT(PD.RejectedDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'RequestForCompletion' AND DATE_FORMAT(PD.RequestForCompletionDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'RequestForModification' AND DATE_FORMAT(PD.RequestForModificationDate, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') or " + " " +
                "(PD.ProjectStatus = 'Draft' AND DATE_FORMAT(PD.Edited_Date, '%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "') " + " " +
                " and  SR.isProfileEdit=1) Order by PD.Title";

                //"WHERE (PD.ManagerId = " + ManagerId.ToString() + ")  and ((date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //  "OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //  "OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //  "OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //  "OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                //  "OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))) and SR.isProfileEdit=1" + " " +
                //  "Order by PD.Title";

            }
            else if ((ProjectType == "[All]") && (StudentType != "[All]"))
            {
                string temp = "";
                if (StudentType == "Student")
                {
                    temp = " and(SR.Student_Type = '" + StudentType.ToString() + "')";
                }
                else
                {
                    temp = " and(PD.Project_Levels = '" + StudentType.ToString() + "')";
                }
                /*   gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Student_Type,SR.Gender, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ")  and ((date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                     "OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                     "OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                     "OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                     "OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                     "OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d')))"+" "+
                     "and (SR.isProfileEdit=1) " + " " +
                     temp.ToString() + " " +
                    // " and (SR.Student_Type='" + StudentType.ToString() + "')" + " " +
                     " Order by PD.Title";*/
                gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Student_Type,SR.Gender, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate" + " " +
                "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId  inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                "WHERE (PD.ManagerId = " + ManagerId.ToString() + ")  and ((date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                  "OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                  "OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                  "OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                  "OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                  "OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d')))" + " " +
                  "and  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (SR.isProfileEdit=1) " + " " +
                  temp.ToString() + " " +
                  // " and (SR.Student_Type='" + StudentType.ToString() + "')" + " " +
                  " Order by PD.Title";
                //"WHERE (PD.ManagerId = " + ManagerId.ToString() + ")"+" "+
                //"and (date(PD.Created_Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')"+" "+
                //"and (SR.Student_Type='" + StudentType.ToString() + "') order by Date_Format(PD.Created_Date,'%d-%m-%Y %h:%i:%s') desc";
            }

            else if (ProjectType == "Proposed")
            {
                if (StudentType == "[All]")
                {
                    /*  gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,Student_Type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                     "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                     "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                     "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.proposedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,Student_Type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.proposedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,Student_Type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "')  and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') order by Date_Format(PD.proposedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,Student_Type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                        "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN " + " " +
                        "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                        "WHERE  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "')  and (date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') order by Date_Format(PD.proposedDate,'%d-%m-%Y %h:%i:%s') desc";
                }

            }
            else if (ProjectType == "Approved")
            {
                if (StudentType == "[All]")
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount ,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount ,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,sem.semname" + " " +
                       "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                       "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                       "WHERE  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ManagerId = " + ManagerId.ToString() + ")  AND  (SR.student_type='" + StudentType.ToString() + "') and (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND(PD.Project_Levels = '" + StudentType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,SR.MobileNo,SR.MailId,sem.semname" + " " +
                       "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                       "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                       "WHERE  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ")  AND  (SR.student_type='" + StudentType.ToString() + "') and (PD.ProjectStatus = '" + ProjectType.ToString() + "') AND(PD.Project_Levels = '" + StudentType.ToString() + "') and (date(PD.ApprovedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_format(PD.ApprovedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
            else if (ProjectType == "Completed")
            {
                if (StudentType == "[All]")
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y'),date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y'),date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                        "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                        "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                        "WHERE colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND (PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y'),date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y'),date_format(PD.CompletedDate,'%d-%m-%Y'),PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                       "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                       "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                       "WHERE  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
            else if (ProjectType == "RequestForModification")
            {
                if (StudentType == "[All]")
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type = '" + StudentType.ToString() + "')  and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,PD.ManagerComments,date_format(PD.RequestForModificationDate,'%d-%m-%Y') as RequestForModificationDate,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE  colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') AND (SR.student_type = '" + StudentType.ToString() + "')  and (date(PD.RequestForModificationDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.RequestForModificationDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
            else if (ProjectType == "RequestForCompletion")
            {
                if (StudentType == "[All]")
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.RequestForCompletionDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_Format(PD.RequestForCompletionDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "')  and  (SR.student_type='" + StudentType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RequestForCompletionDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,PD.RequestForCompletionDate,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "')  and  (SR.student_type='" + StudentType.ToString() + "') and (date(PD.RequestForCompletionDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RequestForCompletionDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }

            else if (ProjectType == "Rejected")
            {
                if (StudentType == "[All]")
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "') and (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "')  AND (SR.student_type='" + StudentType.ToString() + "') and  (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate,PD.Title,IFNULL(PD.Amount,0) as RequestedAmount, PD.ProjectStatus,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.ProjectStatus = '" + ProjectType.ToString() + "')  AND (SR.student_type='" + StudentType.ToString() + "') and  (date(PD.RejectedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.RejectedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
            else if (ProjectType == "Impact")
            {
                if (StudentType == "[All]")
                {
                    /* gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y')as RequestForCompletionDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments, SR.MobileNo, SR.MailId, sem.semname,date_format(PD.ImpactDate,'%d-%m-%Y') as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities, PD.Experience_Of_Initiative, PD.Lacking_initiative,PD.Against_Tide," + " " +
                    "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " " +
                    "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                    "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                    "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.isImpact_Project = 1) and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.student_type,SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName,date_format(PD.RequestForCompletionDate,'%d-%m-%Y')as RequestForCompletionDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments, SR.MobileNo, SR.MailId, sem.semname,date_format(PD.ImpactDate,'%d-%m-%Y') as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities, PD.Experience_Of_Initiative, PD.Lacking_initiative,PD.Against_Tide," + " " +
                   "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on SR.CollegeCode = clgprgrm.college_id" + " " +
                   "WHERE colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.isImpact_Project = 1) and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
                else
                {
                    /*gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName, date_format(PD.RequestForCompletionDate,'%d-%m-%Y') as RequestForCompletionDate, date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname," + " " +
                    "date_format(PD.ImpactDate,'%d-%m-%Y') as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities,PD.Experience_Of_Initiative,PD.Lacking_initiative,PD.Against_Tide," + " " +
                   "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
                   "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.isImpact_Project =1) AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";*/
                    gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender,SR.student_type, SR.StudentName,colleges.College_Name, date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate,PD.Title, IFNULL(PD.Amount,0) as RequestedAmount,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,IFNULL(PD.SanctionAmount,0) as ApprovedAmount, PD.ProjectStatus,MD.ManagerName, date_format(PD.RequestForCompletionDate,'%d-%m-%Y') as RequestForCompletionDate, date_format(PD.CompletedDate,'%d-%m-%Y') as CompletionDate, PD.Rating,PD.ManagerComments,SR.MobileNo,SR.MailId,sem.semname," + " " +
                    "date_format(PD.ImpactDate,'%d-%m-%Y') as ImpactDate, PD.Collaboration_Supported,PD.Permission_And_Activities,PD.Experience_Of_Initiative,PD.Lacking_initiative,PD.Against_Tide," + " " +
                   "PD.Cross_Hurdles,PD.Entrepreneurial_Venture,PD.Government_Awarded,PD.Leadership_Roles" + " " +
                   "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
                   "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId inner join college_programs as clgprgrm on colleges.CollegeId = clgprgrm.college_id" + " " +
                   "WHERE colleges.status='1' and clgprgrm.program_id= '" + ProgramId.ToString() + "' and  (PD.ManagerId = " + ManagerId.ToString() + ") AND(PD.isImpact_Project =1) AND (SR.student_type='" + StudentType.ToString() + "') and (date(PD.CompletedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')  order by Date_format(PD.CompletedDate,'%d-%m-%Y %h:%i:%s') desc";
                }
            }
        }
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }

    public DataTable Manager_GetListingReportFolderWise(string FromDate, string ToDate, string ManagerId, string ProjectStatus)
    {
        string gstrQrystr = "";
        if (ManagerId != "[All]")
        {
            gstrQrystr = "SELECT Distinct SR.Lead_Id,SR.StudentName,colleges.College_Name,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
            "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
            "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
            "WHERE (PD.ManagerId = " + ManagerId.ToString() + ") AND (PD.ProjectStatus = '" + ProjectStatus.ToString() + "') and (date(PD." + ProjectStatus.ToString() + "Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD." + ProjectStatus.ToString() + "date,'%d-%m-%Y %h:%i:%s') desc";
        }
        else
        {
            gstrQrystr = "SELECT Distinct SR.Lead_Id,SR.StudentName,colleges.College_Name,SR.MobileNo,SR.MailId,MD.ManagerName,sem.semname" + " " +
            "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
            "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
            "WHERE (PD.ProjectStatus = 'Completed') AND (PD.ProjectStatus = '" + ProjectStatus.ToString() + "') and (date(PD." + ProjectStatus.ToString() + "Date) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')   order by Date_Format(PD." + ProjectStatus.ToString() + "date, '%d-%m-%Y %h:%i:%s') desc";
        }

        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt;
    }
    public string Common_ReturnFolderDownload(string LeadId)
    {
        gstrQrystr = "Select IsFolderDownload from Student_Registration where Lead_Id='" + LeadId.ToString() + "'";
        DataTable dt = DLobj.GetDataTable(gstrQrystr);
        return dt.Rows[0].ItemArray[0].ToString();
    }
    public void Admin_FillManagerddl(DropDownList ddl, string p_userid)
    {
        // gstrQrystr = "select distinct ManagerId,managername from manager_details where isApplicableforMail=1 order by managername";
        gstrQrystr = "select distinct ManagerId,managername from manager_details as md inner join manager_colleges as mc on  md.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id inner join user_programs as up on cp.program_id = up.program_id where isApplicableforMail=1 and up.user_id =" + p_userid.ToString() + " order by managername";

        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "ManagerId", "ManagerName");
    }
    public void Admin_FillManagerWithSelect(DropDownList ddl, string p_userid)
    {
        // gstrQrystr = "select distinct ManagerId,managername from manager_details where isApplicableforMail=1 order by managername";
        gstrQrystr = "select distinct ManagerId,managername from manager_details as md inner join manager_colleges as mc on  md.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id inner join user_programs as up on cp.program_id = up.program_id where isApplicableforMail=1 and up.user_id =" + p_userid.ToString() + " order by managername";
        DLobj.FillDDLWithSelect(ddl, gstrQrystr, "ManagerId", "ManagerName");
    }

    public void Admin_Fillprogramddl(DropDownList ddl, string p_userid)
    {
        gstrQrystr = "select master_programs.program_id,program_name from master_programs inner join user_programs as up on(master_programs.program_id = up.program_id) where  up.user_id = " + p_userid.ToString() + " ";
        DLobj.FillDDL(ddl, gstrQrystr, "program_id", "program_name");
    }

    public void Admin_FillManagerByprogram(string P_programId, DropDownList ddl)
    {
        /*        gstrQrystr = "select distinct ManagerId,managername from manager_details as md inner join manager_colleges as mc on  md.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id where isApplicableforMail=1 and  cp.program_id = "+ P_programId.ToString()  + " order by managername";
        */
        gstrQrystr = "select distinct ManagerId,managername,md.program_Id from manager_details as md inner join manager_colleges as mc on  md.ManagerId = mc.ManagerCode  inner join college_programs as cp on mc.CollegeCode = cp.college_id where isApplicableforMail=1 and  md.program_id =" + P_programId.ToString() + " order by managername";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "ManagerId", "ManagerName");
    }

    public void Admin_FillManagerList(Repeater rpt, string P_userid)
    {
        // gstrQrystr = "select ManagerId,ManagerName,mstr_state.StateName from manager_details" + " " +
        // "inner join mstr_state on manager_details.StateCode = mstr_state.code where mstr_state.Status = 1 and manager_details.Status = 1 and manager_details.isApplicableforMail=1 order by ManagerName asc";

        gstrQrystr = "select distinct ManagerId,ManagerName,mstr_state.StateName from manager_details inner join mstr_state on manager_details.StateCode = mstr_state.code inner join manager_colleges as mc on  manager_details.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id inner join user_programs as up on cp.program_id = up.program_id where mstr_state.Status = 1 and manager_details.Status = 1 and manager_details.isApplicableforMail=1 and up.user_id = " + P_userid.ToString() + " order by ManagerName asc";
        rpt.DataSource = DLobj.GetDataTable(gstrQrystr);
        rpt.DataBind();
    }

    public DataTable GetProjectDetailForDocument(string PDId)
    {
        gstrQrystr = "select pdid,SR.lead_id,SR.studentname,title,SR.MobileNo,beneficiaryno,beneficiaries," + " " +
        "placeofimplement,currentsituation,objectives,actionplan,managercomments,amount,sanctionamount," + " " +
        "projectstatus,challenge,learning,AsAstory,Date_format(Proposeddate,'%y-%m-%d') as Proposeddate," + " " +
        "Date_format(ApprovedDate,'%y-%m-%d') as ApprovedDate,Date_format(CompletedDate,'%y-%m-%d') as CompletedDate," + " " +
        "Date_format(RejectedDate,'%y-%m-%d') as RejectedDate,Date_format(RequestForModificationDate,'%y-%m-%d') as RequestForModificationDate," + " " +
        "rating,isImpact_Project,date_format(ImpactDate,'%d-%m-%Y') as ImpactDate,Collaboration_Supported,Permission_And_Activities,Experience_Of_Initiative," + " " +
        "Lacking_initiative,Against_Tide,Cross_Hurdles,Entrepreneurial_Venture,Government_Awarded,Leadership_Roles,PD.Resource,ifnull(PD.TotalResourses,0) as TotalResourses," + " " +
        "ifnull(PD.HoursSpend,0) as HoursSpend,PD.Project_Levels,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate," + " " +
        "date_format(PD.ProjectStartDate,'%d-%m-%Y') as ProjectStartDate,date_format(PD.ProjectEndDate,'%d-%m-%Y') as ProjectEndDate" + " " +
        "from project_description as PD inner join" + " " +
        "Student_Registration as SR on PD.Lead_Id = SR.Lead_Id where pdid = " + PDId.ToString() + "";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable GetSDG_GoalsForDoc(string pdid)
    {
        gstrQrystr = "select Goals from sdg_goals as SG, project_sdg_details as PSD" + " " +
        "where PSD.PDID = " + pdid + " and SG.slno = PSD.Sdg_Id";
        return DLobj.GetDataTable(gstrQrystr);
    }

    public void CreateWordDocuments(string PDId, string targetPath, string MainFolder, string filename)
    {

        var doc = Xceed.Words.NET.DocX.Create(filename);
        DataTable dt = GetProjectDetailForDocument(PDId.ToString());
        DataTable dtGoal = GetSDG_GoalsForDoc(PDId.ToString());
        if (dt.Rows.Count > 0)
        {
            //Title  
            string title = "Title : " + " " + dt.Rows[0].ItemArray[3].ToString();

            //Text  
            string textParagraph = "1] Student Name :" + dt.Rows[0].ItemArray[2].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "2] Mobile No :" + dt.Rows[0].ItemArray[4].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "3] Title :" + dt.Rows[0].ItemArray[3].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "4] Objectives :" + dt.Rows[0].ItemArray[9].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "5] Total Beneficiaries :" + dt.Rows[0].ItemArray[5].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "6] Place of Implementation :" + dt.Rows[0].ItemArray[7].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "7] Requested Amount :" + dt.Rows[0].ItemArray[12].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "8] Approved Amount :" + dt.Rows[0].ItemArray[13].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "9] Challenges Faced During Project : " + dt.Rows[0].ItemArray[15].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "10] learning from this project : " + dt.Rows[0].ItemArray[16].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "11] Your project as a Story : " + dt.Rows[0].ItemArray[17].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "12] Manager Comments  : " + dt.Rows[0].ItemArray[11].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "13] Rating  : " + dt.Rows[0].ItemArray[23].ToString() + Environment.NewLine;
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "14] Project Status :" + dt.Rows[0].ItemArray[14].ToString() + Environment.NewLine;

            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "15] Resources  : " + dt.Rows[0].ItemArray[35].ToString() + Environment.NewLine;

            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "16] Resources Utilized Amount  : " + dt.Rows[0].ItemArray[36].ToString() + Environment.NewLine;

            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "17] Hours Spent  : " + dt.Rows[0].ItemArray[37].ToString() + Environment.NewLine;

            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "18] Project Level  : " + dt.Rows[0].ItemArray[38].ToString() + Environment.NewLine;

            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "19] Project Start Date  : " + dt.Rows[0].ItemArray[40].ToString() + Environment.NewLine;

            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "20] Project End Date  : " + dt.Rows[0].ItemArray[41].ToString() + Environment.NewLine;

            if (dtGoal.Rows.Count > 0)
            {
                textParagraph = textParagraph + Environment.NewLine;

                textParagraph = textParagraph + "21] SDG Goals :" + Environment.NewLine;

                int j = 1;
                textParagraph = textParagraph + "-------------------------------------------------------------" + Environment.NewLine;
                for (int i = 0; i < dtGoal.Rows.Count; i++)
                {

                    textParagraph = textParagraph + Environment.NewLine;
                    textParagraph = textParagraph + "21." + j + " " + dtGoal.Rows[i].ItemArray[0].ToString() + Environment.NewLine;
                    j++;
                }
                textParagraph = textParagraph + "-------------------------------------------------------------" + Environment.NewLine;

            }
            textParagraph = textParagraph + Environment.NewLine;
            textParagraph = textParagraph + "22] Project Completed Date  : " + dt.Rows[0].ItemArray[39].ToString() + Environment.NewLine;
            if (dt.Rows[0].ItemArray[24].ToString() == "1")
            {
                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "23] Impacted_Date  : " + dt.Rows[0].ItemArray[25].ToString() + Environment.NewLine;


                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "24] Collaboration_Supported  : " + dt.Rows[0].ItemArray[26].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "25] Permission_And_Activities  : " + dt.Rows[0].ItemArray[27].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "26] Experience_Of_Initiative  : " + dt.Rows[0].ItemArray[28].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "27] Lacking_initiative  : " + dt.Rows[0].ItemArray[29].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "28] Against_Tide  : " + dt.Rows[0].ItemArray[30].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "29] Cross_Hurdles  : " + dt.Rows[0].ItemArray[31].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "30] Entrepreneurial_Venture  : " + dt.Rows[0].ItemArray[32].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "31] Government_Awarded  : " + dt.Rows[0].ItemArray[33].ToString() + Environment.NewLine;

                textParagraph = textParagraph + Environment.NewLine;
                textParagraph = textParagraph + "32] Leadership_Roles  : " + dt.Rows[0].ItemArray[34].ToString() + Environment.NewLine;
            }

            //Formatting Title  
            Xceed.Words.NET.Formatting titleFormat = new Xceed.Words.NET.Formatting();
            //Specify font family  
            titleFormat.FontFamily = new Xceed.Words.NET.Font("arial");
            //Specify font size  
            titleFormat.Size = 24D;
            titleFormat.Position = 40;
            titleFormat.FontColor = System.Drawing.Color.Black;

            titleFormat.Italic = false;
            titleFormat.Bold = true;

            //Formatting Text Paragraph  
            Xceed.Words.NET.Formatting textParagraphFormat = new Xceed.Words.NET.Formatting();
            //font family  
            textParagraphFormat.FontFamily = new Xceed.Words.NET.Font("arial");
            //font size  
            textParagraphFormat.Size = 10D;
            //Spaces between characters  
            textParagraphFormat.Spacing = 3;

            Xceed.Words.NET.Paragraph paragraphTitle = doc.InsertParagraph(title, false, titleFormat);
            paragraphTitle.Alignment = Xceed.Words.NET.Alignment.center;
            //Insert text  
            doc.InsertParagraph(textParagraph, false, textParagraphFormat);
            //doc.InsertParagraph("Hello Word");
            doc.Save();
        }
    }
    public DataTable GetSandboxDetails()
    {
        gstrQrystr = "SELECT DISTINCT Sandbox from manager_details where isApplicableforMail=1 order by SandboxPriority";
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable GetSandboxDetailsTshirt()
    {
        gstrQrystr = "SELECT DISTINCT Sandbox from manager_details where isApplicableForTshirt=1 order by SandboxPriority";
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable FillManagerSandboxWise(string Sandbox, string P_userid, string P_programid)
    {
        //gstrQrystr = "SELECT DISTINCT ManagerId,ManagerName,Sandbox from manager_details where isApplicableforMail=1 and sandbox='" + Sandbox.ToString()+"' order by managerid ";
        gstrQrystr = "SELECT DISTINCT ManagerId,ManagerName,Sandbox from manager_details  inner join manager_colleges as mc on  manager_details.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id inner join user_programs as up on cp.program_id = up.program_id  where isApplicableforMail=1 and sandbox='" + Sandbox.ToString() + "' and up.user_id ='" + P_userid.ToString() + "' and up.program_id= '" + P_programid.ToString() + "'  order by managerid ";

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable FillManagerSandboxWiseTshirt(string Sandbox, string P_userid, string P_programid)
    {
        // gstrQrystr = "SELECT DISTINCT ManagerId,ManagerName,Sandbox from manager_details where isApplicableForTshirt=1 and sandbox='" + Sandbox.ToString() + "' order by managerid ";
        gstrQrystr = "SELECT DISTINCT ManagerId,ManagerName,Sandbox from manager_details  inner join manager_colleges as mc on  manager_details.ManagerId = mc.ManagerCode inner join college_programs as cp on mc.CollegeCode = cp.college_id inner join user_programs as up on cp.program_id = up.program_id  where isApplicableforMail=1 and sandbox='" + Sandbox.ToString() + "' and up.user_id ='" + P_userid.ToString() + "' and up.program_id= '" + P_programid.ToString() + "' order by managerid ";
        return DLobj.GetDataTable(gstrQrystr);
    }

    /*public DataTable GetSandboxWiseProjectDetailsTOP(string Sandbox, string P_programid)
    {
        *//*gstrQrystr = "select MD.sandbox,count(Pdid) as PDID,PD.ProjectStatus from project_description as PD inner join manager_details as MD on PD.managerid=MD.ManagerId where MD.sandbox='"+Sandbox.ToString()+"' " + " "+
        "group by sandbox,PD.ProjectStatus order by MD.SandboxPriority,PD.ProjectStatus";*//*

        gstrQrystr = "select MD.Sandbox,count(Pdid) as PDID,PD.ProjectStatus from project_description as PD  inner join manager_details as MD on PD.managerid=MD.ManagerId inner join student_registration as sr  on PD.Student_Id = sr.RegistrationId inner join colleges as cs on sr.CollegeCode = cs.CollegeId inner join college_programs as cp on cs.CollegeId = cp.college_id where  cp.program_id = '" + P_programid.ToString() + "' and MD.Sandbox = '" + Sandbox.ToString() + "' group by sandbox,PD.ProjectStatus order by MD.SandboxPriority,PD.ProjectStatus";
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandTimeout = 600;
        return DLobj.GetDataTable(gstrQrystr);
    }*/

    public DataTable GetSandboxWiseProjectDetailsTOP(string P_programid)
    {
        gstrQrystr = "select MD.Sandbox,count(Pdid) as PDID,PD.ProjectStatus from project_description as PD  inner join manager_details as MD on PD.managerid=MD.ManagerId inner join student_registration as sr  on PD.Student_Id = sr.RegistrationId inner join colleges as cs on sr.CollegeCode = cs.CollegeId inner join college_programs as cp on cs.CollegeId = cp.college_id where  cp.program_id = '" + P_programid.ToString() + "'  group by sandbox,PD.ProjectStatus order by MD.SandboxPriority,PD.ProjectStatus";
        return DLobj.GetDataTable(gstrQrystr);
    }


    /* public DataTable GetSandboxManagerWiseProjectDetails(string ManagerId)
     {
         gstrQrystr = "select MD.sandbox,count(Pdid) as PDID,PD.ProjectStatus,MD.ManagerId from project_description as PD inner join manager_details as MD on PD.managerid=MD.ManagerId where (MD.ManagerId="+ManagerId.ToString()+")  group by sandbox,PD.ProjectStatus order by MD.SandboxPriority,PD.ProjectStatus";
         return DLobj.GetDataTable(gstrQrystr);
     }*/

    public DataTable GetSandboxManagerWiseProjectDetails(string P_programid, string P_ManagerId)
    {
        gstrQrystr = "select MD.sandbox,count(Pdid) as PDID,PD.ProjectStatus,MD.ManagerId from project_description as PD inner join manager_details as MD on PD.managerid = MD.ManagerId inner join student_registration as sr  on PD.Student_Id = sr.RegistrationId inner join colleges as cs on sr.CollegeCode = cs.CollegeId inner join college_programs as cp on cs.CollegeId = cp.college_id where cp.program_id = '" + P_programid.ToString() + "'  and(MD.ManagerId = '" + P_ManagerId.ToString() + "')  group by sandbox,PD.ProjectStatus order by MD.SandboxPriority,PD.ProjectStatus";
        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Admin_GetManagerWiseTshirtStockDetails(string AcademicCode, string ManagerId)
    {
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select MD.ManagerId,MD.ManagerName,Sandbox,AllotedS,UsedS,AllotedM,UsedM,AllotedL,UsedL,AllotedXL,UsedXL,AllotedXXL,UsedXXL," + " " +
            "(select sum(case when SanctionStatus=0  then 1 else 0 end) as RequestCount from student_tshirt_allotment where AcademicCode=" + AcademicCode.ToString() + " and managerid=" + ManagerId.ToString() + ") as RequestCount" + " " +
           "from manager_tshirt as MT" + " " +
            "Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId where MT.AcademicCode = " + AcademicCode.ToString() + " and MT.ManagerId=" + ManagerId.ToString() + "";

        }
        else
        {
            gstrQrystr = "select MD.ManagerId,MD.ManagerName,Sandbox,sum(AllotedS) as AllotedS ,sum(UsedS) as UsedS," + " " +
            "sum(AllotedM) as AllotedM,sum(UsedM) as UsedM,sum(AllotedL) as AllotedL,sum(UsedL) as UsedL,sum(AllotedXL) as AllotedXL,sum(UsedXL) as UsedXL," + " " +
            "sum(AllotedXXL) as AllotedXXL,sum(UsedXXL) as UsedXXL," + " " +
            "(select sum(case when SanctionStatus = 0  then 1 else 0 end) as RequestCount from student_tshirt_allotment where AcademicCode = " + AcademicCode.ToString() + " and managerid = " + ManagerId.ToString() + ") as RequestCount" + " " +
            "from manager_tshirt as MT Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId where MT.ManagerId=" + ManagerId.ToString() + "";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable Admin_GetManagerWiseTshirtStockCount(string AcademicCode, string Sandbox)
    {
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select MD.ManagerId,MD.ManagerName,Sandbox,sum(AllotedS) as AllotedS ,sum(UsedS) as UsedS, sum(AllotedM) as AllotedM,sum(UsedM) as UsedM,sum(AllotedL) as AllotedL,sum(UsedL) as UsedL,sum(AllotedXL) as AllotedXL,sum(UsedXL) as UsedXL, sum(AllotedXXL) as AllotedXXL,sum(UsedXXL) as UsedXXL," + " " +
            "(select sum(case when SanctionStatus = 0 and AcademicCode = " + AcademicCode.ToString() + " then 1 else 0 end) as RequestCount from student_tshirt_allotment as ST" + " " +
            "inner join manager_details as MD on ST.ManagerId = MD.ManagerId where MD.sandbox = '" + Sandbox.ToString() + "' and MD.isApplicableForTshirt=1)" + " " +
            "from manager_tshirt as MT Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId Where MT.AcademicCode = " + AcademicCode.ToString() + " and sandbox='" + Sandbox.ToString() + "'   group by Sandbox order by SandboxPriority";
        }
        else
        {
            gstrQrystr = "select MD.ManagerId,MD.ManagerName,Sandbox,sum(AllotedS) as AllotedS ,sum(UsedS) as UsedS, sum(AllotedM) as AllotedM,sum(UsedM) as UsedM,sum(AllotedL) as AllotedL,sum(UsedL) as UsedL,sum(AllotedXL) as AllotedXL,sum(UsedXL) as UsedXL, sum(AllotedXXL) as AllotedXXL,sum(UsedXXL) as UsedXXL," + " " +
              "(select sum(case when SanctionStatus = 0 and AcademicCode = " + AcademicCode.ToString() + " then 1 else 0 end) as RequestCount from student_tshirt_allotment as ST" + " " +
            "inner join manager_details as MD on ST.ManagerId = MD.ManagerId where MD.sandbox = '" + Sandbox.ToString() + "' and MD.isApplicableForTshirt=1)" + " " +
            " from manager_tshirt as MT Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId where sandbox='" + Sandbox.ToString() + "' group by Sandbox order by SandboxPriority";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Admin_GetSandboxWiseTshirtCount(string AcademicCode)
    {
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select distinct Sandbox,sum(AllotedS) as AllotedS ,sum(UsedS) as UsedS, sum(AllotedM) as AllotedM,sum(UsedM) as UsedM,sum(AllotedL) as AllotedL,sum(UsedL) as UsedL,sum(AllotedXL) as AllotedXL,sum(UsedXL) as UsedXL, sum(AllotedXXL) as AllotedXXL,sum(UsedXXL) as UsedXXL," + " " +
            "SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL) as Total," + " " +
            "SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL) as Used," + " " +
            "(SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL)) - (SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL)) as Balance," + " " +
             "(select sum(case when SanctionStatus = 0 and ST.AcademicCode = 6 then 1 else 0 end)  from student_tshirt_allotment as ST inner join manager_details as MD on ST.ManagerId = MD.ManagerId" + " " +
            "where MD.isApplicableForTshirt = 1 group by MD.Sandbox) as Requests" + " " +
            "from manager_tshirt as MT Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId Where MT.AcademicCode = " + AcademicCode.ToString() + "  group by Sandbox order by SandboxPriority";
        }
        else
        {
            gstrQrystr = "select distinct Sandbox,sum(AllotedS) as AllotedS ,sum(UsedS) as UsedS, sum(AllotedM) as AllotedM,sum(UsedM) as UsedM,sum(AllotedL) as AllotedL,sum(UsedL) as UsedL,sum(AllotedXL) as AllotedXL,sum(UsedXL) as UsedXL, sum(AllotedXXL) as AllotedXXL,sum(UsedXXL) as UsedXXL," + " " +
            "SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL) as Total," + " " +
            "SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL) as Used," + " " +
             "(SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL)) - (SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL)) as Balance" + " " +
            "from manager_tshirt as MT Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId  group by Sandbox order by SandboxPriority";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Admin_GetSandboxWiseTshirtList(string AcademicCode)
    {
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select Sandbox, Managername,sum(AllotedS) as AllotedS ,sum(UsedS) as UsedS, sum(AllotedM) as AllotedM,sum(UsedM) as UsedM,sum(AllotedL) as AllotedL,sum(UsedL) as UsedL,sum(AllotedXL) as AllotedXL,sum(UsedXL) as UsedXL, sum(AllotedXXL) as AllotedXXL,sum(UsedXXL) as UsedXXL," + " " +
            "SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL) as Total," + " " +
            "SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL) as Used," + " " +
            "(SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL)) - (SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL)) as Balance" + " " +

             "from manager_tshirt as MT Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId Where MT.AcademicCode = " + AcademicCode.ToString() + "  group by Sandbox,managername order by SandboxPriority,MD.ManagerId";
        }
        else
        {
            gstrQrystr = "select Sandbox,Managername,sum(AllotedS) as AllotedS ,sum(UsedS) as UsedS, sum(AllotedM) as AllotedM,sum(UsedM) as UsedM,sum(AllotedL) as AllotedL,sum(UsedL) as UsedL,sum(AllotedXL) as AllotedXL,sum(UsedXL) as UsedXL, sum(AllotedXXL) as AllotedXXL,sum(UsedXXL) as UsedXXL," + " " +
            "SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL) as Total," + " " +
            "SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL) as Used," + " " +
             "(SUM(AllotedS)+SUM(AllotedM)+SUM(AllotedL)+SUM(AllotedXL)+SUM(AllotedXXL)) - (SUM(UsedS)+SUM(UsedM)+SUM(UsedL)+SUM(UsedXL)+SUM(UsedXXL)) as Balance" + " " +
           "from manager_tshirt as MT Inner Join manager_details as MD on MT.ManagerId = MD.ManagerId  group by Sandbox,managername order by SandboxPriority,MD.ManagerId";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Manager_FillCollegeByManagerCode(string ManagerCode, DropDownList ddl)
    {
        gstrQrystr = "SELECT Distinct colleges.CollegeId, colleges.College_Name FROM manager_colleges INNER JOIN  ";
        gstrQrystr = gstrQrystr + " colleges ON manager_colleges.CollegeCode = colleges.CollegeId WHERE (manager_colleges.ManagerCode = " + ManagerCode.ToString() + ") Order by College_Name";
        DLobj.FillDDLWithSelectAll(ddl, gstrQrystr, "CollegeId", "College_Name");
    }

    public DataTable Manager_FillTshirtGridview(string managerId, string FromDate, string ToDate, string CollegeId, string TshirtSize)
    {
        DataTable dt = new DataTable();
        if ((CollegeId == "[All]") && (TshirtSize == "[All]"))
        {
            gstrQrystr = "Select SR.LEAD_ID,SR.StudentName,SR.mobileno,clg.College_Name,CourseName,SemName,ST.TshirtSize,ST.status,ST.remark,DATE_FORMAT(ST.RequestedDate,'%d-%m-%Y') as RequestedDate,DATE_FORMAT(ST.SanctionDate,'%d-%m-%Y') as SanctionDate,DATE_FORMAT(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,DATE_FORMAT(ST.ExchangeDate,'%d-%m-%Y') as ExchangeDate,DATE_FORMAT(ST.EditedDate,'%d-%m-%Y') as EditedDate," + " " +
            "ReapplyReson from student_tshirt_allotment as ST inner join Student_registration as SR on" + " " +
            "ST.RegistrationId = SR.RegistrationId inner join Colleges as clg on SR.CollegeCode = clg.collegeid  INNER JOIN mstr_semester AS MS on SR.SemCode=MS.SemId inner join mstr_programme_course as MPC on SR.coursecode=MPC.CourseId where managerid = " + managerId.ToString() + " " +
             "and ((date_format(RequestedDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(SanctionDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(ExchangeDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(RejectedDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "'))" + " " +
             " order by tshirtsize";
            dt = DLobj.GetDataTable(gstrQrystr);
        }
        else if ((CollegeId != "[All]") && (TshirtSize == "[All]"))
        {
            gstrQrystr = "Select SR.LEAD_ID,SR.StudentName,SR.mobileno,clg.College_Name,CourseName,SemName,ST.TshirtSize,ST.status,ST.remark,DATE_FORMAT(ST.RequestedDate,'%d-%m-%Y') as RequestedDate,DATE_FORMAT(ST.SanctionDate,'%d-%m-%Y') as SanctionDate,DATE_FORMAT(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,DATE_FORMAT(ST.ExchangeDate,'%d-%m-%Y') as ExchangeDate,DATE_FORMAT(ST.EditedDate,'%d-%m-%Y') as EditedDate," + " " +
            "ReapplyReson from student_tshirt_allotment as ST inner join Student_registration as SR on" + " " +
            "ST.RegistrationId = SR.RegistrationId inner join Colleges as clg on SR.CollegeCode = clg.collegeid  INNER JOIN mstr_semester AS MS on SR.SemCode=MS.SemId inner join mstr_programme_course as MPC on SR.coursecode=MPC.CourseId where managerid = " + managerId.ToString() + " and clg.collegeid = " + CollegeId.ToString() + " " +
              "and ((date_format(RequestedDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(SanctionDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(ExchangeDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(RejectedDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "'))" + " " +
            "order by tshirtsize";
            dt = DLobj.GetDataTable(gstrQrystr);

        }
        else if ((CollegeId == "[All]") && (TshirtSize != "[All]"))
        {
            gstrQrystr = "Select SR.LEAD_ID,SR.StudentName,SR.mobileno,clg.College_Name,CourseName,SemName,ST.TshirtSize,ST.status,ST.remark,DATE_FORMAT(ST.RequestedDate,'%d-%m-%Y') as RequestedDate,DATE_FORMAT(ST.SanctionDate,'%d-%m-%Y') as SanctionDate,DATE_FORMAT(ST.RejectedDate,'%d-%m-%Y') as RejectedDate,DATE_FORMAT(ST.ExchangeDate,'%d-%m-%Y') as ExchangeDate,DATE_FORMAT(ST.EditedDate,'%d-%m-%Y') as EditedDate," + " " +
            "ReapplyReson from student_tshirt_allotment as ST inner join Student_registration as SR on" + " " +
            "ST.RegistrationId = SR.RegistrationId inner join Colleges as clg on SR.CollegeCode = clg.collegeid  INNER JOIN mstr_semester AS MS on SR.SemCode=MS.SemId inner join mstr_programme_course as MPC on SR.coursecode=MPC.CourseId where managerid = " + managerId.ToString() + " " +
               "and ((date_format(RequestedDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(SanctionDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(ExchangeDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "')" + " " +
             "OR (date_format(RejectedDate,'%Y-%m-%d') BETWEEN '" + FromDate.ToString() + "' AND '" + ToDate.ToString() + "'))" + " " +
            "order by tshirtsize";
            dt = DLobj.GetDataTable(gstrQrystr);

        }
        return dt;
    }

    public DataTable College_GetMaleFemaleCount(string CollegeId, string AcademicCode)
    {
        //gstrQrystr = "select count(lead_ID) as TotalCount, SUM(CASE WHEN Gender = 'M' THEN 1 ELSE 0 END) as Male,SUM(CASE WHEN Gender = 'F' THEN 1 ELSE 0 END) as Female from student_registration where ManagerCode ="+ManagerID.ToString()+"";
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select ifnull(count(lead_ID),0) as TotalCount from student_registration where isprofileedit=1 and CollegeCode =" + CollegeId.ToString() + " and AcademicCode=" + AcademicCode.ToString() + "";
        }
        else
        {
            gstrQrystr = "select ifnull(count(lead_ID),0) as TotalCount from student_registration where isprofileedit=1 and CollegeCode =" + CollegeId.ToString() + "";
        }
        return DLobj.GetDataTable(gstrQrystr);

    }
    public DataTable College_GetProjectStatusWiseProjectCount(string CollegeId, string AcademicCode)
    {
        gstrQrystr = "select ifnull(sum(case when ProjectStatus='Proposed' then 1 else 0 end),0) as Proposed, ";
        gstrQrystr = gstrQrystr + "ifnull(sum(case when projectstatus='Approved' OR projectstatus = 'Draft' then 1 else 0 end),0) as Approved, ";
        gstrQrystr = gstrQrystr + "ifnull(sum(case when projectStatus='Completed' then 1 else 0 end),0) as Completed, ";
        gstrQrystr = gstrQrystr + "ifnull(sum(case when projectstatus='RequestForModification' then 1 else 0 end),0) as RequestForModification, ";
        gstrQrystr = gstrQrystr + "ifnull(sum(case when projectstatus='RequestForCompletion'  then 1 else 0 end),0) as RequestForCompletion, ";
        gstrQrystr = gstrQrystr + "ifnull(sum(case when ProjectStatus='Rejected' then 1 else 0 end),0) as Rejected, ";
        gstrQrystr = gstrQrystr + "ifnull(count(PDId),0) as TotalProjects ";
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = gstrQrystr + "from project_description as PD,Student_Registration as SR where SR.CollegeCode=" + CollegeId.ToString() + "" + " " +
            "and PD.AcademicCode=" + AcademicCode.ToString() + " and PD.lead_id = SR.lead_id and isprofileedit=1";
        }
        else
        {
            gstrQrystr = gstrQrystr + "from project_description as PD,Student_Registration as SR where isprofileedit=1 and SR.CollegeCode=" + CollegeId.ToString() + "" + " " +
           "and PD.lead_id = SR.lead_id";
        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable College_GetRequestedAndSanctionAmount(string CollegeId, string AcademicCode)
    {
        if (AcademicCode.ToString() != "[All]")
        {
            gstrQrystr = "select ifnull(sum(amount),0) as RequestedAmount,ifnull(sum(SanctionAmount),0) as SanctionAmount from project_description as PD," + " " +
            "student_registration as SR where PD.AcademicCode = " + AcademicCode.ToString() + " and isprofileedit=1 and CollegeCode = " + CollegeId.ToString() + " and PD.lead_id = SR.lead_id";

        }
        else
        {
            gstrQrystr = "select ifnull(sum(amount),0) as RequestedAmount,ifnull(sum(SanctionAmount),0) as SanctionAmount from project_description as PD," + " " +
            "student_registration as SR where isprofileedit=1 and CollegeCode = " + CollegeId.ToString() + " and PD.lead_id = SR.lead_id";
        }

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable College_GetReleaseAmount(string CollegeId, string FromDate, string ToDate)
    {
        if ((FromDate.ToString() != "") && (ToDate.ToString() != ""))
        {
            gstrQrystr = "select ifnull(sum(amount),0) as ReleaseAmount from project_fund_details as PFD,Student_Registration as SR" + " " +
            "where isprofileedit=1 and SR.CollegeCode=" + CollegeId.ToString() + " and PFD.LeadId = SR.lead_id and Date_Format(SR.RegistrationDate,'%Y-%m-%d') between Date_Format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d') ";
        }
        else
        {
            gstrQrystr = "select ifnull(sum(amount),0) as ReleaseAmount from project_fund_details as PFD,Student_Registration as SR" + " " +
           "where isprofileedit=1 and SR.CollegeCode=" + CollegeId.ToString() + " and PFD.LeadId = SR.lead_id";
        }
        return DLobj.GetDataTable(gstrQrystr);
    }
    public void Admin_ConsoliatedReport(string FromDate, string ToDate, string Created_By,string Program_Id)
    {
        gstrQrystr = "SET SQL_SAFE_UPDATES=0; Delete from qrymanager_consolidated_report where created_By=" + Created_By.ToString();
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "INSERT INTO qrymanager_consolidated_report (ManagerId, ManagerName,SandboxName,Emailid,MobileNo,deviceid,SandboxPriority,created_By,Created_Date)" + " " +
        "SELECT MD.ManagerId, MD.ManagerName, MD.Sandbox,MD.MailId,MD.MobileNo,UD.DeviceId,SandboxPriority," + Created_By.ToString() + ",now()" + " " +
        "FROM manager_details as MD left outer  join user_device_details as UD on MD.MobileNo = UD.Username" + " " +
       " WHERE MD.Status = 1 and MD.isApplicableforMail=1 and MD.Program_Id='"+Program_Id.ToString()+"' order by MD.ManagerId";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0; update qrymanager_consolidated_report as MC set " + " " +
        "PaidCount = (Select ifnull(sum(case when isFeePaid = 1 and date_format(RegistrationDate, '%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0)  from Student_Registration as SR where MC.managerid = SR.ManagerCode and MC.Created_By=" + Created_By.ToString() + "); SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "unpaidcount = (Select ifnull(sum(case when isFeePaid = 0 and date_format(RegistrationDate, '%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0)  from Student_Registration as SR where MC.managerid = SR.ManagerCode and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr+=" SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "ProposedCount = (Select ifnull(sum(case when ProjectStatus = 'Proposed' and Date_Format(ProposedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "ApprovedCount = (select ifnull(sum(case when ProjectStatus = 'Approved' and Date_Format(ApprovedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "CompltedCount = (select ifnull(sum(case when ProjectStatus = 'Completed' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "RejectedCount = (select ifnull(sum(case when ProjectStatus = 'Rejected' and Date_Format(RejectedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "RequestForCompletion = (select ifnull(sum(case when ProjectStatus = 'RequestForCompletion' and Date_Format(RequestForCompletionDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "RequestForModification = (select ifnull(sum(case when ProjectStatus = 'RequestForModification' and Date_Format(RequestForModificationDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
        "Drafted = (select ifnull(sum(case when ProjectStatus = 'Draft' and Date_Format(Edited_Date, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "p_InitiatorCount = (select ifnull(sum(case when Project_Levels = 'Initiator' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "p_ChangeMakerCount = (select ifnull(sum(case when Project_Levels = 'Change Maker' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "p_LeaderCount = (select ifnull(sum(case when Project_Levels = 'LEADer' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "p_impactCount = (select ifnull(sum(case when isImpact_Project = 1 and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "L_InitiatorCount=(Select ifnull(sum(case when Levels='Initiator' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "L_ChangeMakerCount=(Select ifnull(sum(case when Levels='Change Maker' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "L_LeaderCount=(Select ifnull(sum(case when Levels='LEADer' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By)," + " " +
        "LMasterLeaderCount=(Select ifnull(sum(case when Levels='Master Leader' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);

        gstrQrystr = "SET SQL_SAFE_UPDATES=0;";
        gstrQrystr += "update qrymanager_consolidated_report as MC set ";
        gstrQrystr += "L_AmbassadorCount=(Select ifnull(sum(case when Levels='Lead Amabassador' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By and MC.Created_By=" + Created_By.ToString() + ");";
        gstrQrystr += " SET SQL_SAFE_UPDATES=1;";
        DLobj.ExecuteQuery(gstrQrystr);


        gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update qrymanager_consolidated_report set GrandTotal=(ProposedCount+ApprovedCount+CompltedCount+RejectedCount+RequestForCompletion+RequestForModification+Drafted) where Created_By=" + Created_By.ToString() + "; SET SQL_SAFE_UPDATES=0;";
        DLobj.ExecuteQuery(gstrQrystr);



        /*  gstrQrystr = "SET SQL_SAFE_UPDATES=0; Delete from qrymanager_consolidated_report where created_By=" + Created_By.ToString();
          DLobj.ExecuteQuery(gstrQrystr);

          gstrQrystr = "INSERT INTO qrymanager_consolidated_report (ManagerId, ManagerName,SandboxName,Emailid,MobileNo,deviceid,SandboxPriority,created_By,Created_Date)" + " " +
          "SELECT MD.ManagerId, MD.ManagerName, MD.Sandbox,MD.MailId,MD.MobileNo,UD.DeviceId,SandboxPriority," + Created_By.ToString() + ",now()" + " " +
          "FROM manager_details as MD left outer  join user_device_details as UD on MD.MobileNo = UD.Username" + " " +
         " WHERE MD.Status = 1 and MD.isApplicableforMail=1 order by MD.ManagerId";
          DLobj.ExecuteQuery(gstrQrystr);

          gstrQrystr = "SET SQL_SAFE_UPDATES=0; update qrymanager_consolidated_report as MC set " + " " +
          "PaidCount = (Select ifnull(sum(case when isFeePaid = 1 and date_format(RegistrationDate, '%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0)  from Student_Registration as SR where MC.managerid = SR.ManagerCode)," + " " +
          "unpaidcount = (Select ifnull(sum(case when isFeePaid = 0 and date_format(RegistrationDate, '%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0)  from Student_Registration as SR where MC.managerid = SR.ManagerCode)," + " " +
          "ProposedCount = (Select ifnull(sum(case when ProjectStatus = 'Proposed' and Date_Format(ProposedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "ApprovedCount = (select ifnull(sum(case when ProjectStatus = 'Approved' and Date_Format(ApprovedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "CompltedCount = (select ifnull(sum(case when ProjectStatus = 'Completed' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "RejectedCount = (select ifnull(sum(case when ProjectStatus = 'Rejected' and Date_Format(RejectedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "RequestForCompletion = (select ifnull(sum(case when ProjectStatus = 'RequestForCompletion' and Date_Format(RequestForCompletionDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "RequestForModification = (select ifnull(sum(case when ProjectStatus = 'RequestForModification' and Date_Format(RequestForModificationDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "Drafted = (select ifnull(sum(case when ProjectStatus = 'Draft' and Date_Format(Edited_Date, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "p_InitiatorCount = (select ifnull(sum(case when Project_Levels = 'Initiator' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "p_ChangeMakerCount = (select ifnull(sum(case when Project_Levels = 'Change Maker' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "p_LeaderCount = (select ifnull(sum(case when Project_Levels = 'LEADer' and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid)," + " " +
          "p_impactCount = (select ifnull(sum(case when isImpact_Project = 1 and Date_Format(CompletedDate, '%Y-%m-%d') between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "' then 1 else 0 end), 0) from project_description as PD where MC.managerid = PD.managerid), " + " " +
          "L_InitiatorCount=(Select ifnull(sum(case when Levels='Initiator' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By)," + " " +
          "L_ChangeMakerCount=(Select ifnull(sum(case when Levels='Change Maker' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By)," + " " +
          "L_LeaderCount=(Select ifnull(sum(case when Levels='LEADer' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By)," + " " +
          "LMasterLeaderCount=(Select ifnull(sum(case when Levels='Master Leader' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By)," + " " +
          "L_AmbassadorCount=(Select ifnull(sum(case when Levels='Lead Amabassador' and date_format(Created_Date,'%Y-%m-%d')  between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'  then 1 else 0 end),0)  from student_levels as SL where MC.managerid=SL.Created_By)" + " " +
          "where MC.Created_By=" + Created_By.ToString() + "";
          DLobj.ExecuteQuery(gstrQrystr);

          gstrQrystr = "SET SQL_SAFE_UPDATES=0;  update qrymanager_consolidated_report set GrandTotal=(ProposedCount+ApprovedCount+CompltedCount+RejectedCount+RequestForCompletion+RequestForModification+Drafted) where Created_By=" + Created_By.ToString() + "";
          DLobj.ExecuteQuery(gstrQrystr);
*/
    }


    public DataTable Bind_AdminConsoliatedReport(string createdBy, string programid)
    {
        /* gstrQrystr = "select ManagerId,ManagerName,SandboxName,paidcount,unpaidcount,ProposedCount,ApprovedCount,CompltedCount,"+" "+
        "RejectedCount,RequestForModification,RequestForCompletion,Drafted,GrandTotal,p_InitiatorCount,p_ChangeMakerCount,"+" "+
        "p_LeaderCount,p_impactCount,L_InitiatorCount,L_ChangeMakerCount,L_LeaderCount,LMasterLeaderCount,L_AmbassadorCount "+" "+
        "from qrymanager_consolidated_report where Created_By=" + createdBy.ToString()+"";
 */

        gstrQrystr = "select distinct qc.ManagerId, qc.ManagerName,qc.SandboxName,paidcount,qc.unpaidcount,qc.ProposedCount,qc.ApprovedCount,qc.CompltedCount,qc.RejectedCount,qc.RequestForModification,qc.RequestForCompletion,Drafted,qc.GrandTotal,qc.p_InitiatorCount,qc.p_ChangeMakerCount, qc.p_LeaderCount,qc.p_impactCount,qc.L_InitiatorCount,qc.L_ChangeMakerCount,qc.L_LeaderCount,qc.LMasterLeaderCount,qc.L_AmbassadorCount from qrymanager_consolidated_report as qc inner join manager_details as md  on md.ManagerId = qc.ManagerId inner join manager_colleges as mc on mc.ManagerCode = md.ManagerId inner join college_programs as cp on mc.CollegeCode = cp.college_id inner join user_programs as up on cp.program_id = up.program_id and up.user_id = " + createdBy.ToString() + " where up.program_id = " + programid.ToString() + " group by qc.ManagerId  ";

        return DLobj.GetDataTable(gstrQrystr);
    }

    public DataTable Admin_GetProject_List(string FromDate, string ToDate)
    {
        gstrQrystr = "SELECT PD.PDId,date_format(SR.RegistrationDate,'%d-%m-%Y') as RegistrationDate, SR.Lead_Id,SR.Gender, SR.StudentName,SR.MobileNo,SR.MailId,colleges.College_Name,date_format(PD.ProposedDate,'%d-%m-%Y') as ProposedDate, PD.Title,IFNULL(PD.Amount,0) as RequestedAmount," + " " +
                "PD.SanctionAmount AS SanctionAmount," + " " +
               "(SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
               "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "')) as Disperse," + " " +
               "PD.SanctionAmount - (SELECT IFNULL(SUM(PF.Amount),0) AS DisperseAmount from project_fund_details as PF where PD.PDId = PF.PDId and PD.Lead_Id = PF.LeadId" + " " +
               "and (Date(PD.ProposedDate) between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "'))  as Balance," + " " +
               "PD.ProjectStatus,MD.ManagerName,sem.semname,date_format(PD.ApprovedDate,'%d-%m-%Y') as ApprovedDate,date_format(PD.CompletedDate,'%d-%m-%Y') as CompletedDate,date_format(PD.RejectedDate,'%d-%m-%Y') as RejectedDate" + " " +
               "FROM student_registration AS SR INNER JOIN project_description AS PD ON SR.Lead_Id = PD.Lead_Id INNER JOIN" + " " +
               "manager_details AS MD ON PD.ManagerId = MD.ManagerId INNER JOIN colleges ON SR.CollegeCode = colleges.CollegeId INNER JOIN mstr_semester as Sem ON SR.SemCode = sem.SemId" + " " +
               "WHERE (date_format(PD.ProposedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                 "OR (date_format(PD.ApprovedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                 "OR (date_format(PD.RequestForModificationDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                 "OR (date_format(PD.RequestForCompletionDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                 "OR (date_format(PD.RejectedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d'))" + " " +
                 "OR (date_format(PD.CompletedDate,'%Y-%m-%d') between date_format('" + FromDate.ToString() + "','%Y-%m-%d') and Date_Format('" + ToDate.ToString() + "','%Y-%m-%d')) and SR.isProfileEdit=1" + " " +
                 "Order by PD.Title";

        return DLobj.GetDataTable(gstrQrystr);
    }
}