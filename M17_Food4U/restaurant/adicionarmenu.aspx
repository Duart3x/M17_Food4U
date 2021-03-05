<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/restaurant_master.Master" AutoEventWireup="true" CodeBehind="adicionarmenu.aspx.cs" Inherits="M17_Food4U.restaurant.adicionarmenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="px-5">
    <h2 class="mt-5">Adicionar Menu</h2>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_nome">Nome</label>
            <asp:TextBox ID="txt_nome" runat="server" CssClass="form-control" placeholder="Nome"/>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_descricao">Descrição</label>
            <asp:TextBox ID="txt_descricao" runat="server" CssClass="form-control" placeholder="Descrição"/>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_preco">Preço</label>
            <asp:TextBox ID="txt_preco" runat="server" CssClass="form-control" placeholder="Preço"/>
        </div>
       <div class="form-group">
            <label for="ContentPlaceHolder1_FileUpload1">Foto do menu</label>

            <asp:FileUpload CssClass="form-control-file" ID="FileUpload1" runat="server" />
        </div>
        <div class="alert alert-danger" visible="false" id="div_erro" runat="server">
            <asp:Label ID="lb_erro"  Text="" runat="server" />
        </div>
        <asp:Button CssClass="btn btn-lg btn-primary" ID="btn_adicionarmenu" OnClick="btn_adicionarmenu_Click" Text="Adicionar" runat="server" />
        <asp:Button CssClass="btn btn-secondary" Text="Voltar" runat="server" PostBackUrl="~/restaurant/menus.aspx"/>
    </div>
</asp:Content>
