<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="AccessLog.aspx.cs" Inherits="NFC.AccessLog1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/CSS/jquery.datetimepicker.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pt-4 px-4">
        <div class="row g-4">
            <div class="col-12 vh-100">
                <div class="bg-secondary rounded h-100 p-5">
                    <h1 class="mb-4 text-center">ACCESS TABLE</h1>
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
                            <asp:TextBox runat="server" ID="TxtSearch" ClientIDMode="Static" CssClass="form-control form-control-lg w-100" placeholder="SEARCH..."></asp:TextBox>
                        </div>
                    </div>
                    <table class="table table-striped text-center mt-4" id="access-table">
                        <thead>
                            <tr>
                                <th scope="col">Nr</th>
                                <th scope="col">User Name</th>
                                <th scope="col">Date</th>
                                <th scope="col">Access Type</th>
                                <th scope="col">Detail</th>
                                <th scope="col">Action</th>
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
