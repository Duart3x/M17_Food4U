<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/restaurant_master.Master" AutoEventWireup="true" CodeBehind="alterarestado.aspx.cs" Inherits="M17_Food4U.restaurant.alterarestado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-items-center flex-column" style="height: 90vh;">
        <div class="shadow border p-5" style="border-radius: 5px;">
            <h2 style="font-weight: 600;">Alterar Estado Pedido</h2>
            <p >Francesinha</p>
            <asp:RadioButtonList ID="radius_list" runat="server">
                <asp:ListItem Value="1" Text="Em espera"/>
                <asp:ListItem Value="2" Text="A ser preparado" />
                <asp:ListItem Value="3" Text="Concluído" />
            </asp:RadioButtonList>
            <asp:Button ID="btn_submit" OnClick="btn_submit_Click" CssClass="btn btn-lg btn-block btn-primary" Text="Submeter" runat="server" />
        </div>
        
    </div>
    
</asp:Content>
