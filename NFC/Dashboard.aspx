<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="NFC.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pt-4 px-4">
        <div class="row g-4">
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <i class="fa fa-chart-line fa-3x text-primary"></i>
                    <div class="ms-3">
                        <p class="mb-2">Current Access</p>
                        <h6 class="mb-0">12</h6>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <i class="fa fa-chart-bar fa-3x text-primary"></i>
                    <div class="ms-3">
                        <p class="mb-2">Today Access</p>
                        <h6 class="mb-0">45</h6>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <i class="fa fa-chart-area fa-3x text-primary"></i>
                    <div class="ms-3">
                        <p class="mb-2">Week Access</p>
                        <h6 class="mb-0">123</h6>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-xl-3">
                <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                    <i class="fa fa-chart-pie fa-3x text-primary"></i>
                    <div class="ms-3">
                        <p class="mb-2">Month Access</p>
                        <h6 class="mb-0">567</h6>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script>
        DrawCharts();
        function DrawCharts() {
            var ctx1 = $("#bar-chart0").get(0).getContext("2d");
            var myChart1 = new Chart(ctx1, {
                type: "bar",
                data: {
                    labels: ["Button", "RFID", "TAG", "NFC"],
                    datasets: [{
                        label: "MORNING",
                        data: [15, 30, 80, 95],
                        backgroundColor: "rgba(235, 22, 22, .7)"
                    },
                    {
                        label: "AFTERNOON",
                        data: [8, 35, 40, 75],
                        backgroundColor: "rgba(235, 22, 22, .5)"
                    },
                    {
                        label: "EVENING",
                        data: [12, 25, 45, 55],
                        backgroundColor: "rgba(235, 22, 22, .3)"
                    }
                    ]
                },
                options: {
                    responsive: true
                }
            });


            // Salse & Revenue Chart
            var ctx2 = $("#total-chart").get(0).getContext("2d");
            var myChart2 = new Chart(ctx2, {
                type: "line",
                data: {
                    labels: ["2016", "2017", "2018", "2019", "2020", "2021", "2022"],
                    datasets: [{
                        label: "BUTTON",
                        data: [15, 30, 55, 45, 70, 65, 85],
                        backgroundColor: "rgba(235, 22, 22, .7)",
                        fill: true
                    },
                    {
                        label: "RFID",
                        data: [99, 135, 170, 130, 190, 180, 270],
                        backgroundColor: "rgba(235, 22, 22, .5)",
                        fill: true
                    },
                    {
                        label: "TAG",
                        data: [89, 115, 170, 120, 150, 150, 170],
                        backgroundColor: "rgba(235, 22, 22, .3)",
                        fill: true
                    },
                    {
                        label: "NFC",
                        data: [29, 115, 100, 155, 195, 134, 233],
                        backgroundColor: "rgba(235, 22, 22, .1)",
                        fill: true
                    }]
                },
                options: {
                    responsive: true
                }
            });

            var ctx4 = $("#bar-chart").get(0).getContext("2d");
            var myChart4 = new Chart(ctx4, {
                type: "bar",
                data: {
                    labels: ["Button", "RFID", "TAG", "NFC"],
                    datasets: [{
                        label: "ALL DAY",
                        backgroundColor: [
                            "rgba(235, 22, 22, .7)",
                            "rgba(235, 22, 22, .6)",
                            "rgba(235, 22, 22, .5)",
                            "rgba(235, 22, 22, .4)",
                        ],
                        data: [55, 49, 44, 24]
                    }]
                },
                options: {
                    responsive: true
                }
            });
        };        
    </script>
</asp:Content>
