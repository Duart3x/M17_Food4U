<%@ Page Title="" Language="C#" MasterPageFile="~/estafeta/master_estafetas.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="M17_Food4U.estafeta.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Dashboard</h2>
    <div class="p-3 border shadow">
        <p><b>Dinheiro angariado: </b><span id="lb_saldo" runat="server"></span></p>
        <hr />
        <p><b>Total pedidos finalizados: </b><span id="lb_pedidosfinalizados" runat="server"></span></p>

    </div>
</asp:Content>
