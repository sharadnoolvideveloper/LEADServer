using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_Admin_Consoliated_Graphs : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    Charts chrt = new Charts();
    vmCookies cook = new vmCookies();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
           
                BindChart1();
                BindChart2();
                BindChart3();
           
        }

    }


    private void BindChart1()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = chrt.GetStudentGenderWiseMaleFemale("2000-01-01", "2019-03-31", "");

            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*],callback: drawChart});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'StateName');
        data.addColumn('number', 'Male');
        data.addColumn('number', 'Female');       

        data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue(" + i + "," + 0 + "," + "'" + dt.Rows[i]["StateName"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Male"].ToString() + ") ;");
                str.Append("data.setValue(" + i + "," + 2 + "," + dt.Rows[i]["Female"].ToString() + ") ;");
            }

            str.Append(" var chart = new google.visualization.BarChart(document.getElementById('chart_div1'));");
            str.Append("chart.draw(data, {width:700, height:400,enableTooltip:true,is3D: true,isStacked:true,backgroundColor: {stroke:'black', fill:'#fff',strokeSize: 1}, title: 'State Wise Male Female',");
            str.Append("hAxis: {title: 'District - Taluka', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");
            lt1.Text = str.ToString().TrimEnd(',').Replace('*', '"');

        }
        catch
        {
        }
    }
    private void BindChart2()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = chrt.GetStudentFundingDetails("2000-01-01", "2019-03-31", "");

            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*],callback: drawChart});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'ManagerName');
       
        data.addColumn('number', 'RequestedAmount');
        data.addColumn('number', 'SanctionAmount'); 
         

        data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue(" + i + "," + 0 + "," + "'" + dt.Rows[i]["ManagerName"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["RequestedAmount"].ToString() + ") ;");
                str.Append("data.setValue(" + i + "," + 2 + "," + dt.Rows[i]["SanctionAmount"].ToString() + ") ;");
               
            }
            str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div2'));");
            str.Append("chart.draw(data, {width:700, height:400,enableTooltip:true,is3D: true,isStacked: true,backgroundColor: {stroke:'black', fill:'#fff',strokeSize: 1}, title: 'State Wise Fund Details',");
            str.Append("hAxis: {title: 'State Wise - Funding', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");
            lt2.Text = str.ToString().TrimEnd(',').Replace('*', '"');

        }
        catch
        {
        }
    }

    private void BindChart3()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = chrt.GetManagerWiseProjectStatusCount("2000-01-01", "2019-03-31", "");

            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*],callback: drawChart});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'ManagerName');
        data.addColumn('number', 'Proposed',{ role: 'annotation'});
        data.addColumn('number', 'Approved',{ role: 'annotation'}); 
        data.addColumn('number', 'Completed',{ role: 'annotation'}); 
        data.addColumn('number', 'RequestForModification',{ role: 'annotation'});
        data.addColumn('number', 'RequestForCompletion',{ role: 'annotation'});   

        data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue(" + i + "," + 0 + "," + "'" + dt.Rows[i]["ManagerName"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Proposed"].ToString() + ",'300') ;");
                str.Append("data.setValue(" + i + "," + 2 + "," + dt.Rows[i]["Approved"].ToString() + ",'200') ;");
                str.Append("data.setValue(" + i + "," + 3 + "," + dt.Rows[i]["Completed"].ToString() + ",'300') ;");
                str.Append("data.setValue(" + i + "," + 4 + "," + dt.Rows[i]["RequestForModification"].ToString() + ") ;");
                str.Append("data.setValue(" + i + "," + 5 + "," + dt.Rows[i]["RequestForCompletion"].ToString() + ") ;");
            }
            str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div3'));");
            str.Append("chart.draw(data, {width:1400, height:600,enableTooltip:true,is3D: true,isStacked: true,backgroundColor: {stroke:'black', fill:'#fff',strokeSize: 1}, title: 'Manager Wise Project Status',");
            str.Append("hAxis: {title: 'Manager Wise - Project Status', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");
            lt3.Text = str.ToString().TrimEnd(',').Replace('*', '"');

        }
        catch
        {
        }
    }
}