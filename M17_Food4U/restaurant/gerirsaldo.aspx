<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/restaurant_master.Master" AutoEventWireup="true" CodeBehind="gerirsaldo.aspx.cs" Inherits="M17_Food4U.restaurant.gerirsaldo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Saldo</h2>

    <div class="d-flex w-100 flex-column">
        <div class="w-100">
            <asp:Label Text="Levantar" runat="server" />
            <asp:TextBox runat="server" ID="txt_emaillevantar" CssClass="form-control" placeholder="Email paypal para levantar"/>
            <asp:TextBox runat="server" ID="txt_moneylevantar" CssClass="form-control mt-4" placeholder="Valor a levantar" />
        </div>
        <div>
            <asp:Button Text="Levantar Saldo" CssClass="btn btn-lg btn-outline-success mt-2" id="btn_levantarsaldo" OnClick="btn_levantarsaldo_Click" runat="server" />
        </div>
    </div>

</asp:Content>
