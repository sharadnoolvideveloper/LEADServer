<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Admin/AdminPanel.master" AutoEventWireup="true" CodeFile="AdminAnalyticalCharts.aspx.cs" Inherits="Pages_Admin_AdminAnalyticalCharts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../JS/CommonJS/1.11.1jquery.min.js"></script>
    <script src="../../JS/StudentJS/bootstrap.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/funnel.js" type="text/javascript"></script>

    <%--   // <link href="CSS/chart.css" rel="stylesheet" />--%>

    <script>
        // MaNAGER WISE pROJECTCOUNT STATUS START
        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/getManagerprojectcounts",
                data: {},
                // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    ListProjectstatuscount(response);
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

        function ListProjectstatuscount(response) {
            var ManagerName = [];
            var Proposed = [];
            var Approved = [];
            var Completed = [];
            var RequestForModification = [];
            var RequestForCompletion = [];
            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                ManagerName.push(val1.ManagerName);
                Proposed.push(val1.Proposed);
                Approved.push(val1.Approved);
                Completed.push(val1.Completed);
                RequestForModification.push(val1.RequestForModification);
                RequestForCompletion.push(val1.RequestForCompletion);
            });

            Highcharts.chart('Managerprojectstatuscount', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Manager wise ProjectStatus counts'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: ManagerName

                    //crosshair: true
                },               
                yAxis: {
                    min: 0,
                    title: {
                        text: 'ProjectStatus counts'
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
                    name: 'Proposed',
                    data: Proposed,
                    color: '#27a9e3'
                }, {
                    name: 'Approved',
                    data: Approved,
                    color: '#3e1999'
                }, {
                    name: 'Completed',
                    data: Completed,
                        color: '#28b779'
                }, {
                    name: 'RequestForModification',
                    data: RequestForModification,
                        color: '#f74d4d'
                }, {
                    name: 'RequestForCompletion',
                    data: RequestForCompletion,
                        color: '#da9628'
                }]
            });
        }

        // END

        // MANAGE WISE PROJECT FUND SANCTION AND ASKED START
        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/getManagersanctionamount",
                data: {},
                // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
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
            var ManagerName = [];
            var RequestedAmount = [];
            var SanctionAmount = [];
            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                ManagerName.push(val1.ManagerName);
                RequestedAmount.push(val1.RequestedAmount);
                SanctionAmount.push(val1.SanctionAmount);

            });

            Highcharts.chart('ManagerprojectAmountstatus', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Manager wise Request and Sanction Amount List'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: ManagerName,//['DKF', 'DFP'],
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
                    name: 'RequestedAmount',
                    data: RequestedAmount,
                    color: '#3e1999'
                }, {
                    name: 'SanctionAmount',
                    data: SanctionAmount,
                    color: '#4285f4'
                }]
            });
        }
        //END

        // ALL PROJECT STATUS COUNT LIST

        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/getprojectstatuscountlist",
                data: {},
                // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    ListProjectstatuslist(response);
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

        function ListProjectstatuslist(response) {
            var Proposed =0;
            var Approved = 0;
            var Completed = 0;
            var RequestForModification = 0;
            var RequestForCompletion = 0;

          
            var data = [];

            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                Proposed=val1.Proposed;
                Approved=val1.Approved;
                Completed=val1.Completed;
                RequestForModification=val1.RequestForModification;
                RequestForCompletion=val1.RequestForCompletion;


            });

            Highcharts.chart('projectcountstatus', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 30
                    }
                },
                title: {
                    text: 'All Project Status count List'
                },
                tooltip: {
                    //pointFormat: '{series.name}: <b>{series.y}</b>'
                },
                plotOptions: {
                    pie: {
                        innerSize: 100,
                        depth: 100,
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b></b><br>{point.percentage:.0f} %',
                            distance: -40

                        },
                        showInLegend: true
                    }
                },
                series: [{ 
                    name: 'Projects',
                    data: [
                        {
                            name: 'Proposed',
                            y: Proposed,
                            color: '#27a9e3'
                        },
                        {
                            name: 'Approved',
                            y: Approved,
                            color: '#3e1999'
                        },
                        {
                            name: 'Completed',
                            y: Completed,
                            color: '#28b779'
                        },
                            {
                                name: 'RequestForModification',
                                y: RequestForModification,
                                color: '#f74d4d'
                        },
                                {
                                    name: 'RequestForCompletion',
                                    y: RequestForCompletion,
                                    color: '#da9628'
                        }]               
                }],
                legend: {
                    reversed: true
                }
            });
        }

        //END

        // Statewise project counts

        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/getstatewiseprojectcount",
                data: {},
                // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    Liststatewiseprojectcount(response);
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

        function Liststatewiseprojectcount(response) {
            var StateName = [];
            var ProjectCounts = [];
            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                StateName.push(val1.StateName);
                ProjectCounts.push(val1.ProjectCounts);


            });

            Highcharts.chart('Statewiseprojectcounts', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'All Project State Wise count List'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: StateName,//['DKF', 'DFP'],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'All Project State wise count List'
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
                    name: 'ProjectCounts',
                    data: ProjectCounts,
                    color: 'blue'
                }]
            });
        }
        // END



        // Theme wise project counts
        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/getThemewiseprojectcount",
                data: {},
                // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    ListThemewiseprojectcount(response);
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


        function ListThemewiseprojectcount(response) {
            var ThemeName = [];
            var ProjectCounts = [];
            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                ThemeName.push(val1.ThemeName);
                ProjectCounts.push(val1.ProjectCounts);


            });

            Highcharts.chart('Themewiseprojectcounts', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Theme wise All Project counts'
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
                        text: 'Theme wise project counts'
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
                    name: 'ProjectCounts',
                    data: ProjectCounts,
                    color: 'Green'
                }]
            });
        }
        //end

        // Yearwise gender counts
        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/getMaleandfemalecount",
                data: {},
                // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    Listmaleandfemalecount(response);
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


        function Listmaleandfemalecount(response) {
            var AcademiCYear = [];
            var Male = [];
            var Female = [];
            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                AcademiCYear.push(val1.AcademiCYear);
                Male.push(val1.Male);
                Female.push(val1.Female);


            });

            Highcharts.chart('YearwiseGender', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Year wise Student registration for Male and Female counts'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: AcademiCYear,//['DKF', 'DFP'],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Male and Female counts'
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
                    name: 'Male',
                    data: Male,
                    color: '#2da9d9'
                }, {
                    name: 'Female',
                    data: Female,
                        color: '#b73377'
                }]
            });
        }

        //end


        // START PIE CHAET
        $(function () {

            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/Getstudentcounts",
                // data: { EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), ProgramID: 0, AcademicID: $('#ContentPlaceHolder1_ddlAcademic').val(), StartDate: 0, EndDate: 0 },
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var TotalRegistration = 0;
                    var TotalActivestudent = 0;
                    var PresentYearRgistration = 0;


                    var data1 = [];

                    $.each(data, function (i, val) {
                        data1 = val;
                        return;
                    });



                    if (data1.length > 0 && data != 'Error') {

                        $.each(data1, function (i, val) {
                            TotalRegistration = val.TotalRegistration;
                            TotalActivestudent = val.TotalActivestudent;
                            PresentYearRgistration = val.PresentYearRgistration;

                        });

                        //Application & Admission
                        Highcharts.chart('Totalstudentcount', {
                            chart: {
                                type: 'pie',
                                options3d: {
                                    enabled: true,
                                    alpha: 30
                                }
                            },
                            title: {
                                text: 'TotalRegistration,TotalActivestudent & PresentYearRgistration'
                            },
                            tooltip: {
                                //pointFormat: '{series.name}: <b>{series.y}</b>'
                            },
                            plotOptions: {
                                pie: {
                                    innerSize: 140,
                                    depth: 140,
                                    allowPointSelect: true,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: true,
                                        format: '<b></b><br>{point.percentage:.0f} %',
                                        distance: -40

                                    },
                                    showInLegend: true
                                }
                            },
                            series: [{
                                name: 'Students',
                                data: [
                                    {
                                        name: 'TotalRegistration',
                                        y: TotalRegistration,
                                        color: '#0000ff'
                                    },
                                     {
                                         name: 'TotalActivestudent',
                                         y: TotalActivestudent,
                                         color: '#ffa500'
                                     },
                                    {
                                        name: 'PresentYearRgistration',
                                        y: PresentYearRgistration,
                                        color: '#28b779'
                                    }
                                ]
                            }],
                            legend: {
                                reversed: true
                            }
                        });


                    }
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        //END
    </script>

    <script>
        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/getTotalRegistration",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //alert(response.Application);
                    ListTotalRegistration(response);
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

        function ListTotalRegistration(response) {
            var ManagerName = [];
            var TotalRegistration = [];
            var PresentYearRgistration = [];

            var data = [];
            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                ManagerName.push(val1.ManagerName);
                TotalRegistration.push(val1.TotalRegistration);
                PresentYearRgistration.push(val1.PresentYearRgistration);

            });
            Highcharts.chart('container', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Total Registration Vs PresentYearRgistration'
                },
                credits: {
                    enabled: false
                },
                xAxis: {
                    categories: ManagerName
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'TotalRegistration and PresentYearRgistration'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false
                },
                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
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
                    name: 'TotalRegistration',
                    data: TotalRegistration,
                    color: '#0000ff'
                }, {
                    name: 'PresentYearRgistration',
                    data: PresentYearRgistration,
                        color: '#ffa500'
                }, ]
            });
        }
    </script>

    <script>
        // start total reg
        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/Getstudentcountss",
                // data: { SandboxID: $("#ContentPlaceHolder1_ddlSandbox").val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val() },
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    ListStudentcounts(response);
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        function ListStudentcounts(response) {

            var TotalRegistration = 0;//[];
            var TotalActivestudent = 0;//[];
            var PresentYearRgistration = 0;//[];

            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                //TotalRegistration.push(val1.TotalRegistration);
                //TotalActivestudent.push(val1.TotalActivestudent);
                //PresentYearRgistration.push(val1.PresentYearRgistration);
                TotalRegistration = val1.TotalRegistration;
                TotalActivestudent = val1.TotalActivestudent;
                PresentYearRgistration = val1.PresentYearRgistration;
            });

            Highcharts.chart('containerID', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    text: 'Total<br>registration',
                    align: 'center',
                    verticalAlign: 'middle',
                    y: 40
                },
                credits: {
                    enabled: false
                },
                subtitle: {
                    text: 'Total Registration',
                    align: 'center',
                    verticalAlign: 'top',
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b>'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'black'
                            }
                        },
                        startAngle: -90,
                        endAngle: 90,
                        center: ['50%', '75%']
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'ID<br>Total<br>Reg',
                    innerSize: '50%',
                    data: [
                        {
                            name: 'TotalRegistration (' + TotalRegistration.toFixed(0) + ')',
                            y: TotalRegistration,
                            color: '#0000ff'
                        },
                        {
                            name: 'TotalActivestudent (' + TotalActivestudent.toFixed(0) + ')',
                            y: TotalActivestudent,
                            color: '#ffa500'
                        },
                         {
                             name: 'PresentYearRgistration(' + PresentYearRgistration.toFixed(0) + ')',
                             y: PresentYearRgistration,
                             color: '#4285f4'
                         }
                    ]
                }]
            });



        }
        //end

        //start
        $(function () {
            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/GetstudentandFundamount",
                // data: { SandboxID: $("#ContentPlaceHolder1_ddlSandbox").val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val() },
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    ListFundStudent(response);
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        function ListFundStudent(response) {

            var Totalstudent = 0;//[];
            var SanctionAmount = 0;//[];
            var Totalamount = 0;//[];


            $.each(response, function (i, val) {
                data = val;
                return;
            });

            $.each(data, function (i, val1) {
                //TotalRegistration.push(val1.TotalRegistration);
                //TotalActivestudent.push(val1.TotalActivestudent);
                //PresentYearRgistration.push(val1.PresentYearRgistration);
                Totalstudent = val1.Totalstudent;
                SanctionAmount1 = val1.SanctionAmount;
                SanctionAmount = val1.SanctionAmount - val1.Totalamount;
                Totalamount = val1.Totalamount;

            });

            Highcharts.chart('funddetails', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    text: 'Funding<br>Details',
                    align: 'center',
                    verticalAlign: 'middle',
                    y: 40
                },
                credits: {
                    enabled: false
                },
                subtitle: {
                    text: 'Totalstudent,SanctionAmount,TotalFundAmount and Remaining',
                    align: 'center',
                    verticalAlign: 'top',
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b>'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'black'
                            }
                        },
                        startAngle: -90,
                        endAngle: 90,
                        center: ['50%', '75%']
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'ID<br>Total<br>Student<br> Amount',
                    innerSize: '50%',
                    data: [
                        {
                            name: 'Totalstudent (' + Totalstudent.toFixed(2) + ')',
                            y: Totalstudent,
                            color: '#0000ff'
                        },
                        {
                            name: 'SanctionAmount (' + SanctionAmount1.toFixed(2) + ')',
                            y: SanctionAmount1,
                            color: '#00538b'
                        },

                        {
                            name: 'FundedAmount (' + Totalamount.toFixed(2) + ')',
                            y: Totalamount,
                            color: '#24a45a'
                        },
                        {
                            name: 'RemainingAmount (' + SanctionAmount.toFixed(2) + ')',
                            y: SanctionAmount,
                            color: '#008000'
                        }

                    ]
                }]
            });



        }


        //end 

        // START PIE CHAET TPTAL FUND LIST IN LEAD
        $(function () {

            $.ajax({
                type: "GET",
                url: "AdminAnalyticalCharts.aspx/GetTotalfundlistinlead",
                // data: { EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), ProgramID: 0, AcademicID: $('#ContentPlaceHolder1_ddlAcademic').val(), StartDate: 0, EndDate: 0 },
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var Totalstudent = 0;
                    var TotalRequestAmount = 0;
                    var TotalSanctionAmount = 0;


                    var data1 = [];

                    $.each(data, function (i, val) {
                        data1 = val;
                        return;
                    });



                    if (data1.length > 0 && data != 'Error') {

                        $.each(data1, function (i, val) {
                            Totalstudent = val.Totalstudent;
                            TotalRequestAmount = val.TotalRequestAmount;
                            TotalSanctionAmount = val.TotalSanctionAmount;

                        });




                        //Student Fund list in leadproject
                        Highcharts.chart('totalfundlistinlead', {
                            chart: {
                                type: 'funnel'
                            },
                            title: {
                                text: 'Overall FundDetails'
                            },
                            plotOptions: {
                                series: {
                                    dataLabels: {
                                        enabled: true,
                                        format: '<b>{point.name}</b> ({point.y:,.0f})',
                                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black',
                                        softConnector: true
                                    },
                                    center: ['50%', '50%'],
                                    neckWidth: '30%',
                                    neckHeight: '25%',
                                    width: '80%'
                                }
                            },
                            legend: {
                                enabled: true
                            },
                            series: [{
                                name: 'Students and funddetails',
                                color: '#f3830d',
                                data: [
                                    {
                                        name: 'TotalRequestAmount',
                                        y: TotalRequestAmount,
                                        color: '#28b779'
                                    },
                                    {
                                        name: 'TotalSanctionAmount',
                                        y: TotalSanctionAmount,
                                        color: '#2255a4'
                                    },
                                    {
                                        name: 'Totalstudent',
                                        y: Totalstudent,
                                        color: '#da542e'
                                    }
                                ]
                            }]
                        });


                    }
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

        //END

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
       <div class="row">
        <div class="col-lg-12 ">
            <div id="Managerprojectstatuscount" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
        </div>
    </div>
  
    <hr />
 
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <div id="ManagerprojectAmountstatus" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
        </div>
    </div>
    <hr />
    <div class="row ">
        <div class="col-lg-12">
            <div class="col-lg-6 form-group">

                <div id="Statewiseprojectcounts" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>

            <div class="col-lg-6 form-group">
                <div id="Themewiseprojectcounts" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>

        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-6 form-group">
                <div id="YearwiseGender" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
            <div class="col-lg-6 form-group">
                <div id="projectcountstatus" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
        </div>
    </div>
    <hr />
    <div class="row" style="margin-top: 4px;">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <div id="container" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-6 form-group">
                <div id="funddetails" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
            <div class="col-lg-6 form-group">
                <div id="Totalstudentcount" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
        </div>
    </div>
    <hr />
     <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-6 form-group">
                <div id="containerID" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>

            <div class="col-lg-6 form-group" >
                <div id="totalfundlistinlead" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
        </div>
    </div>
    <br />
    <hr />

    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <div id="collegecount" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
        </div>
        <script>
            $(function () {
                $.ajax({
                    type: "GET",
                    url: "AdminAnalyticalCharts.aspx/getTotalcollegecount",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //alert(response.Application);
                        ListTotalCollegecount(response);
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

            function ListTotalCollegecount(response) {
                var College_Name = [];
                var Taluk_Name = [];
                var DistrictName = [];
                var StateName = [];
                var Registrations = [];

                var data = [];
                $.each(response, function (i, val) {
                    data = val;
                    return;
                });

                $.each(data, function (i, val1) {
                    College_Name.push(val1.College_Name);
                    Taluk_Name.push(val1.Taluk_Name);
                    DistrictName.push(val1.DistrictName);
                    StateName.push(val1.StateName);
                    Registrations.push(val1.Registrations);
                });
                Highcharts.chart('collegecount', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'College wise Student Registration counts'
                    },
                    credits: {
                        enabled: false
                    },
                    xAxis: {
                        categories: College_Name
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'College wise Student Registration counts'
                        },
                        stackLabels: {
                            enabled: true,
                            style: {
                                fontWeight: 'bold',
                                color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                            }
                        }
                    },
                    legend: {
                        align: 'right',
                        x: -30,
                        verticalAlign: 'top',
                        y: 25,
                        floating: true,
                        backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                        borderColor: '#CCC',
                        borderWidth: 1,
                        shadow: false
                    },
                    tooltip: {
                        headerFormat: '<b>{point.x}</b><br/>',
                        pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
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
                        name: 'Taluk_Name',
                        data: Taluk_Name,
                        color: 'dd55#fe'
                    }, {
                        name: 'DistrictName',
                        data: DistrictName,
                        color: '#90c133'
                    },
                    {
                        name: 'StateName',
                        data: StateName,
                        color: 'dd55#fe'
                    },
                     {
                         name: 'Registrations',
                         data: Registrations,
                         color: '#0000ff'
                     }]
                });
            }
        </script>
    </div>
    <hr />


    <div class="row">
        <div class="col-lg-12">

            <div class="col-lg-6 form-group">
                <div id="proposedcount" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>


            <div class="col-lg-6 form-group">

                <div id="Complitedcount" class="z-depth-1 hoverable" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
            </div>
            

        </div>
        <script>
    $(function () {
        $.ajax({
            type: "GET",
            url: "AdminAnalyticalCharts.aspx/getprojectproposedcountlist",
            data: {},
            // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //alert(response.Application);
                ListProjectproposedlist(response);
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

    function ListProjectproposedlist(response) {
        var College_Name = [];
        var Proposed = [];
        var data = [];
        $.each(response, function (i, val) {
            data = val;
            return;
        });

        $.each(data, function (i, val1) {
            College_Name.push(val1.College_Name);
            Proposed.push(val1.Proposed);


        });

        Highcharts.chart('proposedcount', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'College Wise Proposed Count List'
            },
            credits: {
                enabled: false
            },
            xAxis: {
                categories: College_Name,
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Project Proposed Counts'
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
                name: 'Proposed',
                data: Proposed,
                color: '#27a9e3'
            }]
        });
    }

   
</script>
        <script>
            $(function () {
                $.ajax({
                    type: "GET",
                    url: "AdminAnalyticalCharts.aspx/getTotalcomplitedcount",
                    data: {},
                    // data: { SandboxID: $('#ContentPlaceHolder1_ddlSandbox').val(), EmployeeID: $('#ContentPlaceHolder1_hfEmployeeID').val(), AcademicID: 0 },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //alert(response.Application);
                        ListProjectComplitedlist(response);
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

            function ListProjectComplitedlist(response) {
                var College_Name = [];
                var Completed = [];
                var data = [];
                $.each(response, function (i, val) {
                    data = val;
                    return;
                });

                $.each(data, function (i, val1) {
                    College_Name.push(val1.College_Name);
                    Completed.push(val1.Completed);


                });

                Highcharts.chart('Complitedcount', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'College Wise Completed Count List'
                    },
                    credits: {
                        enabled: false
                    },
                    xAxis: {
                        categories: College_Name,//['DKF', 'DFP'],
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Project Completed counts'
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
                        name: 'Completed',
                        data: Completed,
                        color: '#28b779'
                    }]
                });
            }
        </script>
    </div>
    <hr />
     <div class="row" style="background-color:white;">
        <div class="col-md-12">
            <iframe style="height:600px;width:100%;" src="https://app.powerbi.com/view?r=eyJrIjoiNmVhMjA0Y2EtZDZjNC00NjBlLWJjMGYtMjI1ZjM0OGQwMjgzIiwidCI6IjYzODU5MGQ2LWIyZjQtNGE2ZC04YTcxLTFiMGU5ZDkwOTgyMyJ9" frameborder="0" allowFullScreen="true" class="z-depth-1 hoverable" ></iframe>
        </div>
    </div>
</asp:Content>

