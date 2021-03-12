<%@ Page Title="" Language="C#" MasterPageFile="~/restaurant/restaurant_master.Master" AutoEventWireup="true" CodeBehind="novorestaurante.aspx.cs" Inherits="M17_Food4U.restaurant.novorestaurante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-5 mt-5">
        <h2>Criar novo restaurante</h2>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_nome_restaurante" >Nome do Restaurante</label>
            <asp:TextBox class="form-control" ID="txt_nome_restaurante" runat="server" placeholder="Nome do Restaurante" />
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_cidade" >Cidade</label>
            <asp:TextBox class="form-control" ID="txt_cidade" runat="server" placeholder="Cidade" />
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_cp" >Código Postal</label>
            <asp:TextBox class="form-control" ID="txt_cp" runat="server" placeholder="Código Postal" />
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txt_morada" >Morada</label>
            <asp:TextBox class="form-control" ID="txt_morada" runat="server" placeholder="Morada" />
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_FileUpload1" >Foto do restaurante</label>
            <asp:FileUpload CssClass="form-control-file" ID="FileUpload1" runat="server" />
        </div>
        <div class="alert alert-danger" visible="false" id="div_erro" runat="server">
            <asp:Label ID="lb_erro"  Text="" runat="server" />
        </div>

        <div class="g-recaptcha" data-sitekey="6Ldg9moaAAAAAHKq47HPojxcYrVCbFBwMIgUsZsd"></div>
        <asp:Button class="btn-lg btn-primary" runat="server" Text="Criar" ID="btn_criarrestaurante" OnClick="btn_criarrestaurante_Click" />
    </div>
</asp:Content>
