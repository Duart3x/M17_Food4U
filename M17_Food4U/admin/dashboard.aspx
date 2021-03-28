<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="M17_Food4U.admin.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Dashboard</h2>
    <p class="text-muted">Informações sobre o estado da aplicação</p>
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

    <h2 class="mt-5">News Letter</h2>
    <p class="text-muted">Envio de emails publicitários/informativos para os clientes</p>
    <div class="border p-3 shadow mt-4 d-flex flex-column w-100 mb-4">
        <div class="d-flex justify-content-between w-100">
            <button class="btn btn-outline-success" style="height: 300px; max-width: 300px;" id="btn_menusmaisvendidos" onserverclick="btn_menusmaisvendidos_ServerClick" runat="server">Enviar emails a publicitar os 3 menus mais vendidos</button>
            <button class="btn btn-outline-success" style="height: 300px; max-width: 300px;" id="btn_menusmenosvendidos" onserverclick="btn_menusmenosvendidos_ServerClick" runat="server">Enviar emails a publicitar os 3 menus menos vendidos</button>
            <button class="btn btn-outline-success" style="height: 300px; max-width: 300px;" id="btn_carrinhocheio" onserverclick="btn_carrinhocheio_ServerClick" runat="server">Enviar emails a avisar clientes que têm no carrinho pedidos pendentes</button>
        </div>
        <div class="d-flex justify-content-between w-100 mt-3">
            <button class="btn btn-outline-success" style="height: 300px; max-width: 300px;" id="btn_saldoplataforma" onserverclick="btn_saldoplataforma_ServerClick" runat="server">Enviar emails a clientes a avisar que têm saldo na plataforma</button>
        <!--<button class="btn btn-outline-success" style="height: 300px; max-width: 300px;"></button>
            <button class="btn btn-outline-success" style="height: 300px; max-width: 300px;"></button>-->
        </div>
    </div>
    <div id="div_emailenviado" runat="server" class="p-3 shadow border">
        
    </div>

</asp:Content>