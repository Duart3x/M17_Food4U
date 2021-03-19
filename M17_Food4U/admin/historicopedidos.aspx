<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="historicopedidos.aspx.cs" Inherits="M17_Food4U.admin.historicopedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Histórico Pedidos</h2>
    <div class="border p-3 shadow">
        <asp:TreeView ID="tree_pedidos"  runat="server">

        </asp:TreeView>
    </div>

</asp:Content>