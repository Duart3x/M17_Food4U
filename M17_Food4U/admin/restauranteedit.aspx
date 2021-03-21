<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="restauranteedit.aspx.cs" Inherits="M17_Food4U.admin.restauranteedit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-5">Editar Restaurante</h2>
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
    <div>
        <asp:Button Text="Confirmar" ID="btn_confirmar" OnClick="btn_confirmar_Click" CssClass="btn btn-outline-success" runat="server" />
        <asp:Button Text="Voltar" CssClass="btn btn-secondary" PostBackUrl="~/admin/restaurantes.aspx" runat="server" />
    </div>
</asp:Content>
