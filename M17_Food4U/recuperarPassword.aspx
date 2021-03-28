<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="recuperarPassword.aspx.cs" Inherits="M17_Food4U.recuperarPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Recuperar Password</h2>
    Nova password: <br /> <asp:TextBox CssClass="form-control" runat="server" ID="tbPassword" TextMode="Password" /><br />
    <br />
    <asp:Button CssClass="btn btn-info" runat="server" ID="btNovaPassword" Text="Atualizar" onclick="btNovaPassword_Click" /><br />
    <br />
    <asp:Label runat="server" ID="lbErro" />
</asp:Content>
