<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="M17_Food4U.user.perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mt-5 d-flex align-items-center justify-content-between">
        <h2>Perfil</h2>
        <asp:Button Text="Logout" CssClass="btn btn-lg btn-outline-danger" runat="server" ID="btn_logout" OnClick="btn_logout_Click" />
    </div>
    <div class="w-75 mt-4 border p-3">
        <div class="d-flex align-items-center justify-content-between mb-3">
            <label for="ContentPlaceHolder1_txt_email" class="font-weight-bold mb-0"  runat="server">Email:</label>
            <asp:TextBox runat="server" ID="txt_email" value="email@gmail.com" placeholder="email" CssClass="form-control-plaintext w-75"/>
        </div>
        <div class="d-flex align-items-center justify-content-between mb-3">
            <label for="ContentPlaceHolder1_txt_nome" class="font-weight-bold mb-0" runat="server">Nome:</label>
            <asp:TextBox runat="server" ID="txt_nome" value="Duarte Santos" placeholder="Nome Completo" CssClass="form-control-plaintext w-75"/>
        </div>
        <div class="d-flex align-items-center justify-content-between mb-3">
            <label for="ContentPlaceHolder1_txt_nif" class="font-weight-bold" runat="server">NIF:</label>
            <asp:TextBox runat="server" ID="txt_nif" value="235987654" placeholder="NIF" CssClass="form-control-plaintext w-75"/>
        </div>
        <div class="d-flex align-items-center justify-content-between">
            <label for="ContentPlaceHolder1_txt_data_nasc" class="font-weight-bold mb-0" runat="server">Data Nascimento:</label>
            <asp:TextBox runat="server" ID="txt_data_nasc" value="2003/05/13" TextMode="Date" placeholder="Data de nascimento" CssClass="form-control-plaintext w-75"/>
        </div>
        <div class="d-flex flex-row-reverse mt-3">
            <asp:Button Text="Editar" ID="btn_editar" OnClick="btn_editar_Click" CssClass="btn btn-primary" runat="server" />
            <asp:Button Text="Confirmar" ID="btn_confirmar" OnClick="btn_confirmar_Click" OnClientClick="openConfirmPassword(); return false;" CssClass="btn btn-outline-success " runat="server" Visible="false"/>
            <asp:Button Text="Cancelar" ID="btn_cancelar" OnClick="btn_cancelar_Click" CssClass="btn btn-outline-secondary mr-2" runat="server" Visible="false"/>
        </div>
    </div>
    <div class="modal fade" id="modal-confirm-password" tabindex="-1" aria-labelledby="ModalPassword" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalPassword">Confirmar Password</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" id="modal_body2">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_txt_password" runat="server">Password:</label>
                        <asp:TextBox runat="server" ID="txt_password" TextMode="Password" placeholder="Password" CssClass="form-control"/>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" runat="server" onserverclick="btn_ConfirmarPassword_ServerClick" id="btn_ConfirmarPassword" class="btn btn-success">Confirmar</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        function openConfirmPassword() {
            $('#modal-confirm-password').modal('show')
        }
    </script>
</asp:Content>
