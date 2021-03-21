<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="detalhespedido.aspx.cs" Inherits="M17_Food4U.admin.detalhespedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Detalhes Pedido</h2>
    <asp:GridView runat="server" EmptyDataText="Sem Informação" CssClass="table" ID="dgv_pedidos" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>

    <asp:Button Text="Voltar" CssClass="btn btn-secondary" PostBackUrl="~/admin/pedidos.aspx" runat="server" />
</asp:Content>
