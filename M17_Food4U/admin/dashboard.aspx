<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="M17_Food4U.admin.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Dashboard</h2>
    <div class="border p-3 shadow">
        <p><b>Saldo depositado na plataforma: </b><span id="lb_saldo" runat="server"></span></p>
        <hr />
        <p><b>Total Utilizadores Registados: </b><span id="lb_userscount" runat="server"></span></p>
        <p><b>Utilizadores: </b><span id="lb_normaluserscount" runat="server"></span></p>
        <p><b>Estafetas: </b><span id="lb_estafetascount" runat="server"></span></p>
        <p><b>Administradores: </b><span id="lb_admincount" runat="server"></span></p>
        <p><b>Donos Restaurantes: </b><span id="lb_donosrestaurantes" runat="server"></span></p>
        <hr />
        <p><b>Total Restaurantes Registados: </b><span id="lb_restaurantescount" runat="server"></span></p>
        <hr />
        <p><b>Total Pedidos: </b><span id="lb_pedidoscount" runat="server"></span></p>
        <p><b>Total Pedidos Finalizados: </b><span id="lb_pedidosfinished" runat="server"></span></p>
        <p><b>Menu mais pedido: </b><span id="lb_menumaispedido" runat="server"></span></p>
        <p><b>Menu com mais rating: </b><span id="lb_menurating" runat="server"></span></p>
        <p><b>Restaurante com mais rating: </b><span id="lb_restaurantrating" runat="server"></span></p>
    </div>
    

</asp:Content>