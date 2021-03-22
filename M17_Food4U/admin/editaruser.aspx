<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="editaruser.aspx.cs" Inherits="M17_Food4U.admin.editaruser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Editar Utilizador</h2>
    <div class="border shadow p-3">
        <asp:RadioButtonList ID="radius_list" runat="server">
            <asp:ListItem Value="0" Text="Administrador"/>
            <asp:ListItem Value="1" Text="Dono Restaurante" />
            <asp:ListItem Value="2" Text="Estafeta" />
            <asp:ListItem Value="3" Text="Utilizador" />
        </asp:RadioButtonList>
        <asp:Button Text="Confimar" id="btn_confimar" OnClick="btn_confimar_Click" CssClass="btn btn-primary btn-lg" runat="server" />
        <asp:Button Text="Voltar" CssClass="btn btn-secondary btn-lg" PostBackUrl="~/admin/users.aspx" runat="server" />
    </div>
</asp:Content>
