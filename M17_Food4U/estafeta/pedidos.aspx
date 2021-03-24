<%@ Page Title="" Language="C#" MasterPageFile="~/estafeta/master_estafetas.Master" AutoEventWireup="true" CodeBehind="pedidos.aspx.cs" Inherits="M17_Food4U.estafeta.pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Pedidos em Espera</h2>
    <p class="text-muted">Só podes aceitar um pedido de cada vez</p>
    <asp:GridView OnRowDataBound="dgv_pedidos_RowDataBound" runat="server" EmptyDataText="Sem Pedidos" CssClass="table" ID="dgv_pedidos" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>

    <h2 class="mt-5">Pedido Aceite</h2>
    <p class="text-muted">Informações do pedido que aceitou</p>
    <div class="border shadow p-3 mb-3" runat="server" id="div_pedidoaceite" visible="false">
        <p><b>Cliente: </b><span id="lb_cliente" runat="server"></span></p>
        <div class="d-flex w-10 mb-4 align-items-center justify-content-between">
            <span class="mb-0"><b>Estado: </b><span id="lb_estado" runat="server"></span></span>
            <div class=" d-flex w-25">
                <asp:DropDownList CssClass="form-control" ID="dp_estados" runat="server">
                    <asp:ListItem Text="Em processamento" Value="1"/>
                    <asp:ListItem Text="A ser preparado" Value="2"/>
                    <asp:ListItem Text="A caminho" Value="3"/>
                    <asp:ListItem Text="Entregue" Value="4"/>
                </asp:DropDownList>
                <asp:Button Text="Mudar Estado" id="btn_mudarestado" OnClick="btn_mudarestado_Click" CssClass="btn btn-primary ml-2" runat="server" />
            </div>
        </div>
        <p><b>Data: </b><span id="lb_data" runat="server"></span></p>
        <p><b>Cidade: </b><span id="lb_city" runat="server"></span></p>
        <p><b>Código Postal: </b><span id="lb_cp" runat="server"></span></p>
        <p><b>Morada: </b><span id="lb_morada" runat="server"></span></p>
        <asp:GridView OnRowDataBound="dgv_pedidoinfo_RowDataBound" runat="server" EmptyDataText="Sem Menus" CssClass="table" ID="dgv_pedidoinfo" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    </div>

    
</asp:Content>
