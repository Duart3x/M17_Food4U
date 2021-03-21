<%@ Page Title="" Language="C#" MasterPageFile="~/admin/master_admin.Master" AutoEventWireup="true" CodeBehind="editarmenu.aspx.cs" Inherits="M17_Food4U.admin.editarmenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="px-5">
    <h2 class="mt-5">Editar Menu</h2>
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
           <img id="img_menu" style="max-height:200px" class="img-thumbnail" src="#" alt="Imagem Menu"  runat="server"/>
        </div>
       <div class="form-group">
            <label for="ContentPlaceHolder1_FileUpload1">Foto do menu</label>
            
            <asp:FileUpload CssClass="form-control-file" ID="FileUpload1"  onchange="UploadFile(this)" runat="server" />
        </div>
        <div class="alert alert-danger" visible="false" id="div_erro" runat="server">
            <asp:Label ID="lb_erro"  Text="" runat="server" />
        </div>
        <asp:Button CssClass="btn btn-lg btn-primary" ID="btn_confirmar" OnClick="btn_confirmar_Click" Text="Confirmar" runat="server" />
        <asp:Button CssClass="btn btn-secondary" Text="Voltar" runat="server" PostBackUrl="~/admin/restaurantemenus.aspx"/>
    </div>

    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                var url = $(fileUpload).val();
                var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
                if (fileUpload.files && fileUpload.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#ContentPlaceHolder1_img_menu').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(fileUpload.files[0]);
                }
                else {

                }

            }
        }
    </script>
</asp:Content>
