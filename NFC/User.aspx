<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="NFC.User1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/CSS/jquery.datetimepicker.min.css" />
    <link rel="stylesheet" href="Content/CSS/toastr.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HfUserID" runat="server" ClientIDMode="Static" />
    <div class="container-fluid pt-4 px-4">
        <div class="row g-4">
            <div class="col-12" style="min-height: 100vh;">
                <div class="bg-secondary rounded h-100 p-5">
                    <h1 class="mb-4 text-center">CLIENTI</h1>
                    <div class="mt-5 row">
                        <div class="col-md-3">
                            <button class="btn btn-lg btn-primary w-100 mb-2 btn-add">+ AGG. CLIENTE</button>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="ComboType" CssClass="form-select form-select-lg" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <button class="btn btn-lg btn-primary w-100 mb-2 btn-csv">Import CSV</button>
                            <asp:Button ID="BtnImportUser" ClientIDMode="Static" CssClass="d-none" runat="server" Text="Import..." OnClick="BtnImportUser_Click" OnClientClick="return onClickImport();" />
                        </div>
                        <div class="col-md-2 ms-auto">
                            <asp:TextBox runat="server" ID="TxtSearch" ClientIDMode="Static" CssClass="form-control form-control-lg w-100" placeholder="CERCA..."></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <button class="btn btn-lg btn-primary w-100 mb-2 btn-msg">INVIA MESSAGGIO GLOBALE</button>
                        </div>
                    </div>
                    <div class="d-none">
                        <asp:FileUpload ID="FileUploadCSV" runat="server" accept=".csv" ClientIDMode="Static"/>
                    </div>
                    <table class="table table-striped text-center mt-4" id="user-table">
                        <thead>
                            <tr>
                                <th scope="col">Box</th>
                                <th scope="col">Nome</th>
                                <th scope="col">Cognome</th>
                                <th scope="col">UID</th>
                                <th scope="col">Targa</th>
                                <th scope="col">Email</th>
                                <th scope="col">Dispositivo</th>
                                <th scope="col">Note</th>
                                <th scope="col">Stato</th>
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
    <div class="modal fade show" id="UserModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" data-bs-backdrop="static" aria-modal="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content bg-secondary">
                <div class="modal-header">
                    <h4 class="modal-title text-white" id="modalTitle">Utente</h4>
                </div>
                <div class="modal-body">
                    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel" ClientIDMode="Static" class="row gy-3">
                        <ContentTemplate>
                            <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="mt-lg mb-lg text-left bg-gradient" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="ReqValName" runat="server" ErrorMessage="Inserire un indirizzo Name." CssClass="text-bg-danger" ControlToValidate="TxtName" Display="None"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="ReqValType" runat="server" ErrorMessage="Inserire un indirizzo Type." CssClass="text-bg-danger" ControlToValidate="ComboType1" Display="None"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="ReqValEmail" runat="server" ErrorMessage="Inserire un indirizzo Email." CssClass="text-bg-danger" ControlToValidate="TxtEmail" Display="None"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Save Failed." Display="None"></asp:CustomValidator>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtName" class="form-label">Nome</label>
                                    <asp:TextBox runat="server" ID="TxtName" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtSurname" class="form-label">Cognome</label>
                                    <asp:TextBox runat="server" ID="TxtSurname" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
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
                                    <label for="TxtTitle" class="form-label">Targa</label>
                                    <asp:TextBox runat="server" ID="TxtTarga" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Città</label>
                                    <asp:TextBox runat="server" ID="TxtCity" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Tel. fisso</label>
                                    <asp:TextBox runat="server" ID="TxtPhone" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Mobile</label>
                                    <asp:TextBox runat="server" ID="TxtMobile" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
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
                                    <label for="TxtTitle" class="form-label">Box</label>
                                    <asp:TextBox runat="server" ID="TxtBox" ClientIDMode="Static" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Nota</label>
                                    <asp:TextBox runat="server" ID="TxtNote" ClientIDMode="Static" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="BtnSave" CssClass="btn btn-primary" Text="Conferma" OnClick="BtnSave_Click" />
                    <button type="button" class="btn btn-warning" data-bs-dismiss="modal">Chiudi</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade show" id="PlaceModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" data-bs-backdrop="static" aria-modal="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content bg-secondary">
                <div class="modal-header">
                    <h4 class="modal-title text-white">Varco</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static" class="row gy-3">
                        <ContentTemplate>
                            <asp:ValidationSummary ID="ValSummary1" runat="server" CssClass="mt-lg mb-lg text-left bg-gradient" ClientIDMode="Static" />
                            <asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="Save Failed." Display="None"></asp:CustomValidator>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Varco</label>
                                    <asp:DropDownList runat="server" ID="ComboPlace" CssClass="form-select form-select-lg" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Scadenza</label>
                                    <asp:TextBox runat="server" ID="TxtDate" CssClass="form-control form-control-lg" ClientIDMode="Static" placeholder="DAL ..."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Nota</label>
                                    <asp:TextBox runat="server" ID="TxtPlaceNote" ClientIDMode="Static" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <asp:Button runat="server" ID="BtnPlace" CssClass="btn btn-lg btn-primary w-100 mb-2 btn-add-place" ClientIDMode="Static" CausesValidation="false" Text="ADD PLACE" />
                                    <%--<button id="BtnAddPlace" class="btn btn-lg btn-primary w-100 mb-2 btn-add-place">+ ADD PLACE</button>--%>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <table class="table table-striped text-center mt-4" id="place-table">
                                    <thead>
                                        <tr>
                                            <th scope="col">Nr</th>
                                            <th scope="col">Varco</th>
                                            <th scope="col">Scadenza</th>
                                            <th scope="col">Nota</th>
                                            <th scope="col">Azione</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" data-bs-dismiss="modal">Chiudi</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade show" id="MessageModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" data-bs-backdrop="static" aria-modal="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content bg-secondary">
                <div class="modal-header">
                    <h4 class="modal-title text-white">Messaggio Whatsapp</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" ClientIDMode="Static" class="row gy-3">
                        <ContentTemplate>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="mt-lg mb-lg text-left bg-gradient" ClientIDMode="Static" />
                            <asp:CustomValidator ID="CustomValidator0" runat="server" ErrorMessage="Inserisci il messaggio." Display="None"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Save Failed." Display="None"></asp:CustomValidator>
                            <div class="col-md-12">
                                <div class="mb-3">
                                    <label for="TxtTitle" class="form-label">Messaggio</label>
                                    <asp:TextBox runat="server" ID="TxtMsg" ClientIDMode="Static" TextMode="MultiLine" Rows="3" CssClass="form-control form-control-lg"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSend" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="BtnSend" CssClass="btn btn-primary" Text="Send Message" OnClick="BtnSend_Click" CausesValidation="false" />
                    <button type="button" class="btn btn-warning" data-bs-dismiss="modal">Chiudi</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script src="Scripts/JS/jquery.datetimepicker.full.min.js"></script>
    <script src="Scripts/JS/toastr.min.js"></script>
    <script>
        $('.btn-csv').click(function () {
            $('#FileUploadCSV').click();
            return false;
        });

        $("#FileUploadCSV").change(function () {
            $("#BtnImportUser").click();
        });

        function onClickImport() {
            if (!$('#FileUploadCSV')[0].files.length) {
                alert("Please choose csv file before import.");
                return false;
            }
            return true;
        }
</script>
    <script>
        $.datetimepicker.setLocale('it');

        $("#TxtDate").datetimepicker({
            format: "d/m/Y H.i",
        });

        $("#BtnPlace").click(function () {
            //TODO
            $.ajax({
                type: "POST",
                url: 'DataService.asmx/AddPlace',
                data: {
                    userID: $('#HfUserID').val(),
                    placeID: $("#ComboPlace").val(),
                    expireDate: $("#TxtDate").val(),
                    note: $("#TxtPlaceNote").val()
                },
                success: function () {
                    placeDatatable.fnDraw();
                },
                error: function () {
                    alert("Failed!");
                }
            });
            return false;
        });
    </script>
    <script>
        $(".btn-add").click(function () {
            $("#UserModal").modal('show');
            $(".modal-title").text("AGG. USER");
            $("#HfUserID").val("");
            $("#ValSummary").addClass("d-none");
            $("#TxtName").val("");
            $("#TxtSurname").val("");
            $("#TxtEmail").val("");
            $("#ComboType1").val("");
            $("#TxtTarga").val("");
            $("#TxtCity").val("");
            $("#TxtPhone").val("");
            $("#TxtMobile").val("");
            $("#TxtUID").val("");
            $("#TxtNote").val("");
            $("#TxtBox").val("");

            return false;
        });

        var placeDatatable = $('#place-table').dataTable({
            "serverSide": true,
            "ajax": 'DataService.asmx/FindPlaces',
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
                "data": "PlaceTitle",
            }, {
                "data": "ExpireDate",
            }, {
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
                var userID = $('#HfUserID').val();
                if (userID == "") userID = 0;
                aoData.userID = userID;
            },

            "drawCallback": function (settings) {
                var api = this.api();
                var data = api.data();
                if (data.length == 0) {
                    // If empty, set the default colspan value
                    $('#place-table tbody').children().remove();
                    $('#place-table tbody').append('<tr><td colspan="5">Nessun record trovato</td></tr>');
                }
            }
        });

        placeDatatable.on('click', '.btn-delete', function (e) {
            e.preventDefault();

            var row = placeDatatable.fnGetData($(this).closest('tr'));

            if (!confirm("Click OK per cancellare."))
                return;

            $.ajax({
                type: "POST",
                url: 'DataService.asmx/DeletePlace',
                data: {
                    id: row.Id
                },
                success: function () {
                    placeDatatable.fnDraw();
                },
                error: function () {
                    alert("Failed!");
                }
            });
        });


        var datatable = $('#user-table').dataTable({
            "serverSide": true,
            "ajax": 'DataService.asmx/FindUsers',
            "dom": '<"table-responsive"t>pr',
            "autoWidth": false,
            "pageLength": 20,
            "processing": true,
            "ordering": false,
            "columns": [{
                "data": "BOX",
            }, {
                "data": "Name",
            }, {
                "data": "Surname",
            }, {
                "data": "UID",
            }, {
                "data": "Targa",
            }, {
                "data": "Email",
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
            } , {
                "data": "IsEnabled",
                "render": function (data, type, row, meta) {
                    if (data) return '<button class="btn btn-success btn-enabled">Abilitato</button>';
                    else return '<button class="btn btn-danger btn-enabled">Disabilitato</button>';
                }
            }, {
                "data": null,
                "render": function (data, type, row, meta) {
                    return '<div class="justify-content-center">' +
                        '<i class="fa fa-edit mt-1 btn-edit" style="font-size:20px; padding-right: 10px; color:greenyellow"></i>' +
                        '<i class="fa fa-trash mt-1 btn-delete" style="font-size:20px; padding-right: 10px; color:red"></i>' +
                        '<i class="fa fa-parking mt-1 btn-place" style="font-size:20px; padding-right: 10px; color:yellow"></i>' +
                        '<i class="fa fa-comment-dots mt-1 btn-msg" style="font-size:20px; padding-right: 10px; color:lightgreen"></i>' +
                        '</div > ';
                }
            }],

            "fnServerParams": function (aoData) {
                aoData.searchVal = $('#TxtSearch').val();
                aoData.type = $("#ComboType").val();
            },

            "rowCallback": function (row, data, index) {
                $(row).find('td').css({ 'vertical-align': 'middle' });
                $("#user-table_wrapper").css('width', '100%');
            }
        });

        $('#TxtSearch').on('input', function () {
            datatable.fnDraw();
        });

        $('#ComboType').change(function () {
            datatable.fnDraw();
        });

        datatable.on('click', '.btn-edit', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            $("#UserModal").modal('show');
            $(".modal-title").text("AGGIORNA USER");
            $("#HfUserID").val(row.Id);
            $("#ValSummary").addClass("d-none");
            $("#TxtName").val(row.Name);
            $("#TxtSurname").val(row.Surname);
            $("#TxtEmail").val(row.Email);
            $("#ComboType1").val(row.TagType);
            $("#TxtTarga").val(row.Targa);
            $("#TxtCity").val(row.City);
            $("#TxtPhone").val(row.Phone);
            $("#TxtMobile").val(row.Mobile);
            $("#TxtUID").val(row.UID);
            $("#TxtNote").val(row.Note);
            $("#TxtBox").val(row.BOX);
        });

        datatable.on('click', '.btn-place', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            $("#PlaceModal").modal('show');
            $(".modal-title").text("AGG. PLACE");
            $("#HfUserID").val(row.Id);
            $("#ValSummary1").addClass("d-none");
            $("#TxtDate").val("");
            $("#TxtPlaceNote").val("");

            placeDatatable.fnDraw();
        });

        datatable.on('click', '.btn-delete', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            if (!confirm("Click OK per cancellare."))
                return;

            $.ajax({
                type: "POST",
                url: 'DataService.asmx/DeleteUser',
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

        datatable.on('click', '.btn-enabled', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            $.ajax({
                type: "POST",
                url: 'DataService.asmx/EnableUser',
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

        datatable.on('click', '.btn-msg', function (e) {
            e.preventDefault();

            var row = datatable.fnGetData($(this).closest('tr'));

            $("#MessageModal").modal('show');
            $(".modal-title").text("Invia msg via WhatsApp");
            $("#HfUserID").val(row.Id);
            $("#ValidationSummary1").addClass("d-none");
            $("#TxtMsg").val("");

        });

        $(".btn-msg").click(function () {
            $("#MessageModal").modal('show');
            $(".modal-title").text("Invia msg via WhatsApp");
            $("#HfUserID").val("");
            $("#ValidationSummary1").addClass("d-none");
            $("#TxtMsg").val("");

            return false;
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
