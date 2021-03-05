<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/restaurant_master.Master" AutoEventWireup="true" CodeBehind="menus.aspx.cs" Inherits="M17_Food4U.restaurant.menus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mt-5 mb-5 d-flex w-100 align-items-center justify-content-between">
        <h2>Menus</h2>
        <asp:Button Text="Adicionar" CssClass="btn btn-success py-3 px-5" runat="server" ID="btn_adicionarmenu" OnClick="btn_adicionarmenu_Click" />
    </div>
    <div>
        <asp:GridView OnRowDataBound="dgv_menus_RowDataBound" EmptyDataText="Sem Menus" CssClass="table" ID="dgv_menus" runat="server" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    </div>

</asp:Content>
