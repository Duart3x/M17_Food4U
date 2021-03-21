<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="estadopedido.aspx.cs" Inherits="M17_Food4U.admin.estadopedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Alterar Estado Pedido</h2>
    <div class="border shadow p-3">
        <asp:RadioButtonList ID="radius_list" runat="server">
            <asp:ListItem Value="1" Text="Em processamento"/>
            <asp:ListItem Value="2" Text="A ser preparado" />
            <asp:ListItem Value="3" Text="A caminho" />
            <asp:ListItem Value="4" Text="Entregue" />
            <asp:ListItem Value="5" Text="Cancelado" />
        </asp:RadioButtonList>
        <asp:Button Text="Confimar" id="btn_confimar" OnClick="btn_confimar_Click" CssClass="btn btn-primary btn-lg" runat="server" />
        <asp:Button Text="Voltar" CssClass="btn btn-secondary btn-lg" PostBackUrl="~/admin/pedidos.aspx" runat="server" />
    </div>
</asp:Content>
