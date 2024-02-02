<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="AccessLog.aspx.cs" Inherits="NFC.AccessLog1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/CSS/jquery.datetimepicker.min.css" />
    <link rel="stylesheet" href="Content/CSS/toastr.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pt-4 px-4">
        <div class="row g-4">
            <div class="col-12" style="min-height: 100vh;">
                <div class="bg-secondary rounded h-100 p-5">
                    <h1 class="mb-4 text-center">Lista degli Accessi</h1>
                    <div class="mt-5 row">
                        <div class="col-md-3 ">
                            <asp:TextBox runat="server" ID="TxtFrom" CssClass="form-control form-control-lg" ClientIDMode="Static" placeholder="DAL ..."></asp:TextBox>
                        </div>
                        <div class="col-md-3 ">
                            <asp:TextBox runat="server" ID="TxtTo" CssClass="form-control form-control-lg" ClientIDMode="Static" placeholder="AL ..."></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="ComboType" CssClass="form-select form-select-lg" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="TxtSearch" ClientIDMode="Static" CssClass="form-control form-control-lg w-100" placeholder="CERCA..."></asp:TextBox>
                        </div>
                    </div>
                    <table class="table table-striped text-center mt-4" id="access-table">
                        <thead>
                            <tr>
                                <th scope="col">Nr.</th>
                                <th scope="col">UID</th>
                                <th scope="col">Cognome</th>
                                <%--<th scope="col">Nominativo</th>--%>
                                <th scope="col">Data</th>
                                <th scope="col">Varco</th>
                                <th scope="col">Dispositivo</th>
                                <%--<th scope="col">In/Out</th>--%>
                                <th scope="col">Nota</th>
                                <th scope="col">Azione</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script src="Scripts/JS/jquery.datetimepicker.full.min.js"></script>
    <script src="Scripts/JS/toastr.min.js"></script>
    <script>
        // RealTime Notification
        var proxy = $.connection.signalRHub;

        proxy.client.receiveAccessNotification = function (message) {
            toastr.info(message);
            datatable.fnDraw();
        };

        proxy.client.receiveAccessErrorNotification = function (message) {
            toastr.error(message);
            datatable.fnDraw();
        };

        $.connection.hub.start();
    </script>
    <script>
        $.datetimepicker.setLocale('it');

        $("#TxtFrom").datetimepicker({
            format: "d/m/Y H.i",
        });

        $("#TxtTo").datetimepicker({
            format: "d/m/Y H.i",
        });

        var datatable = $('#access-table').dataTable({
            "serverSide": true,
            "ajax": 'DataService.asmx/FindAccessLogs',
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
                "data": "UID",
            }, {
                "data": "SurName",
            }, {
                "data": "AccessDate",
            }, {
                "data": "PlaceTitle",
                }, {
                    "data": "AccessType",
                    "render": function (data, type, row, meta) {
                        if (data == 1) return '<p class="text-success">TELECOMANDO</p>';
                        else if (data == 2) return '<p class="text-danger">RFID</p>';
                        else if (data == 3) return '<p class="text-warning">TAG</p>';
                        else if (data == 4) return '<p class="text-white">NFC</p>';
                        else return "";
                    }
                },
            //}, {
            //    "render": function (data, type, row, meta) {
            //        if (row.IsIn) return "In";
            //        else return "Out";
            //    }
                //}, {
            {
                "data": "Note",
            }, {
                "data": null,
                "render": function (data, type, row, meta) {
                    return '<div class="justify-content-center">' +
                        '<i class="fa fa-trash mt-1 btn-delete" style="font-size:20px; padding-right: 10px; color:red"></i>' +
                        '</div > ';
                }
            }],

            "fnServerParams": function (aoData) {
                aoData.searchVal = $('#TxtSearch').val();
                aoData.searchFrom = $('#TxtFrom').val();
                aoData.searchTo = $('#TxtTo').val();
                aoData.type = $('#ComboType').val();
            },

            "rowCallback": function (row, data, index) {
                $(row).find('td').css({ 'vertical-align': 'middle' });
                $("#access-table_wrapper").css('width', '100%');
            }
        });

        $('#TxtSearch').on('input', function () {
            datatable.fnDraw();
        });

        $('#TxtFrom, #TxtTo').change(function () {
            datatable.fnDraw();
        });

        $('#ComboType').change(function () {
            datatable.fnDraw();
        });

        datatable.on('click', '.btn-delete', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            if (!confirm("Click OK per cancellare."))
                return;

            $.ajax({
                type: "POST",
                url: 'DataService.asmx/DeleteAccessLog',
                data: {
                    id: row.Id
                },
                success: function () {
                    onSuccess({ success: true });
                },
                error: function () {
                    onSuccess({ success: false });
                }
            });
        });

        var onSuccess = function (data) {
            if (data.success) {

                datatable.fnDraw();

            } else {
                alert("Failed!");
            }
        };
    </script>
</asp:Content>
