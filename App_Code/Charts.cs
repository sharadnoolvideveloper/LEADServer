using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Universal.Standard;
using System.Data;

/// <summary>
/// Summary description for Charts
/// </summary>
public class Charts
{
    UniversalDL DLobj = new UniversalDL();
    string gstrQrystr = "";
    public Charts()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetStudentGenderWiseMaleFemale(string FromDate,string ToDate,string ManagerCode)
    {
        if (ManagerCode.ToString() != "")
        {
            gstrQrystr = "SELECT sum(case when Gender='M' then 1 else 0 end) as Male,sum(case when Gender='F' then 1 else 0 end) as Female," + " " +
       "CONCAT(Dist.DistrictName, ' ', '-', ' ', Tal.Taluk_Name) AS Taluka FROM student_registration AS SR " + " " +
       "INNER JOIN mstr_district AS Dist ON SR.DistrictCode = Dist.DistrictId INNER JOIN" + " " +
       "mstr_taluka AS Tal ON SR.TalukaCode = Tal.Id AND Dist.DistrictId = Tal.District_Id" + " " +
       "WHERE(SR.RegistrationDate between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') and (SR.ManagerCode=" + ManagerCode.ToString() + ") group by Tal.Taluk_Name";
        }
        else
        {
            gstrQrystr = "SELECT sum(case when Gender='M' then 1 else 0 end) as Male,sum(case when Gender='F' then 1 else 0 end) as Female, state.StateName" + " " +
      "FROM student_registration AS SR INNER JOIN mstr_state AS state ON SR.StateCode = state.code " + " " +
      "WHERE(SR.RegistrationDate between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') group by state.StateName";
        }
       
        return DLobj.GetDataTable(gstrQrystr);

    }

    public DataTable GetStudentFundingDetails(string FromDate,string ToDate,string ManagerId)
    {
        if(ManagerId.ToString()=="")
        {
            //      gstrQrystr = "SELECT mstr_state.Statename,IFNULL(sum(PD.Amount), 0) AS RequestedAmount, IFNULL(sum(PD.SanctionAmount), 0) AS SanctionAmount, IFNULL(SUM(PFD.Amount), 0) AS giventotal" + " " +
            //"FROM project_description PD Left Outer JOIN student_registration SD ON PD.Lead_Id = SD.Lead_Id" + " " +
            //"Left Outer JOIN mstr_state ON SD.StateCode = mstr_state.code LEFT Outer JOIN project_fund_details PFD ON PD.PDId = PFD.PDId" + " " +
            //"where (GivenDate between '" + FromDate.ToString() + "' and '" + ToDate.ToString() + "') group by mstr_state.Statename";
            gstrQrystr = "SELECT manager_details.ManagerName,IFNULL(sum(PD.Amount), 0) AS RequestedAmount, IFNULL(sum(PD.SanctionAmount), 0) AS SanctionAmount" + " " +
            "FROM project_description as PD INNER JOIN Manager_details ON PD.ManagerId = manager_details.ManagerId group by manager_details.ManagerName";


        }
        else
        {
           
        }     

        return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable GetStudentProjectListThemeWise(string FromDate,string ToDate,string ManagerId)
    {
        gstrQrystr = "";
       return DLobj.GetDataTable(gstrQrystr);
    }
    public DataTable GetManagerWiseProjectStatusCount(string FromDate, string ToDate, string ManagerId)
    {
        gstrQrystr = "SELECT sum(case when ProjectStatus='Proposed' then 1 else 0 end) as Proposed,"+" "+
        "sum(case when ProjectStatus = 'Approved' then 1 else 0 end) as Approved,"+" "+
        "sum(case when ProjectStatus = 'Completed' then 1 else 0 end) as Completed,"+" "+
        "sum(case when ProjectStatus = 'RequestForModification' then 1 else 0 end) as RequestForModification,"+" "+
        "sum(case when ProjectStatus = 'RequestForModification' then 1 else 0 end) as RequestForCompletion,MD.ManagerName"+" "+
        "FROM manager_details AS MD INNER JOIN project_description AS PD ON MD.ManagerId = PD.ManagerId group by MD.ManagerName";
        return DLobj.GetDataTable(gstrQrystr);
    }



}