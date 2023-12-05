﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="NFC.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/CSS/toastr.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pt-4 px-4">
        <div class="row g-4">
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <a href="#0" id="Current"><i class="fa fa-chart-line fa-3x text-primary"></i></a>
                    <div class="ms-3">
                        <p class="mb-2">Current Access</p>
                        <h6 runat="server" id="CurrentAccess" clientIDMode="static" class="mb-0">12</h6>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <a href="#0" id="Today"><i class="fa fa-chart-bar fa-3x text-primary"></i></a>
                    <div class="ms-3">
                        <p class="mb-2">Today Access</p>
                        <h6 runat="server" id="TodayAccess" clientIDMode="static" class="mb-0">45</h6>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <a href="#0" id="Week"><i class="fa fa-chart-area fa-3x text-primary"></i></a>
                    <div class="ms-3">
                        <p class="mb-2">Week Access</p>
                        <h6 runat="server" id="WeekAccess" clientIDMode="static" class="mb-0">123</h6>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <a href="#0" id="Month"><i class="fa fa-chart-pie fa-3x text-primary"></i></a>
                    <div class="ms-3">
                        <p class="mb-2">Month Access</p>
                        <h6 runat="server" id="MonthAccess" clientIDMode="static" class="mb-0">567</h6>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Chart Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row g-4">
            <div class="col-sm-12 col-xl-6">
                <div class="bg-secondary text-center rounded p-4">
                    <div class="d-flex align-items-center justify-content-between mb-4">
                        <h6 class="mb-0">WorkDay Accesses</h6>
                    </div>
                    <canvas id="bar-chart0"></canvas>
                </div>
            </div>
            <div class="col-sm-12 col-xl-6">
                <div class="bg-secondary rounded h-100 p-4">
                    <h6 class="mb-4">Weekend Accesses</h6>
                    <canvas id="bar-chart"></canvas>
                </div>
            </div>
            <div class="col-sm-12 col-xl-12">
                <div class="bg-secondary text-center rounded p-4">
                    <div class="d-flex align-items-center justify-content-between mb-4">
                        <h6 class="mb-0">Total Accesses</h6>
                        <%--<a href="">Show All</a>--%>
                    </div>
                    <canvas id="total-chart"></canvas>
                </div>
            </div>
        </div>
    </div>
    <!-- Chart End -->
    <div class="modal fade show" id="AccessModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" data-bs-backdrop="static" aria-modal="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content bg-secondary">
                <div class="modal-header">
                    <h4 class="modal-title text-white">Access Log</h4>
                </div>
                <div class="modal-body">
                    <div class="row gy-3">
                        <div class="col-md-12">
                            <table class="table table-striped text-center mt-4" id="access-table">
                                <thead>
                                    <tr>
                                        <th scope="col">Nr</th>
                                        <th scope="col">User Name</th>
                                        <th scope="col">Date</th>
                                        <th scope="col">Access Type</th>
                                        <th scope="col">Detail</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" data-bs-dismiss="modal">Chiudi</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script src="Scripts/JS/toastr.min.js"></script>
    <script>
        // RealTime Notification
        var proxy = $.connection.signalRHub;

        proxy.client.receiveAccessNotification = function (message) {
            toastr.info(message);
            DrawCharts();
            // Increase Accesses
            $("#CurrentAccess").text(parseInt($("#CurrentAccess").text())++);
            $("#TodayAccess").text(parseInt($("#TodayAccess").text())++);
            $("#WeekAccess").text(parseInt($("#WeekAccess").text())++);
            $("#MonthAccess").text(parseInt($("#MonthAccess").text())++);
        };

        $.connection.hub.start();
    </script>
    <script>
        var key = 1;

        $("#Current").click(function () {
            $("#AccessModal").modal('show');
            key = 1;
            datatable.fnDraw();
        });

        $("#Today").click(function () {
            $("#AccessModal").modal('show');
            key = 2;
            datatable.fnDraw();
        });

        $("#Week").click(function () {
            $("#AccessModal").modal('show');
            key = 3;
            datatable.fnDraw();
        });

        $("#Month").click(function () {
            $("#AccessModal").modal('show');
            key = 4;
            datatable.fnDraw();
        });

        var datatable = $('#access-table').dataTable({
            "serverSide": true,
            "ajax": 'DataService.asmx/FindDashboardAccessLogs',
            "dom": '<"table-responsive"t>pr',
            "autoWidth": false,
            "pageLength": 20,
            "processing": true,
            "ordering": false,
            "columns": [{
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            }, {
                "data": "UserName",
            }, {
                "data": "AccessDate",
            }, {
                "data": "AccessType",
                "render": function (data, type, row, meta) {
                    if (data == 1) return "Button";
                    else if (data == 2) return "RFID";
                    else if (data == 3) return "TAG";
                    else if (data == 4) return "NFC";
                    else return "";
                }
            }, {
                "data": "Detail",
            }],

            "fnServerParams": function (aoData) {
                aoData.key = key;
            },

            "rowCallback": function (row, data, index) {
                $(row).find('td').css({ 'vertical-align': 'middle' });
                $("#access-table_wrapper").css('width', '100%');
            },

            "drawCallback": function (settings) {
                var api = this.api();
                var data = api.data();
                if (data.length == 0) {
                    // If empty, set the default colspan value
                    $('#access-table tbody').children().remove();
                    $('#access-table tbody').append('<tr><td colspan="5">Nessun record trovato</td></tr>');
                }
            }
        });
    </script>
    <script>
        DrawCharts();
        function DrawCharts() {
            var ctx1 = $("#bar-chart0").get(0).getContext("2d");
            $.ajax({
                type: "GET",
                url: 'DataService.asmx/GetChart1Data',
                success: function (res) {
                    var chartData = res.data;
                    var myChart1 = new Chart(ctx1, {
                        type: "bar",
                        data: {
                            labels: ["BUTTON", "RFID", "TAG", "NFC"],
                            datasets: [{
                                label: "MORNING",
                                data: chartData[0],
                                backgroundColor: "rgba(235, 22, 22, .7)"
                            },
                            {
                                label: "AFTERNOON",
                                data: chartData[1],
                                backgroundColor: "rgba(235, 22, 22, .5)"
                            },
                            {
                                label: "EVENING",
                                data: chartData[2],
                                backgroundColor: "rgba(235, 22, 22, .3)"
                            }
                            ]
                        },
                        options: {
                            responsive: true
                        }
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle the error response
                    console.log('Error:', textStatus, errorThrown);
                }
            });

            // Salse & Revenue Chart
            var ctx2 = $("#total-chart").get(0).getContext("2d");
            $.ajax({
                type: "GET",
                url: 'DataService.asmx/GetChart2Data',
                success: function (res) {
                    var currentDate = new Date();
                    var previousMonthsDates = [];
                    for (var i = 0; i < 12; i++) {
                        var previousMonthDate = new Date(currentDate.getFullYear(), currentDate.getMonth() - i);
                        var options = { year: 'numeric', month: '2-digit' };
                        var dateString = previousMonthDate.toLocaleDateString('en-US', options).replace('/', '.');
                        previousMonthsDates.unshift(dateString);
                    }

                    var chartData = res.data;
                    var myChart2 = new Chart(ctx2, {
                        type: "line",
                        data: {
                            labels: previousMonthsDates,
                            datasets: [{
                                label: "BUTTON",
                                data: chartData[0],
                                backgroundColor: "rgba(235, 22, 22, .7)",
                                fill: true
                            },
                            {
                                label: "RFID",
                                data: chartData[1],
                                backgroundColor: "rgba(235, 22, 22, .5)",
                                fill: true
                            },
                            {
                                label: "TAG",
                                data: chartData[2],
                                backgroundColor: "rgba(235, 22, 22, .3)",
                                fill: true
                            },
                            {
                                label: "NFC",
                                data: chartData[3],
                                backgroundColor: "rgba(235, 22, 22, .1)",
                                fill: true
                            }]
                        },
                        options: {
                            responsive: true
                        }
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle the error response
                    console.log('Error:', textStatus, errorThrown);
                }
            });

            var ctx3 = $("#bar-chart").get(0).getContext("2d");
            $.ajax({
                type: "GET",
                url: 'DataService.asmx/GetChart3Data',
                success: function (res) {
                    var chartData = res.data;
                    var myChart4 = new Chart(ctx3, {
                        type: "bar",
                        data: {
                            labels: ["BUTTON", "RFID", "TAG", "NFC"],
                            datasets: [{
                                label: "ALL DAY",
                                backgroundColor: [
                                    "rgba(235, 22, 22, .7)",
                                    "rgba(235, 22, 22, .6)",
                                    "rgba(235, 22, 22, .5)",
                                    "rgba(235, 22, 22, .4)",
                                ],
                                data: chartData
                            }]
                        },
                        options: {
                            responsive: true
                        }
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle the error response
                    console.log('Error:', textStatus, errorThrown);
                }
            });
        };        
    </script>
</asp:Content>
