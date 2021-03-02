<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/restaurant_master.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="M17_Food4U.restaurant.dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Dashboard Restaurant</h2>
    <asp:DropDownList CssClass="form-control" ID="dp_estado_pedidos" OnSelectedIndexChanged="dp_estado_pedidos_SelectedIndexChanged" AutoPostBack="true" runat="server">
        <asp:ListItem Text="Todos" />
        <asp:ListItem Text="Em espera" />
        <asp:ListItem Text="A ser preparados" />
        <asp:ListItem Text="Concluidos" />
    </asp:DropDownList>
    <asp:TextBox OnTextChanged="txt_datepicker_TextChanged" CssClass="form-control" ID="txt_datepicker" TextMode="Date" runat="server" AutoPostBack="true" ></asp:TextBox>
    <asp:GridView ID="dgv_pedidos" runat="server" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
</asp:Content>