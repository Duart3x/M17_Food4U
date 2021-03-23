<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17_Food4U.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mt-5" id="template_restaurants" runat="server">
        <h2>Restaurantes Disponíveis</h2>
        <div class='menu_grid mt-2' id="menu_grid_restaurants" runat="server">
        </div>
    </div>
    <div class="mt-5" id="template_menus" runat="server">
        <h2>Menus Disponíveis</h2>

        <div class='menu_grid mt-2' id="menu_grid_menus" runat="server">
            
        </div>
    </div>
    <script>
        function showMenu(id) {
            document.location.href = "menu.aspx?id=" + id;
        }
        function showRestaurante(id) {
            document.location.href = "restaurante.aspx?id=" + id;
        }
    </script>
</asp:Content>
