<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="pedidos.aspx.cs" Inherits="M17_Food4U.admin.pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Pedidos</h2>
    <asp:GridView OnRowDataBound="dgv_pedidos_RowDataBound" runat="server" EmptyDataText="Sem Pedidos" CssClass="table" ID="dgv_pedidos" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>

</asp:Content>
