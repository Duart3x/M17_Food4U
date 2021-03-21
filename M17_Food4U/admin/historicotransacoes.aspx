<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="historicotransacoes.aspx.cs" Inherits="M17_Food4U.admin.historicotransacoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Histórico Transações</h2>
    <asp:GridView runat="server" EmptyDataText="Sem Transações" CssClass="table" ID="dgv_transacoes" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    
    <h2 class="mt-5">Histórico Pagamentos</h2>
    <asp:GridView runat="server" EmptyDataText="Sem Pagamentos" CssClass="table" ID="dgv_pagamentos" HeaderStyle-BackColor="#212529" HeaderStyle-ForeColor="White"></asp:GridView>
    <asp:Button Text="Voltar" CssClass="btn btn-secondary" PostBackUrl="~/admin/users.aspx" runat="server" />

</asp:Content>
