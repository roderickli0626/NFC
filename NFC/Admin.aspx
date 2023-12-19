<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="NFC.Admin1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HfAdminID" runat="server" ClientIDMode="Static" />
    <div class="container-fluid pt-4 px-4">
        <div class="row g-4">
            <div class="col-12" style="min-height: 100vh;">
                <div class="bg-secondary rounded h-100 p-5">
                    <h1 class="mb-4 text-center">Lista Operatori</h1>
                    <div class="mt-5 row">
                        <div class="col-md-4">
                            <button class="btn btn-lg btn-primary w-100 mb-2 btn-add">+ Agg. STAFF</button>
                        </div>
                        <div class="col-md-4 ms-auto">
                            <asp:TextBox runat="server" ID="TxtSearch" ClientIDMode="Static" CssClass="form-control form-control-lg w-100" placeholder="CERCA..."></asp:TextBox>
                        </div>
                    </div>
                    <table class="table table-striped text-center mt-4" id="admin-table">
                        <thead>
                            <tr>
                                <th scope="col">Nr</th>
                                <th scope="col">Nome</th>
                                <th scope="col">Email</th>
                                <th scope="col">UID</th>
                                <th scope="col">Dispositivo</th>
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
    <div class="modal fade show" id="AdminModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" data-bs-backdrop="static" aria-modal="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content bg-secondary">
                <div class="modal-header">
                    <h4 class="modal-title text-white" id="modalTitle">Admin</h4>
                </div>
                <div class="modal-body">
                    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel" ClientIDMode="Static" class="row gy-3">
                        <ContentTemplate>
                            <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="mt-lg mb-lg text-left bg-gradient" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="ReqValEmail" runat="server" ErrorMessage="Inserire un indirizzo Email." CssClass="text-bg-danger" ControlToValidate="TxtEmail" Display="None"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="PasswordValidator" runat="server" ErrorMessage="Le Password non corrispondono." Display="None"></asp:CustomValidator>
                            <asp:CustomValidator ID="EmailValidator" runat="server" ErrorMessage="Email non è corretta." Display="None"></asp:CustomValidator>
                            <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Questo indirizzo Email è già registrato." Display="None"></asp:CustomValidator>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Nome</label>
                                    <asp:TextBox runat="server" ID="TxtName" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Email</label>
                                    <asp:TextBox runat="server" ID="TxtEmail" ClientIDMode="Static" TextMode="Email" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Dispositivo</label>
                                    <asp:DropDownList runat="server" ID="ComboType1" CssClass="form-select form-select-lg" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">UID</label>
                                    <asp:TextBox runat="server" ID="TxtUID" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Password</label>
                                    <asp:TextBox runat="server" ID="TxtPassword" ClientIDMode="Static" TextMode="Password" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Conferma Password</label>
                                    <asp:TextBox runat="server" ID="TxtPasswordRepeat" ClientIDMode="Static" TextMode="Password" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Note</label>
                                    <asp:TextBox runat="server" ID="TxtNote" ClientIDMode="Static" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer modal--footer">
                    <asp:Button runat="server" ID="BtnSave" CssClass="btn btn-primary" Text="Conferma" OnClick="BtnSave_Click" />
                    <button type="button" class="btn btn-warning" data-bs-dismiss="modal">Chiudi</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script>
        $(".btn-add").click(function () {
            $("#AdminModal").modal('show');
            $(".modal-title").text("AGG. ADMIN");
            $("#HfAdminID").val("");
            $("#ValSummary").addClass("d-none");
            $("#TxtName").val("");
            $("#TxtEmail").val("");
            $("#ComboType1").val("");
            $("#TxtUID").val("");
            $("#TxtNote").val("");
            $("#TxtPassword").val("");
            $("#TxtPasswordRepeat").val("");

            return false;
        });

        var datatable = $('#admin-table').dataTable({
            "serverSide": true,
            "ajax": 'DataService.asmx/FindAdmins',
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
                "data": "Name",
            }, {
                "data": "Email",
            }, {
                "data": "UID",
            }, {
                "data": "TagType",
                "render": function (data, type, row, meta) {
                    if (data == 1) return '<p class="text-success">TELECOMANDO</p>';
                    else if (data == 2) return '<p class="text-danger">RFID</p>';
                    else if (data == 3) return '<p class="text-warning">TAG</p>';
                    else if (data == 4) return '<p class="text-white">NFC</p>';
                    else return "";
                }
            }, {
                "data": "Note",
            }, {
                "data": null,
                "render": function (data, type, row, meta) {
                    return '<div class="justify-content-center">' +
                        '<i class="fa fa-edit mt-1 btn-edit" style="font-size:20px; padding-right: 10px; color:greenyellow"></i>' +
                        '<i class="fa fa-trash mt-1 btn-delete" style="font-size:20px; padding-right: 10px; color:red"></i>' +
                        '</div > ';
                }
            }],

            "fnServerParams": function (aoData) {
                aoData.searchVal = $('#TxtSearch').val();
            },

            "rowCallback": function (row, data, index) {
                $(row).find('td').css({ 'vertical-align': 'middle' });
                $("#admin-table_wrapper").css('width', '100%');
            }
        });

        $('#TxtSearch').on('input', function () {
            datatable.fnDraw();
        });

        datatable.on('click', '.btn-edit', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            $("#AdminModal").modal('show');
            $(".modal-title").text("AGGIORNA STAFF");
            $("#HfAdminID").val(row.Id);
            $("#ValSummary").addClass("d-none");
            $("#TxtName").val(row.Name);
            $("#TxtEmail").val(row.Email);
            $("#ComboType1").val(row.TagType);
            $("#TxtUID").val(row.UID);
            $("#TxtNote").val(row.Note);
            $("#TxtPassword").val("");
            $("#TxtPasswordRepeat").val("");
        });

        datatable.on('click', '.btn-delete', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            if (!confirm("Click OK per cancellare."))
                return;

            $.ajax({
                type: "POST",
                url: 'DataService.asmx/DeleteAdmin',
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
                alert("Fallito!");
            }
        };
    </script>
</asp:Content>
