<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Manager/Manager.master" AutoEventWireup="true" CodeFile="ManagerAnalyticalCharts.aspx.cs" Inherits="Pages_Manager_ManagerAnalyticalCharts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script src="../../JS/ManagerJS/app.v2.js"></script>
   
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/funnel.js" type="text/javascript"></script>

     <script>
        // MANAGE WISE PROJECT FUND SANCTION AND ASKED START
        $(function () {
            $.ajax({
                type: "GET",
                url: "ManagerAnalyticalCharts.aspx/getManagersanctionamount",
                // data: {},
                data: { ManagerCode: 2 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    ListProjectSanctionamontlist(response);
                    //OnSuccess(JSON.stringify(response));
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        function ListProjectSanctionamontlist(response) {
            var AcademicCode = [];
            var RequestAmount = [];
            var SanctionAmount = [];
            var fundRelised = [];
            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                AcademicCode.push(val1.AcademicCode);
                RequestAmount.push(val1.RequestAmount);
                SanctionAmount.push(val1.SanctionAmount);
                fundRelised.push(val1.fundRelised);
            });

            Highcharts.chart('ManagerprojectAmountstatus', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Yearwise Request, Sanction  and fundrelised Amount List'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: AcademicCode,//['DKF', 'DFP'],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Request and Sanction Amount List'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                },
                series: [{
                    name: 'RequestAmount',
                    data: RequestAmount,
                    color: '#00538b'
                }, {
                    name: 'SanctionAmount',
                    data: SanctionAmount,
                        color: '#24a45a'
                }, {
                    name: 'fundRelised',
                    data: fundRelised,
                        color: '#ff9c00'
                }]
            });
        }
        //END
        // MANAGER WISE PROJECT STATUS COUNT
        $(function () {
            $.ajax({
                type: "GET",
                url: "ManagerAnalyticalCharts.aspx/getManagerprojectstatuscount",
                // data: {},
                data: { ManagerId: 2 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    ListProjectStatuscount(response);
                    //OnSuccess(JSON.stringify(response));
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        function ListProjectStatuscount(response) {
            var ProjectStatus = [];
            var Counts = [];

            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                ProjectStatus.push(val1.ProjectStatus);
                Counts.push(val1.Counts);

            });

            Highcharts.chart('Managerwiseprojectstatuscount', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Manager Wise peojectstatus counts'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: ProjectStatus,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'ProjectStatus'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                },
                series: [{
                    name:'Project Count',
                    data: [{
                        name: 'Approved',
                        y: Counts[0],
                        color: '#3e1999',
                        
                       
                    }, {
                            name: 'Completed',
                            y: Counts[1],
                            color: '#28b779'
                            
                        }, {
                            name: 'Draft',
                            y: Counts[2],
                            color: '#ffe400'
                           
                        }, {
                            name: 'Proposed',
                            y: Counts[3],
                            color: '#27a9e3'
                            
                        }, {
                            name: 'Rejected',
                            y: Counts[4],
                            color: '#aa0b0b'
                            
                        }, {
                            name: 'RequestForCompletion',
                            y: Counts[5],
                            color: '#da9628'
                            
                        }, {
                            name: 'RequestForModification',
                            y: Counts[6],
                            color: '#f74d4d'
                            
                        }]
                }]
            });
        }
        //END
        // Managerwise theme count
        $(function () {
            $.ajax({
                type: "GET",
                url: "ManagerAnalyticalCharts.aspx/getManagerThemewiseprojectcount",
                // data: {},
                data: { ManagerId: 8 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    ListThemeprojectcount(response);
                    //OnSuccess(JSON.stringify(response));
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        function ListThemeprojectcount(response) {
            var ThemeName = [];
            var Counts = [];

            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                ThemeName.push(val1.ThemeName);
                Counts.push(val1.Counts);

            });

            Highcharts.chart('ManagerwiseThemecount', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Theme wise project counts'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: ThemeName,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'ThemeName'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                },
                series: [{
                    name: 'Counts',
                    data: Counts,
                    color: '#20B2AA'
                }]
            });
        }
        // end 

    </script>

<%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Manager wise ALL Status project counts',
                pieHole: 0.5
            };
            $.ajax({
                type: "POST",
                url: "ManagerAnalyticalCharts.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.PieChart($("#chart1")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-6">

                <div id="ManagerprojectAmountstatus" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>

            <div class="col-lg-6">
                <div id="Managerwiseprojectstatuscount" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>

        </div>
    </div>
    <hr />

    <div class="row" style="margin-top: 4px;">
        <div class="col-lg-12">
            <div class="col-lg-6 hidden">


                <div id="chart1" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

            </div>
            <div class="col-lg-12">

                <div id="ManagerwiseThemecount" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

            </div>
        </div>

    </div>
</asp:Content>

